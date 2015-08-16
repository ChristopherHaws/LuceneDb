namespace Lucene.Net.Database.Indexing
{
	public interface ILuceneIndex<TEntity>
	{
		void Configure(IIndexBuilder<TEntity> builder);
	}
}