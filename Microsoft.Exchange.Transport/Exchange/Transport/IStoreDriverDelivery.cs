using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000026 RID: 38
	internal interface IStoreDriverDelivery : IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x060000CF RID: 207
		SmtpResponse DoLocalDelivery(TransportMailItem mailItem);

		// Token: 0x060000D0 RID: 208
		void Retire();
	}
}
