using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200021F RID: 543
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SyncStorageProviderFactory
	{
		// Token: 0x06001354 RID: 4948 RVA: 0x00041930 File Offset: 0x0003FB30
		internal static void Register(ISyncStorageProvider provider, AggregationSubscriptionType subscriptionType)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			lock (SyncStorageProviderFactory.syncStorageProviders)
			{
				if (!SyncStorageProviderFactory.registrationAllowed)
				{
					throw new InvalidOperationException("Registration attempted when it was explicitly disabled by the consuming code");
				}
				if (!SyncStorageProviderFactory.syncStorageProviders.ContainsKey((int)subscriptionType))
				{
					if (!Enum.IsDefined(typeof(AggregationSubscriptionType), subscriptionType))
					{
						throw new ArgumentOutOfRangeException("subscriptionType");
					}
					SyncStorageProviderFactory.syncStorageProviders[(int)subscriptionType] = provider;
				}
				else if (SyncStorageProviderFactory.syncStorageProviders[(int)subscriptionType].GetType() != provider.GetType())
				{
					throw new InvalidOperationException("Already a different kind of sync storage provider is registered for this aggregation type.");
				}
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000419F0 File Offset: 0x0003FBF0
		internal static void Unregister(AggregationSubscriptionType subscriptionType)
		{
			lock (SyncStorageProviderFactory.syncStorageProviders)
			{
				if (SyncStorageProviderFactory.syncStorageProviders.ContainsKey((int)subscriptionType))
				{
					SyncStorageProviderFactory.syncStorageProviders.Remove((int)subscriptionType);
				}
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00041A44 File Offset: 0x0003FC44
		internal static void DisableRegistration()
		{
			lock (SyncStorageProviderFactory.syncStorageProviders)
			{
				SyncStorageProviderFactory.registrationAllowed = false;
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00041A84 File Offset: 0x0003FC84
		internal static void EnableRegistration()
		{
			lock (SyncStorageProviderFactory.syncStorageProviders)
			{
				SyncStorageProviderFactory.registrationAllowed = true;
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00041AC4 File Offset: 0x0003FCC4
		internal static NativeSyncStorageProvider CreateNativeSyncStorageProvider(ISyncWorkerData subscription)
		{
			NativeSyncStorageProvider result;
			if (subscription.IsMigration || subscription.IsMirrored || SyncUtilities.IsContactSubscriptionType(subscription.SubscriptionType))
			{
				result = SyncStorageProviderFactory.XSOSyncStorageProvider;
			}
			else
			{
				result = SyncStorageProviderFactory.TransportSyncStorageProvider;
			}
			return result;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00041B00 File Offset: 0x0003FD00
		internal static ISyncStorageProvider CreateCloudSyncStorageProvider(ISyncWorkerData subscription)
		{
			ISyncStorageProvider result = null;
			lock (SyncStorageProviderFactory.syncStorageProviders)
			{
				if (SyncStorageProviderFactory.syncStorageProviders.ContainsKey((int)subscription.SubscriptionType))
				{
					result = SyncStorageProviderFactory.syncStorageProviders[(int)subscription.SubscriptionType];
				}
			}
			return result;
		}

		// Token: 0x04000A38 RID: 2616
		private static readonly TransportSyncStorageProvider TransportSyncStorageProvider = new TransportSyncStorageProvider();

		// Token: 0x04000A39 RID: 2617
		private static readonly XSOSyncStorageProvider XSOSyncStorageProvider = new XSOSyncStorageProvider();

		// Token: 0x04000A3A RID: 2618
		private static Dictionary<int, ISyncStorageProvider> syncStorageProviders = new Dictionary<int, ISyncStorageProvider>(Enum.GetNames(typeof(AggregationSubscriptionType)).Length);

		// Token: 0x04000A3B RID: 2619
		private static bool registrationAllowed = true;
	}
}
