using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000005 RID: 5
	public struct PropertyCategories
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002F8A File Offset: 0x0000118A
		public PropertyCategories(int c1)
		{
			this.categoryBitmask = 1UL << c1;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002F99 File Offset: 0x00001199
		public PropertyCategories(int c1, int c2)
		{
			this.categoryBitmask = (1UL << c1 | 1UL << c2);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002FB0 File Offset: 0x000011B0
		public PropertyCategories(int c1, int c2, int c3)
		{
			this.categoryBitmask = (1UL << c1 | 1UL << c2 | 1UL << c3);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002FCF File Offset: 0x000011CF
		public PropertyCategories(int c1, int c2, int c3, int c4)
		{
			this.categoryBitmask = (1UL << c1 | 1UL << c2 | 1UL << c3 | 1UL << c4);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002FF7 File Offset: 0x000011F7
		public PropertyCategories(int c1, int c2, int c3, int c4, int c5)
		{
			this.categoryBitmask = (1UL << c1 | 1UL << c2 | 1UL << c3 | 1UL << c4 | 1UL << c5);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003028 File Offset: 0x00001228
		public PropertyCategories(params int[] c)
		{
			ulong num = 0UL;
			for (int i = 0; i < c.Length; i++)
			{
				num |= 1UL << c[i];
			}
			this.categoryBitmask = num;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003059 File Offset: 0x00001259
		public bool CheckCategory(int categoryNumber)
		{
			return 0UL != (this.categoryBitmask & 1UL << categoryNumber);
		}

		// Token: 0x0400004D RID: 77
		public static readonly PropertyCategories Empty = default(PropertyCategories);

		// Token: 0x0400004E RID: 78
		private ulong categoryBitmask;
	}
}
