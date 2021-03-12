using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C10 RID: 3088
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregationSubscriptionMessageSchema : MessageItemSchema
	{
		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x06006E21 RID: 28193 RVA: 0x001D9B2C File Offset: 0x001D7D2C
		public new static AggregationSubscriptionMessageSchema Instance
		{
			get
			{
				if (AggregationSubscriptionMessageSchema.instance == null)
				{
					AggregationSubscriptionMessageSchema.instance = new AggregationSubscriptionMessageSchema();
				}
				return AggregationSubscriptionMessageSchema.instance;
			}
		}

		// Token: 0x04004018 RID: 16408
		public static readonly StorePropertyDefinition SharingInitiatorName = InternalSchema.SharingInitiatorName;

		// Token: 0x04004019 RID: 16409
		public static readonly StorePropertyDefinition SharingInitiatorSmtp = InternalSchema.SharingInitiatorSmtp;

		// Token: 0x0400401A RID: 16410
		public static readonly StorePropertyDefinition SharingRemoteUser = InternalSchema.SharingRemoteUser;

		// Token: 0x0400401B RID: 16411
		public static readonly StorePropertyDefinition SharingRemotePass = InternalSchema.SharingRemotePass;

		// Token: 0x0400401C RID: 16412
		public static readonly StorePropertyDefinition SharingLastSuccessSyncTime = InternalSchema.SharingLastSuccessSyncTime;

		// Token: 0x0400401D RID: 16413
		public static readonly StorePropertyDefinition SharingSyncRange = InternalSchema.SharingSyncRange;

		// Token: 0x0400401E RID: 16414
		public static readonly StorePropertyDefinition SharingAggregationStatus = InternalSchema.SharingAggregationStatus;

		// Token: 0x0400401F RID: 16415
		public static readonly StorePropertyDefinition SharingWlidAuthPolicy = InternalSchema.SharingWlidAuthPolicy;

		// Token: 0x04004020 RID: 16416
		public static readonly StorePropertyDefinition SharingWlidUserPuid = InternalSchema.SharingWlidUserPuid;

		// Token: 0x04004021 RID: 16417
		public static readonly StorePropertyDefinition SharingWlidAuthToken = InternalSchema.SharingWlidAuthToken;

		// Token: 0x04004022 RID: 16418
		public static readonly StorePropertyDefinition SharingWlidAuthTokenExpireTime = InternalSchema.SharingWlidAuthTokenExpireTime;

		// Token: 0x04004023 RID: 16419
		public static readonly StorePropertyDefinition SharingMinSyncPollInterval = InternalSchema.SharingMinSyncPollInterval;

		// Token: 0x04004024 RID: 16420
		public static readonly StorePropertyDefinition SharingMinSettingPollInterval = InternalSchema.SharingMinSettingPollInterval;

		// Token: 0x04004025 RID: 16421
		public static readonly StorePropertyDefinition SharingSyncMultiplier = InternalSchema.SharingSyncMultiplier;

		// Token: 0x04004026 RID: 16422
		public static readonly StorePropertyDefinition SharingMaxObjectsInSync = InternalSchema.SharingMaxObjectsInSync;

		// Token: 0x04004027 RID: 16423
		public static readonly StorePropertyDefinition SharingMaxNumberOfEmails = InternalSchema.SharingMaxNumberOfEmails;

		// Token: 0x04004028 RID: 16424
		public static readonly StorePropertyDefinition SharingMaxNumberOfFolders = InternalSchema.SharingMaxNumberOfFolders;

		// Token: 0x04004029 RID: 16425
		public static readonly StorePropertyDefinition SharingMaxAttachments = InternalSchema.SharingMaxAttachments;

		// Token: 0x0400402A RID: 16426
		public static readonly StorePropertyDefinition SharingMaxMessageSize = InternalSchema.SharingMaxMessageSize;

		// Token: 0x0400402B RID: 16427
		public static readonly StorePropertyDefinition SharingMaxRecipients = InternalSchema.SharingMaxRecipients;

		// Token: 0x0400402C RID: 16428
		public static readonly StorePropertyDefinition SharingMigrationState = InternalSchema.SharingMigrationState;

		// Token: 0x0400402D RID: 16429
		public static readonly StorePropertyDefinition SharingAggregationType = InternalSchema.SharingAggregationType;

		// Token: 0x0400402E RID: 16430
		public static readonly StorePropertyDefinition SharingPoisonCallstack = InternalSchema.SharingPoisonCallstack;

		// Token: 0x0400402F RID: 16431
		public static readonly StorePropertyDefinition SharingSubscriptionConfiguration = InternalSchema.SharingSubscriptionConfiguration;

		// Token: 0x04004030 RID: 16432
		public static readonly StorePropertyDefinition SharingAggregationProtocolVersion = InternalSchema.SharingAggregationProtocolVersion;

		// Token: 0x04004031 RID: 16433
		public static readonly StorePropertyDefinition SharingAggregationProtocolName = InternalSchema.SharingAggregationProtocolName;

		// Token: 0x04004032 RID: 16434
		public static readonly StorePropertyDefinition SharingSubscriptionName = InternalSchema.SharingSubscriptionName;

		// Token: 0x04004033 RID: 16435
		public static readonly StorePropertyDefinition SharingSubscriptionsCache = InternalSchema.SharingSubscriptionsCache;

		// Token: 0x04004034 RID: 16436
		public static readonly StorePropertyDefinition SharingSubscriptionCreationType = InternalSchema.SharingSubscriptionCreationType;

		// Token: 0x04004035 RID: 16437
		public static readonly StorePropertyDefinition SharingSubscriptionSyncPhase = InternalSchema.SharingSubscriptionSyncPhase;

		// Token: 0x04004036 RID: 16438
		public static readonly StorePropertyDefinition SharingSubscriptionExclusionFolders = InternalSchema.SharingSubscriptionExclusionFolders;

		// Token: 0x04004037 RID: 16439
		public static readonly StorePropertyDefinition SharingSendAsVerificationEmailState = InternalSchema.SharingSendAsVerificationEmailState;

		// Token: 0x04004038 RID: 16440
		public static readonly StorePropertyDefinition SharingSendAsVerificationMessageId = InternalSchema.SharingSendAsVerificationMessageId;

		// Token: 0x04004039 RID: 16441
		public static readonly StorePropertyDefinition SharingSendAsVerificationTimestamp = InternalSchema.SharingSendAsVerificationTimestamp;

		// Token: 0x0400403A RID: 16442
		public static readonly StorePropertyDefinition SharingSubscriptionEvents = InternalSchema.SharingSubscriptionEvents;

		// Token: 0x0400403B RID: 16443
		public static readonly StorePropertyDefinition SharingSubscriptionItemsSynced = InternalSchema.SharingSubscriptionItemsSynced;

		// Token: 0x0400403C RID: 16444
		public static readonly StorePropertyDefinition SharingSubscriptionItemsSkipped = InternalSchema.SharingSubscriptionItemsSkipped;

		// Token: 0x0400403D RID: 16445
		public static readonly StorePropertyDefinition SharingSubscriptionTotalItemsInSourceMailbox = InternalSchema.SharingSubscriptionTotalItemsInSourceMailbox;

		// Token: 0x0400403E RID: 16446
		public static readonly StorePropertyDefinition SharingSubscriptionTotalSizeOfSourceMailbox = InternalSchema.SharingSubscriptionTotalSizeOfSourceMailbox;

		// Token: 0x0400403F RID: 16447
		public static readonly StorePropertyDefinition SharingImapPathPrefix = InternalSchema.SharingImapPathPrefix;

		// Token: 0x04004040 RID: 16448
		public static readonly StorePropertyDefinition SharingAdjustedLastSuccessfulSyncTime = InternalSchema.SharingAdjustedLastSuccessfulSyncTime;

		// Token: 0x04004041 RID: 16449
		public static readonly StorePropertyDefinition SharingLastSyncNowRequest = InternalSchema.SharingLastSyncNowRequest;

		// Token: 0x04004042 RID: 16450
		public static readonly StorePropertyDefinition SharingOutageDetectionDiagnostics = InternalSchema.SharingOutageDetectionDiagnostics;

		// Token: 0x04004043 RID: 16451
		public static readonly StorePropertyDefinition SharingEncryptedAccessToken = InternalSchema.SharingEncryptedAccessToken;

		// Token: 0x04004044 RID: 16452
		public static readonly StorePropertyDefinition SharingAppId = InternalSchema.SharingAppId;

		// Token: 0x04004045 RID: 16453
		public static readonly StorePropertyDefinition SharingUserId = InternalSchema.SharingUserId;

		// Token: 0x04004046 RID: 16454
		public static readonly StorePropertyDefinition SharingEncryptedAccessTokenSecret = InternalSchema.SharingEncryptedAccessTokenSecret;

		// Token: 0x04004047 RID: 16455
		public static readonly StorePropertyDefinition SharingInitialSyncInRecoveryMode = InternalSchema.SharingInitialSyncInRecoveryMode;

		// Token: 0x04004048 RID: 16456
		private static AggregationSubscriptionMessageSchema instance = null;
	}
}
