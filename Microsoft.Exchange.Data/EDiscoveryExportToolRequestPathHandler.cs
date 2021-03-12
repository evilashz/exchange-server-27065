using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200021D RID: 541
	internal static class EDiscoveryExportToolRequestPathHandler
	{
		// Token: 0x060012E9 RID: 4841 RVA: 0x00039CB8 File Offset: 0x00037EB8
		public static bool IsEDiscoveryExportToolRequest(HttpRequest request)
		{
			string absolutePath = request.Url.AbsolutePath;
			if (string.IsNullOrEmpty(absolutePath))
			{
				return false;
			}
			if (absolutePath.IndexOf("/exporttool/", StringComparison.OrdinalIgnoreCase) < 0)
			{
				return false;
			}
			EDiscoveryExportToolRequestPathHandler.EnsureRegexInit();
			return EDiscoveryExportToolRequestPathHandler.applicationPathRegex.IsMatch(absolutePath);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00039CFC File Offset: 0x00037EFC
		public static Match GetPathMatch(HttpRequest request)
		{
			EDiscoveryExportToolRequestPathHandler.EnsureRegexInit();
			return EDiscoveryExportToolRequestPathHandler.applicationPathRegex.Match(request.Url.AbsolutePath);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00039D18 File Offset: 0x00037F18
		private static void EnsureRegexInit()
		{
			if (EDiscoveryExportToolRequestPathHandler.applicationPathRegex == null)
			{
				lock (EDiscoveryExportToolRequestPathHandler.Monitor)
				{
					if (EDiscoveryExportToolRequestPathHandler.applicationPathRegex == null)
					{
						EDiscoveryExportToolRequestPathHandler.applicationPathRegex = new Regex(EDiscoveryExportToolRequestPathHandler.applicationPathPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
					}
				}
			}
		}

		// Token: 0x04000B2B RID: 2859
		private static readonly object Monitor = new object();

		// Token: 0x04000B2C RID: 2860
		private static readonly string applicationPathPattern = "/ecp/(?<major>\\d{2})\\.(?<minor>\\d{1,})\\.(?<build>\\d{1,})\\.(?<revision>\\d{1,})/exporttool/.*$";

		// Token: 0x04000B2D RID: 2861
		private static Regex applicationPathRegex;
	}
}
