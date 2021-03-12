using System;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200007B RID: 123
	public struct SortColumn
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x00019848 File Offset: 0x00017A48
		public SortColumn(Column column, bool ascending)
		{
			this.column = column;
			this.ascending = ascending;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00019858 File Offset: 0x00017A58
		public Column Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00019860 File Offset: 0x00017A60
		public bool Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00019868 File Offset: 0x00017A68
		public static int MaxSortColumnLength(Type type)
		{
			if (!(type == typeof(string)))
			{
				return 255;
			}
			return 127;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00019884 File Offset: 0x00017A84
		public static int MaxSortColumnLength(PropertyType propType)
		{
			if (propType != PropertyType.Unicode)
			{
				return 255;
			}
			return 127;
		}

		// Token: 0x040001A0 RID: 416
		public const int MaxSortColumnLengthBytes = 255;

		// Token: 0x040001A1 RID: 417
		private Column column;

		// Token: 0x040001A2 RID: 418
		private bool ascending;
	}
}
