using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CacheDataProvider : IConfigDataProvider
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000BF02 File Offset: 0x0000A102
		public CacheDataProvider(SubscriptionCacheAction cacheAction, ExchangePrincipal userPrincipal)
		{
			SyncUtilities.ThrowIfArgumentNull("userPrincipal", userPrincipal);
			this.cacheAction = cacheAction;
			this.userPrincipal = userPrincipal;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000BF23 File Offset: 0x0000A123
		public string Source
		{
			get
			{
				return "SubscriptionCacheDataProvider";
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000BF2A File Offset: 0x0000A12A
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			return this.GetCacheForRead();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000BF34 File Offset: 0x0000A134
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			SubscriptionsCache cacheForRead = this.GetCacheForRead();
			return new IConfigurable[]
			{
				cacheForRead
			};
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000BF54 File Offset: 0x0000A154
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			IConfigurable cacheForRead = this.GetCacheForRead();
			return new T[]
			{
				(T)((object)cacheForRead)
			};
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000BF80 File Offset: 0x0000A180
		public void Save(IConfigurable instance)
		{
			SubscriptionsCache subscriptionsCache = (SubscriptionsCache)instance;
			this.TestUserCache(subscriptionsCache);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000BF9C File Offset: 0x0000A19C
		public void Delete(IConfigurable instance)
		{
			SubscriptionsCache subscriptionsCache = (SubscriptionsCache)instance;
			this.TestUserCache(subscriptionsCache);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		private SubscriptionsCache GetCacheForRead()
		{
			SubscriptionsCache subscriptionsCache = new SubscriptionsCache();
			subscriptionsCache.SetIdentity(this.userPrincipal.ObjectId);
			if (this.cacheAction == SubscriptionCacheAction.None || this.cacheAction == SubscriptionCacheAction.Validate)
			{
				this.TestUserCache(subscriptionsCache);
			}
			return subscriptionsCache;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		private void TestUserCache(SubscriptionsCache subscriptionsCache)
		{
			string serverFqdn = this.userPrincipal.MailboxInfo.Location.ServerFqdn;
			string primarySmtpAddress = this.userPrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			string failureReason;
			uint num;
			List<SubscriptionCacheObject> subscriptionCacheObjects;
			ObjectState objectState;
			if (!SubscriptionCacheClient.TryTestUserCache(serverFqdn, primarySmtpAddress, this.cacheAction, out failureReason, out num, out subscriptionCacheObjects, out objectState))
			{
				subscriptionsCache.FailureReason = failureReason;
				return;
			}
			uint num2 = num;
			if (num2 != 0U)
			{
				switch (num2)
				{
				case 268435456U:
					subscriptionsCache.FailureReason = Strings.CacheRpcServerFailed(serverFqdn, failureReason);
					break;
				case 268435457U:
					subscriptionsCache.FailureReason = Strings.CacheRpcServerStopped(serverFqdn);
					break;
				case 268435458U:
					subscriptionsCache.FailureReason = Strings.CacheRpcInvalidServerVersionIssue(serverFqdn);
					break;
				default:
					throw new InvalidObjectOperationException(Strings.InvalidCacheActionResult(num));
				}
			}
			subscriptionsCache.SubscriptionCacheObjects = subscriptionCacheObjects;
			subscriptionsCache.propertyBag.SetField(SimpleProviderObjectSchema.ObjectState, objectState);
			subscriptionsCache.propertyBag.ResetChangeTracking(SimpleProviderObjectSchema.ObjectState);
		}

		// Token: 0x040000BA RID: 186
		private SubscriptionCacheAction cacheAction;

		// Token: 0x040000BB RID: 187
		private ExchangePrincipal userPrincipal;
	}
}
