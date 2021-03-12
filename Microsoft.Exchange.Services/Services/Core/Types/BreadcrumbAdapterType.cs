using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A6 RID: 1446
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "BreadcrumbAdapterType")]
	[Serializable]
	public class BreadcrumbAdapterType : RelatedItemInfoTypeBase
	{
		// Token: 0x06002937 RID: 10551 RVA: 0x000AD00D File Offset: 0x000AB20D
		public BreadcrumbAdapterType(ItemId itemId, SingleRecipientType from, string preview, ItemId conversationId, string itemClass, bool isNewTimeProposal) : base(itemId, from, preview)
		{
			this.LinkedConversationId = conversationId;
			this.ItemClass = itemClass;
			this.IsNewTimeProposal = isNewTimeProposal;
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x000AD030 File Offset: 0x000AB230
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x000AD038 File Offset: 0x000AB238
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public ItemId LinkedConversationId { get; set; }

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x000AD041 File Offset: 0x000AB241
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x000AD049 File Offset: 0x000AB249
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string ItemClass { get; set; }

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x000AD052 File Offset: 0x000AB252
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x000AD05A File Offset: 0x000AB25A
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public bool IsNewTimeProposal { get; set; }

		// Token: 0x0600293E RID: 10558 RVA: 0x000AD063 File Offset: 0x000AB263
		public static BreadcrumbAdapterType FromRelatedItemInfo(IRelatedItemInfo itemInfo)
		{
			return new BreadcrumbAdapterType(itemInfo.ItemId, itemInfo.From, itemInfo.Preview, itemInfo.ConversationId, itemInfo.ItemClass, BreadcrumbAdapterType.IsItemANewTimeProposal(itemInfo));
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000AD090 File Offset: 0x000AB290
		private static bool IsItemANewTimeProposal(IRelatedItemInfo itemInfo)
		{
			MeetingResponseMessageType meetingResponseMessageType = itemInfo as MeetingResponseMessageType;
			return meetingResponseMessageType != null && meetingResponseMessageType.IsNewTimeProposal;
		}
	}
}
