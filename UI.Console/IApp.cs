namespace UI.Console
{
	using System.Collections.Generic;

	public interface IApp
	{
		/// <summary>
		/// Creates a Command from input text and process it.
		/// </summary>
		/// <param name="input">Input text with command and data.</param>
		/// <returns>The output messages.</returns>
		ICollection<string> ProcessInput(string input);
	}
}