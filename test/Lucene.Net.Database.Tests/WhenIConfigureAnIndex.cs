using System;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Database.Indexing;
using Xunit;

namespace Lucene.Net.Database.Tests
{
	public class WhenIConfigureAnIndex
	{
		[Fact]
		public void ThenIWantFieldsToBeIndexed()
		{
			// Arrange
			var index = new TestIndex();
			var builder = new IndexBuilder<TestEntity>();

			// Act
			index.Configure(builder);

			// Assert
			foreach (var field in builder.Build())
			{
				if (field.Key.Contains("Indexed"))
				{
					Assert.True(field.Value.IsIndexed);
				}
				else
				{
					Assert.False(field.Value.IsIndexed);
				}
			}
		}

		[Fact]
		public void ThenIWantFieldsToBeStored()
		{
			// Arrange
			var index = new TestIndex();
			var builder = new IndexBuilder<TestEntity>();

			// Act
			index.Configure(builder);

			// Assert
			foreach (var field in builder.Build())
			{
				if (field.Key.Contains("Stored"))
				{
					Assert.True(field.Value.IsStored);
				}
				else
				{
					Assert.False(field.Value.IsStored);
				}
			}
		}

		[Fact]
		public void ThenIWantFieldsToBeAnalyzed()
		{
			// Arrange
			var index = new TestIndex();
			var builder = new IndexBuilder<TestEntity>();

			// Act
			index.Configure(builder);

			// Assert
			foreach (var field in builder.Build())
			{
				if (field.Key.Contains("Analyzed"))
				{
					Assert.True(field.Value.IsAnalyzed);

					if (field.Key.Contains("SimpleAnalyzed"))
					{
						Assert.Equal(typeof(SimpleAnalyzer), field.Value.Analyzer);
					}
					else
					{
						Assert.Equal(typeof(StandardAnalyzer), field.Value.Analyzer);
					}
				}
				else
				{
					Assert.False(field.Value.IsAnalyzed);
					Assert.Null(field.Value.Analyzer);
				}
				
			}
		}

		[Fact]
		public void ThenIWantChildFieldsExist()
		{
			// Arrange
			var index = new TestIndex();
			var builder = new IndexBuilder<TestEntity>();

			// Act
			index.Configure(builder);

			// Assert
			var fields = builder.Build();

			Assert.True(fields.ContainsKey("Child.String"));
		}

		[Fact]
		public void ThenIWantFieldsToOmitNorms()
		{
			// Arrange
			var index = new TestIndex();
			var builder = new IndexBuilder<TestEntity>();

			// Act
			index.Configure(builder);

			// Assert
			foreach (var field in builder.Build())
			{
				if (field.Key.Contains("WithoutNorms"))
				{
					Assert.True(field.Value.WithoutNorms);
				}
				else
				{
					Assert.False(field.Value.WithoutNorms);
				}
			}
		}
	}


	public class TestIndex : ILuceneIndex<TestEntity>
	{
		public void Configure(IIndexBuilder<TestEntity> builder)
		{
			builder.DefaultAnalyzer<StandardAnalyzer>();
            builder.StringProperty(x => x.StoredString).Stored();
			builder.StringProperty(x => x.StoredIndexedString).Stored().Indexed();
			builder.StringProperty(x => x.StoredIndexedWithoutNormsString).Stored().Indexed().WithoutNorms();
			builder.StringProperty(x => x.StoredIndexedAnalyzedString).Stored().Indexed().Analyzed();
			builder.StringProperty(x => x.StoredIndexedAnalyzedWithoutNormsString).Stored().Indexed().Analyzed().WithoutNorms();
			builder.StringProperty(x => x.StoredIndexedSimpleAnalyzedString).Stored().Indexed().Analyzed<SimpleAnalyzer>();
			builder.StringProperty(x => x.StoredIndexedSimpleAnalyzedWithoutNormsString).Stored().Indexed().Analyzed<SimpleAnalyzer>().WithoutNorms();
			builder.StringProperty(x => x.String);
			builder.StringProperty(x => x.IndexedString).Indexed();
			builder.StringProperty(x => x.IndexedWithoutNormsString).Indexed().WithoutNorms();
			builder.StringProperty(x => x.IndexedAnalyzedString).Indexed().Analyzed();
			builder.StringProperty(x => x.IndexedAnalyzedWithoutNormsString).Indexed().Analyzed().WithoutNorms();
			builder.StringProperty(x => x.IndexedSimpleAnalyzedString).Indexed().Analyzed<SimpleAnalyzer>();
			builder.StringProperty(x => x.IndexedSimpleAnalyzedWithoutNormsString).Indexed().Analyzed<SimpleAnalyzer>().WithoutNorms();
			builder.NumericProperty(x => x.StoredInt32).Stored();
			builder.NumericProperty(x => x.StoredIndexedInt32).Stored().Indexed();
			builder.NumericProperty(x => x.Int32);
			builder.NumericProperty(x => x.IndexedInt32).Indexed();
			builder.StringProperty(x => x.Child.String);
		}
	}

	public class TestEntity
	{
		public String StoredString { get; set; }
		public String StoredIndexedString { get; set; }
		public String StoredIndexedWithoutNormsString { get; set; }
		public String StoredIndexedAnalyzedString { get; set; }
		public String StoredIndexedAnalyzedWithoutNormsString { get; set; }
		public String StoredIndexedSimpleAnalyzedString { get; set; }
		public String StoredIndexedSimpleAnalyzedWithoutNormsString { get; set; }
		public String String { get; set; }
		public String IndexedString { get; set; }
		public String IndexedWithoutNormsString { get; set; }
		public String IndexedAnalyzedString { get; set; }
		public String IndexedAnalyzedWithoutNormsString { get; set; }
		public String IndexedSimpleAnalyzedString { get; set; }
		public String IndexedSimpleAnalyzedWithoutNormsString { get; set; }
		public Int32 StoredInt32 { get; set; }
		public Int32 StoredIndexedInt32 { get; set; }
		public Int32 Int32 { get; set; }
		public Int32 IndexedInt32 { get; set; }
		public TestChildEntity Child { get; set; }
	}

	public class TestChildEntity
	{
		public String String { get; set; }
	}
}