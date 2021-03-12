using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200003D RID: 61
	public class StartTlsCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00006218 File Offset: 0x00004418
		internal StartTlsCommandEventArgs()
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006220 File Offset: 0x00004420
		internal StartTlsCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00006229 File Offset: 0x00004429
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00006236 File Offset: 0x00004436
		public bool RequestClientTlsCertificate
		{
			get
			{
				return base.SmtpSession.RequestClientTlsCertificate;
			}
			set
			{
				base.SmtpSession.RequestClientTlsCertificate = value;
			}
		}
	}
}
