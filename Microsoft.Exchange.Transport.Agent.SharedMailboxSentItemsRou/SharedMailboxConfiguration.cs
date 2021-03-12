using System;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x02000008 RID: 8
	internal class SharedMailboxConfiguration
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000022D4 File Offset: 0x000004D4
		public SharedMailboxConfiguration(bool isSharedMailbox, SharedMailboxSentItemBehavior sentAsBehavior, SharedMailboxSentItemBehavior sentOnBehalfOfBehavior)
		{
			this.IsSharedMailbox = isSharedMailbox;
			this.SentAsBehavior = sentAsBehavior;
			this.SentOnBehalfOfBehavior = sentOnBehalfOfBehavior;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022F1 File Offset: 0x000004F1
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022F9 File Offset: 0x000004F9
		public bool IsSharedMailbox { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002302 File Offset: 0x00000502
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000230A File Offset: 0x0000050A
		public SharedMailboxSentItemBehavior SentAsBehavior { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002313 File Offset: 0x00000513
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000231B File Offset: 0x0000051B
		public SharedMailboxSentItemBehavior SentOnBehalfOfBehavior { get; private set; }
	}
}
