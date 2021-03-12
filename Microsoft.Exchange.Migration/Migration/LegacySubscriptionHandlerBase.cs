using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200013C RID: 316
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class LegacySubscriptionHandlerBase : DisposeTrackableBase
	{
		// Token: 0x06000FDA RID: 4058 RVA: 0x00043628 File Offset: 0x00041828
		internal LegacySubscriptionHandlerBase(IMigrationDataProvider dataProvider, MigrationJob migrationJob)
		{
			this.DataProvider = dataProvider;
			this.Job = migrationJob;
			string jobName = (migrationJob == null) ? null : migrationJob.JobName;
			this.SubscriptionAccessor = MigrationServiceFactory.Instance.GetSubscriptionAccessor(this.DataProvider, this.Job.MigrationType, jobName, false, !this.Job.AutoComplete);
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000FDB RID: 4059
		public abstract bool SupportsActiveIncrementalSync { get; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000FDC RID: 4060
		public abstract bool SupportsAdvancedValidation { get; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00043687 File Offset: 0x00041887
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0004368F File Offset: 0x0004188F
		private protected IMigrationDataProvider DataProvider { protected get; private set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00043698 File Offset: 0x00041898
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x000436A0 File Offset: 0x000418A0
		private protected MigrationJob Job { protected get; private set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x000436A9 File Offset: 0x000418A9
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x000436B1 File Offset: 0x000418B1
		private protected SubscriptionAccessorBase SubscriptionAccessor { protected get; private set; }

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000436BC File Offset: 0x000418BC
		public static ILegacySubscriptionHandler CreateSubscriptionHandler(IMigrationDataProvider dataProvider, MigrationJob migrationJob)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(migrationJob, "migrationJob");
			MigrationType migrationType = migrationJob.MigrationType;
			if (migrationType <= MigrationType.BulkProvisioning)
			{
				if (migrationType == MigrationType.IMAP)
				{
					return new LegacyIMAPSubscriptionHandler(dataProvider, migrationJob);
				}
				if (migrationType == MigrationType.ExchangeOutlookAnywhere)
				{
					return new LegacyExchangeSubscriptionHandler(dataProvider, migrationJob);
				}
				if (migrationType == MigrationType.BulkProvisioning)
				{
					return null;
				}
			}
			else
			{
				if (migrationType == MigrationType.ExchangeRemoteMove)
				{
					return new LegacyRemoteMoveSubscriptionHandler(dataProvider, migrationJob);
				}
				if (migrationType == MigrationType.ExchangeLocalMove)
				{
					return new LegacyLocalMoveSubscriptionHandler(dataProvider, migrationJob);
				}
				if (migrationType == MigrationType.PublicFolder)
				{
					return new LegacyPublicFolderSubscriptionHandler(dataProvider, migrationJob);
				}
			}
			throw new ArgumentException("No handler defined for protocol " + migrationJob.MigrationType);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00043760 File Offset: 0x00041960
		public virtual IEnumerable<MigrationJobItem> GetJobItemsForSubscriptionCheck(ExDateTime? cutoffTime, MigrationUserStatus status, int maxItemsToCheck)
		{
			MigrationUserStatus effectiveJobItemStatus = SubscriptionArbiterBase.GetEffectiveJobItemStatus(this.Job, status);
			MigrationUserStatus migrationUserStatus = effectiveJobItemStatus;
			switch (migrationUserStatus)
			{
			case MigrationUserStatus.Syncing:
			case MigrationUserStatus.Completing:
				break;
			case MigrationUserStatus.Failed:
				goto IL_46;
			case MigrationUserStatus.Synced:
			case MigrationUserStatus.IncrementalFailed:
				return this.GetJobItemsForIncrementalSubscriptionCheck(cutoffTime, status, maxItemsToCheck);
			default:
				if (migrationUserStatus != MigrationUserStatus.IncrementalSyncing)
				{
					goto IL_46;
				}
				break;
			}
			return this.GetJobItemsForActiveSubscriptionCheck(cutoffTime, status, maxItemsToCheck);
			IL_46:
			throw new MigrationDataCorruptionException(string.Format(CultureInfo.InvariantCulture, "invalid effective job item status {0} for subscription check job status {1}, job item status {2}", new object[]
			{
				effectiveJobItemStatus,
				this.Job.Status,
				status
			}));
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000437F4 File Offset: 0x000419F4
		public void CancelUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			this.DisableSubscriptions(jobItem);
			jobItem.DisableMigration(this.DataProvider, MigrationUserStatus.Stopped);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0004380B File Offset: 0x00041A0B
		public void StopUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			if (this.SupportsActiveIncrementalSync)
			{
				this.DisableSubscriptions(jobItem);
			}
			jobItem.DisableMigration(this.DataProvider, MigrationUserStatus.IncrementalStopped);
		}

		// Token: 0x06000FE7 RID: 4071
		public abstract void DisableSubscriptions(MigrationJobItem jobItem);

		// Token: 0x06000FE8 RID: 4072
		public abstract void SyncSubscriptionSettings(MigrationJobItem jobItem);

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004382A File Offset: 0x00041A2A
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0004382C File Offset: 0x00041A2C
		protected virtual IEnumerable<MigrationJobItem> GetJobItemsForIncrementalSubscriptionCheck(ExDateTime? cutoffTime, MigrationUserStatus status, int maxItemsToCheck)
		{
			throw new NotSupportedException(string.Format("GetJobItemsForIncrementalSubscriptionCheck is not supported by {0}", base.GetType().Name));
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00043D08 File Offset: 0x00041F08
		protected virtual IEnumerable<MigrationJobItem> GetJobItemsForActiveSubscriptionCheck(ExDateTime? cutoffTime, MigrationUserStatus status, int maxItemsToCheck)
		{
			Dictionary<ISubscriptionId, MigrationJobItem> subscriptionMap = this.GetSubscriptionIdsForActiveSubscriptions(status, maxItemsToCheck);
			MigrationLogger.Log(MigrationEventType.Verbose, "querying for active subscription, Found {0} for job {1}", new object[]
			{
				subscriptionMap.Count,
				this.Job
			});
			ExDateTime statisticCutoffTime = ExDateTime.UtcNow - ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationInitialSyncStartPollingTimeout");
			if (cutoffTime == null || cutoffTime.Value < statisticCutoffTime)
			{
				cutoffTime = new ExDateTime?(statisticCutoffTime);
			}
			Dictionary<ISubscriptionId, MigrationJobItem> skippedIds = new Dictionary<ISubscriptionId, MigrationJobItem>();
			foreach (KeyValuePair<ISubscriptionId, MigrationJobItem> item in subscriptionMap)
			{
				KeyValuePair<ISubscriptionId, MigrationJobItem> keyValuePair = item;
				MigrationJobItem jobItem = keyValuePair.Value;
				if (jobItem.SubscriptionLastChecked == null || jobItem.SubscriptionLastChecked.Value < cutoffTime.Value)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "Found old job item {0} for job {1}", new object[]
					{
						jobItem,
						this.Job
					});
					yield return jobItem;
				}
				else
				{
					Dictionary<ISubscriptionId, MigrationJobItem> dictionary = skippedIds;
					KeyValuePair<ISubscriptionId, MigrationJobItem> keyValuePair2 = item;
					ISubscriptionId key = keyValuePair2.Key;
					KeyValuePair<ISubscriptionId, MigrationJobItem> keyValuePair3 = item;
					dictionary.Add(key, keyValuePair3.Value);
				}
			}
			foreach (KeyValuePair<ISubscriptionId, MigrationJobItem> item2 in skippedIds)
			{
				SnapshotStatus subscriptionStatus = SnapshotStatus.InProgress;
				KeyValuePair<ISubscriptionId, MigrationJobItem> localItem = item2;
				KeyValuePair<ISubscriptionId, MigrationJobItem> keyValuePair4 = item2;
				this.RunJobItemOperation(keyValuePair4.Value, delegate
				{
					MigrationJobItem value = localItem.Value;
					ISubscriptionId key2 = localItem.Key;
					subscriptionStatus = this.GetJobItemSubscriptionStatus(key2, value);
				});
				if (subscriptionStatus != SnapshotStatus.InProgress)
				{
					MigrationEventType eventType = MigrationEventType.Verbose;
					string format = "subscription has finished with status {0} for job item {1}";
					object[] array = new object[2];
					array[0] = subscriptionStatus;
					object[] array2 = array;
					int num = 1;
					KeyValuePair<ISubscriptionId, MigrationJobItem> keyValuePair5 = item2;
					array2[num] = keyValuePair5.Value;
					MigrationLogger.Log(eventType, format, array);
					KeyValuePair<ISubscriptionId, MigrationJobItem> keyValuePair6 = item2;
					yield return keyValuePair6.Value;
				}
			}
			yield break;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00043E04 File Offset: 0x00042004
		protected virtual Dictionary<ISubscriptionId, MigrationJobItem> GetSubscriptionIdsForActiveSubscriptions(MigrationUserStatus status, int maxItemsToCheck)
		{
			Dictionary<ISubscriptionId, MigrationJobItem> subscriptionIds = new Dictionary<ISubscriptionId, MigrationJobItem>(maxItemsToCheck);
			foreach (MigrationJobItem migrationJobItem in MigrationJobItem.GetBySubscriptionLastChecked(this.DataProvider, this.Job, null, status, maxItemsToCheck))
			{
				MigrationJobItem item = migrationJobItem;
				this.RunJobItemOperation(migrationJobItem, delegate
				{
					ISubscriptionId subscriptionId = item.SubscriptionId;
					if (subscriptionId != null)
					{
						if (subscriptionIds.ContainsKey(subscriptionId))
						{
							string internalDetails = string.Format("subscription id {0} already found for item {1} with id {2}, other item {3} with id {4}", new object[]
							{
								subscriptionId,
								subscriptionIds[subscriptionId],
								subscriptionIds[subscriptionId].JobItemGuid,
								item,
								item.JobItemGuid
							});
							throw new MigrationDataCorruptionException(internalDetails);
						}
						subscriptionIds.Add(subscriptionId, item);
					}
				});
			}
			return subscriptionIds;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00043EA8 File Offset: 0x000420A8
		protected virtual SnapshotStatus GetJobItemSubscriptionStatus(ISubscriptionId subscriptionId, MigrationJobItem migrationJobItem)
		{
			throw new NotSupportedException(string.Format("GetJobItemSubscriptionStatus is not supported by {0}", base.GetType().Name));
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00043EC4 File Offset: 0x000420C4
		protected virtual MigrationProcessorResult RunJobItemOperation(MigrationJobItem jobItem, Action itemOperation)
		{
			throw new NotSupportedException(string.Format("RunJobItemOperation is not supported by {0}", base.GetType().Name));
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00043EE0 File Offset: 0x000420E0
		protected virtual ISubscriptionId GetJobItemSubscriptionId(MigrationJobItem item)
		{
			throw new NotSupportedException(string.Format("GetJobItemSubscriptionId is not supported by {0}", base.GetType().Name));
		}
	}
}
