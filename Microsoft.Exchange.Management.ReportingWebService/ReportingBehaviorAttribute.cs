using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.Class)]
	public class ReportingBehaviorAttribute : Attribute, IServiceBehavior
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004A65 File Offset: 0x00002C65
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004A68 File Offset: 0x00002C68
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = (ChannelDispatcher)channelDispatcherBase;
				foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
				{
					endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new ResponseFormatInspector());
					endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new RewriteBaseUrlMessageInspector());
					endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new HttpCachePolicyInspector());
					endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new QueryValidationInspector());
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004B44 File Offset: 0x00002D44
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}
