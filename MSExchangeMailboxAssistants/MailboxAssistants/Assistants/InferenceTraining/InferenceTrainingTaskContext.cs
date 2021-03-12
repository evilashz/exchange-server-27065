using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Inference.Mdb;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.InferenceTraining
{
	// Token: 0x020001C9 RID: 457
	internal sealed class InferenceTrainingTaskContext : AssistantTaskContext
	{
		// Token: 0x060011AB RID: 4523 RVA: 0x000677EE File Offset: 0x000659EE
		public InferenceTrainingTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, AssistantStep step, MailboxTrainingState mailboxTrainingState, MailboxTruthLoggingState mailboxTruthLoggingState) : base(mailboxData, job, null)
		{
			base.Step = step;
			this.MailboxTrainingState = mailboxTrainingState;
			this.MailboxTruthLoggingState = mailboxTruthLoggingState;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00067810 File Offset: 0x00065A10
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x00067818 File Offset: 0x00065A18
		public MailboxTrainingState MailboxTrainingState { get; private set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00067821 File Offset: 0x00065A21
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00067829 File Offset: 0x00065A29
		public MailboxTruthLoggingState MailboxTruthLoggingState { get; private set; }
	}
}
