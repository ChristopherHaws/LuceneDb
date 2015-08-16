using System;
using JetBrains.Annotations;

namespace Lucene.Net.Database.Indexing
{
	public class FieldInfo
	{
		public FieldInfo([NotNull] String name)
		{
			this.Name = name;
		}

		public String Name { get; set; }

		public Boolean IsStored { get; set; }

		public Boolean IsIndexed { get; set; }

		public Boolean IsAnalyzed { get; set; }

		public Boolean WithoutNorms { get; set; }

		public Type Analyzer { get; set; }

		public Int32 PrecisionStep { get; set; }
	}
}