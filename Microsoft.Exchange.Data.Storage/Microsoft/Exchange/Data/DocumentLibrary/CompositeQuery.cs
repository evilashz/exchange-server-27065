using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B8 RID: 1720
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CompositeQuery : Query
	{
		// Token: 0x0600455D RID: 17757 RVA: 0x0012737B File Offset: 0x0012557B
		protected CompositeQuery(IList<Query> queries)
		{
			this.Queries = queries;
		}

		// Token: 0x040025F6 RID: 9718
		protected readonly IList<Query> Queries;
	}
}
