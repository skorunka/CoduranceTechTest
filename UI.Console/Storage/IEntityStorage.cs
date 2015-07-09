namespace UI.Console.Storage
{
	using System.Collections.Generic;

	using Entities;

	/// <summary>
	/// Object storage. It stores the whole graph of an Entity.
	/// </summary>
	/// <typeparam name="T">Type of an Entity.</typeparam>
	public interface IEntityStorage<T> where T : EntityBase
	{
		/// <summary>
		/// Entry point for quering entities of specified type.
		/// </summary>
		IReadOnlyCollection<T> Entities { get; }

		/// <summary>
		/// Stores the whole Entity.
		/// </summary>
		/// <param name="entity">Entity to store.</param>
		void Add(T entity);
	}
}