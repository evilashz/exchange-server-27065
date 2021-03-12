using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000316 RID: 790
	internal class QueueNode : Node<IQueueItem>
	{
		// Token: 0x0600222F RID: 8751 RVA: 0x00080FA0 File Offset: 0x0007F1A0
		public QueueNode(IQueueItem item) : base(item)
		{
			this.createdAt = DateTime.UtcNow.Ticks;
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x00080FC7 File Offset: 0x0007F1C7
		public long CreatedAt
		{
			get
			{
				return this.createdAt;
			}
		}

		// Token: 0x040011DA RID: 4570
		private readonly long createdAt;
	}
}
