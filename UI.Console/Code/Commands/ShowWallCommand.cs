namespace UI.Console.Code.Commands
{
	using System;
	using System.Collections.Generic;

	public class ShowWallCommand : ICommand
	{
		public string CommandText => "wall";

		public ICollection<string> Process(string userName, string data)
		{
			throw new NotImplementedException();
		}
	}
}