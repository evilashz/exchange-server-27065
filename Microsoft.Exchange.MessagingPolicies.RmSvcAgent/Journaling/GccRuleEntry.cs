using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000005 RID: 5
	internal struct GccRuleEntry
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002C81 File Offset: 0x00000E81
		public GccRuleEntry(Guid immutableId, string ruleName, SmtpAddress recipient, bool fullReport, DateTime? expiryDate, SmtpAddress journalEmailAddress)
		{
			this.ImmutableId = immutableId;
			this.Name = ruleName;
			this.Recipient = recipient;
			this.FullReport = fullReport;
			this.ExpiryDate = expiryDate;
			this.JournalEmailAddress = journalEmailAddress;
		}

		// Token: 0x0400000F RID: 15
		public Guid ImmutableId;

		// Token: 0x04000010 RID: 16
		public string Name;

		// Token: 0x04000011 RID: 17
		public SmtpAddress Recipient;

		// Token: 0x04000012 RID: 18
		public bool FullReport;

		// Token: 0x04000013 RID: 19
		public DateTime? ExpiryDate;

		// Token: 0x04000014 RID: 20
		public SmtpAddress JournalEmailAddress;
	}
}
