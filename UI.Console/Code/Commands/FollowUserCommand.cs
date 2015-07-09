namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;

	public class FollowUserCommand : ICommand
	{
		public string CommandText => "follows";

		public ICollection<string> Process(string userName, string data)
		{
			throw new NotImplementedException();
		}
	}
}