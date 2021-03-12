using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001AB RID: 427
	internal class ActivityContextManager
	{
		// Token: 0x0600239B RID: 9115 RVA: 0x0006D088 File Offset: 0x0006B288
		public static void InitializeActivityContext(HttpContext httpContext, ActivityContextLoggerId eventId = ActivityContextLoggerId.Request)
		{
			try
			{
				if (httpContext != null && !ActivityContext.IsStarted)
				{
					ActivityScope activityScope = ActivityContext.DeserializeFrom(httpContext.Request, null);
					activityScope.SetProperty(ExtensibleLoggerMetadata.EventId, eventId.ToString());
					if (activityScope.DisposeTracker is DisposeTrackerObject<ActivityScope>)
					{
						activityScope.DisposeTracker.AddExtraData(httpContext.GetRequestUrl().ToString());
					}
					httpContext.Items[ActivityContextManager.ECPActivityScopePropertyName] = activityScope;
				}
			}
			catch (Exception exception)
			{
				EcpEventLogConstants.Tuple_ActivityContextError.LogPeriodicFailure(EcpEventLogExtensions.GetUserNameToLog(), httpContext.GetRequestUrlForLog(), exception, EcpEventLogExtensions.GetFlightInfoForLog());
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0006D128 File Offset: 0x0006B328
		public static void CleanupActivityContext(HttpContext httpContext)
		{
			try
			{
				if (httpContext != null)
				{
					ActivityScope activityScope = (ActivityScope)httpContext.Items[ActivityContextManager.ECPActivityScopePropertyName];
					if (activityScope != null)
					{
						httpContext.Items.Remove(ActivityContextManager.ECPActivityScopePropertyName);
						if (!activityScope.IsDisposed)
						{
							activityScope.End();
						}
					}
				}
			}
			catch (Exception exception)
			{
				EcpEventLogConstants.Tuple_ActivityContextError.LogPeriodicFailure(EcpEventLogExtensions.GetUserNameToLog(), httpContext.GetRequestUrlForLog(), exception, EcpEventLogExtensions.GetFlightInfoForLog());
			}
		}

		// Token: 0x04001E0A RID: 7690
		private static string ECPActivityScopePropertyName = "ECPActivityScope";
	}
}
