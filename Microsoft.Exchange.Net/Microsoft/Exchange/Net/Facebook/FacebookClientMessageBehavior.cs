using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200071A RID: 1818
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookClientMessageBehavior : IEndpointBehavior
	{
		// Token: 0x06002275 RID: 8821 RVA: 0x000471F2 File Offset: 0x000453F2
		public FacebookClientMessageBehavior(FacebookClientMessageInspector clientInspector)
		{
			ArgumentValidator.ThrowIfNull("ClientInspector", clientInspector);
			this.clientInspector = clientInspector;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0004720C File Offset: 0x0004540C
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0004720E File Offset: 0x0004540E
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			if (!clientRuntime.MessageInspectors.Contains(this.clientInspector))
			{
				clientRuntime.MessageInspectors.Add(this.clientInspector);
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00047234 File Offset: 0x00045434
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00047236 File Offset: 0x00045436
		public void Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x040020D6 RID: 8406
		private FacebookClientMessageInspector clientInspector;
	}
}
