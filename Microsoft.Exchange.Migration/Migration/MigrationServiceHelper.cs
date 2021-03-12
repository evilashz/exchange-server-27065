using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.SyncMigrationServicelet;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationServiceHelper
	{
		// Token: 0x06000187 RID: 391 RVA: 0x0000796C File Offset: 0x00005B6C
		internal static bool TryParseSmtpAddress(string emailAddress, out SmtpAddress smtpAddress)
		{
			smtpAddress = new SmtpAddress(emailAddress);
			if (!smtpAddress.IsValidAddress)
			{
				smtpAddress = SmtpAddress.Empty;
				return false;
			}
			return true;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007990 File Offset: 0x00005B90
		internal static void SafeInvokeImplMethod(Action method, MigrationServiceRpcMethodCode methodCode)
		{
			try
			{
				method();
			}
			catch (MigrationMailboxNotFoundOnServerException ex)
			{
				MigrationApplication.NotifyOfTransientException(ex, "SafeInvokeImplMethod: " + methodCode);
				throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.MailboxNotFound, ex.Message);
			}
			catch (WrongServerException ex2)
			{
				MigrationLogger.Log(MigrationEventType.Warning, ex2, "SafeInvokeImplMethod: WrongServerException for method {0}", new object[]
				{
					methodCode
				});
				MigrationApplication.NotifyOfTransientException(ex2, "SafeInvokeImplMethod: " + methodCode);
				throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.MailboxNotFound, ex2.Message);
			}
			catch (MigrationServiceRpcTransientException ex3)
			{
				MigrationApplication.NotifyOfTransientException(ex3, "SafeInvokeImplMethod: " + methodCode);
				throw new MigrationServiceRpcException(ex3.ResultCode, ex3.Message);
			}
			catch (MigrationTransientException ex4)
			{
				MigrationApplication.NotifyOfTransientException(ex4, "SafeInvokeImplMethod: " + methodCode);
				throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.MigrationTransientError, ex4.Message);
			}
			catch (MigrationServiceRpcException ex5)
			{
				if ((ex5.ResultCode & (MigrationServiceRpcResultCode)32768) != (MigrationServiceRpcResultCode)0)
				{
					MigrationApplication.NotifyOfTransientException(ex5, "SafeInvokeImplMethod: " + methodCode);
				}
				else
				{
					MigrationApplication.NotifyOfPermanentException(ex5, "SafeInvokeImplMethod: " + methodCode);
				}
				throw;
			}
			catch (MigrationPermanentException ex6)
			{
				MigrationApplication.NotifyOfPermanentException(ex6, "SafeInvokeImplMethod: " + methodCode);
				throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.MigrationPermanentError, ex6.Message);
			}
			catch (MigrationDataCorruptionException ex7)
			{
				MigrationApplication.NotifyOfPermanentException(ex7, "SafeInvokeImplMethod found corruption: " + methodCode);
				throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.MigrationPermanentError, ex7.Message);
			}
			catch (StorageTransientException ex8)
			{
				MigrationApplication.NotifyOfTransientException(ex8, "SafeInvokeImplMethod: " + methodCode);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2867211581U);
				throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.StorageTransientError, ex8.Message);
			}
			catch (StoragePermanentException ex9)
			{
				MigrationApplication.NotifyOfPermanentException(ex9, "SafeInvokeImplMethod: " + methodCode);
				throw new MigrationServiceRpcException(MigrationServiceRpcResultCode.StoragePermanentError, ex9.Message);
			}
			finally
			{
				MigrationLogContext.Clear();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007C64 File Offset: 0x00005E64
		internal static MigrationJobItem GetJobItemByEmailAddress(IMigrationDataProvider dataProvider, string emailAddress, bool cleanupCorrupted)
		{
			IEnumerable<MigrationJobItem> byIdentifier = MigrationJobItem.GetByIdentifier(dataProvider, null, emailAddress, null);
			MigrationJobItem migrationJobItem = MigrationServiceHelper.GetUniqueAndNonCorruptJobItem(dataProvider, byIdentifier, cleanupCorrupted, "GetJobItemByEmailAddress," + emailAddress);
			if (migrationJobItem == null)
			{
				try
				{
					MailboxData mailboxDataFromSmtpAddress = dataProvider.ADProvider.GetMailboxDataFromSmtpAddress(emailAddress, false, true);
					migrationJobItem = MigrationServiceHelper.GetJobItemByLegacyDN(dataProvider, mailboxDataFromSmtpAddress.MailboxLegacyDN, cleanupCorrupted);
				}
				catch (MissingExchangeGuidException)
				{
					migrationJobItem = null;
				}
				catch (MigrationRecipientNotFoundException)
				{
					migrationJobItem = null;
				}
			}
			return migrationJobItem;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007CD8 File Offset: 0x00005ED8
		internal static MigrationJobItem GetJobItemByLegacyDN(IMigrationDataProvider dataProvider, string userLegacyDn, bool cleanupCorrupted)
		{
			IEnumerable<MigrationJobItem> byLegacyDN = MigrationJobItem.GetByLegacyDN(dataProvider, userLegacyDn);
			return MigrationServiceHelper.GetUniqueAndNonCorruptJobItem(dataProvider, byLegacyDN, cleanupCorrupted, "GetJobItemByLegDN," + userLegacyDn);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007D04 File Offset: 0x00005F04
		internal static MigrationJobItem GetIMAPJobItemBySubscriptionID(IMigrationDataProvider dataProvider, StoreObjectId subscriptionMessageId)
		{
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobItemSubscriptionMessageId, subscriptionMessageId.GetBytes());
			IEnumerable<MigrationJobItem> byFilter = MigrationJobItem.GetByFilter(dataProvider, primaryFilter, new MigrationEqualityFilter[]
			{
				MigrationJobItem.MessageClassEqualityFilter
			}, null, new MigrationJobObjectCache(dataProvider), null);
			return MigrationServiceHelper.GetUniqueAndNonCorruptJobItem(dataProvider, byFilter, false, "GetIMAPJobItemBySubscriptionID," + subscriptionMessageId.ToString());
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007D68 File Offset: 0x00005F68
		private static MigrationJobItem GetUniqueAndNonCorruptJobItem(IMigrationDataProvider dataProvider, IEnumerable<MigrationJobItem> jobItems, bool cleanupCorrupted, string debugInfo)
		{
			MigrationJobItem migrationJobItem = null;
			List<StoreObjectId> list = new List<StoreObjectId>();
			foreach (MigrationJobItem migrationJobItem2 in jobItems)
			{
				if (migrationJobItem2.Status == MigrationUserStatus.Corrupted)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "GetUniqueAndNonCorruptJobItem: {0}, skipping corrupt job item {1}", new object[]
					{
						debugInfo,
						migrationJobItem2
					});
					list.Add(migrationJobItem2.StoreObjectId);
				}
				else if (migrationJobItem == null)
				{
					migrationJobItem = migrationJobItem2;
				}
				else
				{
					MigrationLogger.Log(MigrationEventType.Error, "GetUniqueAndNonCorruptJobItem: {0}, deleting {1} because of dup with {2}", new object[]
					{
						debugInfo,
						migrationJobItem2,
						migrationJobItem
					});
					MigrationPermanentException exception = new MigrationPermanentException(Strings.MultipleMigrationJobItems(migrationJobItem2.Identifier));
					migrationJobItem2.SetCorruptStatus(dataProvider, exception);
					list.Add(migrationJobItem2.StoreObjectId);
				}
			}
			if (cleanupCorrupted && list.Count > 0)
			{
				MigrationLogger.Log(MigrationEventType.Warning, "GetUniqueAndNonCorruptJobItem: {0}, moving {1} job items to Corrupted folder", new object[]
				{
					debugInfo,
					list.Count
				});
				dataProvider.MoveMessageItems(list.ToArray(), MigrationFolderName.CorruptedItems);
			}
			return migrationJobItem;
		}
	}
}
