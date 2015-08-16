using System;
using System.Linq;
using JetBrains.Annotations;

namespace Lucene.Net.Database.Indexing
{
	public class NumericFieldInfoBuilder<TEntity, TProperty> : IFieldInfoBuilder<TEntity>
	{
		private readonly FieldInfo fieldInfo;

		public NumericFieldInfoBuilder([NotNull] String name)
		{
			this.fieldInfo = new FieldInfo(name);

			var doubleByteTypes = new[]
			{
				typeof (Int64),
				typeof (Double)
			};

			if (doubleByteTypes.Contains(typeof(TProperty)))
			{
				this.WithPrecisionStep(8);
			}
		}

		public NumericFieldInfoBuilder<TEntity, TProperty> Stored()
		{
			this.fieldInfo.IsStored = true;
			return this;
		}

		public NumericFieldInfoBuilder<TEntity, TProperty> Indexed()
		{
			this.fieldInfo.IsIndexed = true;
			return this;
		}

		public NumericFieldInfoBuilder<TEntity, TProperty> WithPrecisionStep(Int32 precisionStep)
		{
			this.fieldInfo.PrecisionStep = precisionStep;
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