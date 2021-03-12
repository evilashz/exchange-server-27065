using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200002D RID: 45
	public class AuthCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005CFC File Offset: 0x00003EFC
		internal AuthCommandEventArgs()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005D04 File Offset: 0x00003F04
		internal AuthCommandEventArgs(SmtpSession smtpSession, string authenticationMechanism) : base(smtpSession)
		{
			this.AuthenticationMechanism = authenticationMechanism;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005D1C File Offset: 0x00003F1C
		public string AuthenticationMechanism { get; internal set; }
	}
}
