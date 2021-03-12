using System;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	internal class OwaBrowserUpdateRequiredException : OwaPermanentException
	{
		// Token: 0x06000EE6 RID: 3814 RVA: 0x0005E498 File Offset: 0x0005C698
		public OwaBrowserUpdateRequiredException(BrowserPlatform browserPlatform) : base(null)
		{
			this.browserPlatform = browserPlatform;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0005E4A8 File Offset: 0x0005C6A8
		public string GetErrorDetails()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(729144936));
			stringBuilder.Append("<br><br>");
			if (this.browserPlatform == BrowserPlatform.Windows)
			{
				OwaBrowserUpdateRequiredException.AppendBrowserLink(stringBuilder, "http://microsoft.com/ie", 76632944);
				OwaBrowserUpdateRequiredException.AppendAlternateSuggestion(stringBuilder);
				OwaBrowserUpdateRequiredException.AppendBrowserLink(stringBuilder, "http://mozilla.org/firefox", 951275809);
				stringBuilder.Append("<br>");
				OwaBrowserUpdateRequiredException.AppendBrowserLink(stringBuilder, "http://www.google.com/chrome", 1899309595);
			}
			else if (this.browserPlatform == BrowserPlatform.Macintosh)
			{
				OwaBrowserUpdateRequiredException.AppendBrowserLink(stringBuilder, "http://www.apple.com/safari/", 2140109750);
				OwaBrowserUpdateRequiredException.AppendAlternateSuggestion(stringBuilder);
				OwaBrowserUpdateRequiredException.AppendBrowserLink(stringBuilder, "http://mozilla.org/firefox", 951275809);
			}
			else
			{
				OwaBrowserUpdateRequiredException.AppendBrowserLink(stringBuilder, "http://mozilla.org/firefox", 2093055582);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0005E570 File Offset: 0x0005C770
		private static void AppendBrowserLink(StringBuilder errorDetails, string linkValue, Strings.IDs linkText)
		{
			errorDetails.Append("<a href=\"");
			errorDetails.Append(linkValue);
			errorDetails.Append("\">");
			errorDetails.Append(LocalizedStrings.GetHtmlEncoded(linkText));
			errorDetails.Append("</a>");
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0005E5AB File Offset: 0x0005C7AB
		private static void AppendAlternateSuggestion(StringBuilder errorDetails)
		{
			errorDetails.Append("<br><br><br>");
			errorDetails.Append(LocalizedStrings.GetHtmlEncoded(427833413));
			errorDetails.Append("<br><br>");
		}

		// Token: 0x04000A1B RID: 2587
		private const string DownloadLocationIE = "http://microsoft.com/ie";

		// Token: 0x04000A1C RID: 2588
		private const string DownloadLocationSafari = "http://www.apple.com/safari/";

		// Token: 0x04000A1D RID: 2589
		private const string DownloadLocationFireFox = "http://mozilla.org/firefox";

		// Token: 0x04000A1E RID: 2590
		private const string DownloadLocationChrome = "http://www.google.com/chrome";

		// Token: 0x04000A1F RID: 2591
		private BrowserPlatform browserPlatform;
	}
}
