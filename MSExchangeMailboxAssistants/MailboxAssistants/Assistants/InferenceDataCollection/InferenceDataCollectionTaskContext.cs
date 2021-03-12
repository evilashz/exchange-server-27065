using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Inference.DataAnalysis;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.InferenceDataCollection
{
	// Token: 0x02000218 RID: 536
	internal class InferenceDataCollectionTaskContext : AssistantTaskContext
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x0007618F File Offset: 0x0007438F
		public InferenceDataCollectionTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, AssistantStep step, IMailboxProcessingState mailboxProcessingState) : base(mailboxData, job, null)
		{
			base.Step = step;
			this.MailboxProcessingState = mailboxProcessingState;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x000761A9 File Offset: 0x000743A9
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x000761B1 File Offset: 0x000743B1
		public IMailboxProcessingState MailboxProcessingState { get; set; }
	}
}
