using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000054 RID: 84
	internal interface IConfigurationFolder : IPromptCounter
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000311 RID: 785
		// (set) Token: 0x06000312 RID: 786
		MailboxGreetingEnum CurrentMailboxGreetingType { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000313 RID: 787
		// (set) Token: 0x06000314 RID: 788
		bool IsFirstTimeUser { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000315 RID: 789
		// (set) Token: 0x06000316 RID: 790
		bool IsOof { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000317 RID: 791
		// (set) Token: 0x06000318 RID: 792
		string PlayOnPhoneDialString { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000319 RID: 793
		// (set) Token: 0x0600031A RID: 794
		string TelephoneAccessFolderEmail { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600031B RID: 795
		// (set) Token: 0x0600031C RID: 796
		bool UseAsr { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600031D RID: 797
		// (set) Token: 0x0600031E RID: 798
		bool ReceivedVoiceMailPreviewEnabled { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600031F RID: 799
		// (set) Token: 0x06000320 RID: 800
		bool SentVoiceMailPreviewEnabled { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000321 RID: 801
		// (set) Token: 0x06000322 RID: 802
		bool ReadUnreadVoicemailInFIFOOrder { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000323 RID: 803
		// (set) Token: 0x06000324 RID: 804
		MultiValuedProperty<string> BlockedNumbers { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000325 RID: 805
		// (set) Token: 0x06000326 RID: 806
		VoiceNotificationStatus VoiceNotificationStatus { get; set; }

		// Token: 0x06000327 RID: 807
		GreetingBase OpenCustomMailboxGreeting(MailboxGreetingEnum g);

		// Token: 0x06000328 RID: 808
		GreetingBase OpenNameGreeting();

		// Token: 0x06000329 RID: 809
		IPassword OpenPassword();

		// Token: 0x0600032A RID: 810
		void RemoveCustomMailboxGreeting(MailboxGreetingEnum g);

		// Token: 0x0600032B RID: 811
		bool HasCustomMailboxGreeting(MailboxGreetingEnum g);

		// Token: 0x0600032C RID: 812
		void Save();
	}
}
