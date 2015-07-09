namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;

	public class PostMessageCommand : ICommand
	{
		public string CommandText => "->";

		public ICollection<string> Process(string userName, string data)
		{
			throw new NotImplementedException();
		}
	}
}