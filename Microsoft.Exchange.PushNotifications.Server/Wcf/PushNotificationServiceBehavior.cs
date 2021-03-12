using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.PushNotifications.Server.Wcf
{
	// Token: 0x0200002E RID: 46
	[AttributeUsage(AttributeTargets.Class)]
	public class PushNotificationServiceBehavior : Attribute, IServiceBehavior
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00004916 File Offset: 0x00002B16
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004918 File Offset: 0x00002B18
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = (ChannelDispatcher)channelDispatcherBase;
				channelDispatcher.ErrorHandlers.Add(new PushNotificationErrorHandler());
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004974 File Offset: 0x00002B74
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}
