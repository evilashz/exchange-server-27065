using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000032 RID: 50
	public class EndOfAuthenticationEventArgs : ReceiveEventArgs
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00005E2E File Offset: 0x0000402E
		internal EndOfAuthenticationEventArgs(SmtpSession smtpSession, string authenticationMechanism, string remoteIdentityName) : base(smtpSession)
		{
			this.AuthenticationMechanism = authenticationMechanism;
			this.RemoteIdentityName = remoteIdentityName;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005E45 File Offset: 0x00004045
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005E4D File Offset: 0x0000404D
		public string AuthenticationMechanism { get; internal set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005E56 File Offset: 0x00004056
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00005E5E File Offset: 0x0000405E
		public string RemoteIdentityName { get; internal set; }
	}
}
