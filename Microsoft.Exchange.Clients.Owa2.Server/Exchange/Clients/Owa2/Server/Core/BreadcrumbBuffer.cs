using System;
using System.Collections.Concurrent;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000094 RID: 148
	internal class BreadcrumbBuffer
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x000108AD File Offset: 0x0000EAAD
		internal BreadcrumbBuffer(int maxEntries)
		{
			this.maxEntries = maxEntries;
			this.buffer = new ConcurrentQueue<Breadcrumb>();
			this.lockObj = new object();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000108D4 File Offset: 0x0000EAD4
		public void DumpTo(StringBuilder sb)
		{
			lock (this.lockObj)
			{
				foreach (Breadcrumb breadcrumb in this.buffer)
				{
					sb.Append(breadcrumb.ToString());
				}
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00010950 File Offset: 0x0000EB50
		internal void Add(Breadcrumb entry)
		{
			this.buffer.Enqueue(entry);
			if (this.buffer.Count > this.maxEntries)
			{
				lock (this.lockObj)
				{
					while (this.buffer.Count > this.maxEntries)
					{
						Breadcrumb breadcrumb = null;
						if (!this.buffer.TryDequeue(out breadcrumb))
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x04000327 RID: 807
		private readonly int maxEntries;

		// Token: 0x04000328 RID: 808
		private readonly ConcurrentQueue<Breadcrumb> buffer;

		// Token: 0x04000329 RID: 809
		private readonly object lockObj;
	}
}
