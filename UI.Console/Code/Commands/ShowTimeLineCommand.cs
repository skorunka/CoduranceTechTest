namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Services;

	public class ShowTimeLineCommand : ICommand
	{
		private readonly IUserService _userService;

		#region ctors

		public ShowTimeLineCommand(IUserService userService)
		{
			if (null == userService)
			{
				throw new ArgumentNullException(nameof(userService));
			}

			this._userService = userService;
		}

		#endregion

		public string CommandText => "timeline";

		public ICollection<string> Execute(string userName, string data = null)
		{
			var user = this._userService.GetUserByUserName(userName);
			////TODO[FS]: We could order messages while inserting them to USer.Messages and use ordered container.
			return user?.Messages.OrderBy(x => x.TimeStampUtc).Select(x => $"{x.Text} ({x.TimeStampUtc.TimeAgo()})").ToList();
		}
	}
}