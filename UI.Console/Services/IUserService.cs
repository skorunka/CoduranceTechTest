namespace UI.Console.Services
{
	using Entities;

	//// TODO [FS]: consider using DTOs.
	//// TODO [FS]: woudl be nice to implemend as DDD(using CQRS and EventSourcing). In fact the methods are more like an Events...

	public interface IUserService
	{
		User GetUserByUserName(string userName);

		User GetOrRegisterNewUserByUserName(string userName);

		User RegisterNewUser(string userName);

		/// <summary>
		/// User can subscribe to another User's TimeLine.
		/// </summary>
		/// <param name="userName">User who wants to follow someone.</param>
		/// <param name="userNameToFollow">Target user.</param>
		/// <returns>True on success</returns>
		bool FollowUser(string userName, string userNameToFollow);

		void PublishMessage(string userName, string text);
	}
}
