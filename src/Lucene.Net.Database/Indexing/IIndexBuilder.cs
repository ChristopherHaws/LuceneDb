using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lucene.Net.Analysis;

namespace Lucene.Net.Database.Indexing
{
	public interface IIndexBuilder<TEntity>
	{
		IIndexBuilder<TEntity> DefaultAnalyzer<TAnalyzer>() where TAnalyzer : Analyzer;

        StringFieldInfoBuilder<TEntity> StringProperty(Expression<Func<TEntity, String>> expression);

		NumericFieldInfoBuilder<TEntity, TProperty> NumericProperty<TProperty>(Expression<Func<TEntity, TProperty>> expression);

		IDictionary<String, FieldInfo> Build();
	}
}