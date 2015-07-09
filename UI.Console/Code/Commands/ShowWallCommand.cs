namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

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

		public ICollection<string> Execute(string userName, string data = null)
		{
			var user = this._userService.GetUserByUserName(userName);
			if (user?.SubscribedTo == null || !user.SubscribedTo.Any())
			{
				return null;
			}

			var temp = user.Messages.Select(x => new { user.UserName, Message = x }).ToList();

			foreach (var subscription in user.SubscribedTo)
			{
				foreach (var message in subscription.Messages)
				{
					temp.Add(new { subscription.UserName, Message = message });
				}
			}

			return temp.OrderByDescending(x => x.Message.TimeStampUtc).Select(x => $"{x.UserName} - {x.Message.Text} ({x.Message.TimeStampUtc.TimeAgo()})").ToList();
		}
	}
}