namespace UI.Console.Services
{
	using Entities;

	//// TODO [FS]: consider using DTOs.
	//// TODO [FS]: woudl be nice to implemend as DDD(using CQRS and EventSourcing). In fact the methods are more like an Events...

	public interface IUserService
	{
		User GetUserByUserName(string userName);

		void RegisterNewUser(string userName);

		void FollowUser(string userName, string userNameToFollow);

		void PublishMessage(string userName, string text);
	}
}
