using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020003AE RID: 942
	internal interface IRoutedMessageQueue
	{
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002A43 RID: 10819
		NextHopSolutionKey Key { get; }

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002A44 RID: 10820
		SmtpResponse LastError { get; }

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002A45 RID: 10821
		int ActiveQueueLength { get; }
	}
}
