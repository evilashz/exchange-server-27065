using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001A4 RID: 420
	internal static class EcpEventLogExtensions
	{
		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x0006C6DB File Offset: 0x0006A8DB
		public static ExEventLog EventLog
		{
			get
			{
				return EcpEventLogExtensions.eventLog;
			}
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0006C6E2 File Offset: 0x0006A8E2
		public static bool IsEnabled(this ExEventLog.EventTuple tuple)
		{
			return EcpEventLogExtensions.eventLog.IsEventCategoryEnabled(tuple.CategoryId, tuple.Level);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0006C6FC File Offset: 0x0006A8FC
		public static void LogEvent(this ExEventLog.EventTuple tuple, params object[] messageArgs)
		{
			EcpEventLogExtensions.InternalLogEvent(tuple, null, messageArgs);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0006C706 File Offset: 0x0006A906
		public static void LogPeriodicEvent(this ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			EcpEventLogExtensions.InternalLogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0006C710 File Offset: 0x0006A910
		public static void LogPeriodicFailure(this ExEventLog.EventTuple tuple, string userName, string requestUrl, Exception exception, string featureInfo)
		{
			if (tuple.IsEnabled())
			{
				using (new ThreadCultureSwitch())
				{
					HttpContext httpContext = HttpContext.Current;
					if (string.IsNullOrEmpty(userName))
					{
						userName = ((httpContext != null && httpContext.User != null && httpContext.User.Identity != null) ? httpContext.User.Identity.GetSafeName(true) : string.Empty);
					}
					string periodicKey = requestUrl + LocalizedException.GenerateErrorCode(exception) + userName;
					tuple.LogPeriodicEvent(periodicKey, new object[]
					{
						userName,
						requestUrl,
						exception.GetTraceFormatter().ToString(),
						featureInfo
					});
				}
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0006C7C8 File Offset: 0x0006A9C8
		internal static string GetUserNameToLog()
		{
			string result = Strings.UserUnauthenticated;
			RbacPrincipal current = RbacPrincipal.GetCurrent(false);
			if (current != null)
			{
				result = current.NameForEventLog;
			}
			else if (HttpContext.Current.Request.IsAuthenticated)
			{
				result = HttpContext.Current.User.Identity.GetSafeName(true);
			}
			else if (HttpContext.Current.User == null)
			{
				result = Strings.UserNotSet;
			}
			return result;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0006C834 File Offset: 0x0006AA34
		internal static string GetFlightInfoForLog()
		{
			string result;
			try
			{
				bool flag;
				VariantConfigurationSnapshot snapshotForCurrentUser = EacFlightUtility.GetSnapshotForCurrentUser(out flag);
				Dictionary<string, bool> allEacRelatedFeatures = EacFlightUtility.GetAllEacRelatedFeatures(snapshotForCurrentUser);
				string[] flights = snapshotForCurrentUser.Flights;
				KeyValuePair<string, string>[] constraints = snapshotForCurrentUser.Constraints;
				result = string.Format("Features:{0},  Flights:[{1}],  Constraints:{2}, IsGlobalSnapshot: {3}", new object[]
				{
					allEacRelatedFeatures.ToLogString<string, bool>(),
					(flights == null) ? "" : string.Join(",", flights),
					constraints.ToLogString<string, string>(),
					flag.ToString()
				});
			}
			catch (Exception)
			{
				result = "Feature status not available, Flight APIs is in Invalid Status";
			}
			return result;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0006C8CC File Offset: 0x0006AACC
		internal static string GetPeriodicKeyPerUser()
		{
			RbacPrincipal current = RbacPrincipal.GetCurrent(false);
			if (current != null)
			{
				return current.NameForEventLog;
			}
			if (HttpContext.Current.Request.IsAuthenticated)
			{
				return HttpContext.Current.User.Identity.GetSafeName(true);
			}
			return null;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0006C914 File Offset: 0x0006AB14
		private static void InternalLogEvent(ExEventLog.EventTuple tuple, string periodicKey, object[] messageArgs)
		{
			int num = messageArgs.Length;
			object[] array = new object[num + 1];
			Array.Copy(messageArgs, array, num);
			array[num] = "ActivityId: " + ActivityContext.ActivityId.FormatForLog();
			EcpEventLogExtensions.eventLog.LogEvent(tuple, periodicKey, array);
		}

		// Token: 0x04001DED RID: 7661
		private const string EventSource = "MSExchange Control Panel";

		// Token: 0x04001DEE RID: 7662
		private static ExEventLog eventLog = new ExEventLog(ExTraceGlobals.EventLogTracer.Category, "MSExchange Control Panel");
	}
}
