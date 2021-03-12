using System;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001F3 RID: 499
	internal class MailboxSessionCache : LazyLookupTimeoutCache<ExchangePrincipal, StoreSession>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000D1B RID: 3355 RVA: 0x00037428 File Offset: 0x00035628
		// (remove) Token: 0x06000D1C RID: 3356 RVA: 0x00037460 File Offset: 0x00035660
		internal event MailboxSessionCache.AddedToCacheDelegate OnAddedToCached;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000D1D RID: 3357 RVA: 0x00037498 File Offset: 0x00035698
		// (remove) Token: 0x06000D1E RID: 3358 RVA: 0x000374D0 File Offset: 0x000356D0
		internal event MailboxSessionCache.RemovedFromCacheDelegate OnRemovedFromCache;

		// Token: 0x06000D1F RID: 3359 RVA: 0x00037514 File Offset: 0x00035714
		public MailboxSessionCache(int cacheSize, GenericIdentity executingUserIdentity, Guid queryCorrelationId, TimeSpan cacheExpiryPeriod, MailboxSessionCache.CreateSessionHandler createSessionHandler = null) : base(1, cacheSize, true, cacheExpiryPeriod)
		{
			this.executingUserIdentity = executingUserIdentity;
			this.queryCorrelationId = queryCorrelationId;
			AppDomain.CurrentDomain.DomainUnload += this.HandleDomainUnload;
			AppDomain.CurrentDomain.ProcessExit += this.HandleProcessExit;
			if (createSessionHandler == null)
			{
				this.OnCreateSession = ((ExchangePrincipal mailboxIdentity) => SearchUtils.OpenSession(mailboxIdentity, this.ExecutingUserGenericIdentity, false));
				return;
			}
			this.OnCreateSession = createSessionHandler;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0003758C File Offset: 0x0003578C
		internal int ItemsInCache
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00037594 File Offset: 0x00035794
		internal GenericIdentity ExecutingUserGenericIdentity
		{
			get
			{
				return this.executingUserIdentity;
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003759C File Offset: 0x0003579C
		protected override StoreSession CreateOnCacheMiss(ExchangePrincipal userPrincipal, ref bool shouldAdd)
		{
			shouldAdd = true;
			Factory.Current.LocalTaskTracer.TraceInformation<Guid, string>(this.GetHashCode(), 0L, "Correlation Id:{0}. MailboxSession for {1} not found in the SessionCache, creating one and adding it to the cache.", this.queryCorrelationId, userPrincipal.ToString());
			StoreSession result = this.OnCreateSession(userPrincipal);
			if (this.OnAddedToCached != null)
			{
				this.OnAddedToCached(userPrincipal);
			}
			return result;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000375F8 File Offset: 0x000357F8
		protected override void HandleRemove(ExchangePrincipal key, StoreSession value, RemoveReason reason)
		{
			Factory.Current.LocalTaskTracer.TraceInformation<Guid, string, string>(this.GetHashCode(), 0L, "Correlation Id:{0}. Removing MailboxSession for mailbox:{1} from the cache, removal reason is {2}", this.queryCorrelationId, key.ToString(), reason.ToString());
			base.HandleRemove(key, value, reason);
			if (this.OnRemovedFromCache != null)
			{
				this.OnRemovedFromCache(key, reason);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00037656 File Offset: 0x00035856
		protected override void CleanupValue(ExchangePrincipal key, StoreSession value)
		{
			Factory.Current.LocalTaskTracer.TraceInformation<Guid, string>(this.GetHashCode(), 0L, "Correlation Id:{0}. Cleanup called for MailboxSession for mailbox:{1} from the cache.", this.queryCorrelationId, key.ToString());
			if (value != null)
			{
				value.Dispose();
				value = null;
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003768C File Offset: 0x0003588C
		private void HandleDomainUnload(object sender, EventArgs e)
		{
			AppDomain.CurrentDomain.ProcessExit -= this.HandleProcessExit;
			this.Dispose();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000376AA File Offset: 0x000358AA
		private void HandleProcessExit(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x04000934 RID: 2356
		private readonly GenericIdentity executingUserIdentity;

		// Token: 0x04000935 RID: 2357
		private readonly Guid queryCorrelationId;

		// Token: 0x04000936 RID: 2358
		private MailboxSessionCache.CreateSessionHandler OnCreateSession;

		// Token: 0x020001F4 RID: 500
		// (Invoke) Token: 0x06000D29 RID: 3369
		internal delegate void AddedToCacheDelegate(ExchangePrincipal mailboxIdentity);

		// Token: 0x020001F5 RID: 501
		// (Invoke) Token: 0x06000D2D RID: 3373
		internal delegate void RemovedFromCacheDelegate(ExchangePrincipal mailboxIdentity, RemoveReason removeReason);

		// Token: 0x020001F6 RID: 502
		// (Invoke) Token: 0x06000D31 RID: 3377
		internal delegate StoreSession CreateSessionHandler(ExchangePrincipal mailboxIdentity);
	}
}
