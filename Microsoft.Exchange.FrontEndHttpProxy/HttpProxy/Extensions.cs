using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000087 RID: 135
	internal static class Extensions
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00018C2C File Offset: 0x00016E2C
		public static int GetTraceContext(this HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			object obj = httpContext.Items[Constants.TraceContextKey];
			if (obj != null)
			{
				return (int)obj;
			}
			return httpContext.GetHashCode();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00018C68 File Offset: 0x00016E68
		public static RequestDetailsLogger GetLogger(this HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException();
			}
			return RequestDetailsLoggerBase<RequestDetailsLogger>.GetCurrent(httpContext);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00018C7C File Offset: 0x00016E7C
		public static string GetSerializedAccessTokenString(this HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			string result = null;
			try
			{
				IIdentity callerIdentity = httpContext.GetCallerIdentity();
				using (ClientSecurityContext clientSecurityContext = callerIdentity.CreateClientSecurityContext(true))
				{
					SerializedAccessToken serializedAccessToken = new SerializedAccessToken(callerIdentity.GetSafeName(true), callerIdentity.AuthenticationType, clientSecurityContext);
					result = serializedAccessToken.ToString();
				}
			}
			catch (AuthzException ex)
			{
				throw new HttpException(401, ex.Message);
			}
			return result;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00018D04 File Offset: 0x00016F04
		public static SerializedClientSecurityContext GetSerializedClientSecurityContext(this HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			SerializedClientSecurityContext result = null;
			try
			{
				IIdentity callerIdentity = httpContext.GetCallerIdentity();
				using (ClientSecurityContext clientSecurityContext = callerIdentity.CreateClientSecurityContext(true))
				{
					result = SerializedClientSecurityContext.CreateFromClientSecurityContext(clientSecurityContext, callerIdentity.GetSafeName(true), callerIdentity.AuthenticationType);
				}
			}
			catch (AuthzException ex)
			{
				throw new HttpException(401, ex.Message);
			}
			return result;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00018D84 File Offset: 0x00016F84
		public static byte[] CreateSerializedSecurityAccessToken(this HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			SerializedSecurityAccessToken serializedSecurityAccessToken = new SerializedSecurityAccessToken();
			try
			{
				IIdentity callerIdentity = httpContext.GetCallerIdentity();
				using (ClientSecurityContext clientSecurityContext = callerIdentity.CreateClientSecurityContext(true))
				{
					clientSecurityContext.SetSecurityAccessToken(serializedSecurityAccessToken);
				}
			}
			catch (AuthzException ex)
			{
				throw new HttpException(401, ex.Message);
			}
			return serializedSecurityAccessToken.GetSecurityContextBytes();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00018E00 File Offset: 0x00017000
		public static IIdentity GetCallerIdentity(this HttpContext httpContext)
		{
			IIdentity identity = httpContext.User.Identity;
			if (identity.GetType().Equals(typeof(GenericIdentity)) && string.Equals(identity.AuthenticationType, "LiveIdBasic", StringComparison.OrdinalIgnoreCase))
			{
				identity = LiveIdBasicHelper.GetCallerIdentity(httpContext);
			}
			return identity;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00018E4C File Offset: 0x0001704C
		public static HttpMethod GetHttpMethod(this HttpRequest request)
		{
			HttpMethod result = HttpMethod.Unknown;
			if (!Enum.TryParse<HttpMethod>(request.HttpMethod, true, out result))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "Extensions.GetHttpMethod. HttpMethod unrecognised or has no enum value: {0}", request.HttpMethod);
			}
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00018E84 File Offset: 0x00017084
		public static bool IsDownLevelClient(this HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "Extensions.IsDownLevelClient. user-agent = {0}", (request.UserAgent == null) ? string.Empty : request.UserAgent);
			string a;
			UserAgentParser.UserAgentVersion userAgentVersion;
			string a2;
			UserAgentParser.Parse(request.UserAgent, out a, out userAgentVersion, out a2);
			return (!string.Equals(a, "rv:", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 11 || !string.Equals(a2, "Windows NT", StringComparison.OrdinalIgnoreCase)) && (!string.Equals(a, "MSIE", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 7 || (!string.Equals(a2, "Windows NT", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows 98; Win 9x 4.90", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows 2000", StringComparison.OrdinalIgnoreCase))) && (!string.Equals(a, "Safari", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 3 || !string.Equals(a2, "Macintosh", StringComparison.OrdinalIgnoreCase)) && (!string.Equals(a, "Firefox", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 3 || (!string.Equals(a2, "Windows NT", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows 98; Win 9x 4.90", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows 2000", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Macintosh", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Linux", StringComparison.OrdinalIgnoreCase))) && (!string.Equals(a, "Chrome", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 1 || (!string.Equals(a2, "Windows NT", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Macintosh", StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001900C File Offset: 0x0001720C
		public static string GetFullRawUrl(this HttpRequest httpRequest)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			string text = httpRequest.Url.IsDefaultPort ? string.Empty : (":" + httpRequest.Url.Port.ToString());
			return string.Concat(new string[]
			{
				httpRequest.Url.Scheme,
				"://",
				httpRequest.Url.Host,
				text,
				httpRequest.RawUrl
			});
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00019097 File Offset: 0x00017297
		public static bool IsAnyWsSecurityRequest(this HttpRequest request)
		{
			return ProtocolHelper.IsAnyWsSecurityRequest(request.Url.LocalPath);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000190A9 File Offset: 0x000172A9
		public static bool IsWsSecurityRequest(this HttpRequest request)
		{
			return ProtocolHelper.IsWsSecurityRequest(request.Url.LocalPath);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000190BB File Offset: 0x000172BB
		public static bool IsPartnerAuthRequest(this HttpRequest request)
		{
			return ProtocolHelper.IsPartnerAuthRequest(request.Url.LocalPath);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000190CD File Offset: 0x000172CD
		public static bool IsX509CertAuthRequest(this HttpRequest request)
		{
			return ProtocolHelper.IsX509CertAuthRequest(request.Url.LocalPath);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000190DF File Offset: 0x000172DF
		public static bool IsChangePasswordLogoff(this HttpRequest request)
		{
			return request.QueryString["ChgPwd"] == "1";
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000190FC File Offset: 0x000172FC
		public static bool CanHaveBody(this HttpRequest request)
		{
			HttpMethod httpMethod = request.GetHttpMethod();
			return httpMethod != HttpMethod.Get && httpMethod != HttpMethod.Head;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00019120 File Offset: 0x00017320
		public static bool IsRequestChunked(this HttpRequest request)
		{
			string text = request.Headers["Transfer-Encoding"];
			return text != null && text.IndexOf("chunked", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00019158 File Offset: 0x00017358
		public static bool IsChunkedResponse(this HttpWebResponse response)
		{
			string text = response.Headers["Transfer-Encoding"];
			return text != null && text.IndexOf("chunked", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001918D File Offset: 0x0001738D
		public static bool HasBody(this HttpRequest request)
		{
			return request.CanHaveBody() && (request.IsRequestChunked() || request.ContentLength > 0);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000191AC File Offset: 0x000173AC
		public static string GetBaseUrl(this HttpRequest httpRequest)
		{
			return new UriBuilder
			{
				Host = httpRequest.Url.Host,
				Port = httpRequest.Url.Port,
				Scheme = httpRequest.Url.Scheme,
				Path = httpRequest.ApplicationPath
			}.Uri.ToString();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00019209 File Offset: 0x00017409
		public static string GetTestBackEndUrl(this HttpRequest clientRequest)
		{
			return clientRequest.Headers[Constants.TestBackEndUrlRequestHeaderKey];
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001921B File Offset: 0x0001741B
		public static void LogSharedCacheCall(this IRequestContext requestContext, long latency, string diagnostics)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			requestContext.LatencyTracker.HandleSharedCacheLatency(latency);
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(requestContext.Logger, "SharedCache", diagnostics);
			PerfCounters.UpdateMovingAveragePerformanceCounter(PerfCounters.HttpProxyCountersInstance.MovingAverageSharedCacheLatency, latency);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00019258 File Offset: 0x00017458
		public static string GetFriendlyName(this OrganizationId organizationId)
		{
			if (organizationId != null && organizationId.OrganizationalUnit != null)
			{
				return organizationId.OrganizationalUnit.Name;
			}
			return null;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00019278 File Offset: 0x00017478
		public static bool TryGetSite(this ServiceTopology serviceTopology, string fqdn, out Site site)
		{
			if (serviceTopology == null)
			{
				throw new ArgumentNullException("serviceTopology");
			}
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			site = null;
			try
			{
				site = serviceTopology.GetSite(fqdn, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\Misc\\Extensions.cs", "TryGetSite", 495);
			}
			catch (ServerNotFoundException)
			{
				return false;
			}
			catch (ServerNotInSiteException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000192EC File Offset: 0x000174EC
		internal static void SetActivityScopeOnCurrentThread(this HttpContext httpContext, RequestDetailsLogger logger)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException();
			}
			if (logger != null)
			{
				ActivityContext.SetThreadScope(logger.ActivityScope);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00019308 File Offset: 0x00017508
		internal static void Shuffle<T>(this T[] array, Random random)
		{
			for (int i = array.Length - 1; i > 0; i--)
			{
				int num = random.Next(i + 1);
				T t = array[i];
				array[i] = array[num];
				array[num] = t;
			}
		}
	}
}
