using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000227 RID: 551
	[Flags]
	public enum UMMailboxPolicyEnabledFlags2
	{
		// Token: 0x04000CA7 RID: 3239
		None = 0,
		// Token: 0x04000CA8 RID: 3240
		AllowFax = 1,
		// Token: 0x04000CA9 RID: 3241
		AllowTUIAccessToCalendar = 2,
		// Token: 0x04000CAA RID: 3242
		AllowTUIAccessToEmail = 4,
		// Token: 0x04000CAB RID: 3243
		AllowSubscriberAccess = 8,
		// Token: 0x04000CAC RID: 3244
		AllowTUIAccessToDirectory = 16,
		// Token: 0x04000CAD RID: 3245
		AllowASR = 32,
		// Token: 0x04000CAE RID: 3246
		AllowPlayOnPhone = 64,
		// Token: 0x04000CAF RID: 3247
		AllowVoiceMailPreview = 128,
		// Token: 0x04000CB0 RID: 3248
		AllowPersonalAutoAttendant = 256,
		// Token: 0x04000CB1 RID: 3249
		AllowMessageWaitingIndicator = 512,
		// Token: 0x04000CB2 RID: 3250
		AllowTUIAccessToPersonalContacts = 1024,
		// Token: 0x04000CB3 RID: 3251
		AllowSMSNotification = 2048,
		// Token: 0x04000CB4 RID: 3252
		AllowVoiceResponseToOtherMessageTypes = 4096,
		// Token: 0x04000CB5 RID: 3253
		InformCallerOfVoiceMailAnalysis = 8192,
		// Token: 0x04000CB6 RID: 3254
		AllowVoiceNotification = 16384,
		// Token: 0x04000CB7 RID: 3255
		Default = 65534
	}
}
