// ReSharper disable InconsistentNaming
namespace UI.Console.UnitTests.Code
{
	using System.Collections.Generic;
	using System.Linq;

	using Moq;

	using NUnit.Framework;

	using Console.Code;
	using Console.Code.Commands;

	[TestFixture]
	public class AppUnitTests
	{
		#region ProcessInput

		[Test, Category("ProcessInput")]
		public void ProcessInput_WhenInputIsInvalid_ReturnErrorMessage()
		{
			var inputParserMock = new Mock<IInputParser>();
			inputParserMock.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns<ParsedInput>(null);

			var app = new App(inputParserMock.Object, new List<ICommand>()) as IApp;

			var result = app.ProcessInput("input text");

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count);
			Assert.AreEqual(string.Format(App.ErrorTextInvalidInput, "input text"), result.First());
		}

		[Test, Category("ProcessInput")]
		public void ProcessInput_WhenCommandExists_ProcessIt()
		{
			var inputParserMock = new Mock<IInputParser>();
			inputParserMock.Setup(x => x.Parse(It.Is<string>(p => p == "Frantisek Wall data")))
				.Returns(new ParsedInput { UserName = "Frantisek", CommandText = "Wall", Data = "data" })
				.Verifiable();

			var commandMock = new Mock<ICommand>();
			commandMock.SetupGet(x => x.CommandText).Returns("Wall").Verifiable();
			commandMock.Setup(x => x.Execute(It.Is<string>(p => p == "Frantisek"), It.Is<string>(p => p == "data")))
				.Returns(new List<string>())
				.Verifiable();

			var app = new App(inputParserMock.Object, new List<ICommand> { commandMock.Object }) as IApp;

			var result = app.ProcessInput("Frantisek Wall data");

			inputParserMock.Verify();
			commandMock.Verify();
			Assert.IsEmpty(result);
		}

		[Test, Category("ProcessInput")]
		public void ProcessInput_WhenCommandDoesNotExist_ReturnErrorMessage()
		{
			var inputParserMock = new Mock<IInputParser>();
			inputParserMock.Setup(x => x.Parse(It.Is<string>(p => p == "Frantisek Wall data")))
				.Returns(new ParsedInput { UserName = "Frantisek", CommandText = "Wall", Data = "data" })
				.Verifiable();

			var commandMock = new Mock<ICommand>();
			commandMock.SetupGet(x => x.CommandText).Returns("Follow").Verifiable();
			commandMock.Setup(x => x.Execute(It.Is<string>(p => p == "Frantisek"), It.Is<string>(p => p == "data")))
				.Throws(new AssertionException("Should not be called."));

			var app = new App(inputParserMock.Object, new List<ICommand> { commandMock.Object }) as IApp;

			var result = app.ProcessInput("Frantisek Wall data");

			inputParserMock.Verify();
			commandMock.Verify();
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count);
			Assert.AreEqual(string.Format(App.ErrorTextCommandNotFound, "Wall"), result.First());
		}

		[Test, Category("ProcessInput")]
		[TestCase("wall"), TestCase("WALL"), TestCase("wALl")]
		public void ProcessInput_EnsureTheCommandTextIsCaseInsensitiveAndTrimmed(string commandText)
		{
			var inputParserMock = new Mock<IInputParser>();
			inputParserMock.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ParsedInput { UserName = "Frantisek", CommandText = commandText });

			var commandMock = new Mock<ICommand>();
			commandMock.SetupGet(x => x.CommandText).Returns("Wall").Verifiable();
			commandMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>()))
				.Verifiable();

			var app = new App(inputParserMock.Object, new List<ICommand> { commandMock.Object }) as IApp;

			app.ProcessInput($"Frantisek {commandText}");

			commandMock.Verify();
		}

		[Test, Category("ProcessInput")]
		public void ProcessInput_WhenCommandTestIsNull_UseDefaultCommand()
		{
			var inputParserMock = new Mock<IInputParser>();
			inputParserMock.Setup(x => x.Parse(It.IsAny<string>()))
				.Returns(new ParsedInput { UserName = "Frantisek", CommandText = null });

			var commandMock = new Mock<ICommand>();
			commandMock.SetupGet(x => x.CommandText).Returns(App.DefaultCommandText);
			commandMock.Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>()))
				.Verifiable();

			var app = new App(inputParserMock.Object, new List<ICommand> { commandMock.Object }) as IApp;

			app.ProcessInput("Frantisek");

			commandMock.Verify();
		}

		#endregion
	}
}