using System;
using System.Globalization;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x020001BB RID: 443
	internal class ProxyAddressHasher
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x00058678 File Offset: 0x00056878
		public ProxyAddressHasher()
		{
			this.hasher = new StringHasher(UsageScenario.Production);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0005868C File Offset: 0x0005688C
		public ProxyAddressHasher(UsageScenario scenario)
		{
			this.hasher = new StringHasher(scenario);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000586A0 File Offset: 0x000568A0
		public static string GetHashedFormWithoutPrefix(StringHasher stringHasher, string smtpAddress)
		{
			return stringHasher.GetHash(smtpAddress).ToString("X16", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000586C8 File Offset: 0x000568C8
		public string GetHashedFormWithPrefix(string smtpAddress)
		{
			return "sh:" + this.hasher.GetHash(smtpAddress).ToString("X16", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04000A90 RID: 2704
		internal const string SmtpHashPrefix = "sh";

		// Token: 0x04000A91 RID: 2705
		internal const string SmtpHashPrefixWithColon = "sh:";

		// Token: 0x04000A92 RID: 2706
		internal const string HashCodeFormat = "X16";

		// Token: 0x04000A93 RID: 2707
		private StringHasher hasher;
	}
}
