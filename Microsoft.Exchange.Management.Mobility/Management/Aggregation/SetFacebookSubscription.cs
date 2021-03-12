using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Facebook;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SetFacebookSubscription : ISetConnectSubscription
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		public void StampChangesOn(ConnectSubscriptionProxy proxy)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.InitializeConfiguration(proxy.RedirectUri);
			string text;
			string userId;
			if (SetFacebookSubscription.IsTestHook(proxy.AppAuthorizationCode))
			{
				text = "***TEST_SET_ACCESS_TOKEN***";
				userId = "***TEST_SET_USER_ID***";
			}
			else
			{
				text = new FacebookAuthenticator(this.config).ExchangeAppAuthorizationCodeForAccessToken(proxy.AppAuthorizationCode);
				userId = SetFacebookSubscription.GetUserId(text);
			}
			proxy.AssignAccessToken(text);
			proxy.AppId = this.config.AppId;
			proxy.UserId = userId;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000AB78 File Offset: 0x00008D78
		private static string GetUserId(string accessToken)
		{
			if (string.IsNullOrEmpty(accessToken))
			{
				throw new FacebookUpdateSubscriptionException(Strings.FacebookEmptyAccessToken);
			}
			string result = null;
			try
			{
				string graphApiEndpoint = CachedPeopleConnectApplicationConfig.Instance.ReadFacebook().GraphApiEndpoint;
				FacebookUser profile = new FacebookClient(new Uri(graphApiEndpoint)).GetProfile(accessToken, "id");
				if (profile != null)
				{
					result = profile.Id;
				}
			}
			catch (TimeoutException innerException)
			{
				throw new FacebookUpdateSubscriptionException(Strings.FacebookTimeoutError, innerException);
			}
			catch (ProtocolException innerException2)
			{
				throw new FacebookUpdateSubscriptionException(Strings.FacebookAuthorizationError, innerException2);
			}
			catch (CommunicationException innerException3)
			{
				throw new FacebookUpdateSubscriptionException(Strings.FacebookCommunicationError, innerException3);
			}
			return result;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000AC24 File Offset: 0x00008E24
		public void NotifyApps(MailboxSession mailbox)
		{
			if (mailbox == null)
			{
				throw new ArgumentNullException("mailbox");
			}
			new PeopleConnectNotifier(mailbox).NotifyConnected(WellKnownNetworkNames.Facebook);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000AC44 File Offset: 0x00008E44
		private void InitializeConfiguration(string redirectUri)
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadFacebook();
			this.config = FacebookAuthenticatorConfig.CreateForAppAuthentication(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, redirectUri, peopleConnectApplicationConfig.GraphTokenEndpoint, new FacebookAuthenticationWebClient(), peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.ReadTimeUtc);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000AC8B File Offset: 0x00008E8B
		private static bool IsTestHook(string appAuthorizationCode)
		{
			return "***TEST_SET_APPCODE***".Equals(appAuthorizationCode, StringComparison.Ordinal);
		}

		// Token: 0x0400009B RID: 155
		private const string TestAppAuthorizationCode = "***TEST_SET_APPCODE***";

		// Token: 0x0400009C RID: 156
		private const string TestAccessToken = "***TEST_SET_ACCESS_TOKEN***";

		// Token: 0x0400009D RID: 157
		private const string TestUserId = "***TEST_SET_USER_ID***";

		// Token: 0x0400009E RID: 158
		private FacebookAuthenticatorConfig config;
	}
}
