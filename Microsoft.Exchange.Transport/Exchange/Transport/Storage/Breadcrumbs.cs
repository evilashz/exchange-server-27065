using System;
using System.Threading;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000D1 RID: 209
	internal class Breadcrumbs
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0001E278 File Offset: 0x0001C478
		internal Breadcrumbs(int size)
		{
			if (size <= 32)
			{
				if (size == 8 || size == 16 || size == 32)
				{
					goto IL_4E;
				}
			}
			else if (size == 64 || size == 128 || size == 256)
			{
				goto IL_4E;
			}
			throw new ArgumentException(Strings.BreadCrumbSize, "size");
			IL_4E:
			this.trail = new Breadcrumb[size];
			this.mask = size - 1;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001E2E8 File Offset: 0x0001C4E8
		private Breadcrumbs()
		{
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0001E2F7 File Offset: 0x0001C4F7
		public int LastFilledIndex
		{
			get
			{
				return this.index & this.mask;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001E308 File Offset: 0x0001C508
		public void Drop(Breadcrumb bc)
		{
			int num = Interlocked.Increment(ref this.index);
			this.trail[num & this.mask] = bc;
		}

		// Token: 0x040003BF RID: 959
		private readonly int mask;

		// Token: 0x040003C0 RID: 960
		private Breadcrumb[] trail;

		// Token: 0x040003C1 RID: 961
		private int index = -1;
	}
}
