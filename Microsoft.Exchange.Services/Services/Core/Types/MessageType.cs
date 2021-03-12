using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005EC RID: 1516
	[XmlInclude(typeof(PostReplyItemBaseType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Message")]
	[XmlInclude(typeof(MeetingMessageType))]
	[XmlInclude(typeof(MeetingCancellationMessageType))]
	[XmlInclude(typeof(MeetingResponseMessageType))]
	[XmlInclude(typeof(MeetingRequestMessageType))]
	[XmlInclude(typeof(ResponseObjectCoreType))]
	[XmlInclude(typeof(ResponseObjectType))]
	[XmlInclude(typeof(PostReplyItemType))]
	[XmlInclude(typeof(RemoveItemType))]
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
	[XmlInclude(typeof(DeclineItemType))]
	[XmlInclude(typeof(TentativelyAcceptItemType))]
	[XmlInclude(typeof(AcceptItemType))]
	[XmlInclude(typeof(AddItemToMyCalendarType))]
	[XmlInclude(typeof(ProposeNewTimeType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "Message")]
	[KnownType(typeof(SmartResponseBaseType))]
	[KnownType(typeof(ProposeNewTimeType))]
	[KnownType(typeof(MeetingCancellationMessageType))]
	[KnownType(typeof(MeetingResponseMessageType))]
	[KnownType(typeof(MeetingRequestMessageType))]
	[KnownType(typeof(ResponseObjectCoreType))]
	[KnownType(typeof(ResponseObjectType))]
	[KnownType(typeof(PostReplyItemBaseType))]
	[KnownType(typeof(PostReplyItemType))]
	[KnownType(typeof(RemoveItemType))]
	[KnownType(typeof(ReferenceItemResponseType))]
	[KnownType(typeof(AcceptSharingInvitationType))]
	[KnownType(typeof(SuppressReadReceiptType))]
	[KnownType(typeof(MeetingMessageType))]
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
	[Serializable]
	public class MessageType : ItemType, IRelatedItemInfo
	{
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B28DB File Offset: 0x000B0ADB
		internal new static MessageType CreateFromStoreObjectType(StoreObjectType storeObjectType)
		{
			if (MessageType.createMethods.Member.ContainsKey(storeObjectType))
			{
				return MessageType.createMethods.Member[storeObjectType]();
			}
			return MessageType.createMethods.Member[StoreObjectType.Message]();
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000B291B File Offset: 0x000B0B1B
		// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x000B292D File Offset: 0x000B0B2D
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public SingleRecipientType Sender
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<SingleRecipientType>(MessageSchema.Sender);
			}
			set
			{
				base.PropertyBag[MessageSchema.Sender] = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000B2940 File Offset: 0x000B0B40
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x000B2952 File Offset: 0x000B0B52
		[DataMember(EmitDefaultValue = false, Order = 2)]
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressWrapper[] ToRecipients
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressWrapper[]>(MessageSchema.ToRecipients);
			}
			set
			{
				base.PropertyBag[MessageSchema.ToRecipients] = value;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x000B2965 File Offset: 0x000B0B65
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x000B2977 File Offset: 0x000B0B77
		[DataMember(EmitDefaultValue = false, Order = 3)]
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressWrapper[] CcRecipients
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressWrapper[]>(MessageSchema.CcRecipients);
			}
			set
			{
				base.PropertyBag[MessageSchema.CcRecipients] = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000B298A File Offset: 0x000B0B8A
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x000B299C File Offset: 0x000B0B9C
		[DataMember(EmitDefaultValue = false, Order = 4)]
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressWrapper[] BccRecipients
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressWrapper[]>(MessageSchema.BccRecipients);
			}
			set
			{
				base.PropertyBag[MessageSchema.BccRecipients] = value;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000B29AF File Offset: 0x000B0BAF
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x000B29C1 File Offset: 0x000B0BC1
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public bool? IsReadReceiptRequested
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(MessageSchema.IsReadReceiptRequested);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(MessageSchema.IsReadReceiptRequested, value);
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000B29D4 File Offset: 0x000B0BD4
		// (set) Token: 0x06002DCD RID: 11725 RVA: 0x000B29E1 File Offset: 0x000B0BE1
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsReadReceiptRequestedSpecified
		{
			get
			{
				return base.IsSet(MessageSchema.IsReadReceiptRequested);
			}
			set
			{
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000B29E3 File Offset: 0x000B0BE3
		// (set) Token: 0x06002DCF RID: 11727 RVA: 0x000B29F5 File Offset: 0x000B0BF5
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public bool? IsDeliveryReceiptRequested
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(MessageSchema.IsDeliveryReceiptRequested);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(MessageSchema.IsDeliveryReceiptRequested, value);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000B2A08 File Offset: 0x000B0C08
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x000B2A15 File Offset: 0x000B0C15
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsDeliveryReceiptRequestedSpecified
		{
			get
			{
				return base.IsSet(MessageSchema.IsDeliveryReceiptRequested);
			}
			set
			{
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000B2A17 File Offset: 0x000B0C17
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x000B2A29 File Offset: 0x000B0C29
		[IgnoreDataMember]
		[XmlElement(DataType = "base64Binary")]
		public byte[] ConversationIndex
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<byte[]>(MessageSchema.ConversationIndex);
			}
			set
			{
				base.PropertyBag[MessageSchema.ConversationIndex] = value;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x000B2A3C File Offset: 0x000B0C3C
		// (set) Token: 0x06002DD5 RID: 11733 RVA: 0x000B2A5B File Offset: 0x000B0C5B
		[DataMember(Name = "ConversationIndex", EmitDefaultValue = false, Order = 7)]
		[XmlIgnore]
		public string ConversationIndexString
		{
			get
			{
				byte[] conversationIndex = this.ConversationIndex;
				if (conversationIndex == null)
				{
					return null;
				}
				return Convert.ToBase64String(conversationIndex);
			}
			set
			{
				this.ConversationIndex = ((value != null) ? Convert.FromBase64String(value) : null);
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x000B2A6F File Offset: 0x000B0C6F
		// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x000B2A81 File Offset: 0x000B0C81
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public string ConversationTopic
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(MessageSchema.ConversationTopic);
			}
			set
			{
				base.PropertyBag[MessageSchema.ConversationTopic] = value;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x000B2A94 File Offset: 0x000B0C94
		// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x000B2AA6 File Offset: 0x000B0CA6
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public SingleRecipientType From
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<SingleRecipientType>(MessageSchema.From);
			}
			set
			{
				base.PropertyBag[MessageSchema.From] = value;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x000B2AB9 File Offset: 0x000B0CB9
		// (set) Token: 0x06002DDB RID: 11739 RVA: 0x000B2ACB File Offset: 0x000B0CCB
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public string InternetMessageId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(MessageSchema.InternetMessageId);
			}
			set
			{
				base.PropertyBag[MessageSchema.InternetMessageId] = value;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x000B2ADE File Offset: 0x000B0CDE
		// (set) Token: 0x06002DDD RID: 11741 RVA: 0x000B2AF0 File Offset: 0x000B0CF0
		[DataMember(EmitDefaultValue = false, Order = 11)]
		public bool? IsRead
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(MessageSchema.IsRead);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(MessageSchema.IsRead, value);
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000B2B03 File Offset: 0x000B0D03
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x000B2B10 File Offset: 0x000B0D10
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsReadSpecified
		{
			get
			{
				return base.IsSet(MessageSchema.IsRead);
			}
			set
			{
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x000B2B12 File Offset: 0x000B0D12
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x000B2B24 File Offset: 0x000B0D24
		[DataMember(EmitDefaultValue = false, Order = 12)]
		public bool? IsResponseRequested
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(MessageSchema.IsResponseRequested);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(MessageSchema.IsResponseRequested, value);
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000B2B37 File Offset: 0x000B0D37
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x000B2B44 File Offset: 0x000B0D44
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsResponseRequestedSpecified
		{
			get
			{
				return base.IsSet(MessageSchema.IsResponseRequested);
			}
			set
			{
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000B2B46 File Offset: 0x000B0D46
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x000B2B58 File Offset: 0x000B0D58
		[DataMember(EmitDefaultValue = false, Order = 13)]
		public string References
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(MessageSchema.References);
			}
			set
			{
				base.PropertyBag[MessageSchema.References] = value;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000B2B6B File Offset: 0x000B0D6B
		// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x000B2B7D File Offset: 0x000B0D7D
		[XmlArrayItem("Mailbox", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 14)]
		public EmailAddressWrapper[] ReplyTo
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressWrapper[]>(MessageSchema.ReplyTo);
			}
			set
			{
				base.PropertyBag[MessageSchema.ReplyTo] = value;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x000B2B90 File Offset: 0x000B0D90
		// (set) Token: 0x06002DE9 RID: 11753 RVA: 0x000B2BA2 File Offset: 0x000B0DA2
		[DataMember(EmitDefaultValue = false, Order = 15)]
		public SingleRecipientType ReceivedBy
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<SingleRecipientType>(MessageSchema.ReceivedBy);
			}
			set
			{
				base.PropertyBag[MessageSchema.ReceivedBy] = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x000B2BB5 File Offset: 0x000B0DB5
		// (set) Token: 0x06002DEB RID: 11755 RVA: 0x000B2BC7 File Offset: 0x000B0DC7
		[DataMember(EmitDefaultValue = false, Order = 16)]
		public SingleRecipientType ReceivedRepresenting
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<SingleRecipientType>(MessageSchema.ReceivedRepresenting);
			}
			set
			{
				base.PropertyBag[MessageSchema.ReceivedRepresenting] = value;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000B2BDA File Offset: 0x000B0DDA
		// (set) Token: 0x06002DED RID: 11757 RVA: 0x000B2BEC File Offset: 0x000B0DEC
		[DataMember(EmitDefaultValue = false, Order = 17)]
		public ApprovalRequestDataType ApprovalRequestData
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ApprovalRequestDataType>(MessageSchema.ApprovalRequestData);
			}
			set
			{
				base.PropertyBag[MessageSchema.ApprovalRequestData] = value;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000B2BFF File Offset: 0x000B0DFF
		// (set) Token: 0x06002DEF RID: 11759 RVA: 0x000B2C11 File Offset: 0x000B0E11
		[DataMember(EmitDefaultValue = false, Order = 18)]
		public VotingInformationType VotingInformation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<VotingInformationType>(MessageSchema.VotingInformation);
			}
			set
			{
				base.PropertyBag[MessageSchema.VotingInformation] = value;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000B2C24 File Offset: 0x000B0E24
		// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x000B2C36 File Offset: 0x000B0E36
		[DataMember(EmitDefaultValue = false, Order = 19)]
		[XmlIgnore]
		public bool? RelyOnConversationIndex
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(MessageSchema.RelyOnConversationIndex);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(MessageSchema.RelyOnConversationIndex, value);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000B2C49 File Offset: 0x000B0E49
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x000B2C5B File Offset: 0x000B0E5B
		[DataMember(EmitDefaultValue = false, Order = 20)]
		public ReminderMessageDataType ReminderMessageData
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ReminderMessageDataType>(MessageSchema.ReminderMessageData);
			}
			set
			{
				base.PropertyBag[MessageSchema.ReminderMessageData] = value;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x000B2C6E File Offset: 0x000B0E6E
		// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x000B2C80 File Offset: 0x000B0E80
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 21)]
		public ModernReminderType[] ModernReminders
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ModernReminderType[]>(MessageSchema.ModernReminders);
			}
			set
			{
				base.PropertyBag[MessageSchema.ModernReminders] = value;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000B2C93 File Offset: 0x000B0E93
		// (set) Token: 0x06002DF7 RID: 11767 RVA: 0x000B2CA5 File Offset: 0x000B0EA5
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 22)]
		public int LikeCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int>(MessageSchema.LikeCount);
			}
			set
			{
				base.PropertyBag[MessageSchema.LikeCount] = value;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000B2CBD File Offset: 0x000B0EBD
		// (set) Token: 0x06002DF9 RID: 11769 RVA: 0x000B2CCF File Offset: 0x000B0ECF
		[DataMember(EmitDefaultValue = false, Order = 23)]
		[XmlIgnore]
		public RecipientCountsType RecipientCounts
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<RecipientCountsType>(MessageSchema.RecipientCounts);
			}
			set
			{
				base.PropertyBag[MessageSchema.RecipientCounts] = value;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000B2CE2 File Offset: 0x000B0EE2
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x000B2CF4 File Offset: 0x000B0EF4
		[DataMember(EmitDefaultValue = false, Order = 24)]
		[XmlIgnore]
		public EmailAddressWrapper[] Likers
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressWrapper[]>(MessageSchema.Likers);
			}
			set
			{
				base.PropertyBag[MessageSchema.Likers] = value;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x000B2D07 File Offset: 0x000B0F07
		// (set) Token: 0x06002DFD RID: 11773 RVA: 0x000B2D19 File Offset: 0x000B0F19
		[DataMember(EmitDefaultValue = false, Order = 25)]
		[XmlIgnore]
		public bool? IsGroupEscalationMessage
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(MessageSchema.IsGroupEscalationMessage);
			}
			set
			{
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000B2D1B File Offset: 0x000B0F1B
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.Message;
			}
		}

		// Token: 0x04001B5D RID: 7005
		private static LazyMember<Dictionary<StoreObjectType, Func<MessageType>>> createMethods = new LazyMember<Dictionary<StoreObjectType, Func<MessageType>>>(delegate()
		{
			Dictionary<StoreObjectType, Func<MessageType>> dictionary = new Dictionary<StoreObjectType, Func<MessageType>>();
			dictionary.Add(StoreObjectType.MeetingCancellation, () => new MeetingCancellationMessageType());
			dictionary.Add(StoreObjectType.MeetingMessage, () => new MeetingMessageType());
			dictionary.Add(StoreObjectType.MeetingRequest, () => new MeetingRequestMessageType());
			dictionary.Add(StoreObjectType.MeetingResponse, () => new MeetingResponseMessageType());
			dictionary.Add(StoreObjectType.Message, () => new MessageType());
			dictionary.Add(StoreObjectType.Unknown, () => new MessageType());
			return dictionary;
		});
	}
}
