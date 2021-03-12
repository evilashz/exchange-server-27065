using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200003C RID: 60
	internal class Average
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000B9FD File Offset: 0x00009BFD
		internal Average()
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000BA08 File Offset: 0x00009C08
		internal long Update(long dataPoint)
		{
			long result;
			lock (this)
			{
				this.sum += dataPoint;
				result = this.sum / (this.count += 1L);
			}
			return result;
		}

		// Token: 0x040000CF RID: 207
		private long sum;

		// Token: 0x040000D0 RID: 208
		private long count;
	}
}
