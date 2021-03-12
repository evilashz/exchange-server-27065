using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001CA RID: 458
	public sealed class UMLanguagePackHelper
	{
		// Token: 0x06001020 RID: 4128 RVA: 0x00031110 File Offset: 0x0002F310
		public static bool IsUmLanguagePack(string pathname)
		{
			string input = Path.GetFileName(pathname).ToLower();
			string pattern = string.Format("[{0}][a-z][a-z]-[a-z][a-z][{1}]", "UMLanguagePack.".ToLower(), ".exe");
			string pattern2 = string.Format("[{0}][a-z][a-z][a-z]-[a-z][a-z][{1}]", "UMLanguagePack.".ToLower(), ".exe");
			string pattern3 = string.Format("[{0}][a-z][a-z]-[a-z][a-z][a-z][a-z]-[a-z][a-z][{1}]", "UMLanguagePack.".ToLower(), ".exe");
			return Regex.IsMatch(input, pattern) | Regex.IsMatch(input, pattern2) | Regex.IsMatch(input, pattern3);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00031194 File Offset: 0x0002F394
		public static LongPath GetUMLanguagePackFilename(string pathDirectory, CultureInfo culture)
		{
			LongPath result = null;
			string path = Path.Combine(pathDirectory, "UMLanguagePack." + culture.ToString() + ".exe");
			if (!LongPath.TryParse(path, out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000311CC File Offset: 0x0002F3CC
		public static LocalLongFullPath GetAddUMLanguageLogPath(string setupLoggingPath, CultureInfo culture)
		{
			string path = string.Format("add-{0}{1}.msilog", "UMLanguagePack.", culture.ToString());
			string path2 = Path.Combine(setupLoggingPath, path);
			LocalLongFullPath result = null;
			if (!LocalLongFullPath.TryParse(path2, out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000999 RID: 2457
		private const string languagePackFilePrefix = "UMLanguagePack.";

		// Token: 0x0400099A RID: 2458
		private const string languagePackExtension = ".exe";
	}
}
