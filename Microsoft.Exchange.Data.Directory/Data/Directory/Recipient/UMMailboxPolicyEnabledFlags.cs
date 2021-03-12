using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000226 RID: 550
	[Flags]
	public enum UMMailboxPolicyEnabledFlags
	{
		// Token: 0x04000CA0 RID: 3232
		None = 0,
		// Token: 0x04000CA1 RID: 3233
		AllowMissedCallNotifications = 1,
		// Token: 0x04000CA2 RID: 3234
		AllowSMSMessageWaitingNotifications = 2,
		// Token: 0x04000CA3 RID: 3235
		AllowVirtualNumber = 4,
		// Token: 0x04000CA4 RID: 3236
		AllowPinlessVoiceMailAccess = 8,
		// Token: 0x04000CA5 RID: 3237
		AllowVoiceMailAnalysis = 16
	}
}
