using System;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x02000107 RID: 263
	internal static class LoggerHelper
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001E86E File Offset: 0x0001CA6E
		internal static bool IsPswsNormalRequest
		{
			get
			{
				return LoggerSettings.IsPowerShellWebService && HttpContext.Current != null;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0001E884 File Offset: 0x0001CA84
		internal static bool IsPswsCmdletDirectInvoke
		{
			get
			{
				return LoggerSettings.IsPowerShellWebService && HttpContext.Current == null;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001E898 File Offset: 0x0001CA98
		internal static string GetContributeToFailFastValue(string blockedScope, string blockedUserOrTenant, string blockedAction, double blockedTime)
		{
			return string.Format("{0}#{1}#{2}#{3}", new object[]
			{
				blockedScope,
				blockedUserOrTenant,
				blockedAction,
				blockedTime
			});
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001E8CC File Offset: 0x0001CACC
		internal static void UpdateActivityScopeRequestIdFromUrl(string httpUrl)
		{
			if (string.IsNullOrWhiteSpace(httpUrl))
			{
				return;
			}
			Uri uri = new Uri(httpUrl);
			NameValueCollection urlProperties = LoggerHelper.GetUrlProperties(uri);
			string text = urlProperties["RequestId48CD6591-0506-4D6E-9131-797489A3260F"];
			Guid guid;
			if (text == null || !Guid.TryParse(text, out guid))
			{
				return;
			}
			ActivityScopeImpl activityScopeImpl = ActivityContext.GetCurrentActivityScope() as ActivityScopeImpl;
			if (activityScopeImpl == null)
			{
				return;
			}
			if (activityScopeImpl.ActivityId == guid)
			{
				return;
			}
			ActivityContextState state = new ActivityContextState(new Guid?(guid), LoggerHelper.EmptyConcurrentDic);
			activityScopeImpl.UpdateFromState(state);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001E948 File Offset: 0x0001CB48
		internal static NameValueCollection GetUrlProperties(Uri uri)
		{
			if (uri == null)
			{
				return null;
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			return HttpUtility.ParseQueryString(uriBuilder.Query.Replace(';', '&'));
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001E97C File Offset: 0x0001CB7C
		internal static bool IsProbePingRequest(HttpRequest request)
		{
			if (request == null)
			{
				return false;
			}
			Uri url = request.Url;
			NameValueCollection urlProperties = LoggerHelper.GetUrlProperties(url);
			string text = (urlProperties != null) ? urlProperties.Get("ping") : null;
			return !string.IsNullOrEmpty(text) && text.Equals("probe", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040004CD RID: 1229
		internal const string RemotePSRequestIdUrlParameterName = "RequestId48CD6591-0506-4D6E-9131-797489A3260F";

		// Token: 0x040004CE RID: 1230
		private const string ContributeToFailFastFmt = "{0}#{1}#{2}#{3}";

		// Token: 0x040004CF RID: 1231
		internal static IScopedPerformanceMonitor[] CmdletPerfMonitors = new IScopedPerformanceMonitor[]
		{
			CmdletLatencyMonitor.Instance
		};

		// Token: 0x040004D0 RID: 1232
		private static readonly ConcurrentDictionary<Enum, object> EmptyConcurrentDic = new ConcurrentDictionary<Enum, object>();
	}
}
