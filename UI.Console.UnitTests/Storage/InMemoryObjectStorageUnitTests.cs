// ReSharper disable InconsistentNaming
namespace UI.Console.UnitTests.Storage
{
	using System;

	using NUnit.Framework;

	using Entities;
	using UI.Console.Storage;

	[TestFixture]
	public class InMemoryObjectStorageUnitTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void WhenAddingNullObject_ThrowArgumentNullException()
		{
			var storage = new InMemoryEntityStorage<TestEntity>();

			storage.Add(null);
		}

		private class TestEntity : EntityBase
		{
			public string Text { get; set; }
		}
	}
}
