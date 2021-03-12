using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000AE RID: 174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncWorkerData
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000437 RID: 1079
		bool IsValid { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000438 RID: 1080
		bool IsMirrored { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000439 RID: 1081
		bool IsMigration { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600043A RID: 1082
		bool IsPartnerProtocol { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600043B RID: 1083
		Guid SubscriptionGuid { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600043C RID: 1084
		AggregationSubscriptionType SubscriptionType { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600043D RID: 1085
		AggregationType AggregationType { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600043E RID: 1086
		string IncomingServerName { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600043F RID: 1087
		int IncomingServerPort { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000440 RID: 1088
		bool InitialSyncInRecoveryMode { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000441 RID: 1089
		string Domain { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000442 RID: 1090
		string Name { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000443 RID: 1091
		int? EnumeratedItemsLimitPerConnection { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000444 RID: 1092
		bool Inactive { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000445 RID: 1093
		DateTime CreationTime { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000446 RID: 1094
		// (set) Token: 0x06000447 RID: 1095
		string UserExchangeMailboxSmtpAddress { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000448 RID: 1096
		string UserLegacyDN { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000449 RID: 1097
		// (set) Token: 0x0600044A RID: 1098
		StoreObjectId SubscriptionMessageId { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600044B RID: 1099
		FolderSupport FolderSupport { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600044C RID: 1100
		ItemSupport ItemSupport { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600044D RID: 1101
		SyncQuirks SyncQuirks { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600044E RID: 1102
		// (set) Token: 0x0600044F RID: 1103
		SyncPhase SyncPhase { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000450 RID: 1104
		// (set) Token: 0x06000451 RID: 1105
		AggregationStatus Status { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000452 RID: 1106
		// (set) Token: 0x06000453 RID: 1107
		DetailedAggregationStatus DetailedAggregationStatus { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000454 RID: 1108
		// (set) Token: 0x06000455 RID: 1109
		DateTime? LastSyncTime { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000456 RID: 1110
		// (set) Token: 0x06000457 RID: 1111
		DateTime? LastSuccessfulSyncTime { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000458 RID: 1112
		// (set) Token: 0x06000459 RID: 1113
		DateTime AdjustedLastSuccessfulSyncTime { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600045A RID: 1114
		bool IsInitialSyncDone { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600045B RID: 1115
		bool WasInitialSyncDone { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600045C RID: 1116
		long ItemsSynced { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600045D RID: 1117
		long ItemsSkipped { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600045E RID: 1118
		// (set) Token: 0x0600045F RID: 1119
		DateTime? LastSyncNowRequestTime { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000460 RID: 1120
		// (set) Token: 0x06000461 RID: 1121
		string Diagnostics { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000462 RID: 1122
		// (set) Token: 0x06000463 RID: 1123
		string OutageDetectionDiagnostics { get; set; }

		// Token: 0x17000139 RID: 313
		// (set) Token: 0x06000464 RID: 1124
		string PoisonCallstack { set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000465 RID: 1125
		// (set) Token: 0x06000466 RID: 1126
		long? TotalItemsInSourceMailbox { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000467 RID: 1127
		// (set) Token: 0x06000468 RID: 1128
		long? TotalSizeOfSourceMailbox { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000469 RID: 1129
		AggregationSubscriptionIdentity SubscriptionIdentity { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600046A RID: 1130
		// (set) Token: 0x0600046B RID: 1131
		string UserExchangeMailboxDisplayName { get; set; }

		// Token: 0x0600046C RID: 1132
		void UpdateItemStatistics(long itemsSynced, long itemsSkipped);

		// Token: 0x0600046D RID: 1133
		bool ShouldFolderBeExcluded(string folderName, char folderSeparator);

		// Token: 0x0600046E RID: 1134
		void AppendOutageDetectionDiagnostics(string machineName, Guid databaseGuid, TimeSpan configuredOutageDetectionThreshold, TimeSpan observedOutageDuration);

		// Token: 0x0600046F RID: 1135
		void SetToMessageObject(MessageItem message);
	}
}
