namespace UI.Console
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public interface ICommand
	{
		void Process();
	}

	public class Program
	{
		private static void Main(string[] args)
		{
			while (true)
			{
				var command = Console.ReadLine();
			}
		}
	}
}
