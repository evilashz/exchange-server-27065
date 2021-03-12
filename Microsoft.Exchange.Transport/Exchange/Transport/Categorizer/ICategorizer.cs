using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001AF RID: 431
	internal interface ICategorizer
	{
		// Token: 0x060013C4 RID: 5060
		SmtpResponse EnqueueSubmittedMessage(TransportMailItem mailItem);

		// Token: 0x060013C5 RID: 5061
		void SetLoadTimeDependencies(IProcessingQuotaComponent processingQuotaComponent, IMessageDepotComponent messageDepotComponent);
	}
}
