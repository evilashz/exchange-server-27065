using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GlobalLocatorCache;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x0200088E RID: 2190
	internal class MserveCacheServiceProvider : IDisposeTrackable, IDisposable
	{
		// Token: 0x06002EC1 RID: 11969 RVA: 0x00068880 File Offset: 0x00066A80
		internal MserveCacheServiceProvider() : this("localhost")
		{
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00068890 File Offset: 0x00066A90
		internal MserveCacheServiceProvider(string machineName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("machineName", machineName);
			ExTraceGlobals.ClientTracer.TraceDebug<string>((long)this.GetHashCode(), "Creating new MserveCacheService provider instance to server {0}", machineName);
			this.machineName = machineName;
			this.InitializeServiceProxyPool();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000688E0 File Offset: 0x00066AE0
		private void InitializeServiceProxyPool()
		{
			ExTraceGlobals.ClientTracer.TraceDebug((long)this.GetHashCode(), "MserveCacheServiceProvider - Initializing Service proxy pool");
			NetNamedPipeBinding defaultBinding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
			EndpointAddress endpointAddress = MserveCacheServiceProvider.CreateAndConfigureMserveCacheServiceEndpoint(this.machineName);
			this.serviceProxyPool = MserveCacheServiceProxyPool<IMserveCacheService>.CreateMserveCacheServiceProxyPool("MserveCacheServiceNetPipeEndpoint", endpointAddress, ExTraceGlobals.ClientTracer, 2, defaultBinding, new GetWrappedExceptionDelegate(MserveCacheServiceProvider.GetTransientWrappedException), new GetWrappedExceptionDelegate(MserveCacheServiceProvider.GetPermanentWrappedException), CommonEventLogConstants.Tuple_CannotContactMserveCacheService, true);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x0006894C File Offset: 0x00066B4C
		private static Exception GetTransientWrappedException(Exception wcfException, string targetInfo)
		{
			if (wcfException is TimeoutException)
			{
				return new MserveCacheServiceTransientException(NetServerException.MserveCacheTimeoutError(wcfException.Message), wcfException);
			}
			if (wcfException is EndpointNotFoundException)
			{
				return new MserveCacheServiceTransientException(NetServerException.MserveCacheEndpointNotFound(targetInfo, wcfException.ToString()));
			}
			return new MserveCacheServiceTransientException(NetServerException.TransientMserveCacheError(wcfException.Message), wcfException);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x0006899E File Offset: 0x00066B9E
		private static Exception GetPermanentWrappedException(Exception wcfException, string targetInfo)
		{
			return new MserveCacheServicePermanentException(NetServerException.PermanentMserveCacheError(wcfException.Message), wcfException);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000689B4 File Offset: 0x00066BB4
		internal static EndpointAddress CreateAndConfigureMserveCacheServiceEndpoint(string machineName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("machineName", machineName);
			string uri = string.Format(MserveCacheServiceProvider.serviceEndpointFormat, machineName);
			EndpointAddress result;
			try
			{
				EndpointAddress endpointAddress = new EndpointAddress(uri);
				result = endpointAddress;
			}
			catch (UriFormatException arg)
			{
				ExTraceGlobals.ClientTracer.TraceError<string, UriFormatException>(0L, "MserveCacheServiceProvider.MserveCacheServiceProvider() - Invalid Server Name {0}.  Exception: {1}", machineName, arg);
				throw;
			}
			return result;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x00068A0C File Offset: 0x00066C0C
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MserveCacheServiceProvider>(this);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x00068A14 File Offset: 0x00066C14
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x00068A29 File Offset: 0x00066C29
		public void Dispose()
		{
			ExTraceGlobals.ClientTracer.TraceDebug(0L, "Disposing of MserveCacheServiceProvider instance");
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.serviceProxyPool.Dispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x00068A60 File Offset: 0x00066C60
		internal static MserveCacheServiceProvider GetInstance()
		{
			MserveCacheServiceProvider mserveCacheServiceProvider = MserveCacheServiceProvider.staticInstance;
			if (mserveCacheServiceProvider == null)
			{
				lock (MserveCacheServiceProvider.instanceLockRoot)
				{
					if (MserveCacheServiceProvider.staticInstance == null)
					{
						MserveCacheServiceProvider.staticInstance = new MserveCacheServiceProvider();
						MserveCacheServiceProvider.staticInstance.SuppressDisposeTracker();
						mserveCacheServiceProvider = MserveCacheServiceProvider.staticInstance;
					}
				}
			}
			return mserveCacheServiceProvider;
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x00068AC4 File Offset: 0x00066CC4
		internal static ExEventLog EventLog
		{
			get
			{
				lock (MserveCacheServiceProvider.eventLock)
				{
					if (MserveCacheServiceProvider.eventlog == null)
					{
						MserveCacheServiceProvider.eventlog = new ExEventLog(ExTraceGlobals.ClientTracer.Category, "MSExchange Common");
					}
				}
				return MserveCacheServiceProvider.eventlog;
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x00068B48 File Offset: 0x00066D48
		public string ReadMserveData(string requestName)
		{
			ExTraceGlobals.ClientTracer.TraceFunction(0L, "Enter MserveCacheServiceProvider.ReadMserveData().");
			string partnerId = string.Empty;
			this.serviceProxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMserveCacheService> proxy)
			{
				partnerId = proxy.Client.ReadMserveData(requestName);
			}, string.Format("ReadMserveData for {0}", requestName), 3);
			string.IsNullOrEmpty(partnerId);
			ExTraceGlobals.ClientTracer.TraceFunction(0L, "Exit MserveCacheServiceProvider.ReadMserveData().");
			return partnerId;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x00068BE4 File Offset: 0x00066DE4
		public int GetChunkSize()
		{
			ExTraceGlobals.ClientTracer.TraceFunction(0L, "Enter MserveCacheServiceProvider.GetChunkSize().");
			int chunkSize = 0;
			this.serviceProxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMserveCacheService> proxy)
			{
				chunkSize = proxy.Client.GetChunkSize();
			}, "GetChunkSize", 3);
			if (chunkSize != 0)
			{
				ExTraceGlobals.ClientTracer.TraceDebug<int>(0L, "Returning Chunk Size = {0}", chunkSize);
			}
			ExTraceGlobals.ClientTracer.TraceFunction(0L, "Exit MserveCacheServiceProvider.GetChunkSize().");
			return chunkSize;
		}

		// Token: 0x040028C3 RID: 10435
		private const int TotalRetries = 3;

		// Token: 0x040028C4 RID: 10436
		private const string MserveCacheServiceNetPipeEndpoint = "MserveCacheServiceNetPipeEndpoint";

		// Token: 0x040028C5 RID: 10437
		private const int MaxNumberOfClientProxies = 2;

		// Token: 0x040028C6 RID: 10438
		private static readonly TimeSpan defaultSendTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x040028C7 RID: 10439
		private static string serviceEndpointFormat = "net.pipe://{0}/MserveCacheService/service.svc";

		// Token: 0x040028C8 RID: 10440
		private static object instanceLockRoot = new object();

		// Token: 0x040028C9 RID: 10441
		private static object eventLock = new object();

		// Token: 0x040028CA RID: 10442
		private static MserveCacheServiceProvider staticInstance;

		// Token: 0x040028CB RID: 10443
		private static ExEventLog eventlog;

		// Token: 0x040028CC RID: 10444
		private readonly string machineName;

		// Token: 0x040028CD RID: 10445
		private DisposeTracker disposeTracker;

		// Token: 0x040028CE RID: 10446
		private MserveCacheServiceProxyPool<IMserveCacheService> serviceProxyPool;
	}
}
