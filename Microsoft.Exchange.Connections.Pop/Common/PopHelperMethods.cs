using System;
using Microsoft.Exchange.Connections.Pop;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200001A RID: 26
	internal static class PopHelperMethods
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000061B7 File Offset: 0x000043B7
		public static string ToStringParameterValue(Pop3SecurityMechanism securityMechanism)
		{
			if (securityMechanism == Pop3SecurityMechanism.None)
			{
				return string.Empty;
			}
			return securityMechanism.ToString();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000061CD File Offset: 0x000043CD
		public static Pop3SecurityMechanism ToPopSecurityMechanism(string securityMechanism)
		{
			if (string.IsNullOrWhiteSpace(securityMechanism))
			{
				return Pop3SecurityMechanism.None;
			}
			return (Pop3SecurityMechanism)Enum.Parse(typeof(Pop3SecurityMechanism), securityMechanism, true);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000061EF File Offset: 0x000043EF
		public static string ToStringParameterValue(Pop3AuthenticationMechanism authMechanism)
		{
			return authMechanism.ToString();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000061FC File Offset: 0x000043FC
		public static Pop3AuthenticationMechanism ToPopAuthenticationMechanism(string authMechanism)
		{
			if (string.IsNullOrWhiteSpace(authMechanism))
			{
				return Pop3AuthenticationMechanism.Basic;
			}
			return (Pop3AuthenticationMechanism)Enum.Parse(typeof(Pop3AuthenticationMechanism), authMechanism, true);
		}
	}
}
