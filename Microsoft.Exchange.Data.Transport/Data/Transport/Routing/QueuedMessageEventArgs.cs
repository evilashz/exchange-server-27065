using System;

namespace Microsoft.Exchange.Data.Transport.Routing
{
	// Token: 0x0200008D RID: 141
	public abstract class QueuedMessageEventArgs : EventArgs
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00008112 File Offset: 0x00006312
		internal QueuedMessageEventArgs()
		{
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600033F RID: 831
		public abstract MailItem MailItem { get; }
	}
}
