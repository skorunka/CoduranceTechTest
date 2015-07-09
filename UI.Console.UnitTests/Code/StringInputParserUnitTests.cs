// ReSharper disable InconsistentNaming
namespace UI.Console.UnitTests.Code
{
	using NUnit.Framework;

	using Console.Code;

	[TestFixture]
	public class StringInputParserUnitTests
	{
		#region Parse

		[Test, Category("Parse")]
		[TestCase(null), TestCase(""), TestCase("\n"), TestCase(" \n"), TestCase("\n "), TestCase(" \n ")]
		public void Parse_WhenInputTextIsNullOrWhiteSpace_ReturnNull(string input)
		{
			var parser = new StringInputParser() as IInputParser;

			var result = parser.Parse(input);

			Assert.IsNull(result);
		}

		[Test, Category("ProcessInput")]
		[TestCase("frantisek"), TestCase("frantisek follow")]
		public void Parse_GetFirstWordAsUserName(string input)
		{
			var parser = new StringInputParser() as IInputParser;

			var result = parser.Parse(input);

			Assert.IsNotNull(result);
			Assert.AreEqual("frantisek", result.UserName);
		}

		[Test, Category("ProcessInput")]
		public void Parse_WhenThereIsJustOneWord_ReturnCommandAndDataAsNull()
		{
			var parser = new StringInputParser() as IInputParser;

			var result = parser.Parse("frantisek");

			Assert.IsNotNull(result);
			Assert.AreEqual("frantisek", result.UserName);
			Assert.IsNull(result.CommandText);
			Assert.IsNull(result.Data);
		}

		[Test, Category("ProcessInput")]
		public void Parse_GetSecondWordAsCommand()
		{
			var parser = new StringInputParser() as IInputParser;

			var result = parser.Parse("frantisek command");

			Assert.IsNotNull(result);
			Assert.AreEqual("frantisek", result.UserName);
			Assert.AreEqual("command", result.CommandText);
			Assert.IsNull(result.Data);
		}

		[Test, Category("ProcessInput")]
		[TestCase("frantisek wall data", "data"), TestCase("frantisek wall data more data", "data more data")]
		public void Parse_GetTheRestAfterSecondWordAsData(string input, string expectedData)
		{
			var parser = new StringInputParser() as IInputParser;

			var result = parser.Parse(input);

			Assert.IsNotNull(result);
			Assert.AreEqual("frantisek", result.UserName);
			Assert.AreEqual("wall", result.CommandText);
			Assert.AreEqual(expectedData, result.Data);
		}

		#endregion
	}
}