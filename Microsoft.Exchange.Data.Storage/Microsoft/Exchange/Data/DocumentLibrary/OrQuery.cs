using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006BA RID: 1722
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OrQuery : CompositeQuery
	{
		// Token: 0x06004560 RID: 17760 RVA: 0x001273CE File Offset: 0x001255CE
		internal OrQuery(IList<Query> queries) : base(queries)
		{
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x001273D8 File Offset: 0x001255D8
		public override bool IsMatch(object[] row)
		{
			for (int i = 0; i < this.Queries.Count; i++)
			{
				if (this.Queries[i].IsMatch(row))
				{
					return true;
				}
			}
			return false;
		}
	}
}
