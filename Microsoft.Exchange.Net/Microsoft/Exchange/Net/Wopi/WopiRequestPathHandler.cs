using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.Net.Wopi
{
	// Token: 0x0200096F RID: 2415
	public static class WopiRequestPathHandler
	{
		// Token: 0x06003441 RID: 13377 RVA: 0x00080198 File Offset: 0x0007E398
		public static bool IsWopiRequest(HttpRequest request, bool isFrontend)
		{
			string text = request.Url.AbsolutePath;
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			text = HttpUtility.UrlDecode(text);
			if (text.IndexOf("/wopi/", StringComparison.OrdinalIgnoreCase) < 0)
			{
				return false;
			}
			WopiRequestPathHandler.EnsureRegexInit(isFrontend);
			bool flag = WopiRequestPathHandler.wopiUrlRegex.IsMatch(text);
			if (!flag)
			{
				ExTraceGlobals.VerboseTracer.TraceWarning<string, Uri>((long)typeof(WopiRequestPathHandler).GetHashCode(), "[WopiRequestMatcher::IsWopiRequest]: Method {0}; Url {1}; Matched /wopi/ but regex match failed.", request.HttpMethod, request.Url);
			}
			return flag;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x00080214 File Offset: 0x0007E414
		public static string GetUserEmailAddress(HttpRequest request)
		{
			string text = request.Url.AbsolutePath;
			text = HttpUtility.UrlDecode(text);
			WopiRequestPathHandler.EnsureRegexInit(false);
			Match match = WopiRequestPathHandler.wopiUrlRegex.Match(text);
			string value = match.Groups[1].Value;
			return value.Trim();
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x00080260 File Offset: 0x0007E460
		public static string StripEmailAddress(string path, string emailAddress)
		{
			string str = path.Substring(0, 5);
			string str2 = path.Substring(5 + (emailAddress.Length + 1));
			return str + str2;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x00080290 File Offset: 0x0007E490
		private static void EnsureRegexInit(bool isFrontEnd)
		{
			if (WopiRequestPathHandler.wopiUrlRegex == null)
			{
				lock (WopiRequestPathHandler.Monitor)
				{
					if (WopiRequestPathHandler.wopiUrlRegex == null)
					{
						if (isFrontEnd)
						{
							WopiRequestPathHandler.wopiUrlRegex = new Regex(WopiRequestPathHandler.WopiUrlRegexPatternFrontend, RegexOptions.IgnoreCase | RegexOptions.Compiled);
						}
						else if (WopiRequestPathHandler.IsRunningDfpowa)
						{
							WopiRequestPathHandler.wopiUrlRegex = new Regex(WopiRequestPathHandler.DfpowaWopiUrlRegexPatternBackend, RegexOptions.IgnoreCase | RegexOptions.Compiled);
						}
						else
						{
							WopiRequestPathHandler.wopiUrlRegex = new Regex(WopiRequestPathHandler.WopiUrlRegexPatternBackend, RegexOptions.IgnoreCase | RegexOptions.Compiled);
						}
					}
				}
			}
		}

		// Token: 0x04002C50 RID: 11344
		private static readonly bool IsRunningDfpowa = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["IsPreCheckinApp"]) && StringComparer.OrdinalIgnoreCase.Equals("true", ConfigurationManager.AppSettings["IsPreCheckinApp"]);

		// Token: 0x04002C51 RID: 11345
		private static readonly object Monitor = new object();

		// Token: 0x04002C52 RID: 11346
		private static readonly string WopiUrlRegexPatternFrontend = "^/owa/(.+?@.+?)/wopi/files/@/owaatt(/contents)?$";

		// Token: 0x04002C53 RID: 11347
		private static readonly string WopiUrlRegexPatternBackend = "^/owa/wopi/files/@/owaatt(/contents)?$";

		// Token: 0x04002C54 RID: 11348
		private static readonly string DfpowaWopiUrlRegexPatternBackend = "^/dfpowa[1-5]?/wopi/files/@/owaatt(/contents)?$";

		// Token: 0x04002C55 RID: 11349
		private static Regex wopiUrlRegex;
	}
}
