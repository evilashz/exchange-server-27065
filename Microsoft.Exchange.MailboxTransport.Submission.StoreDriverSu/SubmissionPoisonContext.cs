using System;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200002B RID: 43
	internal class SubmissionPoisonContext
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000B509 File Offset: 0x00009709
		public SubmissionPoisonContext(Guid resourceGuid, long mapiEventCounter)
		{
			this.ResourceGuid = resourceGuid;
			this.MapiEventCounter = mapiEventCounter;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000B51F File Offset: 0x0000971F
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000B527 File Offset: 0x00009727
		public Guid ResourceGuid { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000B530 File Offset: 0x00009730
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000B538 File Offset: 0x00009738
		public long MapiEventCounter { get; private set; }

		// Token: 0x060001CE RID: 462 RVA: 0x0000B541 File Offset: 0x00009741
		public override string ToString()
		{
			return this.ResourceGuid + ":" + this.MapiEventCounter;
		}
	}
}
