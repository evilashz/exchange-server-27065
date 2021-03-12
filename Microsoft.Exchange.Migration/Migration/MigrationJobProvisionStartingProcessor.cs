using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000117 RID: 279
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class MigrationJobProvisionStartingProcessor : JobProcessor
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003D099 File Offset: 0x0003B299
		protected virtual bool IsProvisioningSupported
		{
			get
			{
				return base.Job.IsProvisioningSupported;
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003D0A8 File Offset: 0x0003B2A8
		internal static MigrationJobProvisionStartingProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (type != MigrationType.IMAP)
				{
					if (type != MigrationType.ExchangeOutlookAnywhere)
					{
						goto IL_38;
					}
					return new ExchangeJobProvisionStartingProcessor();
				}
			}
			else if (type != MigrationType.ExchangeRemoteMove && type != MigrationType.ExchangeLocalMove)
			{
				goto IL_38;
			}
			throw new ArgumentException("MigrationType does not support Provisioning!: " + type);
			IL_38:
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003D102 File Offset: 0x0003B302
		internal override bool Validate()
		{
			return true;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003D105 File Offset: 0x0003B305
		internal override MigrationJobStatus GetNextStageStatus()
		{
			return MigrationJobStatus.SyncStarting;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003D108 File Offset: 0x0003B308
		protected sealed override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			if (scheduleNewWork)
			{
				this.AutoCancelIfTooManyErrors();
			}
			return base.ProcessActions(scheduleNewWork, new Func<bool, LegacyMigrationJobProcessorResponse>[]
			{
				new Func<bool, LegacyMigrationJobProcessorResponse>(this.ProcessPendingJobItems)
			});
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003D13C File Offset: 0x0003B33C
		protected virtual IProvisioningHandler GetProvisioningHandler()
		{
			if (this.IsProvisioningSupported)
			{
				return MigrationApplication.ProvisioningHandler;
			}
			return null;
		}

		// Token: 0x06000E94 RID: 3732
		protected abstract IProvisioningData GetProvisioningData(MigrationJobItem jobItem);

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003D150 File Offset: 0x0003B350
		protected virtual void HandleProvisioningCompletedEvent(ProvisionedObject provisionedObj, MigrationJobItem jobItem)
		{
			if (provisionedObj.Succeeded)
			{
				MigrationUserStatus value = MigrationUserStatus.Queued;
				jobItem.SetUserMailboxProperties(base.DataProvider, new MigrationUserStatus?(value), (MailboxData)provisionedObj.MailboxData, null, new ExDateTime?(ExDateTime.UtcNow));
				return;
			}
			jobItem.SetUserMailboxProperties(base.DataProvider, new MigrationUserStatus?(MigrationUserStatus.Failed), (MailboxData)provisionedObj.MailboxData, new ProvisioningFailedException(new LocalizedString(provisionedObj.Error)), null);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003D1CC File Offset: 0x0003B3CC
		protected virtual void HandleNullIProvisioningDataEvent(MigrationJobItem jobItem)
		{
			jobItem.SetStatusAndSubscriptionLastChecked(base.DataProvider, new MigrationUserStatus?(MigrationUserStatus.Failed), new UserProvisioningInternalException(), null, null);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003D1FC File Offset: 0x0003B3FC
		private LegacyMigrationJobProcessorResponse ProcessPendingJobItems(bool scheduleNewWork)
		{
			if (!this.IsProvisioningSupported)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} no provisioning supported, so all process pending is finished", new object[]
				{
					base.Job
				});
				return LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			}
			int jobItemCount = base.GetJobItemCount(new MigrationUserStatus[]
			{
				MigrationUserStatus.Provisioning
			});
			MigrationUserStatus status = (jobItemCount <= 0) ? MigrationUserStatus.ProvisionUpdating : MigrationUserStatus.Provisioning;
			return this.ProcessProvisioningItems(status, scheduleNewWork);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003D268 File Offset: 0x0003B468
		private MigrationProcessorResult ProcessProvisioningItem(IProvisioningHandler scheduler, MigrationJobItem jobItem, bool scheduleNewWork, ref int scheduledCount)
		{
			if (scheduler.IsItemQueued(jobItem.StoreObjectId))
			{
				if (this.ProcessQueuedItem(scheduler, jobItem))
				{
					return MigrationProcessorResult.Completed;
				}
				return MigrationProcessorResult.Waiting;
			}
			else
			{
				if (!scheduleNewWork)
				{
					return MigrationProcessorResult.Failed;
				}
				if (!scheduler.HasCapacity(base.Job.JobId))
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} doesn't have capacity anymore, will process on next cycle", new object[]
					{
						base.Job
					});
					return MigrationProcessorResult.Failed;
				}
				if (this.QueueItem(scheduler, jobItem))
				{
					scheduledCount++;
					return MigrationProcessorResult.Waiting;
				}
				return MigrationProcessorResult.Completed;
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003D3BC File Offset: 0x0003B5BC
		private LegacyMigrationJobProcessorResponse ProcessProvisioningItems(MigrationUserStatus status, bool scheduleNewWork)
		{
			LegacyMigrationJobProcessorResponse response = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Working, null);
			response.NumItemsProcessed = new int?(0);
			response.NumItemsTransitioned = new int?(0);
			IProvisioningHandler scheduler = this.GetProvisioningHandler();
			this.RegisterJob(scheduler);
			bool foundWork = false;
			int scheduledCount = 0;
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MaxItemsToProvisionInOnePass");
			IEnumerable<MigrationJobItem> itemsByStatus = base.Job.GetItemsByStatus(base.DataProvider, status, null);
			using (IEnumerator<MigrationJobItem> enumerator = itemsByStatus.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MigrationJobItem jobItem = enumerator.Current;
					MigrationProcessorResult migrationProcessorResult = ItemStateTransitionHelper.RunJobItemOperation(base.Job, jobItem, base.DataProvider, MigrationUserStatus.Failed, delegate
					{
						MigrationProcessorResult migrationProcessorResult2 = this.ProcessProvisioningItem(scheduler, jobItem, scheduleNewWork, ref scheduledCount);
						if (migrationProcessorResult2 != MigrationProcessorResult.Failed)
						{
							response.NumItemsProcessed++;
							foundWork = true;
							if (migrationProcessorResult2 == MigrationProcessorResult.Completed)
							{
								response.NumItemsTransitioned++;
							}
						}
					});
					if (migrationProcessorResult == MigrationProcessorResult.Waiting)
					{
						foundWork = true;
					}
					if (migrationProcessorResult == MigrationProcessorResult.Failed)
					{
						response.NumItemsTransitioned++;
					}
					if (scheduledCount > config)
					{
						MigrationLogger.Log(MigrationEventType.Verbose, "Job {0} provisioned too many in one pass {1}, max {2}", new object[]
						{
							base.Job,
							scheduledCount,
							config
						});
						break;
					}
				}
			}
			if (!foundWork)
			{
				scheduler.UnregisterJob(base.Job.JobId);
				response.Result = MigrationProcessorResult.Completed;
			}
			else
			{
				response.NumItemsOutstanding = new int?(base.GetJobItemCount(new MigrationUserStatus[]
				{
					status
				}));
			}
			return response;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003D5D8 File Offset: 0x0003B7D8
		private void RegisterJob(IProvisioningHandler scheduler)
		{
			if (scheduler.IsJobRegistered(base.Job.JobId))
			{
				return;
			}
			Guid jobId = base.Job.JobId;
			CultureInfo adminCulture = base.Job.AdminCulture;
			Guid ownerExchangeObjectId = base.Job.OwnerExchangeObjectId;
			ADObjectId ownerId = base.Job.OwnerId;
			DelegatedPrincipal delegatedAdminOwner = base.Job.DelegatedAdminOwner;
			if (ownerId == null && delegatedAdminOwner == null)
			{
				throw MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationUnknownException>("Cannot do provisioning since both owner id and delegated admin are null");
			}
			scheduler.RegisterJob(jobId, adminCulture, ownerExchangeObjectId, ownerId, delegatedAdminOwner, base.Job.SubmittedByUserAdminType, base.DataProvider.ADProvider.TenantOrganizationName, base.DataProvider.OrganizationId);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003D67C File Offset: 0x0003B87C
		private bool ProcessQueuedItem(IProvisioningHandler scheduler, MigrationJobItem jobItem)
		{
			MigrationUserStatus status = jobItem.Status;
			if (scheduler.IsItemCompleted(jobItem.StoreObjectId))
			{
				ProvisionedObject provisionedObj = scheduler.DequeueItem(jobItem.StoreObjectId);
				this.HandleProvisioningCompletedEvent(provisionedObj, jobItem);
			}
			else if (base.Job.IsCancelled)
			{
				scheduler.CancelItem(jobItem.StoreObjectId);
			}
			return status != jobItem.Status;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003D6DC File Offset: 0x0003B8DC
		private bool QueueItem(IProvisioningHandler scheduler, MigrationJobItem jobItem)
		{
			IProvisioningData provisioningData = this.GetProvisioningData(jobItem);
			if (provisioningData == null)
			{
				this.HandleNullIProvisioningDataEvent(jobItem);
				return false;
			}
			scheduler.QueueItem(base.Job.JobId, jobItem.StoreObjectId, provisioningData);
			return true;
		}
	}
}
