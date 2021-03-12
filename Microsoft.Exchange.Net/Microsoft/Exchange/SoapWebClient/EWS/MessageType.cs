using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001F8 RID: 504
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(MeetingMessageType))]
	[XmlInclude(typeof(MeetingCancellationMessageType))]
	[XmlInclude(typeof(MeetingResponseMessageType))]
	[XmlInclude(typeof(MeetingRequestMessageType))]
	[XmlInclude(typeof(ResponseObjectCoreType))]
	[XmlInclude(typeof(ResponseObjectType))]
	[XmlInclude(typeof(PostReplyItemBaseType))]
	[XmlInclude(typeof(PostReplyItemType))]
	[XmlInclude(typeof(AddItemToMyCalendarType))]
	[XmlInclude(typeof(RemoveItemType))]
	[XmlInclude(typeof(ProposeNewTimeType))]
	[XmlInclude(typeof(ReferenceItemResponseType))]
	[XmlInclude(typeof(AcceptSharingInvitationType))]
	[XmlInclude(typeof(SuppressReadReceiptType))]
	[XmlInclude(typeof(SmartResponseBaseType))]
	[XmlInclude(typeof(SmartResponseType))]
	[XmlInclude(typeof(CancelCalendarItemType))]
	[XmlInclude(typeof(ForwardItemType))]
	[XmlInclude(typeof(ReplyAllToItemType))]
	[XmlInclude(typeof(ReplyToItemType))]
	[XmlInclude(typeof(WellKnownResponseObjectType))]
	[XmlInclude(typeof(MeetingRegistrationResponseObjectType))]
	[XmlInclude(typeof(DeclineItemType))]
	[XmlInclude(typeof(TentativelyAcceptItemType))]
	[XmlInclude(typeof(AcceptItemType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MessageType : ItemType
	{
		// Token: 0x04000D37 RID: 3383
		public SingleRecipientType Sender;

		// Token: 0x04000D38 RID: 3384
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] ToRecipients;

		// Token: 0x04000D39 RID: 3385
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] CcRecipients;

		// Token: 0x04000D3A RID: 3386
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] BccRecipients;

		// Token: 0x04000D3B RID: 3387
		public bool IsReadReceiptRequested;

		// Token: 0x04000D3C RID: 3388
		[XmlIgnore]
		public bool IsReadReceiptRequestedSpecified;

		// Token: 0x04000D3D RID: 3389
		public bool IsDeliveryReceiptRequested;

		// Token: 0x04000D3E RID: 3390
		[XmlIgnore]
		public bool IsDeliveryReceiptRequestedSpecified;

		// Token: 0x04000D3F RID: 3391
		[XmlElement(DataType = "base64Binary")]
		public byte[] ConversationIndex;

		// Token: 0x04000D40 RID: 3392
		public string ConversationTopic;

		// Token: 0x04000D41 RID: 3393
		public SingleRecipientType From;

		// Token: 0x04000D42 RID: 3394
		public string InternetMessageId;

		// Token: 0x04000D43 RID: 3395
		public bool IsRead;

		// Token: 0x04000D44 RID: 3396
		[XmlIgnore]
		public bool IsReadSpecified;

		// Token: 0x04000D45 RID: 3397
		public bool IsResponseRequested;

		// Token: 0x04000D46 RID: 3398
		[XmlIgnore]
		public bool IsResponseRequestedSpecified;

		// Token: 0x04000D47 RID: 3399
		public string References;

		// Token: 0x04000D48 RID: 3400
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] ReplyTo;

		// Token: 0x04000D49 RID: 3401
		public SingleRecipientType ReceivedBy;

		// Token: 0x04000D4A RID: 3402
		public SingleRecipientType ReceivedRepresenting;

		// Token: 0x04000D4B RID: 3403
		public ApprovalRequestDataType ApprovalRequestData;

		// Token: 0x04000D4C RID: 3404
		public VotingInformationType VotingInformation;

		// Token: 0x04000D4D RID: 3405
		public ReminderMessageDataType ReminderMessageData;
	}
}
