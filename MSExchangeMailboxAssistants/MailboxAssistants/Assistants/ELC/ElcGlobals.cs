using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000041 RID: 65
	internal static class ElcGlobals
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000ED55 File Offset: 0x0000CF55
		internal static bool ExpireDumpsterRightNow
		{
			get
			{
				if (ElcGlobals.expireDumpsterRightNow == null)
				{
					object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.ExpireDumpsterRightNowName);
					if (obj != null && obj is string && string.Compare((string)obj, "true", true) == 0)
					{
						ElcGlobals.expireDumpsterRightNow = new bool?(true);
					}
					else
					{
						ElcGlobals.expireDumpsterRightNow = new bool?(false);
					}
				}
				return ElcGlobals.expireDumpsterRightNow.Value;
			}
			set
			{
				ElcGlobals.expireDumpsterRightNow = new bool?(value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000ED62 File Offset: 0x0000CF62
		internal static IConfigProvider Configuration
		{
			get
			{
				return ElcGlobals.configProvider.Value;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000ED6E File Offset: 0x0000CF6E
		internal static void ForceRegistryRead()
		{
			ElcGlobals.expireDumpsterRightNow = null;
		}

		// Token: 0x040001FD RID: 509
		internal const int RetryForStoreDBUnHealthyException = 5;

		// Token: 0x040001FE RID: 510
		internal const int QueryResultBatchSize = 100;

		// Token: 0x040001FF RID: 511
		internal const int ItemsCollectionForProcessingBatchSize = 2000;

		// Token: 0x04000200 RID: 512
		internal const long EhaMigrationMessageCountNotStamped = 0L;

		// Token: 0x04000201 RID: 513
		private static Lazy<IConfigProvider> configProvider = new Lazy<IConfigProvider>(delegate()
		{
			IConfigProvider configProvider = ConfigProvider.CreateProvider(new ElcConfigSchema());
			configProvider.Initialize();
			return configProvider;
		}, true);

		// Token: 0x04000202 RID: 514
		internal static readonly string ParameterRegistryKeyPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange MRM\\Parameters";

		// Token: 0x04000203 RID: 515
		internal static readonly string ExpireDumpsterRightNowName = "ExpireDumpsterRightNow";

		// Token: 0x04000204 RID: 516
		internal static readonly string TestELCAssistantModeName = "TestELCAssistantMode";

		// Token: 0x04000205 RID: 517
		internal static readonly string MaxItemsToMigrateForEHA = "MaxItemsToMigrateForEHA";

		// Token: 0x04000206 RID: 518
		internal static readonly string MigrationFolderName = "MigrationFolder";

		// Token: 0x04000207 RID: 519
		internal static readonly string EhaMigrationMailboxName = "ehamigrationmailbox";

		// Token: 0x04000208 RID: 520
		internal static readonly string ConfirmationMailboxAlias = "ehamsgconfirmation";

		// Token: 0x04000209 RID: 521
		internal static readonly string ELCReportEnabed = "ELCReportEnabled";

		// Token: 0x0400020A RID: 522
		internal static readonly int ReportItemLimit = 500;

		// Token: 0x0400020B RID: 523
		internal static readonly string HoldCleanupEnabledForELC = "HoldCleanupEnabledForELC";

		// Token: 0x0400020C RID: 524
		internal static readonly string HoldCleanupBatchSizeForELC = "HoldCleanupBatchSizeForELC";

		// Token: 0x0400020D RID: 525
		internal static readonly string EHAHiddenFolderCleanupBatchSizeForELC = "EHAHiddenFolderCleanupBatchSizeForELC";

		// Token: 0x0400020E RID: 526
		internal static readonly string HoldCleanupLogOnly = "HoldCleanupLogOnly";

		// Token: 0x0400020F RID: 527
		internal static readonly string UseXtcMoveToArchive = "UseXtcMoveToArchive";

		// Token: 0x04000210 RID: 528
		internal static readonly string MoveToArchiveTotalCountLimit = "MoveToArchiveTotalCountLimit";

		// Token: 0x04000211 RID: 529
		internal static readonly string MoveToArchiveBatchCountLimit = "MoveToArchiveBatchCountLimit";

		// Token: 0x04000212 RID: 530
		internal static readonly string MoveToArchiveBatchSizeLimit = "MoveToArchiveBatchSizeLimit";

		// Token: 0x04000213 RID: 531
		internal static readonly int MaxIntervalBetweenTwoSuccessfulElcRunsInDays = 7;

		// Token: 0x04000214 RID: 532
		internal static readonly int MailboxProcessingTimeFirstLevelThresholdInMin = 5;

		// Token: 0x04000215 RID: 533
		internal static readonly int MailboxProcessingTimeSecondLevelThresholdInMin = 10;

		// Token: 0x04000216 RID: 534
		private static bool? expireDumpsterRightNow = null;

		// Token: 0x02000042 RID: 66
		internal enum ELCReportItemProps
		{
			// Token: 0x04000219 RID: 537
			ConversationIdIndex,
			// Token: 0x0400021A RID: 538
			ConversaionTopicIndex,
			// Token: 0x0400021B RID: 539
			SenderDisplayNameIndex,
			// Token: 0x0400021C RID: 540
			FolderNameIndex,
			// Token: 0x0400021D RID: 541
			RetentionTagIndex,
			// Token: 0x0400021E RID: 542
			AdditionalTagIndex,
			// Token: 0x0400021F RID: 543
			ReceivedTimeIndex,
			// Token: 0x04000220 RID: 544
			ModifiedTimeIndex,
			// Token: 0x04000221 RID: 545
			ConversationItemsIndex
		}

		// Token: 0x02000043 RID: 67
		internal enum ArchiveLocation
		{
			// Token: 0x04000223 RID: 547
			None,
			// Token: 0x04000224 RID: 548
			SameServer,
			// Token: 0x04000225 RID: 549
			CrossServerSameForest,
			// Token: 0x04000226 RID: 550
			Archive = 4,
			// Token: 0x04000227 RID: 551
			ArchiveNotConfigured,
			// Token: 0x04000228 RID: 552
			RemoteArchive
		}
	}
}
