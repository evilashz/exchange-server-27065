using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.LinkedIn;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200004E RID: 78
	public class LinkedInSetup : EcpContentPage
	{
		// Token: 0x060019C9 RID: 6601 RVA: 0x00052BAC File Offset: 0x00050DAC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			try
			{
				LinkedInConfig linkedInConfig = this.ReadConfiguration();
				LinkedInAppConfig config = this.ReadAppConfiguration();
				LinkedInAppAuthorizationResponse response = new LinkedInAuthenticator(linkedInConfig, new LinkedInWebClient(config, LinkedInSetup.Tracer), LinkedInSetup.Tracer).AuthorizeApplication(base.Request.QueryString, base.Request.Cookies, base.Response.Cookies, this.GetAuthorizationCallbackUrl());
				this.ProcessAuthorizationResponse(response);
			}
			catch (ExchangeConfigurationException ex)
			{
				EcpEventLogConstants.Tuple_BadLinkedInConfiguration.LogPeriodicEvent(EcpEventLogExtensions.GetPeriodicKeyPerUser(), new object[]
				{
					EcpEventLogExtensions.GetUserNameToLog(),
					ex
				});
				ErrorHandlingUtil.TransferToErrorPage("badlinkedinconfiguration");
			}
			catch (LinkedInAuthenticationException ex2)
			{
				EcpEventLogConstants.Tuple_LinkedInAuthorizationError.LogEvent(new object[]
				{
					EcpEventLogExtensions.GetUserNameToLog(),
					ex2
				});
				ErrorHandlingUtil.TransferToErrorPage("linkedinauthorizationerror");
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00052C9C File Offset: 0x00050E9C
		private void ProcessAuthorizationResponse(LinkedInAppAuthorizationResponse response)
		{
			if (!string.IsNullOrEmpty(response.AppAuthorizationRedirectUri))
			{
				this.RedirectToAuthorizationEndpoint(response.AppAuthorizationRedirectUri);
				return;
			}
			if (!string.IsNullOrEmpty(response.OAuthProblem))
			{
				this.ProcessAuthorizationDenied();
				return;
			}
			this.ProcessAuthorizationGranted(response.RequestToken, response.RequestSecret, response.OAuthVerifier);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00052CEF File Offset: 0x00050EEF
		private void RedirectToAuthorizationEndpoint(string authorizationEndpoint)
		{
			base.Response.Redirect(authorizationEndpoint);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00052CFD File Offset: 0x00050EFD
		private void ProcessAuthorizationGranted(string requestToken, string requestSecret, string verifier)
		{
			this.ctlNewConnectSubscription.CreateLinkedIn = true;
			this.ctlNewConnectSubscription.RequestToken = requestToken;
			this.ctlNewConnectSubscription.RequestSecret = requestSecret;
			this.ctlNewConnectSubscription.Verifier = verifier;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00052D2F File Offset: 0x00050F2F
		private void ProcessAuthorizationDenied()
		{
			this.ctlNewConnectSubscription.CloseWindowWithoutCreatingSubscription = true;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00052D40 File Offset: 0x00050F40
		private Uri GetAuthorizationCallbackUrl()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			if (!string.IsNullOrWhiteSpace(peopleConnectApplicationConfig.ConsentRedirectEndpoint))
			{
				return new Uri(peopleConnectApplicationConfig.ConsentRedirectEndpoint);
			}
			return EcpFeature.LinkedInSetup.GetFeatureDescriptor().AbsoluteUrl;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00052D80 File Offset: 0x00050F80
		private LinkedInConfig ReadConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			return LinkedInConfig.CreateForAppAuth(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.RequestTokenEndpoint, peopleConnectApplicationConfig.AccessTokenEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri, peopleConnectApplicationConfig.ReadTimeUtc);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00052DC8 File Offset: 0x00050FC8
		private LinkedInAppConfig ReadAppConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			return new LinkedInAppConfig(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.ProfileEndpoint, peopleConnectApplicationConfig.ConnectionsEndpoint, peopleConnectApplicationConfig.RemoveAppEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri);
		}

		// Token: 0x04001AE2 RID: 6882
		private static readonly Trace Tracer = ExTraceGlobals.LinkedInTracer;

		// Token: 0x04001AE3 RID: 6883
		protected NewConnectSubscription ctlNewConnectSubscription;
	}
}
