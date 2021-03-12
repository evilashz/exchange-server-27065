using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006C4 RID: 1732
	public class ServiceHostFactory : ServiceHostFactory
	{
		// Token: 0x060049C7 RID: 18887 RVA: 0x000E1383 File Offset: 0x000DF583
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return new ServiceHostFactory.EcpServiceHost(serviceType, baseAddresses);
		}

		// Token: 0x020006C5 RID: 1733
		private class EcpServiceHost : EcpServiceHostBase
		{
			// Token: 0x060049C9 RID: 18889 RVA: 0x000E1394 File Offset: 0x000DF594
			public EcpServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
			{
			}

			// Token: 0x060049CA RID: 18890 RVA: 0x000E13A0 File Offset: 0x000DF5A0
			protected override Binding CreateBinding(Uri address)
			{
				WebHttpBinding webHttpBinding = new WebHttpBinding(address.Scheme);
				if (VirtualDirectoryConfiguration.IsClientCertificateRequired(address))
				{
					webHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
				}
				return webHttpBinding;
			}

			// Token: 0x060049CB RID: 18891 RVA: 0x000E13D3 File Offset: 0x000DF5D3
			protected override void ApplyServiceEndPointConfiguration(ServiceEndpoint serviceEndpoint)
			{
				base.ApplyServiceEndPointConfiguration(serviceEndpoint);
				serviceEndpoint.Behaviors.Insert(0, ServiceHostFactory.EcpServiceHost.webScriptEnablingBehavior);
			}

			// Token: 0x0400317A RID: 12666
			private static readonly WebScriptEnablingBehavior webScriptEnablingBehavior = new WebScriptEnablingBehavior();
		}
	}
}
