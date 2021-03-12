using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008C2 RID: 2242
	internal class WebServiceByFQDNCache : BaseWebCache<string, WebServicesInfo>
	{
		// Token: 0x06003F74 RID: 16244 RVA: 0x000DB6F5 File Offset: 0x000D98F5
		public WebServiceByFQDNCache() : base(WebServiceByFQDNCache.WebServiceByFQDNCacheKeyPrefix, SlidingOrAbsoluteTimeout.Sliding, WebServiceByFQDNCache.TimeoutInMinutes)
		{
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x000DB708 File Offset: 0x000D9908
		public static WebServiceByFQDNCache Singleton
		{
			get
			{
				return WebServiceByFQDNCache.singleton;
			}
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000DB710 File Offset: 0x000D9910
		public override WebServicesInfo Get(string key)
		{
			WebServicesInfo webServicesInfo = base.Get(key);
			if (webServicesInfo != null)
			{
				return webServicesInfo;
			}
			webServicesInfo = this.GetWebServiceInfoForServer(key);
			if (webServicesInfo != null)
			{
				this.Add(key, webServicesInfo);
			}
			return webServicesInfo;
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000DB75C File Offset: 0x000D995C
		private WebServicesInfo GetWebServiceInfoForServer(string serverFQDN)
		{
			Exception ex = null;
			Uri uri = null;
			try
			{
				ServiceTopology currentLegacyServiceTopology = ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\Types\\WebServiceByFQDNCache.cs", "GetWebServiceInfoForServer", 85);
				WebServicesService webServicesService = currentLegacyServiceTopology.FindAny<WebServicesService>(ClientAccessType.InternalNLBBypass, (WebServicesService s) => string.Equals(s.ServerFullyQualifiedDomainName, serverFQDN, StringComparison.OrdinalIgnoreCase), "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\Types\\WebServiceByFQDNCache.cs", "GetWebServiceInfoForServer", 86);
				if (webServicesService != null)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, WebServicesService>(0L, "[WebServiceByFQDNCache::GetWebServiceInfoForServer] Found WebServicesService {1} for server {0}", serverFQDN, webServicesService);
					return WebServicesInfo.CreateFromWebServicesService(webServicesService);
				}
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[WebServiceByFQDNCache::GetWebServiceInfoForServer] Did not find a WebServicesService object for server {0}", serverFQDN);
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[WebServiceByFQDNCache::GetWebServiceInfoForServer] Cannot find {0} as a E14 server, Use BackEndLocator to find E15 urls", serverFQDN);
				BackEndServer backEndServer = new BackEndServer(serverFQDN, Server.E15MinVersion);
				uri = BackEndLocator.GetBackEndWebServicesUrl(backEndServer);
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (DataSourceOperationException ex3)
			{
				ex = ex3;
			}
			catch (DataValidationException ex4)
			{
				ex = ex4;
			}
			catch (BackEndLocatorException ex5)
			{
				ex = ex5;
			}
			catch (ServiceDiscoveryTransientException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, Exception>(0L, "[WebServiceByFQDNCache::GetWebServiceInfoForServer] GetBackEndWebServicesUrl for server {0} failed with {1}", serverFQDN, ex);
				return null;
			}
			if (!(uri != null))
			{
				return null;
			}
			return WebServicesInfo.Create(uri, serverFQDN, Server.E15MinVersion, false);
		}

		// Token: 0x04002458 RID: 9304
		private static readonly string WebServiceByFQDNCacheKeyPrefix = "_WSFCKP_";

		// Token: 0x04002459 RID: 9305
		private static readonly int TimeoutInMinutes = 5;

		// Token: 0x0400245A RID: 9306
		private static WebServiceByFQDNCache singleton = new WebServiceByFQDNCache();
	}
}
