using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OutlookService.Service;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F67 RID: 3943
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OutlookServiceSubscriptionStorage : DisposableObject, IOutlookServiceSubscriptionStorage, IDisposable
	{
		// Token: 0x060063CE RID: 25550 RVA: 0x0013727F File Offset: 0x0013547F
		internal OutlookServiceSubscriptionStorage(IMailboxSession mailboxSession, IFolder folder, string tenantId) : this(mailboxSession, folder, tenantId, XSOFactory.Default)
		{
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x00137290 File Offset: 0x00135490
		internal OutlookServiceSubscriptionStorage(IMailboxSession mailboxSession, IFolder folder, string tenantId, IXSOFactory xsoFactory)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("folder", folder);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("tenantId", tenantId);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			this.mailboxSession = mailboxSession;
			this.folder = folder;
			this.xsoFactory = xsoFactory;
			this.TenantId = tenantId;
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x060063D0 RID: 25552 RVA: 0x001372ED File Offset: 0x001354ED
		// (set) Token: 0x060063D1 RID: 25553 RVA: 0x001372F5 File Offset: 0x001354F5
		public string TenantId { get; private set; }

		// Token: 0x060063D2 RID: 25554 RVA: 0x001372FE File Offset: 0x001354FE
		public static IOutlookServiceSubscriptionStorage Create(IMailboxSession mailboxSession, IFolder folder, string tenantId)
		{
			return new OutlookServiceSubscriptionStorage(mailboxSession, folder, tenantId);
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x00137308 File Offset: 0x00135508
		public List<OutlookServiceNotificationSubscription> GetActiveNotificationSubscriptions(string appId, uint deactivationInHours = 72U)
		{
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.GetActiveNotificationSubscriptions: Requested active subscriptions under this folder.");
			return this.GetOutlookServiceSubscriptions(new ActiveOutlookServiceSubscriptionItemEnumerator(this.folder, appId, deactivationInHours));
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x00137334 File Offset: 0x00135534
		public List<OutlookServiceNotificationSubscription> GetActiveNotificationSubscriptionsForContext(string notificationContext)
		{
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.GetActiveNotificationSubscriptionsForContext: Requested active subscriptions under this folder for a context.");
			List<OutlookServiceNotificationSubscription> list = new List<OutlookServiceNotificationSubscription>();
			foreach (OutlookServiceNotificationSubscription outlookServiceNotificationSubscription in this.GetActiveNotificationSubscriptions(null, 72U))
			{
				if (!(outlookServiceNotificationSubscription.AppId == OutlookServiceNotificationSubscription.AppId_HxMail) || !(outlookServiceNotificationSubscription.SubscriptionId != notificationContext))
				{
					list.Add(outlookServiceNotificationSubscription);
				}
			}
			return list;
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x001373C8 File Offset: 0x001355C8
		public List<string> GetDeactivatedNotificationSubscriptions(string appId, uint deactivationInHours = 72U)
		{
			List<StoreObjectId> outlookServiceSubscriptionIds = this.GetOutlookServiceSubscriptionIds(new DeactivatedOutlookServiceSubscriptionItemEnumerator(this.folder, appId, deactivationInHours));
			List<string> list = new List<string>();
			foreach (StoreObjectId storeId in outlookServiceSubscriptionIds)
			{
				using (IOutlookServiceSubscriptionItem outlookServiceSubscriptionItem = this.xsoFactory.BindToOutlookServiceSubscriptionItem(this.mailboxSession, storeId, null))
				{
					new OutlookServiceNotificationSubscription(outlookServiceSubscriptionItem);
					list.Add(outlookServiceSubscriptionItem.SubscriptionId);
				}
			}
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.GetDeactivatedNotificationSubscriptions: A total {0} deactivated subscription items found.", list.Count);
			return list;
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x00137488 File Offset: 0x00135688
		public List<string> GetExpiredNotificationSubscriptions(string appId)
		{
			List<StoreObjectId> outlookServiceSubscriptionIds = this.GetOutlookServiceSubscriptionIds(new ExpiredOutlookServiceSubscriptionItemEnumerator(this.folder, appId, OutlookServiceSubscriptionStorage.ExpiredSubscriptionItemThresholdPolicy + 1U));
			List<string> list = new List<string>();
			foreach (StoreObjectId storeId in outlookServiceSubscriptionIds)
			{
				using (IOutlookServiceSubscriptionItem outlookServiceSubscriptionItem = this.xsoFactory.BindToOutlookServiceSubscriptionItem(this.mailboxSession, storeId, null))
				{
					new OutlookServiceNotificationSubscription(outlookServiceSubscriptionItem);
					list.Add(outlookServiceSubscriptionItem.SubscriptionId);
				}
			}
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.GetExpiredNotificationSubscriptions: A total {0} expired subscription items found.", list.Count);
			return list;
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x00137554 File Offset: 0x00135754
		public List<StoreObjectId> GetExpiredNotificationStoreIds(string appId)
		{
			List<StoreObjectId> outlookServiceSubscriptionIds = this.GetOutlookServiceSubscriptionIds(new ExpiredOutlookServiceSubscriptionItemEnumerator(this.folder, appId, OutlookServiceSubscriptionStorage.ExpiredSubscriptionItemThresholdPolicy + 1U));
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.GetExpiredNotificationSubscriptions: A total {0} expired subscription items found.", outlookServiceSubscriptionIds.Count);
			return outlookServiceSubscriptionIds;
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x0013759D File Offset: 0x0013579D
		public List<OutlookServiceNotificationSubscription> GetNotificationSubscriptions(string appId)
		{
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.GetNotificationSubscriptions: Requested all subscription under this folder.");
			return this.GetOutlookServiceSubscriptions(new OutlookServiceSubscriptionItemEnumerator(this.folder, appId));
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x001375C8 File Offset: 0x001357C8
		public OutlookServiceNotificationSubscription CreateOrUpdateSubscriptionItem(OutlookServiceNotificationSubscription subscription)
		{
			ArgumentValidator.ThrowIfNull("subscription", subscription);
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string, IExchangePrincipal>(0L, "OutlookServiceSubscriptionItem.CreateOrUpdateSubscription: Searching for Subscription {0} on Mailbox {1}.", subscription.SubscriptionId, this.mailboxSession.MailboxOwner);
			IStorePropertyBag[] array = OutlookServiceSubscriptionStorage.GetSubscriptionById(this.folder, subscription.SubscriptionId).ToArray<IStorePropertyBag>();
			IOutlookServiceSubscriptionItem outlookServiceSubscriptionItem = null;
			OutlookServiceNotificationSubscription result = null;
			try
			{
				if (array.Length >= 1)
				{
					if (array.Length > 1)
					{
						ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceWarning<string, Guid>(0L, "OutlookServiceSubscriptionItem.CreateOrUpdateSubscription: AmbiguousSubscription for subscription {0} and user {1}", subscription.SubscriptionId, this.mailboxSession.MailboxGuid);
					}
					IStorePropertyBag storePropertyBag = array[0];
					VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
					if (valueOrDefault == null)
					{
						ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<string>((long)storePropertyBag.GetHashCode(), "OutlookServiceSubscriptionItem.CreateOrUpdateSubscription: Cannot resolve the ItemSchema.Id property from the Enumerable.", subscription.SubscriptionId);
						throw new CannotResolvePropertyException(ItemSchema.Id.Name);
					}
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<VersionedId>((long)storePropertyBag.GetHashCode(), "OutlookServiceSubscriptionItem.CreateOrUpdateSubscription: Found one existing subscription with ItemSchema.Id = {0}.", valueOrDefault);
					outlookServiceSubscriptionItem = this.xsoFactory.BindToOutlookServiceSubscriptionItem(this.mailboxSession, valueOrDefault, null);
					outlookServiceSubscriptionItem.LastUpdateTimeUTC = ExDateTime.UtcNow;
					outlookServiceSubscriptionItem.AppId = subscription.AppId;
					outlookServiceSubscriptionItem.DeviceNotificationId = subscription.DeviceNotificationId;
					if (subscription.ExpirationTime != null)
					{
						outlookServiceSubscriptionItem.ExpirationTime = subscription.ExpirationTime.Value;
					}
					outlookServiceSubscriptionItem.LockScreen = subscription.LockScreen;
					ConflictResolutionResult conflictResolutionResult = outlookServiceSubscriptionItem.Save(SaveMode.ResolveConflicts);
					if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
					{
						ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<string>((long)storePropertyBag.GetHashCode(), "OutlookServiceSubscriptionItem.CreateOrUpdateSubscription: Save failed due to conflicts for subscription {0}.", subscription.SubscriptionId);
						throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(subscription.SubscriptionId));
					}
					outlookServiceSubscriptionItem.Load(OutlookServiceSubscriptionItemEnumeratorBase.OutlookServiceSubscriptionItemProperties);
				}
				else
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug(0L, "OutlookServiceSubscriptionItem.CreateOrUpdateSubscription: Cannot resolve given subscription, about to create a new SubscriptionItem.");
					outlookServiceSubscriptionItem = this.CreateSubscriptionItem(subscription);
				}
				result = new OutlookServiceNotificationSubscription(outlookServiceSubscriptionItem);
			}
			finally
			{
				if (outlookServiceSubscriptionItem != null)
				{
					outlookServiceSubscriptionItem.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x001377A8 File Offset: 0x001359A8
		private IOutlookServiceSubscriptionItem CreateSubscriptionItem(OutlookServiceNotificationSubscription subscription)
		{
			IOutlookServiceSubscriptionItem outlookServiceSubscriptionItem = null;
			try
			{
				outlookServiceSubscriptionItem = this.xsoFactory.CreateOutlookServiceSubscriptionItem(this.mailboxSession, this.folder.StoreObjectId);
				outlookServiceSubscriptionItem.SubscriptionId = subscription.SubscriptionId;
				outlookServiceSubscriptionItem.AppId = subscription.AppId;
				outlookServiceSubscriptionItem.DeviceNotificationId = subscription.DeviceNotificationId;
				if (subscription.ExpirationTime != null)
				{
					outlookServiceSubscriptionItem.ExpirationTime = subscription.ExpirationTime.Value;
				}
				outlookServiceSubscriptionItem.LockScreen = subscription.LockScreen;
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)outlookServiceSubscriptionItem.GetHashCode(), "OutlookServiceSubscriptionItem.Create: Created SubscriptionItem on store, Id:{0}, RefTm:{1}, AppId:{2}, DevNotifId:{3}, Expire:{4}, Lock:{5}", new object[]
					{
						outlookServiceSubscriptionItem.SubscriptionId,
						outlookServiceSubscriptionItem.LastUpdateTimeUTC,
						outlookServiceSubscriptionItem.AppId,
						outlookServiceSubscriptionItem.DeviceNotificationId,
						outlookServiceSubscriptionItem.ExpirationTime.ToString(),
						outlookServiceSubscriptionItem.LockScreen
					});
				}
				ConflictResolutionResult conflictResolutionResult = outlookServiceSubscriptionItem.Save(SaveMode.FailOnAnyConflict);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<string>((long)outlookServiceSubscriptionItem.GetHashCode(), "OutlookServiceSubscriptionItem.Create: Save failed due to conflicts for subscription {0}.", subscription.SubscriptionId);
					throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(outlookServiceSubscriptionItem.SubscriptionId));
				}
				outlookServiceSubscriptionItem.Load(OutlookServiceSubscriptionItemEnumeratorBase.OutlookServiceSubscriptionItemProperties);
			}
			catch
			{
				if (outlookServiceSubscriptionItem != null)
				{
					outlookServiceSubscriptionItem.Dispose();
				}
				throw;
			}
			return outlookServiceSubscriptionItem;
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x0013795C File Offset: 0x00135B5C
		public static IEnumerable<IStorePropertyBag> GetSubscriptionById(IFolder folder, string subscriptionId)
		{
			Util.ThrowOnNullArgument(folder, "folder");
			Util.ThrowOnNullArgument(subscriptionId, "subscriptionId");
			return new OutlookServiceSubscriptionItemEnumerator(folder).Where(delegate(IStorePropertyBag x)
			{
				string valueOrDefault = x.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.SubscriptionId, null);
				return !string.IsNullOrEmpty(valueOrDefault) && valueOrDefault.Equals(subscriptionId);
			});
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x001379A8 File Offset: 0x00135BA8
		public void DeleteExpiredSubscriptions(string appId)
		{
			StoreObjectId[] array = this.GetExpiredNotificationStoreIds(appId).ToArray();
			if ((long)array.Length > (long)((ulong)OutlookServiceSubscriptionStorage.ExpiredSubscriptionItemThresholdPolicy))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.DeleteExpiredSubscriptions: Number of expired subscription items exceeded threshold {0}.", array.Length);
				this.folder.DeleteObjects(DeleteItemFlags.HardDelete, array);
			}
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x001379F8 File Offset: 0x00135BF8
		public void DeleteAllSubscriptions(string appId)
		{
			StoreObjectId[] array = this.GetOutlookServiceSubscriptionIds(new OutlookServiceSubscriptionItemEnumerator(this.folder, appId)).ToArray();
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.DeleteAllSubscriptions: Number of subscription items to be deleted {0}.", array.Length);
			this.folder.DeleteObjects(DeleteItemFlags.HardDelete, array);
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x00137A44 File Offset: 0x00135C44
		public void DeleteSubscription(StoreObjectId itemId)
		{
			ArgumentValidator.ThrowIfNull("itemId", itemId);
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.DeleteSubscription: Requested deletion of subscription {0}.", itemId);
			this.folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
			{
				itemId
			});
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x00137A8C File Offset: 0x00135C8C
		public void DeleteSubscription(string subscriptionId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("subscriptionId", subscriptionId);
			bool flag = false;
			foreach (IStorePropertyBag storePropertyBag in OutlookServiceSubscriptionStorage.GetSubscriptionById(this.folder, subscriptionId))
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				if (valueOrDefault == null)
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError((long)this.GetHashCode(), "NotificationStorage.DeleteSubscription: A subscription with an empty ItemSchema.Id value was returned by the Enumerator.");
					throw new CannotResolvePropertyException(ItemSchema.Id.Name);
				}
				this.DeleteSubscription(valueOrDefault.ObjectId);
				flag = true;
			}
			if (!flag)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "NotificationStorage.DeleteSubscription: Did not find the subscription to delete.");
			}
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x00137B44 File Offset: 0x00135D44
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<OutlookServiceSubscriptionStorage>(this);
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x00137B4C File Offset: 0x00135D4C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x060063E2 RID: 25570 RVA: 0x00137B74 File Offset: 0x00135D74
		private List<StoreObjectId> GetOutlookServiceSubscriptionIds(OutlookServiceSubscriptionItemEnumeratorBase enumerable)
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			foreach (IStorePropertyBag propertyBag in enumerable)
			{
				list.Add(this.GetVersionedId(propertyBag).ObjectId);
			}
			return list;
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x00137BD0 File Offset: 0x00135DD0
		private VersionedId GetVersionedId(IStorePropertyBag propertyBag)
		{
			VersionedId valueOrDefault = propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			if (valueOrDefault != null)
			{
				return valueOrDefault;
			}
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.GetOutlookServiceSubscriptionIds: A subscription with an empty ItemSchema.Id value was returned by the Enumerator.");
			throw new CannotResolvePropertyException(ItemSchema.Id.Name);
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x00137C14 File Offset: 0x00135E14
		private List<OutlookServiceNotificationSubscription> GetOutlookServiceSubscriptions(OutlookServiceSubscriptionItemEnumeratorBase enumerable)
		{
			ArgumentValidator.ThrowIfNull("enumerable", enumerable);
			List<OutlookServiceNotificationSubscription> list = new List<OutlookServiceNotificationSubscription>();
			foreach (IStorePropertyBag storePropertyBag in enumerable)
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				using (IOutlookServiceSubscriptionItem outlookServiceSubscriptionItem = this.xsoFactory.BindToOutlookServiceSubscriptionItem(this.mailboxSession, valueOrDefault, null))
				{
					OutlookServiceNotificationSubscription item = new OutlookServiceNotificationSubscription(outlookServiceSubscriptionItem);
					list.Add(item);
				}
			}
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "OutlookServiceSubscriptionStorage.GetOutlookServiceSubscriptions: A total {0} subscription items found.", list.Count);
			return list;
		}

		// Token: 0x04003524 RID: 13604
		public static readonly uint ExpiredSubscriptionItemThresholdPolicy = (!OutlookServiceSubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize.IsUnlimited) ? (OutlookServiceSubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize.Value * 8U / 10U) : 80U;

		// Token: 0x04003525 RID: 13605
		private IFolder folder;

		// Token: 0x04003526 RID: 13606
		private IMailboxSession mailboxSession;

		// Token: 0x04003527 RID: 13607
		private IXSOFactory xsoFactory;
	}
}
