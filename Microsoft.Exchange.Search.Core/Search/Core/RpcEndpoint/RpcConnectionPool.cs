using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.RpcEndpoint
{
	// Token: 0x020000B6 RID: 182
	internal class RpcConnectionPool
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		private RpcConnectionPool()
		{
			RpcConnectionPool.tracingContext = this.GetHashCode();
			this.searchConfig = SearchConfig.Instance;
			RpcConnectionPool.maxCacheSize = this.searchConfig.DocumentTrackingMaxClientCacheSize;
			this.rpcClientCache = new ExactTimeoutCache<int, SearchServiceRpcClient>(new RemoveItemDelegate<int, SearchServiceRpcClient>(RpcConnectionPool.RemoveFromCacheCallback), null, null, RpcConnectionPool.maxCacheSize, true, CacheFullBehavior.ExpireExisting);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00011D04 File Offset: 0x0000FF04
		internal static SearchServiceRpcClient GetSearchRpcClient()
		{
			SearchServiceRpcClient searchServiceRpcClient;
			lock (RpcConnectionPool.lockObject)
			{
				if (!RpcConnectionPool.TryGetRpcClientFromCache(out searchServiceRpcClient))
				{
					RpcConnectionPool.tracer.TraceDebug((long)RpcConnectionPool.tracingContext, "RpcConnectionPool: Creating new RpcClient.");
					searchServiceRpcClient = new SearchServiceRpcClient("localhost");
				}
			}
			if (searchServiceRpcClient == null)
			{
				throw new InvalidOperationException("RPC client pool returned null client.");
			}
			return searchServiceRpcClient;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00011D78 File Offset: 0x0000FF78
		internal static void ReturnSearchRpcClientToCache(ref SearchServiceRpcClient rpcClient, bool discard)
		{
			if (rpcClient == null)
			{
				throw new ArgumentNullException("rpcClient");
			}
			bool flag = false;
			try
			{
				if (!discard)
				{
					lock (RpcConnectionPool.lockObject)
					{
						if (RpcConnectionPool.instance.rpcClientCache.Count < RpcConnectionPool.maxCacheSize)
						{
							int key = Interlocked.Increment(ref RpcConnectionPool.globalCacheId);
							flag = RpcConnectionPool.instance.rpcClientCache.TryAddSliding(key, rpcClient, RpcConnectionPool.instance.searchConfig.DocumentTrackingClientCacheTimeout);
						}
						if (flag)
						{
							RpcConnectionPool.tracer.TraceDebug<int>((long)RpcConnectionPool.tracingContext, "RpcConnectionPool: RpcClient returned to the cache. Current Cache size: {0}", RpcConnectionPool.instance.rpcClientCache.Count);
						}
						else
						{
							RpcConnectionPool.tracer.TraceDebug<int>((long)RpcConnectionPool.tracingContext, "RpcConnectionPool: RpcClient discarded - Cache Full: {0}", RpcConnectionPool.instance.rpcClientCache.Count);
						}
						goto IL_D8;
					}
				}
				RpcConnectionPool.tracer.TraceDebug((long)RpcConnectionPool.tracingContext, "RpcConnectionPool: RpcClient discarded - User Instructed.");
				IL_D8:;
			}
			finally
			{
				if (!flag)
				{
					rpcClient.Dispose();
					rpcClient = null;
				}
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00011E8C File Offset: 0x0001008C
		internal static void RegisterCaller()
		{
			Interlocked.Increment(ref RpcConnectionPool.currentReferenceCount);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00011E9C File Offset: 0x0001009C
		internal static void UnRegisterCaller()
		{
			if (Interlocked.Decrement(ref RpcConnectionPool.currentReferenceCount) == 0)
			{
				lock (RpcConnectionPool.lockObject)
				{
					SearchServiceRpcClient searchServiceRpcClient;
					while (RpcConnectionPool.TryGetRpcClientFromCache(out searchServiceRpcClient))
					{
						searchServiceRpcClient.Dispose();
					}
				}
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00011EF4 File Offset: 0x000100F4
		private static bool TryGetRpcClientFromCache(out SearchServiceRpcClient rpcClient)
		{
			rpcClient = null;
			List<int> list = new List<int>(RpcConnectionPool.instance.rpcClientCache.Keys);
			if (list.Count <= 0)
			{
				RpcConnectionPool.tracer.TraceDebug((long)RpcConnectionPool.tracingContext, "RpcConnectionPool: Cache miss due to no available RpcClients in the cache.");
				return false;
			}
			rpcClient = RpcConnectionPool.instance.rpcClientCache.Remove(list[0]);
			if (rpcClient != null)
			{
				RpcConnectionPool.tracer.TraceDebug<int>((long)RpcConnectionPool.tracingContext, "RpcConnectionPool: RpcClient checked out from the cache. Current Cache size: {0}", RpcConnectionPool.instance.rpcClientCache.Count);
				return true;
			}
			throw new InvalidOperationException(string.Format("Null value was returned from RPC connection pool. Failed to Remove an RpcClient from the ExactTimeoutCache with a Key provided by the ExactTimeoutCache. Cache.Count: {0}, Cache.Keys.Count: {1}", RpcConnectionPool.instance.rpcClientCache.Count, RpcConnectionPool.instance.rpcClientCache.Keys.Count));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00011FB8 File Offset: 0x000101B8
		private static void RemoveFromCacheCallback(int key, SearchServiceRpcClient rpcClient, RemoveReason reason)
		{
			if (reason == RemoveReason.Removed)
			{
				return;
			}
			lock (RpcConnectionPool.lockObject)
			{
				RpcConnectionPool.tracer.TraceDebug<int>((long)RpcConnectionPool.tracingContext, "RpcConnectionPool: RpcClient discarded - Cache Timeout. Current Cache size: {0}", RpcConnectionPool.instance.rpcClientCache.Count);
			}
			rpcClient.Dispose();
		}

		// Token: 0x04000282 RID: 642
		private static readonly Trace tracer = ExTraceGlobals.SearchRpcClientTracer;

		// Token: 0x04000283 RID: 643
		private static readonly RpcConnectionPool instance = new RpcConnectionPool();

		// Token: 0x04000284 RID: 644
		private static readonly object lockObject = new object();

		// Token: 0x04000285 RID: 645
		private static int tracingContext;

		// Token: 0x04000286 RID: 646
		private static int globalCacheId;

		// Token: 0x04000287 RID: 647
		private static int maxCacheSize;

		// Token: 0x04000288 RID: 648
		private static int currentReferenceCount;

		// Token: 0x04000289 RID: 649
		private readonly SearchConfig searchConfig;

		// Token: 0x0400028A RID: 650
		private ExactTimeoutCache<int, SearchServiceRpcClient> rpcClientCache;
	}
}
