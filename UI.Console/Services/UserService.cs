namespace UI.Console.Services
{
	using System;
	using System.Linq;

	using Entities;
	using Storage;

	public class UserService : IUserService
	{
		private readonly IEntityStorage<User> _userStorage;

		#region ctors

		public UserService(IEntityStorage<User> userStorage)
		{
			if (null == userStorage)
			{
				throw new ArgumentNullException(nameof(userStorage));
			}

			this._userStorage = userStorage;
		}

		#endregion

		public User GetUserByUserName(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new ArgumentException(nameof(userName));
			}

			var user = this._userStorage.Entities.FirstOrDefault(x => string.Compare(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase) == 0);
			return user;
		}

		public void RegisterNewUser(string userName)
		{
			throw new System.NotImplementedException();
		}

		public void FollowUser(string userName, string userNameToFollow)
		{
			throw new System.NotImplementedException();
		}

		public void PublishMessage(string userName, string text)
		{
			throw new System.NotImplementedException();
		}
	}
}