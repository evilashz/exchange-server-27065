using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000489 RID: 1161
	internal class ResourcePathBuilderUtilities
	{
		// Token: 0x06002777 RID: 10103 RVA: 0x0009286D File Offset: 0x00090A6D
		public static string GetManifestDiskRelativeFolderPath(string owaVersion)
		{
			return Path.Combine(ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(owaVersion), "manifests");
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0009287F File Offset: 0x00090A7F
		public static string GetResourcesRelativeFolderPath(string owaVersion)
		{
			return string.Format("prem/{0}", owaVersion);
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0009288C File Offset: 0x00090A8C
		public static string GetScriptResourcesRelativeFolderPath(string resourcesRelativeFolderPath)
		{
			return resourcesRelativeFolderPath + "/scripts";
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00092899 File Offset: 0x00090A99
		public static string GetGlobalizeScriptResourcesRelativeFolderPath(string resourcesRelativeFolderPath)
		{
			return ResourcePathBuilderUtilities.GetScriptResourcesRelativeFolderPath(resourcesRelativeFolderPath) + "/globalize";
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000928AB File Offset: 0x00090AAB
		public static string GetLocalizedScriptResourcesRelativeFolderPath(string resourcesRelativeFolderPath)
		{
			return ResourcePathBuilderUtilities.GetScriptResourcesRelativeFolderPath(resourcesRelativeFolderPath) + "/ext";
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000928BD File Offset: 0x00090ABD
		public static string GetLocalizedScriptResourcesRelativeFolderPath(string resourcesRelativeFolderPath, string cultureName)
		{
			return ResourcePathBuilderUtilities.GetLocalizedScriptResourcesRelativeFolderPath(resourcesRelativeFolderPath) + "/" + cultureName;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000928D0 File Offset: 0x00090AD0
		public static string GetBootResourcesRelativeFolderPath(string resourcesRelativeFolderPath)
		{
			return resourcesRelativeFolderPath + "/resources";
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000928DD File Offset: 0x00090ADD
		public static string GetStyleResourcesRelativeFolderPath(string resourcesRelativeFolderPath)
		{
			return ResourcePathBuilderUtilities.GetBootResourcesRelativeFolderPath(resourcesRelativeFolderPath) + "/styles";
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000928EF File Offset: 0x00090AEF
		public static string GetBootImageResourcesRelativeFolderPath(string resourcesRelativeFolderPath, bool isRtl)
		{
			return string.Format("{0}/resources/images/{1}", resourcesRelativeFolderPath, isRtl ? "rtl" : "0");
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0009290B File Offset: 0x00090B0B
		public static string GetThemeResourcesRelativeFolderPath(string resourcesRelativeFolderPath)
		{
			return ResourcePathBuilderUtilities.GetBootResourcesRelativeFolderPath(resourcesRelativeFolderPath) + "/themes/{0}/";
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0009291D File Offset: 0x00090B1D
		public static string GetStyleResourcesRelativeFolderPathWithSlash(string resourcesRelativeFolderPath)
		{
			return ResourcePathBuilderUtilities.GetStyleResourcesRelativeFolderPath(resourcesRelativeFolderPath) + "/";
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x0009292F File Offset: 0x00090B2F
		public static string GetThemeImageResourcesRelativeFolderPath(string resourcesRelativeFolderPath)
		{
			return ResourcePathBuilderUtilities.GetThemeResourcesRelativeFolderPath(resourcesRelativeFolderPath) + "images/";
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00092941 File Offset: 0x00090B41
		public static string GetImageResourcesRelativeFolderPath(string resourcesRelativeFolderPath)
		{
			return ResourcePathBuilderUtilities.GetBootResourcesRelativeFolderPath(resourcesRelativeFolderPath) + "/images/";
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00092953 File Offset: 0x00090B53
		public static string GetBootThemeImageResourcesRelativeFolderPath(string version, string resourcesRelativeFolderPath, bool isRtl, bool shouldSkipThemeFolder)
		{
			if (!shouldSkipThemeFolder)
			{
				return ResourcePathBuilderUtilities.GetThemedLocaleImageResourcesRelativeFolderPath(resourcesRelativeFolderPath, isRtl);
			}
			return string.Format("{0}/resources/images/{1}", resourcesRelativeFolderPath, isRtl ? "rtl" : "0");
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0009297A File Offset: 0x00090B7A
		public static string GetThemedLocaleImageResourcesRelativeFolderPath(string resourcesRelativeFolderPath, bool isRtl)
		{
			return string.Format("{0}/resources/themes/{1}/images/{2}", resourcesRelativeFolderPath, "{0}", isRtl ? "rtl" : "0");
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0009299B File Offset: 0x00090B9B
		public static string GetBootStyleResourcesRelativeFolderPath(string version, string resourcesRelativeFolderPath, string stylesFolderCulturePlaceHolder, bool shouldSkipThemeFolder)
		{
			if (!shouldSkipThemeFolder)
			{
				return string.Format("{0}/resources/themes/{1}/{2}", resourcesRelativeFolderPath, "{0}", stylesFolderCulturePlaceHolder);
			}
			return string.Format("{0}/resources/styles/{1}", resourcesRelativeFolderPath, stylesFolderCulturePlaceHolder);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000929BE File Offset: 0x00090BBE
		public static string GetScriptResourcesRootFolderPath(string exchangeInstallPath, string resourcesRelativeFolderPath)
		{
			return Path.Combine(exchangeInstallPath, string.Format("ClientAccess\\Owa\\{0}", ResourcePathBuilderUtilities.GetScriptResourcesRelativeFolderPath(resourcesRelativeFolderPath)));
		}
	}
}
