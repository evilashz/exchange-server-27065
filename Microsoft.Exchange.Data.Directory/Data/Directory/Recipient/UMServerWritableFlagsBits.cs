using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000223 RID: 547
	[Flags]
	internal enum UMServerWritableFlagsBits
	{
		// Token: 0x04000C91 RID: 3217
		None = 0,
		// Token: 0x04000C92 RID: 3218
		MissedCallNotificationEnabled = 1,
		// Token: 0x04000C93 RID: 3219
		SMSVoiceMailNotificationEnabled = 2,
		// Token: 0x04000C94 RID: 3220
		SMSMissedCallNotificationEnabled = 4,
		// Token: 0x04000C95 RID: 3221
		PinlessAccessToVoiceMailEnabled = 8
	}
}
