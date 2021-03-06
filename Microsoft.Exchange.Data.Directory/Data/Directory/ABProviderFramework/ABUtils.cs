using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000015 RID: 21
	internal static class ABUtils
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00004134 File Offset: 0x00002334
		public static string EmailToLegacyExchangeDN(string email, string providerName)
		{
			return "/o=" + providerName + "/cn=" + email;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004148 File Offset: 0x00002348
		public static ProxyAddress LegacyExchangeDNToProxyAddress(string legacyExchangeDN, string providerName)
		{
			if (legacyExchangeDN.StartsWith("/o=" + providerName, StringComparison.OrdinalIgnoreCase))
			{
				int num = legacyExchangeDN.IndexOf("/cn=");
				string text = legacyExchangeDN.Substring(num + 4);
				if (!string.IsNullOrEmpty(text))
				{
					return ProxyAddress.Parse(text);
				}
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "legacyExchangeDN '{0}' is not generated by the '{1}' provider or is invalid.", new object[]
			{
				legacyExchangeDN,
				providerName
			}));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000041B2 File Offset: 0x000023B2
		public static bool IsValidEmailAddress(string emailAddress)
		{
			return !string.IsNullOrEmpty(emailAddress) && SmtpAddress.IsValidSmtpAddress(emailAddress);
		}
	}
}
