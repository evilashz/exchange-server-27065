using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C8D RID: 3213
	public class JsonServiceHostFactory : WebServiceHostFactory
	{
		// Token: 0x06005716 RID: 22294 RVA: 0x001119C8 File Offset: 0x0010FBC8
		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
		{
			ServiceHost serviceHost = new ServiceHost(typeof(JsonService), baseAddresses);
			serviceHost.Description.Endpoints[0].Behaviors.Add(new JsonWebHttpBehavior());
			return serviceHost;
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x00111A07 File Offset: 0x0010FC07
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return base.CreateServiceHost(serviceType, baseAddresses);
		}
	}
}
