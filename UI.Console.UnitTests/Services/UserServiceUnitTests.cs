// ReSharper disable InconsistentNaming
namespace UI.Console.UnitTests.Services
{
	using System;
	using System.Collections.Generic;

	using Moq;

	using NUnit.Framework;

	using Entities;
	using Console.Services;
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

		#region GetOrRegisterNewUserByUserName

		[Test, Category("GetOrRegisterNewUserByUserName")]
		public void GetOrRegisterNewUserByUserName_WhenUserDoesNotExist_RegisterNewUser()
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			storageMock.Setup(x => x.Entities)
				.Returns(new List<User>());
			storageMock.Setup(x => x.Add(It.Is<User>(p => p.UserName == "franta")))
				.Verifiable();
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			var user = service.GetOrRegisterNewUserByUserName("franta");

			Assert.IsNotNull(user);
			storageMock.Verify();
		}

		[Test, Category("GetOrRegisterNewUserByUserName")]
		public void GetOrRegisterNewUserByUserName_WhenUserExist_DoNotCreateNewOne()
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			storageMock.Setup(x => x.Entities)
				.Returns(new List<User> { new User { UserName = "franta" } });
			storageMock.Setup(x => x.Add(It.Is<User>(p => p.UserName == "franta")))
				.Throws(new AssertionException("Should not be called."));
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			var user = service.GetOrRegisterNewUserByUserName("franta");

			Assert.IsNotNull(user);
			storageMock.Verify();
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

		#region FollowUser

		[Test, Category("FollowUser")]
		public void FollowUser_WhenUserDoesNotExist_ReturnFalse()
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			storageMock.SetupGet(x => x.Entities)
				.Returns(new List<User> { new User { UserName = "john" } });
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			var result = service.FollowUser("franta", "john");

			Assert.IsFalse(result);
		}

		[Test, Category("FollowUser")]
		public void FollowUser_WhenUserToFollowDoesNotExist_ReturnFalse()
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			storageMock.SetupGet(x => x.Entities)
				.Returns(new List<User> { new User { UserName = "franta" } });
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			var result = service.FollowUser("franta", "john");

			Assert.IsFalse(result);
		}

		[Test, Category("FollowUser")]
		public void FollowUser_WhenUserWantsToFollowHimself_ReturnFalse()
		{
			var storageMock = new Mock<IEntityStorage<User>>();
			storageMock.SetupGet(x => x.Entities)
				.Returns(new List<User> { new User { UserName = "franta" } });
			var dateTimeServiceMock = new Mock<IDateTimeService>();
			var service = new UserService(storageMock.Object, dateTimeServiceMock.Object) as IUserService;

			var result = service.FollowUser("franta", "franta");

			Assert.IsFalse(result);
		}

		#endregion
	}
}