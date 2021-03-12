using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000222 RID: 546
	[Flags]
	public enum UMEnabledFlags
	{
		// Token: 0x04000C85 RID: 3205
		None = 0,
		// Token: 0x04000C86 RID: 3206
		UMEnabled = 1,
		// Token: 0x04000C87 RID: 3207
		FaxEnabled = 2,
		// Token: 0x04000C88 RID: 3208
		TUIAccessToCalendarEnabled = 4,
		// Token: 0x04000C89 RID: 3209
		TUIAccessToEmailEnabled = 8,
		// Token: 0x04000C8A RID: 3210
		SubscriberAccessEnabled = 16,
		// Token: 0x04000C8B RID: 3211
		TUIAccessToAddressBookEnabled = 32,
		// Token: 0x04000C8C RID: 3212
		AnonymousCallersCanLeaveMessages = 256,
		// Token: 0x04000C8D RID: 3213
		ASREnabled = 512,
		// Token: 0x04000C8E RID: 3214
		VoiceMailAnalysisEnabled = 1024,
		// Token: 0x04000C8F RID: 3215
		Default = 830
	}
}
