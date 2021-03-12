using System;
using System.Linq;
using System.Web;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000098 RID: 152
	internal static class UrlUtilities
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0001A064 File Offset: 0x00018264
		public static bool IsEcpUrl(string urlString)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return false;
			}
			if (urlString.Equals("/ecp", StringComparison.OrdinalIgnoreCase) || urlString.StartsWith("/ecp/", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			Uri uri = null;
			return Uri.TryCreate(urlString, UriKind.Absolute, out uri) && uri != null && (uri.AbsolutePath.Equals("/ecp", StringComparison.OrdinalIgnoreCase) || uri.AbsolutePath.StartsWith("/ecp/", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001A0DC File Offset: 0x000182DC
		public static bool IsEacUrl(string urlString)
		{
			if (!UrlUtilities.IsEcpUrl(urlString))
			{
				return false;
			}
			int num = urlString.IndexOf('?');
			if (num > 0)
			{
				string[] source = urlString.Substring(num + 1).Split(new char[]
				{
					'&'
				});
				return !source.Contains("rfr=owa") && !source.Contains("rfr=olk");
			}
			return true;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001A13B File Offset: 0x0001833B
		public static bool IsIntegratedAuthUrl(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			return url.AbsolutePath.IndexOf(Constants.IntegratedAuthPathWithTrailingSlash, StringComparison.OrdinalIgnoreCase) != -1 || url.AbsolutePath.EndsWith(Constants.IntegratedAuthPath, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001A178 File Offset: 0x00018378
		public static Uri FixIntegratedAuthUrlForBackEnd(Uri url)
		{
			if (!UrlUtilities.IsIntegratedAuthUrl(url))
			{
				return url;
			}
			UriBuilder uriBuilder = new UriBuilder(url);
			string absolutePath = url.AbsolutePath;
			int num = url.AbsolutePath.IndexOf(Constants.IntegratedAuthPath, StringComparison.OrdinalIgnoreCase);
			uriBuilder.Path = absolutePath.Substring(0, num) + absolutePath.Substring(num + Constants.IntegratedAuthPath.Length);
			return uriBuilder.Uri;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001A1DC File Offset: 0x000183DC
		public static Uri FixDFPOWAVdirUrlForBackEnd(Uri url, string dfpOwaVdir)
		{
			if (string.IsNullOrEmpty(dfpOwaVdir))
			{
				return url;
			}
			UriBuilder uriBuilder = new UriBuilder(url);
			string absolutePath = url.AbsolutePath;
			int num = url.AbsolutePath.IndexOf("/owa", StringComparison.OrdinalIgnoreCase);
			uriBuilder.Path = absolutePath.Substring(0, num + 1) + dfpOwaVdir;
			int num2 = num + "/owa".Length;
			if (absolutePath.Length > num2)
			{
				uriBuilder.Path += absolutePath.Substring(num2);
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001A260 File Offset: 0x00018460
		internal static bool IsOwaMiniUrl(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			return url.AbsolutePath.EndsWith(Constants.OMAPath, StringComparison.OrdinalIgnoreCase) || url.AbsolutePath.IndexOf(Constants.OMAPath + "/", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001A2B8 File Offset: 0x000184B8
		internal static bool IsCmdWebPart(HttpRequest request)
		{
			string text = request.QueryString["cmd"];
			return !string.IsNullOrEmpty(text) && string.Equals(text, "contents", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000389 RID: 905
		private const string Command = "cmd";

		// Token: 0x0400038A RID: 906
		private const string CommandValue = "contents";
	}
}
