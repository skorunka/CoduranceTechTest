namespace UI.Console.Entities
{
	using System;

	public class Message : EntityBase
	{
		public string Text { get; set; }

		public DateTime TimeStampUtc { get; set; }
	}
}