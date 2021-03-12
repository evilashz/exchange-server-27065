using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.Class)]
	internal class LegacyServiceBehavior : Attribute, IServiceBehavior
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00013D89 File Offset: 0x00011F89
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00013D8C File Offset: 0x00011F8C
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = (ChannelDispatcher)channelDispatcherBase;
				channelDispatcher.ErrorHandlers.Add(new LegacyErrorHandler());
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00013DE8 File Offset: 0x00011FE8
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}
