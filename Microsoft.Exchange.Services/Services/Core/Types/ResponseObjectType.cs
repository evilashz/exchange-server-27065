using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F2 RID: 1522
	[XmlInclude(typeof(SmartResponseBaseType))]
	[KnownType(typeof(ApproveRequestItemType))]
	[KnownType(typeof(DeclineItemType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(ProposeNewTimeType))]
	[KnownType(typeof(ProposeNewTimeType))]
	[XmlInclude(typeof(PostReplyItemBaseType))]
	[XmlInclude(typeof(PostReplyItemType))]
	[XmlInclude(typeof(RemoveItemType))]
	[XmlInclude(typeof(ReferenceItemResponseType))]
	[XmlInclude(typeof(AcceptSharingInvitationType))]
	[XmlInclude(typeof(SuppressReadReceiptType))]
	[XmlInclude(typeof(SmartResponseType))]
	[XmlInclude(typeof(CancelCalendarItemType))]
	[XmlInclude(typeof(ForwardItemType))]
	[XmlInclude(typeof(ReplyAllToItemType))]
	[XmlInclude(typeof(ReplyToItemType))]
	[XmlInclude(typeof(WellKnownResponseObjectType))]
	[XmlInclude(typeof(DeclineItemType))]
	[XmlInclude(typeof(TentativelyAcceptItemType))]
	[XmlInclude(typeof(AcceptItemType))]
	[XmlInclude(typeof(AddItemToMyCalendarType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
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
	[KnownType(typeof(TentativelyAcceptItemType))]
	[KnownType(typeof(AcceptItemType))]
	[KnownType(typeof(RejectRequestItemType))]
	[KnownType(typeof(VotingResponseItemType))]
	[KnownType(typeof(AddItemToMyCalendarType))]
	[Serializable]
	public class ResponseObjectType : ResponseObjectCoreType
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000B3B3A File Offset: 0x000B1D3A
		// (set) Token: 0x06002EEE RID: 12014 RVA: 0x000B3B42 File Offset: 0x000B1D42
		[DataMember(EmitDefaultValue = false)]
		[XmlAttribute]
		public string ObjectName { get; set; }
	}
}
