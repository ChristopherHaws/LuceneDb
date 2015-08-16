using System;
using Lucene.Net.Documents;

namespace Lucene.Net.Database.Builders
{
	public class ReflectionDocumentBuilder
	{
		private readonly Object model;

		public ReflectionDocumentBuilder(Object model)
		{
			this.model = model;
		}

		public Document Build()
		{
			var document = new Document();
			
			var fields = new ReflectionObjectFieldBuilder(this.model).Build();

			foreach (var field in fields)
			{
				document.Add(field);
			}

			return document;
		}
	}
}