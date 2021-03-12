using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F1 RID: 1521
	[XmlInclude(typeof(AddItemToMyCalendarType))]
	[XmlInclude(typeof(SmartResponseType))]
	[XmlInclude(typeof(CancelCalendarItemType))]
	[XmlInclude(typeof(ForwardItemType))]
	[XmlInclude(typeof(ReplyAllToItemType))]
	[XmlInclude(typeof(ReplyToItemType))]
	[XmlInclude(typeof(WellKnownResponseObjectType))]
	[XmlInclude(typeof(DeclineItemType))]
	[XmlInclude(typeof(TentativelyAcceptItemType))]
	[XmlInclude(typeof(AcceptItemType))]
	[XmlInclude(typeof(ResponseObjectType))]
	[XmlInclude(typeof(ProposeNewTimeType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(ResponseObjectType))]
	[KnownType(typeof(PostReplyItemBaseType))]
	[KnownType(typeof(PostReplyItemType))]
	[KnownType(typeof(RemoveItemType))]
	[KnownType(typeof(ReferenceItemResponseType))]
	[KnownType(typeof(AcceptSharingInvitationType))]
	[KnownType(typeof(SuppressReadReceiptType))]
	[KnownType(typeof(SmartResponseBaseType))]
	[KnownType(typeof(SmartResponseType))]
	[KnownType(typeof(CancelCalendarItemType))]
	[KnownType(typeof(ForwardItemType))]
	[KnownType(typeof(ReplyAllToItemType))]
	[KnownType(typeof(ReplyToItemType))]
	[KnownType(typeof(WellKnownResponseObjectType))]
	[KnownType(typeof(DeclineItemType))]
	[KnownType(typeof(TentativelyAcceptItemType))]
	[KnownType(typeof(AcceptItemType))]
	[KnownType(typeof(AddItemToMyCalendarType))]
	[KnownType(typeof(ProposeNewTimeType))]
	[XmlInclude(typeof(SmartResponseBaseType))]
	[XmlInclude(typeof(PostReplyItemType))]
	[XmlInclude(typeof(RemoveItemType))]
	[XmlInclude(typeof(ReferenceItemResponseType))]
	[XmlInclude(typeof(PostReplyItemBaseType))]
	[XmlInclude(typeof(AcceptSharingInvitationType))]
	[XmlInclude(typeof(SuppressReadReceiptType))]
	[Serializable]
	public abstract class ResponseObjectCoreType : MessageType
	{
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000B3B21 File Offset: 0x000B1D21
		// (set) Token: 0x06002EEB RID: 12011 RVA: 0x000B3B29 File Offset: 0x000B1D29
		[DataMember(EmitDefaultValue = false)]
		[XmlElement(typeof(ItemId))]
		public BaseItemId ReferenceItemId { get; set; }
	}
}
