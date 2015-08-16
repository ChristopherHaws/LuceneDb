using System;
using System.Collections.Generic;
using Lucene.Net.Documents;

namespace Lucene.Net.Database.Builders
{
	internal class StringFieldBuilder : IFieldBuilder
	{
		private readonly String name;
		private readonly String value;

		public StringFieldBuilder(String name, String value)
		{
			this.name = name;
			this.value = value;
		}

		public IList<IFieldable> Build()
		{
			return new List<IFieldable>
			{
				new Field(this.name, this.value, Field.Store.YES, Field.Index.ANALYZED)
			};
		}
	}
}