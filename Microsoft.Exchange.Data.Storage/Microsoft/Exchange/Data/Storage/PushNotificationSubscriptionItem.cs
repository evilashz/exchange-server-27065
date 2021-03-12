using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B05 RID: 2821
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PushNotificationSubscriptionItem : Item, IPushNotificationSubscriptionItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06006683 RID: 26243 RVA: 0x001B2E03 File Offset: 0x001B1003
		internal PushNotificationSubscriptionItem(ICoreItem coreItem) : base(coreItem, false)
		{
			if (base.IsNew)
			{
				this.Initialize();
			}
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x06006684 RID: 26244 RVA: 0x001B2E1B File Offset: 0x001B101B
		// (set) Token: 0x06006685 RID: 26245 RVA: 0x001B2E34 File Offset: 0x001B1034
		public string SubscriptionId
		{
			get
			{
				this.CheckDisposed("Type::get");
				return base.GetValueOrDefault<string>(PushNotificationSubscriptionItemSchema.SubscriptionId, null);
			}
			set
			{
				this.CheckDisposed("Type::set");
				this[PushNotificationSubscriptionItemSchema.SubscriptionId] = value;
			}
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x06006686 RID: 26246 RVA: 0x001B2E4D File Offset: 0x001B104D
		// (set) Token: 0x06006687 RID: 26247 RVA: 0x001B2E6A File Offset: 0x001B106A
		public ExDateTime LastUpdateTimeUTC
		{
			get
			{
				this.CheckDisposed("Type::get");
				return base.GetValueOrDefault<ExDateTime>(PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC, ExDateTime.UtcNow);
			}
			set
			{
				this.CheckDisposed("Type::set");
				this[PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC] = value;
			}
		}

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06006688 RID: 26248 RVA: 0x001B2E88 File Offset: 0x001B1088
		// (set) Token: 0x06006689 RID: 26249 RVA: 0x001B2EA1 File Offset: 0x001B10A1
		public string SerializedNotificationSubscription
		{
			get
			{
				this.CheckDisposed("Type::get");
				return base.GetValueOrDefault<string>(PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription, null);
			}
			set
			{
				this.CheckDisposed("Type::set");
				this[PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription] = value;
			}
		}

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x0600668A RID: 26250 RVA: 0x001B2EBA File Offset: 0x001B10BA
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return PushNotificationSubscriptionItemSchema.Instance;
			}
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x001B2ECC File Offset: 0x001B10CC
		public static IPushNotificationSubscriptionItem CreateOrUpdateSubscription(IMailboxSession session, IXSOFactory xsoFactory, IFolder folder, string subscriptionId, PushNotificationServerSubscription subscription)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			Util.ThrowOnNullArgument(folder, "folder");
			Util.ThrowOnNullOrEmptyArgument(subscriptionId, "subscriptionId");
			Util.ThrowOnNullArgument(subscription, "subscription");
			ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string, IExchangePrincipal>((long)subscription.GetHashCode(), "PushNotificationSubscriptionItem.CreateOrUpdateSubscription: Searching for Subscription {0} on Mailbox {1}.", subscriptionId, session.MailboxOwner);
			IStorePropertyBag[] array = PushNotificationSubscriptionItem.GetSubscriptionById(folder, subscriptionId).ToArray<IStorePropertyBag>();
			IPushNotificationSubscriptionItem pushNotificationSubscriptionItem = null;
			try
			{
				if (array.Length >= 1)
				{
					if (array.Length > 1)
					{
						ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceWarning<string, Guid>(0L, "PushNotificationSubscriptionItem.CreateOrUpdateSubscription: AmbiguousSubscription for subscription {0} and user {1}", subscriptionId, session.MailboxGuid);
					}
					IStorePropertyBag storePropertyBag = array[0];
					VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
					if (valueOrDefault == null)
					{
						ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<string>((long)storePropertyBag.GetHashCode(), "PushNotificationSubscriptionItem.CreateOrUpdateSubscription: Cannot resolve the ItemSchema.Id property from the Enumerable.", subscriptionId);
						throw new CannotResolvePropertyException(ItemSchema.Id.Name);
					}
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<VersionedId>((long)storePropertyBag.GetHashCode(), "PushNotificationSubscriptionItem.CreateOrUpdateSubscription: Found one existing subscription with ItemSchema.Id = {0}.", valueOrDefault);
					pushNotificationSubscriptionItem = xsoFactory.BindToPushNotificationSubscriptionItem(session, valueOrDefault, null);
					pushNotificationSubscriptionItem.LastUpdateTimeUTC = ExDateTime.UtcNow;
					subscription.LastSubscriptionUpdate = (DateTime)pushNotificationSubscriptionItem.LastUpdateTimeUTC;
					pushNotificationSubscriptionItem.SerializedNotificationSubscription = subscription.ToJson();
					ConflictResolutionResult conflictResolutionResult = pushNotificationSubscriptionItem.Save(SaveMode.ResolveConflicts);
					if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
					{
						ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<string>((long)storePropertyBag.GetHashCode(), "PushNotificationSubscriptionItem.CreateOrUpdateSubscription: Save failed due to conflicts for subscription {0}.", subscriptionId);
						throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(subscriptionId));
					}
					pushNotificationSubscriptionItem.Load(SubscriptionItemEnumeratorBase.PushNotificationSubscriptionItemProperties);
				}
				else
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)subscription.GetHashCode(), "PushNotificationSubscriptionItem.CreateOrUpdateSubscription: Cannot resolve given subscription, about to create a new SubscriptionItem.");
					pushNotificationSubscriptionItem = PushNotificationSubscriptionItem.Create(session, xsoFactory, folder.StoreObjectId, subscriptionId, subscription);
				}
			}
			catch
			{
				if (pushNotificationSubscriptionItem != null)
				{
					pushNotificationSubscriptionItem.Dispose();
				}
				throw;
			}
			return pushNotificationSubscriptionItem;
		}

		// Token: 0x0600668C RID: 26252 RVA: 0x001B3084 File Offset: 0x001B1284
		public static string GenerateSubscriptionId(string protocol, string deviceId, string deviceType)
		{
			return string.Format("{0}-{1}-{2}", protocol, deviceType, deviceId);
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x001B30D0 File Offset: 0x001B12D0
		public static IEnumerable<IStorePropertyBag> GetSubscriptionById(IFolder folder, string subscriptionId)
		{
			Util.ThrowOnNullArgument(folder, "folder");
			Util.ThrowOnNullOrEmptyArgument(subscriptionId, "subscriptionId");
			return new SubscriptionItemEnumerator(folder).Where(delegate(IStorePropertyBag x)
			{
				string valueOrDefault = x.GetValueOrDefault<string>(PushNotificationSubscriptionItemSchema.SubscriptionId, null);
				return !string.IsNullOrEmpty(valueOrDefault) && valueOrDefault.Equals(subscriptionId);
			});
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x001B311C File Offset: 0x001B131C
		private static IPushNotificationSubscriptionItem Create(IMailboxSession session, IXSOFactory xsoFactory, StoreId locationFolderId, string subscriptionId, PushNotificationServerSubscription subscription)
		{
			IPushNotificationSubscriptionItem pushNotificationSubscriptionItem = null;
			try
			{
				pushNotificationSubscriptionItem = xsoFactory.CreatePushNotificationSubscriptionItem(session, locationFolderId);
				subscription.LastSubscriptionUpdate = (DateTime)pushNotificationSubscriptionItem.LastUpdateTimeUTC;
				pushNotificationSubscriptionItem.SubscriptionId = subscriptionId;
				pushNotificationSubscriptionItem.SerializedNotificationSubscription = subscription.ToJson();
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string, ExDateTime, string>((long)subscription.GetHashCode(), "PushNotificationSubscriptionItem.Create: Created SubscriptionItem on store, Id:{0}, RefTm:{1}, Json:{2}", pushNotificationSubscriptionItem.SubscriptionId, pushNotificationSubscriptionItem.LastUpdateTimeUTC, pushNotificationSubscriptionItem.SerializedNotificationSubscription);
				}
				ConflictResolutionResult conflictResolutionResult = pushNotificationSubscriptionItem.Save(SaveMode.FailOnAnyConflict);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<string>((long)pushNotificationSubscriptionItem.GetHashCode(), "PushNotificationSubscriptionItem.Create: Save failed due to conflicts for subscription {0}.", subscriptionId);
					throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(pushNotificationSubscriptionItem.SubscriptionId));
				}
				pushNotificationSubscriptionItem.Load(SubscriptionItemEnumeratorBase.PushNotificationSubscriptionItemProperties);
			}
			catch
			{
				if (pushNotificationSubscriptionItem != null)
				{
					pushNotificationSubscriptionItem.Dispose();
				}
				throw;
			}
			return pushNotificationSubscriptionItem;
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x001B31F4 File Offset: 0x001B13F4
		private void Initialize()
		{
			this[InternalSchema.ItemClass] = "Exchange.PushNotification.Subscription";
			this[PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC] = ExDateTime.UtcNow;
			if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string>((long)this.GetHashCode(), "PushNotificationSubscriptionItem.Initialize: Initialized new SubscriptionItem, RefTm:{1}", this[PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC].ToString());
			}
		}
	}
}
