using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000935 RID: 2357
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ItemSchema : StoreObjectSchema
	{
		// Token: 0x060057D9 RID: 22489 RVA: 0x00169424 File Offset: 0x00167624
		protected ItemSchema()
		{
			base.AddDependencies(new Schema[]
			{
				CoreItemSchema.Instance
			});
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x060057DA RID: 22490 RVA: 0x0016944D File Offset: 0x0016764D
		public new static ItemSchema Instance
		{
			get
			{
				if (ItemSchema.instance == null)
				{
					ItemSchema.instance = new ItemSchema();
				}
				return ItemSchema.instance;
			}
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x00169468 File Offset: 0x00167668
		internal virtual void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			Item.CoreObjectUpdateInternetMessageId(coreItem);
			Item.CoreObjectUpdatePreview(coreItem);
			Item.CoreObjectUpdateSentRepresentingType(coreItem);
			Item.CoreObjectUpdateAnnotationToken(coreItem);
			this.CoreObjectUpdateAllAttachmentsHidden(coreItem);
			if (coreItem != null && ((IValidatable)coreItem).ValidateAllProperties)
			{
				foreach (PropertyRule propertyRule in this.PropertyRules)
				{
					bool arg = propertyRule.WriteEnforce(coreItem.PropertyBag);
					ExTraceGlobals.StorageTracer.Information<string, bool>((long)this.GetHashCode(), "ItemSchema.CoreObjectUpdate. PropertyRule enfoced. Rule = {0}. Result = {1}", propertyRule.ToString(), arg);
				}
			}
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x00169504 File Offset: 0x00167704
		internal virtual void CoreObjectUpdateComplete(CoreItem coreItem, SaveResult saveResult)
		{
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x00169506 File Offset: 0x00167706
		protected virtual void CoreObjectUpdateAllAttachmentsHidden(CoreItem coreItem)
		{
			Item.CoreObjectUpdateAllAttachmentsHidden(coreItem);
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x060057DE RID: 22494 RVA: 0x0016950E File Offset: 0x0016770E
		protected virtual ICollection<PropertyRule> PropertyRules
		{
			get
			{
				return ItemSchema.ItemPropertyRules;
			}
		}

		// Token: 0x04002F0C RID: 12044
		[Autoload]
		public static readonly StorePropertyDefinition Id = CoreItemSchema.Id;

		// Token: 0x04002F0D RID: 12045
		public static readonly StorePropertyDefinition DocumentId = InternalSchema.DocumentId;

		// Token: 0x04002F0E RID: 12046
		public static readonly StorePropertyDefinition ConversationDocumentId = InternalSchema.ConversationDocumentId;

		// Token: 0x04002F0F RID: 12047
		[Autoload]
		public static readonly StorePropertyDefinition LastModifiedBy = InternalSchema.LastModifierName;

		// Token: 0x04002F10 RID: 12048
		[Autoload]
		public static readonly StorePropertyDefinition IsReplyRequested = InternalSchema.IsReplyRequested;

		// Token: 0x04002F11 RID: 12049
		[Autoload]
		public static readonly StorePropertyDefinition IsResponseRequested = InternalSchema.IsResponseRequested;

		// Token: 0x04002F12 RID: 12050
		[Autoload]
		public static readonly StorePropertyDefinition Categories = InternalSchema.Categories;

		// Token: 0x04002F13 RID: 12051
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition HasAttachment = InternalSchema.HasAttachment;

		// Token: 0x04002F14 RID: 12052
		[Autoload]
		public static readonly StorePropertyDefinition Importance = InternalSchema.Importance;

		// Token: 0x04002F15 RID: 12053
		[Autoload]
		public static readonly StorePropertyDefinition Privacy = InternalSchema.Privacy;

		// Token: 0x04002F16 RID: 12054
		[Autoload]
		public static readonly StorePropertyDefinition NormalizedSubject = CoreItemSchema.NormalizedSubject;

		// Token: 0x04002F17 RID: 12055
		[Autoload]
		public static readonly StorePropertyDefinition Subject = CoreItemSchema.Subject;

		// Token: 0x04002F18 RID: 12056
		[Autoload]
		public static readonly StorePropertyDefinition SubjectPrefix = CoreItemSchema.SubjectPrefix;

		// Token: 0x04002F19 RID: 12057
		[Autoload]
		public static readonly StorePropertyDefinition Sensitivity = InternalSchema.Sensitivity;

		// Token: 0x04002F1A RID: 12058
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Sender = InternalSchema.Sender;

		// Token: 0x04002F1B RID: 12059
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition From = InternalSchema.From;

		// Token: 0x04002F1C RID: 12060
		[Autoload]
		public static readonly StorePropertyDefinition Preview = InternalSchema.Preview;

		// Token: 0x04002F1D RID: 12061
		[Autoload]
		public static readonly StorePropertyDefinition OriginalSensitivity = InternalSchema.OriginalSensitivity;

		// Token: 0x04002F1E RID: 12062
		[Autoload]
		public static readonly StorePropertyDefinition InReplyTo = InternalSchema.InReplyTo;

		// Token: 0x04002F1F RID: 12063
		[Autoload]
		public static readonly StorePropertyDefinition ReceivedTime = CoreItemSchema.ReceivedTime;

		// Token: 0x04002F20 RID: 12064
		public static readonly StorePropertyDefinition XSimSlotNumber = InternalSchema.XSimSlotNumber;

		// Token: 0x04002F21 RID: 12065
		public static readonly StorePropertyDefinition XSentItem = InternalSchema.XSentItem;

		// Token: 0x04002F22 RID: 12066
		public static readonly StorePropertyDefinition XSentTime = InternalSchema.XSentTime;

		// Token: 0x04002F23 RID: 12067
		public static readonly StorePropertyDefinition XMmsMessageId = InternalSchema.XMmsMessageId;

		// Token: 0x04002F24 RID: 12068
		public static readonly StorePropertyDefinition RenewTime = CoreItemSchema.RenewTime;

		// Token: 0x04002F25 RID: 12069
		public static readonly StorePropertyDefinition ReceivedOrRenewTime = CoreItemSchema.ReceivedOrRenewTime;

		// Token: 0x04002F26 RID: 12070
		public static readonly StorePropertyDefinition RichContent = CoreItemSchema.RichContent;

		// Token: 0x04002F27 RID: 12071
		public static readonly StorePropertyDefinition MailboxGuid = CoreItemSchema.MailboxGuid;

		// Token: 0x04002F28 RID: 12072
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition SentTime = InternalSchema.SentTime;

		// Token: 0x04002F29 RID: 12073
		[Autoload]
		internal static readonly StorePropertyDefinition BodyContentBase = InternalSchema.BodyContentBase;

		// Token: 0x04002F2A RID: 12074
		[Autoload]
		internal static readonly StorePropertyDefinition BodyContentLocation = InternalSchema.BodyContentLocation;

		// Token: 0x04002F2B RID: 12075
		[Autoload]
		public static readonly StorePropertyDefinition Codepage = CoreItemSchema.Codepage;

		// Token: 0x04002F2C RID: 12076
		[Autoload]
		public static readonly StorePropertyDefinition ConversationIndex = InternalSchema.ConversationIndex;

		// Token: 0x04002F2D RID: 12077
		[Autoload]
		public static readonly StorePropertyDefinition ConversationTopic = InternalSchema.ConversationTopic;

		// Token: 0x04002F2E RID: 12078
		[Autoload]
		public static readonly StorePropertyDefinition ConversationTopicHash = InternalSchema.ConversationTopicHash;

		// Token: 0x04002F2F RID: 12079
		[DetectCodepage]
		public static readonly StorePropertyDefinition SentRepresentingDisplayName = InternalSchema.SentRepresentingDisplayName;

		// Token: 0x04002F30 RID: 12080
		[Autoload]
		public static readonly StorePropertyDefinition SentRepresentingEmailAddress = InternalSchema.SentRepresentingEmailAddress;

		// Token: 0x04002F31 RID: 12081
		[Autoload]
		internal static readonly StorePropertyDefinition SentRepresentingEntryId = InternalSchema.SentRepresentingEntryId;

		// Token: 0x04002F32 RID: 12082
		[Autoload]
		internal static readonly StorePropertyDefinition SentRepresentingSearchKey = InternalSchema.SentRepresentingSearchKey;

		// Token: 0x04002F33 RID: 12083
		[Autoload]
		public static readonly StorePropertyDefinition SentRepresentingSmtpAddress = InternalSchema.SentRepresentingSmtpAddress;

		// Token: 0x04002F34 RID: 12084
		[Autoload]
		public static readonly StorePropertyDefinition SentRepresentingType = InternalSchema.SentRepresentingType;

		// Token: 0x04002F35 RID: 12085
		[Autoload]
		public static readonly StorePropertyDefinition InternetMessageId = InternalSchema.InternetMessageId;

		// Token: 0x04002F36 RID: 12086
		[Autoload]
		public static readonly StorePropertyDefinition InternetMessageIdHash = InternalSchema.InternetMessageIdHash;

		// Token: 0x04002F37 RID: 12087
		[Autoload]
		public static readonly StorePropertyDefinition InternetReferences = InternalSchema.InternetReferences;

		// Token: 0x04002F38 RID: 12088
		[Autoload]
		internal static readonly StorePropertyDefinition InternetCpid = InternalSchema.InternetCpid;

		// Token: 0x04002F39 RID: 12089
		[Autoload]
		public static readonly StorePropertyDefinition Size = CoreItemSchema.Size;

		// Token: 0x04002F3A RID: 12090
		[Autoload]
		public static readonly StorePropertyDefinition SendInternetEncoding = InternalSchema.SendInternetEncoding;

		// Token: 0x04002F3B RID: 12091
		[Autoload]
		public static readonly StorePropertyDefinition QuotaProhibitReceive = InternalSchema.ProhibitReceiveQuota;

		// Token: 0x04002F3C RID: 12092
		[Autoload]
		public static readonly StorePropertyDefinition QuotaProhibitSend = InternalSchema.ProhibitSendQuota;

		// Token: 0x04002F3D RID: 12093
		[Autoload]
		public static readonly StorePropertyDefinition SubmittedByAdmin = InternalSchema.SubmittedByAdmin;

		// Token: 0x04002F3E RID: 12094
		[Autoload]
		public static readonly StorePropertyDefinition SvrGeneratingQuotaMsg = InternalSchema.SvrGeneratingQuotaMsg;

		// Token: 0x04002F3F RID: 12095
		[Autoload]
		public static readonly StorePropertyDefinition PrimaryMbxOverQuota = InternalSchema.PrimaryMbxOverQuota;

		// Token: 0x04002F40 RID: 12096
		[Autoload]
		public static readonly StorePropertyDefinition IsPublicFolderQuotaMessage = InternalSchema.IsPublicFolderQuotaMessage;

		// Token: 0x04002F41 RID: 12097
		[Autoload]
		public static readonly StorePropertyDefinition QuotaType = InternalSchema.QuotaType;

		// Token: 0x04002F42 RID: 12098
		[Autoload]
		public static readonly StorePropertyDefinition FavLevelMask = CoreItemSchema.FavLevelMask;

		// Token: 0x04002F43 RID: 12099
		[Autoload]
		public static readonly StorePropertyDefinition StorageQuotaLimit = InternalSchema.StorageQuotaLimit;

		// Token: 0x04002F44 RID: 12100
		[Autoload]
		public static readonly StorePropertyDefinition ExcessStorageUsed = InternalSchema.ExcessStorageUsed;

		// Token: 0x04002F45 RID: 12101
		[Autoload]
		public static readonly StorePropertyDefinition SendRichInfo = InternalSchema.SendRichInfo;

		// Token: 0x04002F46 RID: 12102
		[Autoload]
		public static readonly StorePropertyDefinition Responsibility = InternalSchema.Responsibility;

		// Token: 0x04002F47 RID: 12103
		[Autoload]
		public static readonly StorePropertyDefinition RecipientType = InternalSchema.RecipientType;

		// Token: 0x04002F48 RID: 12104
		[Autoload]
		public static readonly StorePropertyDefinition SpamConfidenceLevel = InternalSchema.SpamConfidenceLevel;

		// Token: 0x04002F49 RID: 12105
		[Autoload]
		public static readonly StorePropertyDefinition ReminderDueBy = InternalSchema.ReminderDueBy;

		// Token: 0x04002F4A RID: 12106
		internal static readonly StorePropertyDefinition ReminderDueByInternal = InternalSchema.ReminderDueByInternal;

		// Token: 0x04002F4B RID: 12107
		[Autoload]
		public static readonly StorePropertyDefinition ReminderIsSet = InternalSchema.ReminderIsSet;

		// Token: 0x04002F4C RID: 12108
		public static readonly StorePropertyDefinition ReminderIsSetInternal = InternalSchema.ReminderIsSetInternal;

		// Token: 0x04002F4D RID: 12109
		[Autoload]
		public static readonly StorePropertyDefinition ReminderNextTime = InternalSchema.ReminderNextTime;

		// Token: 0x04002F4E RID: 12110
		[Autoload]
		public static readonly StorePropertyDefinition ReminderMinutesBeforeStart = InternalSchema.ReminderMinutesBeforeStart;

		// Token: 0x04002F4F RID: 12111
		[Autoload]
		public static readonly StorePropertyDefinition VoiceReminderPhoneNumber = InternalSchema.VoiceReminderPhoneNumber;

		// Token: 0x04002F50 RID: 12112
		[Autoload]
		public static readonly StorePropertyDefinition IsVoiceReminderEnabled = InternalSchema.IsVoiceReminderEnabled;

		// Token: 0x04002F51 RID: 12113
		[Autoload]
		public static readonly StorePropertyDefinition LocalDueDate = InternalSchema.LocalDueDate;

		// Token: 0x04002F52 RID: 12114
		[Autoload]
		public static readonly StorePropertyDefinition LocalStartDate = InternalSchema.LocalStartDate;

		// Token: 0x04002F53 RID: 12115
		[Autoload]
		public static readonly StorePropertyDefinition UtcStartDate = InternalSchema.UtcStartDate;

		// Token: 0x04002F54 RID: 12116
		[Autoload]
		public static readonly StorePropertyDefinition UtcDueDate = InternalSchema.UtcDueDate;

		// Token: 0x04002F55 RID: 12117
		[Autoload]
		public static readonly StorePropertyDefinition TaskStatus = InternalSchema.TaskStatus;

		// Token: 0x04002F56 RID: 12118
		public static readonly StorePropertyDefinition ReminderMinutesBeforeStartInternal = InternalSchema.ReminderMinutesBeforeStartInternal;

		// Token: 0x04002F57 RID: 12119
		[Autoload]
		internal static readonly StorePropertyDefinition SentMailEntryId = InternalSchema.SentMailEntryId;

		// Token: 0x04002F58 RID: 12120
		[Autoload]
		internal static readonly StorePropertyDefinition DeleteAfterSubmit = InternalSchema.DeleteAfterSubmit;

		// Token: 0x04002F59 RID: 12121
		[Autoload]
		public static readonly StorePropertyDefinition TimeZoneDefinitionStart = InternalSchema.TimeZoneDefinitionStart;

		// Token: 0x04002F5A RID: 12122
		[OptionalAutoload]
		[LegalTracking]
		internal static readonly StorePropertyDefinition HtmlBody = InternalSchema.HtmlBody;

		// Token: 0x04002F5B RID: 12123
		[LegalTracking]
		[OptionalAutoload]
		internal static readonly StorePropertyDefinition RtfBody = InternalSchema.RtfBody;

		// Token: 0x04002F5C RID: 12124
		[Autoload]
		internal static readonly StorePropertyDefinition RtfInSync = InternalSchema.RtfInSync;

		// Token: 0x04002F5D RID: 12125
		[LegalTracking]
		[OptionalAutoload]
		public static readonly StorePropertyDefinition TextBody = InternalSchema.TextBody;

		// Token: 0x04002F5E RID: 12126
		public static readonly StorePropertyDefinition BodyTag = InternalSchema.BodyTag;

		// Token: 0x04002F5F RID: 12127
		[Autoload]
		public static readonly StorePropertyDefinition NativeBodyInfo = CoreItemSchema.NativeBodyInfo;

		// Token: 0x04002F60 RID: 12128
		[Autoload]
		public static readonly StorePropertyDefinition FlagRequest = InternalSchema.FlagRequest;

		// Token: 0x04002F61 RID: 12129
		public static readonly StorePropertyDefinition RequestedAction = InternalSchema.RequestedAction;

		// Token: 0x04002F62 RID: 12130
		[Autoload]
		public static readonly StorePropertyDefinition IconIndex = InternalSchema.IconIndex;

		// Token: 0x04002F63 RID: 12131
		[Autoload]
		public static readonly StorePropertyDefinition PercentComplete = InternalSchema.PercentComplete;

		// Token: 0x04002F64 RID: 12132
		[Autoload]
		public static readonly StorePropertyDefinition IsToDoItem = InternalSchema.IsToDoItem;

		// Token: 0x04002F65 RID: 12133
		[Autoload]
		public static readonly StorePropertyDefinition ConversationId = InternalSchema.ConversationId;

		// Token: 0x04002F66 RID: 12134
		[Autoload]
		public static readonly StorePropertyDefinition ConversationIdHash = InternalSchema.ConversationIdHash;

		// Token: 0x04002F67 RID: 12135
		[Autoload]
		public static readonly StorePropertyDefinition ConversationIndexTracking = InternalSchema.ConversationIndexTracking;

		// Token: 0x04002F68 RID: 12136
		public static readonly StorePropertyDefinition ConversationIndexTrackingEx = InternalSchema.ConversationIndexTrackingEx;

		// Token: 0x04002F69 RID: 12137
		[Autoload]
		public static readonly StorePropertyDefinition IsFlagSetForRecipient = InternalSchema.IsFlagSetForRecipient;

		// Token: 0x04002F6A RID: 12138
		[Autoload]
		public static readonly StorePropertyDefinition PropertyExistenceTracker = InternalSchema.PropertyExistenceTracker;

		// Token: 0x04002F6B RID: 12139
		[LegalTracking]
		public static readonly StorePropertyDefinition AttachmentContent = InternalSchema.AttachmentContent;

		// Token: 0x04002F6C RID: 12140
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition DisplayTo = InternalSchema.DisplayTo;

		// Token: 0x04002F6D RID: 12141
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition DisplayCc = InternalSchema.DisplayCc;

		// Token: 0x04002F6E RID: 12142
		[LegalTracking]
		public static readonly StorePropertyDefinition DisplayBcc = InternalSchema.DisplayBcc;

		// Token: 0x04002F6F RID: 12143
		public static readonly StorePropertyDefinition ParentDisplayName = InternalSchema.ParentDisplayName;

		// Token: 0x04002F70 RID: 12144
		public static readonly StorePropertyDefinition ArticleId = InternalSchema.ArticleId;

		// Token: 0x04002F71 RID: 12145
		public static readonly StorePropertyDefinition ImapId = InternalSchema.ImapId;

		// Token: 0x04002F72 RID: 12146
		public static readonly StorePropertyDefinition OriginalSourceServerVersion = InternalSchema.OriginalSourceServerVersion;

		// Token: 0x04002F73 RID: 12147
		public static readonly StorePropertyDefinition ImapInternalDate = InternalSchema.ImapInternalDate;

		// Token: 0x04002F74 RID: 12148
		public static readonly StorePropertyDefinition IsUnmodified = InternalSchema.IsUnmodified;

		// Token: 0x04002F75 RID: 12149
		public static readonly StorePropertyDefinition Not822Renderable = InternalSchema.Not822Renderable;

		// Token: 0x04002F76 RID: 12150
		public static readonly StorePropertyDefinition ElcAutoCopyTag = InternalSchema.ElcAutoCopyTag;

		// Token: 0x04002F77 RID: 12151
		public static readonly StorePropertyDefinition ElcMoveDate = InternalSchema.ElcMoveDate;

		// Token: 0x04002F78 RID: 12152
		public static readonly StorePropertyDefinition EHAMigrationExpiryDate = InternalSchema.EHAMigrationExpirationDate;

		// Token: 0x04002F79 RID: 12153
		public static readonly StorePropertyDefinition RetentionDate = InternalSchema.RetentionDate;

		// Token: 0x04002F7A RID: 12154
		public static readonly StorePropertyDefinition ArchiveDate = InternalSchema.ArchiveDate;

		// Token: 0x04002F7B RID: 12155
		public static readonly StorePropertyDefinition StartDateEtc = InternalSchema.StartDateEtc;

		// Token: 0x04002F7C RID: 12156
		internal static readonly StorePropertyDefinition RowType = InternalSchema.RowType;

		// Token: 0x04002F7D RID: 12157
		internal static readonly StorePropertyDefinition SyncCustomState = InternalSchema.SyncCustomState;

		// Token: 0x04002F7E RID: 12158
		[Autoload]
		public static readonly StorePropertyDefinition ItemColor = InternalSchema.ItemColor;

		// Token: 0x04002F7F RID: 12159
		public static readonly StorePropertyDefinition FlagStatus = InternalSchema.FlagStatus;

		// Token: 0x04002F80 RID: 12160
		public static readonly StorePropertyDefinition FlagCompleteTime = InternalSchema.FlagCompleteTime;

		// Token: 0x04002F81 RID: 12161
		public static readonly StorePropertyDefinition IsClassified = InternalSchema.IsClassified;

		// Token: 0x04002F82 RID: 12162
		public static readonly StorePropertyDefinition Classification = InternalSchema.Classification;

		// Token: 0x04002F83 RID: 12163
		public static readonly StorePropertyDefinition ClassificationDescription = InternalSchema.ClassificationDescription;

		// Token: 0x04002F84 RID: 12164
		public static readonly StorePropertyDefinition ClassificationGuid = InternalSchema.ClassificationGuid;

		// Token: 0x04002F85 RID: 12165
		public static readonly StorePropertyDefinition ClassificationKeep = InternalSchema.ClassificationKeep;

		// Token: 0x04002F86 RID: 12166
		public static readonly StorePropertyDefinition QuarantineOriginalSender = InternalSchema.QuarantineOriginalSender;

		// Token: 0x04002F87 RID: 12167
		public static readonly StorePropertyDefinition JournalingRemoteAccounts = InternalSchema.JournalingRemoteAccounts;

		// Token: 0x04002F88 RID: 12168
		public static readonly StorePropertyDefinition PurportedSenderDomain = InternalSchema.PurportedSenderDomain;

		// Token: 0x04002F89 RID: 12169
		public static readonly StorePropertyDefinition BlockStatus = InternalSchema.BlockStatus;

		// Token: 0x04002F8A RID: 12170
		public static readonly StorePropertyDefinition ReplyTemplateId = InternalSchema.ReplyTemplateId;

		// Token: 0x04002F8B RID: 12171
		public static readonly StorePropertyDefinition RuleTriggerHistory = InternalSchema.RuleTriggerHistory;

		// Token: 0x04002F8C RID: 12172
		public static readonly StorePropertyDefinition DelegatedByRule = InternalSchema.DelegatedByRule;

		// Token: 0x04002F8D RID: 12173
		public static readonly StorePropertyDefinition OriginalMessageEntryId = InternalSchema.OriginalMessageEntryId;

		// Token: 0x04002F8E RID: 12174
		public static readonly StorePropertyDefinition OriginalMessageSvrEId = InternalSchema.OriginalMessageSvrEId;

		// Token: 0x04002F8F RID: 12175
		public static readonly StorePropertyDefinition DeferredActionMessageBackPatched = InternalSchema.DeferredActionMessageBackPatched;

		// Token: 0x04002F90 RID: 12176
		public static readonly StorePropertyDefinition HasDeferredActionMessage = InternalSchema.HasDeferredActionMessage;

		// Token: 0x04002F91 RID: 12177
		[Autoload]
		public static readonly StorePropertyDefinition MessageStatus = CoreItemSchema.MessageStatus;

		// Token: 0x04002F92 RID: 12178
		public static readonly StorePropertyDefinition MoveToFolderEntryId = InternalSchema.MoveToFolderEntryId;

		// Token: 0x04002F93 RID: 12179
		public static readonly StorePropertyDefinition MoveToStoreEntryId = InternalSchema.MoveToStoreEntryId;

		// Token: 0x04002F94 RID: 12180
		public static readonly StorePropertyDefinition IsAutoForwarded = InternalSchema.IsAutoForwarded;

		// Token: 0x04002F95 RID: 12181
		public static readonly StorePropertyDefinition FlagSubject = InternalSchema.FlagSubject;

		// Token: 0x04002F96 RID: 12182
		public static readonly StorePropertyDefinition SearchFullText = InternalSchema.SearchFullText;

		// Token: 0x04002F97 RID: 12183
		public static readonly StorePropertyDefinition SearchSender = InternalSchema.SearchSender;

		// Token: 0x04002F98 RID: 12184
		public static readonly StorePropertyDefinition SearchRecipients = InternalSchema.SearchRecipients;

		// Token: 0x04002F99 RID: 12185
		public static readonly StorePropertyDefinition SearchRecipientsTo = InternalSchema.SearchRecipientsTo;

		// Token: 0x04002F9A RID: 12186
		public static readonly StorePropertyDefinition SearchRecipientsCc = InternalSchema.SearchRecipientsCc;

		// Token: 0x04002F9B RID: 12187
		public static readonly StorePropertyDefinition SearchRecipientsBcc = InternalSchema.SearchRecipientsBcc;

		// Token: 0x04002F9C RID: 12188
		public static readonly StorePropertyDefinition SearchAllIndexedProps = InternalSchema.SearchAllIndexedProps;

		// Token: 0x04002F9D RID: 12189
		public static readonly StorePropertyDefinition SearchIsPartiallyIndexed = InternalSchema.SearchIsPartiallyIndexed;

		// Token: 0x04002F9E RID: 12190
		public static readonly StorePropertyDefinition SearchFullTextSubject = InternalSchema.SearchFullTextSubject;

		// Token: 0x04002F9F RID: 12191
		public static readonly StorePropertyDefinition SearchFullTextBody = InternalSchema.SearchFullTextBody;

		// Token: 0x04002FA0 RID: 12192
		[Autoload]
		public static readonly StorePropertyDefinition CompleteDate = InternalSchema.CompleteDate;

		// Token: 0x04002FA1 RID: 12193
		[Autoload]
		public static readonly StorePropertyDefinition EdgePcl = InternalSchema.ContentFilterPcl;

		// Token: 0x04002FA2 RID: 12194
		[Autoload]
		public static readonly StorePropertyDefinition LinkEnabled = InternalSchema.LinkEnabled;

		// Token: 0x04002FA3 RID: 12195
		[Autoload]
		public static readonly StorePropertyDefinition IsComplete = InternalSchema.IsComplete;

		// Token: 0x04002FA4 RID: 12196
		public static readonly StorePropertyDefinition PopImapPoisonMessageStamp = InternalSchema.PopImapPoisonMessageStamp;

		// Token: 0x04002FA5 RID: 12197
		public static readonly StorePropertyDefinition PopMIMESize = InternalSchema.PopMIMESize;

		// Token: 0x04002FA6 RID: 12198
		public static readonly StorePropertyDefinition PopMIMEOptions = InternalSchema.PopMIMEOptions;

		// Token: 0x04002FA7 RID: 12199
		public static readonly StorePropertyDefinition ImapMIMESize = InternalSchema.ImapMIMESize;

		// Token: 0x04002FA8 RID: 12200
		public static readonly StorePropertyDefinition ImapMIMEOptions = InternalSchema.ImapMIMEOptions;

		// Token: 0x04002FA9 RID: 12201
		public static readonly StorePropertyDefinition ImapAppendStamp = InternalSchema.XMsExchImapAppendStamp;

		// Token: 0x04002FAA RID: 12202
		public static readonly StorePropertyDefinition ProtocolLog = InternalSchema.ProtocolLog;

		// Token: 0x04002FAB RID: 12203
		[Autoload]
		public static readonly StorePropertyDefinition CloudId = InternalSchema.CloudId;

		// Token: 0x04002FAC RID: 12204
		public static readonly StorePropertyDefinition CloudVersion = InternalSchema.CloudVersion;

		// Token: 0x04002FAD RID: 12205
		public static readonly StorePropertyDefinition InstanceKey = InternalSchema.InstanceKey;

		// Token: 0x04002FAE RID: 12206
		public static readonly StorePropertyDefinition ResentFrom = InternalSchema.ResentFrom;

		// Token: 0x04002FAF RID: 12207
		public static readonly StorePropertyDefinition PredictedActions = InternalSchema.PredictedActions;

		// Token: 0x04002FB0 RID: 12208
		public static readonly StorePropertyDefinition InferencePredictedReplyForwardReasons = InternalSchema.InferencePredictedReplyForwardReasons;

		// Token: 0x04002FB1 RID: 12209
		public static readonly StorePropertyDefinition InferencePredictedDeleteReasons = InternalSchema.InferencePredictedDeleteReasons;

		// Token: 0x04002FB2 RID: 12210
		public static readonly StorePropertyDefinition InferencePredictedIgnoreReasons = InternalSchema.InferencePredictedIgnoreReasons;

		// Token: 0x04002FB3 RID: 12211
		[Autoload]
		public static readonly StorePropertyDefinition IsClutter = InternalSchema.IsClutter;

		// Token: 0x04002FB4 RID: 12212
		public static readonly StorePropertyDefinition OriginalDeliveryFolderInfo = InternalSchema.OriginalDeliveryFolderInfo;

		// Token: 0x04002FB5 RID: 12213
		public static readonly StorePropertyDefinition ExtractedMeetings = InternalSchema.ExtractedMeetings;

		// Token: 0x04002FB6 RID: 12214
		public static readonly StorePropertyDefinition ExtractedTasks = InternalSchema.ExtractedTasks;

		// Token: 0x04002FB7 RID: 12215
		public static readonly StorePropertyDefinition ExtractedAddresses = InternalSchema.ExtractedAddresses;

		// Token: 0x04002FB8 RID: 12216
		public static readonly StorePropertyDefinition ExtractedKeywords = InternalSchema.ExtractedKeywords;

		// Token: 0x04002FB9 RID: 12217
		public static readonly StorePropertyDefinition ExtractedPhones = InternalSchema.ExtractedPhones;

		// Token: 0x04002FBA RID: 12218
		public static readonly StorePropertyDefinition ExtractedEmails = InternalSchema.ExtractedEmails;

		// Token: 0x04002FBB RID: 12219
		public static readonly StorePropertyDefinition ExtractedUrls = InternalSchema.ExtractedUrls;

		// Token: 0x04002FBC RID: 12220
		public static readonly StorePropertyDefinition ExtractedContacts = InternalSchema.ExtractedContacts;

		// Token: 0x04002FBD RID: 12221
		public static readonly StorePropertyDefinition ExtractedMeetingsExists = InternalSchema.ExtractedMeetingsExists;

		// Token: 0x04002FBE RID: 12222
		public static readonly StorePropertyDefinition ExtractedTasksExists = InternalSchema.ExtractedTasksExists;

		// Token: 0x04002FBF RID: 12223
		public static readonly StorePropertyDefinition ExtractedAddressesExists = InternalSchema.ExtractedAddressesExists;

		// Token: 0x04002FC0 RID: 12224
		public static readonly StorePropertyDefinition ExtractedKeywordsExists = InternalSchema.ExtractedKeywordsExists;

		// Token: 0x04002FC1 RID: 12225
		public static readonly StorePropertyDefinition ExtractedUrlsExists = InternalSchema.ExtractedUrlsExists;

		// Token: 0x04002FC2 RID: 12226
		public static readonly StorePropertyDefinition ExtractedPhonesExists = InternalSchema.ExtractedPhonesExists;

		// Token: 0x04002FC3 RID: 12227
		public static readonly StorePropertyDefinition ExtractedEmailsExists = InternalSchema.ExtractedEmailsExists;

		// Token: 0x04002FC4 RID: 12228
		public static readonly StorePropertyDefinition ExtractedContactsExists = InternalSchema.ExtractedContactsExists;

		// Token: 0x04002FC5 RID: 12229
		public static readonly StorePropertyDefinition XmlExtractedMeetings = InternalSchema.XmlExtractedMeetings;

		// Token: 0x04002FC6 RID: 12230
		public static readonly StorePropertyDefinition XmlExtractedTasks = InternalSchema.XmlExtractedTasks;

		// Token: 0x04002FC7 RID: 12231
		public static readonly StorePropertyDefinition XmlExtractedAddresses = InternalSchema.XmlExtractedAddresses;

		// Token: 0x04002FC8 RID: 12232
		public static readonly StorePropertyDefinition XmlExtractedKeywords = InternalSchema.XmlExtractedKeywords;

		// Token: 0x04002FC9 RID: 12233
		public static readonly StorePropertyDefinition XmlExtractedPhones = InternalSchema.XmlExtractedPhones;

		// Token: 0x04002FCA RID: 12234
		public static readonly StorePropertyDefinition XmlExtractedEmails = InternalSchema.XmlExtractedEmails;

		// Token: 0x04002FCB RID: 12235
		public static readonly StorePropertyDefinition XmlExtractedUrls = InternalSchema.XmlExtractedUrls;

		// Token: 0x04002FCC RID: 12236
		public static readonly StorePropertyDefinition XmlExtractedContacts = InternalSchema.XmlExtractedContacts;

		// Token: 0x04002FCD RID: 12237
		public static readonly StorePropertyDefinition AnnotationToken = InternalSchema.AnnotationToken;

		// Token: 0x04002FCE RID: 12238
		public static readonly StorePropertyDefinition DetectedLanguage = InternalSchema.DetectedLanguage;

		// Token: 0x04002FCF RID: 12239
		public static readonly StorePropertyDefinition IsPartiallyIndexed = InternalSchema.IsPartiallyIndexed;

		// Token: 0x04002FD0 RID: 12240
		public static readonly StorePropertyDefinition LastIndexingAttemptTime = InternalSchema.LastIndexingAttemptTime;

		// Token: 0x04002FD1 RID: 12241
		public static readonly StorePropertyDefinition IndexingErrorCode = InternalSchema.IndexingErrorCode;

		// Token: 0x04002FD2 RID: 12242
		[Autoload]
		public static readonly StorePropertyDefinition Fid = InternalSchema.Fid;

		// Token: 0x04002FD3 RID: 12243
		[Autoload]
		public static readonly StorePropertyDefinition DlpSenderOverride = InternalSchema.DlpSenderOverride;

		// Token: 0x04002FD4 RID: 12244
		[Autoload]
		public static readonly StorePropertyDefinition DlpFalsePositive = InternalSchema.DlpFalsePositive;

		// Token: 0x04002FD5 RID: 12245
		[Autoload]
		public static readonly StorePropertyDefinition DlpDetectedClassifications = InternalSchema.DlpDetectedClassifications;

		// Token: 0x04002FD6 RID: 12246
		[Autoload]
		public static readonly StorePropertyDefinition DlpDetectedClassificationObjects = InternalSchema.DlpDetectedClassificationObjects;

		// Token: 0x04002FD7 RID: 12247
		[Autoload]
		public static readonly StorePropertyDefinition HasDlpDetectedClassifications = InternalSchema.HasDlpDetectedClassifications;

		// Token: 0x04002FD8 RID: 12248
		[Autoload]
		public static readonly StorePropertyDefinition RecoveryOptions = InternalSchema.RecoveryOptions;

		// Token: 0x04002FD9 RID: 12249
		[Autoload]
		public static readonly StorePropertyDefinition ConversationCreatorSID = InternalSchema.ConversationCreatorSID;

		// Token: 0x04002FDA RID: 12250
		public static readonly StorePropertyDefinition ConversationFamilyId = InternalSchema.ConversationFamilyId;

		// Token: 0x04002FDB RID: 12251
		public static readonly StorePropertyDefinition ConversationFamilyIndex = InternalSchema.ConversationFamilyIndex;

		// Token: 0x04002FDC RID: 12252
		[Autoload]
		public static readonly StorePropertyDefinition ExchangeApplicationFlags = InternalSchema.ExchangeApplicationFlags;

		// Token: 0x04002FDD RID: 12253
		[Autoload]
		public static readonly StorePropertyDefinition SupportsSideConversation = InternalSchema.SupportsSideConversation;

		// Token: 0x04002FDE RID: 12254
		[Autoload]
		public static readonly StorePropertyDefinition InferenceProcessingNeeded = InternalSchema.InferenceProcessingNeeded;

		// Token: 0x04002FDF RID: 12255
		[Autoload]
		public static readonly StorePropertyDefinition InferenceProcessingActions = InternalSchema.InferenceProcessingActions;

		// Token: 0x04002FE0 RID: 12256
		public static readonly StorePropertyDefinition InferenceClassificationResult = InternalSchema.InferenceClassificationResult;

		// Token: 0x04002FE1 RID: 12257
		public static readonly StorePropertyDefinition WorkingSetId = InternalSchema.WorkingSetId;

		// Token: 0x04002FE2 RID: 12258
		public static readonly StorePropertyDefinition WorkingSetSource = InternalSchema.WorkingSetSource;

		// Token: 0x04002FE3 RID: 12259
		public static readonly StorePropertyDefinition WorkingSetSourcePartition = InternalSchema.WorkingSetSourcePartition;

		// Token: 0x04002FE4 RID: 12260
		public static readonly StorePropertyDefinition WorkingSetFlags = InternalSchema.WorkingSetFlags;

		// Token: 0x04002FE5 RID: 12261
		public static readonly StorePropertyDefinition ConversationLoadRequiredByInference = InternalSchema.ConversationLoadRequiredByInference;

		// Token: 0x04002FE6 RID: 12262
		public static readonly StorePropertyDefinition InferenceConversationClutterActionApplied = InternalSchema.InferenceConversationClutterActionApplied;

		// Token: 0x04002FE7 RID: 12263
		public static readonly StorePropertyDefinition InferenceNeverClutterOverrideApplied = InternalSchema.InferenceNeverClutterOverrideApplied;

		// Token: 0x04002FE8 RID: 12264
		public static readonly StorePropertyDefinition ItemMovedByRule = InternalSchema.ItemMovedByRule;

		// Token: 0x04002FE9 RID: 12265
		public static readonly StorePropertyDefinition ItemMovedByConversationAction = InternalSchema.ItemMovedByConversationAction;

		// Token: 0x04002FEA RID: 12266
		public static readonly StorePropertyDefinition IsStopProcessingRuleApplicable = InternalSchema.IsStopProcessingRuleApplicable;

		// Token: 0x04002FEB RID: 12267
		private static ItemSchema instance = null;

		// Token: 0x04002FEC RID: 12268
		private static readonly PropertyRule[] ItemPropertyRules = new PropertyRule[]
		{
			PropertyRuleLibrary.TruncateSubject
		};
	}
}
