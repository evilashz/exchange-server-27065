using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000110 RID: 272
	internal class TokenRenewSubmitter
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0002EAC8 File Offset: 0x0002CCC8
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
		internal int GetTokensCount { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002EAD9 File Offset: 0x0002CCD9
		internal int QueryQueueCount
		{
			get
			{
				return this.queryQueue.Count;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002EAE6 File Offset: 0x0002CCE6
		internal TokenRenewSubmitter(OmexWebServiceUrlsCache urlsCache)
		{
			if (urlsCache == null)
			{
				throw new ArgumentNullException("urlsCache");
			}
			this.urlsCache = urlsCache;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002EB20 File Offset: 0x0002CD20
		internal void SubmitRenewQuery(ICollection<ExtensionData> extensions, TokenRenewQueryContext queryContext)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			if (extensions.Count == 0)
			{
				throw new ArgumentException("extensions must contain one or more extensions");
			}
			List<TokenRenewRequestAsset> list = new List<TokenRenewRequestAsset>(extensions.Count);
			foreach (ExtensionData extensionData in extensions)
			{
				list.Add(new TokenRenewRequestAsset
				{
					MarketplaceContentMarket = extensionData.MarketplaceContentMarket,
					ExtensionID = extensionData.ExtensionId,
					MarketplaceAssetID = extensionData.MarketplaceAssetID,
					Scope = extensionData.Scope.Value,
					Etoken = extensionData.Etoken
				});
			}
			if (list.Count == 0)
			{
				TokenRenewSubmitter.Tracer.TraceDebug(0L, "ExtensionsCache.SubmitRenewQuery: TokenRenewRequestAssets count is 0. Token renew query will not be started.");
				return;
			}
			queryContext.TokenRenewRequestAssets = list;
			queryContext.DeploymentId = ExtensionDataHelper.GetDeploymentId(queryContext.Domain);
			this.QueueQueryItem(queryContext);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002EC1C File Offset: 0x0002CE1C
		internal void QueueQueryItem(TokenRenewQueryContext queryContext)
		{
			GetTokens getTokens = null;
			lock (this.queryQueueLockObject)
			{
				if (this.queryQueue.Count > 500)
				{
					TokenRenewSubmitter.Tracer.TraceError<IExchangePrincipal>(0L, "Query for {0} not added to the query queue because queue is full.", queryContext.ExchangePrincipal);
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ExtensionTokenQueryMaxExceeded, null, new object[]
					{
						"ProcessTokenRenew",
						ExtensionDiagnostics.GetLoggedMailboxIdentifier(queryContext.ExchangePrincipal)
					});
					return;
				}
				TokenRenewSubmitter.Tracer.TraceDebug<IExchangePrincipal>(0L, "Adding query for {0} to the query queue.", queryContext.ExchangePrincipal);
				this.queryQueue.Enqueue(queryContext);
				if (this.GetTokensCount < 50)
				{
					getTokens = new GetTokens(this.urlsCache, this);
					this.GetTokensCount++;
					TokenRenewSubmitter.Tracer.TraceDebug<int>(0L, "Creating a new instance of GetTokens. GetTokens Count {0}", this.GetTokensCount);
				}
				else
				{
					TokenRenewSubmitter.Tracer.TraceDebug<int>(0L, "Too many GetTokens commands. Query will be handled from pool. GetTokens Count {0}", this.GetTokensCount);
				}
			}
			if (getTokens != null)
			{
				this.ExecuteTokenRenewQuery(getTokens);
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002ED38 File Offset: 0x0002CF38
		internal void ExecuteTokenRenewQuery(GetTokens getTokens)
		{
			TokenRenewQueryContext tokenRenewQueryContext = null;
			lock (this.queryQueueLockObject)
			{
				if (this.queryQueue.Count > 0)
				{
					tokenRenewQueryContext = this.queryQueue.Dequeue();
				}
				else
				{
					this.GetTokensCount--;
					if (this.GetTokensCount < 0)
					{
						throw new InvalidOperationException("GetTokensCount can't be less than 0.");
					}
					TokenRenewSubmitter.Tracer.TraceDebug<int>(0L, "Query queue is empty. GetTokens Count {0}", this.GetTokensCount);
				}
			}
			if (tokenRenewQueryContext != null)
			{
				TokenRenewSubmitter.Tracer.TraceDebug<IExchangePrincipal>(0L, "Starting query for {0}.", tokenRenewQueryContext.ExchangePrincipal);
				getTokens.Execute(tokenRenewQueryContext);
			}
		}

		// Token: 0x040005C7 RID: 1479
		private const string ScenarioProcessTokenRenew = "ProcessTokenRenew";

		// Token: 0x040005C8 RID: 1480
		private const int QueryQueueMaxCount = 500;

		// Token: 0x040005C9 RID: 1481
		internal const int MaxGetTokensCount = 50;

		// Token: 0x040005CA RID: 1482
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x040005CB RID: 1483
		private OmexWebServiceUrlsCache urlsCache;

		// Token: 0x040005CC RID: 1484
		private object queryQueueLockObject = new object();

		// Token: 0x040005CD RID: 1485
		private Queue<TokenRenewQueryContext> queryQueue = new Queue<TokenRenewQueryContext>(500);
	}
}
