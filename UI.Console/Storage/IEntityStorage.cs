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
		/// Adds the whole Entity inluding its objects graph.
		/// </summary>
		/// <param name="entity">Entity to add.</param>
		void Add(T entity);

		/// <summary>
		/// Save the entity into inlusing its objects graph.
		/// </summary>
		/// <param name="entity">Entity to save</param>
		void Save(T entity);
	}
}