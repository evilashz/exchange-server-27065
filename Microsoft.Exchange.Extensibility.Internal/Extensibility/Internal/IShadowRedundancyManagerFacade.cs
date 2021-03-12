using System;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000050 RID: 80
	internal interface IShadowRedundancyManagerFacade
	{
		// Token: 0x060002F4 RID: 756
		void LinkSideEffectMailItemIfNeeded(ITransportMailItemFacade originalMailItem, ITransportMailItemFacade sideEffectMailItem);
	}
}
