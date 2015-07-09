namespace UI.Console.Services
{
	using System;

	public interface IDateTimeService
	{
		DateTime UtcNow { get; }
	}
}