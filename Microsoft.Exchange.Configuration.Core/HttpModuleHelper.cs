using System;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000020 RID: 32
	public static class HttpModuleHelper
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005C60 File Offset: 0x00003E60
		internal static IScopedPerformanceMonitor[] HttpPerfMonitors
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					return HttpModuleHelper.EmptyMonitors;
				}
				LatencyTracker latencyTracker = httpContext.Items["Logging-HttpRequest-Latency"] as LatencyTracker;
				if (latencyTracker == null)
				{
					return HttpModuleHelper.EmptyMonitors;
				}
				return new IScopedPerformanceMonitor[]
				{
					new LatencyMonitor(latencyTracker)
				};
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005CAC File Offset: 0x00003EAC
		internal static UserToken CurrentUserToken(this HttpContext httpContext)
		{
			return HttpContext.Current.Items["X-EX-UserToken"] as UserToken;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005CC8 File Offset: 0x00003EC8
		internal static void EndPowerShellRequestWithFriendlyError(HttpContext context, FailureCategory failureCategory, string failureName, string errorMessage, string className, bool isCriticalError)
		{
			context.Response.StatusCode = 400;
			string text = string.Format("[FailureCategory={0}] ", failureCategory + "-" + failureName) + errorMessage;
			HttpLogger.SafeAppendGenericError(className, text, isCriticalError);
			ExTraceGlobals.HttpModuleTracer.TraceError<string>(0L, className + " Get error. {0}", text);
			context.Response.Write(text);
			context.Response.End();
		}

		// Token: 0x04000085 RID: 133
		public const string ExtendedStatusContextKeyName = "ExtendedStatus";

		// Token: 0x04000086 RID: 134
		private static readonly IScopedPerformanceMonitor[] EmptyMonitors = new IScopedPerformanceMonitor[0];
	}
}
