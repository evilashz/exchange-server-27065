using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200066F RID: 1647
	public class ThemeResource
	{
		// Token: 0x1700276B RID: 10091
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x000D86E4 File Offset: 0x000D68E4
		private static string ContentDeliveryNetworkEndpoint
		{
			get
			{
				string text = ConfigurationManager.AppSettings["ContentDeliveryNetworkEndpoint"];
				if (string.IsNullOrWhiteSpace(text))
				{
					return string.Empty;
				}
				text = text.Trim();
				if (text.EndsWith("/", StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring(0, text.Length - 1);
				}
				return text;
			}
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x000D8735 File Offset: 0x000D6935
		public static string GetThemeResource(Control c, string resourceName)
		{
			return ThemeResource.Private_GetThemeResource(c, resourceName);
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x000D8740 File Offset: 0x000D6940
		public static string Private_GetThemeResource(Control c, string resourceName)
		{
			string text = string.Empty;
			IThemable themable = c.Page as IThemable;
			text = ThemeManager.GetDefaultThemeName((themable != null) ? themable.FeatureSet : FeatureSet.Admin);
			string contentDeliveryNetworkEndpoint = ThemeResource.ContentDeliveryNetworkEndpoint;
			int capacity = contentDeliveryNetworkEndpoint.Length + ThemeResource.LocalThemesPath.Length + text.Length + 1 + resourceName.Length;
			StringBuilder stringBuilder = new StringBuilder(contentDeliveryNetworkEndpoint, capacity);
			stringBuilder.Append(ThemeResource.LocalThemesPath);
			stringBuilder.Append(text);
			stringBuilder.Append("/");
			stringBuilder.Append(resourceName.ToLower());
			return c.ResolveUrl(stringBuilder.ToString());
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000D87E0 File Offset: 0x000D69E0
		public static string GetThemeResource(string theme, string resourceName)
		{
			string contentDeliveryNetworkEndpoint = ThemeResource.ContentDeliveryNetworkEndpoint;
			int capacity = contentDeliveryNetworkEndpoint.Length + ThemeResource.LocalThemesPath.Length + theme.Length + 1 + resourceName.Length;
			StringBuilder stringBuilder = new StringBuilder(contentDeliveryNetworkEndpoint, capacity);
			stringBuilder.Append(ThemeResource.LocalThemesPath);
			stringBuilder.Append(theme);
			stringBuilder.Append("/");
			stringBuilder.Append(resourceName.ToLower());
			Uri uri = new Uri(stringBuilder.ToString(), UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				uri = new Uri(HttpContext.Current.GetRequestUrl(), uri);
			}
			return uri.ToEscapedString();
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x000D8878 File Offset: 0x000D6A78
		private static string GetApplicationVersion()
		{
			string text = typeof(ThemeResource).GetApplicationVersion();
			if (string.IsNullOrEmpty(text))
			{
				text = "Current";
			}
			return text;
		}

		// Token: 0x04002FFE RID: 12286
		private const string CurrentDirectory = "Current";

		// Token: 0x04002FFF RID: 12287
		public const string DefaultTheme = "default";

		// Token: 0x04003000 RID: 12288
		public static readonly string ApplicationVersion = ThemeResource.GetApplicationVersion();

		// Token: 0x04003001 RID: 12289
		private static readonly string LocalThemesPath = HttpRuntime.AppDomainAppVirtualPath + "/" + ThemeResource.ApplicationVersion + "/themes/";

		// Token: 0x04003002 RID: 12290
		private static readonly string LocalScriptPath = HttpRuntime.AppDomainAppVirtualPath + "/" + ThemeResource.ApplicationVersion + "/scripts/";

		// Token: 0x04003003 RID: 12291
		private static readonly string LocalExportToolPath = HttpRuntime.AppDomainAppVirtualPath + "/" + ThemeResource.ApplicationVersion + "/exporttool/";

		// Token: 0x04003004 RID: 12292
		public static readonly string ScriptPath = ThemeResource.ContentDeliveryNetworkEndpoint + ThemeResource.LocalScriptPath;

		// Token: 0x04003005 RID: 12293
		public static readonly string ExportToolPath = ThemeResource.ContentDeliveryNetworkEndpoint + ThemeResource.LocalExportToolPath + (string.IsNullOrEmpty(ThemeResource.ContentDeliveryNetworkEndpoint) ? "{0}/" : string.Empty);

		// Token: 0x04003006 RID: 12294
		public static readonly string BlankHtmlPath = ThemeResource.LocalScriptPath + "blank.htm";
	}
}
