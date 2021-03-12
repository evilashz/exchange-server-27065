using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B80 RID: 2944
	public class EWSServiceHostFactory : WebServiceHostFactory
	{
		// Token: 0x060055D6 RID: 21974 RVA: 0x00110A60 File Offset: 0x0010EC60
		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
		{
			ServiceHost serviceHost = new ServiceHost(typeof(EWSService), baseAddresses);
			serviceHost.Description.Endpoints[0].Behaviors.Add(new EwsWebHttpBehavior());
			return serviceHost;
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x00110A9F File Offset: 0x0010EC9F
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return base.CreateServiceHost(serviceType, baseAddresses);
		}
	}
}
