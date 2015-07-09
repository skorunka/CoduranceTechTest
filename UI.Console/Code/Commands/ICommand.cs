namespace UI.Console.Code.Commands
{
	using System.Collections.Generic;

	public interface ICommand
	{
		string CommandText { get; }

		/// <summary>
		/// Execute Command and returns the output messages.
		/// </summary>
		/// <param name="userName">Who is executting the Command.</param>
		/// <param name="data">Additional payload data. Not mandatory.</param>
		/// <returns>The output messages.</returns>
		ICollection<string> Execute(string userName, string data = null);
	}
}