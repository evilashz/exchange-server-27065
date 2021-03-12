using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200023B RID: 571
	public sealed class ProxySecurityContextModule : IHttpModule
	{
		// Token: 0x06001437 RID: 5175 RVA: 0x0004A0B2 File Offset: 0x000482B2
		public void Init(HttpApplication context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.AuthenticateRequest += this.OnAuthenticateRequest;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0004A0D4 File Offset: 0x000482D4
		public void Dispose()
		{
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0004A0D8 File Offset: 0x000482D8
		private void OnAuthenticateRequest(object source, EventArgs e)
		{
			ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "Start OnAuthenticateRequest");
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			UserToken userToken = context.CurrentUserToken();
			if (this.IsDelegatedAuth(context))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "Delegated auth, let delegated auth module handle the request");
			}
			else
			{
				if (context.Request.IsAuthenticated && userToken.HasCommonAccessToken)
				{
					try
					{
						if (userToken.HasCommonAccessToken && ProxySecurityContextModule.AuthenticationTypes.ContainsKey(userToken.AuthenticationType))
						{
							string text = ProxySecurityContextModule.AuthenticationTypes[userToken.AuthenticationType];
							if (context.User != null && context.User.Identity != null)
							{
								context.Items["AuthType"] = context.User.Identity.AuthenticationType;
								if (!context.Items.Contains("AuthenticatedUser"))
								{
									context.Items["AuthenticatedUser"] = context.User.Identity.Name;
								}
								else
								{
									HttpLogger.SafeAppendGenericInfo("User.Identity", context.User.Identity.Name);
								}
							}
							ExTraceGlobals.AccessCheckTracer.TraceDebug<AuthenticationType, string>(0L, "Token Type = {0}, AuthenticationType = {1}.", userToken.AuthenticationType, text);
							ExAssert.RetailAssert(context.User != null && context.User.Identity != null && context.User.Identity.Name != null, "HttpContext.User.Identity.Name should not be null.");
							GenericIdentity identity = new GenericIdentity(context.User.Identity.Name.ToLower(), text);
							context.User = new GenericPrincipal(identity, new string[0]);
							ProxySecurityContextModule.SendAuthenticationDataToWinRM(context, userToken);
						}
						goto IL_201;
					}
					catch (Exception ex)
					{
						HttpModuleHelper.EndPowerShellRequestWithFriendlyError(context, FailureCategory.ProxySecurityContext, "Exception", ex.ToString(), "ProxySecurityContextModule", KnownException.IsUnhandledException(ex));
						goto IL_201;
					}
				}
				ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "Request is Unauthorized.");
				WinRMInfo.SetFailureCategoryInfo(context.Response.Headers, FailureCategory.ProxySecurityContext, "Unauthorized");
				context.Response.StatusCode = 401;
				httpApplication.CompleteRequest();
			}
			IL_201:
			ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "Exit OnAuthenticateRequest");
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0004A314 File Offset: 0x00048514
		private bool IsDelegatedAuth(HttpContext httpContext)
		{
			return httpContext.Request.IsAuthenticated && httpContext.User is DelegatedPrincipal;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0004A334 File Offset: 0x00048534
		private static void SendAuthenticationDataToWinRM(HttpContext httpContext, UserToken userToken)
		{
			WinRMInfo winRMInfo = httpContext.Items["X-RemotePS-WinRMInfo"] as WinRMInfo;
			if (ProxySecurityContextModule.NeedSendDataToWinRM(winRMInfo))
			{
				userToken.UniformCommonAccessToken();
				if (winRMInfo != null && "New-PSSession".Equals(winRMInfo.Action, StringComparison.OrdinalIgnoreCase))
				{
					HttpLogger.SafeAppendGenericInfo("WinRMCAT", userToken.GetReadableCommonAccessToken());
				}
				LatencyTracker latencyTracker = HttpContext.Current.Items["Logging-HttpRequest-Latency"] as LatencyTracker;
				using (WinRMDataSender winRMDataSender = new WinRMDataSender(httpContext, latencyTracker))
				{
					winRMDataSender.SessionId = winRMInfo.FomattedSessionId;
					if (HttpLogger.ActivityScope != null)
					{
						Guid activityId = HttpLogger.ActivityScope.ActivityId;
						winRMDataSender.RequestId = HttpLogger.ActivityScope.ActivityId.ToString();
					}
					winRMDataSender.UserToken = userToken;
					winRMDataSender.Send();
				}
			}
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0004A418 File Offset: 0x00048618
		private static bool NeedSendDataToWinRM(WinRMInfo winRMInfo)
		{
			return winRMInfo == null || string.IsNullOrEmpty(winRMInfo.Action) || winRMInfo.Action.Equals("New-PSSession", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005B7 RID: 1463
		private static readonly Dictionary<AuthenticationType, string> AuthenticationTypes = new Dictionary<AuthenticationType, string>
		{
			{
				AuthenticationType.Kerberos,
				"Cafe-WindowsIdentity"
			},
			{
				AuthenticationType.LiveIdBasic,
				"Cafe-GenericIdentity"
			},
			{
				AuthenticationType.LiveIdNego2,
				"Cafe-GenericIdentity"
			},
			{
				AuthenticationType.Certificate,
				"Cafe-GenericIdentity"
			}
		};
	}
}
