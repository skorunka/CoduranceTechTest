namespace UI.Console.Services
{
	using System;

	public sealed class DateTimeService : IDateTimeService
	{
		public DateTime UtcNow => DateTime.UtcNow;
	}
}