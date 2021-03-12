using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000031 RID: 49
	internal sealed class AutodiscoverProxy
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00007EF3 File Offset: 0x000060F3
		static AutodiscoverProxy()
		{
			CertificateValidationManager.RegisterCallback("AutodiscoverProxy", delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				if (!(sender is HttpWebRequest))
				{
					return false;
				}
				HttpWebRequest httpWebRequest = (HttpWebRequest)sender;
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_ErrProviderRedirectServerCertificate, Common.PeriodicKey, new object[]
				{
					(httpWebRequest.RequestUri != null) ? httpWebRequest.RequestUri.Host : "Unknown Host",
					(certificate == null) ? "No Certificate" : certificate.Subject,
					sslPolicyErrors
				});
				return SslConfiguration.AllowExternalUntrustedCerts;
			});
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007F30 File Offset: 0x00006130
		internal static bool TryParseOutlookUserAgent(string userAgent, out int majorVersion, out int? minorVersion, out int? buildNumber)
		{
			majorVersion = 0;
			minorVersion = null;
			buildNumber = null;
			if (string.IsNullOrEmpty(userAgent))
			{
				return false;
			}
			Match match = AutodiscoverProxy.outlookUserAgentRegEx.Match(userAgent);
			if (!match.Success)
			{
				return false;
			}
			if (!match.Groups["buildNumber"].Success)
			{
				return int.TryParse(match.Groups["majorVersion2"].Value, out majorVersion);
			}
			if (!int.TryParse(match.Groups["majorVersion"].Value, out majorVersion))
			{
				return false;
			}
			int value;
			if (!int.TryParse(match.Groups["minorVersion"].Value, out value))
			{
				majorVersion = 0;
				return false;
			}
			minorVersion = new int?(value);
			if (!int.TryParse(match.Groups["buildNumber"].Value, out value))
			{
				majorVersion = 0;
				minorVersion = null;
				return false;
			}
			buildNumber = new int?(value);
			return true;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008031 File Offset: 0x00006231
		internal static bool IsProxyingNeededForClient(HttpRequest request)
		{
			return AutodiscoverProxy.IsProxyingNeededForClient(request.IsAuthenticated, request.Path, Common.SafeGetUserAgent(request), request.Headers.Get("Authorization"));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000805C File Offset: 0x0000625C
		internal static bool IsProxyingNeededForClient(bool isAuthenticated, string path, string userAgent, string authorization)
		{
			int length;
			if (authorization != null && (length = authorization.IndexOf(' ')) >= 0)
			{
				authorization = authorization.Substring(0, length);
			}
			bool enabled = VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.RedirectOutlookClient.Enabled;
			ExTraceGlobals.AuthenticationTracer.TraceDebug(-1L, "AutodiscoverProxy::IsProxyNeededForClient. path = {0}, userAgent = \"{1}\", authorization = \"{2}\", isRedirectOutlookClientEnabled = {3}", new object[]
			{
				path,
				userAgent,
				authorization,
				enabled
			});
			return enabled && !string.IsNullOrEmpty(path) && string.Equals(path, "/autodiscover/autodiscover.xml", StringComparison.OrdinalIgnoreCase) && !(authorization != "Basic") && AutodiscoverProxy.CanRedirectOutlookClient(userAgent);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000080F8 File Offset: 0x000062F8
		internal static bool CanRedirectOutlookClient(string userAgent)
		{
			int num;
			int? num2;
			int? num3;
			if (AutodiscoverProxy.TryParseOutlookUserAgent(userAgent, out num, out num2, out num3))
			{
				if (num < 14)
				{
					return true;
				}
				if (num == 14 && num2 != null && num2.Value == 0 && num3 != null && num3.Value < 4327)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000814C File Offset: 0x0000634C
		internal static bool IsRedirectFaultInjectionEnabledOnRequest(bool canFollowRedirect)
		{
			string text = FaultInjection.TraceTest<string>((FaultInjection.LIDs)2535861565U);
			return text != null && !canFollowRedirect && VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.RedirectOutlookClient.Enabled;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008184 File Offset: 0x00006384
		internal string ProxyingAutodiscoverRequestIfApplicable(ProxyRequestData proxyRequestData, string redirectServer)
		{
			if (proxyRequestData == null)
			{
				return null;
			}
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "AutodiscoverProxy::ProxyingAutodiscoverRequestIfApplicable. Entry. redirectServer = {0}", redirectServer);
			string result = null;
			if (!AutodiscoverProxy.TryProxyAutodiscoverRequest(proxyRequestData, redirectServer, out result))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<ProxyRequestData, string>(0L, "AutodiscoverProxy::ProxyingAutodiscoverRequestIfApplicable. Cannot proxy this request. proxyRequestData = {0}, redirectServer = {1}", proxyRequestData, redirectServer);
			}
			else
			{
				proxyRequestData.Response.AddHeader("X-Autodiscover-Proxy", redirectServer);
			}
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "AutodiscoverProxy::ProxyingAutodiscoverRequestIfApplicable. Exit. redirectServer = {0}", redirectServer);
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000081F0 File Offset: 0x000063F0
		private static string GetResponse(Stream responseStream)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(responseStream))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008228 File Offset: 0x00006428
		private static bool TryProxyAutodiscoverRequest(ProxyRequestData proxyRequestData, string redirectServer, out string rawResponse)
		{
			ExTraceGlobals.AuthenticationTracer.TraceDebug<string>(0L, "AutodiscoverProxy::TryProxyAutodiscoverRequest. Entrypoint. redirectServer = {0}.", redirectServer);
			rawResponse = null;
			if (string.IsNullOrEmpty(redirectServer))
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "AutodiscoverProxy::TryProxyAutodiscoverRequest. This is an invalid server name. redirectServer = {0}.", redirectServer);
				return false;
			}
			try
			{
				HttpWebRequest httpWebRequest = proxyRequestData.CloneRequest(redirectServer);
				CertificateValidationManager.SetComponentId(httpWebRequest, "AutodiscoverProxy");
				using (WebResponse response = httpWebRequest.GetResponse())
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						rawResponse = AutodiscoverProxy.GetResponse(responseStream);
						if (rawResponse == null)
						{
							ExTraceGlobals.AuthenticationTracer.TraceError<string>(0L, "AutodiscoverProxy::TryProxyAutodiscoverRequest. received wrong number of user response. rawResponse = {0}.", rawResponse);
							return false;
						}
						ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>(0L, "AutodiscoverProxy::TryProxyAutodiscoverRequest. Received response for user. redirectServer = {0}, rawResponse = {1}.", redirectServer, rawResponse);
					}
				}
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<LocalizedException>(0L, "AutodiscoverProxy::TryProxyAutodiscoverRequest caught with a LocalizedException. LocalizedException = {0}.", arg);
				return false;
			}
			catch (WebException arg2)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<WebException>(0L, "AutodiscoverProxy::TryProxyAutodiscoverRequest caught with a WebException. WebException = {0}.", arg2);
				return false;
			}
			return true;
		}

		// Token: 0x0400017C RID: 380
		private const string ComponentId = "AutodiscoverProxy";

		// Token: 0x0400017D RID: 381
		private static readonly Regex outlookUserAgentRegEx = new Regex("Microsoft\\sOffice/(?<majorVersion2>\\d+)\\.\\d+\\s\\([\\w|\\d|\\s|.|;]+\\sOutlook\\s(?<majorVersion>\\d+)\\.(?<minorVersion>\\d+)(\\.(?<buildNumber>\\d+)){0,1};", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
