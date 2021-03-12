using System;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000044 RID: 68
	internal class ElcDataTaskContext : AssistantTaskContext
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000EE99 File Offset: 0x0000D099
		public ElcDataTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, AssistantStep step, int currentRetryNumber) : base(mailboxData, job, null)
		{
			base.Step = step;
			this.retriesAttemped = currentRetryNumber;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000EEB3 File Offset: 0x0000D0B3
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000EEBB File Offset: 0x0000D0BB
		public int RetriesAttempted
		{
			get
			{
				return this.retriesAttemped;
			}
			set
			{
				this.retriesAttemped = value;
			}
		}

		// Token: 0x04000229 RID: 553
		private int retriesAttemped;
	}
}
