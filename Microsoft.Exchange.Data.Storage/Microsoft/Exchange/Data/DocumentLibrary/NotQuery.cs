using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006BB RID: 1723
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotQuery : Query
	{
		// Token: 0x06004562 RID: 17762 RVA: 0x00127412 File Offset: 0x00125612
		internal NotQuery(Query query)
		{
			this.Query = query;
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x00127421 File Offset: 0x00125621
		public override bool IsMatch(object[] row)
		{
			return !this.Query.IsMatch(row);
		}

		// Token: 0x040025F7 RID: 9719
		private readonly Query Query;
	}
}
