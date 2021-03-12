using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x0200013C RID: 316
	internal static class IPAddressValidation
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x00035550 File Offset: 0x00033750
		static IPAddressValidation()
		{
			string pattern = "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
			IPAddressValidation.regExValidIPv4 = new Regex(pattern, RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
			string text = string.Format("^10\\.{0}\\.{1}\\.{2}$", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])");
			string text2 = string.Format("^172\\.(1[6-9]|2[0-9]|3[0-1])\\.{0}\\.{1}$", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])");
			string text3 = string.Format("^192\\.168\\.{0}\\.{1}$", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])");
			string text4 = string.Format("^127\\.0\\.0\\.{0}$", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])");
			string text5 = string.Format("^169\\.254\\.{0}\\.{1}$", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])", "(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])");
			pattern = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", new object[]
			{
				text,
				text2,
				text3,
				text4,
				text5,
				"^0\\.0\\.0\\.0$"
			});
			IPAddressValidation.regExReservedIPv4 = new Regex(pattern, RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00035621 File Offset: 0x00033821
		internal static bool IsValidIPv4Address(string ipAddr)
		{
			return IPAddressValidation.regExValidIPv4.IsMatch(ipAddr);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0003562E File Offset: 0x0003382E
		internal static bool IsReservedIPv4Address(string ipAddr)
		{
			return IPAddressValidation.regExReservedIPv4.IsMatch(ipAddr);
		}

		// Token: 0x04000598 RID: 1432
		private static Regex regExValidIPv4;

		// Token: 0x04000599 RID: 1433
		private static Regex regExReservedIPv4;
	}
}
