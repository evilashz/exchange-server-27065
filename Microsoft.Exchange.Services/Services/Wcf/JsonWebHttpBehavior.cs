using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C89 RID: 3209
	public class JsonWebHttpBehavior : WebHttpBehavior
	{
		// Token: 0x06005709 RID: 22281 RVA: 0x00111869 File Offset: 0x0010FA69
		protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();
			endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new JsonServiceErrorHandler());
		}
	}
}
