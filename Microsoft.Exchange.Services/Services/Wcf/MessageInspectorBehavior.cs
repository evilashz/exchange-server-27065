using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB9 RID: 3513
	[AttributeUsage(AttributeTargets.Class)]
	internal class MessageInspectorBehavior : Attribute, IServiceBehavior
	{
		// Token: 0x06005971 RID: 22897 RVA: 0x00117DEC File Offset: 0x00115FEC
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x00117DF0 File Offset: 0x00115FF0
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = (ChannelDispatcher)channelDispatcherBase;
				foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
				{
					if (endpointDispatcher.ContractName.Equals("IUM12LegacyContract"))
					{
						endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new UMLegacyMessageInspectorManager());
						endpointDispatcher.DispatchRuntime.OperationSelector = new DispatchByBodyElementOperationSelector();
					}
					else if (endpointDispatcher.ContractName.Equals("IJsonServiceContract") || endpointDispatcher.ContractName.Equals("IEWSStreamingContract"))
					{
						endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new JsonMessageInspectorManager());
					}
					else
					{
						endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MessageInspectorManager());
						endpointDispatcher.DispatchRuntime.OperationSelector = new DispatchByBodyElementOperationSelector();
					}
				}
				channelDispatcher.ErrorHandlers.Add(new ExceptionHandlerInspector());
			}
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x00117F28 File Offset: 0x00116128
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}
