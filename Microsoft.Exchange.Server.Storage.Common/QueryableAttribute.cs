using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000077 RID: 119
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class QueryableAttribute : Attribute
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001308C File Offset: 0x0001128C
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00013094 File Offset: 0x00011294
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001309D File Offset: 0x0001129D
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x000130A5 File Offset: 0x000112A5
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x000130AE File Offset: 0x000112AE
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x000130B6 File Offset: 0x000112B6
		public Visibility Visibility
		{
			get
			{
				return this.visibility;
			}
			set
			{
				this.visibility = value;
			}
		}

		// Token: 0x04000622 RID: 1570
		public const int DefaultIndex = -1;

		// Token: 0x04000623 RID: 1571
		public const int DefaultLength = 1048576;

		// Token: 0x04000624 RID: 1572
		private int index = -1;

		// Token: 0x04000625 RID: 1573
		private int length = 1048576;

		// Token: 0x04000626 RID: 1574
		private Visibility visibility;
	}
}
