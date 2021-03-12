using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200005E RID: 94
	internal static class MigrationJobItemLog
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x0001C5E2 File Offset: 0x0001A7E2
		public static void LogStatusEvent(MigrationJobItem migrationObject)
		{
			if (!MigrationJobItemLog.jobItemStatusForLogging.Contains(migrationObject.Status))
			{
				return;
			}
			MigrationJobItemLog.MigrationJobItemLogger.LogEvent(migrationObject);
		}

		// Token: 0x040001F8 RID: 504
		private const int MaxInternalErrorSize = 8192;

		// Token: 0x040001F9 RID: 505
		private static HashSet<MigrationUserStatus> jobItemStatusForLogging = new HashSet<MigrationUserStatus>(new MigrationUserStatus[]
		{
			MigrationUserStatus.Queued,
			MigrationUserStatus.Syncing,
			MigrationUserStatus.Failed,
			MigrationUserStatus.Synced,
			MigrationUserStatus.IncrementalSyncing,
			MigrationUserStatus.IncrementalFailed,
			MigrationUserStatus.Completing,
			MigrationUserStatus.Completed,
			MigrationUserStatus.CompletionFailed,
			MigrationUserStatus.CompletedWithWarnings
		});

		// Token: 0x02000060 RID: 96
		private class MigrationJobItemLogger : MigrationObjectLog<MigrationJobItem, MigrationJobItemLog.MigrationJobItemLogSchema, MigrationJobItemLog.MigrationJobItemLogConfiguration>
		{
			// Token: 0x060005FE RID: 1534 RVA: 0x0001C67C File Offset: 0x0001A87C
			public static void LogEvent(MigrationJobItem migrationObject)
			{
				MigrationObjectLog<MigrationJobItem, MigrationJobItemLog.MigrationJobItemLogSchema, MigrationJobItemLog.MigrationJobItemLogConfiguration>.Write(migrationObject);
			}
		}

		// Token: 0x02000061 RID: 97
		private class MigrationJobItemLogSchema : ObjectLogSchema
		{
			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001C68C File Offset: 0x0001A88C
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Migration";
				}
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001C693 File Offset: 0x0001A893
			public override string LogType
			{
				get
				{
					return "Migration JobItem";
				}
			}

			// Token: 0x040001FB RID: 507
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> JobItemGuid = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("JobItemGuid", (MigrationJobItem d) => d.JobItemGuid);

			// Token: 0x040001FC RID: 508
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> MigrationJobId = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("MigrationJobId", (MigrationJobItem d) => d.MigrationJobId);

			// Token: 0x040001FD RID: 509
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> ObjectVersion = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("ObjectVersion", (MigrationJobItem d) => d.Version);

			// Token: 0x040001FE RID: 510
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> Status = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("Status", (MigrationJobItem d) => (d.Status == MigrationUserStatus.Synced && d.IncrementalSyncDuration != null) ? MigrationUserStatus.IncrementalSynced : d.Status);

			// Token: 0x040001FF RID: 511
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> ItemsSynced = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("ItemsSynced", (MigrationJobItem d) => d.ItemsSynced);

			// Token: 0x04000200 RID: 512
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> ItemsSkipped = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("ItemsSkipped", (MigrationJobItem d) => d.ItemsSkipped);

			// Token: 0x04000201 RID: 513
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> LocalizedError = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("LocalizedError", (MigrationJobItem d) => d.StatusData.LocalizedError);

			// Token: 0x04000202 RID: 514
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> InternalError = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("InternalError", delegate(MigrationJobItem d)
			{
				if (d.StatusData.InternalError == null)
				{
					return null;
				}
				if (d.StatusData.InternalError.Length <= 8192)
				{
					return d.StatusData.InternalError;
				}
				return d.StatusData.InternalError.Remove(8192);
			});

			// Token: 0x04000203 RID: 515
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> Organization = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("TenantName", (MigrationJobItem d) => d.TenantName);

			// Token: 0x04000204 RID: 516
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> JobName = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("JobName", (MigrationJobItem d) => d.JobName);

			// Token: 0x04000205 RID: 517
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> MigrationType = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("MigrationType", (MigrationJobItem d) => MigrationJobLog.GetMigrationTypeString(d.IsStaged, d.MigrationType));

			// Token: 0x04000206 RID: 518
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> SubscriptionId = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("SubscriptionId", (MigrationJobItem d) => d.SubscriptionId);

			// Token: 0x04000207 RID: 519
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> WatsonHash = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("WatsonHash", (MigrationJobItem d) => d.StatusData.WatsonHash);

			// Token: 0x04000208 RID: 520
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> OverallCmdletDuration = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("OverallCmdletDuration", (MigrationJobItem d) => (d.OverallCmdletDuration == null) ? null : new long?((long)d.OverallCmdletDuration.Value.TotalMilliseconds));

			// Token: 0x04000209 RID: 521
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> SubscriptionInjectionDuration = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("SubscriptionInjectionDuration", (MigrationJobItem d) => (d.SubscriptionInjectionDuration == null) ? null : new long?((long)d.SubscriptionInjectionDuration.Value.TotalMilliseconds));

			// Token: 0x0400020A RID: 522
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> ProvisioningDuration = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("ProvisioningDuration", (MigrationJobItem d) => (d.ProvisioningDuration == null) ? null : new long?((long)d.ProvisioningDuration.Value.TotalMilliseconds));

			// Token: 0x0400020B RID: 523
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> ProvisionedTime = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("ProvisionedTime", (MigrationJobItem d) => d.ProvisionedTime);

			// Token: 0x0400020C RID: 524
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJobItem> SubscriptionQueuedTime = new ObjectLogSimplePropertyDefinition<MigrationJobItem>("SubscriptionQueuedTime", (MigrationJobItem d) => d.SubscriptionQueuedTime);
		}

		// Token: 0x02000063 RID: 99
		private class MigrationJobItemLogConfiguration : MigrationObjectLogConfiguration
		{
			// Token: 0x170001FE RID: 510
			// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001CC09 File Offset: 0x0001AE09
			public override string LoggingFolder
			{
				get
				{
					return base.LoggingFolder + "\\MigrationJobItem";
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001CC1B File Offset: 0x0001AE1B
			public override long MaxLogDirSize
			{
				get
				{
					return ConfigBase<MigrationServiceConfigSchema>.GetConfig<long>("MigrationReportingJobItemMaxDirSize");
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001CC27 File Offset: 0x0001AE27
			public override long MaxLogFileSize
			{
				get
				{
					return ConfigBase<MigrationServiceConfigSchema>.GetConfig<long>("MigrationReportingJobItemMaxFileSize");
				}
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001CC33 File Offset: 0x0001AE33
			public override string LogComponentName
			{
				get
				{
					return "MigrationJobItemLog";
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001CC3A File Offset: 0x0001AE3A
			public override string FilenamePrefix
			{
				get
				{
					return "MigrationJobItem_Log_";
				}
			}
		}
	}
}
