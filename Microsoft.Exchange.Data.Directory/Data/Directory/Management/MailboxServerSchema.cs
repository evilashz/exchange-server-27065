using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000736 RID: 1846
	internal class MailboxServerSchema : ADPresentationSchema
	{
		// Token: 0x06005890 RID: 22672 RVA: 0x0013AC02 File Offset: 0x00138E02
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ActiveDirectoryServerSchema>();
		}

		// Token: 0x04003B9B RID: 15259
		public static readonly ADPropertyDefinition CalendarRepairWorkCycle = ActiveDirectoryServerSchema.CalendarRepairWorkCycle;

		// Token: 0x04003B9C RID: 15260
		public static readonly ADPropertyDefinition CalendarRepairWorkCycleCheckpoint = ActiveDirectoryServerSchema.CalendarRepairWorkCycleCheckpoint;

		// Token: 0x04003B9D RID: 15261
		public static readonly ADPropertyDefinition SharingPolicyWorkCycle = ActiveDirectoryServerSchema.SharingPolicyWorkCycle;

		// Token: 0x04003B9E RID: 15262
		public static readonly ADPropertyDefinition SharingPolicyWorkCycleCheckpoint = ActiveDirectoryServerSchema.SharingPolicyWorkCycleCheckpoint;

		// Token: 0x04003B9F RID: 15263
		public static readonly ADPropertyDefinition SharingSyncWorkCycle = ActiveDirectoryServerSchema.SharingSyncWorkCycle;

		// Token: 0x04003BA0 RID: 15264
		public static readonly ADPropertyDefinition SharingSyncWorkCycleCheckpoint = ActiveDirectoryServerSchema.SharingSyncWorkCycleCheckpoint;

		// Token: 0x04003BA1 RID: 15265
		public static readonly ADPropertyDefinition PublicFolderWorkCycle = ActiveDirectoryServerSchema.PublicFolderWorkCycle;

		// Token: 0x04003BA2 RID: 15266
		public static readonly ADPropertyDefinition PublicFolderWorkCycleCheckpoint = ActiveDirectoryServerSchema.PublicFolderWorkCycleCheckpoint;

		// Token: 0x04003BA3 RID: 15267
		public static readonly ADPropertyDefinition SiteMailboxWorkCycle = ActiveDirectoryServerSchema.SiteMailboxWorkCycle;

		// Token: 0x04003BA4 RID: 15268
		public static readonly ADPropertyDefinition SiteMailboxWorkCycleCheckpoint = ActiveDirectoryServerSchema.SiteMailboxWorkCycleCheckpoint;

		// Token: 0x04003BA5 RID: 15269
		public static readonly ADPropertyDefinition ManagedFolderWorkCycle = ActiveDirectoryServerSchema.ManagedFolderWorkCycle;

		// Token: 0x04003BA6 RID: 15270
		public static readonly ADPropertyDefinition ManagedFolderWorkCycleCheckpoint = ActiveDirectoryServerSchema.ManagedFolderWorkCycleCheckpoint;

		// Token: 0x04003BA7 RID: 15271
		public static readonly ADPropertyDefinition MailboxAssociationReplicationWorkCycle = ActiveDirectoryServerSchema.MailboxAssociationReplicationWorkCycle;

		// Token: 0x04003BA8 RID: 15272
		public static readonly ADPropertyDefinition MailboxAssociationReplicationWorkCycleCheckpoint = ActiveDirectoryServerSchema.MailboxAssociationReplicationWorkCycleCheckpoint;

		// Token: 0x04003BA9 RID: 15273
		public static readonly ADPropertyDefinition GroupMailboxWorkCycle = ActiveDirectoryServerSchema.GroupMailboxWorkCycle;

		// Token: 0x04003BAA RID: 15274
		public static readonly ADPropertyDefinition GroupMailboxWorkCycleCheckpoint = ActiveDirectoryServerSchema.GroupMailboxWorkCycleCheckpoint;

		// Token: 0x04003BAB RID: 15275
		public static readonly ADPropertyDefinition TopNWorkCycle = ActiveDirectoryServerSchema.TopNWorkCycle;

		// Token: 0x04003BAC RID: 15276
		public static readonly ADPropertyDefinition TopNWorkCycleCheckpoint = ActiveDirectoryServerSchema.TopNWorkCycleCheckpoint;

		// Token: 0x04003BAD RID: 15277
		public static readonly ADPropertyDefinition UMReportingWorkCycle = ActiveDirectoryServerSchema.UMReportingWorkCycle;

		// Token: 0x04003BAE RID: 15278
		public static readonly ADPropertyDefinition UMReportingWorkCycleCheckpoint = ActiveDirectoryServerSchema.UMReportingWorkCycleCheckpoint;

		// Token: 0x04003BAF RID: 15279
		public static readonly ADPropertyDefinition InferenceTrainingWorkCycle = ActiveDirectoryServerSchema.InferenceTrainingWorkCycle;

		// Token: 0x04003BB0 RID: 15280
		public static readonly ADPropertyDefinition InferenceTrainingWorkCycleCheckpoint = ActiveDirectoryServerSchema.InferenceTrainingWorkCycleCheckpoint;

		// Token: 0x04003BB1 RID: 15281
		public static readonly ADPropertyDefinition DataPath = ServerSchema.DataPath;

		// Token: 0x04003BB2 RID: 15282
		public static readonly ADPropertyDefinition DirectoryProcessorWorkCycle = ActiveDirectoryServerSchema.DirectoryProcessorWorkCycle;

		// Token: 0x04003BB3 RID: 15283
		public static readonly ADPropertyDefinition DirectoryProcessorWorkCycleCheckpoint = ActiveDirectoryServerSchema.DirectoryProcessorWorkCycleCheckpoint;

		// Token: 0x04003BB4 RID: 15284
		public static readonly ADPropertyDefinition OABGeneratorWorkCycle = ActiveDirectoryServerSchema.OABGeneratorWorkCycle;

		// Token: 0x04003BB5 RID: 15285
		public static readonly ADPropertyDefinition OABGeneratorWorkCycleCheckpoint = ActiveDirectoryServerSchema.OABGeneratorWorkCycleCheckpoint;

		// Token: 0x04003BB6 RID: 15286
		public static readonly ADPropertyDefinition InferenceDataCollectionWorkCycle = ActiveDirectoryServerSchema.InferenceDataCollectionWorkCycle;

		// Token: 0x04003BB7 RID: 15287
		public static readonly ADPropertyDefinition InferenceDataCollectionWorkCycleCheckpoint = ActiveDirectoryServerSchema.InferenceDataCollectionWorkCycleCheckpoint;

		// Token: 0x04003BB8 RID: 15288
		public static readonly ADPropertyDefinition PeopleRelevanceWorkCycle = ActiveDirectoryServerSchema.PeopleRelevanceWorkCycle;

		// Token: 0x04003BB9 RID: 15289
		public static readonly ADPropertyDefinition PeopleRelevanceWorkCycleCheckpoint = ActiveDirectoryServerSchema.PeopleRelevanceWorkCycleCheckpoint;

		// Token: 0x04003BBA RID: 15290
		public static readonly ADPropertyDefinition SharePointSignalStoreWorkCycle = ActiveDirectoryServerSchema.SharePointSignalStoreWorkCycle;

		// Token: 0x04003BBB RID: 15291
		public static readonly ADPropertyDefinition SharePointSignalStoreWorkCycleCheckpoint = ActiveDirectoryServerSchema.SharePointSignalStoreWorkCycleCheckpoint;

		// Token: 0x04003BBC RID: 15292
		public static readonly ADPropertyDefinition PeopleCentricTriageWorkCycle = ActiveDirectoryServerSchema.PeopleCentricTriageWorkCycle;

		// Token: 0x04003BBD RID: 15293
		public static readonly ADPropertyDefinition PeopleCentricTriageWorkCycleCheckpoint = ActiveDirectoryServerSchema.PeopleCentricTriageWorkCycleCheckpoint;

		// Token: 0x04003BBE RID: 15294
		public static readonly ADPropertyDefinition MailboxProcessorWorkCycle = ActiveDirectoryServerSchema.MailboxProcessorWorkCycle;

		// Token: 0x04003BBF RID: 15295
		public static readonly ADPropertyDefinition StoreDsMaintenanceWorkCycle = ActiveDirectoryServerSchema.StoreDsMaintenanceWorkCycle;

		// Token: 0x04003BC0 RID: 15296
		public static readonly ADPropertyDefinition StoreDsMaintenanceWorkCycleCheckpoint = ActiveDirectoryServerSchema.StoreDsMaintenanceWorkCycleCheckpoint;

		// Token: 0x04003BC1 RID: 15297
		public static readonly ADPropertyDefinition StoreIntegrityCheckWorkCycle = ActiveDirectoryServerSchema.StoreIntegrityCheckWorkCycle;

		// Token: 0x04003BC2 RID: 15298
		public static readonly ADPropertyDefinition StoreIntegrityCheckWorkCycleCheckpoint = ActiveDirectoryServerSchema.StoreIntegrityCheckWorkCycleCheckpoint;

		// Token: 0x04003BC3 RID: 15299
		public static readonly ADPropertyDefinition StoreMaintenanceWorkCycle = ActiveDirectoryServerSchema.StoreMaintenanceWorkCycle;

		// Token: 0x04003BC4 RID: 15300
		public static readonly ADPropertyDefinition StoreMaintenanceWorkCycleCheckpoint = ActiveDirectoryServerSchema.StoreMaintenanceWorkCycleCheckpoint;

		// Token: 0x04003BC5 RID: 15301
		public static readonly ADPropertyDefinition StoreScheduledIntegrityCheckWorkCycle = ActiveDirectoryServerSchema.StoreScheduledIntegrityCheckWorkCycle;

		// Token: 0x04003BC6 RID: 15302
		public static readonly ADPropertyDefinition StoreScheduledIntegrityCheckWorkCycleCheckpoint = ActiveDirectoryServerSchema.StoreScheduledIntegrityCheckWorkCycleCheckpoint;

		// Token: 0x04003BC7 RID: 15303
		public static readonly ADPropertyDefinition StoreUrgentMaintenanceWorkCycle = ActiveDirectoryServerSchema.StoreUrgentMaintenanceWorkCycle;

		// Token: 0x04003BC8 RID: 15304
		public static readonly ADPropertyDefinition StoreUrgentMaintenanceWorkCycleCheckpoint = ActiveDirectoryServerSchema.StoreUrgentMaintenanceWorkCycleCheckpoint;

		// Token: 0x04003BC9 RID: 15305
		public static readonly ADPropertyDefinition JunkEmailOptionsCommitterWorkCycle = ActiveDirectoryServerSchema.JunkEmailOptionsCommitterWorkCycle;

		// Token: 0x04003BCA RID: 15306
		public static readonly ADPropertyDefinition ProbeTimeBasedAssistantWorkCycle = ActiveDirectoryServerSchema.ProbeTimeBasedAssistantWorkCycle;

		// Token: 0x04003BCB RID: 15307
		public static readonly ADPropertyDefinition ProbeTimeBasedAssistantWorkCycleCheckpoint = ActiveDirectoryServerSchema.ProbeTimeBasedAssistantWorkCycleCheckpoint;

		// Token: 0x04003BCC RID: 15308
		public static readonly ADPropertyDefinition SearchIndexRepairTimeBasedAssistantWorkCycle = ActiveDirectoryServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycle;

		// Token: 0x04003BCD RID: 15309
		public static readonly ADPropertyDefinition SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint = ActiveDirectoryServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint;

		// Token: 0x04003BCE RID: 15310
		public static readonly ADPropertyDefinition DarTaskStoreTimeBasedAssistantWorkCycle = ActiveDirectoryServerSchema.DarTaskStoreTimeBasedAssistantWorkCycle;

		// Token: 0x04003BCF RID: 15311
		public static readonly ADPropertyDefinition DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint = ActiveDirectoryServerSchema.DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint;

		// Token: 0x04003BD0 RID: 15312
		public static readonly ADPropertyDefinition SharingPolicySchedule = ActiveDirectoryServerSchema.SharingPolicySchedule;

		// Token: 0x04003BD1 RID: 15313
		public static readonly ADPropertyDefinition CalendarRepairMissingItemFixDisabled = ActiveDirectoryServerSchema.CalendarRepairMissingItemFixDisabled;

		// Token: 0x04003BD2 RID: 15314
		public static readonly ADPropertyDefinition CalendarRepairLogEnabled = ActiveDirectoryServerSchema.CalendarRepairLogEnabled;

		// Token: 0x04003BD3 RID: 15315
		public static readonly ADPropertyDefinition CalendarRepairLogSubjectLoggingEnabled = ActiveDirectoryServerSchema.CalendarRepairLogSubjectLoggingEnabled;

		// Token: 0x04003BD4 RID: 15316
		public static readonly ADPropertyDefinition CalendarRepairLogPath = ActiveDirectoryServerSchema.CalendarRepairLogPath;

		// Token: 0x04003BD5 RID: 15317
		public static readonly ADPropertyDefinition CalendarRepairIntervalEndWindow = ActiveDirectoryServerSchema.CalendarRepairIntervalEndWindow;

		// Token: 0x04003BD6 RID: 15318
		public static readonly ADPropertyDefinition CalendarRepairLogFileAgeLimit = ActiveDirectoryServerSchema.CalendarRepairLogFileAgeLimit;

		// Token: 0x04003BD7 RID: 15319
		public static readonly ADPropertyDefinition CalendarRepairLogDirectorySizeLimit = ActiveDirectoryServerSchema.CalendarRepairLogDirectorySizeLimit;

		// Token: 0x04003BD8 RID: 15320
		public static readonly ADPropertyDefinition CalendarRepairMode = ActiveDirectoryServerSchema.CalendarRepairMode;

		// Token: 0x04003BD9 RID: 15321
		public static readonly ADPropertyDefinition ElcSchedule = ServerSchema.ElcSchedule;

		// Token: 0x04003BDA RID: 15322
		public static readonly ADPropertyDefinition ElcAuditLogPath = ActiveDirectoryServerSchema.ElcAuditLogPath;

		// Token: 0x04003BDB RID: 15323
		public static readonly ADPropertyDefinition ElcAuditLogFileAgeLimit = ActiveDirectoryServerSchema.ElcAuditLogFileAgeLimit;

		// Token: 0x04003BDC RID: 15324
		public static readonly ADPropertyDefinition ElcAuditLogDirectorySizeLimit = ActiveDirectoryServerSchema.ElcAuditLogDirectorySizeLimit;

		// Token: 0x04003BDD RID: 15325
		public static readonly ADPropertyDefinition ElcAuditLogFileSizeLimit = ActiveDirectoryServerSchema.ElcAuditLogFileSizeLimit;

		// Token: 0x04003BDE RID: 15326
		public static readonly ADPropertyDefinition MAPIEncryptionRequired = ActiveDirectoryServerSchema.MAPIEncryptionRequired;

		// Token: 0x04003BDF RID: 15327
		public static readonly ADPropertyDefinition ExpirationAuditLogEnabled = ActiveDirectoryServerSchema.ExpirationAuditLogEnabled;

		// Token: 0x04003BE0 RID: 15328
		public static readonly ADPropertyDefinition AutocopyAuditLogEnabled = ActiveDirectoryServerSchema.AutocopyAuditLogEnabled;

		// Token: 0x04003BE1 RID: 15329
		public static readonly ADPropertyDefinition FolderAuditLogEnabled = ActiveDirectoryServerSchema.FolderAuditLogEnabled;

		// Token: 0x04003BE2 RID: 15330
		public static readonly ADPropertyDefinition ElcSubjectLoggingEnabled = ActiveDirectoryServerSchema.ElcSubjectLoggingEnabled;

		// Token: 0x04003BE3 RID: 15331
		public static readonly ADPropertyDefinition SubmissionServerOverrideLIst = ServerSchema.SubmissionServerOverrideList;

		// Token: 0x04003BE4 RID: 15332
		public static readonly ADPropertyDefinition AutoDatabaseMountDialType = ActiveDirectoryServerSchema.AutoDatabaseMountDialType;

		// Token: 0x04003BE5 RID: 15333
		public static readonly ADPropertyDefinition IsPhoneticSupportEnabled = ServerSchema.IsPhoneticSupportEnabled;

		// Token: 0x04003BE6 RID: 15334
		public static readonly ADPropertyDefinition Locale = ServerSchema.Locale;

		// Token: 0x04003BE7 RID: 15335
		public static readonly ADPropertyDefinition MigrationLogLoggingLevel = ActiveDirectoryServerSchema.MigrationLogLoggingLevel;

		// Token: 0x04003BE8 RID: 15336
		public static readonly ADPropertyDefinition MigrationLogFilePath = ActiveDirectoryServerSchema.MigrationLogFilePath;

		// Token: 0x04003BE9 RID: 15337
		public static readonly ADPropertyDefinition MigrationLogMaxAge = ActiveDirectoryServerSchema.MigrationLogMaxAge;

		// Token: 0x04003BEA RID: 15338
		public static readonly ADPropertyDefinition MigrationLogMaxDirectorySize = ActiveDirectoryServerSchema.MigrationLogMaxDirectorySize;

		// Token: 0x04003BEB RID: 15339
		public static readonly ADPropertyDefinition MigrationLogMaxFileSize = ActiveDirectoryServerSchema.MigrationLogMaxFileSize;

		// Token: 0x04003BEC RID: 15340
		public static readonly ADPropertyDefinition DatabaseAvailabilityGroup = ServerSchema.DatabaseAvailabilityGroup;

		// Token: 0x04003BED RID: 15341
		public static readonly ADPropertyDefinition ForceGroupMetricsGeneration = ActiveDirectoryServerSchema.ForceGroupMetricsGeneration;

		// Token: 0x04003BEE RID: 15342
		public static readonly ADPropertyDefinition TransportSyncDispatchEnabled = ServerSchema.TransportSyncDispatchEnabled;

		// Token: 0x04003BEF RID: 15343
		public static readonly ADPropertyDefinition MaxTransportSyncDispatchers = ServerSchema.MaxTransportSyncDispatchers;

		// Token: 0x04003BF0 RID: 15344
		public static readonly ADPropertyDefinition TransportSyncLogEnabled = ServerSchema.TransportSyncMailboxLogEnabled;

		// Token: 0x04003BF1 RID: 15345
		public static readonly ADPropertyDefinition TransportSyncLogLoggingLevel = ServerSchema.TransportSyncMailboxLogLoggingLevel;

		// Token: 0x04003BF2 RID: 15346
		public static readonly ADPropertyDefinition TransportSyncLogFilePath = ServerSchema.TransportSyncMailboxLogFilePath;

		// Token: 0x04003BF3 RID: 15347
		public static readonly ADPropertyDefinition TransportSyncLogMaxAge = ServerSchema.TransportSyncMailboxLogMaxAge;

		// Token: 0x04003BF4 RID: 15348
		public static readonly ADPropertyDefinition TransportSyncLogMaxDirectorySize = ServerSchema.TransportSyncMailboxLogMaxDirectorySize;

		// Token: 0x04003BF5 RID: 15349
		public static readonly ADPropertyDefinition TransportSyncLogMaxFileSize = ServerSchema.TransportSyncMailboxLogMaxFileSize;

		// Token: 0x04003BF6 RID: 15350
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogEnabled = ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogEnabled;

		// Token: 0x04003BF7 RID: 15351
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogFilePath = ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogFilePath;

		// Token: 0x04003BF8 RID: 15352
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogMaxAge = ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxAge;

		// Token: 0x04003BF9 RID: 15353
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogMaxDirectorySize = ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxDirectorySize;

		// Token: 0x04003BFA RID: 15354
		public static readonly ADPropertyDefinition TransportSyncMailboxHealthLogMaxFileSize = ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxFileSize;

		// Token: 0x04003BFB RID: 15355
		public static readonly ADPropertyDefinition DatabaseCopyAutoActivationPolicy = ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy;

		// Token: 0x04003BFC RID: 15356
		public static readonly ADPropertyDefinition DatabaseCopyActivationDisabledAndMoveNow = ActiveDirectoryServerSchema.DatabaseCopyActivationDisabledAndMoveNow;

		// Token: 0x04003BFD RID: 15357
		public static readonly ADPropertyDefinition AutoDagServerConfigured = ActiveDirectoryServerSchema.AutoDagServerConfigured;

		// Token: 0x04003BFE RID: 15358
		public static readonly ADPropertyDefinition FaultZone = ActiveDirectoryServerSchema.FaultZone;

		// Token: 0x04003BFF RID: 15359
		public static readonly ADPropertyDefinition AutoDagFlags = ActiveDirectoryServerSchema.AutoDagFlags;

		// Token: 0x04003C00 RID: 15360
		public static readonly ADPropertyDefinition IsExcludedFromProvisioning = ActiveDirectoryServerSchema.IsExcludedFromProvisioning;

		// Token: 0x04003C01 RID: 15361
		public static readonly ADPropertyDefinition MaxActiveMailboxDatabases = ServerSchema.MaxActiveMailboxDatabases;

		// Token: 0x04003C02 RID: 15362
		public static readonly ADPropertyDefinition MaxPreferredActiveDatabases = ServerSchema.MaxPreferredActiveDatabases;

		// Token: 0x04003C03 RID: 15363
		public static readonly ADPropertyDefinition ComponentStates = ServerSchema.ComponentStates;

		// Token: 0x04003C04 RID: 15364
		public static readonly ADPropertyDefinition AdminDisplayVersion = ServerSchema.AdminDisplayVersion;

		// Token: 0x04003C05 RID: 15365
		public static readonly ADPropertyDefinition CurrentServerRole = ServerSchema.CurrentServerRole;

		// Token: 0x04003C06 RID: 15366
		public static readonly ADPropertyDefinition ExchangeLegacyServerRole = ServerSchema.ExchangeLegacyServerRole;

		// Token: 0x04003C07 RID: 15367
		public static readonly ADPropertyDefinition IsE15OrLater = ServerSchema.IsE15OrLater;
	}
}
