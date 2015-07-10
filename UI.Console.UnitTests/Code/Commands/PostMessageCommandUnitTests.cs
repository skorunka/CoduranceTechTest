// ReSharper disable InconsistentNaming
namespace UI.Console.UnitTests.Code.Commands
{
	using System;

	using Moq;

	using NUnit.Framework;

	using Console.Code.Commands;
	using Console.Services;

	[TestFixture]
	public class PostMessageCommandUnitTests
	{
		#region Execute

		[Test, Category("Execute")]
		[TestCase(null), TestCase(""), TestCase("\n"), TestCase(" \n"), TestCase("\n "), TestCase(" \n "), TestCase("  ")]
		[ExpectedException(typeof(ArgumentException))]
		public void Execute_WhenMessageIsNullOrWhiteSpace_ThrowArgumentException(string message)
		{
			var userServiceMock = new Mock<IUserService>();
			var postMessageCommand = new PostMessageCommand(userServiceMock.Object);

			postMessageCommand.Execute("franta", message);
		}

		#endregion
	}
}