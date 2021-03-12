using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001F7 RID: 503
	[XmlInclude(typeof(DistributionListType))]
	[XmlInclude(typeof(PostItemType))]
	[XmlInclude(typeof(TaskType))]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(CalendarItemType))]
	[XmlInclude(typeof(MessageType))]
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
	[XmlInclude(typeof(ContactItemType))]
	[DesignerCategory("code")]
	[Serializable]
	public class ItemType
	{
		// Token: 0x04000CEA RID: 3306
		public MimeContentType MimeContent;

		// Token: 0x04000CEB RID: 3307
		public ItemIdType ItemId;

		// Token: 0x04000CEC RID: 3308
		public FolderIdType ParentFolderId;

		// Token: 0x04000CED RID: 3309
		public string ItemClass;

		// Token: 0x04000CEE RID: 3310
		public string Subject;

		// Token: 0x04000CEF RID: 3311
		public SensitivityChoicesType Sensitivity;

		// Token: 0x04000CF0 RID: 3312
		[XmlIgnore]
		public bool SensitivitySpecified;

		// Token: 0x04000CF1 RID: 3313
		public BodyType Body;

		// Token: 0x04000CF2 RID: 3314
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), IsNullable = false)]
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), IsNullable = false)]
		public AttachmentType[] Attachments;

		// Token: 0x04000CF3 RID: 3315
		public DateTime DateTimeReceived;

		// Token: 0x04000CF4 RID: 3316
		[XmlIgnore]
		public bool DateTimeReceivedSpecified;

		// Token: 0x04000CF5 RID: 3317
		public int Size;

		// Token: 0x04000CF6 RID: 3318
		[XmlIgnore]
		public bool SizeSpecified;

		// Token: 0x04000CF7 RID: 3319
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories;

		// Token: 0x04000CF8 RID: 3320
		public ImportanceChoicesType Importance;

		// Token: 0x04000CF9 RID: 3321
		[XmlIgnore]
		public bool ImportanceSpecified;

		// Token: 0x04000CFA RID: 3322
		public string InReplyTo;

		// Token: 0x04000CFB RID: 3323
		public bool IsSubmitted;

		// Token: 0x04000CFC RID: 3324
		[XmlIgnore]
		public bool IsSubmittedSpecified;

		// Token: 0x04000CFD RID: 3325
		public bool IsDraft;

		// Token: 0x04000CFE RID: 3326
		[XmlIgnore]
		public bool IsDraftSpecified;

		// Token: 0x04000CFF RID: 3327
		public bool IsFromMe;

		// Token: 0x04000D00 RID: 3328
		[XmlIgnore]
		public bool IsFromMeSpecified;

		// Token: 0x04000D01 RID: 3329
		public bool IsResend;

		// Token: 0x04000D02 RID: 3330
		[XmlIgnore]
		public bool IsResendSpecified;

		// Token: 0x04000D03 RID: 3331
		public bool IsUnmodified;

		// Token: 0x04000D04 RID: 3332
		[XmlIgnore]
		public bool IsUnmodifiedSpecified;

		// Token: 0x04000D05 RID: 3333
		[XmlArrayItem("InternetMessageHeader", IsNullable = false)]
		public InternetHeaderType[] InternetMessageHeaders;

		// Token: 0x04000D06 RID: 3334
		public DateTime DateTimeSent;

		// Token: 0x04000D07 RID: 3335
		[XmlIgnore]
		public bool DateTimeSentSpecified;

		// Token: 0x04000D08 RID: 3336
		public DateTime DateTimeCreated;

		// Token: 0x04000D09 RID: 3337
		[XmlIgnore]
		public bool DateTimeCreatedSpecified;

		// Token: 0x04000D0A RID: 3338
		[XmlArrayItem("AcceptItem", typeof(AcceptItemType), IsNullable = false)]
		[XmlArrayItem("CancelCalendarItem", typeof(CancelCalendarItemType), IsNullable = false)]
		[XmlArrayItem("TentativelyAcceptItem", typeof(TentativelyAcceptItemType), IsNullable = false)]
		[XmlArrayItem("DeclineItem", typeof(DeclineItemType), IsNullable = false)]
		[XmlArrayItem("SuppressReadReceipt", typeof(SuppressReadReceiptType), IsNullable = false)]
		[XmlArrayItem("AcceptSharingInvitation", typeof(AcceptSharingInvitationType), IsNullable = false)]
		[XmlArrayItem("AddItemToMyCalendar", typeof(AddItemToMyCalendarType), IsNullable = false)]
		[XmlArrayItem("PostReplyItem", typeof(PostReplyItemType), IsNullable = false)]
		[XmlArrayItem("ProposeNewTime", typeof(ProposeNewTimeType), IsNullable = false)]
		[XmlArrayItem("ReplyToItem", typeof(ReplyToItemType), IsNullable = false)]
		[XmlArrayItem("ForwardItem", typeof(ForwardItemType), IsNullable = false)]
		[XmlArrayItem("RemoveItem", typeof(RemoveItemType), IsNullable = false)]
		[XmlArrayItem("ReplyAllToItem", typeof(ReplyAllToItemType), IsNullable = false)]
		public ResponseObjectType[] ResponseObjects;

		// Token: 0x04000D0B RID: 3339
		public DateTime ReminderDueBy;

		// Token: 0x04000D0C RID: 3340
		[XmlIgnore]
		public bool ReminderDueBySpecified;

		// Token: 0x04000D0D RID: 3341
		public bool ReminderIsSet;

		// Token: 0x04000D0E RID: 3342
		[XmlIgnore]
		public bool ReminderIsSetSpecified;

		// Token: 0x04000D0F RID: 3343
		public DateTime ReminderNextTime;

		// Token: 0x04000D10 RID: 3344
		[XmlIgnore]
		public bool ReminderNextTimeSpecified;

		// Token: 0x04000D11 RID: 3345
		public string ReminderMinutesBeforeStart;

		// Token: 0x04000D12 RID: 3346
		public string DisplayCc;

		// Token: 0x04000D13 RID: 3347
		public string DisplayTo;

		// Token: 0x04000D14 RID: 3348
		public bool HasAttachments;

		// Token: 0x04000D15 RID: 3349
		[XmlIgnore]
		public bool HasAttachmentsSpecified;

		// Token: 0x04000D16 RID: 3350
		[XmlElement("ExtendedProperty")]
		public ExtendedPropertyType[] ExtendedProperty;

		// Token: 0x04000D17 RID: 3351
		[XmlElement(DataType = "language")]
		public string Culture;

		// Token: 0x04000D18 RID: 3352
		public EffectiveRightsType EffectiveRights;

		// Token: 0x04000D19 RID: 3353
		public string LastModifiedName;

		// Token: 0x04000D1A RID: 3354
		public DateTime LastModifiedTime;

		// Token: 0x04000D1B RID: 3355
		[XmlIgnore]
		public bool LastModifiedTimeSpecified;

		// Token: 0x04000D1C RID: 3356
		public bool IsAssociated;

		// Token: 0x04000D1D RID: 3357
		[XmlIgnore]
		public bool IsAssociatedSpecified;

		// Token: 0x04000D1E RID: 3358
		public string WebClientReadFormQueryString;

		// Token: 0x04000D1F RID: 3359
		public string WebClientEditFormQueryString;

		// Token: 0x04000D20 RID: 3360
		public ItemIdType ConversationId;

		// Token: 0x04000D21 RID: 3361
		public BodyType UniqueBody;

		// Token: 0x04000D22 RID: 3362
		public FlagType Flag;

		// Token: 0x04000D23 RID: 3363
		[XmlElement(DataType = "base64Binary")]
		public byte[] StoreEntryId;

		// Token: 0x04000D24 RID: 3364
		[XmlElement(DataType = "base64Binary")]
		public byte[] InstanceKey;

		// Token: 0x04000D25 RID: 3365
		public BodyType NormalizedBody;

		// Token: 0x04000D26 RID: 3366
		public EntityExtractionResultType EntityExtractionResult;

		// Token: 0x04000D27 RID: 3367
		public RetentionTagType PolicyTag;

		// Token: 0x04000D28 RID: 3368
		public RetentionTagType ArchiveTag;

		// Token: 0x04000D29 RID: 3369
		public DateTime RetentionDate;

		// Token: 0x04000D2A RID: 3370
		[XmlIgnore]
		public bool RetentionDateSpecified;

		// Token: 0x04000D2B RID: 3371
		public string Preview;

		// Token: 0x04000D2C RID: 3372
		public RightsManagementLicenseDataType RightsManagementLicenseData;

		// Token: 0x04000D2D RID: 3373
		[XmlArrayItem("PredictedActionReason", IsNullable = false)]
		public PredictedActionReasonType[] PredictedActionReasons;

		// Token: 0x04000D2E RID: 3374
		public bool IsClutter;

		// Token: 0x04000D2F RID: 3375
		[XmlIgnore]
		public bool IsClutterSpecified;

		// Token: 0x04000D30 RID: 3376
		public bool BlockStatus;

		// Token: 0x04000D31 RID: 3377
		[XmlIgnore]
		public bool BlockStatusSpecified;

		// Token: 0x04000D32 RID: 3378
		public bool HasBlockedImages;

		// Token: 0x04000D33 RID: 3379
		[XmlIgnore]
		public bool HasBlockedImagesSpecified;

		// Token: 0x04000D34 RID: 3380
		public BodyType TextBody;

		// Token: 0x04000D35 RID: 3381
		public IconIndexType IconIndex;

		// Token: 0x04000D36 RID: 3382
		[XmlIgnore]
		public bool IconIndexSpecified;
	}
}
