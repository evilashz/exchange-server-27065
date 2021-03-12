using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C0F RID: 3087
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MessageItemSchema : ItemSchema
	{
		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06006E1C RID: 28188 RVA: 0x001D8F8B File Offset: 0x001D718B
		public new static MessageItemSchema Instance
		{
			get
			{
				if (MessageItemSchema.instance == null)
				{
					MessageItemSchema.instance = new MessageItemSchema();
				}
				return MessageItemSchema.instance;
			}
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x001D8FA4 File Offset: 0x001D71A4
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			base.CoreObjectUpdate(coreItem, operation);
			MessageItem.CoreObjectUpdateConversationTopic(coreItem);
			MessageItem.CoreObjectUpdateConversationIndex(coreItem);
			MessageItem.CoreObjectUpdateConversationIndexFixup(coreItem, operation);
			MessageItem.CoreObjectUpdateIconIndex(coreItem);
			MessageItem.CoreObjectUpdateMimeSkeleton(coreItem);
			if (operation == CoreItemOperation.Send && coreItem != null && coreItem.Session != null && coreItem.Session.ActivitySession != null)
			{
				StoreObjectId internalStoreObjectId = ((ICoreObject)coreItem).InternalStoreObjectId;
				string name = base.GetType().Name;
				coreItem.Session.ActivitySession.CaptureMessageSent(internalStoreObjectId, name);
			}
		}

		// Token: 0x06006E1E RID: 28190 RVA: 0x001D9019 File Offset: 0x001D7219
		protected override void AddConstraints(List<StoreObjectConstraint> constraints)
		{
			constraints.Add(new ExtendedRuleConditionConstraint());
			base.AddConstraints(constraints);
		}

		// Token: 0x04003F04 RID: 16132
		public static readonly StorePropertyDefinition MessageBccMe = InternalSchema.MessageBccMe;

		// Token: 0x04003F05 RID: 16133
		[Autoload]
		public static readonly StorePropertyDefinition DRMLicense = InternalSchema.DRMLicense;

		// Token: 0x04003F06 RID: 16134
		[Autoload]
		public static readonly StorePropertyDefinition DRMServerLicenseCompressed = InternalSchema.DRMServerLicenseCompressed;

		// Token: 0x04003F07 RID: 16135
		[Autoload]
		internal static readonly StorePropertyDefinition DRMServerLicense = InternalSchema.DRMServerLicense;

		// Token: 0x04003F08 RID: 16136
		[Autoload]
		public static readonly StorePropertyDefinition DRMRights = InternalSchema.DRMRights;

		// Token: 0x04003F09 RID: 16137
		[Autoload]
		public static readonly StorePropertyDefinition DRMExpiryTime = InternalSchema.DRMExpiryTime;

		// Token: 0x04003F0A RID: 16138
		[Autoload]
		public static readonly StorePropertyDefinition DRMPropsSignature = InternalSchema.DRMPropsSignature;

		// Token: 0x04003F0B RID: 16139
		internal static readonly StorePropertyDefinition DrmPublishLicense = InternalSchema.DrmPublishLicense;

		// Token: 0x04003F0C RID: 16140
		public static readonly StorePropertyDefinition DRMPrelicenseFailure = InternalSchema.DRMPrelicenseFailure;

		// Token: 0x04003F0D RID: 16141
		public static readonly StorePropertyDefinition IsSigned = InternalSchema.IsSigned;

		// Token: 0x04003F0E RID: 16142
		public static readonly StorePropertyDefinition IsReadReceipt = InternalSchema.IsReadReceipt;

		// Token: 0x04003F0F RID: 16143
		public static readonly StorePropertyDefinition DeliverAsRead = InternalSchema.DeliverAsRead;

		// Token: 0x04003F10 RID: 16144
		[Autoload]
		public static readonly StorePropertyDefinition DavSubmitData = InternalSchema.DavSubmitData;

		// Token: 0x04003F11 RID: 16145
		[Autoload]
		public static readonly StorePropertyDefinition IsReadReceiptPending = InternalSchema.IsReadReceiptPending;

		// Token: 0x04003F12 RID: 16146
		[Autoload]
		public static readonly StorePropertyDefinition IsNotReadReceiptPending = InternalSchema.IsNotReadReceiptPending;

		// Token: 0x04003F13 RID: 16147
		[Autoload]
		public static readonly StorePropertyDefinition Flags = CoreItemSchema.Flags;

		// Token: 0x04003F14 RID: 16148
		[Autoload]
		public static readonly StorePropertyDefinition LinkedUrl = InternalSchema.LinkedUrl;

		// Token: 0x04003F15 RID: 16149
		[Autoload]
		public static readonly StorePropertyDefinition LinkedId = InternalSchema.LinkedId;

		// Token: 0x04003F16 RID: 16150
		[Autoload]
		public static readonly StorePropertyDefinition LinkedSiteUrl = InternalSchema.LinkedSiteUrl;

		// Token: 0x04003F17 RID: 16151
		[Autoload]
		public static readonly StorePropertyDefinition LinkedObjectVersion = InternalSchema.LinkedObjectVersion;

		// Token: 0x04003F18 RID: 16152
		[Autoload]
		public static readonly StorePropertyDefinition LinkedDocumentCheckOutTo = InternalSchema.LinkedDocumentCheckOutTo;

		// Token: 0x04003F19 RID: 16153
		[Autoload]
		public static readonly StorePropertyDefinition LinkedDocumentSize = InternalSchema.LinkedDocumentSize;

		// Token: 0x04003F1A RID: 16154
		[Autoload]
		public static readonly StorePropertyDefinition LinkedPendingState = InternalSchema.LinkedPendingState;

		// Token: 0x04003F1B RID: 16155
		[Autoload]
		public static readonly StorePropertyDefinition LinkedLastFullSyncTime = InternalSchema.LinkedLastFullSyncTime;

		// Token: 0x04003F1C RID: 16156
		[Autoload]
		public static readonly StorePropertyDefinition LinkedItemUpdateHistory = InternalSchema.LinkedItemUpdateHistory;

		// Token: 0x04003F1D RID: 16157
		[Autoload]
		public static readonly StorePropertyDefinition UnifiedPolicyNotificationId = InternalSchema.UnifiedPolicyNotificationId;

		// Token: 0x04003F1E RID: 16158
		[Autoload]
		public static readonly StorePropertyDefinition UnifiedPolicyNotificationData = InternalSchema.UnifiedPolicyNotificationData;

		// Token: 0x04003F1F RID: 16159
		[Autoload]
		public static readonly StorePropertyDefinition IsReadReceiptRequested = InternalSchema.IsReadReceiptRequested;

		// Token: 0x04003F20 RID: 16160
		[Autoload]
		public static readonly StorePropertyDefinition DeferUnreadFlag = InternalSchema.ItemTemporaryFlag;

		// Token: 0x04003F21 RID: 16161
		[DetectCodepage]
		public static readonly StorePropertyDefinition ReadReceiptDisplayName = InternalSchema.ReadReceiptDisplayName;

		// Token: 0x04003F22 RID: 16162
		public static readonly StorePropertyDefinition ReadReceiptEmailAddress = InternalSchema.ReadReceiptEmailAddress;

		// Token: 0x04003F23 RID: 16163
		[Autoload]
		public static readonly StorePropertyDefinition ReadReceiptAddrType = InternalSchema.ReadReceiptAddrType;

		// Token: 0x04003F24 RID: 16164
		[Autoload]
		public static readonly StorePropertyDefinition LastVerbExecuted = InternalSchema.LastVerbExecuted;

		// Token: 0x04003F25 RID: 16165
		[Autoload]
		public static readonly StorePropertyDefinition LastVerbExecutionTime = InternalSchema.LastVerbExecutionTime;

		// Token: 0x04003F26 RID: 16166
		[Autoload]
		public static readonly StorePropertyDefinition ReplyForwardStatus = InternalSchema.ReplyForwardStatus;

		// Token: 0x04003F27 RID: 16167
		[Autoload]
		public static readonly StorePropertyDefinition ReplyToBlob = InternalSchema.ReplyToBlob;

		// Token: 0x04003F28 RID: 16168
		public static readonly StorePropertyDefinition ReplyToBlobExists = InternalSchema.ReplyToBlobExists;

		// Token: 0x04003F29 RID: 16169
		[DetectCodepage]
		public static readonly StorePropertyDefinition ReplyToNames = InternalSchema.ReplyToNames;

		// Token: 0x04003F2A RID: 16170
		public static readonly StorePropertyDefinition ReplyToNamesExists = InternalSchema.ReplyToNamesExists;

		// Token: 0x04003F2B RID: 16171
		[Autoload]
		public static readonly StorePropertyDefinition LikersBlob = InternalSchema.LikersBlob;

		// Token: 0x04003F2C RID: 16172
		[DetectCodepage]
		public static readonly StorePropertyDefinition LikeCount = InternalSchema.LikeCount;

		// Token: 0x04003F2D RID: 16173
		public static readonly StorePropertyDefinition PeopleCentricConversationId = InternalSchema.PeopleCentricConversationId;

		// Token: 0x04003F2E RID: 16174
		[Autoload]
		public static readonly StorePropertyDefinition SenderAddressType = InternalSchema.SenderAddressType;

		// Token: 0x04003F2F RID: 16175
		[DetectCodepage]
		public static readonly StorePropertyDefinition SenderDisplayName = InternalSchema.SenderDisplayName;

		// Token: 0x04003F30 RID: 16176
		[Autoload]
		public static readonly StorePropertyDefinition SenderEmailAddress = InternalSchema.SenderEmailAddress;

		// Token: 0x04003F31 RID: 16177
		[Autoload]
		internal static readonly StorePropertyDefinition SenderEntryId = InternalSchema.SenderEntryId;

		// Token: 0x04003F32 RID: 16178
		[Autoload]
		public static readonly StorePropertyDefinition DRMProtectionNeeded = InternalSchema.XMsExchangeOrganizationRightsProtectMessage;

		// Token: 0x04003F33 RID: 16179
		[Autoload]
		public static readonly StorePropertyDefinition IsResend = CoreItemSchema.IsResend;

		// Token: 0x04003F34 RID: 16180
		[Autoload]
		public static readonly StorePropertyDefinition NeedSpecialRecipientProcessing = CoreItemSchema.NeedSpecialRecipientProcessing;

		// Token: 0x04003F35 RID: 16181
		public static readonly StorePropertyDefinition SwappedToDoStore = InternalSchema.SwappedToDoStore;

		// Token: 0x04003F36 RID: 16182
		[Autoload]
		public static readonly StorePropertyDefinition AllAttachmentsHidden = InternalSchema.AllAttachmentsHidden;

		// Token: 0x04003F37 RID: 16183
		public static readonly StorePropertyDefinition MessageAudioNotes = InternalSchema.MessageAudioNotes;

		// Token: 0x04003F38 RID: 16184
		public static readonly StorePropertyDefinition SenderTelephoneNumber = InternalSchema.SenderTelephoneNumber;

		// Token: 0x04003F39 RID: 16185
		public static readonly StorePropertyDefinition PstnCallbackTelephoneNumber = InternalSchema.PstnCallbackTelephoneNumber;

		// Token: 0x04003F3A RID: 16186
		public static readonly StorePropertyDefinition UcSubject = InternalSchema.UcSubject;

		// Token: 0x04003F3B RID: 16187
		public static readonly StorePropertyDefinition VoiceMessageDuration = InternalSchema.VoiceMessageDuration;

		// Token: 0x04003F3C RID: 16188
		public static readonly StorePropertyDefinition VoiceMessageSenderName = InternalSchema.VoiceMessageSenderName;

		// Token: 0x04003F3D RID: 16189
		public static readonly StorePropertyDefinition FaxNumberOfPages = InternalSchema.FaxNumberOfPages;

		// Token: 0x04003F3E RID: 16190
		public static readonly StorePropertyDefinition VoiceMessageAttachmentOrder = InternalSchema.VoiceMessageAttachmentOrder;

		// Token: 0x04003F3F RID: 16191
		public static readonly StorePropertyDefinition QuickCaptureReminders = InternalSchema.ModernReminders;

		// Token: 0x04003F40 RID: 16192
		public static readonly StorePropertyDefinition ModernReminders = InternalSchema.ModernReminders;

		// Token: 0x04003F41 RID: 16193
		public static readonly StorePropertyDefinition ModernRemindersState = InternalSchema.ModernRemindersState;

		// Token: 0x04003F42 RID: 16194
		public static readonly StorePropertyDefinition CallId = InternalSchema.CallId;

		// Token: 0x04003F43 RID: 16195
		public static readonly StorePropertyDefinition XMsExchangeUMPartnerAssignedID = InternalSchema.XMsExchangeUMPartnerAssignedID;

		// Token: 0x04003F44 RID: 16196
		public static readonly StorePropertyDefinition XMsExchangeUMPartnerContent = InternalSchema.XMsExchangeUMPartnerContent;

		// Token: 0x04003F45 RID: 16197
		public static readonly StorePropertyDefinition XMsExchangeUMPartnerContext = InternalSchema.XMsExchangeUMPartnerContext;

		// Token: 0x04003F46 RID: 16198
		public static readonly StorePropertyDefinition XMsExchangeUMPartnerStatus = InternalSchema.XMsExchangeUMPartnerStatus;

		// Token: 0x04003F47 RID: 16199
		public static readonly StorePropertyDefinition XMsExchangeUMDialPlanLanguage = InternalSchema.XMsExchangeUMDialPlanLanguage;

		// Token: 0x04003F48 RID: 16200
		public static readonly StorePropertyDefinition XMsExchangeUMCallerInformedOfAnalysis = InternalSchema.XMsExchangeUMCallerInformedOfAnalysis;

		// Token: 0x04003F49 RID: 16201
		public static readonly StorePropertyDefinition ReceivedSPF = InternalSchema.ReceivedSPF;

		// Token: 0x04003F4A RID: 16202
		public static readonly StorePropertyDefinition AsrData = InternalSchema.AsrData;

		// Token: 0x04003F4B RID: 16203
		public static readonly StorePropertyDefinition XCDRDataCallStartTime = InternalSchema.XCDRDataCallStartTime;

		// Token: 0x04003F4C RID: 16204
		public static readonly StorePropertyDefinition XCDRDataCallType = InternalSchema.XCDRDataCallType;

		// Token: 0x04003F4D RID: 16205
		public static readonly StorePropertyDefinition XCDRDataCallIdentity = InternalSchema.XCDRDataCallIdentity;

		// Token: 0x04003F4E RID: 16206
		public static readonly StorePropertyDefinition XCDRDataParentCallIdentity = InternalSchema.XCDRDataParentCallIdentity;

		// Token: 0x04003F4F RID: 16207
		public static readonly StorePropertyDefinition XCDRDataUMServerName = InternalSchema.XCDRDataUMServerName;

		// Token: 0x04003F50 RID: 16208
		public static readonly StorePropertyDefinition XCDRDataDialPlanGuid = InternalSchema.XCDRDataDialPlanGuid;

		// Token: 0x04003F51 RID: 16209
		public static readonly StorePropertyDefinition XCDRDataDialPlanName = InternalSchema.XCDRDataDialPlanName;

		// Token: 0x04003F52 RID: 16210
		public static readonly StorePropertyDefinition XCDRDataCallDuration = InternalSchema.XCDRDataCallDuration;

		// Token: 0x04003F53 RID: 16211
		public static readonly StorePropertyDefinition XCDRDataGatewayGuid = InternalSchema.XCDRDataGatewayGuid;

		// Token: 0x04003F54 RID: 16212
		public static readonly StorePropertyDefinition XCDRDataIPGatewayAddress = InternalSchema.XCDRDataIPGatewayAddress;

		// Token: 0x04003F55 RID: 16213
		public static readonly StorePropertyDefinition XCDRDataIPGatewayName = InternalSchema.XCDRDataIPGatewayName;

		// Token: 0x04003F56 RID: 16214
		public static readonly StorePropertyDefinition XCDRDataCalledPhoneNumber = InternalSchema.XCDRDataCalledPhoneNumber;

		// Token: 0x04003F57 RID: 16215
		public static readonly StorePropertyDefinition XCDRDataCallerPhoneNumber = InternalSchema.XCDRDataCallerPhoneNumber;

		// Token: 0x04003F58 RID: 16216
		public static readonly StorePropertyDefinition XCDRDataOfferResult = InternalSchema.XCDRDataOfferResult;

		// Token: 0x04003F59 RID: 16217
		public static readonly StorePropertyDefinition XCDRDataDropCallReason = InternalSchema.XCDRDataDropCallReason;

		// Token: 0x04003F5A RID: 16218
		public static readonly StorePropertyDefinition XCDRDataReasonForCall = InternalSchema.XCDRDataReasonForCall;

		// Token: 0x04003F5B RID: 16219
		public static readonly StorePropertyDefinition XCDRDataTransferredNumber = InternalSchema.XCDRDataTransferredNumber;

		// Token: 0x04003F5C RID: 16220
		public static readonly StorePropertyDefinition XCDRDataDialedString = InternalSchema.XCDRDataDialedString;

		// Token: 0x04003F5D RID: 16221
		public static readonly StorePropertyDefinition XCDRDataCallerMailboxAlias = InternalSchema.XCDRDataCallerMailboxAlias;

		// Token: 0x04003F5E RID: 16222
		public static readonly StorePropertyDefinition XCDRDataCalleeMailboxAlias = InternalSchema.XCDRDataCalleeMailboxAlias;

		// Token: 0x04003F5F RID: 16223
		public static readonly StorePropertyDefinition XCDRDataAutoAttendantName = InternalSchema.XCDRDataAutoAttendantName;

		// Token: 0x04003F60 RID: 16224
		public static readonly StorePropertyDefinition XCDRDataAudioCodec = InternalSchema.XCDRDataAudioCodec;

		// Token: 0x04003F61 RID: 16225
		public static readonly StorePropertyDefinition XCDRDataBurstDensity = InternalSchema.XCDRDataBurstDensity;

		// Token: 0x04003F62 RID: 16226
		public static readonly StorePropertyDefinition XCDRDataBurstDuration = InternalSchema.XCDRDataBurstDuration;

		// Token: 0x04003F63 RID: 16227
		public static readonly StorePropertyDefinition XCDRDataJitter = InternalSchema.XCDRDataJitter;

		// Token: 0x04003F64 RID: 16228
		public static readonly StorePropertyDefinition XCDRDataNMOS = InternalSchema.XCDRDataNMOS;

		// Token: 0x04003F65 RID: 16229
		public static readonly StorePropertyDefinition XCDRDataNMOSDegradation = InternalSchema.XCDRDataNMOSDegradation;

		// Token: 0x04003F66 RID: 16230
		public static readonly StorePropertyDefinition XCDRDataNMOSDegradationJitter = InternalSchema.XCDRDataNMOSDegradationJitter;

		// Token: 0x04003F67 RID: 16231
		public static readonly StorePropertyDefinition XCDRDataNMOSDegradationPacketLoss = InternalSchema.XCDRDataNMOSDegradationPacketLoss;

		// Token: 0x04003F68 RID: 16232
		public static readonly StorePropertyDefinition XCDRDataPacketLoss = InternalSchema.XCDRDataPacketLoss;

		// Token: 0x04003F69 RID: 16233
		public static readonly StorePropertyDefinition XCDRDataRoundTrip = InternalSchema.XCDRDataRoundTrip;

		// Token: 0x04003F6A RID: 16234
		public static readonly StorePropertyDefinition ExpiryTime = InternalSchema.ExpiryTime;

		// Token: 0x04003F6B RID: 16235
		[Autoload]
		public static readonly StorePropertyDefinition IsDeliveryReceiptRequested = InternalSchema.IsDeliveryReceiptRequested;

		// Token: 0x04003F6C RID: 16236
		[Autoload]
		public static readonly StorePropertyDefinition IsNonDeliveryReceiptRequested = InternalSchema.IsNonDeliveryReceiptRequested;

		// Token: 0x04003F6D RID: 16237
		[Autoload]
		public static readonly StorePropertyDefinition HasBeenSubmitted = InternalSchema.HasBeenSubmitted;

		// Token: 0x04003F6E RID: 16238
		[Autoload]
		public static readonly StorePropertyDefinition IsAssociated = InternalSchema.IsAssociated;

		// Token: 0x04003F6F RID: 16239
		[Autoload]
		public static readonly StorePropertyDefinition IsDraft = InternalSchema.IsDraft;

		// Token: 0x04003F70 RID: 16240
		[Autoload]
		public static readonly StorePropertyDefinition IsRead = InternalSchema.IsRead;

		// Token: 0x04003F71 RID: 16241
		[Autoload]
		public static readonly StorePropertyDefinition WasEverRead = InternalSchema.WasEverRead;

		// Token: 0x04003F72 RID: 16242
		public static readonly StorePropertyDefinition MapiHasAttachment = CoreItemSchema.MapiHasAttachment;

		// Token: 0x04003F73 RID: 16243
		internal static readonly StorePropertyDefinition MapiPriority = InternalSchema.MapiPriority;

		// Token: 0x04003F74 RID: 16244
		[Autoload]
		internal static readonly StorePropertyDefinition MapiReplyToBlob = InternalSchema.MapiReplyToBlob;

		// Token: 0x04003F75 RID: 16245
		internal static readonly StorePropertyDefinition MapiReplyToNames = InternalSchema.MapiReplyToNames;

		// Token: 0x04003F76 RID: 16246
		internal static readonly StorePropertyDefinition MapiLikersBlob = InternalSchema.MapiLikersBlob;

		// Token: 0x04003F77 RID: 16247
		internal static readonly StorePropertyDefinition MapiLikeCount = InternalSchema.MapiLikeCount;

		// Token: 0x04003F78 RID: 16248
		public static readonly StorePropertyDefinition MessageDeliveryNotificationSent = InternalSchema.MessageDeliveryNotificationSent;

		// Token: 0x04003F79 RID: 16249
		public static readonly StorePropertyDefinition MessageAnswered = InternalSchema.MessageAnswered;

		// Token: 0x04003F7A RID: 16250
		public static readonly StorePropertyDefinition MimeConversionFailed = InternalSchema.MimeConversionFailed;

		// Token: 0x04003F7B RID: 16251
		public static readonly StorePropertyDefinition MessageDelMarked = InternalSchema.MessageDelMarked;

		// Token: 0x04003F7C RID: 16252
		public static readonly StorePropertyDefinition MessageDraft = InternalSchema.MessageDraft;

		// Token: 0x04003F7D RID: 16253
		public static readonly StorePropertyDefinition MessageHidden = InternalSchema.MessageHidden;

		// Token: 0x04003F7E RID: 16254
		public static readonly StorePropertyDefinition MessageHighlighted = InternalSchema.MessageHighlighted;

		// Token: 0x04003F7F RID: 16255
		public static readonly StorePropertyDefinition MessageInConflict = CoreItemSchema.MessageInConflict;

		// Token: 0x04003F80 RID: 16256
		internal static readonly StorePropertyDefinition MessageRecipients = InternalSchema.MessageRecipients;

		// Token: 0x04003F81 RID: 16257
		public static readonly StorePropertyDefinition MessageRemoteDelete = InternalSchema.MessageRemoteDelete;

		// Token: 0x04003F82 RID: 16258
		public static readonly StorePropertyDefinition MessageRemoteDownload = InternalSchema.MessageRemoteDownload;

		// Token: 0x04003F83 RID: 16259
		public static readonly StorePropertyDefinition MessageTagged = InternalSchema.MessageTagged;

		// Token: 0x04003F84 RID: 16260
		public static readonly StorePropertyDefinition MID = InternalSchema.MID;

		// Token: 0x04003F85 RID: 16261
		public static readonly StorePropertyDefinition LTID = InternalSchema.LTID;

		// Token: 0x04003F86 RID: 16262
		public static readonly StorePropertyDefinition OriginalAuthorName = InternalSchema.OriginalAuthorName;

		// Token: 0x04003F87 RID: 16263
		public static readonly StorePropertyDefinition ReceivedRepresenting = InternalSchema.ReceivedRepresenting;

		// Token: 0x04003F88 RID: 16264
		public static readonly StorePropertyDefinition ReceivedRepresentingEntryId = InternalSchema.ReceivedRepresentingEntryId;

		// Token: 0x04003F89 RID: 16265
		public static readonly StorePropertyDefinition ReceivedRepresentingAddressType = InternalSchema.ReceivedRepresentingAddressType;

		// Token: 0x04003F8A RID: 16266
		[DetectCodepage]
		public static readonly StorePropertyDefinition ReceivedRepresentingDisplayName = InternalSchema.ReceivedRepresentingDisplayName;

		// Token: 0x04003F8B RID: 16267
		public static readonly StorePropertyDefinition ReceivedRepresentingEmailAddress = InternalSchema.ReceivedRepresentingEmailAddress;

		// Token: 0x04003F8C RID: 16268
		public static readonly StorePropertyDefinition ReceivedRepresentingSmtpAddress = InternalSchema.ReceivedRepresentingSmtpAddress;

		// Token: 0x04003F8D RID: 16269
		public static readonly StorePropertyDefinition ReceivedRepresentingSearchKey = InternalSchema.ReceivedRepresentingSearchKey;

		// Token: 0x04003F8E RID: 16270
		public static readonly StorePropertyDefinition ElcAutoCopyLabel = InternalSchema.ElcAutoCopyLabel;

		// Token: 0x04003F8F RID: 16271
		public static readonly StorePropertyDefinition SharingProviderGuid = InternalSchema.SharingProviderGuid;

		// Token: 0x04003F90 RID: 16272
		public static readonly StorePropertyDefinition SharingProviderName = InternalSchema.SharingProviderName;

		// Token: 0x04003F91 RID: 16273
		public static readonly StorePropertyDefinition SharingProviderUrl = InternalSchema.SharingProviderUrl;

		// Token: 0x04003F92 RID: 16274
		public static readonly StorePropertyDefinition SharingRemotePath = InternalSchema.SharingRemotePath;

		// Token: 0x04003F93 RID: 16275
		public static readonly StorePropertyDefinition SharingRemoteName = InternalSchema.SharingRemoteName;

		// Token: 0x04003F94 RID: 16276
		public static readonly StorePropertyDefinition SharingLocalName = InternalSchema.SharingLocalName;

		// Token: 0x04003F95 RID: 16277
		public static readonly StorePropertyDefinition SharingLocalUid = InternalSchema.SharingLocalUid;

		// Token: 0x04003F96 RID: 16278
		public static readonly StorePropertyDefinition SharingLocalType = InternalSchema.SharingLocalType;

		// Token: 0x04003F97 RID: 16279
		public static readonly StorePropertyDefinition SharingCapabilities = InternalSchema.SharingCapabilities;

		// Token: 0x04003F98 RID: 16280
		public static readonly StorePropertyDefinition SharingFlavor = InternalSchema.SharingFlavor;

		// Token: 0x04003F99 RID: 16281
		public static readonly StorePropertyDefinition SharingInstanceGuid = InternalSchema.SharingInstanceGuid;

		// Token: 0x04003F9A RID: 16282
		public static readonly StorePropertyDefinition SharingRemoteType = InternalSchema.SharingRemoteType;

		// Token: 0x04003F9B RID: 16283
		public static readonly StorePropertyDefinition SharingLastSync = InternalSchema.SharingLastSync;

		// Token: 0x04003F9C RID: 16284
		public static readonly StorePropertyDefinition SharingRssHash = InternalSchema.SharingRssHash;

		// Token: 0x04003F9D RID: 16285
		public static readonly StorePropertyDefinition SharingRemoteLastMod = InternalSchema.SharingRemoteLastMod;

		// Token: 0x04003F9E RID: 16286
		public static readonly StorePropertyDefinition SharingConfigUrl = InternalSchema.SharingConfigUrl;

		// Token: 0x04003F9F RID: 16287
		public static readonly StorePropertyDefinition SharingDetail = InternalSchema.SharingDetail;

		// Token: 0x04003FA0 RID: 16288
		public static readonly StorePropertyDefinition SharingTimeToLive = InternalSchema.SharingTimeToLive;

		// Token: 0x04003FA1 RID: 16289
		public static readonly StorePropertyDefinition SharingBindingEid = InternalSchema.SharingBindingEid;

		// Token: 0x04003FA2 RID: 16290
		public static readonly StorePropertyDefinition SharingIndexEid = InternalSchema.SharingIndexEid;

		// Token: 0x04003FA3 RID: 16291
		public static readonly StorePropertyDefinition SharingRemoteComment = InternalSchema.SharingRemoteComment;

		// Token: 0x04003FA4 RID: 16292
		public static readonly StorePropertyDefinition SharingLocalStoreUid = InternalSchema.SharingLocalStoreUid;

		// Token: 0x04003FA5 RID: 16293
		public static readonly StorePropertyDefinition SharingRemoteByteSize = InternalSchema.SharingRemoteByteSize;

		// Token: 0x04003FA6 RID: 16294
		public static readonly StorePropertyDefinition SharingRemoteCrc = InternalSchema.SharingRemoteCrc;

		// Token: 0x04003FA7 RID: 16295
		public static readonly StorePropertyDefinition SharingLastAutoSync = InternalSchema.SharingLastAutoSync;

		// Token: 0x04003FA8 RID: 16296
		public static readonly StorePropertyDefinition SharingSavedSession = InternalSchema.SharingSavedSession;

		// Token: 0x04003FA9 RID: 16297
		public static readonly StorePropertyDefinition SharingSubscriptionVersion = InternalSchema.SharingSubscriptionVersion;

		// Token: 0x04003FAA RID: 16298
		public static readonly StorePropertyDefinition SharingDetailedStatus = InternalSchema.SharingDetailedStatus;

		// Token: 0x04003FAB RID: 16299
		public static readonly StorePropertyDefinition SharingDiagnostics = InternalSchema.SharingDiagnostics;

		// Token: 0x04003FAC RID: 16300
		public static readonly StorePropertyDefinition SharingSendAsState = InternalSchema.SharingSendAsState;

		// Token: 0x04003FAD RID: 16301
		public static readonly StorePropertyDefinition SharingSendAsValidatedEmail = InternalSchema.SharingSendAsValidatedEmail;

		// Token: 0x04003FAE RID: 16302
		public static readonly StorePropertyDefinition SharingSendAsSubmissionUrl = InternalSchema.SharingSendAsSubmissionUrl;

		// Token: 0x04003FAF RID: 16303
		public static readonly StorePropertyDefinition SharingEwsUri = InternalSchema.SharingEwsUri;

		// Token: 0x04003FB0 RID: 16304
		public static readonly StorePropertyDefinition SharingRemoteExchangeVersion = InternalSchema.SharingRemoteExchangeVersion;

		// Token: 0x04003FB1 RID: 16305
		public static readonly StorePropertyDefinition SharingRemoteUserDomain = InternalSchema.SharingRemoteUserDomain;

		// Token: 0x04003FB2 RID: 16306
		public static readonly StorePropertyDefinition RssServerLockStartTime = InternalSchema.RssServerLockStartTime;

		// Token: 0x04003FB3 RID: 16307
		public static readonly StorePropertyDefinition RssServerLockTimeout = InternalSchema.RssServerLockTimeout;

		// Token: 0x04003FB4 RID: 16308
		public static readonly StorePropertyDefinition RssServerLockClientName = InternalSchema.RssServerLockClientName;

		// Token: 0x04003FB5 RID: 16309
		public static readonly StorePropertyDefinition ReceivedBy = InternalSchema.ReceivedBy;

		// Token: 0x04003FB6 RID: 16310
		public static readonly StorePropertyDefinition ReceivedByAddrType = InternalSchema.ReceivedByAddrType;

		// Token: 0x04003FB7 RID: 16311
		public static readonly StorePropertyDefinition ReceivedByEmailAddress = InternalSchema.ReceivedByEmailAddress;

		// Token: 0x04003FB8 RID: 16312
		public static readonly StorePropertyDefinition ReceivedByEntryId = InternalSchema.ReceivedByEntryId;

		// Token: 0x04003FB9 RID: 16313
		[DetectCodepage]
		public static readonly StorePropertyDefinition ReceivedByName = InternalSchema.ReceivedByName;

		// Token: 0x04003FBA RID: 16314
		public static readonly StorePropertyDefinition ReceivedBySearchKey = InternalSchema.ReceivedBySearchKey;

		// Token: 0x04003FBB RID: 16315
		[Autoload]
		public static readonly StorePropertyDefinition ReplyTime = InternalSchema.ReplyTime;

		// Token: 0x04003FBC RID: 16316
		internal static readonly StorePropertyDefinition TnefCorrelationKey = InternalSchema.TnefCorrelationKey;

		// Token: 0x04003FBD RID: 16317
		public static readonly StorePropertyDefinition TransportMessageHeaders = InternalSchema.TransportMessageHeaders;

		// Token: 0x04003FBE RID: 16318
		public static readonly StorePropertyDefinition OofReplyType = InternalSchema.OofReplyType;

		// Token: 0x04003FBF RID: 16319
		[Autoload]
		public static readonly StorePropertyDefinition AutoResponseSuppress = InternalSchema.AutoResponseSuppress;

		// Token: 0x04003FC0 RID: 16320
		public static readonly StorePropertyDefinition MessageLocaleId = InternalSchema.MessageLocaleId;

		// Token: 0x04003FC1 RID: 16321
		internal static readonly StorePropertyDefinition AssociatedSearchFolderLastUsedTime = InternalSchema.AssociatedSearchFolderLastUsedTime;

		// Token: 0x04003FC2 RID: 16322
		public static readonly StorePropertyDefinition ToDoSubOrdinal = InternalSchema.ToDoSubOrdinal;

		// Token: 0x04003FC3 RID: 16323
		public static readonly StorePropertyDefinition ToDoOrdinalDate = InternalSchema.ToDoOrdinalDate;

		// Token: 0x04003FC4 RID: 16324
		public static readonly StorePropertyDefinition XMsExchOrganizationAuthDomain = InternalSchema.XMsExchOrganizationAuthDomain;

		// Token: 0x04003FC5 RID: 16325
		public static readonly StorePropertyDefinition XMsExchOrganizationAuthAs = InternalSchema.XMsExchOrganizationAuthAs;

		// Token: 0x04003FC6 RID: 16326
		public static readonly StorePropertyDefinition XMsExchOrganizationAuthMechanism = InternalSchema.XMsExchOrganizationAuthMechanism;

		// Token: 0x04003FC7 RID: 16327
		public static readonly StorePropertyDefinition XMsExchOrganizationAuthSource = InternalSchema.XMsExchOrganizationAuthSource;

		// Token: 0x04003FC8 RID: 16328
		public static readonly StorePropertyDefinition XMsExchOrganizationOriginalClientIPAddress = CoreItemSchema.XMsExchOrganizationOriginalClientIPAddress;

		// Token: 0x04003FC9 RID: 16329
		public static readonly StorePropertyDefinition XMsExchOrganizationOriginalServerIPAddress = CoreItemSchema.XMsExchOrganizationOriginalServerIPAddress;

		// Token: 0x04003FCA RID: 16330
		public static readonly StorePropertyDefinition SenderIdStatus = InternalSchema.SenderIdStatus;

		// Token: 0x04003FCB RID: 16331
		public static readonly StorePropertyDefinition ApprovalAllowedDecisionMakers = InternalSchema.ApprovalAllowedDecisionMakers;

		// Token: 0x04003FCC RID: 16332
		public static readonly StorePropertyDefinition ApprovalRequestor = InternalSchema.ApprovalRequestor;

		// Token: 0x04003FCD RID: 16333
		public static readonly StorePropertyDefinition ApprovalDecisionMaker = InternalSchema.ApprovalDecisionMaker;

		// Token: 0x04003FCE RID: 16334
		public static readonly StorePropertyDefinition ApprovalDecision = InternalSchema.ApprovalDecision;

		// Token: 0x04003FCF RID: 16335
		public static readonly StorePropertyDefinition ApprovalDecisionTime = InternalSchema.ApprovalDecisionTime;

		// Token: 0x04003FD0 RID: 16336
		public static readonly StorePropertyDefinition ApprovalRequestMessageId = InternalSchema.ApprovalRequestMessageId;

		// Token: 0x04003FD1 RID: 16337
		public static readonly StorePropertyDefinition ApprovalStatus = InternalSchema.ApprovalStatus;

		// Token: 0x04003FD2 RID: 16338
		public static readonly StorePropertyDefinition ApprovalDecisionMakersNdred = InternalSchema.ApprovalDecisionMakersNdred;

		// Token: 0x04003FD3 RID: 16339
		public static readonly StorePropertyDefinition ApprovalApplicationId = InternalSchema.ApprovalApplicationId;

		// Token: 0x04003FD4 RID: 16340
		public static readonly StorePropertyDefinition ApprovalApplicationData = InternalSchema.ApprovalApplicationData;

		// Token: 0x04003FD5 RID: 16341
		public static readonly StorePropertyDefinition SecureSubmitFlags = InternalSchema.SecureSubmitFlags;

		// Token: 0x04003FD6 RID: 16342
		[Autoload]
		public static readonly StorePropertyDefinition ClientSubmittedSecurely = CoreItemSchema.ClientSubmittedSecurely;

		// Token: 0x04003FD7 RID: 16343
		[Autoload]
		public static readonly StorePropertyDefinition ServerSubmittedSecurely = InternalSchema.ServerSubmittedSecurely;

		// Token: 0x04003FD8 RID: 16344
		[Autoload]
		public static readonly StorePropertyDefinition AcceptLanguage = InternalSchema.AcceptLanguage;

		// Token: 0x04003FD9 RID: 16345
		public static readonly StorePropertyDefinition DlExpansionProhibited = InternalSchema.DlExpansionProhibited;

		// Token: 0x04003FDA RID: 16346
		public static readonly StorePropertyDefinition RecipientReassignmentProhibited = InternalSchema.RecipientReassignmentProhibited;

		// Token: 0x04003FDB RID: 16347
		public static readonly StorePropertyDefinition DeferredDeliveryTime = InternalSchema.DeferredDeliveryTime;

		// Token: 0x04003FDC RID: 16348
		public static readonly StorePropertyDefinition DeferredSendTime = InternalSchema.DeferredSendTime;

		// Token: 0x04003FDD RID: 16349
		public static readonly StorePropertyDefinition HasWrittenTracking = InternalSchema.HasWrittenTracking;

		// Token: 0x04003FDE RID: 16350
		public static readonly StorePropertyDefinition MessageSubmissionId = InternalSchema.MessageSubmissionId;

		// Token: 0x04003FDF RID: 16351
		public static readonly StorePropertyDefinition ProviderSubmitTime = InternalSchema.ProviderSubmitTime;

		// Token: 0x04003FE0 RID: 16352
		public static readonly StorePropertyDefinition SenderSearchKey = InternalSchema.SenderSearchKey;

		// Token: 0x04003FE1 RID: 16353
		public static readonly StorePropertyDefinition SenderSmtpAddress = InternalSchema.SenderSmtpAddress;

		// Token: 0x04003FE2 RID: 16354
		public static readonly StorePropertyDefinition MapiRecurrenceType = InternalSchema.MapiRecurrenceType;

		// Token: 0x04003FE3 RID: 16355
		[Autoload]
		internal static readonly StorePropertyDefinition ReportTag = InternalSchema.ReportTag;

		// Token: 0x04003FE4 RID: 16356
		[Autoload]
		public static readonly StorePropertyDefinition VotingBlob = InternalSchema.OutlookUserPropsVerbStream;

		// Token: 0x04003FE5 RID: 16357
		[Autoload]
		public static readonly StorePropertyDefinition VotingResponse = InternalSchema.VotingResponse;

		// Token: 0x04003FE6 RID: 16358
		[Autoload]
		internal static readonly StorePropertyDefinition IsVotingResponse = InternalSchema.IsVotingResponse;

		// Token: 0x04003FE7 RID: 16359
		internal static readonly StorePropertyDefinition LocalDirectory = InternalSchema.LocalDirectory;

		// Token: 0x04003FE8 RID: 16360
		[Autoload]
		public static readonly StorePropertyDefinition MimeSkeleton = InternalSchema.MimeSkeleton;

		// Token: 0x04003FE9 RID: 16361
		public static readonly StorePropertyDefinition BodyContentId = InternalSchema.BodyContentId;

		// Token: 0x04003FEA RID: 16362
		[Autoload]
		public static readonly StorePropertyDefinition MessageToMe = InternalSchema.MessageToMe;

		// Token: 0x04003FEB RID: 16363
		[Autoload]
		public static readonly StorePropertyDefinition MessageCcMe = InternalSchema.MessageCcMe;

		// Token: 0x04003FEC RID: 16364
		public static readonly StorePropertyDefinition FolderId = InternalSchema.FolderId;

		// Token: 0x04003FED RID: 16365
		public static readonly StorePropertyDefinition XLoop = InternalSchema.XLoop;

		// Token: 0x04003FEE RID: 16366
		public static readonly StorePropertyDefinition DoNotDeliver = InternalSchema.DoNotDeliver;

		// Token: 0x04003FEF RID: 16367
		public static readonly StorePropertyDefinition DropMessageInHub = InternalSchema.DropMessageInHub;

		// Token: 0x04003FF0 RID: 16368
		public static readonly StorePropertyDefinition SystemProbeDrop = InternalSchema.SystemProbeDrop;

		// Token: 0x04003FF1 RID: 16369
		public static readonly StorePropertyDefinition XLAMNotificationId = InternalSchema.XLAMNotificationId;

		// Token: 0x04003FF2 RID: 16370
		public static readonly StorePropertyDefinition MapiSubmitLamNotificationId = InternalSchema.MapiSubmitLamNotificationId;

		// Token: 0x04003FF3 RID: 16371
		public static readonly StorePropertyDefinition MapiSubmitSystemProbeActivityId = InternalSchema.MapiSubmitSystemProbeActivityId;

		// Token: 0x04003FF4 RID: 16372
		public static readonly StorePropertyDefinition XMSExchangeOutlookProtectionRuleVersion = InternalSchema.XMSExchangeOutlookProtectionRuleVersion;

		// Token: 0x04003FF5 RID: 16373
		public static readonly StorePropertyDefinition XMSExchangeOutlookProtectionRuleConfigTimestamp = InternalSchema.XMSExchangeOutlookProtectionRuleConfigTimestamp;

		// Token: 0x04003FF6 RID: 16374
		public static readonly StorePropertyDefinition XMSExchangeOutlookProtectionRuleOverridden = InternalSchema.XMSExchangeOutlookProtectionRuleOverridden;

		// Token: 0x04003FF7 RID: 16375
		[Autoload]
		public static readonly StorePropertyDefinition RequireProtectedPlayOnPhone = InternalSchema.XRequireProtectedPlayOnPhone;

		// Token: 0x04003FF8 RID: 16376
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentState = InternalSchema.AppointmentState;

		// Token: 0x04003FF9 RID: 16377
		public static readonly StorePropertyDefinition TextMessageDeliveryStatus = InternalSchema.TextMessageDeliveryStatus;

		// Token: 0x04003FFA RID: 16378
		public static readonly StorePropertyDefinition MessageAnnotation = InternalSchema.MessageAnnotation;

		// Token: 0x04003FFB RID: 16379
		public static readonly StorePropertyDefinition OscContactSources = InternalSchema.OscContactSources;

		// Token: 0x04003FFC RID: 16380
		public static readonly StorePropertyDefinition OscSyncEnabledOnServer = InternalSchema.OscSyncEnabledOnServer;

		// Token: 0x04003FFD RID: 16381
		public static readonly StorePropertyDefinition OutlookContactLinkDateTime = InternalSchema.OutlookContactLinkDateTime;

		// Token: 0x04003FFE RID: 16382
		public static readonly StorePropertyDefinition OutlookContactLinkVersion = InternalSchema.OutlookContactLinkVersion;

		// Token: 0x04003FFF RID: 16383
		public static readonly StorePropertyDefinition ExtractionResult = InternalSchema.ExtractionResult;

		// Token: 0x04004000 RID: 16384
		public static readonly StorePropertyDefinition TriageFeatureVector = InternalSchema.TriageFeatureVector;

		// Token: 0x04004001 RID: 16385
		public static readonly StorePropertyDefinition InferenceClassificationTrackingEx = InternalSchema.InferenceClassificationTrackingEx;

		// Token: 0x04004002 RID: 16386
		public static readonly StorePropertyDefinition InferenceActionTruth = InternalSchema.InferenceActionTruth;

		// Token: 0x04004003 RID: 16387
		public static readonly StorePropertyDefinition InferenceUniqueActionLabelData = InternalSchema.InferenceUniqueActionLabelData;

		// Token: 0x04004004 RID: 16388
		public static readonly StorePropertyDefinition LatestMessageWordCount = InternalSchema.LatestMessageWordCount;

		// Token: 0x04004005 RID: 16389
		[Autoload]
		public static readonly StorePropertyDefinition IsFromFavoriteSender = InternalSchema.IsFromFavoriteSender;

		// Token: 0x04004006 RID: 16390
		[Autoload]
		public static readonly StorePropertyDefinition IsFromPerson = InternalSchema.IsFromPerson;

		// Token: 0x04004007 RID: 16391
		[Autoload]
		public static readonly StorePropertyDefinition IsSpecificMessageReply = InternalSchema.IsSpecificMessageReply;

		// Token: 0x04004008 RID: 16392
		[Autoload]
		public static readonly StorePropertyDefinition IsSpecificMessageReplyStamped = InternalSchema.IsSpecificMessageReplyStamped;

		// Token: 0x04004009 RID: 16393
		[Autoload]
		public static readonly StorePropertyDefinition RelyOnConversationIndex = InternalSchema.RelyOnConversationIndex;

		// Token: 0x0400400A RID: 16394
		[Autoload]
		public static readonly StorePropertyDefinition IsClutterOverridden = InternalSchema.IsClutterOverridden;

		// Token: 0x0400400B RID: 16395
		[Autoload]
		public static readonly StorePropertyDefinition IsGroupEscalationMessage = InternalSchema.IsGroupEscalationMessage;

		// Token: 0x0400400C RID: 16396
		public static readonly StorePropertyDefinition NeedGroupExpansion = InternalSchema.NeedGroupExpansion;

		// Token: 0x0400400D RID: 16397
		public static readonly StorePropertyDefinition GroupExpansionRecipients = InternalSchema.GroupExpansionRecipients;

		// Token: 0x0400400E RID: 16398
		public static readonly StorePropertyDefinition ToGroupExpansionRecipients = InternalSchema.ToGroupExpansionRecipients;

		// Token: 0x0400400F RID: 16399
		public static readonly StorePropertyDefinition CcGroupExpansionRecipients = InternalSchema.CcGroupExpansionRecipients;

		// Token: 0x04004010 RID: 16400
		public static readonly StorePropertyDefinition BccGroupExpansionRecipients = InternalSchema.BccGroupExpansionRecipients;

		// Token: 0x04004011 RID: 16401
		public static readonly StorePropertyDefinition GroupExpansionError = InternalSchema.GroupExpansionError;

		// Token: 0x04004012 RID: 16402
		public static readonly StorePropertyDefinition InferenceMessageIdentifier = InternalSchema.InferenceMessageIdentifier;

		// Token: 0x04004013 RID: 16403
		public static readonly StorePropertyDefinition SenderRelevanceScore = InternalSchema.SenderRelevanceScore;

		// Token: 0x04004014 RID: 16404
		public static readonly StorePropertyDefinition SenderClass = InternalSchema.SenderClass;

		// Token: 0x04004015 RID: 16405
		public static readonly StorePropertyDefinition CurrentFolderReason = InternalSchema.CurrentFolderReason;

		// Token: 0x04004016 RID: 16406
		public static readonly PropertyDefinition[] SingleRecipientProperties = new PropertyDefinition[]
		{
			ItemSchema.From,
			ItemSchema.Sender,
			MessageItemSchema.ReceivedRepresenting,
			MessageItemSchema.ReceivedBy
		};

		// Token: 0x04004017 RID: 16407
		private static MessageItemSchema instance = null;
	}
}
