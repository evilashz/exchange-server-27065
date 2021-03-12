using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000152 RID: 338
	[AttributeUsage(AttributeTargets.Class)]
	internal class MessageInspectorBehavior : Attribute, IServiceBehavior
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x0002E17C File Offset: 0x0002C37C
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002E180 File Offset: 0x0002C380
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = (ChannelDispatcher)channelDispatcherBase;
				foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
				{
					endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new OWAMessageInspector());
				}
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002E218 File Offset: 0x0002C418
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x040007BE RID: 1982
		private const int DefaultMaxRequestsQueued = 500;

		// Token: 0x040007BF RID: 1983
		private const int DefaultMaxWorkerThreadsPerProc = 10;

		// Token: 0x040007C0 RID: 1984
		private const int DefaultIdentityCacheSize = 4000;
	}
}
