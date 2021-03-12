using System;
using System.Collections.Concurrent;
using System.Net;
using System.ServiceModel;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000130 RID: 304
	internal class ServiceProxyPool<T> : IServiceProxyPool<T>
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00038B1C File Offset: 0x00036D1C
		internal ServiceProxyPool(WSHttpBinding binding, ServiceEndpoint serviceEndpoint)
		{
			this.pool = new ConcurrentQueue<T>();
			this.channelFactory = new ChannelFactory<T>(binding, serviceEndpoint.Uri.ToString());
			try
			{
				this.channelFactory.Credentials.ClientCertificate.Certificate = TlsCertificateInfo.FindFirstCertWithSubjectDistinguishedName(serviceEndpoint.CertificateSubject);
			}
			catch (ArgumentException ex)
			{
				throw new GlsPermanentException(DirectoryStrings.PermanentGlsError(ex.Message));
			}
			ServicePointManager.DefaultConnectionLimit = Math.Max(ServicePointManager.DefaultConnectionLimit, 8 * Environment.ProcessorCount);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00038BAC File Offset: 0x00036DAC
		public T Acquire()
		{
			T t;
			if (!this.pool.TryDequeue(out t) || t == null)
			{
				t = this.CreateServiceProxy();
			}
			return t;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00038BD8 File Offset: 0x00036DD8
		public void Release(T serviceProxy)
		{
			this.pool.Enqueue(serviceProxy);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00038BE6 File Offset: 0x00036DE6
		private T CreateServiceProxy()
		{
			return this.channelFactory.CreateChannel();
		}

		// Token: 0x04000685 RID: 1669
		private ConcurrentQueue<T> pool;

		// Token: 0x04000686 RID: 1670
		private ChannelFactory<T> channelFactory;
	}
}
