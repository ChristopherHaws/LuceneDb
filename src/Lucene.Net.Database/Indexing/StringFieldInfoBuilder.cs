using System;
using JetBrains.Annotations;
using Lucene.Net.Analysis;

namespace Lucene.Net.Database.Indexing
{
	public class StringFieldInfoBuilder<TEntity> : IFieldInfoBuilder<TEntity>
	{
		private readonly FieldInfo fieldInfo;

		public StringFieldInfoBuilder([NotNull] String name)
		{
			this.fieldInfo = new FieldInfo(name);
		}

		public StringFieldInfoBuilder<TEntity> Stored()
		{
			this.fieldInfo.IsStored = true;
			return this;
		}

		public StringFieldInfoBuilder<TEntity> Indexed()
		{
			this.fieldInfo.IsIndexed = true;
			return this;
		}

		public StringFieldInfoBuilder<TEntity> Analyzed()
		{
			if (!this.fieldInfo.IsIndexed)
			{
				throw new InvalidOperationException($"Field '{this.fieldInfo.Name}' must be indexed to be analyzed.");
			}

			this.fieldInfo.IsAnalyzed = true;
			return this;
		}

		public StringFieldInfoBuilder<TEntity> Analyzed<TAnalyzer>() where TAnalyzer : Analyzer
		{
			this.fieldInfo.IsAnalyzed = true;
			this.fieldInfo.Analyzer = typeof(TAnalyzer);
			return this;
		}

		public StringFieldInfoBuilder<TEntity> WithoutNorms()
		{
			if (!this.fieldInfo.IsIndexed)
			{
				throw new InvalidOperationException($"Field '{this.fieldInfo.Name}' must be indexed to omit norms.");
			}

			this.fieldInfo.WithoutNorms = true;
			return this;
		}
		
		public FieldInfo Build(Type defaultAnalyzer)
		{
			if (this.fieldInfo.IsAnalyzed && this.fieldInfo.Analyzer == null)
			{
				this.fieldInfo.Analyzer = defaultAnalyzer;
			}

			return this.fieldInfo;
		}
	}
}