using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001B1 RID: 433
	internal class CategorizerItem
	{
		// Token: 0x06001414 RID: 5140 RVA: 0x00051065 File Offset: 0x0004F265
		public CategorizerItem(TransportMailItem mailItem, int stage)
		{
			this.mailItem = mailItem;
			this.stage = stage;
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0005107B File Offset: 0x0004F27B
		public TransportMailItem TransportMailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00051083 File Offset: 0x0004F283
		public int Stage
		{
			get
			{
				return this.stage;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0005108B File Offset: 0x0004F28B
		public void Reset(TransportMailItem mailItem, int stage)
		{
			this.mailItem = mailItem;
			this.stage = stage;
		}

		// Token: 0x04000A31 RID: 2609
		public const int QueuedLockedStageId = -1;

		// Token: 0x04000A32 RID: 2610
		public const int QueuedUnsafeStageId = -2;

		// Token: 0x04000A33 RID: 2611
		private TransportMailItem mailItem;

		// Token: 0x04000A34 RID: 2612
		private int stage;
	}
}
