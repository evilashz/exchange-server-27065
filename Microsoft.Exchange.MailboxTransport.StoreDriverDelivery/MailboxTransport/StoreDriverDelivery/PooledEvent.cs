using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000033 RID: 51
	internal class PooledEvent : DisposeTrackableBase
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000C20F File Offset: 0x0000A40F
		public EventWaitHandle WaitHandle
		{
			get
			{
				return this.sessionEvent;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C217 File Offset: 0x0000A417
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PooledEvent>(this);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C21F File Offset: 0x0000A41F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.sessionEvent.Dispose();
				this.sessionEvent = null;
			}
		}

		// Token: 0x0400010C RID: 268
		private ManualResetEvent sessionEvent = new ManualResetEvent(false);
	}
}
