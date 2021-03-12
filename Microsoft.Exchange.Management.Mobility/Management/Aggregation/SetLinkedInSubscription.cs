using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.LinkedIn;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SetLinkedInSubscription : ISetConnectSubscription
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000B2E0 File Offset: 0x000094E0
		public void StampChangesOn(ConnectSubscriptionProxy proxy)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.InitializeConfiguration();
			LinkedInTokenInformation linkedInTokenInformation = this.ExchangeRequestTokenForAccessTokenAndSecret(proxy.RequestToken, proxy.RequestSecret, proxy.OAuthVerifier);
			this.RejectIfDifferentAccount(proxy, proxy.RequestToken, linkedInTokenInformation.Token, linkedInTokenInformation.Secret);
			proxy.AssignAccessToken(linkedInTokenInformation.Token);
			proxy.AssignAccessTokenSecret(linkedInTokenInformation.Secret);
			proxy.AppId = this.authConfig.AppId;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B35C File Offset: 0x0000955C
		public void NotifyApps(MailboxSession mailbox)
		{
			if (mailbox == null)
			{
				throw new ArgumentNullException("mailbox");
			}
			new PeopleConnectNotifier(mailbox).NotifyConnected(WellKnownNetworkNames.LinkedIn);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B37C File Offset: 0x0000957C
		private void InitializeConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			this.authConfig = LinkedInConfig.CreateForAppAuth(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.RequestTokenEndpoint, peopleConnectApplicationConfig.AccessTokenEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri, peopleConnectApplicationConfig.ReadTimeUtc);
			this.appConfig = new LinkedInAppConfig(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.ProfileEndpoint, peopleConnectApplicationConfig.ConnectionsEndpoint, peopleConnectApplicationConfig.RemoveAppEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B400 File Offset: 0x00009600
		private void RejectIfDifferentAccount(ConnectSubscriptionProxy proxy, string requestToken, string newAccessToken, string newAccessTokenSecret)
		{
			if (SetLinkedInSubscription.IsTestHook(requestToken))
			{
				return;
			}
			string emailAddress = new LinkedInWebClient(this.appConfig, SetLinkedInSubscription.Tracer).GetProfile(newAccessToken, newAccessTokenSecret, "email-address").EmailAddress;
			if (!string.Equals(proxy.UserId, emailAddress, StringComparison.Ordinal))
			{
				throw new CannotSwitchLinkedInAccountException();
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B450 File Offset: 0x00009650
		private LinkedInTokenInformation ExchangeRequestTokenForAccessTokenAndSecret(string requestToken, string requestSecret, string oauthVerifier)
		{
			if (SetLinkedInSubscription.IsTestHook(requestToken))
			{
				return new LinkedInTokenInformation
				{
					Token = "***TEST_SET_ACCESS_TOKEN***",
					Secret = "***TEST_SET_ACCESS_TOKEN_SECRET***"
				};
			}
			return this.CreateAuthenticator().GetAccessToken(requestToken, requestSecret, oauthVerifier);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B491 File Offset: 0x00009691
		private static bool IsTestHook(string requestToken)
		{
			return "***TEST_SET_REQUEST_TOKEN***".Equals(requestToken, StringComparison.Ordinal);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B49F File Offset: 0x0000969F
		private LinkedInAuthenticator CreateAuthenticator()
		{
			return new LinkedInAuthenticator(this.authConfig, new LinkedInWebClient(this.appConfig, SetLinkedInSubscription.Tracer), SetLinkedInSubscription.Tracer);
		}

		// Token: 0x040000A1 RID: 161
		private const string TestRequestToken = "***TEST_SET_REQUEST_TOKEN***";

		// Token: 0x040000A2 RID: 162
		private const string TestAccessToken = "***TEST_SET_ACCESS_TOKEN***";

		// Token: 0x040000A3 RID: 163
		private const string TestAccessTokenSecret = "***TEST_SET_ACCESS_TOKEN_SECRET***";

		// Token: 0x040000A4 RID: 164
		private const string TestUserId = "***TEST_SET_USER_ID***";

		// Token: 0x040000A5 RID: 165
		private static readonly Trace Tracer = ExTraceGlobals.LinkedInTracer;

		// Token: 0x040000A6 RID: 166
		private LinkedInConfig authConfig;

		// Token: 0x040000A7 RID: 167
		private LinkedInAppConfig appConfig;
	}
}
