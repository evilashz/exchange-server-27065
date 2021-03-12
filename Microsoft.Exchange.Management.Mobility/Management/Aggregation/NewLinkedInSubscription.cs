using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.LinkedIn;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NewLinkedInSubscription : INewConnectSubscription
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00008C41 File Offset: 0x00006E41
		internal NewLinkedInSubscription()
		{
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008C49 File Offset: 0x00006E49
		public string SubscriptionName
		{
			get
			{
				return WellKnownNetworkNames.LinkedIn;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008C50 File Offset: 0x00006E50
		public string SubscriptionDisplayName
		{
			get
			{
				return WellKnownNetworkNames.LinkedIn;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008C58 File Offset: 0x00006E58
		public IConfigurable PrepareSubscription(MailboxSession mailbox, ConnectSubscriptionProxy proxy)
		{
			ArgumentValidator.ThrowIfNull("mailbox", mailbox);
			ArgumentValidator.ThrowIfNull("proxy", proxy);
			this.InitializeConfiguration();
			LinkedInTokenInformation linkedInTokenInformation = this.ExchangeRequestTokenForAccessTokenAndSecret(proxy.RequestToken, proxy.RequestSecret, proxy.OAuthVerifier);
			proxy.Subscription.SubscriptionType = AggregationSubscriptionType.LinkedIn;
			proxy.Subscription.SubscriptionProtocolName = ConnectSubscription.LinkedInProtocolName;
			proxy.Subscription.SubscriptionProtocolVersion = ConnectSubscription.LinkedInProtocolVersion;
			proxy.Subscription.SubscriptionEvents = SubscriptionEvents.WorkItemCompleted;
			proxy.SendAsCheckNeeded = false;
			proxy.ProviderGuid = ConnectSubscription.LinkedInProviderGuid;
			proxy.MessageClass = "IPM.Aggregation.LinkedIn";
			proxy.AssignAccessToken(linkedInTokenInformation.Token);
			proxy.AssignAccessTokenSecret(linkedInTokenInformation.Secret);
			proxy.AppId = this.authConfig.AppId;
			proxy.UserId = this.GetUserId(proxy.RequestToken, linkedInTokenInformation.Token, linkedInTokenInformation.Secret);
			OscFolderCreateResult oscFolderCreateResult = new OscFolderCreator(mailbox).Create("LinkedIn", proxy.UserId);
			if (!oscFolderCreateResult.Created)
			{
				new OscFolderMigration(mailbox, OscContactSourcesForContactParser.Instance).Migrate(oscFolderCreateResult.FolderId);
				proxy.InitialSyncInRecoveryMode = true;
			}
			return proxy;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008D74 File Offset: 0x00006F74
		public void InitializeFolderAndNotifyApps(MailboxSession mailbox, ConnectSubscriptionProxy subscription)
		{
			if (mailbox == null)
			{
				throw new ArgumentNullException("mailbox");
			}
			if (subscription == null)
			{
				throw new ArgumentNullException("subscription");
			}
			if (NewLinkedInSubscription.IsTestHook(subscription.RequestToken))
			{
				return;
			}
			new OscSyncLockCreator(mailbox).Create("LinkedIn", subscription.UserId);
			new PeopleConnectNotifier(mailbox).NotifyConnected(WellKnownNetworkNames.LinkedIn);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008DD4 File Offset: 0x00006FD4
		private void InitializeConfiguration()
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			this.authConfig = LinkedInConfig.CreateForAppAuth(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.RequestTokenEndpoint, peopleConnectApplicationConfig.AccessTokenEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri, peopleConnectApplicationConfig.ReadTimeUtc);
			this.appConfig = new LinkedInAppConfig(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.ProfileEndpoint, peopleConnectApplicationConfig.ConnectionsEndpoint, peopleConnectApplicationConfig.RemoveAppEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008E58 File Offset: 0x00007058
		private LinkedInTokenInformation ExchangeRequestTokenForAccessTokenAndSecret(string requestToken, string requestSecret, string oauthVerifier)
		{
			if (NewLinkedInSubscription.IsTestHook(requestToken))
			{
				return new LinkedInTokenInformation
				{
					Token = "***TEST_ACCESS_TOKEN***",
					Secret = "***TEST_ACCESS_TOKEN_SECRET***"
				};
			}
			return this.CreateAuthenticator().GetAccessToken(requestToken, requestSecret, oauthVerifier);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008E99 File Offset: 0x00007099
		private string GetUserId(string requestToken, string accessToken, string accessTokenSecret)
		{
			if (NewLinkedInSubscription.IsTestHook(requestToken))
			{
				return "***TEST_USER_ID***";
			}
			return new LinkedInWebClient(this.appConfig, NewLinkedInSubscription.Tracer).GetProfile(accessToken, accessTokenSecret, "email-address").EmailAddress;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008ECA File Offset: 0x000070CA
		private static bool IsTestHook(string requestToken)
		{
			return "***TEST_REQUEST_TOKEN***".Equals(requestToken, StringComparison.Ordinal);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008ED8 File Offset: 0x000070D8
		private LinkedInAuthenticator CreateAuthenticator()
		{
			return new LinkedInAuthenticator(this.authConfig, new LinkedInWebClient(this.appConfig, NewLinkedInSubscription.Tracer), NewLinkedInSubscription.Tracer);
		}

		// Token: 0x04000087 RID: 135
		private const string TestRequestToken = "***TEST_REQUEST_TOKEN***";

		// Token: 0x04000088 RID: 136
		private const string TestAccessToken = "***TEST_ACCESS_TOKEN***";

		// Token: 0x04000089 RID: 137
		private const string TestAccessTokenSecret = "***TEST_ACCESS_TOKEN_SECRET***";

		// Token: 0x0400008A RID: 138
		private const string TestUserId = "***TEST_USER_ID***";

		// Token: 0x0400008B RID: 139
		private static readonly Trace Tracer = ExTraceGlobals.LinkedInTracer;

		// Token: 0x0400008C RID: 140
		private LinkedInConfig authConfig;

		// Token: 0x0400008D RID: 141
		private LinkedInAppConfig appConfig;
	}
}
