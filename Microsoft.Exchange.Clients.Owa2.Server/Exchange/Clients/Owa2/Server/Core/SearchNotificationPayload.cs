using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001B3 RID: 435
	[DataContract]
	internal class SearchNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x0003C559 File Offset: 0x0003A759
		public SearchNotificationPayload()
		{
			base.SubscriptionId = NotificationType.SearchNotification.ToString();
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003C573 File Offset: 0x0003A773
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x0003C57B File Offset: 0x0003A77B
		[DataMember]
		public string FolderId { get; set; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0003C584 File Offset: 0x0003A784
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x0003C58C File Offset: 0x0003A78C
		[DataMember]
		public string ClientId { get; set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0003C595 File Offset: 0x0003A795
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x0003C59D File Offset: 0x0003A79D
		[DataMember]
		public bool IsComplete { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0003C5A6 File Offset: 0x0003A7A6
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x0003C5AE File Offset: 0x0003A7AE
		[DataMember]
		public ItemType[] MessageItems { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0003C5B7 File Offset: 0x0003A7B7
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x0003C5BF File Offset: 0x0003A7BF
		[DataMember]
		public ConversationType[] Conversations { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0003C5C8 File Offset: 0x0003A7C8
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x0003C5D0 File Offset: 0x0003A7D0
		[DataMember]
		public int ServerSearchResultsRowCount { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0003C5D9 File Offset: 0x0003A7D9
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x0003C5E1 File Offset: 0x0003A7E1
		[DataMember]
		public HighlightTermType[] HighlightTerms { get; set; }
	}
}
