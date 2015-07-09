namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;

	using Services;

	public class PostMessageCommand : ICommand
	{
		private readonly IUserService _userService;

		//// primitive locking
		private static readonly object SyncLock = new object();

		#region ctors

		public PostMessageCommand(IUserService userService)
		{
			if (null == userService)
			{
				throw new ArgumentNullException(nameof(userService));
			}

			this._userService = userService;
		}

		#endregion

		public string CommandText => "->";

		public ICollection<string> Execute(string userName, string message)
		{
			message = message?.Trim();

			if (string.IsNullOrWhiteSpace(message))
			{
				throw new ArgumentException(nameof(message));
			}

			var user = this._userService.GetUserByUserName(userName);

			#region create user if does not exist

			if (null == user)
			{
				//// double-checked locking
				lock (SyncLock)
				{
					user = this._userService.GetUserByUserName(userName);
					if (null == user)
					{
						this._userService.RegisterNewUser(userName);
						user = this._userService.GetUserByUserName(userName);
					}
				}
			}

			#endregion

			this._userService.PublishMessage(user.UserName, message);

			return null;
		}
	}
}