using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003D1 RID: 977
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetModernAttachmentsRequest
	{
		// Token: 0x06001F47 RID: 8007 RVA: 0x00077425 File Offset: 0x00075625
		public GetModernAttachmentsRequest()
		{
			this.ItemCreationTimeStart = null;
			this.ItemCreationTimeEnd = null;
			this.ItemFromStart = null;
			this.ItemFromEnd = null;
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00077449 File Offset: 0x00075649
		// (set) Token: 0x06001F49 RID: 8009 RVA: 0x00077451 File Offset: 0x00075651
		[DataMember]
		public int AttachmentsReturnedMax { get; set; }

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x0007745A File Offset: 0x0007565A
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x00077462 File Offset: 0x00075662
		[DataMember]
		public int ItemsToProcessMax { get; set; }

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0007746B File Offset: 0x0007566B
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x00077473 File Offset: 0x00075673
		[DataMember]
		public int ItemsOffset { get; set; }

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x0007747C File Offset: 0x0007567C
		// (set) Token: 0x06001F4F RID: 8015 RVA: 0x00077484 File Offset: 0x00075684
		[DataMember]
		public string ItemCreationTimeStart { get; set; }

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x0007748D File Offset: 0x0007568D
		// (set) Token: 0x06001F51 RID: 8017 RVA: 0x00077495 File Offset: 0x00075695
		[DataMember]
		public string ItemCreationTimeEnd { get; set; }

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x0007749E File Offset: 0x0007569E
		// (set) Token: 0x06001F53 RID: 8019 RVA: 0x000774A6 File Offset: 0x000756A6
		[DataMember]
		public string ItemFromStart { get; set; }

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x000774AF File Offset: 0x000756AF
		// (set) Token: 0x06001F55 RID: 8021 RVA: 0x000774B7 File Offset: 0x000756B7
		[DataMember]
		public string ItemFromEnd { get; set; }

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x000774C0 File Offset: 0x000756C0
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x000774C8 File Offset: 0x000756C8
		[DataMember]
		public string[] Filter { get; set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x000774D1 File Offset: 0x000756D1
		// (set) Token: 0x06001F59 RID: 8025 RVA: 0x000774D9 File Offset: 0x000756D9
		[DataMember]
		public GetModernAttachmentsRequest.AttachmentsSortBy[] SortByColumns { get; set; }

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x000774E2 File Offset: 0x000756E2
		// (set) Token: 0x06001F5B RID: 8027 RVA: 0x000774EA File Offset: 0x000756EA
		[DataMember]
		public BaseFolderId[] FolderId { get; set; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x000774F3 File Offset: 0x000756F3
		// (set) Token: 0x06001F5D RID: 8029 RVA: 0x000774FB File Offset: 0x000756FB
		[DataMember]
		public BaseItemId[] ItemId { get; set; }

		// Token: 0x020003D2 RID: 978
		[Flags]
		public enum AttachmentsSortBy
		{
			// Token: 0x040011D6 RID: 4566
			ItemConversationTopic = 1,
			// Token: 0x040011D7 RID: 4567
			ItemClass = 2,
			// Token: 0x040011D8 RID: 4568
			ItemSubject = 3,
			// Token: 0x040011D9 RID: 4569
			ItemReceivedTime = 4,
			// Token: 0x040011DA RID: 4570
			ItemCreationTime = 5,
			// Token: 0x040011DB RID: 4571
			ItemLastModifiedTime = 6,
			// Token: 0x040011DC RID: 4572
			ItemSize = 7,
			// Token: 0x040011DD RID: 4573
			ItemSentRepresentingDisplayName = 8,
			// Token: 0x040011DE RID: 4574
			DescendingOrder = 256
		}
	}
}
