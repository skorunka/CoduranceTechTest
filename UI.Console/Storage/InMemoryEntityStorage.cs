namespace UI.Console.Storage
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Concurrent;
	using System.Linq;

	using Entities;

	public class InMemoryEntityStorage<T> : IEntityStorage<T> where T : EntityBase
	{
		private readonly ConcurrentBag<T> _objects;

		public InMemoryEntityStorage()
		{
			this._objects = new ConcurrentBag<T>();
		}

		public IReadOnlyCollection<T> Entities => this._objects.ToList();

		public void Add(T entity)
		{
			if (null == entity)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			this._objects.Add(entity);
		}
	}
}