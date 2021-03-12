using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Data.Transport.Delivery
{
	// Token: 0x0200005B RID: 91
	public abstract class CloseConnectionEventSource
	{
		// Token: 0x06000217 RID: 535
		public abstract void UnregisterConnection(SmtpResponse smtpResponse);
	}
}
