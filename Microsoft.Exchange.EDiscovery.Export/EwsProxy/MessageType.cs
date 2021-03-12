using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000117 RID: 279
	[XmlInclude(typeof(SmartResponseBaseType))]
	[XmlInclude(typeof(PostReplyItemType))]
	[XmlInclude(typeof(RemoveItemType))]
	[XmlInclude(typeof(ProposeNewTimeType))]
	[XmlInclude(typeof(ReferenceItemResponseType))]
	[XmlInclude(typeof(AcceptSharingInvitationType))]
	[XmlInclude(typeof(SuppressReadReceiptType))]
	[XmlInclude(typeof(MeetingMessageType))]
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
	[XmlInclude(typeof(PostReplyItemBaseType))]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(AddItemToMyCalendarType))]
	[XmlInclude(typeof(MeetingCancellationMessageType))]
	[DebuggerStepThrough]
	[XmlInclude(typeof(MeetingResponseMessageType))]
	[XmlInclude(typeof(MeetingRequestMessageType))]
	[XmlInclude(typeof(ResponseObjectCoreType))]
	[XmlInclude(typeof(ResponseObjectType))]
	[Serializable]
	public class MessageType : ItemType
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00021AC2 File Offset: 0x0001FCC2
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00021ACA File Offset: 0x0001FCCA
		public SingleRecipientType Sender
		{
			get
			{
				return this.senderField;
			}
			set
			{
				this.senderField = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00021AD3 File Offset: 0x0001FCD3
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00021ADB File Offset: 0x0001FCDB
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] ToRecipients
		{
			get
			{
				return this.toRecipientsField;
			}
			set
			{
				this.toRecipientsField = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00021AEC File Offset: 0x0001FCEC
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] CcRecipients
		{
			get
			{
				return this.ccRecipientsField;
			}
			set
			{
				this.ccRecipientsField = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00021AF5 File Offset: 0x0001FCF5
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00021AFD File Offset: 0x0001FCFD
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] BccRecipients
		{
			get
			{
				return this.bccRecipientsField;
			}
			set
			{
				this.bccRecipientsField = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00021B06 File Offset: 0x0001FD06
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00021B0E File Offset: 0x0001FD0E
		public bool IsReadReceiptRequested
		{
			get
			{
				return this.isReadReceiptRequestedField;
			}
			set
			{
				this.isReadReceiptRequestedField = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00021B17 File Offset: 0x0001FD17
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00021B1F File Offset: 0x0001FD1F
		[XmlIgnore]
		public bool IsReadReceiptRequestedSpecified
		{
			get
			{
				return this.isReadReceiptRequestedFieldSpecified;
			}
			set
			{
				this.isReadReceiptRequestedFieldSpecified = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00021B28 File Offset: 0x0001FD28
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00021B30 File Offset: 0x0001FD30
		public bool IsDeliveryReceiptRequested
		{
			get
			{
				return this.isDeliveryReceiptRequestedField;
			}
			set
			{
				this.isDeliveryReceiptRequestedField = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00021B39 File Offset: 0x0001FD39
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00021B41 File Offset: 0x0001FD41
		[XmlIgnore]
		public bool IsDeliveryReceiptRequestedSpecified
		{
			get
			{
				return this.isDeliveryReceiptRequestedFieldSpecified;
			}
			set
			{
				this.isDeliveryReceiptRequestedFieldSpecified = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00021B4A File Offset: 0x0001FD4A
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x00021B52 File Offset: 0x0001FD52
		[XmlElement(DataType = "base64Binary")]
		public byte[] ConversationIndex
		{
			get
			{
				return this.conversationIndexField;
			}
			set
			{
				this.conversationIndexField = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00021B5B File Offset: 0x0001FD5B
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x00021B63 File Offset: 0x0001FD63
		public string ConversationTopic
		{
			get
			{
				return this.conversationTopicField;
			}
			set
			{
				this.conversationTopicField = value;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00021B6C File Offset: 0x0001FD6C
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x00021B74 File Offset: 0x0001FD74
		public SingleRecipientType From
		{
			get
			{
				return this.fromField;
			}
			set
			{
				this.fromField = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00021B7D File Offset: 0x0001FD7D
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00021B85 File Offset: 0x0001FD85
		public string InternetMessageId
		{
			get
			{
				return this.internetMessageIdField;
			}
			set
			{
				this.internetMessageIdField = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00021B8E File Offset: 0x0001FD8E
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x00021B96 File Offset: 0x0001FD96
		public bool IsRead
		{
			get
			{
				return this.isReadField;
			}
			set
			{
				this.isReadField = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00021B9F File Offset: 0x0001FD9F
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x00021BA7 File Offset: 0x0001FDA7
		[XmlIgnore]
		public bool IsReadSpecified
		{
			get
			{
				return this.isReadFieldSpecified;
			}
			set
			{
				this.isReadFieldSpecified = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		public bool IsResponseRequested
		{
			get
			{
				return this.isResponseRequestedField;
			}
			set
			{
				this.isResponseRequestedField = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00021BC1 File Offset: 0x0001FDC1
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x00021BC9 File Offset: 0x0001FDC9
		[XmlIgnore]
		public bool IsResponseRequestedSpecified
		{
			get
			{
				return this.isResponseRequestedFieldSpecified;
			}
			set
			{
				this.isResponseRequestedFieldSpecified = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00021BD2 File Offset: 0x0001FDD2
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x00021BDA File Offset: 0x0001FDDA
		public string References
		{
			get
			{
				return this.referencesField;
			}
			set
			{
				this.referencesField = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00021BE3 File Offset: 0x0001FDE3
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00021BEB File Offset: 0x0001FDEB
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] ReplyTo
		{
			get
			{
				return this.replyToField;
			}
			set
			{
				this.replyToField = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00021BF4 File Offset: 0x0001FDF4
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x00021BFC File Offset: 0x0001FDFC
		public SingleRecipientType ReceivedBy
		{
			get
			{
				return this.receivedByField;
			}
			set
			{
				this.receivedByField = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00021C05 File Offset: 0x0001FE05
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x00021C0D File Offset: 0x0001FE0D
		public SingleRecipientType ReceivedRepresenting
		{
			get
			{
				return this.receivedRepresentingField;
			}
			set
			{
				this.receivedRepresentingField = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00021C16 File Offset: 0x0001FE16
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x00021C1E File Offset: 0x0001FE1E
		public ApprovalRequestDataType ApprovalRequestData
		{
			get
			{
				return this.approvalRequestDataField;
			}
			set
			{
				this.approvalRequestDataField = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00021C27 File Offset: 0x0001FE27
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x00021C2F File Offset: 0x0001FE2F
		public VotingInformationType VotingInformation
		{
			get
			{
				return this.votingInformationField;
			}
			set
			{
				this.votingInformationField = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00021C38 File Offset: 0x0001FE38
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00021C40 File Offset: 0x0001FE40
		public ReminderMessageDataType ReminderMessageData
		{
			get
			{
				return this.reminderMessageDataField;
			}
			set
			{
				this.reminderMessageDataField = value;
			}
		}

		// Token: 0x040008E5 RID: 2277
		private SingleRecipientType senderField;

		// Token: 0x040008E6 RID: 2278
		private EmailAddressType[] toRecipientsField;

		// Token: 0x040008E7 RID: 2279
		private EmailAddressType[] ccRecipientsField;

		// Token: 0x040008E8 RID: 2280
		private EmailAddressType[] bccRecipientsField;

		// Token: 0x040008E9 RID: 2281
		private bool isReadReceiptRequestedField;

		// Token: 0x040008EA RID: 2282
		private bool isReadReceiptRequestedFieldSpecified;

		// Token: 0x040008EB RID: 2283
		private bool isDeliveryReceiptRequestedField;

		// Token: 0x040008EC RID: 2284
		private bool isDeliveryReceiptRequestedFieldSpecified;

		// Token: 0x040008ED RID: 2285
		private byte[] conversationIndexField;

		// Token: 0x040008EE RID: 2286
		private string conversationTopicField;

		// Token: 0x040008EF RID: 2287
		private SingleRecipientType fromField;

		// Token: 0x040008F0 RID: 2288
		private string internetMessageIdField;

		// Token: 0x040008F1 RID: 2289
		private bool isReadField;

		// Token: 0x040008F2 RID: 2290
		private bool isReadFieldSpecified;

		// Token: 0x040008F3 RID: 2291
		private bool isResponseRequestedField;

		// Token: 0x040008F4 RID: 2292
		private bool isResponseRequestedFieldSpecified;

		// Token: 0x040008F5 RID: 2293
		private string referencesField;

		// Token: 0x040008F6 RID: 2294
		private EmailAddressType[] replyToField;

		// Token: 0x040008F7 RID: 2295
		private SingleRecipientType receivedByField;

		// Token: 0x040008F8 RID: 2296
		private SingleRecipientType receivedRepresentingField;

		// Token: 0x040008F9 RID: 2297
		private ApprovalRequestDataType approvalRequestDataField;

		// Token: 0x040008FA RID: 2298
		private VotingInformationType votingInformationField;

		// Token: 0x040008FB RID: 2299
		private ReminderMessageDataType reminderMessageDataField;
	}
}
