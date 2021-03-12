using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000203 RID: 515
	public class OWAWebHttpBehavior : WebHttpBehavior
	{
		// Token: 0x06001416 RID: 5142 RVA: 0x0004856F File Offset: 0x0004676F
		protected override WebHttpDispatchOperationSelector GetOperationSelector(ServiceEndpoint endpoint)
		{
			return new OWADispatchOperationSelector(endpoint);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00048577 File Offset: 0x00046777
		protected override IDispatchMessageFormatter GetRequestDispatchFormatter(OperationDescription operationDescription, ServiceEndpoint endpoint)
		{
			return base.GetRequestDispatchFormatter(operationDescription, endpoint);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00048581 File Offset: 0x00046781
		protected override IClientMessageFormatter GetRequestClientFormatter(OperationDescription operationDescription, ServiceEndpoint endpoint)
		{
			return base.GetRequestClientFormatter(operationDescription, endpoint);
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0004858B File Offset: 0x0004678B
		protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();
			endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new OWAFaultHandler());
		}
	}
}
