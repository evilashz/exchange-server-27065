using System;
using System.Collections.Generic;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x02000097 RID: 151
	internal class ColumnValueComparer : IComparer<ColumnValue>
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x0001D404 File Offset: 0x0001B604
		private ColumnValueComparer()
		{
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001D40C File Offset: 0x0001B60C
		public static ColumnValueComparer Instance
		{
			get
			{
				return ColumnValueComparer.instance;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001D414 File Offset: 0x0001B614
		public int Compare(ColumnValue x, ColumnValue y)
		{
			return x.Columnid.CompareTo(y.Columnid);
		}

		// Token: 0x0400025B RID: 603
		private static readonly ColumnValueComparer instance = new ColumnValueComparer();
	}
}
