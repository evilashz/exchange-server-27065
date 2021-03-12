using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000110 RID: 272
	internal sealed class ServiceProxyPoolsManager<TWebservice> : IDisposable
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x00017036 File Offset: 0x00015236
		private ServiceProxyPoolsManager()
		{
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00017040 File Offset: 0x00015240
		~ServiceProxyPoolsManager()
		{
			this.Dispose(false);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00017070 File Offset: 0x00015270
		public static ServiceProxyPoolsManager<TWebservice> Instance
		{
			get
			{
				return ServiceProxyPoolsManager<TWebservice>.instance;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000170C4 File Offset: 0x000152C4
		public ServiceProxyPool<TWebservice> GetOrCreateProxyPool(ServiceProxyIdentity identity, ExecutionLog logProvider)
		{
			ArgumentValidator.ThrowIfNull("identity", identity);
			ArgumentValidator.ThrowIfNull("logProvider", logProvider);
			return ServiceProxyPoolsManager<TWebservice>.proxyPools.GetOrAddSafe(identity, delegate(ServiceProxyIdentity proxyPool)
			{
				string name = typeof(TWebservice).Name;
				string hostName = proxyPool.ToString();
				return new ServiceProxyPool<TWebservice>(name, hostName, 5U, this.CreateChannelFactoryInstance(identity), logProvider);
			});
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00017128 File Offset: 0x00015328
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.Dispose(true);
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001713C File Offset: 0x0001533C
		private ChannelFactory<TWebservice> CreateChannelFactoryInstance(ServiceProxyIdentity identity)
		{
			ChannelFactory<TWebservice> channelFactory = new ChannelFactory<TWebservice>(new WSHttpBinding(SecurityMode.Transport)
			{
				MaxReceivedMessageSize = 26214400L,
				Security = 
				{
					Transport = 
					{
						ClientCredentialType = HttpClientCredentialType.Certificate
					}
				}
			}, identity.EndpointAddress);
			if (channelFactory.Credentials != null)
			{
				channelFactory.Credentials.ClientCertificate.Certificate = identity.Certificate;
			}
			return channelFactory;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001719C File Offset: 0x0001539C
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				foreach (KeyValuePair<ServiceProxyIdentity, Lazy<ServiceProxyPool<TWebservice>>> keyValuePair in ServiceProxyPoolsManager<TWebservice>.proxyPools)
				{
					keyValuePair.Value.Value.Dispose();
				}
				this.disposed = true;
				if (disposing)
				{
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x04000420 RID: 1056
		private const uint DefaultMaxNumberOfProxiesInPool = 5U;

		// Token: 0x04000421 RID: 1057
		private static ConcurrentDictionary<ServiceProxyIdentity, Lazy<ServiceProxyPool<TWebservice>>> proxyPools = new ConcurrentDictionary<ServiceProxyIdentity, Lazy<ServiceProxyPool<TWebservice>>>();

		// Token: 0x04000422 RID: 1058
		private static ServiceProxyPoolsManager<TWebservice> instance = new ServiceProxyPoolsManager<TWebservice>();

		// Token: 0x04000423 RID: 1059
		private bool disposed;
	}
}
