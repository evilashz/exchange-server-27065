using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B9 RID: 1721
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AndQuery : CompositeQuery
	{
		// Token: 0x0600455E RID: 17758 RVA: 0x0012738A File Offset: 0x0012558A
		internal AndQuery(IList<Query> queries) : base(queries)
		{
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x00127394 File Offset: 0x00125594
		public override bool IsMatch(object[] row)
		{
			for (int i = 0; i < this.Queries.Count; i++)
			{
				if (!this.Queries[i].IsMatch(row))
				{
					return false;
				}
			}
			return true;
		}
	}
}
