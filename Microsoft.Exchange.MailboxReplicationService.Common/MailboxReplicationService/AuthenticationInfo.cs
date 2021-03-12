using System;
using System.Security.Principal;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000CD RID: 205
	internal class AuthenticationInfo : IAuthenticationInfo
	{
		// Token: 0x060007D6 RID: 2006 RVA: 0x0000CF7B File Offset: 0x0000B17B
		public AuthenticationInfo(WindowsIdentity identity, string name)
		{
			this.principal = new WindowsPrincipal(identity);
			this.name = name;
			this.isCertificateAuthentication = false;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0000CF9D File Offset: 0x0000B19D
		public AuthenticationInfo(bool isCertificateAuthentication)
		{
			this.principal = null;
			this.name = null;
			this.isCertificateAuthentication = isCertificateAuthentication;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0000CFBA File Offset: 0x0000B1BA
		public AuthenticationInfo(SecurityIdentifier sid)
		{
			this.sid = sid;
			this.principal = null;
			this.name = null;
			this.isCertificateAuthentication = false;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0000CFDE File Offset: 0x0000B1DE
		WindowsPrincipal IAuthenticationInfo.WindowsPrincipal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0000CFE6 File Offset: 0x0000B1E6
		string IAuthenticationInfo.PrincipalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0000CFEE File Offset: 0x0000B1EE
		bool IAuthenticationInfo.IsCertificateAuthentication
		{
			get
			{
				return this.isCertificateAuthentication;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0000CFF6 File Offset: 0x0000B1F6
		SecurityIdentifier IAuthenticationInfo.Sid
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x040004A5 RID: 1189
		private WindowsPrincipal principal;

		// Token: 0x040004A6 RID: 1190
		private SecurityIdentifier sid;

		// Token: 0x040004A7 RID: 1191
		private string name;

		// Token: 0x040004A8 RID: 1192
		private bool isCertificateAuthentication;
	}
}
