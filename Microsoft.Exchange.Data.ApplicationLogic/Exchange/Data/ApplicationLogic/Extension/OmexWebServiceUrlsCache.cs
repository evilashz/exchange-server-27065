using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000102 RID: 258
	public sealed class OmexWebServiceUrlsCache
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0002C797 File Offset: 0x0002A997
		internal static OmexWebServiceUrlsCache Singleton
		{
			get
			{
				return OmexWebServiceUrlsCache.singleton;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0002C79E File Offset: 0x0002A99E
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x0002C7A6 File Offset: 0x0002A9A6
		public int MaxInitializeCompletionCallbacks
		{
			get
			{
				return this.maxInitializeCompletionCallbacks;
			}
			set
			{
				this.maxInitializeCompletionCallbacks = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0002C7B0 File Offset: 0x0002A9B0
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x0002C7FF File Offset: 0x0002A9FF
		public bool IsInitialized
		{
			get
			{
				long num = (this.CacheLifeTimeFromHeaderInSeconds > 0L) ? this.CacheLifeTimeFromHeaderInSeconds : 86400L;
				return this.isInitialized && DateTime.UtcNow.Subtract(this.lastUpdatedTime).TotalSeconds < (double)num;
			}
			set
			{
				this.isInitialized = value;
				if (this.isInitialized)
				{
					this.lastUpdatedTime = DateTime.UtcNow;
				}
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0002C81B File Offset: 0x0002AA1B
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x0002C823 File Offset: 0x0002AA23
		public long CacheLifeTimeFromHeaderInSeconds { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002C82C File Offset: 0x0002AA2C
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0002C834 File Offset: 0x0002AA34
		public string AppStateUrl { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002C83D File Offset: 0x0002AA3D
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0002C845 File Offset: 0x0002AA45
		public string DownloadUrl { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0002C84E File Offset: 0x0002AA4E
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0002C856 File Offset: 0x0002AA56
		public string KillbitUrl { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0002C85F File Offset: 0x0002AA5F
		public string ConfigServiceUrl
		{
			get
			{
				if (this.configServiceUrl == null)
				{
					this.configServiceUrl = ExtensionData.ConfigServiceUrl;
				}
				return this.configServiceUrl;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002C87C File Offset: 0x0002AA7C
		internal void Initialize(string configServiceUrl, OmexWebServiceUrlsCache.InitializeCompletionCallback initializeCompletionCallback)
		{
			if (configServiceUrl == null)
			{
				throw new ArgumentNullException("configServiceUrl");
			}
			if (configServiceUrl.Length == 0)
			{
				throw new ArgumentException("configServiceUrl is empty");
			}
			if (initializeCompletionCallback == null)
			{
				throw new ArgumentNullException("initializeCompletionCallback");
			}
			OmexWebServiceUrlsCache.Tracer.TraceDebug<string>(0L, "OmexWebServicesUrlsCache.Initialize: Setting configServiceUrl: {0}", configServiceUrl);
			this.configServiceUrl = configServiceUrl;
			this.Initialize(initializeCompletionCallback);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002C8D8 File Offset: 0x0002AAD8
		internal void Initialize(OmexWebServiceUrlsCache.InitializeCompletionCallback initializeCompletionCallback)
		{
			if (initializeCompletionCallback == null)
			{
				throw new ArgumentNullException("initializeCompletionCallback");
			}
			bool? flag = null;
			bool flag2 = false;
			lock (this.lockObject)
			{
				if (this.IsInitialized)
				{
					flag = new bool?(true);
				}
				else if (this.initializeCompletionCallbacks.Count + 1 > this.maxInitializeCompletionCallbacks)
				{
					OmexWebServiceUrlsCache.Tracer.TraceError(0L, "OmexWebServicesUrlsCache.Initialize: too many completion callbacks");
					flag = new bool?(false);
				}
				else if (this.ConfigServiceUrl == null)
				{
					OmexWebServiceUrlsCache.Tracer.TraceError(0L, "OmexWebServicesUrlsCache.Initialize: Config service url is null");
					flag = new bool?(false);
				}
				else
				{
					this.initializeCompletionCallbacks.Add(initializeCompletionCallback);
					if (!this.isInitializing)
					{
						flag2 = true;
						this.isInitializing = true;
					}
				}
			}
			if (flag2)
			{
				GetConfig getConfig = new GetConfig(this);
				getConfig.Execute(new GetConfig.SuccessCallback(this.CompleteInitialization), new BaseAsyncCommand.FailureCallback(this.GetConfigFailureCallback));
			}
			if (flag != null)
			{
				initializeCompletionCallback(flag.Value);
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		internal void CompleteInitialization(List<ConfigResponseUrl> configResponses)
		{
			OmexWebServiceUrlsCache.Tracer.TraceDebug(0L, "OmexWebServicesUrlsCache.CompleteInitialization: initialization completed");
			foreach (ConfigResponseUrl configResponseUrl in configResponses)
			{
				string serviceName;
				if ((serviceName = configResponseUrl.ServiceName) != null)
				{
					if (serviceName == "AppInfoQuery15")
					{
						this.KillbitUrl = configResponseUrl.Url;
						continue;
					}
					if (serviceName == "AppInstallInfoQuery15")
					{
						this.DownloadUrl = configResponseUrl.Url;
						continue;
					}
					if (serviceName == "AppStateQuery15")
					{
						this.AppStateUrl = configResponseUrl.Url;
						continue;
					}
				}
				throw new NotSupportedException("Service name: " + configResponseUrl.ServiceName);
			}
			this.ExecuteCompletionCallbacks(true);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002CAC4 File Offset: 0x0002ACC4
		private void GetConfigFailureCallback(Exception exception)
		{
			OmexWebServiceUrlsCache.Tracer.TraceError<Exception>(0L, "OmexWebServicesUrlsCache.GetConfigFailureCallback: exception: {0}", exception);
			this.ExecuteCompletionCallbacks(false);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		private void ExecuteCompletionCallbacks(bool initializeSucceeded)
		{
			OmexWebServiceUrlsCache.Tracer.TraceDebug<bool>(0L, "OmexWebServicesUrlsCache.ExecuteCompletionCallbacks: executing callbacks, initializeSucceeded {0}", initializeSucceeded);
			List<OmexWebServiceUrlsCache.InitializeCompletionCallback> list;
			lock (this.lockObject)
			{
				list = this.initializeCompletionCallbacks;
				this.initializeCompletionCallbacks = new List<OmexWebServiceUrlsCache.InitializeCompletionCallback>();
				this.isInitializing = false;
				this.IsInitialized = initializeSucceeded;
			}
			foreach (OmexWebServiceUrlsCache.InitializeCompletionCallback initializeCompletionCallback in list)
			{
				initializeCompletionCallback(initializeSucceeded);
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002CB8C File Offset: 0x0002AD8C
		internal void TestInitialize(OmexWebServiceUrlsCache.InitializeCompletionCallback initializeCompletionCallback)
		{
			this.isInitializing = true;
			this.Initialize("http:\\dummyUrl", initializeCompletionCallback);
		}

		// Token: 0x04000583 RID: 1411
		private const long maxCacheLifeTimeInSeconds = 86400L;

		// Token: 0x04000584 RID: 1412
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x04000585 RID: 1413
		private object lockObject = new object();

		// Token: 0x04000586 RID: 1414
		private static OmexWebServiceUrlsCache singleton = new OmexWebServiceUrlsCache();

		// Token: 0x04000587 RID: 1415
		private List<OmexWebServiceUrlsCache.InitializeCompletionCallback> initializeCompletionCallbacks = new List<OmexWebServiceUrlsCache.InitializeCompletionCallback>();

		// Token: 0x04000588 RID: 1416
		private int maxInitializeCompletionCallbacks = 1000;

		// Token: 0x04000589 RID: 1417
		private bool isInitializing;

		// Token: 0x0400058A RID: 1418
		private bool isInitialized;

		// Token: 0x0400058B RID: 1419
		private DateTime lastUpdatedTime;

		// Token: 0x0400058C RID: 1420
		private string configServiceUrl;

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x06000B1A RID: 2842
		internal delegate void InitializeCompletionCallback(bool isInitialized);
	}
}
