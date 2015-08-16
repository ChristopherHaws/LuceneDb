using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;

namespace Lucene.Net.Database.Builders
{
	public class ReflectionObjectFieldBuilder
	{
		private readonly String prefix;
		private readonly Object model;

		public ReflectionObjectFieldBuilder(Object model)
		{
			this.model = model;
			this.prefix = String.Empty;
		}
		public ReflectionObjectFieldBuilder(Object model, String prefix)
		{
			this.model = model;
			this.prefix = prefix;
		}

		public IList<IFieldable> Build()
		{
			var properties = this.model.GetType().GetProperties();

			return (
				from property in properties
				from field in new ReflectionFieldBuilder(this.model, this.prefix, property).Build()
				select field
				).ToList();
		}
	}
}