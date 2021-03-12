using System;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Diagnostics;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.DirectoryCache;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A8 RID: 168
	internal class ExchangeDirectoryCacheProvider : IDirectoryCacheProvider
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x00028FA2 File Offset: 0x000271A2
		public ExchangeDirectoryCacheProvider()
		{
			ExchangeDirectoryCacheProvider.InitializeProxyPoolIfRequired();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00029084 File Offset: 0x00027284
		public TObject Get<TObject>(DirectoryCacheRequest cacheRequest) where TObject : ADRawEntry, new()
		{
			ArgumentValidator.ThrowIfNull("cacheRequest", cacheRequest);
			CacheUtils.PopulateAndCheckObjectType<TObject>(cacheRequest);
			SimpleADObject sADO = null;
			int retryCount = 0;
			long apiTime = 0L;
			Stopwatch stopwatch = Stopwatch.StartNew();
			ExchangeDirectoryCacheProvider.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IDirectoryCacheClient> proxy)
			{
				Stopwatch stopwatch3 = Stopwatch.StartNew();
				try
				{
					retryCount++;
					this.IsNewProxyObject = string.IsNullOrEmpty(proxy.Tag);
					GetObjectContext @object = proxy.Client.GetObject(cacheRequest);
					sADO = @object.Object;
					this.ResultState = @object.ResultState;
					CachePerformanceTracker.AddPerfData(Operation.WCFBeginOperation, @object.BeginOperationLatency);
					CachePerformanceTracker.AddPerfData(Operation.WCFEndOperation, @object.EndOperationLatency);
					proxy.Tag = DateTime.UtcNow.ToString();
				}
				finally
				{
					stopwatch3.Stop();
					CachePerformanceTracker.AddPerfData(Operation.WCFGetOperation, stopwatch3.ElapsedMilliseconds);
					apiTime = stopwatch3.ElapsedMilliseconds;
				}
			}, string.Format("Get Object {0}", typeof(TObject).FullName), 3);
			stopwatch.Stop();
			CachePerformanceTracker.AddPerfData(Operation.WCFProxyObjectCreation, stopwatch.ElapsedMilliseconds - apiTime);
			this.RetryCount = retryCount;
			TObject result = default(TObject);
			if (sADO != null)
			{
				Stopwatch stopwatch2 = Stopwatch.StartNew();
				CachePerformanceTracker.AddPerfData(Operation.DataSize, (long)sADO.Data.Length);
				sADO.Initialize(false);
				stopwatch2.Stop();
				CachePerformanceTracker.AddPerfData(Operation.ObjectInitialization, stopwatch2.ElapsedMilliseconds);
				stopwatch2.Restart();
				if (!typeof(TObject).Equals(typeof(ADRawEntry)))
				{
					result = SimpleADObject.CreateFrom<TObject>(sADO, null, cacheRequest.ADPropertiesRequested);
				}
				else
				{
					result = (TObject)((object)SimpleADObject.CreateFrom(sADO, cacheRequest.ADPropertiesRequested));
				}
				stopwatch2.Stop();
				CachePerformanceTracker.AddPerfData(Operation.ObjectCreation, stopwatch2.ElapsedMilliseconds);
			}
			return result;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000292A0 File Offset: 0x000274A0
		public void Put(AddDirectoryCacheRequest cacheRequest)
		{
			ArgumentValidator.ThrowIfNull("cacheRequest", cacheRequest);
			int retryCount = 0;
			long apiTime = 0L;
			Stopwatch stopwatch = Stopwatch.StartNew();
			ExchangeDirectoryCacheProvider.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IDirectoryCacheClient> proxy)
			{
				Stopwatch stopwatch2 = Stopwatch.StartNew();
				try
				{
					retryCount++;
					this.IsNewProxyObject = string.IsNullOrEmpty(proxy.Tag);
					CacheResponseContext cacheResponseContext = proxy.Client.PutObject(cacheRequest);
					proxy.Tag = DateTime.UtcNow.ToString();
					CachePerformanceTracker.AddPerfData(Operation.WCFBeginOperation, cacheResponseContext.BeginOperationLatency);
					CachePerformanceTracker.AddPerfData(Operation.WCFEndOperation, cacheResponseContext.EndOperationLatency);
				}
				finally
				{
					stopwatch2.Stop();
					CachePerformanceTracker.AddPerfData(Operation.WCFPutOperation, stopwatch2.ElapsedMilliseconds);
					apiTime = stopwatch2.ElapsedMilliseconds;
				}
			}, string.Format("Adding {0} object to cache", cacheRequest.ObjectType), 3);
			stopwatch.Stop();
			CachePerformanceTracker.AddPerfData(Operation.WCFProxyObjectCreation, stopwatch.ElapsedMilliseconds - apiTime);
			this.RetryCount = retryCount;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000293F8 File Offset: 0x000275F8
		public void Remove(RemoveDirectoryCacheRequest cacheRequest)
		{
			ArgumentValidator.ThrowIfNull("cacheRequest", cacheRequest);
			int retryCount = 0;
			long apiTime = 0L;
			Stopwatch stopwatch = Stopwatch.StartNew();
			ExchangeDirectoryCacheProvider.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IDirectoryCacheClient> proxy)
			{
				Stopwatch stopwatch2 = Stopwatch.StartNew();
				try
				{
					retryCount++;
					this.IsNewProxyObject = string.IsNullOrEmpty(proxy.Tag);
					CacheResponseContext cacheResponseContext = proxy.Client.RemoveObject(cacheRequest);
					proxy.Tag = DateTime.UtcNow.ToString();
					CachePerformanceTracker.AddPerfData(Operation.WCFBeginOperation, cacheResponseContext.BeginOperationLatency);
					CachePerformanceTracker.AddPerfData(Operation.WCFEndOperation, cacheResponseContext.EndOperationLatency);
				}
				finally
				{
					stopwatch2.Stop();
					CachePerformanceTracker.AddPerfData(Operation.WCFRemoveOperation, stopwatch2.ElapsedMilliseconds);
					apiTime = stopwatch2.ElapsedMilliseconds;
				}
			}, string.Format("Removing {0} object from cache", cacheRequest.ObjectType), 3);
			stopwatch.Stop();
			CachePerformanceTracker.AddPerfData(Operation.WCFProxyObjectCreation, stopwatch.ElapsedMilliseconds - apiTime);
			this.RetryCount = retryCount;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00029495 File Offset: 0x00027695
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0002949D File Offset: 0x0002769D
		public ADCacheResultState ResultState { get; private set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x000294A6 File Offset: 0x000276A6
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x000294AE File Offset: 0x000276AE
		public bool IsNewProxyObject { get; private set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x000294B7 File Offset: 0x000276B7
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x000294BF File Offset: 0x000276BF
		public int RetryCount { get; private set; }

		// Token: 0x06000945 RID: 2373 RVA: 0x000294DC File Offset: 0x000276DC
		public void TestOnlyResetAllCaches()
		{
			if (!Globals.IsDatacenter)
			{
				return;
			}
			ExchangeDirectoryCacheProvider.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IDirectoryCacheClient> proxy)
			{
				proxy.Client.Diagnostic(new DiagnosticsCacheRequest());
			}, string.Format("Removing object from cache", new object[0]), 3);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00029550 File Offset: 0x00027750
		private static void InitializeProxyPoolIfRequired()
		{
			if (ExchangeDirectoryCacheProvider.proxyPool == null)
			{
				lock (ExchangeDirectoryCacheProvider.lockObject)
				{
					if (ExchangeDirectoryCacheProvider.proxyPool == null)
					{
						NetNamedPipeBinding netNamedPipeBinding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
						netNamedPipeBinding.OpenTimeout = new TimeSpan(0, 0, 15);
						netNamedPipeBinding.ReceiveTimeout = new TimeSpan(0, 0, 15);
						netNamedPipeBinding.SendTimeout = new TimeSpan(0, 0, 15);
						netNamedPipeBinding.CloseTimeout = new TimeSpan(0, 0, 15);
						netNamedPipeBinding.MaxReceivedMessageSize = 10485760L;
						netNamedPipeBinding.MaxBufferSize = 10485760;
						ExchangeDirectoryCacheProvider.proxyPool = DirectoryServiceProxyPool<IDirectoryCacheClient>.CreateDirectoryServiceProxyPool(string.Format("ADCache {0}", Globals.ProcessAppName), new EndpointAddress("net.pipe://localhost/DirectoryCache/service.svc"), ExTraceGlobals.WCFClientEndpointTracer, 100, netNamedPipeBinding, (Exception x, string y) => new ADTransientException(new LocalizedString(x.Message), x), (Exception x, string y) => new PermanentGlobalException(new LocalizedString(x.Message), x), DirectoryEventLogConstants.Tuple_CannotContactADCacheService, false);
					}
				}
			}
		}

		// Token: 0x04000314 RID: 788
		private static DirectoryServiceProxyPool<IDirectoryCacheClient> proxyPool = null;

		// Token: 0x04000315 RID: 789
		private static readonly object lockObject = new object();
	}
}
