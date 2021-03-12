using System;
using System.Globalization;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000013 RID: 19
	internal abstract class CobrandingAssetReader
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000698C File Offset: 0x00004B8C
		public static string GetBrandId()
		{
			if (HttpContext.Current.Request.Cookies["MH"] != null)
			{
				return HttpContext.Current.Request.Cookies["MH"].Value;
			}
			return "Null";
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000069D8 File Offset: 0x00004BD8
		public string GetOrganizationName(string defaultName)
		{
			string text = this.GetString(CobrandingAssetKey.OrganizationName);
			if (string.IsNullOrEmpty(text))
			{
				text = defaultName;
			}
			return text;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000069F8 File Offset: 0x00004BF8
		public bool HasAssetValue(CobrandingAssetKey assetKey)
		{
			return !string.IsNullOrEmpty(this.GetString(assetKey));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006A0C File Offset: 0x00004C0C
		public string GetBrandImageFileUrl(CobrandingAssetKey assetKey)
		{
			string @string = this.GetString(assetKey);
			if (string.IsNullOrEmpty(@string))
			{
				return null;
			}
			return this.GetBrandResourceUrlString() + @string;
		}

		// Token: 0x06000095 RID: 149
		public abstract bool IsPreviewBrand();

		// Token: 0x06000096 RID: 150
		public abstract string GetString(CobrandingAssetKey assetKey);

		// Token: 0x06000097 RID: 151
		public abstract string GetBrandVersion(CultureInfo cultureInfo);

		// Token: 0x06000098 RID: 152
		public abstract string GetBrandResourceUrlString();

		// Token: 0x06000099 RID: 153
		public abstract string GetLocale(CultureInfo culture);

		// Token: 0x0600009A RID: 154
		public abstract string GetThemeThumbnailUrl();

		// Token: 0x0600009B RID: 155
		public abstract string GetThemeTitle();

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009C RID: 156
		public abstract bool ShouldEnableCustomTheme { get; }

		// Token: 0x0600009D RID: 157 RVA: 0x00006A38 File Offset: 0x00004C38
		protected void LogInitializeException(Exception e, ExEventLog.EventTuple tuple)
		{
			if (!CobrandingAssetReader.initializeErrorLogged)
			{
				CobrandingAssetReader.initializeErrorLogged = true;
				LoggingUtilities.LogEvent(tuple, new object[]
				{
					e.ToString()
				});
				if (!(e is ThreadAbortException))
				{
					LoggingUtilities.SendWatson(e);
				}
			}
		}

		// Token: 0x04000208 RID: 520
		protected static bool initializeErrorLogged;
	}
}
