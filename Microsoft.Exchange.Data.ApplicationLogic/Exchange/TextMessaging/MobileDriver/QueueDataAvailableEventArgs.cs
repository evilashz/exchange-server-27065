using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x020001AC RID: 428
	internal class QueueDataAvailableEventArgs<T> : EventArgs
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x00043971 File Offset: 0x00041B71
		public QueueDataAvailableEventArgs(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.Item = item;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00043993 File Offset: 0x00041B93
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x0004399B File Offset: 0x00041B9B
		public T Item { get; private set; }
	}
}
