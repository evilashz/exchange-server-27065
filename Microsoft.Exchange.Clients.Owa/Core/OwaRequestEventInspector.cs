using System;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F6 RID: 502
	internal class OwaRequestEventInspector : RequestEventInspectorBase
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x000655D8 File Offset: 0x000637D8
		internal override void Init()
		{
			this.RegisterFilterForOnBegin();
			this.RegisterFilterForOnPostAuthorize();
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000655E6 File Offset: 0x000637E6
		internal override void OnBeginRequest(object sender, EventArgs e, out bool stopExecution)
		{
			stopExecution = false;
			if (this.onBeginRequestChain != null)
			{
				stopExecution = this.onBeginRequestChain.ExecuteRequestFilterChain(sender, e, RequestEventType.BeginRequest);
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00065604 File Offset: 0x00063804
		internal override void OnPostAuthorizeRequest(object sender, EventArgs e)
		{
			bool flag = false;
			if (this.onPostAuthorizeRequestChain != null)
			{
				flag = this.onPostAuthorizeRequestChain.ExecuteRequestFilterChain(sender, e, RequestEventType.PostAuthorizeRequest);
			}
			if (flag)
			{
				return;
			}
			HttpApplication httpApplication = (HttpApplication)sender;
			if (UrlUtilities.IsWacRequest(httpApplication.Context.Request))
			{
				return;
			}
			try
			{
				RequestDispatcher.DispatchRequest(OwaContext.Get(httpApplication.Context));
			}
			catch (ThreadAbortException)
			{
				OwaContext.Current.UnlockMinResourcesOnCriticalError();
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00065678 File Offset: 0x00063878
		private void RegisterFilterForOnPostAuthorize()
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.ExplicitLogonAuthFilter.Enabled)
			{
				this.RegisterFilterForPostAuthorizeRequest(new AlternateMailboxFilterChain());
				return;
			}
			this.RegisterFilterForPostAuthorizeRequest(new FBASingleSignOnFilterChain());
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000656BC File Offset: 0x000638BC
		private void RegisterFilterForPostAuthorizeRequest(RequestFilterChain filter)
		{
			if (this.onPostAuthorizeRequestChain == null)
			{
				this.onPostAuthorizeRequestChain = filter;
				return;
			}
			filter.Next = this.onPostAuthorizeRequestChain;
			this.onPostAuthorizeRequestChain = filter;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000656E1 File Offset: 0x000638E1
		private void RegisterFilterForOnBegin()
		{
			this.RegisterFilterForOnBeginRequest(new FBASingleSignOnFilterChain());
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x000656EE File Offset: 0x000638EE
		private void RegisterFilterForOnBeginRequest(RequestFilterChain filter)
		{
			if (this.onBeginRequestChain == null)
			{
				this.onBeginRequestChain = filter;
				return;
			}
			filter.Next = this.onBeginRequestChain;
			this.onBeginRequestChain = filter;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00065714 File Offset: 0x00063914
		internal override void OnEndRequest(OwaContext owaContext)
		{
			try
			{
			}
			finally
			{
				UserContext userContext = owaContext.UserContext;
				bool flag = false;
				try
				{
					if (owaContext.UserContext != null && !owaContext.UserContext.LockedByCurrentThread())
					{
						if (!owaContext.IsAsyncRequest && !owaContext.HandledCriticalError && !owaContext.UserContext.LastLockRequestFailed)
						{
							ExWatson.SendReport(new InvalidOperationException("Entered OwaRequestEventInspector without the UserContext lock when we should have had it."), ReportOptions.None, null);
						}
						owaContext.UserContext.Lock();
					}
					try
					{
						try
						{
							if (userContext != null && userContext.State == UserContextState.Active)
							{
								userContext.CleanupOnEndRequest();
								owaContext.ExitLatencyDetectionContext();
								OwaPerformanceLogger.LogPerformanceStatistics(userContext);
								OwaPerformanceLogger.TracePerformance(userContext);
								this.AppendServerHeaders(owaContext);
								if (owaContext.SearchPerformanceData != null)
								{
									owaContext.SearchPerformanceData.RefreshEnd();
									owaContext.SearchPerformanceData.WriteLog();
								}
							}
						}
						finally
						{
							owaContext.DisposeObjectsOnEndRequest();
						}
					}
					catch (OwaLockTimeoutException)
					{
						flag = true;
					}
					finally
					{
						if (userContext != null && !flag)
						{
							if (owaContext.IgnoreUnlockForcefully)
							{
								userContext.Unlock();
							}
							else
							{
								userContext.UnlockForcefully();
							}
						}
					}
				}
				finally
				{
					owaContext.TryReleaseBudgetAndStopTiming();
					if (owaContext.PreFormActionData != null && owaContext.PreFormActionData is IDisposable)
					{
						((IDisposable)owaContext.PreFormActionData).Dispose();
						owaContext.PreFormActionData = null;
					}
				}
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000658A8 File Offset: 0x00063AA8
		private void AppendServerHeaders(OwaContext owaContext)
		{
			HttpContext httpContext = owaContext.HttpContext;
			if (httpContext == null || httpContext.Response == null || !UserAgentUtilities.IsMonitoringRequest(httpContext.Request.UserAgent))
			{
				return;
			}
			try
			{
				if (owaContext.UserContext.ExchangePrincipal != null)
				{
					string shortServerNameFromFqdn = Utilities.GetShortServerNameFromFqdn(owaContext.UserContext.ExchangePrincipal.MailboxInfo.Location.ServerFqdn);
					if (shortServerNameFromFqdn != null)
					{
						httpContext.Response.AppendHeader("X-DiagInfoMailbox", shortServerNameFromFqdn);
					}
				}
				string lastRecipientSessionDCServerName = owaContext.UserContext.LastRecipientSessionDCServerName;
				if (lastRecipientSessionDCServerName != null)
				{
					httpContext.Response.AppendHeader("X-DiagInfoDomainController", Utilities.GetShortServerNameFromFqdn(lastRecipientSessionDCServerName));
				}
			}
			catch (HttpException arg)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<HttpException>(0L, "Exception happened while trying to append server name headers. Exception will be ignored: {0}", arg);
			}
		}

		// Token: 0x04000B26 RID: 2854
		private RequestFilterChain onPostAuthorizeRequestChain;

		// Token: 0x04000B27 RID: 2855
		private RequestFilterChain onBeginRequestChain;
	}
}
