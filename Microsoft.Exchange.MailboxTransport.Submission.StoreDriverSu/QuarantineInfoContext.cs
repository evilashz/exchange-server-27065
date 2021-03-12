using System;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200002D RID: 45
	internal class QuarantineInfoContext
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000BB8F File Offset: 0x00009D8F
		public QuarantineInfoContext(DateTime quarantineStartTime)
		{
			this.QuarantineStartTime = quarantineStartTime;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000BB9E File Offset: 0x00009D9E
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000BBA6 File Offset: 0x00009DA6
		public DateTime QuarantineStartTime { get; private set; }

		// Token: 0x040000D3 RID: 211
		public const string QuarantineStartName = "QuarantineStart";
	}
}
