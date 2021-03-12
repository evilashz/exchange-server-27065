using System;
using System.Data.Services;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.ReportingWebService;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000031 RID: 49
	internal class ReportingModule : IHttpModule
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000059A4 File Offset: 0x00003BA4
		public void Init(HttpApplication context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "ReportingModule.Init");
			context.BeginRequest += this.OnBeginRequest;
			context.EndRequest += this.OnEndRequest;
			if (ReportingModule.NewAuthZMethodEnabled.Value && !Datacenter.IsForefrontForOfficeDatacenter())
			{
				context.AuthorizeRequest += this.OnAuthorizeRequest;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005A1E File Offset: 0x00003C1E
		void IHttpModule.Dispose()
		{
			this.averageRequestTime.Dispose();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005A2C File Offset: 0x00003C2C
		private void OnBeginRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "ReportingModule.OnBeginRequest");
			this.requestStartTime = DateTime.UtcNow;
			ReportingModule.activeRequestsCounter.Increment();
			this.averageRequestTime.Start();
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			if (!ActivityContext.IsStarted)
			{
				this.activityScope = ActivityContext.DeserializeFrom(context.Request, null);
			}
			RequestStatistics.CreateRequestRequestStatistics(context);
			this.SetCurrentCulture(context);
			this.AddTrailingSlashToServiceFile(context);
			ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "ReportingModule.OnBeginRequest - End");
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005AC4 File Offset: 0x00003CC4
		private void OnEndRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "ReportingModule.OnEndRequest");
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			ReportingVersion.WriteVersionInfoInResponse(context);
			ReportingModule.activeRequestsCounter.Decrement();
			this.averageRequestTime.Stop();
			RequestStatistics requestStatistics = HttpContext.Current.Items[RequestStatistics.RequestStatsKey] as RequestStatistics;
			if (requestStatistics != null)
			{
				requestStatistics.AddStatisticsDataPoint(RequestStatistics.RequestStatItem.RequestResponseTime, this.requestStartTime, DateTime.UtcNow);
				requestStatistics.AddExtendedStatisticsDataPoint("HTTPCODE", context.Response.StatusCode.ToString());
				IPrincipal user = context.User;
				string text = context.Request.Headers["X-SourceCafeServer"];
				ServerLogEvent logEvent = new ServerLogEvent((ActivityContext.ActivityId != null) ? ActivityContext.ActivityId.Value.ToString() : string.Empty, string.IsNullOrEmpty(text) ? string.Empty : text, requestStatistics);
				ServerLogger.Instance.LogEvent(logEvent);
			}
			if (this.activityScope != null && !this.activityScope.IsDisposed)
			{
				this.activityScope.End();
			}
			ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "ReportingModule.OnEndRequest - End");
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005C0C File Offset: 0x00003E0C
		private void OnAuthorizeRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "ReportingModule.OnAuthorizeRequest");
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			RequestStatistics requestStatistics = context.Items[RequestStatistics.RequestStatsKey] as RequestStatistics;
			if (context.Request.IsAuthenticated)
			{
				requestStatistics.AddExtendedStatisticsDataPoint("AuthN", "True");
				try
				{
					RbacPrincipal rbacPrincipal = RbacPrincipalManager.Instance.AcquireRbacPrincipalWrapper(context);
					if (rbacPrincipal != null)
					{
						ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "[OnAuthorizeRequest] RbacPrincipal != null");
						context.User = rbacPrincipal;
						rbacPrincipal.SetCurrentThreadPrincipal();
						requestStatistics.AddExtendedStatisticsDataPoint("AuthZ", "True");
					}
					else
					{
						ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "[OnAuthorizeRequest] RbacPrincipal == null");
						context.Response.StatusCode = 401;
						httpApplication.CompleteRequest();
						requestStatistics.AddExtendedStatisticsDataPoint("AuthZ", "False");
					}
					goto IL_138;
				}
				catch (DataServiceException value)
				{
					ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "[OnAuthorizeRequest] DataServiceException got");
					context.Items.Add(RbacAuthorizationManager.DataServiceExceptionKey, value);
					requestStatistics.AddExtendedStatisticsDataPoint("AuthZ", "False");
					goto IL_138;
				}
			}
			requestStatistics.AddExtendedStatisticsDataPoint("AuthN", "False");
			requestStatistics.AddExtendedStatisticsDataPoint("AuthZ", "False");
			IL_138:
			ExTraceGlobals.ReportingWebServiceTracer.TraceDebug((long)this.GetHashCode(), "ReportingModule.OnAuthorizeRequest - End");
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005D78 File Offset: 0x00003F78
		private void AddTrailingSlashToServiceFile(HttpContext httpContext)
		{
			HttpRequest request = httpContext.Request;
			if (request.FilePath.EndsWith(".svc", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(request.PathInfo))
			{
				string path = request.RawUrl.Insert(request.FilePath.Length, "/");
				httpContext.RewritePath(path);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005DD0 File Offset: 0x00003FD0
		private void SetCurrentCulture(HttpContext httpContext)
		{
			CultureInfo defaultCulture = Culture.GetDefaultCulture(httpContext);
			Thread.CurrentThread.CurrentCulture = defaultCulture;
			Thread.CurrentThread.CurrentUICulture = defaultCulture;
		}

		// Token: 0x0400007E RID: 126
		private static readonly BoolAppSettingsEntry NewAuthZMethodEnabled = new BoolAppSettingsEntry("NewAuthZMethodEnabled", false, ExTraceGlobals.ReportingWebServiceTracer);

		// Token: 0x0400007F RID: 127
		private static readonly PerfCounterGroup activeRequestsCounter = new PerfCounterGroup(RwsPerfCounters.ActiveRequests, RwsPerfCounters.ActiveRequestsPeak, RwsPerfCounters.ActiveRequestsTotal);

		// Token: 0x04000080 RID: 128
		private readonly AverageTimePerfCounter averageRequestTime = new AverageTimePerfCounter(RwsPerfCounters.AverageRequestResponseTime, RwsPerfCounters.AverageRequestResponseTimeBase, true);

		// Token: 0x04000081 RID: 129
		private DateTime requestStartTime;

		// Token: 0x04000082 RID: 130
		private ActivityScope activityScope;
	}
}
