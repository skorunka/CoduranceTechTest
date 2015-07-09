// ReSharper disable InconsistentNaming
namespace UI.Console.UnitTests.Code.Commands
{
	using System;

	using Moq;

	using NUnit.Framework;

	using Console.Code.Commands;
	using Entities;
	using Services;

	[TestFixture]
	public class PostMessageCommandUnitTests
	{
		#region Process

		[Test, Category("Execute")]
		[TestCase(null), TestCase(""), TestCase("\n"), TestCase(" \n"), TestCase("\n "), TestCase(" \n "), TestCase("  ")]
		[ExpectedException(typeof(ArgumentException))]
		public void Execute_WhenMessageIsNullOrWhiteSpace_ThrowArgumentException(string message)
		{
			var userServiceMock = new Mock<IUserService>();
			var postMessageCommand = new PostMessageCommand(userServiceMock.Object);

			postMessageCommand.Execute("franta", message);
		}

		[Test, Category("Execute")]
		public void Execute_WhenUserDoesNotExist_RegisterNewUser()
		{
			var userServiceMock = new Mock<IUserService>();
			userServiceMock.SetupSequence(x => x.GetUserByUserName(It.IsAny<string>()))
				.Returns(null)
				.Returns(null)
				.Returns(new User { UserName = "franta" });
			userServiceMock.Setup(x => x.RegisterNewUser(It.Is<string>(p => p == "franta")))
				.Verifiable();

			var postMessageCommand = new PostMessageCommand(userServiceMock.Object);

			postMessageCommand.Execute("franta", "message");

			userServiceMock.Verify();
		}

		#endregion
	}
}