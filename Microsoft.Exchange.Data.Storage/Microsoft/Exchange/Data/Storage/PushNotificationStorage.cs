using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B04 RID: 2820
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PushNotificationStorage : DisposableObject, IPushNotificationStorage, IDisposable
	{
		// Token: 0x06006668 RID: 26216 RVA: 0x001B2700 File Offset: 0x001B0900
		private PushNotificationStorage(IFolder folder, string tenantId) : this(folder, tenantId, XSOFactory.Default)
		{
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x001B270F File Offset: 0x001B090F
		private PushNotificationStorage(IFolder folder, string tenantId, IXSOFactory xsoFactory)
		{
			ArgumentValidator.ThrowIfNull("folder", xsoFactory);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			this.folder = folder;
			this.xsoFactory = xsoFactory;
			this.TenantId = tenantId;
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x0600666A RID: 26218 RVA: 0x001B2742 File Offset: 0x001B0942
		// (set) Token: 0x0600666B RID: 26219 RVA: 0x001B274A File Offset: 0x001B094A
		public string TenantId { get; private set; }

		// Token: 0x0600666C RID: 26220 RVA: 0x001B2753 File Offset: 0x001B0953
		public static IPushNotificationStorage Create(IMailboxSession mailboxSession)
		{
			return PushNotificationStorage.Create(mailboxSession, XSOFactory.Default, OrganizationIdConvertor.Default);
		}

		// Token: 0x0600666D RID: 26221 RVA: 0x001B2765 File Offset: 0x001B0965
		public static IPushNotificationStorage Create(IMailboxSession mailboxSession, IXSOFactory xsoFactory)
		{
			return PushNotificationStorage.Create(mailboxSession, xsoFactory, OrganizationIdConvertor.Default);
		}

		// Token: 0x0600666E RID: 26222 RVA: 0x001B2773 File Offset: 0x001B0973
		public static void DeleteStorage(IMailboxSession mailboxSession)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			mailboxSession.DeleteDefaultFolder(DefaultFolderType.PushNotificationRoot, DeleteItemFlags.HardDelete);
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x001B278C File Offset: 0x001B098C
		public static IPushNotificationStorage Create(IMailboxSession mailboxSession, IXSOFactory xsoFactory, IOrganizationIdConvertor organizationIdConvertor)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			IPushNotificationStorage pushNotificationStorage = PushNotificationStorage.Find(mailboxSession, xsoFactory);
			if (pushNotificationStorage != null)
			{
				return pushNotificationStorage;
			}
			ArgumentValidator.ThrowIfNull("mailboxSession.MailboxOwner", mailboxSession.MailboxOwner);
			ArgumentValidator.ThrowIfNull("organizationIdConvertor", organizationIdConvertor);
			if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string>(0L, "PushNotificationStorage.Create: Creating a new Notification Subscription folder for user {0}.", (mailboxSession.MailboxOwner.ObjectId != null) ? mailboxSession.MailboxOwner.ObjectId.ToDNString() : string.Empty);
			}
			StoreObjectId folderId = mailboxSession.CreateDefaultFolder(DefaultFolderType.PushNotificationRoot);
			IFolder folder = xsoFactory.BindToFolder(mailboxSession, folderId);
			return new PushNotificationStorage(folder, PushNotificationStorage.GetTenantId(mailboxSession), xsoFactory);
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x001B283A File Offset: 0x001B0A3A
		public static IPushNotificationStorage Find(IMailboxSession mailboxSession)
		{
			return PushNotificationStorage.Find(mailboxSession, XSOFactory.Default);
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x001B2848 File Offset: 0x001B0A48
		public static IPushNotificationStorage Find(IMailboxSession mailboxSession, IXSOFactory xsoFactory)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.PushNotificationRoot);
			if (defaultFolderId != null)
			{
				return new PushNotificationStorage(xsoFactory.BindToFolder(mailboxSession, defaultFolderId), PushNotificationStorage.GetTenantId(mailboxSession));
			}
			return null;
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x001B288C File Offset: 0x001B0A8C
		public List<PushNotificationServerSubscription> GetActiveNotificationSubscriptions(IMailboxSession mailboxSession, uint expirationInHours = 72U)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "PushNotificationStorage.GetActiveNotificationSubscriptions: Requested active subscriptions under this folder.");
			return this.GetPushNotificationSubscriptions(mailboxSession, new ActiveSubscriptionItemEnumerator(this.folder, expirationInHours));
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x001B28C4 File Offset: 0x001B0AC4
		public List<StoreObjectId> GetExpiredNotificationSubscriptions(uint expirationInHours = 72U)
		{
			List<StoreObjectId> pushNotificationSubscriptionIds = this.GetPushNotificationSubscriptionIds(new ExpiredSubscriptionItemEnumerator(this.folder, expirationInHours, PushNotificationStorage.ExpiredSubscriptionItemThresholdPolicy + 1U));
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "PushNotificationStorage.GetExpiredNotificationSubscriptions: A total {0} expired subscription items found.", pushNotificationSubscriptionIds.Count);
			return pushNotificationSubscriptionIds;
		}

		// Token: 0x06006674 RID: 26228 RVA: 0x001B290D File Offset: 0x001B0B0D
		public List<PushNotificationServerSubscription> GetNotificationSubscriptions(IMailboxSession mailboxSession)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "PushNotificationStorage.GetNotificationSubscriptions: Requested all subscription under this folder.");
			return this.GetPushNotificationSubscriptions(mailboxSession, new SubscriptionItemEnumerator(this.folder));
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x001B2942 File Offset: 0x001B0B42
		public IPushNotificationSubscriptionItem CreateOrUpdateSubscriptionItem(IMailboxSession mailboxSession, string subscriptionId, PushNotificationServerSubscription subscription)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNullOrEmpty("subscriptionId", subscriptionId);
			ArgumentValidator.ThrowIfNull("subscription", subscription);
			return PushNotificationSubscriptionItem.CreateOrUpdateSubscription(mailboxSession, this.xsoFactory, this.folder, subscriptionId, subscription);
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x001B297C File Offset: 0x001B0B7C
		public void DeleteExpiredSubscriptions(uint expirationInHours = 72U)
		{
			StoreObjectId[] array = this.GetExpiredNotificationSubscriptions(expirationInHours).ToArray();
			if ((long)array.Length > (long)((ulong)PushNotificationStorage.ExpiredSubscriptionItemThresholdPolicy))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "PushNotificationStorage.DeleteExpiredSubscriptions: Number of expired subscription items exceeded threshold {0}.", array.Length);
				this.folder.DeleteObjects(DeleteItemFlags.HardDelete, array);
			}
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x001B29CC File Offset: 0x001B0BCC
		public void DeleteAllSubscriptions()
		{
			StoreObjectId[] array = this.GetPushNotificationSubscriptionIds(new SubscriptionItemEnumerator(this.folder)).ToArray();
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "PushNotificationStorage.DeleteAllSubscriptions: Number of subscription items to be deleted {0}.", array.Length);
			this.folder.DeleteObjects(DeleteItemFlags.HardDelete, array);
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x001B2A18 File Offset: 0x001B0C18
		public void DeleteSubscription(StoreObjectId itemId)
		{
			ArgumentValidator.ThrowIfNull("itemId", itemId);
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "PushNotificationStorage.DeleteSubscription: Requested deletion of subscription {0}.", itemId);
			this.folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
			{
				itemId
			});
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x001B2A60 File Offset: 0x001B0C60
		public void DeleteSubscription(string subscriptionId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("subscriptionId", subscriptionId);
			bool flag = false;
			foreach (IStorePropertyBag storePropertyBag in PushNotificationSubscriptionItem.GetSubscriptionById(this.folder, subscriptionId))
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

		// Token: 0x0600667A RID: 26234 RVA: 0x001B2B18 File Offset: 0x001B0D18
		internal static PushNotificationStorage GetNotificationFolderRoot(Folder folder)
		{
			return new PushNotificationStorage(folder, PushNotificationStorage.GetTenantId(folder.Session));
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x001B2B2B File Offset: 0x001B0D2B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PushNotificationStorage>(this);
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x001B2B33 File Offset: 0x001B0D33
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x0600667D RID: 26237 RVA: 0x001B2B5C File Offset: 0x001B0D5C
		private List<StoreObjectId> GetPushNotificationSubscriptionIds(SubscriptionItemEnumeratorBase enumerable)
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			foreach (IStorePropertyBag propertyBag in enumerable)
			{
				list.Add(this.GetVersionedId(propertyBag).ObjectId);
			}
			return list;
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x001B2BB8 File Offset: 0x001B0DB8
		private VersionedId GetVersionedId(IStorePropertyBag propertyBag)
		{
			VersionedId valueOrDefault = propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			if (valueOrDefault != null)
			{
				return valueOrDefault;
			}
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError((long)this.GetHashCode(), "PushNotificationStorage.GetPushNotificationSubscriptionIds: A subscription with an empty ItemSchema.Id value was returned by the Enumerator.");
			throw new CannotResolvePropertyException(ItemSchema.Id.Name);
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x001B2BFC File Offset: 0x001B0DFC
		private List<PushNotificationServerSubscription> GetPushNotificationSubscriptions(IMailboxSession mailboxSession, SubscriptionItemEnumeratorBase enumerable)
		{
			ArgumentValidator.ThrowIfNull("enumerable", enumerable);
			List<PushNotificationServerSubscription> list = new List<PushNotificationServerSubscription>();
			foreach (IStorePropertyBag propertyBag in enumerable)
			{
				string serializedNotificationSubscription = PushNotificationStorage.GetSerializedNotificationSubscription(mailboxSession, propertyBag, this.xsoFactory);
				list.Add(PushNotificationServerSubscription.FromJson(serializedNotificationSubscription));
			}
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<int>((long)this.GetHashCode(), "PushNotificationStorage.GetPushNotificationSubscriptions: A total {0} subscription items found.", list.Count);
			return list;
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x001B2C88 File Offset: 0x001B0E88
		private static string GetTenantId(IStoreSession session)
		{
			string result = string.Empty;
			byte[] valueOrDefault = session.Mailbox.GetValueOrDefault<byte[]>(MailboxSchema.PersistableTenantPartitionHint, null);
			if (valueOrDefault != null && valueOrDefault.Length > 0)
			{
				TenantPartitionHint tenantPartitionHint = TenantPartitionHint.FromPersistablePartitionHint(valueOrDefault);
				Guid externalDirectoryOrganizationId = tenantPartitionHint.GetExternalDirectoryOrganizationId();
				result = (Guid.Empty.Equals(externalDirectoryOrganizationId) ? string.Empty : externalDirectoryOrganizationId.ToString());
			}
			return result;
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x001B2CEC File Offset: 0x001B0EEC
		public static string GetSerializedNotificationSubscription(IMailboxSession mailboxSession, IStorePropertyBag propertyBag, IXSOFactory xsoFactory)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription, null);
			if (string.IsNullOrWhiteSpace(valueOrDefault))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError(0L, "PushNotificationStorage.GetSerializedNotificationSubscription: A subscription with an empty serialized value was returned by the Enumerator.");
				throw new CannotResolvePropertyException(PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription.Name);
			}
			if (valueOrDefault.Length < 255)
			{
				return valueOrDefault;
			}
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug(0L, "PushNotificationStorage.GetSerializedNotificationSubscription: We need to bind to the item in order to obtain the full serialized notification subscription.");
			VersionedId valueOrDefault2 = propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			string result;
			using (IPushNotificationSubscriptionItem pushNotificationSubscriptionItem = xsoFactory.BindToPushNotificationSubscriptionItem(mailboxSession, valueOrDefault2, new PropertyDefinition[]
			{
				PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription
			}))
			{
				string serializedNotificationSubscription = pushNotificationSubscriptionItem.SerializedNotificationSubscription;
				if (string.IsNullOrWhiteSpace(serializedNotificationSubscription))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<VersionedId, string>(0L, "PushNotificationStorage.GetFullSerializedNotificationSubscription: Unable to obtain the full SerializedNotificationSubscription from {0}, partial value: {1}", valueOrDefault2, valueOrDefault);
					throw new CannotResolvePropertyException(PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription.Name);
				}
				result = serializedNotificationSubscription;
			}
			return result;
		}

		// Token: 0x04003A24 RID: 14884
		public const uint DefaultSubscriptionExpirationInHours = 72U;

		// Token: 0x04003A25 RID: 14885
		public static readonly uint ExpiredSubscriptionItemThresholdPolicy = (!SubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize.IsUnlimited) ? (SubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize.Value * 8U / 10U) : 80U;

		// Token: 0x04003A26 RID: 14886
		private IFolder folder;

		// Token: 0x04003A27 RID: 14887
		private IXSOFactory xsoFactory;
	}
}
