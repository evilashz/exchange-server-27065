using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006C9 RID: 1737
	public class SoapServiceHostFactory : ServiceHostFactory
	{
		// Token: 0x060049DA RID: 18906 RVA: 0x000E149E File Offset: 0x000DF69E
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return new SoapServiceHostFactory.EcpSoapServiceHost(serviceType, baseAddresses);
		}

		// Token: 0x020006CA RID: 1738
		private class EcpSoapServiceHost : EcpServiceHostBase
		{
			// Token: 0x060049DC RID: 18908 RVA: 0x000E14AF File Offset: 0x000DF6AF
			public EcpSoapServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
			{
			}

			// Token: 0x060049DD RID: 18909 RVA: 0x000E14BC File Offset: 0x000DF6BC
			protected override Binding CreateBinding(Uri address)
			{
				WSHttpBinding wshttpBinding = new WSHttpBinding(address.Scheme);
				if (VirtualDirectoryConfiguration.IsClientCertificateRequired(address))
				{
					wshttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
				}
				else
				{
					wshttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
				}
				return wshttpBinding;
			}
		}
	}
}
