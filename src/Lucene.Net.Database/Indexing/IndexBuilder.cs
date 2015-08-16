using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Database.Utilities;
using Version = Lucene.Net.Util.Version;

namespace Lucene.Net.Database.Indexing
{
	public class IndexBuilder<TEntity> : IIndexBuilder<TEntity>
	{
        private readonly IDictionary<String, IFieldInfoBuilder<TEntity>> fieldInfoBuilders;
		private Type defaultAnalyzer;

		public IndexBuilder()
		{
			this.fieldInfoBuilders = new Dictionary<String, IFieldInfoBuilder<TEntity>>();
			this.defaultAnalyzer = typeof (StandardAnalyzer);
		}

		public IIndexBuilder<TEntity> DefaultAnalyzer<TAnalyzer>() where TAnalyzer : Analyzer
		{
			this.defaultAnalyzer = typeof(TAnalyzer);
			return this;
		}

		public StringFieldInfoBuilder<TEntity> StringProperty(Expression<Func<TEntity, String>> expression)
		{
			var fieldName = expression.GetPropertyPath();
            var fieldInfoBuilder = new StringFieldInfoBuilder<TEntity>(fieldName);

			this.fieldInfoBuilders.Add(fieldName, fieldInfoBuilder);

			return fieldInfoBuilder;
		}

		public NumericFieldInfoBuilder<TEntity, TProperty> NumericProperty<TProperty>(Expression<Func<TEntity, TProperty>> expression)
		{
			var fieldName = expression.GetPropertyPath();
			var fieldInfoBuilder = new NumericFieldInfoBuilder<TEntity, TProperty>(fieldName);

			this.fieldInfoBuilders.Add(fieldName, fieldInfoBuilder);

			return fieldInfoBuilder;
		}

		public IDictionary<String, FieldInfo> Build()
		{
			return this.fieldInfoBuilders.ToDictionary(
				x => x.Key,
				x => x.Value.Build(this.defaultAnalyzer)
			);
		}
	}
}