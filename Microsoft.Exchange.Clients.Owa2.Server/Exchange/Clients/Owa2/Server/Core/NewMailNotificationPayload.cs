using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000180 RID: 384
	[DataContract]
	internal class NewMailNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00034B44 File Offset: 0x00032D44
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00034B4C File Offset: 0x00032D4C
		[DataMember]
		public string Sender { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00034B55 File Offset: 0x00032D55
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00034B5D File Offset: 0x00032D5D
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00034B66 File Offset: 0x00032D66
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00034B6E File Offset: 0x00032D6E
		[DataMember]
		public string PreviewText { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00034B77 File Offset: 0x00032D77
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00034B7F File Offset: 0x00032D7F
		[DataMember]
		public string ItemId { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00034B88 File Offset: 0x00032D88
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00034B90 File Offset: 0x00032D90
		[DataMember]
		public string ConversationId { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00034B99 File Offset: 0x00032D99
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00034BA1 File Offset: 0x00032DA1
		[DataMember]
		public bool IsClutter { get; set; }
	}
}
