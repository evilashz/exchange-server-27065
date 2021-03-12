using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000D7 RID: 215
	internal class BreadcrumbBuffer
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0003948C File Offset: 0x0003768C
		internal BreadcrumbBuffer(int maxEntries)
		{
			this.maxEntries = maxEntries;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0003949C File Offset: 0x0003769C
		internal void Add(Breadcrumb entry)
		{
			if (this.buffer == null)
			{
				this.buffer = new List<Breadcrumb>(1);
			}
			if (this.buffer.Count >= this.maxEntries)
			{
				this.buffer[this.firstEntry] = entry;
				this.firstEntry = (this.firstEntry + 1) % this.maxEntries;
				return;
			}
			this.buffer.Add(entry);
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00039504 File Offset: 0x00037704
		internal int Count
		{
			get
			{
				if (this.buffer != null)
				{
					return this.buffer.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000213 RID: 531
		internal Breadcrumb this[int index]
		{
			get
			{
				if (this.buffer.Count == 0 || index < 0 || index >= this.buffer.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.buffer[(this.firstEntry + index) % this.maxEntries];
			}
		}

		// Token: 0x04000520 RID: 1312
		private List<Breadcrumb> buffer;

		// Token: 0x04000521 RID: 1313
		private int maxEntries;

		// Token: 0x04000522 RID: 1314
		private int firstEntry;
	}
}
