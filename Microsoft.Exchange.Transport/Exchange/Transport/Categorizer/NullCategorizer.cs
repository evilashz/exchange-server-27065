using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001B3 RID: 435
	internal sealed class NullCategorizer : ICategorizer, ICategorizerComponentFacade
	{
		// Token: 0x0600141E RID: 5150 RVA: 0x00051290 File Offset: 0x0004F490
		public SmtpResponse EnqueueSubmittedMessage(TransportMailItem mailItem)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00051297 File Offset: 0x0004F497
		public void SetLoadTimeDependencies(IProcessingQuotaComponent processingQuotaComponent, IMessageDepotComponent messageDepotComponent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0005129E File Offset: 0x0004F49E
		void ICategorizerComponentFacade.EnqueueSideEffectMessage(ITransportMailItemFacade originalMailItem, ITransportMailItemFacade sideEffectMailItem, string agentName)
		{
			throw new NotImplementedException();
		}
	}
}
