using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000030 RID: 48
	public class DisconnectEventArgs : ReceiveEventArgs
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005DA4 File Offset: 0x00003FA4
		public DisconnectReason DisconnectReason
		{
			get
			{
				return base.SmtpSession.DisconnectReason;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005DB1 File Offset: 0x00003FB1
		internal DisconnectEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}
	}
}
