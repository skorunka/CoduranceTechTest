namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;

	using Services;

	public class FollowUserCommand : ICommand
	{
		private readonly IUserService _userService;

		#region ctors

		public FollowUserCommand(IUserService userService)
		{
			if (null == userService)
			{
				throw new ArgumentNullException(nameof(userService));
			}

			this._userService = userService;
		}

		#endregion

		public string CommandText => "follows";

		public ICollection<string> Execute(string userName, string userNameToFollow)
		{
			this._userService.FollowUser(userName, userNameToFollow);
			return null;
		}
	}
}