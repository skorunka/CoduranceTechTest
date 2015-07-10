namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;

	using Services;

	public class PostMessageCommand : ICommand
	{
		private readonly IUserService _userService;

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

			var user = this._userService.GetOrRegisterNewUserByUserName(userName);

			this._userService.PublishMessage(user.UserName, message);

			return null;
		}
	}
}