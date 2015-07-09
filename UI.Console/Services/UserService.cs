namespace UI.Console.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Entities;
	using Storage;

	public class UserService : IUserService
	{
		private readonly IEntityStorage<User> _userStorage;
		private readonly IDateTimeService _dateTimeService;

		#region ctors

		public UserService(IEntityStorage<User> userStorage, IDateTimeService dateTimeService)
		{
			if (null == userStorage)
			{
				throw new ArgumentNullException(nameof(userStorage));
			}

			if (null == dateTimeService)
			{
				throw new ArgumentNullException(nameof(dateTimeService));
			}

			this._userStorage = userStorage;
			this._dateTimeService = dateTimeService;
		}

		#endregion

		public User GetUserByUserName(string userName)
		{
			userName = userName?.Trim();

			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new ArgumentException(nameof(userName));
			}

			var user = this._userStorage.Entities.FirstOrDefault(x => 0 == string.Compare(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase));
			return user;
		}

		public void RegisterNewUser(string userName)
		{
			userName = userName?.Trim();

			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new ArgumentException(nameof(userName));
			}

			this._userStorage.Add(new User { UserName = userName });
		}

		public bool FollowUser(string userName, string userNameToFollow)
		{
			if (0 == string.Compare(userName?.Trim(), userNameToFollow?.Trim(), StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}

			var user = this.GetUserByUserName(userName);
			var userToFollow = this.GetUserByUserName(userNameToFollow);

			if (null == user || null == userToFollow)
			{
				return false;
			}

			if (null == user.SubscribedTo)
			{
				//// TODO [FS]: locking???
				user.SubscribedTo = new List<User> { userToFollow };
			}
			else
			{
				user.SubscribedTo.Add(userToFollow);
			}

			return true;
		}

		public void PublishMessage(string userName, string text)
		{
			text = text?.Trim();
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException(nameof(text));
			}

			var user = this.GetUserByUserName(userName);

			if (null == user)
			{
				return;
			}

			var message = new Message { Text = text, TimeStampUtc = this._dateTimeService.UtcNow };
			if (null == user.Messages)
			{
				//// TODO [FS]: locking???
				user.Messages = new List<Message> { message };
			}
			else
			{
				user.Messages.Add(new Message { Text = text, TimeStampUtc = this._dateTimeService.UtcNow });
			}

			this._userStorage.Save(user);
		}
	}
}