using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200003F RID: 63
	public abstract class ConnectEventSource : ReceiveEventSource
	{
		// Token: 0x06000186 RID: 390 RVA: 0x0000625B File Offset: 0x0000445B
		internal ConnectEventSource(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x06000187 RID: 391
		public abstract void RejectConnection(SmtpResponse response);
	}
}
