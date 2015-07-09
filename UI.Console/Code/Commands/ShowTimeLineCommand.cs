namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;

	public class ShowTimeLineCommand : ICommand
	{
		public string CommandText => "timeline";

		public ICollection<string> Process(string userName, string data)
		{
			throw new NotImplementedException();
		}
	}
}