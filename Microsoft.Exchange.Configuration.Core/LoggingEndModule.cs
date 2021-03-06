using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000021 RID: 33
	public class LoggingEndModule : IHttpModule
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00005D4E File Offset: 0x00003F4E
		void IHttpModule.Init(HttpApplication context)
		{
			context.PostAuthenticateRequest += this.OnPostAuthenticateRequest;
			context.EndRequest += this.OnEndRequest;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005D74 File Offset: 0x00003F74
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005D78 File Offset: 0x00003F78
		private void OnPostAuthenticateRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[LoggingEndModule::OnPostAuthenticateRequest] Enter");
			HttpContext httpContext = HttpContext.Current;
			if (!httpContext.Request.IsAuthenticated)
			{
				ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[LoggingEndModule::OnPostAuthenticateRequest] Exit IsAuthenticated = false.");
				return;
			}
			UserToken userToken = httpContext.CurrentUserToken();
			if (userToken != null)
			{
				httpContext.Items["AuthType"] = userToken.AuthenticationType;
			}
			if (userToken != null && !string.IsNullOrWhiteSpace(userToken.UserName))
			{
				httpContext.Items["AuthenticatedUser"] = userToken.UserName;
			}
			if (userToken != null)
			{
				string friendlyName = userToken.Organization.GetFriendlyName();
				if (!string.IsNullOrWhiteSpace(friendlyName))
				{
					HttpLogger.SafeSetLogger(ActivityStandardMetadata.TenantId, friendlyName);
				}
			}
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[LoggingEndModule::OnPostAuthenticateRequest] Exit.");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005E58 File Offset: 0x00004058
		private void OnEndRequest(object sender, EventArgs e)
		{
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[LoggingEndModule::OnEndRequest] Enter");
			if (HttpLogger.LoggerNotDisposed)
			{
				try
				{
					HttpContext httpContext = HttpContext.Current;
					HttpResponse response = httpContext.Response;
					HttpLogger.SafeSetLogger(ConfigurationCoreMetadata.SubStatus, response.SubStatusCode);
					CPUMemoryLogger.Log();
					LatencyTracker latencyTracker = HttpContext.Current.Items["Logging-HttpRequest-Latency"] as LatencyTracker;
					if (latencyTracker != null)
					{
						long num = latencyTracker.Stop();
						HttpLogger.SafeSetLogger(ConfigurationCoreMetadata.TotalTime, num);
						latencyTracker.PushLatencyDetailsToLog(null, null, delegate(string funcName, string totalLatency)
						{
							HttpLogger.SafeAppendColumn(RpsCommonMetadata.GenericLatency, funcName, totalLatency);
						});
						if (Constants.IsPowerShellWebService)
						{
							PswsPerfCounter.UpdatePerfCounter(num);
						}
						else
						{
							RPSPerfCounter.UpdateAverageRTCounter(num);
						}
					}
					else
					{
						HttpLogger.SafeAppendColumn(RpsCommonMetadata.GenericLatency, "LatencyMissed", "httpRequestLatencyTracker is null");
					}
					HttpLogger.SafeAppendGenericInfo("OnEndRequest.End.ContentType", response.ContentType);
				}
				finally
				{
					if (HttpLogger.ActivityScope != null)
					{
						RequestMonitor.Instance.UnRegisterRequest(HttpLogger.ActivityScope.ActivityId);
					}
					HttpLogger.AsyncCommit(false);
				}
			}
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[LoggingEndModule::OnEndRequest] Exit");
		}
	}
}
