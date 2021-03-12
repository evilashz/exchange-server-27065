using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000C RID: 12
	public class DiagnosticsModule : IHttpModule
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002767 File Offset: 0x00000967
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000276F File Offset: 0x0000096F
		internal RequestLogger Logger { get; private set; }

		// Token: 0x06000038 RID: 56 RVA: 0x00002778 File Offset: 0x00000978
		public void Init(HttpApplication application)
		{
			application.BeginRequest += this.BeginRequest;
			application.EndRequest += this.EndRequest;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000279E File Offset: 0x0000099E
		public void Dispose()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027DC File Offset: 0x000009DC
		internal void BeginRequest(object sender, EventArgs e)
		{
			this.ExecuteHandlerIfModuleIsEnabled(delegate
			{
				HttpApplication httpApplication = (HttpApplication)sender;
				HttpContextBase context = new HttpContextWrapper(httpApplication.Context);
				this.InitializeDiagnostics(context);
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000289C File Offset: 0x00000A9C
		internal void EndRequest(object sender, EventArgs e)
		{
			this.ExecuteHandlerIfModuleIsEnabled(delegate
			{
				this.Logger.LatencyTracker.LogElapsedTimeInDetailedLatencyInfo("Diagnosticmodule_EndRequest_Enter");
				HttpApplication httpApplication = (HttpApplication)sender;
				HttpContextBase context = new HttpContextWrapper(httpApplication.Context);
				this.activityScope.End();
				this.AddDiagnosticInfo(context);
				this.FlushDiagnosticInfo(context);
				this.Logger.LatencyTracker.LogElapsedTimeInDetailedLatencyInfo("Diagnosticmodule_EndRequest_End");
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028D0 File Offset: 0x00000AD0
		internal void InitializeDiagnostics(HttpContextBase context)
		{
			context.InitializeLogging();
			this.Logger = RequestLogger.GetLogger(context);
			this.Logger.LatencyTracker.LogElapsedTimeInDetailedLatencyInfo("Diagnosticmodule_InitializeDiagnostics_Start");
			this.activityScope = (context.Items[typeof(ActivityScope)] as IActivityScope);
			if (this.activityScope == null)
			{
				this.activityScope = ActivityContext.GetCurrentActivityScope();
				if (this.activityScope == null)
				{
					this.activityScope = ActivityContext.Start(null);
				}
				context.Items[typeof(ActivityScope)] = this.activityScope;
			}
			this.AddDiagnosticHeaders(context.Request);
			this.Logger.LatencyTracker.LogElapsedTimeInDetailedLatencyInfo("Diagnosticmodule_InitializeDiagnostics_End");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002987 File Offset: 0x00000B87
		internal void FlushDiagnosticInfo(HttpContextBase context)
		{
			this.Logger.Flush();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002994 File Offset: 0x00000B94
		private void ExecuteHandlerIfModuleIsEnabled(ExWatson.MethodDelegate methodDelegate)
		{
			if (HttpProxySettings.DiagnosticsEnabled.Value)
			{
				Diagnostics.SendWatsonReportOnUnhandledException(methodDelegate, new Diagnostics.LastChanceExceptionHandler(this.LastChanceExceptionHandler));
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029B4 File Offset: 0x00000BB4
		private void AddDiagnosticInfo(HttpContextBase context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.Logger.LogField(LogKey.DateTime, DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ"));
			this.Logger.LogField(LogKey.RequestId, this.activityScope.ActivityId);
			this.Logger.LogField(LogKey.MajorVersion, Constants.VersionMajor);
			this.Logger.LogField(LogKey.MinorVersion, Constants.VersionMinor);
			this.Logger.LogField(LogKey.BuildVersion, Constants.VersionBuild);
			this.Logger.LogField(LogKey.RevisionVersion, Constants.VersionRevision);
			this.Logger.LogField(LogKey.Protocol, HttpProxyGlobals.ProtocolType);
			IIdentity identity = (context.User != null) ? context.User.Identity : null;
			if (identity != null)
			{
				this.Logger.LogField(LogKey.AuthenticationType, identity.AuthenticationType);
				this.Logger.LogField(LogKey.IsAuthenticated, identity.IsAuthenticated);
				this.Logger.LogField(LogKey.AuthenticatedUser, identity.Name);
			}
			this.Logger.LogField(LogKey.UserAgent, context.Request.UserAgent);
			this.Logger.LogField(LogKey.ClientIpAddress, context.Request.UserHostAddress);
			this.Logger.LogField(LogKey.ClientRequestId, context.Request.Headers[WellKnownHeader.XRequestId]);
			this.Logger.LogField(LogKey.UrlHost, context.Request.Url.DnsSafeHost);
			this.Logger.LogField(LogKey.UrlStem, context.Request.Url.LocalPath);
			this.Logger.LogField(LogKey.UrlQuery, context.Request.Url.Query);
			this.Logger.LogField(LogKey.Method, context.Request.HttpMethod);
			this.Logger.LogField(LogKey.ProtocolAction, context.Request.QueryString["Action"]);
			this.Logger.LogField(LogKey.ServerHostName, Constants.MachineName);
			this.Logger.LogField(LogKey.HttpStatus, context.Response.StatusCode);
			this.Logger.LogField(LogKey.BackEndGenericInfo, context.Response.Headers[WellKnownHeader.XBackendHeaderPrefix]);
			string value = context.Items["AnonymousRequestFilterModule"] as string;
			if (!string.IsNullOrEmpty(value))
			{
				this.Logger.AppendGenericInfo("AnonymousRequestFilterModule", value);
			}
			NativeProxyLogHelper.PublishNativeProxyStatistics(context);
			this.Logger.LatencyTracker.LogElapsedTimeInMilliseconds(LogKey.TotalRequestTime);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C34 File Offset: 0x00000E34
		private void AddDiagnosticHeaders(HttpRequestBase request)
		{
			this.activityScope.SerializeMinimalTo(request);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C42 File Offset: 0x00000E42
		private void LastChanceExceptionHandler(Exception ex)
		{
			if (this.Logger != null)
			{
				this.Logger.LastChanceExceptionHandler(ex);
			}
		}

		// Token: 0x04000061 RID: 97
		private IActivityScope activityScope;
	}
}
