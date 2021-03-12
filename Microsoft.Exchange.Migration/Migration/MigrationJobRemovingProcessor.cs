using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000118 RID: 280
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobRemovingProcessor : JobProcessor
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x0003D720 File Offset: 0x0003B920
		internal static MigrationJobRemovingProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (type != MigrationType.IMAP && type != MigrationType.ExchangeOutlookAnywhere)
				{
					goto IL_28;
				}
			}
			else if (type != MigrationType.ExchangeRemoteMove && type != MigrationType.ExchangeLocalMove && type != MigrationType.PublicFolder)
			{
				goto IL_28;
			}
			return new MigrationJobRemovingProcessor();
			IL_28:
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003D76C File Offset: 0x0003B96C
		internal static void RemoveJobItemSubscription(MigrationJobItem item, ILegacySubscriptionHandler handler)
		{
			MigrationUtil.AssertOrThrow(handler != null, "Cannot remove a subscription without a valid subscription handler.", new object[0]);
			MigrationLogger.Log(MigrationEventType.Verbose, "deleting subscriptions for item {0}", new object[]
			{
				item
			});
			handler.DeleteUnderlyingSubscriptions(item);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003D7AE File Offset: 0x0003B9AE
		internal override bool Validate()
		{
			return base.Job.Status == MigrationJobStatus.Removing;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003D7BF File Offset: 0x0003B9BF
		internal override MigrationJobStatus GetNextStageStatus()
		{
			return MigrationJobStatus.Removed;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003D860 File Offset: 0x0003BA60
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			LegacyMigrationJobProcessorResponse response = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			response.NumItemsProcessed = new int?(0);
			response.NumItemsOutstanding = new int?(0);
			IEnumerable<MigrationJobItem> itemsNotInStatus = MigrationJobItem.GetItemsNotInStatus(base.DataProvider, base.Job, MigrationUserStatus.Corrupted, ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationCancellationBatchSize"));
			foreach (MigrationJobItem migrationJobItem in itemsNotInStatus)
			{
				if (response.Result == MigrationProcessorResult.Completed)
				{
					response.Result = MigrationProcessorResult.Working;
				}
				MigrationJobItem item = migrationJobItem;
				MigrationProcessorResult migrationProcessorResult = ItemStateTransitionHelper.RunJobItemOperation(base.Job, migrationJobItem, base.DataProvider, MigrationUserStatus.Corrupted, delegate
				{
					ILegacySubscriptionHandler subscriptionHandler = this.SubscriptionHandler;
					IMigrationDataProvider dataProvider = this.DataProvider;
					if (subscriptionHandler != null)
					{
						MigrationJobRemovingProcessor.RemoveJobItemSubscription(item, subscriptionHandler);
					}
					item.Delete(dataProvider);
					response.NumItemsProcessed++;
				});
				if (migrationProcessorResult == MigrationProcessorResult.Waiting)
				{
					response.Result = MigrationProcessorResult.Waiting;
				}
			}
			int jobItemCount = base.GetJobItemCount(new MigrationUserStatus[]
			{
				MigrationUserStatus.Corrupted
			});
			response.DebugInfo = string.Format("Corrupt:{0}", jobItemCount);
			if (response.Result != MigrationProcessorResult.Completed)
			{
				int jobItemCount2 = base.GetJobItemCount(new MigrationUserStatus[0]);
				response.NumItemsOutstanding = new int?(jobItemCount2 - jobItemCount - response.NumItemsProcessed.Value);
				return response;
			}
			if (jobItemCount > 0)
			{
				MigrationLogger.Log(MigrationEventType.Error, "Job {0} skipping deleting because {1} corrupted items were found", new object[]
				{
					base.Job,
					jobItemCount
				});
				IEnumerable<MigrationJobItem> jobItemsByStatus = base.GetJobItemsByStatus(MigrationUserStatus.Corrupted, null);
				List<StoreObjectId> list = new List<StoreObjectId>(from item in jobItemsByStatus
				select item.StoreObjectId);
				base.DataProvider.MoveMessageItems(list.ToArray(), MigrationFolderName.CorruptedItems);
			}
			if (base.Session.Config.IsSupported(MigrationFeature.MultiBatch))
			{
				using (IMigrationDataProvider providerForFolder = base.DataProvider.GetProviderForFolder(MigrationFolderName.SyncMigrationReports))
				{
					foreach (MigrationReportItem migrationReportItem in MigrationReportItem.GetByJobId(providerForFolder, new Guid?(base.Job.JobId), ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("SyncMigrationCancellationBatchSize")))
					{
						if (response.Result == MigrationProcessorResult.Completed)
						{
							response.Result = MigrationProcessorResult.Working;
						}
						MigrationLogger.Log(MigrationEventType.Information, "Removing report {0}", new object[]
						{
							migrationReportItem.ReportName
						});
						migrationReportItem.Delete(providerForFolder);
					}
				}
			}
			if (response.Result == MigrationProcessorResult.Completed)
			{
				response.Result = MigrationProcessorResult.Deleted;
			}
			return response;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003DB74 File Offset: 0x0003BD74
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobRemovingProcessor>(this);
		}
	}
}
