using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x0200002A RID: 42
	public class UMPropertiesEx : UMProperties
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00009F80 File Offset: 0x00008180
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00009F88 File Offset: 0x00008188
		public bool ReceivedVoiceMailPreviewEnabled { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00009F91 File Offset: 0x00008191
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00009F99 File Offset: 0x00008199
		public bool SentVoiceMailPreviewEnabled { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00009FA2 File Offset: 0x000081A2
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00009FAA File Offset: 0x000081AA
		public bool PlayOnPhoneEnabled { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00009FB3 File Offset: 0x000081B3
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00009FBB File Offset: 0x000081BB
		public bool PinlessAccessToVoicemail { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00009FC4 File Offset: 0x000081C4
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00009FCC File Offset: 0x000081CC
		public bool ReadUnreadVoicemailInFIFOOrder { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00009FD5 File Offset: 0x000081D5
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00009FDD File Offset: 0x000081DD
		public UMSMSNotificationOptions SMSNotificationOption { get; set; }
	}
}
