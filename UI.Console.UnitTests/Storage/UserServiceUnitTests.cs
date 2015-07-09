// ReSharper disable InconsistentNaming
namespace UI.Console.UnitTests.Storage
{
	using System;
	using System.Collections.Generic;

	using Moq;

	using NUnit.Framework;

	using Entities;
	using Services;
	using Console.Storage;

	[TestFixture]
	public class UserServiceUnitTests
	{
		#region GetUserByUserName

		[Test, Category("GetUserByUserName")]
		[TestCase(null), TestCase(""), TestCase("\n"), TestCase(" \n"), TestCase("\n "), TestCase(" \n "), TestCase("  ")]
		[ExpectedException(typeof(ArgumentException))]
		public void GetUserByUserName_WhenUserNameIsNullOrWhiteSpace_ThrowArgumentException(string userName)
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			service.GetUserByUserName(userName);
		}

		[Test, Category("GetUserByUserName"), Description("Ensure the service does not throw Exception for not existing User")]
		public void GetUserByUserName_WhenUserDoesNotExist_ReturnNull()
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			storageMock.SetupGet(x => x.Entities)
				.Returns(new List<User>());

			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			var user = service.GetUserByUserName("non existing username");

			Assert.IsNull(user);
		}

		#endregion

		#region RegisterNewUser

		[Test, Category("RegisterNewUser")]
		[TestCase(null), TestCase(""), TestCase("\n"), TestCase(" \n"), TestCase("\n "), TestCase(" \n "), TestCase("  ")]
		[ExpectedException(typeof(ArgumentException))]
		public void RegisterNewUser_WhenUserNameIsNullOrWhiteSpace_ThrowArgumentException(string userName)
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			service.RegisterNewUser(userName);
		}

		#endregion

		#region PublishMessage

		[Test, Category("PublishMessage")]
		[TestCase(null), TestCase(""), TestCase("\n"), TestCase(" \n"), TestCase("\n "), TestCase(" \n "), TestCase("  ")]
		[ExpectedException(typeof(ArgumentException))]
		public void PublishMessage_WhenUserNameIsNullOrWhiteSpace_ThrowArgumentException(string userName)
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			service.PublishMessage(userName, "message");
		}

		[Test, Category("PublishMessage")]
		[TestCase(null), TestCase(""), TestCase("\n"), TestCase(" \n"), TestCase("\n "), TestCase(" \n "), TestCase("  ")]
		[ExpectedException(typeof(ArgumentException))]
		public void PublishMessage_WhenMessageIsNullOrWhiteSpace_ThrowArgumentException(string message)
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			service.PublishMessage("userName", message);
		}

		#endregion
	}
}