// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Local
namespace UI.Console.UnitTests.Storage
{
	using System;

	using NUnit.Framework;

	using Entities;
	using UI.Console.Storage;

	[TestFixture]
	public class InMemoryObjectStorageUnitTests
	{
		#region Add

		[Test, Category("Add")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Add_WhenAddingNullObject_ThrowArgumentNullException()
		{
			var storage = new InMemoryEntityStorage<TestEntity>() as IEntityStorage<TestEntity>;

			storage.Add(null);
		}

		#endregion

		private class TestEntity : EntityBase
		{
			public string Text { get; set; }
		}
	}
}
