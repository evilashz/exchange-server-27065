using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001A RID: 26
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public class NewMailNotification : ApplicationNotification
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003355 File Offset: 0x00001555
		public NewMailNotification() : base(NotificationType.NewMail)
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000335E File Offset: 0x0000155E
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003366 File Offset: 0x00001566
		[DataMember(EmitDefaultValue = false)]
		public string Sender { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000336F File Offset: 0x0000156F
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003377 File Offset: 0x00001577
		[DataMember(EmitDefaultValue = false)]
		public string Subject { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003380 File Offset: 0x00001580
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003388 File Offset: 0x00001588
		[DataMember(EmitDefaultValue = false)]
		public string PreviewText { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003391 File Offset: 0x00001591
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003399 File Offset: 0x00001599
		[DataMember(EmitDefaultValue = false)]
		public string ItemId { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000033A2 File Offset: 0x000015A2
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000033AA File Offset: 0x000015AA
		[DataMember(EmitDefaultValue = false)]
		public string ConversationId { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000033B3 File Offset: 0x000015B3
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000033BB File Offset: 0x000015BB
		[DataMember(EmitDefaultValue = false)]
		public bool IsClutter { get; set; }
	}
}
