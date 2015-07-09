namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;

	using Services;

	public class ShowWallCommand : ICommand
	{
		private readonly IUserService _userService;

		#region ctors

		public ShowWallCommand(IUserService userService)
		{
			if (null == userService)
			{
				throw new ArgumentNullException(nameof(userService));
			}

			this._userService = userService;
		}

		#endregion

		public string CommandText => "wall";

		public ICollection<string> Execute(string userName, string data)
		{
			throw new NotImplementedException();
		}
	}
}