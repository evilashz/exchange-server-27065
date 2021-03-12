using System;

namespace Microsoft.Exchange.Transport.LoggingCommon
{
	// Token: 0x02000010 RID: 16
	internal enum ThrottlingSource
	{
		// Token: 0x04000097 RID: 151
		SmtpThrottlingAgent,
		// Token: 0x04000098 RID: 152
		PrioritizationAgent,
		// Token: 0x04000099 RID: 153
		ConditionalQueuing,
		// Token: 0x0400009A RID: 154
		ProcessingQuota,
		// Token: 0x0400009B RID: 155
		QueueQuota,
		// Token: 0x0400009C RID: 156
		Journaling,
		// Token: 0x0400009D RID: 157
		ResourceManager,
		// Token: 0x0400009E RID: 158
		MailboxDelivery,
		// Token: 0x0400009F RID: 159
		MSExchangeThrottling
	}
}
