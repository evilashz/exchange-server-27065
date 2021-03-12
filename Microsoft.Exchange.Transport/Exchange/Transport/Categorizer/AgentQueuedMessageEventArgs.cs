using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001FD RID: 509
	internal class AgentQueuedMessageEventArgs : QueuedMessageEventArgs
	{
		// Token: 0x060016AD RID: 5805 RVA: 0x0005C512 File Offset: 0x0005A712
		public AgentQueuedMessageEventArgs(MailItem message)
		{
			this.message = message;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0005C521 File Offset: 0x0005A721
		public override MailItem MailItem
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x04000B5A RID: 2906
		private MailItem message;
	}
}
