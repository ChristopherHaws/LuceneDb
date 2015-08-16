using System;
using System.Collections.Generic;
using System.Reflection;
using Lucene.Net.Documents;

namespace Lucene.Net.Database.Builders
{
	public class ReflectionFieldBuilder : IFieldBuilder
	{
		private readonly Object model;
		private readonly String name;
		private readonly PropertyInfo property;

		public ReflectionFieldBuilder(Object model, String prefix, PropertyInfo property)
		{
			this.model = model;
			this.name = String.IsNullOrWhiteSpace(prefix) ? property.Name : $"{prefix}{Constants.PropertySeparator}{property.Name}";
			this.property = property;
		}

		public IList<IFieldable> Build()
		{
			var value = this.property.GetValue(this.model, null);

			var valueString = value as String;
			if (valueString != null)
			{
				return new StringFieldBuilder(this.name, (String)valueString).Build();
			}
			
			if (value is Int32)
			{
				return new Int32FieldBuilder(this.name, (Int32) value).Build();
			}

			return new ReflectionObjectFieldBuilder(value, this.name).Build();
		}
	}
}