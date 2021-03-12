using System;
using System.Security;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000045 RID: 69
	internal class ProcessAuthenticationEventArgs : ReceiveEventArgs
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00006290 File Offset: 0x00004490
		public ProcessAuthenticationEventArgs(SmtpSession smtpSession, byte[] userName, SecureString password) : base(smtpSession)
		{
			this.UserName = userName;
			this.Password = password;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000062A7 File Offset: 0x000044A7
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000062AF File Offset: 0x000044AF
		public byte[] UserName { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000062B8 File Offset: 0x000044B8
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000062C0 File Offset: 0x000044C0
		public SecureString Password { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000062C9 File Offset: 0x000044C9
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000062D1 File Offset: 0x000044D1
		public WindowsIdentity Identity { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000062DA File Offset: 0x000044DA
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000062E2 File Offset: 0x000044E2
		public object AuthResult { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000062EB File Offset: 0x000044EB
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000062F3 File Offset: 0x000044F3
		public string AuthErrorDetails { get; set; }
	}
}
