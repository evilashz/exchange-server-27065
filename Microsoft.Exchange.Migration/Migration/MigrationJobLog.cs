using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200006D RID: 109
	internal static class MigrationJobLog
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0001D31A File Offset: 0x0001B51A
		public static void LogStatusEvent(MigrationJob migrationObject)
		{
			MigrationJobLog.MigrationJobLogger.LogEvent(migrationObject);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001D322 File Offset: 0x0001B522
		internal static string GetMigrationTypeString(bool isStaged, MigrationType migrationType)
		{
			if (migrationType != MigrationType.ExchangeOutlookAnywhere || !isStaged)
			{
				return migrationType.ToString();
			}
			return "StagedExchangeOutlookAnywhere";
		}

		// Token: 0x04000249 RID: 585
		private const int MaxInternalErrorSize = 8192;

		// Token: 0x0200006E RID: 110
		private class MigrationJobLogger : MigrationObjectLog<MigrationJob, MigrationJobLog.MigrationJobLogSchema, MigrationJobLog.MigrationJobLogConfiguration>
		{
			// Token: 0x0600065A RID: 1626 RVA: 0x0001D33C File Offset: 0x0001B53C
			public static void LogEvent(MigrationJob migrationObject)
			{
				MigrationObjectLog<MigrationJob, MigrationJobLog.MigrationJobLogSchema, MigrationJobLog.MigrationJobLogConfiguration>.Write(migrationObject);
			}
		}

		// Token: 0x0200006F RID: 111
		private class MigrationJobLogSchema : ObjectLogSchema
		{
			// Token: 0x17000219 RID: 537
			// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001D34C File Offset: 0x0001B54C
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Migration";
				}
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001D353 File Offset: 0x0001B553
			public override string LogType
			{
				get
				{
					return "Migration Job";
				}
			}

			// Token: 0x0400024A RID: 586
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> JobId = new ObjectLogSimplePropertyDefinition<MigrationJob>("JobId", (MigrationJob d) => d.JobId);

			// Token: 0x0400024B RID: 587
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> TenantName = new ObjectLogSimplePropertyDefinition<MigrationJob>("TenantName", (MigrationJob d) => d.TenantName);

			// Token: 0x0400024C RID: 588
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> MigrationType = new ObjectLogSimplePropertyDefinition<MigrationJob>("MigrationType", (MigrationJob d) => MigrationJobLog.GetMigrationTypeString(d.IsStaged, d.MigrationType));

			// Token: 0x0400024D RID: 589
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> ObjectVersion = new ObjectLogSimplePropertyDefinition<MigrationJob>("ObjectVersion", (MigrationJob d) => d.Version);

			// Token: 0x0400024E RID: 590
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> Status = new ObjectLogSimplePropertyDefinition<MigrationJob>("Status", (MigrationJob d) => d.Status);

			// Token: 0x0400024F RID: 591
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> JobName = new ObjectLogSimplePropertyDefinition<MigrationJob>("JobName", (MigrationJob d) => d.JobName);

			// Token: 0x04000250 RID: 592
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> LocalizedError = new ObjectLogSimplePropertyDefinition<MigrationJob>("LocalizedError", (MigrationJob d) => d.StatusData.LocalizedError);

			// Token: 0x04000251 RID: 593
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> InternalError = new ObjectLogSimplePropertyDefinition<MigrationJob>("InternalError", delegate(MigrationJob d)
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

			// Token: 0x04000252 RID: 594
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> WatsonHash = new ObjectLogSimplePropertyDefinition<MigrationJob>("WatsonHash", (MigrationJob d) => d.StatusData.WatsonHash);

			// Token: 0x04000253 RID: 595
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> Direction = new ObjectLogSimplePropertyDefinition<MigrationJob>("Direction", (MigrationJob d) => d.JobDirection);

			// Token: 0x04000254 RID: 596
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> CreationTime = new ObjectLogSimplePropertyDefinition<MigrationJob>("CreationTime", (MigrationJob d) => d.OriginalCreationTime);

			// Token: 0x04000255 RID: 597
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> StartTime = new ObjectLogSimplePropertyDefinition<MigrationJob>("StartTime", (MigrationJob d) => d.StartTime);

			// Token: 0x04000256 RID: 598
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> InitialSyncTime = new ObjectLogSimplePropertyDefinition<MigrationJob>("InitialSyncTime", (MigrationJob d) => d.InitialSyncDateTime);

			// Token: 0x04000257 RID: 599
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> FinalizedTime = new ObjectLogSimplePropertyDefinition<MigrationJob>("FinalizedTime", (MigrationJob d) => d.FinalizeTime);

			// Token: 0x04000258 RID: 600
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> LastSyncTime = new ObjectLogSimplePropertyDefinition<MigrationJob>("LastSyncTime", (MigrationJob d) => d.LastSyncTime);

			// Token: 0x04000259 RID: 601
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> StartAfterTime = new ObjectLogSimplePropertyDefinition<MigrationJob>("StartAfterTime", delegate(MigrationJob d)
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = d.SubscriptionSettings as MoveJobSubscriptionSettings;
				ExchangeJobSubscriptionSettings exchangeJobSubscriptionSettings = d.SubscriptionSettings as ExchangeJobSubscriptionSettings;
				return (moveJobSubscriptionSettings != null) ? moveJobSubscriptionSettings.StartAfter : ((exchangeJobSubscriptionSettings != null) ? exchangeJobSubscriptionSettings.StartAfter : null);
			});

			// Token: 0x0400025A RID: 602
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> CompleteAfterTime = new ObjectLogSimplePropertyDefinition<MigrationJob>("CompleteAfter", delegate(MigrationJob d)
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = d.SubscriptionSettings as MoveJobSubscriptionSettings;
				return (moveJobSubscriptionSettings != null) ? moveJobSubscriptionSettings.CompleteAfter : null;
			});

			// Token: 0x0400025B RID: 603
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> InitialSyncDuration = new ObjectLogSimplePropertyDefinition<MigrationJob>("InitialSyncDuration", (MigrationJob d) => 0);

			// Token: 0x0400025C RID: 604
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> TotalCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("TotalCount", (MigrationJob d) => d.TotalCount);

			// Token: 0x0400025D RID: 605
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> ActiveCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("ActiveCount", (MigrationJob d) => d.ActiveItemCount);

			// Token: 0x0400025E RID: 606
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> StoppedCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("StoppedCount", (MigrationJob d) => d.StoppedItemCount);

			// Token: 0x0400025F RID: 607
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> SyncedCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("SyncedCount", (MigrationJob d) => d.SyncedItemCount);

			// Token: 0x04000260 RID: 608
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> FinalizedCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("FinalizedCount", (MigrationJob d) => d.FinalizedItemCount);

			// Token: 0x04000261 RID: 609
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> FailedCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("FailedCount", (MigrationJob d) => d.FailedItemCount);

			// Token: 0x04000262 RID: 610
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> PendingCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("PendingCount", (MigrationJob d) => d.PendingCount);

			// Token: 0x04000263 RID: 611
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> ProvisionedCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("ProvisionedCount", (MigrationJob d) => d.ProvisionedItemCount);

			// Token: 0x04000264 RID: 612
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> ValidationWarningCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("ValidationWarningCount", (MigrationJob d) => d.ValidationWarningCount);

			// Token: 0x04000265 RID: 613
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> Locale = new ObjectLogSimplePropertyDefinition<MigrationJob>("Locale", (MigrationJob d) => d.AdminCulture);

			// Token: 0x04000266 RID: 614
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> BatchFlags = new ObjectLogSimplePropertyDefinition<MigrationJob>("BatchFlags", (MigrationJob d) => d.BatchFlags);

			// Token: 0x04000267 RID: 615
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> AutoRetryCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("AutoRetryCount", (MigrationJob d) => d.MaxAutoRunCount);

			// Token: 0x04000268 RID: 616
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> CurrentRetryCount = new ObjectLogSimplePropertyDefinition<MigrationJob>("CurrentRetryCount", (MigrationJob d) => d.AutoRunCount);

			// Token: 0x04000269 RID: 617
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> BadItemLimit = new ObjectLogSimplePropertyDefinition<MigrationJob>("BadItemLimit", delegate(MigrationJob d)
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = d.SubscriptionSettings as MoveJobSubscriptionSettings;
				return (moveJobSubscriptionSettings != null) ? moveJobSubscriptionSettings.BadItemLimit : null;
			});

			// Token: 0x0400026A RID: 618
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> LargeItemLimit = new ObjectLogSimplePropertyDefinition<MigrationJob>("LargeItemLimit", delegate(MigrationJob d)
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = d.SubscriptionSettings as MoveJobSubscriptionSettings;
				return (moveJobSubscriptionSettings != null) ? moveJobSubscriptionSettings.LargeItemLimit : null;
			});

			// Token: 0x0400026B RID: 619
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> PrimaryOnly = new ObjectLogSimplePropertyDefinition<MigrationJob>("PrimaryOnly", delegate(MigrationJob d)
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = d.SubscriptionSettings as MoveJobSubscriptionSettings;
				return (moveJobSubscriptionSettings != null) ? moveJobSubscriptionSettings.PrimaryOnly : null;
			});

			// Token: 0x0400026C RID: 620
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> ArchiveOnly = new ObjectLogSimplePropertyDefinition<MigrationJob>("ArchiveOnly", delegate(MigrationJob d)
			{
				MoveJobSubscriptionSettings moveJobSubscriptionSettings = d.SubscriptionSettings as MoveJobSubscriptionSettings;
				return (moveJobSubscriptionSettings != null) ? moveJobSubscriptionSettings.ArchiveOnly : null;
			});

			// Token: 0x0400026D RID: 621
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> TargetDeliveryDomain = new ObjectLogSimplePropertyDefinition<MigrationJob>("TargetDeliveryDomain", (MigrationJob d) => d.TargetDomainName);

			// Token: 0x0400026E RID: 622
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> SkipSteps = new ObjectLogSimplePropertyDefinition<MigrationJob>("SkipSteps", (MigrationJob d) => d.SkipSteps);

			// Token: 0x0400026F RID: 623
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> SourceEndpointGuid = new ObjectLogSimplePropertyDefinition<MigrationJob>("SourceEndpointGuid", (MigrationJob d) => (d.SourceEndpoint == null) ? null : new Guid?(d.SourceEndpoint.Guid));

			// Token: 0x04000270 RID: 624
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> TargetEndpointGuid = new ObjectLogSimplePropertyDefinition<MigrationJob>("TargetEndpointGuid", (MigrationJob d) => (d.TargetEndpoint == null) ? null : new Guid?(d.TargetEndpoint.Guid));

			// Token: 0x04000271 RID: 625
			public static readonly ObjectLogSimplePropertyDefinition<MigrationJob> ProcessingDuration = new ObjectLogSimplePropertyDefinition<MigrationJob>("ProcessingDuration", (MigrationJob d) => d.ProcessingDuration.TotalMilliseconds);
		}

		// Token: 0x02000070 RID: 112
		private class MigrationJobLogConfiguration : MigrationObjectLogConfiguration
		{
			// Token: 0x1700021B RID: 539
			// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001DDFD File Offset: 0x0001BFFD
			public override string LoggingFolder
			{
				get
				{
					return base.LoggingFolder + "\\MigrationJob";
				}
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001DE0F File Offset: 0x0001C00F
			public override long MaxLogDirSize
			{
				get
				{
					return ConfigBase<MigrationServiceConfigSchema>.GetConfig<long>("MigrationReportingJobMaxDirSize");
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001DE1B File Offset: 0x0001C01B
			public override long MaxLogFileSize
			{
				get
				{
					return ConfigBase<MigrationServiceConfigSchema>.GetConfig<long>("MigrationReportingJobMaxFileSize");
				}
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001DE27 File Offset: 0x0001C027
			public override string LogComponentName
			{
				get
				{
					return "MigrationJobLog";
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001DE2E File Offset: 0x0001C02E
			public override string FilenamePrefix
			{
				get
				{
					return "MigrationJob_Log_";
				}
			}
		}
	}
}
