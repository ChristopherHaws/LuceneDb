using System;
using System.Collections.Generic;
using Lucene.Net.Documents;

namespace Lucene.Net.Database.Builders
{
	internal class Int32FieldBuilder : IFieldBuilder
	{
		private readonly String name;
		private readonly Int32 value;

		public Int32FieldBuilder(String name, Int32 value)
		{
			this.name = name;
			this.value = value;
		}

		public IList<IFieldable> Build()
		{
			return new List<IFieldable>
			{
				new NumericField(this.name, 8, Field.Store.NO, true).SetIntValue(this.value),
				new Field(this.name, this.value.ToString(), Field.Store.YES, Field.Index.NO)
			};
		}
	}
}