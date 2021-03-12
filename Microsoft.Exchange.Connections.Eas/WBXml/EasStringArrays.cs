using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x02000075 RID: 117
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EasStringArrays
	{
		// Token: 0x040003C6 RID: 966
		internal static readonly string[] AirNotify = new string[]
		{
			"Notify",
			"Notification",
			"Version",
			"LifeTime",
			"DeviceInfo",
			"Enable",
			"Folder",
			"ServerId",
			"DeviceAddress",
			"ValidCarrierProfiles",
			"CarrierProfile",
			"Status",
			"Responses",
			"Devices",
			"Device",
			"Id",
			"Expiry",
			"NotifyGUID",
			"DeviceFriendlyName"
		};

		// Token: 0x040003C7 RID: 967
		internal static readonly string[] AirNotifyAttributes = new string[]
		{
			"Version11"
		};

		// Token: 0x040003C8 RID: 968
		internal static readonly string[] AirSync = new string[]
		{
			"Sync",
			"Responses",
			"Add",
			"Change",
			"Delete",
			"Fetch",
			"SyncKey",
			"ClientId",
			"ServerId",
			"Status",
			"Collection",
			"Class",
			"Version",
			"CollectionId",
			"GetChanges",
			"MoreAvailable",
			"WindowSize",
			"Commands",
			"Options",
			"FilterType",
			"Truncation",
			"RTFTruncation",
			"Conflict",
			"Collections",
			"ApplicationData",
			"DeletesAsMoves",
			"NotifyGUID",
			"Supported",
			"SoftDelete",
			"MIMESupport",
			"MIMETruncation",
			"Wait",
			"Limit",
			"Partial",
			"ConversationMode",
			"MaxItems",
			"HeartbeatInterval"
		};

		// Token: 0x040003C9 RID: 969
		internal static readonly string[] AirsyncBase = new string[]
		{
			"BodyPreference",
			"Type",
			"TruncationSize",
			"AllOrNone",
			"Restriction",
			"Body",
			"Data",
			"EstimatedDataSize",
			"Truncated",
			"Attachments",
			"Attachment",
			"DisplayName",
			"FileReference",
			"Method",
			"ContentId",
			"ContentLocation",
			"IsInline",
			"NativeBodyType",
			"ContentType",
			"Preview",
			"BodyPartPreference",
			"BodyPart",
			"Status"
		};

		// Token: 0x040003CA RID: 970
		internal static readonly string[] Cal = new string[]
		{
			"TimeZone",
			"AllDayEvent",
			"Attendees",
			"Attendee",
			"Email",
			"Name",
			"Body",
			"BodyTruncated",
			"BusyStatus",
			"Categories",
			"Category",
			"Rtf",
			"DtStamp",
			"EndTime",
			"Exception",
			"Exceptions",
			"Deleted",
			"ExceptionStartTime",
			"Location",
			"MeetingStatus",
			"OrganizerEmail",
			"OrganizerName",
			"Recurrence",
			"Type",
			"Until",
			"Occurrences",
			"Interval",
			"DayOfWeek",
			"DayOfMonth",
			"WeekOfMonth",
			"MonthOfYear",
			"Reminder",
			"Sensitivity",
			"Subject",
			"StartTime",
			"UID",
			"AttendeeStatus",
			"AttendeeType",
			"Attachment",
			"Attachments",
			"AttName",
			"AttSize",
			"AttOid",
			"AttMethod",
			"AttRemoved",
			"DisplayName",
			"DisallowNewTimeProposal",
			"ResponseRequested",
			"AppointmentReplyTime",
			"ResponseType",
			"CalendarType",
			"IsLeapMonth",
			"FirstDayOfWeek",
			"OnlineMeetingConfLink",
			"OnlineMeetingExternalLink"
		};

		// Token: 0x040003CB RID: 971
		internal static readonly string[] Contacts = new string[]
		{
			"Anniversary",
			"AssistantName",
			"AssistantPhoneNumber",
			"Birthday",
			"Body",
			"BodySize",
			"BodyTruncated",
			"Business2PhoneNumber",
			"BusinessAddressCity",
			"BusinessAddressCountry",
			"BusinessAddressPostalCode",
			"BusinessAddressState",
			"BusinessAddressStreet",
			"BusinessFaxNumber",
			"BusinessPhoneNumber",
			"CarPhoneNumber",
			"Categories",
			"Category",
			"Children",
			"Child",
			"CompanyName",
			"Department",
			"Email1Address",
			"Email2Address",
			"Email3Address",
			"FileAs",
			"FirstName",
			"Home2PhoneNumber",
			"HomeAddressCity",
			"HomeAddressCountry",
			"HomeAddressPostalCode",
			"HomeAddressState",
			"HomeAddressStreet",
			"HomeFaxNumber",
			"HomePhoneNumber",
			"JobTitle",
			"LastName",
			"MiddleName",
			"MobilePhoneNumber",
			"OfficeLocation",
			"OtherAddressCity",
			"OtherAddressCountry",
			"OtherAddressPostalCode",
			"OtherAddressState",
			"OtherAddressStreet",
			"PagerNumber",
			"RadioPhoneNumber",
			"Spouse",
			"Suffix",
			"Title",
			"WebPage",
			"YomiCompanyName",
			"YomiFirstName",
			"YomiLastName",
			"CompressedRTF",
			"Picture",
			"Alias",
			"WeightedRank"
		};

		// Token: 0x040003CC RID: 972
		internal static readonly string[] Contacts2 = new string[]
		{
			"CustomerId",
			"GovernmentId",
			"IMAddress",
			"IMAddress2",
			"IMAddress3",
			"ManagerName",
			"CompanyMainPhone",
			"AccountName",
			"NickName",
			"MMS"
		};

		// Token: 0x040003CD RID: 973
		internal static readonly string[] DocumentLibrary = new string[]
		{
			"LinkId",
			"DisplayName",
			"IsFolder",
			"CreationDate",
			"LastModifiedDate",
			"IsHidden",
			"ContentLength",
			"ContentType"
		};

		// Token: 0x040003CE RID: 974
		internal static readonly string[] Email = new string[]
		{
			"Attachment",
			"Attachments",
			"AttName",
			"AttSize",
			"AttOid",
			"AttMethod",
			"AttRemoved",
			"Body",
			"BodySize",
			"BodyTruncated",
			"DateReceived",
			"DisplayName",
			"DisplayTo",
			"Importance",
			"MessageClass",
			"Subject",
			"Read",
			"To",
			"CC",
			"From",
			"ReplyTo",
			"AllDayEvent",
			"Categories",
			"Category",
			"DtStamp",
			"EndTime",
			"InstanceType",
			"IntDBusyStatus",
			"Location",
			"MeetingRequest",
			"Organizer",
			"RecurrenceId",
			"Reminder",
			"ResponseRequested",
			"Recurrences",
			"Recurrence",
			"Type",
			"Until",
			"Occurrences",
			"Interval",
			"DayOfWeek",
			"DayOfMonth",
			"WeekOfMonth",
			"MonthOfYear",
			"StartTime",
			"Sensitivity",
			"TimeZone",
			"GlobalObjId",
			"ThreadTopic",
			"MIMEData",
			"MIMETruncated",
			"MIMESize",
			"InternetCPID",
			"Flag",
			"Status",
			"ContentClass",
			"FlagType",
			"CompleteTime",
			"DisallowNewTimeProposal"
		};

		// Token: 0x040003CF RID: 975
		internal static readonly string[] Email2 = new string[]
		{
			"UmCallerID",
			"UmUserNotes",
			"UmAttDuration",
			"UmAttOrder",
			"ConversationId",
			"ConversationIndex",
			"LastVerbExecuted",
			"LastVerbExecutionTime",
			"ReceivedAsBcc",
			"Sender",
			"CalendarType",
			"IsLeapMonth",
			"AccountId",
			"FirstDayOfWeek",
			"MeetingMessageType",
			"IsDraft",
			"Bcc"
		};

		// Token: 0x040003D0 RID: 976
		internal static readonly string[] FolderHierarchy = new string[]
		{
			"Folders",
			"Folder",
			"DisplayName",
			"ServerId",
			"ParentId",
			"Type",
			"Response",
			"Status",
			"ContentClass",
			"Changes",
			"Add",
			"Delete",
			"Update",
			"SyncKey",
			"FolderCreate",
			"FolderDelete",
			"FolderUpdate",
			"FolderSync",
			"Count",
			"Version",
			"Permissions",
			"Owner"
		};

		// Token: 0x040003D1 RID: 977
		internal static readonly string[] Gal = new string[]
		{
			"DisplayName",
			"Phone",
			"Office",
			"Title",
			"Company",
			"Alias",
			"FirstName",
			"LastName",
			"HomePhone",
			"MobilePhone",
			"EmailAddress",
			"Picture",
			"Status",
			"Data"
		};

		// Token: 0x040003D2 RID: 978
		internal static readonly string[] ItemEstimate = new string[]
		{
			"GetItemEstimate",
			"Version",
			"Collections",
			"Collection",
			"Class",
			"CollectionId",
			"DateTime",
			"Estimate",
			"Response",
			"Status"
		};

		// Token: 0x040003D3 RID: 979
		internal static readonly string[] ItemOperations = new string[]
		{
			"ItemOperations",
			"Fetch",
			"Store",
			"Options",
			"Range",
			"Total",
			"Properties",
			"Data",
			"Status",
			"Response",
			"Version",
			"Schema",
			"Part",
			"EmptyFolderContents",
			"DeleteSubFolders",
			"UserName",
			"Password",
			"Move",
			"DstFldId",
			"ConversationId",
			"MoveAlways"
		};

		// Token: 0x040003D4 RID: 980
		internal static readonly string[] MeetingResponse = new string[]
		{
			"CalId",
			"CollectionId",
			"MeetingResponse",
			"RequestId",
			"Request",
			"Result",
			"Status",
			"UserResponse",
			"Version",
			"InstanceId"
		};

		// Token: 0x040003D5 RID: 981
		internal static readonly string[] Move = new string[]
		{
			"MoveItems",
			"Move",
			"SrcMsgId",
			"SrcFldId",
			"DstFldId",
			"Response",
			"Status",
			"DstMsgId"
		};

		// Token: 0x040003D6 RID: 982
		internal static readonly string[] Notes = new string[]
		{
			"Subject",
			"MessageClass",
			"LastModifiedDate",
			"Categories",
			"Category"
		};

		// Token: 0x040003D7 RID: 983
		internal static readonly string[] Ping = new string[]
		{
			"PingRequest",
			"AutdState",
			"Status",
			"HeartbeatInterval",
			"Folders",
			"Folder",
			"Id",
			"Class",
			"MaxFolders"
		};

		// Token: 0x040003D8 RID: 984
		internal static readonly string[] Provision = new string[]
		{
			"ProvisionRequest",
			"Policies",
			"Policy",
			"PolicyType",
			"PolicyKey",
			"Data",
			"Status",
			"RemoteWipe",
			"eas-provisioningdoc",
			"DevicePasswordEnabled",
			"AlphanumericDevicePasswordRequired",
			"RequireStorageCardEncryption",
			"PasswordRecoveryEnabled",
			"DocumentBrowseEnabled",
			"AttachmentsEnabled",
			"MinDevicePasswordLength",
			"MaxInactivityTimeDeviceLock",
			"MaxDevicePasswordFailedAttempts",
			"MaxAttachmentSize",
			"AllowSimpleDevicePassword",
			"DevicePasswordExpiration",
			"DevicePasswordHistory",
			"AllowStorageCard",
			"AllowCamera",
			"RequireDeviceEncryption",
			"AllowUnsignedApplications",
			"AllowUnsignedInstallationPackages",
			"MinDevicePasswordComplexCharacters",
			"AllowWiFi",
			"AllowTextMessaging",
			"AllowPOPIMAPEmail",
			"AllowBluetooth",
			"AllowIrDA",
			"RequireManualSyncWhenRoaming",
			"AllowDesktopSync",
			"MaxCalendarAgeFilter",
			"AllowHTMLEmail",
			"MaxEmailAgeFilter",
			"MaxEmailBodyTruncationSize",
			"MaxEmailHTMLBodyTruncationSize",
			"RequireSignedSMIMEMessages",
			"RequireEncryptedSMIMEMessages",
			"RequireSignedSMIMEAlgorithm",
			"RequireEncryptionSMIMEAlgorithm",
			"AllowSMIMEEncryptionAlgorithmNegotiation",
			"AllowSMIMESoftCerts",
			"AllowBrowser",
			"AllowConsumerEmail",
			"AllowRemoteDesktop",
			"AllowInternetSharing",
			"UnapprovedInROMApplicationList",
			"ApplicationName",
			"ApprovedApplicationList",
			"Hash"
		};

		// Token: 0x040003D9 RID: 985
		internal static readonly string[] ResolveRecipients = new string[]
		{
			"ResolveRecipientsRequest",
			"Response",
			"Status",
			"Type",
			"Recipient",
			"DisplayName",
			"EmailAddress",
			"Certificates",
			"Certificate",
			"MiniCertificate",
			"Options",
			"To",
			"CertificateRetrieval",
			"RecipientCount",
			"MaxCertificates",
			"MaxAmbiguousRecipients",
			"CertificateCount",
			"Availability",
			"StartTime",
			"EndTime",
			"MergedFreeBusy",
			"Picture",
			"MaxSize",
			"Data",
			"MaxPictures",
			"MaxResolution"
		};

		// Token: 0x040003DA RID: 986
		internal static readonly string[] Search = new string[]
		{
			"SearchRequest",
			"Stores",
			"Store",
			"Name",
			"Query",
			"Options",
			"Range",
			"Status",
			"Response",
			"Result",
			"Properties",
			"Total",
			"EqualTo",
			"Value",
			"And",
			"Or",
			"FreeText",
			"Substring",
			"DeepTraversal",
			"LongId",
			"RebuildResults",
			"LessThan",
			"GreaterThan",
			"Schema",
			"Supported",
			"UserName",
			"Password",
			"ConversationId",
			"Picture",
			"MaxSize",
			"MaxPictures",
			"MaxResolution"
		};

		// Token: 0x040003DB RID: 987
		internal static readonly string[] Settings = new string[]
		{
			"Settings",
			"Status",
			"Get",
			"Set",
			"Oof",
			"OofState",
			"StartTime",
			"EndTime",
			"OofMessage",
			"AppliesToInternal",
			"AppliesToExternalKnown",
			"AppliesToExternalUnknown",
			"Enabled",
			"ReplyMessage",
			"BodyType",
			"DevicePassword",
			"Password",
			"DeviceInformation",
			"Model",
			"IMEI",
			"FriendlyName",
			"OS",
			"OSLanguage",
			"PhoneNumber",
			"UserInformation",
			"EmailAddresses",
			"SmtpAddress",
			"UserAgent",
			"EnableOutboundSMS",
			"MobileOperator",
			"PrimarySmtpAddress",
			"Accounts",
			"Account",
			"AccountId",
			"AccountName",
			"UserDisplayName",
			"SendDisabled",
			"DefaultOutboundAccount",
			"RightsManagementInformation"
		};

		// Token: 0x040003DC RID: 988
		internal static readonly string[] Tasks = new string[]
		{
			"Body",
			"BodySize",
			"BodyTruncated",
			"Categories",
			"Category",
			"Complete",
			"DateCompleted",
			"DueDate",
			"UtcDueDate",
			"Importance",
			"Recurrence",
			"Type",
			"Start",
			"Until",
			"Occurrences",
			"Interval",
			"DayOfMonth",
			"DayOfWeek",
			"WeekOfMonth",
			"MonthOfYear",
			"Regenerate",
			"DeadOccur",
			"ReminderSet",
			"ReminderTime",
			"Sensitivity",
			"StartDate",
			"UtcStartDate",
			"Subject",
			"Rtf",
			"OrdinalDate",
			"SubOrdinalDate",
			"CalendarType",
			"IsLeapMonth",
			"FirstDayOfWeek"
		};

		// Token: 0x040003DD RID: 989
		internal static readonly string[] ValidateCert = new string[]
		{
			"ValidateCertRequest",
			"Certificates",
			"Certificate",
			"CertificateChain",
			"CheckCrl",
			"Status"
		};

		// Token: 0x040003DE RID: 990
		internal static readonly string[] ComposeMail = new string[]
		{
			"SendMail",
			"SmartForward",
			"SmartReply",
			"SaveInSentItems",
			"ReplaceMime",
			"Type",
			"Source",
			"FolderId",
			"ItemId",
			"LongId",
			"InstanceId",
			"Mime",
			"ClientId",
			"Status",
			"AccountId"
		};

		// Token: 0x040003DF RID: 991
		internal static readonly string[] RightsManagement = new string[]
		{
			"RightsManagementSupport",
			"RightsManagementTemplates",
			"RightsManagementTemplate",
			"RightsManagementLicense",
			"EditAllowed",
			"ReplyAllowed",
			"ReplyAllAllowed",
			"ForwardAllowed",
			"ModifyRecipientsAllowed",
			"ExtractAllowed",
			"PrintAllowed",
			"ExportAllowed",
			"ProgrammaticAccessAllowed",
			"Owner",
			"ContentExpiryDate",
			"TemplateID",
			"TemplateName",
			"TemplateDescription",
			"ContentOwner",
			"RemoveRightsManagementProtection"
		};

		// Token: 0x040003E0 RID: 992
		internal static readonly string[] WindowsLive = new string[]
		{
			"Unknown5",
			"Unknown6",
			"Unknown7",
			"Unknown8",
			"SystemCategories",
			"CategoryId",
			"Unsubscribe",
			"CategoryMode",
			"SentTime",
			"SentItem",
			"SimSlotNumber",
			"MmsMessageId"
		};
	}
}
