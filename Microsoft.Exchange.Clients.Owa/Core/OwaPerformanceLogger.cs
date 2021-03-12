using System;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001EF RID: 495
	internal static class OwaPerformanceLogger
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x00064094 File Offset: 0x00062294
		internal static void TracePerformance(UserContext userContext)
		{
			OwaContext owaContext = OwaContext.Current;
			if (ETWTrace.ShouldTraceCasStop(owaContext.TraceRequestId))
			{
				string userContext2 = string.Empty;
				HttpRequest request = owaContext.HttpContext.Request;
				int totalBytes = request.TotalBytes;
				string pathAndQuery = request.Url.PathAndQuery;
				ExchangePrincipal exchangePrincipal = userContext.ExchangePrincipal;
				if (exchangePrincipal != null)
				{
					userContext2 = exchangePrincipal.MailboxInfo.DisplayName;
				}
				Trace.TraceCasStop(CasTraceEventType.Owa, owaContext.TraceRequestId, totalBytes, 0, owaContext.LocalHostName, userContext2, "OwaModule", pathAndQuery, string.Empty);
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00064114 File Offset: 0x00062314
		internal static void LogPerformanceStatistics(UserContext userContext)
		{
			OwaContext owaContext = OwaContext.Current;
			if (userContext.IsClientSideDataCollectingEnabled)
			{
				string str = "&v=";
				owaContext.HttpContext.Response.AppendToLog(str + Globals.ApplicationVersion);
			}
			StringBuilder stringBuilder = new StringBuilder(OwaPerformanceLogger.EstimatedIISStringBuilderCapacity);
			long requestLatencyMilliseconds = owaContext.RequestLatencyMilliseconds;
			if (userContext.HasValidMailboxSession())
			{
				ExchangePrincipal exchangePrincipal = userContext.ExchangePrincipal;
				if (exchangePrincipal != null)
				{
					string serverFqdn = exchangePrincipal.MailboxInfo.Location.ServerFqdn;
					if (serverFqdn != null)
					{
						stringBuilder.Append("&mbx=").Append(serverFqdn);
					}
				}
			}
			stringBuilder.Append("&sessionId=").Append(userContext.Key.UserContextId);
			stringBuilder.Append("&prfltncy=").Append(requestLatencyMilliseconds);
			uint num = 0U;
			long num2 = 0L;
			uint num3 = 0U;
			long num4 = 0L;
			if (owaContext.RequestExecution == RequestExecution.Local)
			{
				TaskPerformanceData rpcData = owaContext.RpcData;
				if (rpcData != null && rpcData.End != PerformanceData.Zero)
				{
					PerformanceData difference = rpcData.Difference;
					num = difference.Count;
					num2 = (long)difference.Milliseconds;
					PerformanceData ewsRpcData = owaContext.EwsRpcData;
					if (ewsRpcData != PerformanceData.Zero)
					{
						num += ewsRpcData.Count;
						num2 += (long)ewsRpcData.Milliseconds;
					}
					OwaPerformanceLogger.rpcLogger.AppendIISLogsEntry(stringBuilder, num, num2);
				}
				TaskPerformanceData ldapData = owaContext.LdapData;
				if (ldapData != null)
				{
					PerformanceData difference2 = ldapData.Difference;
					num3 = difference2.Count;
					num4 = (long)difference2.Milliseconds;
					PerformanceData ewsLdapData = owaContext.EwsLdapData;
					if (ewsLdapData != PerformanceData.Zero)
					{
						num3 += ewsLdapData.Count;
						num4 += (long)ewsLdapData.Milliseconds;
					}
					OwaPerformanceLogger.ldapLogger.AppendIISLogsEntry(stringBuilder, num3, num4);
				}
				OwaPerformanceLogger.AppendLatencyHeaders(num2, num4, requestLatencyMilliseconds);
				OwaPerformanceLogger.availabilityLogger.AppendIISLogsEntry(stringBuilder, owaContext.AvailabilityQueryCount, owaContext.AvailabilityQueryLatency);
			}
			owaContext.HttpContext.Response.AppendToLog(stringBuilder.ToString());
			if (Globals.CollectPerRequestPerformanceStats)
			{
				StringBuilder stringBuilder2 = new StringBuilder(OwaPerformanceLogger.EstimatedBreadcrumbStringBuilderCapacity);
				stringBuilder2.Append("Total: ").Append(requestLatencyMilliseconds).Append(" ms");
				owaContext.OwaPerformanceData.TotalLatency = requestLatencyMilliseconds;
				owaContext.OwaPerformanceData.KilobytesAllocated = (long)((ulong)owaContext.MemoryData.Difference.Count);
				if (owaContext.HasTrustworthyRequestCpuLatency)
				{
					stringBuilder2.Append(" CPU: ");
					stringBuilder2.Append(owaContext.RequestCpuLatencyMilliseconds).Append(" ms");
				}
				if (owaContext.RequestExecution == RequestExecution.Local)
				{
					owaContext.OwaPerformanceData.RpcCount = num;
					owaContext.OwaPerformanceData.RpcLatency = (int)num2;
					owaContext.OwaPerformanceData.LdapCount = num3;
					owaContext.OwaPerformanceData.LdapLatency = (int)num4;
					OwaPerformanceLogger.rpcLogger.AppendBreadcrumbEntry(stringBuilder2, num, num2);
					OwaPerformanceLogger.ldapLogger.AppendBreadcrumbEntry(stringBuilder2, num3, num4);
					OwaPerformanceLogger.availabilityLogger.AppendBreadcrumbEntry(stringBuilder2, owaContext.AvailabilityQueryCount, owaContext.AvailabilityQueryLatency);
					if (!string.IsNullOrEmpty(owaContext.EwsPerformanceHeader))
					{
						owaContext.OwaPerformanceData.TraceOther(owaContext.EwsPerformanceHeader);
					}
				}
				owaContext.UserContext.PerformanceConsoleNotifier.UpdatePerformanceData(owaContext.OwaPerformanceData, true);
				userContext.LogBreadcrumb(stringBuilder2.ToString());
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00064448 File Offset: 0x00062648
		private static void AppendLatencyHeaders(long rpcLatency, long ldapLatency, long requestLatency)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null || httpContext.Response == null || !UserAgentUtilities.IsMonitoringRequest(httpContext.Request.UserAgent))
			{
				return;
			}
			try
			{
				httpContext.Response.AppendHeader("X-DiagInfoRpcLatency", rpcLatency.ToString());
				httpContext.Response.AppendHeader("X-DiagInfoLdapLatency", ldapLatency.ToString());
				httpContext.Response.AppendHeader("X-DiagInfoIisLatency", requestLatency.ToString());
			}
			catch (HttpException arg)
			{
				ExTraceGlobals.PerformanceTracer.TraceDebug<HttpException>(0L, "Exception happened while trying to append latency headers. Exception will be ignored: {0}", arg);
			}
		}

		// Token: 0x04000AC2 RID: 2754
		private const string MailboxServerName = "&mbx=";

		// Token: 0x04000AC3 RID: 2755
		private const string UserContextName = "&sessionId=";

		// Token: 0x04000AC4 RID: 2756
		private const string IISLatency = "&prfltncy=";

		// Token: 0x04000AC5 RID: 2757
		private const string BreadcrumbLatency = "Total: ";

		// Token: 0x04000AC6 RID: 2758
		private const string BreadcrumbCpuLatency = " CPU: ";

		// Token: 0x04000AC7 RID: 2759
		private const string Msec = " ms";

		// Token: 0x04000AC8 RID: 2760
		private const int LatencyStringLength = 4;

		// Token: 0x04000AC9 RID: 2761
		private const int CountStringLength = 3;

		// Token: 0x04000ACA RID: 2762
		private const string IISRpcCount = "&prfrpccnt=";

		// Token: 0x04000ACB RID: 2763
		private const string IISRpcLatency = "&prfrpcltncy=";

		// Token: 0x04000ACC RID: 2764
		private const string BreadcrumbRPC = " RPC#: ";

		// Token: 0x04000ACD RID: 2765
		private const string IISLdapCount = "&prfldpcnt=";

		// Token: 0x04000ACE RID: 2766
		private const string IISLdapLatency = "&prfldpltncy=";

		// Token: 0x04000ACF RID: 2767
		private const string BreadcrumbLDAP = " LDAP#: ";

		// Token: 0x04000AD0 RID: 2768
		private const string IISAvailabilityCount = "&prfavlcnt=";

		// Token: 0x04000AD1 RID: 2769
		private const string IISAvailabilityLatency = "&prfavlltncy=";

		// Token: 0x04000AD2 RID: 2770
		private const string BreadcrumbAvailability = " Avail#: ";

		// Token: 0x04000AD3 RID: 2771
		internal const string ClientPerfCounters = "cpc";

		// Token: 0x04000AD4 RID: 2772
		internal const string ClientActionList = "calist";

		// Token: 0x04000AD5 RID: 2773
		internal const string PerfMarkers = "pfmk";

		// Token: 0x04000AD6 RID: 2774
		private static readonly int EstimatedIISStringBuilderCapacity = "&prfltncy=".Length + "&prfrpccnt=".Length + "&prfrpcltncy=".Length + "&prfldpcnt=".Length + "&prfldpltncy=".Length + "&prfavlcnt=".Length + "&prfavlltncy=".Length + 16 + 9;

		// Token: 0x04000AD7 RID: 2775
		private static readonly int EstimatedBreadcrumbStringBuilderCapacity = "Total: ".Length + " CPU: ".Length + 2 * " ms".Length + " RPC#: ".Length + " LDAP#: ".Length + " Avail#: ".Length + 3 * (" (".Length + " ms)".Length) + 20 + 9;

		// Token: 0x04000AD8 RID: 2776
		private static readonly PerformanceLogger rpcLogger = new PerformanceLogger("&prfrpccnt=", "&prfrpcltncy=", " RPC#: ");

		// Token: 0x04000AD9 RID: 2777
		private static readonly PerformanceLogger ldapLogger = new PerformanceLogger("&prfldpcnt=", "&prfldpltncy=", " LDAP#: ");

		// Token: 0x04000ADA RID: 2778
		private static readonly PerformanceLogger availabilityLogger = new PerformanceLogger("&prfavlcnt=", "&prfavlltncy=", " Avail#: ");
	}
}
