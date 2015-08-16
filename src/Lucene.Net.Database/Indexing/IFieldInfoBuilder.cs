using System;

namespace Lucene.Net.Database.Indexing
{
	public interface IFieldInfoBuilder<TEntity>
	{
		FieldInfo Build(Type defaultAnalyzer);
	}
}