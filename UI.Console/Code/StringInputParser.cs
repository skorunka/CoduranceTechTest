namespace UI.Console.Code
{
	using System.Linq;

	public class StringInputParser : IInputParser
	{
		private const char Separator = ' ';

		public ParsedInput Parse(string input)
		{
			input = input?.Trim();

			if (string.IsNullOrWhiteSpace(input))
			{
				return null;
			}

			var parts = input.Split(new[] { Separator }, 3);

			var hasCommandText = parts.Length > 1;
			var hasData = parts.Length > 2;
			return new ParsedInput
			{
				UserName = parts.FirstOrDefault(),
				CommandText = hasCommandText ? parts[1] : null,
				Data = hasData ? parts.Last() : null
			};
		}
	}
}