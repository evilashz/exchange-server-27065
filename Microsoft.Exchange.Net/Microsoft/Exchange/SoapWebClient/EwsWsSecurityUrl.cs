using System;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006C8 RID: 1736
	internal static class EwsWsSecurityUrl
	{
		// Token: 0x06002066 RID: 8294 RVA: 0x0003FA18 File Offset: 0x0003DC18
		public static bool IsWsSecurity(string url)
		{
			return url.EndsWith("/WSSecurity", StringComparison.OrdinalIgnoreCase) || url.EndsWith("/WSSecurity/", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0003FA36 File Offset: 0x0003DC36
		public static string Fix(string url)
		{
			if (EwsWsSecurityUrl.IsWsSecurity(url))
			{
				return url;
			}
			return EwsWsSecurityUrl.AppendSuffix(url);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0003FA48 File Offset: 0x0003DC48
		public static Uri Fix(Uri url)
		{
			if (EwsWsSecurityUrl.IsWsSecurity(url.OriginalString))
			{
				return url;
			}
			return new Uri(EwsWsSecurityUrl.AppendSuffix(url.OriginalString));
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0003FA69 File Offset: 0x0003DC69
		public static string FixForAnonymous(string url)
		{
			if (!EwsWsSecurityUrl.IsWsSecurity(url))
			{
				return url;
			}
			return EwsWsSecurityUrl.RemoveSuffix(url);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0003FA7B File Offset: 0x0003DC7B
		public static Uri FixForAnonymous(Uri url)
		{
			if (!EwsWsSecurityUrl.IsWsSecurity(url.OriginalString))
			{
				return url;
			}
			return new Uri(EwsWsSecurityUrl.RemoveSuffix(url.OriginalString));
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0003FA9C File Offset: 0x0003DC9C
		private static string RemoveSuffix(string url)
		{
			if (url.EndsWith("/WSSecurity", StringComparison.OrdinalIgnoreCase))
			{
				return url.Remove(url.Length - "/WSSecurity".Length, "/WSSecurity".Length);
			}
			return url.Remove(url.Length - "/WSSecurity/".Length, "/WSSecurity/".Length);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0003FAFA File Offset: 0x0003DCFA
		private static string AppendSuffix(string url)
		{
			if (url.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				return url + "WSSecurity";
			}
			return url + "/WSSecurity";
		}

		// Token: 0x04001F3B RID: 7995
		private const string Slash = "/";

		// Token: 0x04001F3C RID: 7996
		private const string Suffix = "WSSecurity";

		// Token: 0x04001F3D RID: 7997
		private const string SlashSuffix = "/WSSecurity";

		// Token: 0x04001F3E RID: 7998
		private const string SlashSuffixSlash = "/WSSecurity/";
	}
}
