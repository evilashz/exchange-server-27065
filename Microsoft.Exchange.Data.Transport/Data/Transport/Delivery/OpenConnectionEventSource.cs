using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Data.Transport.Delivery
{
	// Token: 0x02000057 RID: 87
	public abstract class OpenConnectionEventSource
	{
		// Token: 0x060001FE RID: 510
		public abstract void FailQueue(SmtpResponse smtpResponse);

		// Token: 0x060001FF RID: 511
		public abstract void DeferQueue(SmtpResponse smtpResponse);

		// Token: 0x06000200 RID: 512
		public abstract void DeferQueue(SmtpResponse smtpResponse, TimeSpan interval);

		// Token: 0x06000201 RID: 513
		public abstract void RegisterConnection(string remoteHost, SmtpResponse smtpResponse);
	}
}
