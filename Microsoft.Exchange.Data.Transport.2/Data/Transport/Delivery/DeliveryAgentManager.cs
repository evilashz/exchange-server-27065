using System;

namespace Microsoft.Exchange.Data.Transport.Delivery
{
	// Token: 0x02000056 RID: 86
	public abstract class DeliveryAgentManager : AgentManager
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001FC RID: 508
		public abstract string SupportedDeliveryProtocol { get; }
	}
}
