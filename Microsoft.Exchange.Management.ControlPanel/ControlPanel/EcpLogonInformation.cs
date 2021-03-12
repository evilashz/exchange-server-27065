using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003A7 RID: 935
	internal class EcpLogonInformation
	{
		// Token: 0x0600315A RID: 12634 RVA: 0x00097F7D File Offset: 0x0009617D
		internal EcpLogonInformation(SecurityIdentifier logonMailboxSid, IIdentity logonUserIdentity, IIdentity impersonatedUserIdentity)
		{
			if (logonMailboxSid == null)
			{
				throw new ArgumentNullException("logonMailboxSid");
			}
			if (logonUserIdentity == null)
			{
				throw new ArgumentNullException("LogonUserIdentity");
			}
			this.LogonMailboxSid = logonMailboxSid;
			this.LogonUser = logonUserIdentity;
			this.ImpersonatedUser = impersonatedUserIdentity;
		}

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x00097FBC File Offset: 0x000961BC
		public bool Impersonated
		{
			get
			{
				return this.ImpersonatedUser != null;
			}
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x00097FCC File Offset: 0x000961CC
		public string Name
		{
			get
			{
				string safeName = this.LogonUser.GetSafeName(true);
				if (this.Impersonated)
				{
					return Strings.OnbehalfOf(safeName, this.ImpersonatedUser.GetSafeName(true));
				}
				return safeName;
			}
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x00098008 File Offset: 0x00096208
		public static EcpLogonInformation Create(string logonAccountSddlSid, string logonMailboxSddlSid, string targetMailboxSddlSid, SerializedAccessToken proxySecurityAccessToken)
		{
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(logonMailboxSddlSid);
			IIdentity logonUserIdentity = (proxySecurityAccessToken != null) ? new SerializedIdentity(proxySecurityAccessToken) : new GenericSidIdentity(logonMailboxSddlSid, string.Empty, securityIdentifier);
			IIdentity impersonatedUserIdentity = (string.IsNullOrEmpty(targetMailboxSddlSid) || logonMailboxSddlSid == targetMailboxSddlSid) ? null : new GenericSidIdentity(targetMailboxSddlSid, string.Empty, new SecurityIdentifier(targetMailboxSddlSid));
			return new EcpLogonInformation(securityIdentifier, logonUserIdentity, impersonatedUserIdentity);
		}

		// Token: 0x040023F0 RID: 9200
		public readonly SecurityIdentifier LogonMailboxSid;

		// Token: 0x040023F1 RID: 9201
		public readonly IIdentity LogonUser;

		// Token: 0x040023F2 RID: 9202
		public readonly IIdentity ImpersonatedUser;
	}
}
