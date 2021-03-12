using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.LinkedIn;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009BD RID: 2493
	internal class GetConnectToSocialNetworksOAuthInfoCommand : OptionServiceCommandBase<GetSocialNetworksOAuthInfoRequest, GetSocialNetworksOAuthInfoResponse>
	{
		// Token: 0x060046B8 RID: 18104 RVA: 0x000FB679 File Offset: 0x000F9879
		public GetConnectToSocialNetworksOAuthInfoCommand(CallContext callContext, GetSocialNetworksOAuthInfoRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x000FB684 File Offset: 0x000F9884
		protected override GetSocialNetworksOAuthInfoResponse InternalExecute()
		{
			base.LogRequestForDebug();
			GetSocialNetworksOAuthInfoResponse getSocialNetworksOAuthInfoResponse = null;
			try
			{
				getSocialNetworksOAuthInfoResponse = this.CreateTaskAndExecute();
			}
			catch (ExchangeConfigurationException ex)
			{
				RequestDetailsLogger.Current.AppendGenericError("ErrCode", ex.ErrorCode.ToString());
				RequestDetailsLogger.Current.AppendGenericError("ErrMsg", ex.Message);
				getSocialNetworksOAuthInfoResponse = new GetSocialNetworksOAuthInfoResponse
				{
					WasSuccessful = false,
					ErrorCode = OptionsActionError.Unexpected,
					ErrorMessage = ex.Message
				};
			}
			base.LogResponseForDebug(getSocialNetworksOAuthInfoResponse);
			return getSocialNetworksOAuthInfoResponse;
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x000FB714 File Offset: 0x000F9914
		protected override GetSocialNetworksOAuthInfoResponse CreateTaskAndExecute()
		{
			GetSocialNetworksOAuthInfoResponse result = null;
			switch (this.request.ConnectSubscriptionType)
			{
			case ConnectSubscriptionType.Facebook:
			{
				FacebookAuthenticatorConfig config = this.ReadFacebookConfiguration();
				FacebookAuthenticator facebookAuthenticator = new FacebookAuthenticator(config);
				Uri appAuthorizationUri = facebookAuthenticator.GetAppAuthorizationUri();
				result = new GetSocialNetworksOAuthInfoResponse
				{
					SocialNetworksOAuthInfo = new SocialNetworksOAuthInfo
					{
						OAuthUri = appAuthorizationUri.AbsoluteUri
					}
				};
				break;
			}
			case ConnectSubscriptionType.LinkedIn:
			{
				LinkedInConfig linkedInConfig = this.ReadLinkedInConfiguration();
				LinkedInAppConfig config2 = this.ReadLinkedInAppConfiguration();
				Trace linkedInTracer = ExTraceGlobals.LinkedInTracer;
				LinkedInAuthenticator linkedInAuthenticator = new LinkedInAuthenticator(linkedInConfig, new LinkedInWebClient(config2, linkedInTracer), linkedInTracer);
				string callbackUrl = this.AppendOAuthCallbackToCallbackUrl(new Uri(this.request.RedirectUri, UriKind.Absolute)).ToString();
				LinkedInTokenInformation requestToken = linkedInAuthenticator.GetRequestToken(callbackUrl);
				result = new GetSocialNetworksOAuthInfoResponse
				{
					SocialNetworksOAuthInfo = new SocialNetworksOAuthInfo
					{
						OAuthUri = requestToken.OAuthAccessTokenUrl + "?oauth_token=" + requestToken.Token
					}
				};
				base.CallContext.HttpContext.Response.Cookies.Add(new HttpCookie(PeopleConstants.RequestSecretCookieName, requestToken.Secret)
				{
					HttpOnly = true,
					Secure = true
				});
				break;
			}
			}
			return result;
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x000FB854 File Offset: 0x000F9A54
		private Uri AppendOAuthCallbackToCallbackUrl(Uri authorizationCallbackUrl)
		{
			return this.AppendNameValueToQueryString(authorizationCallbackUrl, "oauth_callback", "1");
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x000FB868 File Offset: 0x000F9A68
		private Uri AppendNameValueToQueryString(Uri url, string name, string value)
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(url.Query);
			if (nameValueCollection[name] != null)
			{
				return url;
			}
			StringBuilder stringBuilder = new StringBuilder(url.Query);
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(0, 1).Append('&');
			}
			UriBuilder uriBuilder = new UriBuilder(url)
			{
				Query = stringBuilder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(name), HttpUtility.UrlEncode(value)).ToString()
			};
			return uriBuilder.Uri;
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x000FB8E4 File Offset: 0x000F9AE4
		private FacebookAuthenticatorConfig ReadFacebookConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadFacebook();
			return FacebookAuthenticatorConfig.CreateForAppAuthorization(peopleConnectApplicationConfig.AppId, this.request.RedirectUri, peopleConnectApplicationConfig.AuthorizationEndpoint, EWSSettings.ClientCulture, peopleConnectApplicationConfig.ReadTimeUtc);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x000FB924 File Offset: 0x000F9B24
		private LinkedInAppConfig ReadLinkedInAppConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			return new LinkedInAppConfig(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.ProfileEndpoint, peopleConnectApplicationConfig.ConnectionsEndpoint, peopleConnectApplicationConfig.RemoveAppEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x000FB96C File Offset: 0x000F9B6C
		private LinkedInConfig ReadLinkedInConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			return LinkedInConfig.CreateForAppAuth(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.RequestTokenEndpoint, peopleConnectApplicationConfig.AccessTokenEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri, peopleConnectApplicationConfig.ReadTimeUtc);
		}

		// Token: 0x040028AC RID: 10412
		private const string OAuthCallback = "oauth_callback";

		// Token: 0x040028AD RID: 10413
		private const string OAuthCallbackValue = "1";
	}
}
