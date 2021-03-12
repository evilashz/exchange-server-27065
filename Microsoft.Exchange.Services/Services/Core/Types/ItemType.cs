using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A8 RID: 1448
	[XmlInclude(typeof(PostReplyItemBaseType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Item")]
	[KnownType(typeof(PostItemType))]
	[KnownType(typeof(SuppressReadReceiptType))]
	[KnownType(typeof(SmartResponseBaseType))]
	[KnownType(typeof(RemoveItemType))]
	[KnownType(typeof(ReferenceItemResponseType))]
	[KnownType(typeof(PostReplyItemType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "Item")]
	[XmlInclude(typeof(AddItemToMyCalendarType))]
	[KnownType(typeof(ProposeNewTimeType))]
	[XmlInclude(typeof(DistributionListType))]
	[XmlInclude(typeof(ContactItemType))]
	[XmlInclude(typeof(EwsCalendarItemType))]
	[XmlInclude(typeof(MessageType))]
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
	[XmlInclude(typeof(TaskType))]
	[XmlInclude(typeof(ProposeNewTimeType))]
	[XmlInclude(typeof(PostItemType))]
	[KnownType(typeof(TaskType))]
	[KnownType(typeof(DistributionListType))]
	[KnownType(typeof(ContactItemType))]
	[KnownType(typeof(EwsCalendarItemType))]
	[KnownType(typeof(MessageType))]
	[KnownType(typeof(MeetingMessageType))]
	[KnownType(typeof(MeetingCancellationMessageType))]
	[KnownType(typeof(MeetingResponseMessageType))]
	[KnownType(typeof(MeetingRequestMessageType))]
	[KnownType(typeof(ResponseObjectCoreType))]
	[KnownType(typeof(ResponseObjectType))]
	[KnownType(typeof(PostReplyItemBaseType))]
	[KnownType(typeof(AcceptSharingInvitationType))]
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
	public class ItemType : ServiceObject
	{
		// Token: 0x0600294A RID: 10570 RVA: 0x000AD13D File Offset: 0x000AB33D
		internal static ItemType CreateFromStoreObjectType(StoreObjectType storeObjectType)
		{
			if (ItemType.createMethods.Member.ContainsKey(storeObjectType))
			{
				return ItemType.createMethods.Member[storeObjectType]();
			}
			return ItemType.createMethods.Member[StoreObjectType.Message]();
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000AD185 File Offset: 0x000AB385
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x000AD197 File Offset: 0x000AB397
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public MimeContentType MimeContent
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<MimeContentType>(ItemSchema.MimeContent);
			}
			set
			{
				base.PropertyBag[ItemSchema.MimeContent] = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x000AD1AA File Offset: 0x000AB3AA
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x000AD1BC File Offset: 0x000AB3BC
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public ItemId ItemId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ItemId>(ItemSchema.ItemId);
			}
			set
			{
				base.PropertyBag[ItemSchema.ItemId] = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x000AD1CF File Offset: 0x000AB3CF
		// (set) Token: 0x06002951 RID: 10577 RVA: 0x000AD1E1 File Offset: 0x000AB3E1
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public FolderId ParentFolderId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<FolderId>(ItemSchema.ParentFolderId);
			}
			set
			{
				base.PropertyBag[ItemSchema.ParentFolderId] = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x000AD1F4 File Offset: 0x000AB3F4
		// (set) Token: 0x06002953 RID: 10579 RVA: 0x000AD206 File Offset: 0x000AB406
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string ItemClass
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.ItemClass);
			}
			set
			{
				base.PropertyBag[ItemSchema.ItemClass] = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002954 RID: 10580 RVA: 0x000AD219 File Offset: 0x000AB419
		// (set) Token: 0x06002955 RID: 10581 RVA: 0x000AD22B File Offset: 0x000AB42B
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public string Subject
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.Subject);
			}
			set
			{
				base.PropertyBag[ItemSchema.Subject] = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x000AD23E File Offset: 0x000AB43E
		// (set) Token: 0x06002957 RID: 10583 RVA: 0x000AD255 File Offset: 0x000AB455
		[IgnoreDataMember]
		[XmlElement]
		public SensitivityType Sensitivity
		{
			get
			{
				if (!this.SensitivitySpecified)
				{
					return SensitivityType.Normal;
				}
				return EnumUtilities.Parse<SensitivityType>(this.SensitivityString);
			}
			set
			{
				this.SensitivityString = EnumUtilities.ToString<SensitivityType>(value);
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x000AD263 File Offset: 0x000AB463
		// (set) Token: 0x06002959 RID: 10585 RVA: 0x000AD275 File Offset: 0x000AB475
		[DataMember(Name = "Sensitivity", EmitDefaultValue = false, Order = 6)]
		[XmlIgnore]
		public string SensitivityString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.Sensitivity);
			}
			set
			{
				base.PropertyBag[ItemSchema.Sensitivity] = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600295A RID: 10586 RVA: 0x000AD288 File Offset: 0x000AB488
		// (set) Token: 0x0600295B RID: 10587 RVA: 0x000AD295 File Offset: 0x000AB495
		[IgnoreDataMember]
		[XmlIgnore]
		public bool SensitivitySpecified
		{
			get
			{
				return base.IsSet(ItemSchema.Sensitivity);
			}
			set
			{
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600295C RID: 10588 RVA: 0x000AD297 File Offset: 0x000AB497
		// (set) Token: 0x0600295D RID: 10589 RVA: 0x000AD2A9 File Offset: 0x000AB4A9
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public BodyContentType Body
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<BodyContentType>(ItemSchema.Body);
			}
			set
			{
				base.PropertyBag[ItemSchema.Body] = value;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x000AD2BC File Offset: 0x000AB4BC
		// (set) Token: 0x0600295F RID: 10591 RVA: 0x000AD2CE File Offset: 0x000AB4CE
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), IsNullable = false)]
		[XmlArrayItem("ReferenceAttachment", typeof(ReferenceAttachmentType), IsNullable = false)]
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public AttachmentType[] Attachments
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<AttachmentType[]>(ItemSchema.Attachments);
			}
			set
			{
				base.PropertyBag[ItemSchema.Attachments] = value;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x000AD2E1 File Offset: 0x000AB4E1
		// (set) Token: 0x06002961 RID: 10593 RVA: 0x000AD2F3 File Offset: 0x000AB4F3
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public string DateTimeReceived
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.DateTimeReceived);
			}
			set
			{
				base.PropertyBag[ItemSchema.DateTimeReceived] = value;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x000AD306 File Offset: 0x000AB506
		// (set) Token: 0x06002963 RID: 10595 RVA: 0x000AD313 File Offset: 0x000AB513
		[IgnoreDataMember]
		[XmlIgnore]
		public bool DateTimeReceivedSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.DateTimeReceived);
			}
			set
			{
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x000AD315 File Offset: 0x000AB515
		// (set) Token: 0x06002965 RID: 10597 RVA: 0x000AD327 File Offset: 0x000AB527
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public int? Size
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(ItemSchema.Size);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(ItemSchema.Size, value);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x000AD33A File Offset: 0x000AB53A
		// (set) Token: 0x06002967 RID: 10599 RVA: 0x000AD347 File Offset: 0x000AB547
		[XmlIgnore]
		[IgnoreDataMember]
		public bool SizeSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.Size);
			}
			set
			{
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000AD349 File Offset: 0x000AB549
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x000AD35B File Offset: 0x000AB55B
		[XmlArrayItem("String", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 11)]
		public string[] Categories
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ItemSchema.Categories);
			}
			set
			{
				base.PropertyBag[ItemSchema.Categories] = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x000AD36E File Offset: 0x000AB56E
		// (set) Token: 0x0600296B RID: 10603 RVA: 0x000AD385 File Offset: 0x000AB585
		[XmlElement]
		[IgnoreDataMember]
		public ImportanceType Importance
		{
			get
			{
				if (!this.ImportanceSpecified)
				{
					return ImportanceType.Normal;
				}
				return EnumUtilities.Parse<ImportanceType>(this.ImportanceString);
			}
			set
			{
				this.ImportanceString = EnumUtilities.ToString<ImportanceType>(value);
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x000AD393 File Offset: 0x000AB593
		// (set) Token: 0x0600296D RID: 10605 RVA: 0x000AD3A5 File Offset: 0x000AB5A5
		[DataMember(Name = "Importance", EmitDefaultValue = false, Order = 6)]
		[XmlIgnore]
		public string ImportanceString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.Importance);
			}
			set
			{
				base.PropertyBag[ItemSchema.Importance] = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x000AD3B8 File Offset: 0x000AB5B8
		// (set) Token: 0x0600296F RID: 10607 RVA: 0x000AD3C5 File Offset: 0x000AB5C5
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ImportanceSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.Importance);
			}
			set
			{
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x000AD3C7 File Offset: 0x000AB5C7
		// (set) Token: 0x06002971 RID: 10609 RVA: 0x000AD3D9 File Offset: 0x000AB5D9
		[DataMember(EmitDefaultValue = false, Order = 13)]
		public string InReplyTo
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.InReplyTo);
			}
			set
			{
				base.PropertyBag[ItemSchema.InReplyTo] = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x000AD3EC File Offset: 0x000AB5EC
		// (set) Token: 0x06002973 RID: 10611 RVA: 0x000AD3FE File Offset: 0x000AB5FE
		[DataMember(EmitDefaultValue = false, Order = 14)]
		public bool? IsSubmitted
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.IsSubmitted);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.IsSubmitted, value);
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000AD411 File Offset: 0x000AB611
		// (set) Token: 0x06002975 RID: 10613 RVA: 0x000AD41E File Offset: 0x000AB61E
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsSubmittedSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.IsSubmitted);
			}
			set
			{
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000AD420 File Offset: 0x000AB620
		// (set) Token: 0x06002977 RID: 10615 RVA: 0x000AD432 File Offset: 0x000AB632
		[DataMember(EmitDefaultValue = false, Order = 15)]
		public bool? IsDraft
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.IsDraft);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.IsDraft, value);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x000AD445 File Offset: 0x000AB645
		// (set) Token: 0x06002979 RID: 10617 RVA: 0x000AD452 File Offset: 0x000AB652
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsDraftSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.IsDraft);
			}
			set
			{
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000AD454 File Offset: 0x000AB654
		// (set) Token: 0x0600297B RID: 10619 RVA: 0x000AD466 File Offset: 0x000AB666
		[DataMember(EmitDefaultValue = false, Order = 16)]
		public bool? IsFromMe
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.IsFromMe);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.IsFromMe, value);
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x000AD479 File Offset: 0x000AB679
		// (set) Token: 0x0600297D RID: 10621 RVA: 0x000AD486 File Offset: 0x000AB686
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsFromMeSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.IsFromMe);
			}
			set
			{
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000AD488 File Offset: 0x000AB688
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x000AD49A File Offset: 0x000AB69A
		[DataMember(EmitDefaultValue = false, Order = 17)]
		public bool? IsResend
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.IsResend);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.IsResend, value);
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x000AD4AD File Offset: 0x000AB6AD
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x000AD4BA File Offset: 0x000AB6BA
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsResendSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.IsResend);
			}
			set
			{
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000AD4BC File Offset: 0x000AB6BC
		// (set) Token: 0x06002983 RID: 10627 RVA: 0x000AD4CE File Offset: 0x000AB6CE
		[DataMember(EmitDefaultValue = false, Order = 18)]
		public bool? IsUnmodified
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.IsUnmodified);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.IsUnmodified, value);
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000AD4E1 File Offset: 0x000AB6E1
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x000AD4EE File Offset: 0x000AB6EE
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsUnmodifiedSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.IsUnmodified);
			}
			set
			{
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000AD4F0 File Offset: 0x000AB6F0
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x000AD502 File Offset: 0x000AB702
		[DataMember(EmitDefaultValue = false, Order = 19)]
		[XmlArrayItem("InternetMessageHeader", IsNullable = false)]
		public InternetHeaderType[] InternetMessageHeaders
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<InternetHeaderType[]>(ItemSchema.InternetMessageHeaders);
			}
			set
			{
				base.PropertyBag[ItemSchema.InternetMessageHeaders] = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000AD515 File Offset: 0x000AB715
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x000AD527 File Offset: 0x000AB727
		[DataMember(EmitDefaultValue = false, Order = 20)]
		[DateTimeString]
		public string DateTimeSent
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.DateTimeSent);
			}
			set
			{
				base.PropertyBag[ItemSchema.DateTimeSent] = value;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x000AD53A File Offset: 0x000AB73A
		// (set) Token: 0x0600298B RID: 10635 RVA: 0x000AD547 File Offset: 0x000AB747
		[IgnoreDataMember]
		[XmlIgnore]
		public bool DateTimeSentSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.DateTimeSent);
			}
			set
			{
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x000AD549 File Offset: 0x000AB749
		// (set) Token: 0x0600298D RID: 10637 RVA: 0x000AD55B File Offset: 0x000AB75B
		[DataMember(EmitDefaultValue = false, Order = 21)]
		[DateTimeString]
		public string DateTimeCreated
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.DateTimeCreated);
			}
			set
			{
				base.PropertyBag[ItemSchema.DateTimeCreated] = value;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x000AD56E File Offset: 0x000AB76E
		// (set) Token: 0x0600298F RID: 10639 RVA: 0x000AD57B File Offset: 0x000AB77B
		[XmlIgnore]
		[IgnoreDataMember]
		public bool DateTimeCreatedSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.DateTimeCreated);
			}
			set
			{
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x000AD57D File Offset: 0x000AB77D
		// (set) Token: 0x06002991 RID: 10641 RVA: 0x000AD58F File Offset: 0x000AB78F
		[XmlArrayItem("RemoveItem", typeof(RemoveItemType), IsNullable = false)]
		[XmlArrayItem("ReplyToItem", typeof(ReplyToItemType), IsNullable = false)]
		[XmlArrayItem("SuppressReadReceipt", typeof(SuppressReadReceiptType), IsNullable = false)]
		[XmlArrayItem("CancelCalendarItem", typeof(CancelCalendarItemType), IsNullable = false)]
		[XmlArrayItem("ReplyAllToItem", typeof(ReplyAllToItemType), IsNullable = false)]
		[XmlArrayItem("TentativelyAcceptItem", typeof(TentativelyAcceptItemType), IsNullable = false)]
		[XmlArrayItem("AddItemToMyCalendar", typeof(AddItemToMyCalendarType), IsNullable = false)]
		[XmlArrayItem("ProposeNewTime", typeof(ProposeNewTimeType), IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 22)]
		[XmlArrayItem("DeclineItem", typeof(DeclineItemType), IsNullable = false)]
		[XmlArrayItem("AcceptItem", typeof(AcceptItemType), IsNullable = false)]
		[XmlArrayItem("ForwardItem", typeof(ForwardItemType), IsNullable = false)]
		[XmlArrayItem("PostReplyItem", typeof(PostReplyItemType), IsNullable = false)]
		[XmlArrayItem("AcceptSharingInvitation", typeof(AcceptSharingInvitationType), IsNullable = false)]
		public ResponseObjectType[] ResponseObjects
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ResponseObjectType[]>(ItemSchema.ResponseObjects);
			}
			set
			{
				base.PropertyBag[ItemSchema.ResponseObjects] = value;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x000AD5A2 File Offset: 0x000AB7A2
		// (set) Token: 0x06002993 RID: 10643 RVA: 0x000AD5B4 File Offset: 0x000AB7B4
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 23)]
		public string ReminderDueBy
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.ReminderDueBy);
			}
			set
			{
				base.PropertyBag[ItemSchema.ReminderDueBy] = value;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x000AD5C7 File Offset: 0x000AB7C7
		// (set) Token: 0x06002995 RID: 10645 RVA: 0x000AD5D4 File Offset: 0x000AB7D4
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ReminderDueBySpecified
		{
			get
			{
				return base.IsSet(ItemSchema.ReminderDueBy);
			}
			set
			{
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x000AD5D6 File Offset: 0x000AB7D6
		// (set) Token: 0x06002997 RID: 10647 RVA: 0x000AD5E8 File Offset: 0x000AB7E8
		[DataMember(EmitDefaultValue = false, Order = 24)]
		public bool? ReminderIsSet
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.ReminderIsSet);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.ReminderIsSet, value);
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x000AD5FB File Offset: 0x000AB7FB
		// (set) Token: 0x06002999 RID: 10649 RVA: 0x000AD608 File Offset: 0x000AB808
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ReminderIsSetSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.ReminderIsSet);
			}
			set
			{
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x000AD60A File Offset: 0x000AB80A
		// (set) Token: 0x0600299B RID: 10651 RVA: 0x000AD617 File Offset: 0x000AB817
		[XmlIgnore]
		[IgnoreDataMember]
		public Guid MailboxGuid
		{
			get
			{
				return base.GetValueOrDefault<Guid>(ItemSchema.MailboxGuid);
			}
			set
			{
				this[ItemSchema.MailboxGuid] = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x000AD62A File Offset: 0x000AB82A
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x000AD63C File Offset: 0x000AB83C
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 25)]
		public string ReminderNextTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.ReminderNextTime);
			}
			set
			{
				base.PropertyBag[ItemSchema.ReminderNextTime] = value;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x000AD64F File Offset: 0x000AB84F
		// (set) Token: 0x0600299F RID: 10655 RVA: 0x000AD65C File Offset: 0x000AB85C
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ReminderNextTimeSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.ReminderNextTime);
			}
			set
			{
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000AD65E File Offset: 0x000AB85E
		// (set) Token: 0x060029A1 RID: 10657 RVA: 0x000AD670 File Offset: 0x000AB870
		[DataMember(EmitDefaultValue = false, Order = 26)]
		public int? ReminderMinutesBeforeStart
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(ItemSchema.ReminderMinutesBeforeStart);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(ItemSchema.ReminderMinutesBeforeStart, value);
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000AD683 File Offset: 0x000AB883
		// (set) Token: 0x060029A3 RID: 10659 RVA: 0x000AD690 File Offset: 0x000AB890
		[IgnoreDataMember]
		[XmlIgnore]
		public bool ReminderMinutesBeforeStartSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.ReminderMinutesBeforeStart);
			}
			set
			{
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x000AD692 File Offset: 0x000AB892
		// (set) Token: 0x060029A5 RID: 10661 RVA: 0x000AD6A4 File Offset: 0x000AB8A4
		[DataMember(EmitDefaultValue = false, Order = 27)]
		public string DisplayCc
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.DisplayCc);
			}
			set
			{
				base.PropertyBag[ItemSchema.DisplayCc] = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x000AD6B7 File Offset: 0x000AB8B7
		// (set) Token: 0x060029A7 RID: 10663 RVA: 0x000AD6C9 File Offset: 0x000AB8C9
		[DataMember(EmitDefaultValue = false, Order = 28)]
		public string DisplayTo
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.DisplayTo);
			}
			set
			{
				base.PropertyBag[ItemSchema.DisplayTo] = value;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000AD6DC File Offset: 0x000AB8DC
		// (set) Token: 0x060029A9 RID: 10665 RVA: 0x000AD6EE File Offset: 0x000AB8EE
		[DataMember(EmitDefaultValue = false, Order = 29)]
		public bool? HasAttachments
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.HasAttachments);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.HasAttachments, value);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x000AD701 File Offset: 0x000AB901
		// (set) Token: 0x060029AB RID: 10667 RVA: 0x000AD70E File Offset: 0x000AB90E
		[XmlIgnore]
		[IgnoreDataMember]
		public bool HasAttachmentsSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.HasAttachments);
			}
			set
			{
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x000AD710 File Offset: 0x000AB910
		// (set) Token: 0x060029AD RID: 10669 RVA: 0x000AD722 File Offset: 0x000AB922
		[DataMember(EmitDefaultValue = false, Order = 30)]
		[XmlElement("ExtendedProperty")]
		public ExtendedPropertyType[] ExtendedProperty
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ExtendedPropertyType[]>(ItemSchema.ExtendedProperty);
			}
			set
			{
				base.PropertyBag[ItemSchema.ExtendedProperty] = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x000AD735 File Offset: 0x000AB935
		// (set) Token: 0x060029AF RID: 10671 RVA: 0x000AD747 File Offset: 0x000AB947
		[XmlElement(DataType = "language")]
		[DataMember(EmitDefaultValue = false, Order = 31)]
		public string Culture
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.Culture);
			}
			set
			{
				base.PropertyBag[ItemSchema.Culture] = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x000AD75A File Offset: 0x000AB95A
		// (set) Token: 0x060029B1 RID: 10673 RVA: 0x000AD76C File Offset: 0x000AB96C
		[DataMember(EmitDefaultValue = false, Order = 32)]
		public EffectiveRightsType EffectiveRights
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EffectiveRightsType>(ItemSchema.EffectiveRights);
			}
			set
			{
				base.PropertyBag[ItemSchema.EffectiveRights] = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x000AD77F File Offset: 0x000AB97F
		// (set) Token: 0x060029B3 RID: 10675 RVA: 0x000AD791 File Offset: 0x000AB991
		[DataMember(EmitDefaultValue = false, Order = 33)]
		public string LastModifiedName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.LastModifiedName);
			}
			set
			{
				base.PropertyBag[ItemSchema.LastModifiedName] = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x000AD7A4 File Offset: 0x000AB9A4
		// (set) Token: 0x060029B5 RID: 10677 RVA: 0x000AD7B6 File Offset: 0x000AB9B6
		[DataMember(EmitDefaultValue = false, Order = 34)]
		[DateTimeString]
		public string LastModifiedTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.LastModifiedTime);
			}
			set
			{
				base.PropertyBag[ItemSchema.LastModifiedTime] = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x000AD7C9 File Offset: 0x000AB9C9
		// (set) Token: 0x060029B7 RID: 10679 RVA: 0x000AD7D6 File Offset: 0x000AB9D6
		[XmlIgnore]
		[IgnoreDataMember]
		public bool LastModifiedTimeSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.LastModifiedTime);
			}
			set
			{
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x000AD7D8 File Offset: 0x000AB9D8
		// (set) Token: 0x060029B9 RID: 10681 RVA: 0x000AD7EA File Offset: 0x000AB9EA
		[DataMember(EmitDefaultValue = false, Order = 35)]
		public bool? IsAssociated
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.IsAssociated);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.IsAssociated, value);
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x000AD7FD File Offset: 0x000AB9FD
		// (set) Token: 0x060029BB RID: 10683 RVA: 0x000AD80A File Offset: 0x000ABA0A
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsAssociatedSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.IsAssociated);
			}
			set
			{
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000AD80C File Offset: 0x000ABA0C
		// (set) Token: 0x060029BD RID: 10685 RVA: 0x000AD835 File Offset: 0x000ABA35
		[DataMember(EmitDefaultValue = false, Order = 36)]
		public string WebClientReadFormQueryString
		{
			get
			{
				StringBuilder valueOrDefault = base.PropertyBag.GetValueOrDefault<StringBuilder>(ItemSchema.WebClientReadFormQueryString);
				if (valueOrDefault != null)
				{
					return valueOrDefault.ToString();
				}
				return null;
			}
			set
			{
				base.PropertyBag[ItemSchema.WebClientReadFormQueryString] = new StringBuilder(value);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x000AD850 File Offset: 0x000ABA50
		// (set) Token: 0x060029BF RID: 10687 RVA: 0x000AD879 File Offset: 0x000ABA79
		[DataMember(EmitDefaultValue = false, Order = 37)]
		public string WebClientEditFormQueryString
		{
			get
			{
				StringBuilder valueOrDefault = base.PropertyBag.GetValueOrDefault<StringBuilder>(ItemSchema.WebClientEditFormQueryString);
				if (valueOrDefault != null)
				{
					return valueOrDefault.ToString();
				}
				return null;
			}
			set
			{
				base.PropertyBag[ItemSchema.WebClientEditFormQueryString] = new StringBuilder(value);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x000AD891 File Offset: 0x000ABA91
		// (set) Token: 0x060029C1 RID: 10689 RVA: 0x000AD8A3 File Offset: 0x000ABAA3
		[DataMember(EmitDefaultValue = false, Order = 38)]
		public ItemId ConversationId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ItemId>(ItemSchema.ConversationId);
			}
			set
			{
				base.PropertyBag[ItemSchema.ConversationId] = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000AD8B6 File Offset: 0x000ABAB6
		// (set) Token: 0x060029C3 RID: 10691 RVA: 0x000AD8C8 File Offset: 0x000ABAC8
		[DataMember(EmitDefaultValue = false, Order = 39)]
		public BodyContentType UniqueBody
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<BodyContentType>(ItemSchema.UniqueBody);
			}
			set
			{
				base.PropertyBag[ItemSchema.UniqueBody] = value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000AD8DB File Offset: 0x000ABADB
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x000AD8ED File Offset: 0x000ABAED
		[DataMember(EmitDefaultValue = false, Order = 40)]
		public FlagType Flag
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<FlagType>(ItemSchema.Flag);
			}
			set
			{
				base.PropertyBag[ItemSchema.Flag] = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000AD900 File Offset: 0x000ABB00
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x000AD912 File Offset: 0x000ABB12
		[XmlElement(DataType = "base64Binary")]
		[IgnoreDataMember]
		public byte[] StoreEntryId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<byte[]>(ItemSchema.StoreEntryId);
			}
			set
			{
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000AD914 File Offset: 0x000ABB14
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x000AD933 File Offset: 0x000ABB33
		[DataMember(Name = "StoreEntryId", EmitDefaultValue = false, Order = 41)]
		[XmlIgnore]
		public string StoreEntryIdString
		{
			get
			{
				byte[] storeEntryId = this.StoreEntryId;
				if (storeEntryId == null)
				{
					return null;
				}
				return Convert.ToBase64String(storeEntryId);
			}
			set
			{
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000AD935 File Offset: 0x000ABB35
		// (set) Token: 0x060029CB RID: 10699 RVA: 0x000AD947 File Offset: 0x000ABB47
		[XmlElement(DataType = "base64Binary")]
		[IgnoreDataMember]
		public byte[] InstanceKey
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<byte[]>(ItemSchema.InstanceKey);
			}
			set
			{
				base.PropertyBag[ItemSchema.InstanceKey] = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000AD95C File Offset: 0x000ABB5C
		// (set) Token: 0x060029CD RID: 10701 RVA: 0x000AD97B File Offset: 0x000ABB7B
		[DataMember(Name = "InstanceKey", EmitDefaultValue = false, Order = 42)]
		[XmlIgnore]
		public string InstanceKeyString
		{
			get
			{
				byte[] instanceKey = this.InstanceKey;
				if (instanceKey == null)
				{
					return null;
				}
				return Convert.ToBase64String(instanceKey);
			}
			set
			{
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x000AD97D File Offset: 0x000ABB7D
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x000AD98F File Offset: 0x000ABB8F
		[DataMember(EmitDefaultValue = false, Order = 43)]
		public BodyContentType NormalizedBody
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<BodyContentType>(ItemSchema.NormalizedBody);
			}
			set
			{
				base.PropertyBag[ItemSchema.NormalizedBody] = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060029D0 RID: 10704 RVA: 0x000AD9A2 File Offset: 0x000ABBA2
		// (set) Token: 0x060029D1 RID: 10705 RVA: 0x000AD9B4 File Offset: 0x000ABBB4
		[DataMember(EmitDefaultValue = false, Order = 44)]
		public EntityExtractionResultType EntityExtractionResult
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EntityExtractionResultType>(ItemSchema.EntityExtractionResult);
			}
			set
			{
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000AD9B6 File Offset: 0x000ABBB6
		// (set) Token: 0x060029D3 RID: 10707 RVA: 0x000AD9C8 File Offset: 0x000ABBC8
		[DataMember(Name = "PolicyTag", EmitDefaultValue = false, Order = 45)]
		public RetentionTagType PolicyTag
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<RetentionTagType>(ItemSchema.PolicyTag);
			}
			set
			{
				base.PropertyBag[ItemSchema.PolicyTag] = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000AD9DB File Offset: 0x000ABBDB
		// (set) Token: 0x060029D5 RID: 10709 RVA: 0x000AD9ED File Offset: 0x000ABBED
		[DataMember(Name = "ArchiveTag", EmitDefaultValue = false, Order = 46)]
		public RetentionTagType ArchiveTag
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<RetentionTagType>(ItemSchema.ArchiveTag);
			}
			set
			{
				base.PropertyBag[ItemSchema.ArchiveTag] = value;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000ADA00 File Offset: 0x000ABC00
		// (set) Token: 0x060029D7 RID: 10711 RVA: 0x000ADA12 File Offset: 0x000ABC12
		[DateTimeString]
		[DataMember(Name = "RetentionDate", EmitDefaultValue = false, Order = 47)]
		public string RetentionDate
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.RetentionDate);
			}
			set
			{
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x000ADA14 File Offset: 0x000ABC14
		// (set) Token: 0x060029D9 RID: 10713 RVA: 0x000ADA26 File Offset: 0x000ABC26
		[DataMember(EmitDefaultValue = false, Order = 48)]
		public string Preview
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.Preview);
			}
			set
			{
				base.PropertyBag[ItemSchema.Preview] = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x000ADA39 File Offset: 0x000ABC39
		// (set) Token: 0x060029DB RID: 10715 RVA: 0x000ADA4B File Offset: 0x000ABC4B
		[DataMember(EmitDefaultValue = false, Order = 49)]
		public RightsManagementLicenseDataType RightsManagementLicenseData
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<RightsManagementLicenseDataType>(ItemSchema.RightsManagementLicenseData);
			}
			set
			{
				base.PropertyBag[ItemSchema.RightsManagementLicenseData] = value;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000ADA5E File Offset: 0x000ABC5E
		// (set) Token: 0x060029DD RID: 10717 RVA: 0x000ADA70 File Offset: 0x000ABC70
		[XmlArrayItem("PredictedActionReason", typeof(PredictedActionReasonType), IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Name = "PredictedActionReasons", Order = 52)]
		public PredictedActionReasonType[] PredictedActionReasons
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PredictedActionReasonType[]>(ItemSchema.PredictedActionReasons);
			}
			set
			{
				base.PropertyBag[ItemSchema.PredictedActionReasons] = value;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060029DE RID: 10718 RVA: 0x000ADA83 File Offset: 0x000ABC83
		// (set) Token: 0x060029DF RID: 10719 RVA: 0x000ADA90 File Offset: 0x000ABC90
		[IgnoreDataMember]
		[XmlIgnore]
		public bool PredictedActionReasonsSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.PredictedActionReasons);
			}
			set
			{
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x000ADA92 File Offset: 0x000ABC92
		// (set) Token: 0x060029E1 RID: 10721 RVA: 0x000ADAA4 File Offset: 0x000ABCA4
		[DataMember(EmitDefaultValue = false, Order = 53)]
		public bool? IsClutter
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.IsClutter);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.IsClutter, value);
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x000ADAB7 File Offset: 0x000ABCB7
		// (set) Token: 0x060029E3 RID: 10723 RVA: 0x000ADAC4 File Offset: 0x000ABCC4
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsClutterSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.IsClutter);
			}
			set
			{
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x000ADAC6 File Offset: 0x000ABCC6
		// (set) Token: 0x060029E5 RID: 10725 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
		[DataMember(EmitDefaultValue = false, Order = 54)]
		public bool? BlockStatus
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.BlockStatus);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ItemSchema.BlockStatus, value);
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000ADAEB File Offset: 0x000ABCEB
		// (set) Token: 0x060029E7 RID: 10727 RVA: 0x000ADAF8 File Offset: 0x000ABCF8
		[XmlIgnore]
		[IgnoreDataMember]
		public bool BlockStatusSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.BlockStatus);
			}
			set
			{
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000ADAFA File Offset: 0x000ABCFA
		// (set) Token: 0x060029E9 RID: 10729 RVA: 0x000ADB0C File Offset: 0x000ABD0C
		[DataMember(EmitDefaultValue = false, Order = 55)]
		public bool? HasBlockedImages
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.HasBlockedImages);
			}
			set
			{
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x000ADB0E File Offset: 0x000ABD0E
		// (set) Token: 0x060029EB RID: 10731 RVA: 0x000ADB1B File Offset: 0x000ABD1B
		[XmlIgnore]
		[IgnoreDataMember]
		public bool HasBlockedImagesSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.HasBlockedImages);
			}
			set
			{
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x000ADB1D File Offset: 0x000ABD1D
		// (set) Token: 0x060029ED RID: 10733 RVA: 0x000ADB2F File Offset: 0x000ABD2F
		[DataMember(EmitDefaultValue = false, Order = 56)]
		public BodyContentType TextBody
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<BodyContentType>(ItemSchema.TextBody);
			}
			set
			{
				base.PropertyBag[ItemSchema.TextBody] = value;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x000ADB42 File Offset: 0x000ABD42
		// (set) Token: 0x060029EF RID: 10735 RVA: 0x000ADB4A File Offset: 0x000ABD4A
		[DataMember(EmitDefaultValue = false, Order = 57)]
		[XmlIgnore]
		public bool ContainsOnlyMandatoryProperties { get; set; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000ADB53 File Offset: 0x000ABD53
		// (set) Token: 0x060029F1 RID: 10737 RVA: 0x000ADB6A File Offset: 0x000ABD6A
		[IgnoreDataMember]
		[XmlElement]
		public IconIndexType IconIndex
		{
			get
			{
				if (!this.IconIndexSpecified)
				{
					return (IconIndexType)0;
				}
				return EnumUtilities.Parse<IconIndexType>(this.IconIndexString);
			}
			set
			{
				this.IconIndexString = EnumUtilities.ToString<IconIndexType>(value);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x000ADB78 File Offset: 0x000ABD78
		// (set) Token: 0x060029F3 RID: 10739 RVA: 0x000ADB8A File Offset: 0x000ABD8A
		[XmlIgnore]
		[DataMember(Name = "IconIndex", EmitDefaultValue = false, Order = 58)]
		public string IconIndexString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.IconIndex);
			}
			set
			{
				base.PropertyBag[ItemSchema.IconIndex] = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060029F4 RID: 10740 RVA: 0x000ADB9D File Offset: 0x000ABD9D
		// (set) Token: 0x060029F5 RID: 10741 RVA: 0x000ADBAA File Offset: 0x000ABDAA
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IconIndexSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.IconIndex);
			}
			set
			{
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060029F6 RID: 10742 RVA: 0x000ADBAC File Offset: 0x000ABDAC
		// (set) Token: 0x060029F7 RID: 10743 RVA: 0x000ADBB4 File Offset: 0x000ABDB4
		[DataMember(EmitDefaultValue = false, Order = 59)]
		[XmlIgnore]
		public PropertyExistenceType PropertyExistence { get; set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000ADBBD File Offset: 0x000ABDBD
		// (set) Token: 0x060029F9 RID: 10745 RVA: 0x000ADBD4 File Offset: 0x000ABDD4
		[DataMember(EmitDefaultValue = false, Order = 60)]
		[XmlIgnore]
		public PropertyErrorType[] ErrorProperties
		{
			get
			{
				if (this.errorProperties == null)
				{
					return null;
				}
				return this.errorProperties.ToArray();
			}
			set
			{
				this.errorProperties = ((value != null) ? new List<PropertyErrorType>(value) : null);
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x000ADBE8 File Offset: 0x000ABDE8
		// (set) Token: 0x060029FB RID: 10747 RVA: 0x000ADBF0 File Offset: 0x000ABDF0
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 61)]
		public ConversationType Conversation { get; set; }

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x000ADBF9 File Offset: 0x000ABDF9
		// (set) Token: 0x060029FD RID: 10749 RVA: 0x000ADC0B File Offset: 0x000ABE0B
		[DataMember(EmitDefaultValue = false, Order = 62)]
		[XmlIgnore]
		public short? RichContent
		{
			get
			{
				return base.PropertyBag.GetNullableValue<short>(ItemSchema.RichContent);
			}
			set
			{
				base.PropertyBag.SetNullableValue<short>(ItemSchema.RichContent, value);
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x000ADC1E File Offset: 0x000ABE1E
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x000ADC30 File Offset: 0x000ABE30
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 63)]
		[DateTimeString]
		public string ReceivedOrRenewTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.ReceivedOrRenewTime);
			}
			set
			{
				base.PropertyBag[ItemSchema.ReceivedOrRenewTime] = value;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000ADC43 File Offset: 0x000ABE43
		// (set) Token: 0x06002A01 RID: 10753 RVA: 0x000ADC50 File Offset: 0x000ABE50
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ReceivedOrRenewTimeSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.ReceivedOrRenewTime);
			}
			set
			{
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000ADC52 File Offset: 0x000ABE52
		// (set) Token: 0x06002A03 RID: 10755 RVA: 0x000ADC64 File Offset: 0x000ABE64
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 64)]
		public string WorkingSetSourcePartition
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.WorkingSetSourcePartition);
			}
			set
			{
				base.PropertyBag[ItemSchema.WorkingSetSourcePartition] = value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000ADC77 File Offset: 0x000ABE77
		// (set) Token: 0x06002A05 RID: 10757 RVA: 0x000ADC89 File Offset: 0x000ABE89
		[XmlIgnore]
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 64)]
		public string RenewTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ItemSchema.RenewTime);
			}
			set
			{
				base.PropertyBag[ItemSchema.RenewTime] = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000ADC9C File Offset: 0x000ABE9C
		// (set) Token: 0x06002A07 RID: 10759 RVA: 0x000ADCA9 File Offset: 0x000ABEA9
		[IgnoreDataMember]
		[XmlIgnore]
		public bool RenewTimeSpecified
		{
			get
			{
				return base.IsSet(ItemSchema.RenewTime);
			}
			set
			{
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000ADCAB File Offset: 0x000ABEAB
		// (set) Token: 0x06002A09 RID: 10761 RVA: 0x000ADCBD File Offset: 0x000ABEBD
		[DataMember(EmitDefaultValue = false, Order = 66)]
		[XmlIgnore]
		public bool? SupportsSideConversation
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ItemSchema.SupportsSideConversation);
			}
			set
			{
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000ADCBF File Offset: 0x000ABEBF
		// (set) Token: 0x06002A0B RID: 10763 RVA: 0x000ADCD1 File Offset: 0x000ABED1
		[DataMember(EmitDefaultValue = false, Order = 67)]
		public MimeContentType MimeContentUTF8
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<MimeContentType>(ItemSchema.MimeContentUTF8);
			}
			set
			{
				base.PropertyBag[ItemSchema.MimeContentUTF8] = value;
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000ADCE4 File Offset: 0x000ABEE4
		internal void AddPropertyError(PropertyPath property, PropertyErrorCodeType errorCode)
		{
			if (this.errorProperties == null)
			{
				this.errorProperties = new List<PropertyErrorType>();
			}
			this.errorProperties.Add(new PropertyErrorType
			{
				PropertyPath = property,
				ErrorCode = errorCode
			});
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x000ADD24 File Offset: 0x000ABF24
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.Unknown;
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000ADD28 File Offset: 0x000ABF28
		internal override void AddExtendedPropertyValue(ExtendedPropertyType extendedPropertyToAdd)
		{
			ExtendedPropertyType[] extendedProperty = this.ExtendedProperty;
			int num = (extendedProperty == null) ? 0 : extendedProperty.Length;
			ExtendedPropertyType[] array = new ExtendedPropertyType[num + 1];
			if (num > 0)
			{
				Array.Copy(extendedProperty, array, num);
			}
			array[num] = extendedPropertyToAdd;
			this.ExtendedProperty = array;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000ADD68 File Offset: 0x000ABF68
		internal bool ContainsExtendedProperty(ExtendedPropertyUri extendedPropertyUri)
		{
			if (this.ExtendedProperty != null)
			{
				foreach (ExtendedPropertyType extendedPropertyType in this.ExtendedProperty)
				{
					if (ExtendedPropertyUri.AreEqual(extendedPropertyUri, extendedPropertyType.ExtendedFieldURI))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04001A07 RID: 6663
		private static LazyMember<Dictionary<StoreObjectType, Func<ItemType>>> createMethods = new LazyMember<Dictionary<StoreObjectType, Func<ItemType>>>(delegate()
		{
			Dictionary<StoreObjectType, Func<ItemType>> dictionary = new Dictionary<StoreObjectType, Func<ItemType>>();
			dictionary.Add(StoreObjectType.CalendarItem, () => new EwsCalendarItemType());
			dictionary.Add(StoreObjectType.CalendarItemOccurrence, () => new EwsCalendarItemType());
			dictionary.Add(StoreObjectType.Contact, () => new ContactItemType());
			dictionary.Add(StoreObjectType.DistributionList, () => new DistributionListType());
			dictionary.Add(StoreObjectType.MeetingCancellation, () => new MeetingCancellationMessageType());
			dictionary.Add(StoreObjectType.MeetingMessage, () => new MeetingMessageType());
			dictionary.Add(StoreObjectType.MeetingRequest, () => new MeetingRequestMessageType());
			dictionary.Add(StoreObjectType.MeetingResponse, () => new MeetingResponseMessageType());
			dictionary.Add(StoreObjectType.Message, () => new MessageType());
			dictionary.Add(StoreObjectType.Post, () => new PostItemType());
			dictionary.Add(StoreObjectType.Task, () => new TaskType());
			dictionary.Add(StoreObjectType.Unknown, () => new MessageType());
			return dictionary;
		});

		// Token: 0x04001A08 RID: 6664
		private List<PropertyErrorType> errorProperties;
	}
}
