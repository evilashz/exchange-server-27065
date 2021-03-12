using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200004D RID: 77
	public class FacebookSetup : EcpContentPage
	{
		// Token: 0x060019C2 RID: 6594 RVA: 0x0005294C File Offset: 0x00050B4C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			try
			{
				this.ctlUserConsentForm.Visible = false;
				FacebookAuthenticatorConfig config = this.ReadConfiguration();
				FacebookAuthenticator facebookAuthenticator = new FacebookAuthenticator(config);
				AppAuthorizationResponse response = FacebookAuthenticator.ParseAppAuthorizationResponse(base.Request.QueryString);
				if (!FacebookAuthenticator.IsRedirectFromFacebook(response))
				{
					string text = facebookAuthenticator.GetAppAuthorizationUri().ToString();
					if (this.IsReconnect())
					{
						base.Response.Redirect(text);
					}
					else
					{
						this.ctlUserConsentForm.Visible = true;
						this.ctlUserConsentForm.AuthorizationUrl = text;
					}
				}
				else if (facebookAuthenticator.IsAuthorizationGranted(response))
				{
					this.ProcessAuthorizationGranted(response);
				}
				else
				{
					this.ProcessAuthorizationDenied();
				}
			}
			catch (ExchangeConfigurationException ex)
			{
				EcpEventLogConstants.Tuple_BadFacebookConfiguration.LogPeriodicEvent(EcpEventLogExtensions.GetPeriodicKeyPerUser(), new object[]
				{
					EcpEventLogExtensions.GetUserNameToLog(),
					ex
				});
				ErrorHandlingUtil.TransferToErrorPage("badfacebookconfiguration");
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00052A30 File Offset: 0x00050C30
		private FacebookAuthenticatorConfig ReadConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadFacebook();
			return FacebookAuthenticatorConfig.CreateForAppAuthorization(peopleConnectApplicationConfig.AppId, this.GetRedirectUri(), peopleConnectApplicationConfig.AuthorizationEndpoint, Thread.CurrentThread.CurrentUICulture, peopleConnectApplicationConfig.ReadTimeUtc);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00052A70 File Offset: 0x00050C70
		private string GetRedirectUri()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadFacebook();
			string text = string.IsNullOrWhiteSpace(peopleConnectApplicationConfig.ConsentRedirectEndpoint) ? EcpFeature.FacebookSetup.GetFeatureDescriptor().AbsoluteUrl.ToEscapedString() : peopleConnectApplicationConfig.ConsentRedirectEndpoint;
			if (this.IsReconnect())
			{
				UriBuilder uriBuilder = new UriBuilder(text)
				{
					Query = string.Format("{0}={1}", "Action", "Reconnect")
				};
				text = uriBuilder.Uri.ToEscapedString();
			}
			return text;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00052AE7 File Offset: 0x00050CE7
		private bool IsReconnect()
		{
			return "Reconnect".Equals(base.Request.QueryString["Action"], StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00052B0C File Offset: 0x00050D0C
		private void ProcessAuthorizationGranted(AppAuthorizationResponse response)
		{
			if (this.IsReconnect())
			{
				this.ctlSetConnectSubscription.SetFacebook = true;
				this.ctlSetConnectSubscription.AppAuthorizationCode = response.AppAuthorizationCode;
				this.ctlSetConnectSubscription.RedirectUri = this.GetRedirectUri();
				return;
			}
			this.ctlNewConnectSubscription.CreateFacebook = true;
			this.ctlNewConnectSubscription.AppAuthorizationCode = response.AppAuthorizationCode;
			this.ctlNewConnectSubscription.RedirectUri = this.GetRedirectUri();
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00052B7E File Offset: 0x00050D7E
		private void ProcessAuthorizationDenied()
		{
			if (this.IsReconnect())
			{
				this.ctlSetConnectSubscription.CloseWindowWithoutUpdatingSubscription = true;
				return;
			}
			this.ctlNewConnectSubscription.CloseWindowWithoutCreatingSubscription = true;
		}

		// Token: 0x04001ADD RID: 6877
		private const string ActionParameter = "Action";

		// Token: 0x04001ADE RID: 6878
		private const string ReconnectValue = "Reconnect";

		// Token: 0x04001ADF RID: 6879
		protected NewConnectSubscription ctlNewConnectSubscription;

		// Token: 0x04001AE0 RID: 6880
		protected SetConnectSubscription ctlSetConnectSubscription;

		// Token: 0x04001AE1 RID: 6881
		protected FacebookUserConsentForm ctlUserConsentForm;
	}
}
