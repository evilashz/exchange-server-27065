using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x0200000B RID: 11
	internal struct GccRuleEntry
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002F8B File Offset: 0x0000118B
		public GccRuleEntry(Guid immutableId, string ruleName, SmtpAddress recipient, bool fullReport, DateTime? expiryDate, SmtpAddress journalEmailAddress)
		{
			this.ImmutableId = immutableId;
			this.Name = ruleName;
			this.Recipient = recipient;
			this.FullReport = fullReport;
			this.ExpiryDate = expiryDate;
			this.JournalEmailAddress = journalEmailAddress;
		}

		// Token: 0x0400004C RID: 76
		public Guid ImmutableId;

		// Token: 0x0400004D RID: 77
		public string Name;

		// Token: 0x0400004E RID: 78
		public SmtpAddress Recipient;

		// Token: 0x0400004F RID: 79
		public bool FullReport;

		// Token: 0x04000050 RID: 80
		public DateTime? ExpiryDate;

		// Token: 0x04000051 RID: 81
		public SmtpAddress JournalEmailAddress;
	}
}
