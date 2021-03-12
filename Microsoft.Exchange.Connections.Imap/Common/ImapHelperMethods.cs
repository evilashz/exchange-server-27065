using System;
using Microsoft.Exchange.Connections.Imap;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200000D RID: 13
	internal static class ImapHelperMethods
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00006328 File Offset: 0x00004528
		public static string ToStringParameterValue(ImapAuthenticationMechanism authMechanism)
		{
			return authMechanism.ToString();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006335 File Offset: 0x00004535
		public static ImapAuthenticationMechanism ToImapAuthenticationMechanism(string authMechanism)
		{
			if (string.IsNullOrWhiteSpace(authMechanism))
			{
				return ImapAuthenticationMechanism.Basic;
			}
			return (ImapAuthenticationMechanism)Enum.Parse(typeof(ImapAuthenticationMechanism), authMechanism, true);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006357 File Offset: 0x00004557
		public static string ToStringParameterValue(ImapSecurityMechanism securityMechanism)
		{
			return securityMechanism.ToString();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006364 File Offset: 0x00004564
		public static ImapSecurityMechanism ToImapSecurityMechanism(string securityMechanism)
		{
			if (string.IsNullOrWhiteSpace(securityMechanism))
			{
				return ImapSecurityMechanism.None;
			}
			return (ImapSecurityMechanism)Enum.Parse(typeof(ImapSecurityMechanism), securityMechanism, true);
		}
	}
}
