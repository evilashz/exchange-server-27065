using System;
using System.Configuration;
using System.Web;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.FailFast;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x0200000E RID: 14
	public class PowerShellFailureThrottlingModule : IHttpModule
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000031FC File Offset: 0x000013FC
		static PowerShellFailureThrottlingModule()
		{
			if (!bool.TryParse(ConfigurationManager.AppSettings["FailureThrottlingEnabled"], out PowerShellFailureThrottlingModule.failureThrottlingEnabled))
			{
				PowerShellFailureThrottlingModule.failureThrottlingEnabled = false;
			}
			Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "failureThrottlingEnabled = {0}.", new object[]
			{
				PowerShellFailureThrottlingModule.failureThrottlingEnabled
			});
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000324E File Offset: 0x0000144E
		void IHttpModule.Init(HttpApplication application)
		{
			application.PreSendRequestHeaders += PowerShellFailureThrottlingModule.ApplicationPreSendRequestHeaders;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003262 File Offset: 0x00001462
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003264 File Offset: 0x00001464
		private static void ApplicationPreSendRequestHeaders(object sender, EventArgs e)
		{
			if (!FailFastUserCache.FailFastEnabled)
			{
				return;
			}
			Logger.EnterFunction(ExTraceGlobals.FailureThrottlingTracer, "PowerShellFailureThrottlingModule.ApplicationPreSendRequestContent");
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			if (context == null || context.Request == null || context.Response == null)
			{
				Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "context == null || context.Request == null || context.Response == null", new object[0]);
				Logger.ExitFunction(ExTraceGlobals.FailureThrottlingTracer, "PowerShellFailureThrottlingModule.ApplicationPreSendRequestContent");
				return;
			}
			string text = context.Response.Headers[FailFastModule.HeaderKeyToStoreUserToken];
			Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "Current UserToken is {0}.", new object[]
			{
				text
			});
			context.Response.Headers.Remove(FailFastModule.HeaderKeyToStoreUserToken);
			Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "Remove {0} from header.", new object[]
			{
				FailFastModule.HeaderKeyToStoreUserToken
			});
			if (!PowerShellFailureThrottlingModule.failureThrottlingEnabled)
			{
				Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "Failure throttling is disabled.", new object[0]);
				Logger.ExitFunction(ExTraceGlobals.FailureThrottlingTracer, "PowerShellFailureThrottlingModule.ApplicationPreSendRequestContent");
				return;
			}
			if (!context.Request.IsAuthenticated)
			{
				Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "Un-authenticated request.", new object[0]);
				Logger.ExitFunction(ExTraceGlobals.FailureThrottlingTracer, "PowerShellFailureThrottlingModule.ApplicationPreSendRequestContent");
				return;
			}
			if (!string.IsNullOrEmpty(text) && FailureThrottling.CountBasedOnStatusCode(text, context.Response.StatusCode))
			{
				HttpLogger.SafeAppendColumn(RpsCommonMetadata.ContributeToFailFast, "FailureThrottling", LoggerHelper.GetContributeToFailFastValue("User", text, "NewRequest", -1.0));
				Logger.TraceDebug(ExTraceGlobals.FailureThrottlingTracer, "Add user {0} to fail-fast user cache.", new object[]
				{
					text
				});
				FailFastUserCache.Instance.AddUserToCache(text, BlockedType.NewRequest, TimeSpan.Zero);
			}
			Logger.ExitFunction(ExTraceGlobals.FailureThrottlingTracer, "PowerShellFailureThrottlingModule.ApplicationPreSendRequestContent");
		}

		// Token: 0x0400002E RID: 46
		private static readonly bool failureThrottlingEnabled;
	}
}
