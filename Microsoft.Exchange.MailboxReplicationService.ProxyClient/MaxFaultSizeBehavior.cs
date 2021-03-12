using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000011 RID: 17
	public class MaxFaultSizeBehavior : IEndpointBehavior
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00007F62 File Offset: 0x00006162
		public MaxFaultSizeBehavior(int size)
		{
			this.size = size;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007F71 File Offset: 0x00006171
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007F73 File Offset: 0x00006173
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.MaxFaultSize = this.size;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007F81 File Offset: 0x00006181
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007F83 File Offset: 0x00006183
		public void Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0400003E RID: 62
		private readonly int size;
	}
}
