using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ReportingWebService;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000020 RID: 32
	internal static class EventLogExtension
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00003831 File Offset: 0x00001A31
		public static bool IsEnabled(this ExEventLog.EventTuple tuple)
		{
			return EventLogExtension.EventLog.IsEventCategoryEnabled(tuple.CategoryId, tuple.Level);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000384B File Offset: 0x00001A4B
		public static void LogEvent(this ExEventLog.EventTuple tuple, params object[] messageArgs)
		{
			EventLogExtension.EventLog.LogEvent(tuple, null, messageArgs);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000385B File Offset: 0x00001A5B
		public static void LogPeriodicEvent(this ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			EventLogExtension.EventLog.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000386C File Offset: 0x00001A6C
		internal static string GetPeriodicKeyPerUser()
		{
			RbacPrincipal current = RbacPrincipal.GetCurrent(false);
			if (current != null)
			{
				return current.NameForEventLog;
			}
			if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.IsAuthenticated)
			{
				return HttpContext.Current.User.Identity.GetSafeName(true);
			}
			return null;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000038C8 File Offset: 0x00001AC8
		internal static string GetUserNameToLog()
		{
			string result = Strings.UserUnauthenticated;
			RbacPrincipal current = RbacPrincipal.GetCurrent(false);
			if (current != null)
			{
				result = current.NameForEventLog;
			}
			else if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.IsAuthenticated)
			{
				result = HttpContext.Current.User.Identity.GetSafeName(true);
			}
			else if (HttpContext.Current.User == null)
			{
				result = Strings.UserNotSet;
			}
			return result;
		}

		// Token: 0x0400004A RID: 74
		private const string EventSource = "MSExchange ReportingWebService";

		// Token: 0x0400004B RID: 75
		public static readonly ExEventLog EventLog = new ExEventLog(ExTraceGlobals.ReportingWebServiceTracer.Category, "MSExchange ReportingWebService");
	}
}
