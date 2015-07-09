// ReSharper disable InconsistentNaming
namespace UI.Console.UnitTests.Code.Commands
{
	using Moq;

	using NUnit.Framework;

	using Console.Code.Commands;
	using Entities;
	using Services;

	[TestFixture]
	public class ShowWallCommandUnitTests
	{
		#region Execute

		[Test, Category("Execute")]
		public void Execute_WhenUserDoesNotExist_ReturnNull()
		{
			var userServiceMock = new Mock<IUserService>();
			userServiceMock.Setup(x => x.GetUserByUserName(It.IsAny<string>()))
				.Returns<User>(null);

			var showTimeLineCommand = new ShowTimeLineCommand(userServiceMock.Object);

			var result = showTimeLineCommand.Execute("franta", null);

			Assert.IsNull(result);
		}

		[Test, Category("Execute")]
		public void Execute_WhenUserIsNotFollowingAnyone_ReturnNull()
		{
			var userServiceMock = new Mock<IUserService>();
			userServiceMock.Setup(x => x.GetUserByUserName(It.IsAny<string>()))
				.Returns(new User { UserName = "franta", SubscribedTo = null });

			var showWallCommand = new ShowWallCommand(userServiceMock.Object);

			var result = showWallCommand.Execute("franta");

			Assert.IsNull(result);
		}

		#endregion
	}
}