namespace UI.Console
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Code;
	using Code.Commands;

	using StructureMap.Diagnostics;

	public class App : IApp
	{
		public const string ErrorTextInvalidInput = "Invalid input \"{0}\".";
		public const string ErrorTextCommandNotFound = "Command \"{0}\" not supported.";

		public const string DefaultCommandText = "timeline";

		private readonly IInputParser _inputParser;
		private readonly ICollection<ICommand> _commands;

		#region ctors

		public App(IInputParser inputParser, ICollection<ICommand> commands)
		{
			if (null == inputParser)
			{
				throw new ArgumentNullException(nameof(inputParser));
			}

			if (null == commands)
			{
				throw new ArgumentNullException(nameof(commands));
			}

			this._inputParser = inputParser;
			this._commands = commands;
		}

		#endregion

		public ICollection<string> ProcessInput(string input)
		{
			var parameters = this._inputParser.Parse(input);
			if (null == parameters)
			{
				return new List<string> { string.Format(ErrorTextInvalidInput, input) };
			}

			parameters.CommandText = parameters.CommandText ?? DefaultCommandText;

			var command = this.FindCommand(parameters.CommandText);
			return null == command ? new List<string> { string.Format(ErrorTextCommandNotFound, parameters.CommandText) } : command.Process(parameters.UserName, parameters.Data);
		}

		private ICommand FindCommand(string commandText)
		{
			//// TODO [FS]: implement better logic, ie. a Dictionary where Key is a CommandText. Ensure the CommandText uniqueness.
			return this._commands.FirstOrDefault(x => x.CommandText.Equals(commandText, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}