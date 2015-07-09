namespace UI.Console.Entities
{
	using System.Collections.Generic;

	public class User : EntityBase
	{
		public string UserName { get; set; }

		public IList<Message> Messages { get; set; }

		public IList<User> SubscribedTo { get; set; }
	}
}
