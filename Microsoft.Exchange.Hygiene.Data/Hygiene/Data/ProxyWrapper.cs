using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200009B RID: 155
	public class ProxyWrapper<TProxy, TInterface> : IDisposable where TProxy : ClientBase<TInterface> where TInterface : class
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x00011670 File Offset: 0x0000F870
		public ProxyWrapper(Uri serviceUri, X509Certificate2 cert)
		{
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
			basicHttpBinding.Security.Mode = BasicHttpSecurityMode.Transport;
			basicHttpBinding.MaxReceivedMessageSize = 2147483647L;
			basicHttpBinding.MaxBufferSize = int.MaxValue;
			basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
			EndpointAddress endpointAddress = new EndpointAddress(serviceUri, new AddressHeader[0]);
			ServicePointManager.EnableDnsRoundRobin = true;
			this.Proxy = (Activator.CreateInstance(typeof(TProxy), new object[]
			{
				basicHttpBinding,
				endpointAddress
			}) as TProxy);
			TProxy proxy = this.Proxy;
			proxy.ClientCredentials.ClientCertificate.Certificate = cert;
			this.disposed = false;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00011728 File Offset: 0x0000F928
		public ProxyWrapper(Uri serviceUri, string userName, SecureString password)
		{
			WSHttpBinding wshttpBinding = new WSHttpBinding();
			wshttpBinding.Security.Mode = SecurityMode.TransportWithMessageCredential;
			wshttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
			wshttpBinding.Security.Message.EstablishSecurityContext = true;
			wshttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
			wshttpBinding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
			EndpointAddress endpointAddress = new EndpointAddress(serviceUri, new AddressHeader[0]);
			this.Proxy = (Activator.CreateInstance(typeof(TProxy), new object[]
			{
				wshttpBinding,
				endpointAddress
			}) as TProxy);
			TProxy proxy = this.Proxy;
			proxy.ClientCredentials.UserName.UserName = userName;
			IntPtr ptr = Marshal.SecureStringToBSTR(password);
			TProxy proxy2 = this.Proxy;
			proxy2.ClientCredentials.UserName.Password = Marshal.PtrToStringUni(ptr);
			this.disposed = false;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00011820 File Offset: 0x0000FA20
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x00011828 File Offset: 0x0000FA28
		public TProxy Proxy { get; private set; }

		// Token: 0x06000540 RID: 1344 RVA: 0x00011834 File Offset: 0x0000FA34
		public void Dispose()
		{
			if (this.Proxy != null && !this.disposed)
			{
				try
				{
					TProxy proxy = this.Proxy;
					proxy.Close();
				}
				catch (CommunicationException)
				{
					TProxy proxy2 = this.Proxy;
					proxy2.Abort();
				}
				catch (Exception)
				{
					TProxy proxy3 = this.Proxy;
					proxy3.Abort();
					throw;
				}
				this.disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400034C RID: 844
		private const int MaxSize = 2147483647;

		// Token: 0x0400034D RID: 845
		private bool disposed;
	}
}
