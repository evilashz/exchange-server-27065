using System;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F7 RID: 1015
	[Flags]
	public enum ScheduleReportType
	{
		// Token: 0x04001C93 RID: 7315
		SpamMailSummary = 1,
		// Token: 0x04001C94 RID: 7316
		SpamMailDetail = 2,
		// Token: 0x04001C95 RID: 7317
		MalwareMailSummary = 4,
		// Token: 0x04001C96 RID: 7318
		MalwareMailDetail = 8,
		// Token: 0x04001C97 RID: 7319
		RuleMailSummary = 16,
		// Token: 0x04001C98 RID: 7320
		RuleMailDetail = 32,
		// Token: 0x04001C99 RID: 7321
		DLPMailSummary = 64,
		// Token: 0x04001C9A RID: 7322
		DLPMailDetail = 256,
		// Token: 0x04001C9B RID: 7323
		DLPUnifiedSummary = 512,
		// Token: 0x04001C9C RID: 7324
		DLPUnifiedDetail = 1024,
		// Token: 0x04001C9D RID: 7325
		TopDLPMailHits = 4096,
		// Token: 0x04001C9E RID: 7326
		TopTransportRuleHits = 8192,
		// Token: 0x04001C9F RID: 7327
		DLPPolicyRuleHits = 16384,
		// Token: 0x04001CA0 RID: 7328
		TopSpamRecipient = 65536,
		// Token: 0x04001CA1 RID: 7329
		TopMailSender = 131072,
		// Token: 0x04001CA2 RID: 7330
		TopMailRecipient = 262144,
		// Token: 0x04001CA3 RID: 7331
		TopMalwareRecipient = 1048576,
		// Token: 0x04001CA4 RID: 7332
		TopMalware = 2097152
	}
}
