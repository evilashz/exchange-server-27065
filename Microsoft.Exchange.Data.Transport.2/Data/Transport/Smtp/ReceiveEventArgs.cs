using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200002B RID: 43
	public abstract class ReceiveEventArgs : EventArgs
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00005CC3 File Offset: 0x00003EC3
		internal ReceiveEventArgs()
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005CCB File Offset: 0x00003ECB
		internal ReceiveEventArgs(SmtpSession smtpSession)
		{
			this.Initialize(smtpSession);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005CDA File Offset: 0x00003EDA
		public SmtpSession SmtpSession
		{
			get
			{
				return this.smtpSession;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005CE2 File Offset: 0x00003EE2
		internal void Initialize(SmtpSession session)
		{
			this.smtpSession = session;
		}

		// Token: 0x04000140 RID: 320
		private SmtpSession smtpSession;
	}
}
