using System;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x0200004E RID: 78
	internal interface ICategorizerComponentFacade
	{
		// Token: 0x060002F2 RID: 754
		void EnqueueSideEffectMessage(ITransportMailItemFacade originalMailItem, ITransportMailItemFacade sideEffectMailItem, string agentName);
	}
}
