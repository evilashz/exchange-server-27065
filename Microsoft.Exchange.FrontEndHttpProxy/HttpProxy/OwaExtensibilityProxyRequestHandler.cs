using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C1 RID: 193
	internal class OwaExtensibilityProxyRequestHandler : ProxyRequestHandler
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0002B428 File Offset: 0x00029628
		internal static bool IsOwaExtensibilityRequest(HttpRequest request)
		{
			return OwaExtensibilityProxyRequestHandler.ExtPathRegex.IsMatch(request.RawUrl) || OwaExtensibilityProxyRequestHandler.ScriptsPathRegex.IsMatch(request.RawUrl) || OwaExtensibilityProxyRequestHandler.StylesPathRegex.IsMatch(request.RawUrl);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0002B460 File Offset: 0x00029660
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			ExTraceGlobals.VerboseTracer.TraceDebug<string, Uri>((long)this.GetHashCode(), "[OwaExtensibilityProxyRequestHandler::ResolveAnchorMailbox]: Method {0}; Url {1};", base.ClientRequest.HttpMethod, base.ClientRequest.Url);
			Match match = OwaExtensibilityProxyRequestHandler.ExtPathRegex.Match(base.ClientRequest.RawUrl);
			if (!match.Success)
			{
				match = OwaExtensibilityProxyRequestHandler.ScriptsPathRegex.Match(base.ClientRequest.RawUrl);
				if (!match.Success)
				{
					match = OwaExtensibilityProxyRequestHandler.StylesPathRegex.Match(base.ClientRequest.RawUrl);
				}
			}
			Guid guid;
			string text;
			if (match.Success && RegexUtilities.TryGetMailboxGuidAddressFromRegexMatch(match, out guid, out text))
			{
				this.routingHint = string.Format("{0}@{1}", guid, text);
				AnchorMailbox result = new MailboxGuidAnchorMailbox(guid, text, this);
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "OwaExtension-MailboxGuidWithDomain");
				return result;
			}
			throw new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.ServerNotFound, string.Format("Unable to find target server for url: {0}", base.ClientRequest.Url));
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0002B55C File Offset: 0x0002975C
		protected override UriBuilder GetClientUrlForProxy()
		{
			UriBuilder uriBuilder = new UriBuilder(base.ClientRequest.Url);
			if (!string.IsNullOrEmpty(this.routingHint))
			{
				string text = base.ClientRequest.Url.AbsolutePath;
				text = HttpUtility.UrlDecode(text);
				string text2 = "/" + this.routingHint;
				int num = text.IndexOf(text2);
				if (num != -1)
				{
					string path = text.Substring(0, num) + text.Substring(num + text2.Length);
					uriBuilder.Path = path;
				}
			}
			return uriBuilder;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0002B5E4 File Offset: 0x000297E4
		protected override Uri GetTargetBackEndServerUrl()
		{
			Uri targetBackEndServerUrl = base.GetTargetBackEndServerUrl();
			return UrlUtilities.FixIntegratedAuthUrlForBackEnd(targetBackEndServerUrl);
		}

		// Token: 0x0400049F RID: 1183
		private static readonly Regex ExtPathRegex = new Regex("/owa/((?<hint>[a-fA-F0-9]{8}-([a-fA-F0-9]{4}-){3}[a-fA-F0-9]{12}@[A-Z0-9.-]+\\.[A-Z]{2,4})/)prem/\\d{2}\\.\\d{1,}\\.\\d{1,}\\.\\d{1,}/ext/def/.*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040004A0 RID: 1184
		private static readonly Regex ScriptsPathRegex = new Regex("/owa/((?<hint>[a-fA-F0-9]{8}-([a-fA-F0-9]{4}-){3}[a-fA-F0-9]{12}@[A-Z0-9.-]+\\.[A-Z]{2,4})/)prem/\\d{2}\\.\\d{1,}\\.\\d{1,}\\.\\d{1,}/scripts/.*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040004A1 RID: 1185
		private static readonly Regex StylesPathRegex = new Regex("/owa/((?<hint>[a-fA-F0-9]{8}-([a-fA-F0-9]{4}-){3}[a-fA-F0-9]{12}@[A-Z0-9.-]+\\.[A-Z]{2,4})/)prem/\\d{2}\\.\\d{1,}\\.\\d{1,}\\.\\d{1,}/resources/styles/.*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040004A2 RID: 1186
		private string routingHint;
	}
}
