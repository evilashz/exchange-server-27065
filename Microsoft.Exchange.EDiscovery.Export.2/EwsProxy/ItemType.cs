using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000116 RID: 278
	[XmlInclude(typeof(DistributionListType))]
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
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(ContactItemType))]
	[XmlInclude(typeof(CalendarItemType))]
	[XmlInclude(typeof(MessageType))]
	[XmlInclude(typeof(MeetingMessageType))]
	[XmlInclude(typeof(MeetingCancellationMessageType))]
	[XmlInclude(typeof(PostItemType))]
	[XmlInclude(typeof(TaskType))]
	[Serializable]
	public class ItemType
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002159D File Offset: 0x0001F79D
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x000215A5 File Offset: 0x0001F7A5
		public MimeContentType MimeContent
		{
			get
			{
				return this.mimeContentField;
			}
			set
			{
				this.mimeContentField = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000215AE File Offset: 0x0001F7AE
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x000215B6 File Offset: 0x0001F7B6
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000215BF File Offset: 0x0001F7BF
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x000215C7 File Offset: 0x0001F7C7
		public FolderIdType ParentFolderId
		{
			get
			{
				return this.parentFolderIdField;
			}
			set
			{
				this.parentFolderIdField = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x000215D0 File Offset: 0x0001F7D0
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x000215D8 File Offset: 0x0001F7D8
		public string ItemClass
		{
			get
			{
				return this.itemClassField;
			}
			set
			{
				this.itemClassField = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000215E1 File Offset: 0x0001F7E1
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x000215E9 File Offset: 0x0001F7E9
		public string Subject
		{
			get
			{
				return this.subjectField;
			}
			set
			{
				this.subjectField = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000215F2 File Offset: 0x0001F7F2
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x000215FA File Offset: 0x0001F7FA
		public SensitivityChoicesType Sensitivity
		{
			get
			{
				return this.sensitivityField;
			}
			set
			{
				this.sensitivityField = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00021603 File Offset: 0x0001F803
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0002160B File Offset: 0x0001F80B
		[XmlIgnore]
		public bool SensitivitySpecified
		{
			get
			{
				return this.sensitivityFieldSpecified;
			}
			set
			{
				this.sensitivityFieldSpecified = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00021614 File Offset: 0x0001F814
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x0002161C File Offset: 0x0001F81C
		public BodyType Body
		{
			get
			{
				return this.bodyField;
			}
			set
			{
				this.bodyField = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00021625 File Offset: 0x0001F825
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x0002162D File Offset: 0x0001F82D
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), IsNullable = false)]
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), IsNullable = false)]
		public AttachmentType[] Attachments
		{
			get
			{
				return this.attachmentsField;
			}
			set
			{
				this.attachmentsField = value;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00021636 File Offset: 0x0001F836
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x0002163E File Offset: 0x0001F83E
		public DateTime DateTimeReceived
		{
			get
			{
				return this.dateTimeReceivedField;
			}
			set
			{
				this.dateTimeReceivedField = value;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00021647 File Offset: 0x0001F847
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0002164F File Offset: 0x0001F84F
		[XmlIgnore]
		public bool DateTimeReceivedSpecified
		{
			get
			{
				return this.dateTimeReceivedFieldSpecified;
			}
			set
			{
				this.dateTimeReceivedFieldSpecified = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00021658 File Offset: 0x0001F858
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00021660 File Offset: 0x0001F860
		public int Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00021669 File Offset: 0x0001F869
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00021671 File Offset: 0x0001F871
		[XmlIgnore]
		public bool SizeSpecified
		{
			get
			{
				return this.sizeFieldSpecified;
			}
			set
			{
				this.sizeFieldSpecified = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0002167A File Offset: 0x0001F87A
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00021682 File Offset: 0x0001F882
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories
		{
			get
			{
				return this.categoriesField;
			}
			set
			{
				this.categoriesField = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002168B File Offset: 0x0001F88B
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00021693 File Offset: 0x0001F893
		public ImportanceChoicesType Importance
		{
			get
			{
				return this.importanceField;
			}
			set
			{
				this.importanceField = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002169C File Offset: 0x0001F89C
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x000216A4 File Offset: 0x0001F8A4
		[XmlIgnore]
		public bool ImportanceSpecified
		{
			get
			{
				return this.importanceFieldSpecified;
			}
			set
			{
				this.importanceFieldSpecified = value;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000216AD File Offset: 0x0001F8AD
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x000216B5 File Offset: 0x0001F8B5
		public string InReplyTo
		{
			get
			{
				return this.inReplyToField;
			}
			set
			{
				this.inReplyToField = value;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x000216BE File Offset: 0x0001F8BE
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x000216C6 File Offset: 0x0001F8C6
		public bool IsSubmitted
		{
			get
			{
				return this.isSubmittedField;
			}
			set
			{
				this.isSubmittedField = value;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x000216CF File Offset: 0x0001F8CF
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x000216D7 File Offset: 0x0001F8D7
		[XmlIgnore]
		public bool IsSubmittedSpecified
		{
			get
			{
				return this.isSubmittedFieldSpecified;
			}
			set
			{
				this.isSubmittedFieldSpecified = value;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x000216E0 File Offset: 0x0001F8E0
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x000216E8 File Offset: 0x0001F8E8
		public bool IsDraft
		{
			get
			{
				return this.isDraftField;
			}
			set
			{
				this.isDraftField = value;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000216F1 File Offset: 0x0001F8F1
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x000216F9 File Offset: 0x0001F8F9
		[XmlIgnore]
		public bool IsDraftSpecified
		{
			get
			{
				return this.isDraftFieldSpecified;
			}
			set
			{
				this.isDraftFieldSpecified = value;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00021702 File Offset: 0x0001F902
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x0002170A File Offset: 0x0001F90A
		public bool IsFromMe
		{
			get
			{
				return this.isFromMeField;
			}
			set
			{
				this.isFromMeField = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00021713 File Offset: 0x0001F913
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x0002171B File Offset: 0x0001F91B
		[XmlIgnore]
		public bool IsFromMeSpecified
		{
			get
			{
				return this.isFromMeFieldSpecified;
			}
			set
			{
				this.isFromMeFieldSpecified = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00021724 File Offset: 0x0001F924
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x0002172C File Offset: 0x0001F92C
		public bool IsResend
		{
			get
			{
				return this.isResendField;
			}
			set
			{
				this.isResendField = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00021735 File Offset: 0x0001F935
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x0002173D File Offset: 0x0001F93D
		[XmlIgnore]
		public bool IsResendSpecified
		{
			get
			{
				return this.isResendFieldSpecified;
			}
			set
			{
				this.isResendFieldSpecified = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00021746 File Offset: 0x0001F946
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x0002174E File Offset: 0x0001F94E
		public bool IsUnmodified
		{
			get
			{
				return this.isUnmodifiedField;
			}
			set
			{
				this.isUnmodifiedField = value;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00021757 File Offset: 0x0001F957
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x0002175F File Offset: 0x0001F95F
		[XmlIgnore]
		public bool IsUnmodifiedSpecified
		{
			get
			{
				return this.isUnmodifiedFieldSpecified;
			}
			set
			{
				this.isUnmodifiedFieldSpecified = value;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00021768 File Offset: 0x0001F968
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00021770 File Offset: 0x0001F970
		[XmlArrayItem("InternetMessageHeader", IsNullable = false)]
		public InternetHeaderType[] InternetMessageHeaders
		{
			get
			{
				return this.internetMessageHeadersField;
			}
			set
			{
				this.internetMessageHeadersField = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00021779 File Offset: 0x0001F979
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00021781 File Offset: 0x0001F981
		public DateTime DateTimeSent
		{
			get
			{
				return this.dateTimeSentField;
			}
			set
			{
				this.dateTimeSentField = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0002178A File Offset: 0x0001F98A
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x00021792 File Offset: 0x0001F992
		[XmlIgnore]
		public bool DateTimeSentSpecified
		{
			get
			{
				return this.dateTimeSentFieldSpecified;
			}
			set
			{
				this.dateTimeSentFieldSpecified = value;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0002179B File Offset: 0x0001F99B
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x000217A3 File Offset: 0x0001F9A3
		public DateTime DateTimeCreated
		{
			get
			{
				return this.dateTimeCreatedField;
			}
			set
			{
				this.dateTimeCreatedField = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x000217AC File Offset: 0x0001F9AC
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x000217B4 File Offset: 0x0001F9B4
		[XmlIgnore]
		public bool DateTimeCreatedSpecified
		{
			get
			{
				return this.dateTimeCreatedFieldSpecified;
			}
			set
			{
				this.dateTimeCreatedFieldSpecified = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x000217BD File Offset: 0x0001F9BD
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x000217C5 File Offset: 0x0001F9C5
		[XmlArrayItem("ForwardItem", typeof(ForwardItemType), IsNullable = false)]
		[XmlArrayItem("RemoveItem", typeof(RemoveItemType), IsNullable = false)]
		[XmlArrayItem("ReplyAllToItem", typeof(ReplyAllToItemType), IsNullable = false)]
		[XmlArrayItem("ReplyToItem", typeof(ReplyToItemType), IsNullable = false)]
		[XmlArrayItem("SuppressReadReceipt", typeof(SuppressReadReceiptType), IsNullable = false)]
		[XmlArrayItem("AcceptItem", typeof(AcceptItemType), IsNullable = false)]
		[XmlArrayItem("TentativelyAcceptItem", typeof(TentativelyAcceptItemType), IsNullable = false)]
		[XmlArrayItem("PostReplyItem", typeof(PostReplyItemType), IsNullable = false)]
		[XmlArrayItem("ProposeNewTime", typeof(ProposeNewTimeType), IsNullable = false)]
		[XmlArrayItem("AcceptSharingInvitation", typeof(AcceptSharingInvitationType), IsNullable = false)]
		[XmlArrayItem("AddItemToMyCalendar", typeof(AddItemToMyCalendarType), IsNullable = false)]
		[XmlArrayItem("CancelCalendarItem", typeof(CancelCalendarItemType), IsNullable = false)]
		[XmlArrayItem("DeclineItem", typeof(DeclineItemType), IsNullable = false)]
		public ResponseObjectType[] ResponseObjects
		{
			get
			{
				return this.responseObjectsField;
			}
			set
			{
				this.responseObjectsField = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000217CE File Offset: 0x0001F9CE
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x000217D6 File Offset: 0x0001F9D6
		public DateTime ReminderDueBy
		{
			get
			{
				return this.reminderDueByField;
			}
			set
			{
				this.reminderDueByField = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000217DF File Offset: 0x0001F9DF
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x000217E7 File Offset: 0x0001F9E7
		[XmlIgnore]
		public bool ReminderDueBySpecified
		{
			get
			{
				return this.reminderDueByFieldSpecified;
			}
			set
			{
				this.reminderDueByFieldSpecified = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x000217F0 File Offset: 0x0001F9F0
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x000217F8 File Offset: 0x0001F9F8
		public bool ReminderIsSet
		{
			get
			{
				return this.reminderIsSetField;
			}
			set
			{
				this.reminderIsSetField = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00021801 File Offset: 0x0001FA01
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00021809 File Offset: 0x0001FA09
		[XmlIgnore]
		public bool ReminderIsSetSpecified
		{
			get
			{
				return this.reminderIsSetFieldSpecified;
			}
			set
			{
				this.reminderIsSetFieldSpecified = value;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00021812 File Offset: 0x0001FA12
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0002181A File Offset: 0x0001FA1A
		public DateTime ReminderNextTime
		{
			get
			{
				return this.reminderNextTimeField;
			}
			set
			{
				this.reminderNextTimeField = value;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00021823 File Offset: 0x0001FA23
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0002182B File Offset: 0x0001FA2B
		[XmlIgnore]
		public bool ReminderNextTimeSpecified
		{
			get
			{
				return this.reminderNextTimeFieldSpecified;
			}
			set
			{
				this.reminderNextTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00021834 File Offset: 0x0001FA34
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x0002183C File Offset: 0x0001FA3C
		public string ReminderMinutesBeforeStart
		{
			get
			{
				return this.reminderMinutesBeforeStartField;
			}
			set
			{
				this.reminderMinutesBeforeStartField = value;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00021845 File Offset: 0x0001FA45
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x0002184D File Offset: 0x0001FA4D
		public string DisplayCc
		{
			get
			{
				return this.displayCcField;
			}
			set
			{
				this.displayCcField = value;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00021856 File Offset: 0x0001FA56
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x0002185E File Offset: 0x0001FA5E
		public string DisplayTo
		{
			get
			{
				return this.displayToField;
			}
			set
			{
				this.displayToField = value;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00021867 File Offset: 0x0001FA67
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0002186F File Offset: 0x0001FA6F
		public bool HasAttachments
		{
			get
			{
				return this.hasAttachmentsField;
			}
			set
			{
				this.hasAttachmentsField = value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00021878 File Offset: 0x0001FA78
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x00021880 File Offset: 0x0001FA80
		[XmlIgnore]
		public bool HasAttachmentsSpecified
		{
			get
			{
				return this.hasAttachmentsFieldSpecified;
			}
			set
			{
				this.hasAttachmentsFieldSpecified = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00021889 File Offset: 0x0001FA89
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x00021891 File Offset: 0x0001FA91
		[XmlElement("ExtendedProperty")]
		public ExtendedPropertyType[] ExtendedProperty
		{
			get
			{
				return this.extendedPropertyField;
			}
			set
			{
				this.extendedPropertyField = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0002189A File Offset: 0x0001FA9A
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x000218A2 File Offset: 0x0001FAA2
		[XmlElement(DataType = "language")]
		public string Culture
		{
			get
			{
				return this.cultureField;
			}
			set
			{
				this.cultureField = value;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000218AB File Offset: 0x0001FAAB
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x000218B3 File Offset: 0x0001FAB3
		public EffectiveRightsType EffectiveRights
		{
			get
			{
				return this.effectiveRightsField;
			}
			set
			{
				this.effectiveRightsField = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000218BC File Offset: 0x0001FABC
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x000218C4 File Offset: 0x0001FAC4
		public string LastModifiedName
		{
			get
			{
				return this.lastModifiedNameField;
			}
			set
			{
				this.lastModifiedNameField = value;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000218CD File Offset: 0x0001FACD
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x000218D5 File Offset: 0x0001FAD5
		public DateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTimeField;
			}
			set
			{
				this.lastModifiedTimeField = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x000218DE File Offset: 0x0001FADE
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x000218E6 File Offset: 0x0001FAE6
		[XmlIgnore]
		public bool LastModifiedTimeSpecified
		{
			get
			{
				return this.lastModifiedTimeFieldSpecified;
			}
			set
			{
				this.lastModifiedTimeFieldSpecified = value;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x000218EF File Offset: 0x0001FAEF
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x000218F7 File Offset: 0x0001FAF7
		public bool IsAssociated
		{
			get
			{
				return this.isAssociatedField;
			}
			set
			{
				this.isAssociatedField = value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00021900 File Offset: 0x0001FB00
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00021908 File Offset: 0x0001FB08
		[XmlIgnore]
		public bool IsAssociatedSpecified
		{
			get
			{
				return this.isAssociatedFieldSpecified;
			}
			set
			{
				this.isAssociatedFieldSpecified = value;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00021911 File Offset: 0x0001FB11
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00021919 File Offset: 0x0001FB19
		public string WebClientReadFormQueryString
		{
			get
			{
				return this.webClientReadFormQueryStringField;
			}
			set
			{
				this.webClientReadFormQueryStringField = value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00021922 File Offset: 0x0001FB22
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0002192A File Offset: 0x0001FB2A
		public string WebClientEditFormQueryString
		{
			get
			{
				return this.webClientEditFormQueryStringField;
			}
			set
			{
				this.webClientEditFormQueryStringField = value;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00021933 File Offset: 0x0001FB33
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0002193B File Offset: 0x0001FB3B
		public ItemIdType ConversationId
		{
			get
			{
				return this.conversationIdField;
			}
			set
			{
				this.conversationIdField = value;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00021944 File Offset: 0x0001FB44
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0002194C File Offset: 0x0001FB4C
		public BodyType UniqueBody
		{
			get
			{
				return this.uniqueBodyField;
			}
			set
			{
				this.uniqueBodyField = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00021955 File Offset: 0x0001FB55
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0002195D File Offset: 0x0001FB5D
		public FlagType Flag
		{
			get
			{
				return this.flagField;
			}
			set
			{
				this.flagField = value;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00021966 File Offset: 0x0001FB66
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0002196E File Offset: 0x0001FB6E
		[XmlElement(DataType = "base64Binary")]
		public byte[] StoreEntryId
		{
			get
			{
				return this.storeEntryIdField;
			}
			set
			{
				this.storeEntryIdField = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00021977 File Offset: 0x0001FB77
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0002197F File Offset: 0x0001FB7F
		[XmlElement(DataType = "base64Binary")]
		public byte[] InstanceKey
		{
			get
			{
				return this.instanceKeyField;
			}
			set
			{
				this.instanceKeyField = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00021988 File Offset: 0x0001FB88
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00021990 File Offset: 0x0001FB90
		public BodyType NormalizedBody
		{
			get
			{
				return this.normalizedBodyField;
			}
			set
			{
				this.normalizedBodyField = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00021999 File Offset: 0x0001FB99
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x000219A1 File Offset: 0x0001FBA1
		public EntityExtractionResultType EntityExtractionResult
		{
			get
			{
				return this.entityExtractionResultField;
			}
			set
			{
				this.entityExtractionResultField = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x000219AA File Offset: 0x0001FBAA
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x000219B2 File Offset: 0x0001FBB2
		public RetentionTagType PolicyTag
		{
			get
			{
				return this.policyTagField;
			}
			set
			{
				this.policyTagField = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x000219BB File Offset: 0x0001FBBB
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x000219C3 File Offset: 0x0001FBC3
		public RetentionTagType ArchiveTag
		{
			get
			{
				return this.archiveTagField;
			}
			set
			{
				this.archiveTagField = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x000219CC File Offset: 0x0001FBCC
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x000219D4 File Offset: 0x0001FBD4
		public DateTime RetentionDate
		{
			get
			{
				return this.retentionDateField;
			}
			set
			{
				this.retentionDateField = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x000219DD File Offset: 0x0001FBDD
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x000219E5 File Offset: 0x0001FBE5
		[XmlIgnore]
		public bool RetentionDateSpecified
		{
			get
			{
				return this.retentionDateFieldSpecified;
			}
			set
			{
				this.retentionDateFieldSpecified = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000219EE File Offset: 0x0001FBEE
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x000219F6 File Offset: 0x0001FBF6
		public string Preview
		{
			get
			{
				return this.previewField;
			}
			set
			{
				this.previewField = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x000219FF File Offset: 0x0001FBFF
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00021A07 File Offset: 0x0001FC07
		public RightsManagementLicenseDataType RightsManagementLicenseData
		{
			get
			{
				return this.rightsManagementLicenseDataField;
			}
			set
			{
				this.rightsManagementLicenseDataField = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00021A10 File Offset: 0x0001FC10
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00021A18 File Offset: 0x0001FC18
		[XmlArrayItem("PredictedActionReason", IsNullable = false)]
		public PredictedActionReasonType[] PredictedActionReasons
		{
			get
			{
				return this.predictedActionReasonsField;
			}
			set
			{
				this.predictedActionReasonsField = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00021A21 File Offset: 0x0001FC21
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00021A29 File Offset: 0x0001FC29
		public bool IsClutter
		{
			get
			{
				return this.isClutterField;
			}
			set
			{
				this.isClutterField = value;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00021A32 File Offset: 0x0001FC32
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00021A3A File Offset: 0x0001FC3A
		[XmlIgnore]
		public bool IsClutterSpecified
		{
			get
			{
				return this.isClutterFieldSpecified;
			}
			set
			{
				this.isClutterFieldSpecified = value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00021A43 File Offset: 0x0001FC43
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00021A4B File Offset: 0x0001FC4B
		public bool BlockStatus
		{
			get
			{
				return this.blockStatusField;
			}
			set
			{
				this.blockStatusField = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00021A54 File Offset: 0x0001FC54
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00021A5C File Offset: 0x0001FC5C
		[XmlIgnore]
		public bool BlockStatusSpecified
		{
			get
			{
				return this.blockStatusFieldSpecified;
			}
			set
			{
				this.blockStatusFieldSpecified = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00021A65 File Offset: 0x0001FC65
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00021A6D File Offset: 0x0001FC6D
		public bool HasBlockedImages
		{
			get
			{
				return this.hasBlockedImagesField;
			}
			set
			{
				this.hasBlockedImagesField = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00021A76 File Offset: 0x0001FC76
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00021A7E File Offset: 0x0001FC7E
		[XmlIgnore]
		public bool HasBlockedImagesSpecified
		{
			get
			{
				return this.hasBlockedImagesFieldSpecified;
			}
			set
			{
				this.hasBlockedImagesFieldSpecified = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00021A87 File Offset: 0x0001FC87
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x00021A8F File Offset: 0x0001FC8F
		public BodyType TextBody
		{
			get
			{
				return this.textBodyField;
			}
			set
			{
				this.textBodyField = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00021A98 File Offset: 0x0001FC98
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00021AA0 File Offset: 0x0001FCA0
		public IconIndexType IconIndex
		{
			get
			{
				return this.iconIndexField;
			}
			set
			{
				this.iconIndexField = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00021AA9 File Offset: 0x0001FCA9
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x00021AB1 File Offset: 0x0001FCB1
		[XmlIgnore]
		public bool IconIndexSpecified
		{
			get
			{
				return this.iconIndexFieldSpecified;
			}
			set
			{
				this.iconIndexFieldSpecified = value;
			}
		}

		// Token: 0x04000898 RID: 2200
		private MimeContentType mimeContentField;

		// Token: 0x04000899 RID: 2201
		private ItemIdType itemIdField;

		// Token: 0x0400089A RID: 2202
		private FolderIdType parentFolderIdField;

		// Token: 0x0400089B RID: 2203
		private string itemClassField;

		// Token: 0x0400089C RID: 2204
		private string subjectField;

		// Token: 0x0400089D RID: 2205
		private SensitivityChoicesType sensitivityField;

		// Token: 0x0400089E RID: 2206
		private bool sensitivityFieldSpecified;

		// Token: 0x0400089F RID: 2207
		private BodyType bodyField;

		// Token: 0x040008A0 RID: 2208
		private AttachmentType[] attachmentsField;

		// Token: 0x040008A1 RID: 2209
		private DateTime dateTimeReceivedField;

		// Token: 0x040008A2 RID: 2210
		private bool dateTimeReceivedFieldSpecified;

		// Token: 0x040008A3 RID: 2211
		private int sizeField;

		// Token: 0x040008A4 RID: 2212
		private bool sizeFieldSpecified;

		// Token: 0x040008A5 RID: 2213
		private string[] categoriesField;

		// Token: 0x040008A6 RID: 2214
		private ImportanceChoicesType importanceField;

		// Token: 0x040008A7 RID: 2215
		private bool importanceFieldSpecified;

		// Token: 0x040008A8 RID: 2216
		private string inReplyToField;

		// Token: 0x040008A9 RID: 2217
		private bool isSubmittedField;

		// Token: 0x040008AA RID: 2218
		private bool isSubmittedFieldSpecified;

		// Token: 0x040008AB RID: 2219
		private bool isDraftField;

		// Token: 0x040008AC RID: 2220
		private bool isDraftFieldSpecified;

		// Token: 0x040008AD RID: 2221
		private bool isFromMeField;

		// Token: 0x040008AE RID: 2222
		private bool isFromMeFieldSpecified;

		// Token: 0x040008AF RID: 2223
		private bool isResendField;

		// Token: 0x040008B0 RID: 2224
		private bool isResendFieldSpecified;

		// Token: 0x040008B1 RID: 2225
		private bool isUnmodifiedField;

		// Token: 0x040008B2 RID: 2226
		private bool isUnmodifiedFieldSpecified;

		// Token: 0x040008B3 RID: 2227
		private InternetHeaderType[] internetMessageHeadersField;

		// Token: 0x040008B4 RID: 2228
		private DateTime dateTimeSentField;

		// Token: 0x040008B5 RID: 2229
		private bool dateTimeSentFieldSpecified;

		// Token: 0x040008B6 RID: 2230
		private DateTime dateTimeCreatedField;

		// Token: 0x040008B7 RID: 2231
		private bool dateTimeCreatedFieldSpecified;

		// Token: 0x040008B8 RID: 2232
		private ResponseObjectType[] responseObjectsField;

		// Token: 0x040008B9 RID: 2233
		private DateTime reminderDueByField;

		// Token: 0x040008BA RID: 2234
		private bool reminderDueByFieldSpecified;

		// Token: 0x040008BB RID: 2235
		private bool reminderIsSetField;

		// Token: 0x040008BC RID: 2236
		private bool reminderIsSetFieldSpecified;

		// Token: 0x040008BD RID: 2237
		private DateTime reminderNextTimeField;

		// Token: 0x040008BE RID: 2238
		private bool reminderNextTimeFieldSpecified;

		// Token: 0x040008BF RID: 2239
		private string reminderMinutesBeforeStartField;

		// Token: 0x040008C0 RID: 2240
		private string displayCcField;

		// Token: 0x040008C1 RID: 2241
		private string displayToField;

		// Token: 0x040008C2 RID: 2242
		private bool hasAttachmentsField;

		// Token: 0x040008C3 RID: 2243
		private bool hasAttachmentsFieldSpecified;

		// Token: 0x040008C4 RID: 2244
		private ExtendedPropertyType[] extendedPropertyField;

		// Token: 0x040008C5 RID: 2245
		private string cultureField;

		// Token: 0x040008C6 RID: 2246
		private EffectiveRightsType effectiveRightsField;

		// Token: 0x040008C7 RID: 2247
		private string lastModifiedNameField;

		// Token: 0x040008C8 RID: 2248
		private DateTime lastModifiedTimeField;

		// Token: 0x040008C9 RID: 2249
		private bool lastModifiedTimeFieldSpecified;

		// Token: 0x040008CA RID: 2250
		private bool isAssociatedField;

		// Token: 0x040008CB RID: 2251
		private bool isAssociatedFieldSpecified;

		// Token: 0x040008CC RID: 2252
		private string webClientReadFormQueryStringField;

		// Token: 0x040008CD RID: 2253
		private string webClientEditFormQueryStringField;

		// Token: 0x040008CE RID: 2254
		private ItemIdType conversationIdField;

		// Token: 0x040008CF RID: 2255
		private BodyType uniqueBodyField;

		// Token: 0x040008D0 RID: 2256
		private FlagType flagField;

		// Token: 0x040008D1 RID: 2257
		private byte[] storeEntryIdField;

		// Token: 0x040008D2 RID: 2258
		private byte[] instanceKeyField;

		// Token: 0x040008D3 RID: 2259
		private BodyType normalizedBodyField;

		// Token: 0x040008D4 RID: 2260
		private EntityExtractionResultType entityExtractionResultField;

		// Token: 0x040008D5 RID: 2261
		private RetentionTagType policyTagField;

		// Token: 0x040008D6 RID: 2262
		private RetentionTagType archiveTagField;

		// Token: 0x040008D7 RID: 2263
		private DateTime retentionDateField;

		// Token: 0x040008D8 RID: 2264
		private bool retentionDateFieldSpecified;

		// Token: 0x040008D9 RID: 2265
		private string previewField;

		// Token: 0x040008DA RID: 2266
		private RightsManagementLicenseDataType rightsManagementLicenseDataField;

		// Token: 0x040008DB RID: 2267
		private PredictedActionReasonType[] predictedActionReasonsField;

		// Token: 0x040008DC RID: 2268
		private bool isClutterField;

		// Token: 0x040008DD RID: 2269
		private bool isClutterFieldSpecified;

		// Token: 0x040008DE RID: 2270
		private bool blockStatusField;

		// Token: 0x040008DF RID: 2271
		private bool blockStatusFieldSpecified;

		// Token: 0x040008E0 RID: 2272
		private bool hasBlockedImagesField;

		// Token: 0x040008E1 RID: 2273
		private bool hasBlockedImagesFieldSpecified;

		// Token: 0x040008E2 RID: 2274
		private BodyType textBodyField;

		// Token: 0x040008E3 RID: 2275
		private IconIndexType iconIndexField;

		// Token: 0x040008E4 RID: 2276
		private bool iconIndexFieldSpecified;
	}
}
