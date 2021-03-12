using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200020F RID: 527
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionInformationLoader : ISubscriptionInformationLoader
	{
		// Token: 0x0600120C RID: 4620 RVA: 0x0003ABA0 File Offset: 0x00038DA0
		private SubscriptionInformationLoader()
		{
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0003ABA8 File Offset: 0x00038DA8
		public static SubscriptionInformationLoader Instance
		{
			get
			{
				return SubscriptionInformationLoader.instance;
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0003ABDC File Offset: 0x00038DDC
		public bool TryReloadStateStorage(AggregationWorkItem workItem, IStateStorage stateStorage, out ISyncException exception)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", workItem);
			SyncUtilities.ThrowIfArgumentNull("stateStorage", stateStorage);
			return this.TryStateStorageAction(workItem.SyncLogSession, delegate
			{
				stateStorage.ReloadForRetry(new EventHandler<RoundtripCompleteEventArgs>(workItem.ConnectionStatistics.OnRoundtripComplete));
			}, out exception);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0003AC40 File Offset: 0x00038E40
		public bool TryLoadSubscription(AggregationWorkItem workItem, SyncMailboxSession syncMailboxSession, out ISyncWorkerData subscription, out ISyncException exception, out bool invalidState)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", workItem);
			SyncUtilities.ThrowIfArgumentNull("syncMailboxSession", syncMailboxSession);
			subscription = null;
			exception = null;
			invalidState = false;
			try
			{
				try
				{
					subscription = SyncStoreLoadManager.LoadSubscription(syncMailboxSession.MailboxSession, workItem.SubscriptionMessageId, workItem.SubscriptionType, new EventHandler<RoundtripCompleteEventArgs>(workItem.ConnectionStatistics.OnRoundtripComplete));
				}
				catch (ObjectNotFoundException)
				{
					workItem.SyncLogSession.LogError((TSLID)948UL, "The submitted subscription could not be found. Notify cache that the subscription needs to be crawled.", new object[0]);
				}
				if (subscription == null)
				{
					exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, this.CreateSubscriptionSyncExceptionWithoutSubscriptionName());
					workItem.SyncLogSession.LogError((TSLID)517UL, "The subscription was either not found or we failed to load it.", new object[0]);
					invalidState = true;
					return false;
				}
				return true;
			}
			catch (InvalidDataException resultException)
			{
				this.WrapException(resultException, workItem.SyncLogSession);
			}
			catch (LocalizedException resultException2)
			{
				this.WrapException(resultException2, workItem.SyncLogSession);
			}
			return false;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0003AD4C File Offset: 0x00038F4C
		public bool TryLoadMailboxSession(AggregationWorkItem workItem, SyncMailboxSession syncMailboxSession, out OrganizationId organizationId, out bool invalidState, out ISyncException exception)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", workItem);
			SyncUtilities.ThrowIfArgumentNull("syncMailboxSession", syncMailboxSession);
			invalidState = false;
			if (!this.TryOpenMailboxSession(workItem.LegacyDN, workItem.UserMailboxGuid, workItem.DatabaseGuid, workItem.MailboxServer, syncMailboxSession, workItem.SyncLogSession, out organizationId, out exception, out invalidState))
			{
				return false;
			}
			if (this.WasMailboxMoved(workItem.SyncLogSession, workItem.DatabaseGuid, syncMailboxSession, out exception))
			{
				invalidState = true;
				return false;
			}
			return !this.WasStoreRestarted(workItem.SyncLogSession, syncMailboxSession, out exception);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0003AE1C File Offset: 0x0003901C
		public bool TryLoadStateStorage(AggregationWorkItem workItem, SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, out IStateStorage stateStorage, out ISyncException exception)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", workItem);
			SyncUtilities.ThrowIfArgumentNull("syncMailboxSession", syncMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			IStateStorage newStateStorage = null;
			bool result = this.TryStateStorageAction(workItem.SyncLogSession, delegate
			{
				newStateStorage = new StateStorage(syncMailboxSession.MailboxSession, subscription, workItem.SyncLogSession, new EventHandler<RoundtripCompleteEventArgs>(workItem.ConnectionStatistics.OnRoundtripComplete));
			}, out exception);
			stateStorage = newStateStorage;
			return result;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0003AEA8 File Offset: 0x000390A8
		public bool TrySaveSubscription(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, EventHandler<RoundtripCompleteEventArgs> onRoundtripComplete, out Exception exception)
		{
			SyncUtilities.ThrowIfArgumentNull("syncMailboxSession", syncMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("syncMailboxSession.MailboxSession", syncMailboxSession.MailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			return SyncStoreLoadManager.TrySaveSubscription(syncMailboxSession.MailboxSession, (AggregationSubscription)subscription, onRoundtripComplete, out exception);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0003AEF4 File Offset: 0x000390F4
		public bool TryDeleteSubscription(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, EventHandler<RoundtripCompleteEventArgs> roundtripComplete)
		{
			return SyncStoreLoadManager.TryDeleteSubscription(syncMailboxSession.MailboxSession, (AggregationSubscription)subscription, roundtripComplete);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0003AF18 File Offset: 0x00039118
		public bool IsMailboxOverQuota(SyncMailboxSession syncMailboxSession, SyncLogSession syncLogSession, ulong requiredFreeBytes)
		{
			SyncUtilities.ThrowIfArgumentNull("syncMailboxSession", syncMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			Mailbox mailbox = syncMailboxSession.MailboxSession.Mailbox;
			mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.QuotaProhibitReceive,
				MailboxSchema.QuotaUsedExtended
			});
			int num = SyncUtilities.SafeGetProperty<int>(mailbox, MailboxSchema.QuotaProhibitReceive, -1);
			if (num < 0)
			{
				return false;
			}
			ulong num2 = (ulong)SyncUtilities.SafeGetProperty<long>(mailbox, MailboxSchema.QuotaUsedExtended);
			ulong num3 = (ulong)((long)num * 1024L - (long)num2);
			if (num3 < requiredFreeBytes)
			{
				syncLogSession.LogError((TSLID)949UL, "User is over the quota. Current Free Space = {0}B, Expected Free Space = {1}B", new object[]
				{
					num3,
					requiredFreeBytes
				});
				return true;
			}
			syncLogSession.LogDebugging((TSLID)1088UL, "User is within the quota. Current Free Space = {0}B, Expected Free Space = {1}B", new object[]
			{
				num3,
				requiredFreeBytes
			});
			return false;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0003B004 File Offset: 0x00039204
		public bool TrySendSubscriptionNotificationEmail(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, SyncLogSession syncLogSession, out bool retry)
		{
			return SubscriptionNotificationHelper.Instance.TrySendSubscriptionNotificationEmail(syncMailboxSession.MailboxSession, (AggregationSubscription)subscription, syncLogSession, out retry);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0003B02C File Offset: 0x0003922C
		private bool TryStateStorageAction(SyncLogSession syncLogSession, Action stateStorageAction, out ISyncException exception)
		{
			exception = null;
			try
			{
				stateStorageAction();
				return true;
			}
			catch (StoragePermanentException ex)
			{
				if (ExceptionUtilities.ShouldPermanentMapiOrXsoExceptionBeTreatedAsTransient(syncLogSession, ex))
				{
					exception = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, ex);
				}
				else
				{
					exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, ex);
				}
			}
			catch (StorageTransientException innerException)
			{
				exception = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException);
			}
			catch (SerializationException innerException2)
			{
				exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException2);
			}
			catch (InvalidOperationException innerException3)
			{
				exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException3);
			}
			catch (SyncStoreUnhealthyException innerException4)
			{
				exception = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, innerException4);
			}
			return false;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0003B0E0 File Offset: 0x000392E0
		private bool WasStoreRestarted(SyncLogSession syncLogSession, SyncMailboxSession syncMailboxSession, out ISyncException exception)
		{
			exception = null;
			Exception ex = null;
			try
			{
				bool flag = syncMailboxSession.EnsureConnectWithStatus();
				if (flag)
				{
					ex = new StoreRestartedException();
				}
			}
			catch (StorageTransientException ex2)
			{
				ex = ex2;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				syncLogSession.LogError((TSLID)958UL, "Store session was dropped or failed to reconnect due to exception {0}. Failing the sync with needs backoff with recovery sync true for the next sync.", new object[]
				{
					ex
				});
				exception = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, ex, true);
				return true;
			}
			return false;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0003B164 File Offset: 0x00039364
		private bool WasMailboxMoved(SyncLogSession syncLogSession, Guid databaseGuid, SyncMailboxSession syncMailboxSession, out ISyncException exception)
		{
			if (!object.Equals(syncMailboxSession.MailboxSession.MailboxOwner.MailboxInfo.GetDatabaseGuid(), databaseGuid))
			{
				syncLogSession.LogError((TSLID)947UL, "The database Guid for the mailbox does not match the submitted database guid. This means the mailbox was moved and we should delete all the subscription in the cache this was submitted from.", new object[0]);
				exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, this.CreateSubscriptionSyncExceptionWithoutSubscriptionName());
				return true;
			}
			exception = null;
			return false;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0003B1CC File Offset: 0x000393CC
		private bool TryOpenMailboxSession(string legacyDN, Guid mailboxGuid, Guid databaseGuid, string mailboxServer, SyncMailboxSession syncMailboxSession, SyncLogSession syncLogSession, out OrganizationId organizationId, out ISyncException exception, out bool invalidState)
		{
			organizationId = null;
			invalidState = false;
			try
			{
				return syncMailboxSession.TryOpen(legacyDN, mailboxGuid, databaseGuid, mailboxServer, out organizationId, out exception, out invalidState);
			}
			catch (InvalidDataException resultException)
			{
				exception = this.WrapException(resultException, syncLogSession);
			}
			catch (LocalizedException resultException2)
			{
				exception = this.WrapException(resultException2, syncLogSession);
			}
			return false;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0003B234 File Offset: 0x00039434
		private SubscriptionSyncException CreateSubscriptionSyncExceptionWithoutSubscriptionName()
		{
			return new SubscriptionSyncException(null);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0003B23C File Offset: 0x0003943C
		private ISyncException WrapException(Exception resultException, SyncLogSession syncLogSession)
		{
			syncLogSession.LogError((TSLID)942UL, "Hit an exception when loading subscription: {0}.", new object[]
			{
				resultException
			});
			if (resultException is TransientException)
			{
				return SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, resultException);
			}
			return SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.CommunicationError, resultException);
		}

		// Token: 0x040009BC RID: 2492
		private static readonly SubscriptionInformationLoader instance = new SubscriptionInformationLoader();
	}
}
