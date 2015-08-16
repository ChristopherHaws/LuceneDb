using System.Collections.Generic;
using Lucene.Net.Documents;

namespace Lucene.Net.Database.Builders
{
	public interface IFieldBuilder
	{
		IList<IFieldable> Build();
	}
}