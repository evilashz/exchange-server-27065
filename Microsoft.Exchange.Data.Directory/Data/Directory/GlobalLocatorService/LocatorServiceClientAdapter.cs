using System;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000121 RID: 289
	internal abstract class LocatorServiceClientAdapter
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x00036A3C File Offset: 0x00034C3C
		internal string ResolveEndpointToIpAddress(bool flushCache)
		{
			DnsResult dnsResult = null;
			DnsQuery query = new DnsQuery(DnsRecordType.A, this.endpointHostName);
			if (flushCache || LocatorServiceClientAdapter.lastCacheUpdate + 300000 < Environment.TickCount)
			{
				LocatorServiceClientAdapter.dnsCache.FlushCache();
				LocatorServiceClientAdapter.lastCacheUpdate = Environment.TickCount;
			}
			dnsResult = LocatorServiceClientAdapter.dnsCache.Find(query);
			if (dnsResult == null)
			{
				try
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(this.endpointHostName);
					IPAddress[] addressList = hostEntry.AddressList;
					if (addressList.Length > 0)
					{
						dnsResult = new DnsResult(DnsStatus.Success, addressList[0], TimeSpan.FromMinutes(1.0));
						LocatorServiceClientAdapter.dnsCache.Add(query, dnsResult);
					}
				}
				catch (SocketException)
				{
				}
			}
			if (dnsResult != null)
			{
				return dnsResult.Server.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00036AF8 File Offset: 0x00034CF8
		internal LocatorServiceClientAdapter(GlsCallerId glsCallerId)
		{
			this.requestIdentity = new RequestIdentity
			{
				CallerId = glsCallerId.CallerIdString,
				TrackingGuid = glsCallerId.TrackingGuid
			};
			WSHttpBinding wshttpBinding = new WSHttpBinding(SecurityMode.Transport)
			{
				ReceiveTimeout = TimeSpan.FromSeconds(20.0),
				SendTimeout = TimeSpan.FromSeconds(15.0),
				MaxBufferPoolSize = 524288L,
				MaxReceivedMessageSize = 65536L
			};
			wshttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
			ServiceEndpoint serviceEndpoint = LocatorServiceClientAdapter.LoadServiceEndpoint();
			this.serviceProxyPool = new ServiceProxyPool<LocatorService>(wshttpBinding, serviceEndpoint);
			this.endpointHostName = serviceEndpoint.Uri.Host;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00036BB0 File Offset: 0x00034DB0
		internal LocatorServiceClientAdapter(GlsCallerId glsCallerId, LocatorService serviceProxy)
		{
			this.requestIdentity = new RequestIdentity
			{
				CallerId = glsCallerId.CallerIdString,
				TrackingGuid = glsCallerId.TrackingGuid
			};
			this.serviceProxyPool = new SingletonServiceProxyPool<LocatorService>(serviceProxy);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00036BF4 File Offset: 0x00034DF4
		protected static void OnWebServiceRequestCompleted(IAsyncResult internalAR)
		{
			GlsAsyncState glsAsyncState = (GlsAsyncState)internalAR.AsyncState;
			AsyncCallback clientCallback = glsAsyncState.ClientCallback;
			object clientAsyncState = glsAsyncState.ClientAsyncState;
			LocatorService serviceProxy = glsAsyncState.ServiceProxy;
			if (clientCallback != null)
			{
				IAsyncResult ar = new GlsAsyncResult(clientCallback, clientAsyncState, serviceProxy, internalAR);
				clientCallback(ar);
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00036C38 File Offset: 0x00034E38
		protected static void ThrowIfNull(object argument, string parameterName)
		{
			if (argument == null)
			{
				throw new ArgumentException(parameterName);
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00036C44 File Offset: 0x00034E44
		protected static void ThrowIfEmptyGuid(Guid argument, string parameterName)
		{
			if (argument == Guid.Empty)
			{
				throw new ArgumentException(parameterName);
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00036C5A File Offset: 0x00034E5A
		protected static void ThrowIfInvalidNamespace(Namespace ns)
		{
			if (ns == Namespace.Invalid)
			{
				throw new ArgumentException("namespace");
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00036C6C File Offset: 0x00034E6C
		protected static void ThrowIfInvalidNamespace(Namespace[] ns)
		{
			foreach (Namespace ns2 in ns)
			{
				LocatorServiceClientAdapter.ThrowIfInvalidNamespace(ns2);
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00036C93 File Offset: 0x00034E93
		protected virtual LocatorService AcquireServiceProxy()
		{
			return this.serviceProxyPool.Acquire();
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00036CA0 File Offset: 0x00034EA0
		protected void ReleaseServiceProxy(LocatorService serviceProxy)
		{
			this.serviceProxyPool.Release(serviceProxy);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00036CB0 File Offset: 0x00034EB0
		private static ServiceEndpoint LoadServiceEndpoint()
		{
			ServiceEndpoint endpoint = LocatorServiceClientConfiguration.Instance.Endpoint;
			if (endpoint != null)
			{
				return endpoint;
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 163, "LoadServiceEndpoint", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\GlobalLocatorService\\LocatorServiceClientAdapter.cs");
			ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
			return endpointContainer.GetEndpoint("GlobalLocatorService");
		}

		// Token: 0x04000637 RID: 1591
		private const string ServiceEndpointName = "GlobalLocatorService";

		// Token: 0x04000638 RID: 1592
		private const int DnsCacheTTLMsec = 300000;

		// Token: 0x04000639 RID: 1593
		private readonly string endpointHostName;

		// Token: 0x0400063A RID: 1594
		private static DnsCache dnsCache = new DnsCache(5);

		// Token: 0x0400063B RID: 1595
		private static int lastCacheUpdate = 0;

		// Token: 0x0400063C RID: 1596
		protected RequestIdentity requestIdentity;

		// Token: 0x0400063D RID: 1597
		private IServiceProxyPool<LocatorService> serviceProxyPool;
	}
}
