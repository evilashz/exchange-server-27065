using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000003 RID: 3
	internal class AsyncNotificationAdapter : IAsyncNotificationAdapter
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		private AsyncNotificationAdapter()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000002D8
		Guid? IAsyncNotificationAdapter.CreateNotification(IMigrationDataProvider dataProvider, MigrationJob job)
		{
			ADRecipientOrAddress owner = null;
			if (job.OwnerId == null)
			{
				if (!(job.OwnerExchangeObjectId != Guid.Empty))
				{
					goto IL_A0;
				}
			}
			try
			{
				ADRecipient adrecipient;
				if (job.OwnerExchangeObjectId != Guid.Empty)
				{
					adrecipient = dataProvider.ADProvider.GetADRecipientByExchangeObjectId(job.OwnerExchangeObjectId);
				}
				else
				{
					adrecipient = dataProvider.ADProvider.GetADRecipientByObjectId(job.OwnerId);
				}
				if (adrecipient != null && !string.IsNullOrEmpty((string)adrecipient[ADRecipientSchema.LegacyExchangeDN]))
				{
					Participant participant = new Participant(adrecipient);
					owner = new ADRecipientOrAddress(participant);
				}
			}
			catch (LocalizedException ex)
			{
				MigrationLogger.Log(MigrationEventType.Warning, "Error fetching the owner while creating async notification: {0}", new object[]
				{
					ex
				});
			}
			IL_A0:
			AsyncOperationNotificationDataProvider.CreateNotification(dataProvider.OrganizationId, job.JobId.ToString(), AsyncOperationType.Migration, AsyncOperationStatus.Created, new LocalizedString(job.JobName), owner, AsyncNotificationAdapter.GetExtendedData(job), false);
			MigrationLogger.Log(MigrationEventType.Verbose, "Created new async notification for job {0}", new object[]
			{
				job.JobName
			});
			return new Guid?(job.JobId);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021F4 File Offset: 0x000003F4
		void IAsyncNotificationAdapter.UpdateNotification(IMigrationDataProvider dataProvider, MigrationJob job)
		{
			if (job.NotificationId == null)
			{
				job.NotificationId = ((IAsyncNotificationAdapter)this).CreateNotification(dataProvider, job);
				job.SaveBatchFlagsAndNotificationId(dataProvider);
			}
			AsyncOperationStatus asyncOperationStatus;
			switch (job.Status)
			{
			case MigrationJobStatus.Created:
				asyncOperationStatus = AsyncOperationStatus.Created;
				break;
			case MigrationJobStatus.SyncInitializing:
			case MigrationJobStatus.CompletionInitializing:
			case MigrationJobStatus.Stopped:
				asyncOperationStatus = AsyncOperationStatus.Queued;
				break;
			case MigrationJobStatus.SyncStarting:
			case MigrationJobStatus.SyncCompleting:
			case MigrationJobStatus.ProvisionStarting:
			case MigrationJobStatus.Validating:
				asyncOperationStatus = AsyncOperationStatus.InProgress;
				break;
			case MigrationJobStatus.SyncCompleted:
				asyncOperationStatus = (job.SupportsMultiBatchFinalization ? AsyncOperationStatus.WaitingForFinalization : AsyncOperationStatus.Completed);
				break;
			case MigrationJobStatus.CompletionStarting:
			case MigrationJobStatus.Completing:
				asyncOperationStatus = AsyncOperationStatus.Completing;
				break;
			case MigrationJobStatus.Completed:
				asyncOperationStatus = AsyncOperationStatus.Completed;
				break;
			case MigrationJobStatus.Failed:
			case MigrationJobStatus.Corrupted:
				asyncOperationStatus = AsyncOperationStatus.Failed;
				break;
			case MigrationJobStatus.Removed:
			case MigrationJobStatus.Removing:
				asyncOperationStatus = AsyncOperationStatus.Removing;
				break;
			default:
				asyncOperationStatus = AsyncOperationStatus.InProgress;
				break;
			}
			if (job.NotificationId != null)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Updated async notification for job {0} with status {1} based on job staus {2}", new object[]
				{
					job.JobName,
					asyncOperationStatus,
					job.Status
				});
				AsyncOperationNotificationDataProvider.UpdateNotification(dataProvider.OrganizationId, job.NotificationId.Value.ToString(), new AsyncOperationStatus?(asyncOperationStatus), null, null, false, AsyncNotificationAdapter.GetExtendedData(job));
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000233C File Offset: 0x0000053C
		void IAsyncNotificationAdapter.RemoveNotification(IMigrationDataProvider dataProvider, MigrationJob job)
		{
			if (job.NotificationId == null)
			{
				return;
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "Removed async notification for job {0}", new object[]
			{
				job.JobName
			});
			AsyncOperationNotificationDataProvider.RemoveNotification(dataProvider.OrganizationId, job.NotificationId.Value.ToString(), false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023A0 File Offset: 0x000005A0
		private static KeyValuePair<string, LocalizedString>[] GetExtendedData(MigrationJob job)
		{
			return new KeyValuePair<string, LocalizedString>[]
			{
				new KeyValuePair<string, LocalizedString>(AsyncNotificationAdapter.TotalItemCount, new LocalizedString(job.TotalItemCount.ToString(CultureInfo.InvariantCulture))),
				new KeyValuePair<string, LocalizedString>(AsyncNotificationAdapter.TotalFailedCount, new LocalizedString(job.FailedItemCount.ToString(CultureInfo.InvariantCulture))),
				new KeyValuePair<string, LocalizedString>(AsyncNotificationAdapter.TotalCompletedCount, new LocalizedString(job.FinalizedItemCount.ToString(CultureInfo.InvariantCulture))),
				new KeyValuePair<string, LocalizedString>(AsyncNotificationAdapter.TotalSyncedCount, new LocalizedString(job.SyncedItemCount.ToString(CultureInfo.InvariantCulture))),
				new KeyValuePair<string, LocalizedString>(AsyncNotificationAdapter.MigrationType, new LocalizedString(job.MigrationType.ToString())),
				new KeyValuePair<string, LocalizedString>(AsyncNotificationAdapter.TotalCompletedWithErrorCount, new LocalizedString((job.FailedOtherItemCount + job.FailedFinalizationItemCount).ToString(CultureInfo.InvariantCulture)))
			};
		}

		// Token: 0x04000001 RID: 1
		public static readonly string TotalItemCount = "TotalItemCount";

		// Token: 0x04000002 RID: 2
		public static readonly string TotalCompletedCount = "TotalCompletedCount";

		// Token: 0x04000003 RID: 3
		public static readonly string TotalSyncedCount = "TotalSyncedCount";

		// Token: 0x04000004 RID: 4
		public static readonly string TotalFailedCount = "TotalFailedCount";

		// Token: 0x04000005 RID: 5
		public static readonly string MigrationType = "MigrationType";

		// Token: 0x04000006 RID: 6
		public static readonly string TotalCompletedWithErrorCount = "TotalCompletedWithErrorCount";

		// Token: 0x04000007 RID: 7
		public static readonly IAsyncNotificationAdapter Empty = new AsyncNotificationAdapter.NullNotificationAdapter();

		// Token: 0x04000008 RID: 8
		public static readonly IAsyncNotificationAdapter Instance = new AsyncNotificationAdapter();

		// Token: 0x02000004 RID: 4
		private class NullNotificationAdapter : IAsyncNotificationAdapter
		{
			// Token: 0x0600000A RID: 10 RVA: 0x00002530 File Offset: 0x00000730
			Guid? IAsyncNotificationAdapter.CreateNotification(IMigrationDataProvider dataProvider, MigrationJob job)
			{
				return null;
			}

			// Token: 0x0600000B RID: 11 RVA: 0x00002546 File Offset: 0x00000746
			void IAsyncNotificationAdapter.UpdateNotification(IMigrationDataProvider dataProvider, MigrationJob job)
			{
			}

			// Token: 0x0600000C RID: 12 RVA: 0x00002548 File Offset: 0x00000748
			void IAsyncNotificationAdapter.RemoveNotification(IMigrationDataProvider dataProvider, MigrationJob job)
			{
			}
		}
	}
}
