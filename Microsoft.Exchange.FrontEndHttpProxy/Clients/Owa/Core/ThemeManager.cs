using System;
using System.IO;
using Microsoft.Exchange.HttpProxy;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000071 RID: 113
	public sealed class ThemeManager
	{
		// Token: 0x0600036F RID: 879 RVA: 0x000143B0 File Offset: 0x000125B0
		public static void RenderBaseThemeFileUrl(TextWriter writer, ThemeFileId themeFileId)
		{
			ThemeManager.RenderBaseThemeFileUrl(writer, themeFileId, false);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000143BA File Offset: 0x000125BA
		public static void RenderBaseThemeFileUrl(TextWriter writer, ThemeFileId themeFileId, bool useCDN)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ThemeManager.RenderThemeFileUrl(writer, (int)themeFileId, false, useCDN);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000143D3 File Offset: 0x000125D3
		public static void RenderThemeFileUrl(TextWriter writer, int themeFileIndex, bool isBasicExperience, bool useCDN)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ThemeManager.RenderThemeFilePath(writer, themeFileIndex, isBasicExperience, useCDN);
			writer.Write(ThemeFileList.GetNameFromId(themeFileIndex));
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000143FC File Offset: 0x000125FC
		private static bool RenderThemeFilePath(TextWriter writer, int themeFileIndex, bool isBasicExperience, bool useCDN)
		{
			writer.Write(ThemeManager.themesFolderPath);
			bool flag = ThemeFileList.IsResourceFile(themeFileIndex);
			if (flag)
			{
				writer.Write(ThemeManager.ResourcesFolderName);
			}
			else if (isBasicExperience)
			{
				writer.Write(ThemeManager.BasicFilesFolderName);
			}
			else
			{
				writer.Write(ThemeManager.BaseThemeFolderName);
			}
			writer.Write("/");
			return !flag;
		}

		// Token: 0x0400026B RID: 619
		public static readonly string BaseThemeFolderName = "base";

		// Token: 0x0400026C RID: 620
		public static readonly string BasicFilesFolderName = "basic";

		// Token: 0x0400026D RID: 621
		public static readonly string ResourcesFolderName = "resources";

		// Token: 0x0400026E RID: 622
		public static readonly string DataCenterThemeStorageId = "datacenter";

		// Token: 0x0400026F RID: 623
		private static readonly string ThemesFolderName = "themes";

		// Token: 0x04000270 RID: 624
		private static string themesFolderPath = string.Format("{0}/{1}/", HttpProxyGlobals.ApplicationVersion, ThemeManager.ThemesFolderName);
	}
}
