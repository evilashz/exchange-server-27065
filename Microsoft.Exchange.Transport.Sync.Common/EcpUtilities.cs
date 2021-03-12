using System;
using System.Globalization;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000066 RID: 102
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class EcpUtilities
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00006C7C File Offset: 0x00004E7C
		public static bool TryGetPopSubscriptionDetailsUrl(IExchangePrincipal subscriptionExchangePrincipal, PopAggregationSubscription popSubscription, SyncLogSession syncLogSession, out string popSubscriptionDetailsUrl)
		{
			popSubscriptionDetailsUrl = null;
			SyncUtilities.ThrowIfArgumentNull("subscriptionExchangePrincipal", subscriptionExchangePrincipal);
			SyncUtilities.ThrowIfArgumentNull("popSubscription", popSubscription);
			string text;
			if (!EcpUtilities.TryGetBaseEcpUrl(subscriptionExchangePrincipal, syncLogSession, out text))
			{
				return false;
			}
			string text2 = HttpUtility.UrlEncode(popSubscription.SubscriptionIdentity.ToString());
			popSubscriptionDetailsUrl = string.Format(CultureInfo.InvariantCulture, "{0}PersonalSettings/EditPopSubscription.aspx?exsvurl=1&id={1}", new object[]
			{
				text,
				text2
			});
			popSubscriptionDetailsUrl = EcpUtilities.AppendRealmForEcpUrl(subscriptionExchangePrincipal, popSubscriptionDetailsUrl, syncLogSession);
			return true;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public static bool TryGetHotmailSubscriptionDetailsUrl(IExchangePrincipal subscriptionExchangePrincipal, DeltaSyncAggregationSubscription hotmailSubscription, SyncLogSession syncLogSession, out string hotmailSubscriptionDetailsUrl)
		{
			hotmailSubscriptionDetailsUrl = null;
			SyncUtilities.ThrowIfArgumentNull("subscriptionExchangePrincipal", subscriptionExchangePrincipal);
			SyncUtilities.ThrowIfArgumentNull("hotmailSubscription", hotmailSubscription);
			string text;
			if (!EcpUtilities.TryGetBaseEcpUrl(subscriptionExchangePrincipal, syncLogSession, out text))
			{
				return false;
			}
			string text2 = HttpUtility.UrlEncode(hotmailSubscription.SubscriptionIdentity.ToString());
			hotmailSubscriptionDetailsUrl = string.Format(CultureInfo.InvariantCulture, "{0}PersonalSettings/EditHotmailSubscription.aspx?exsvurl=1&id={1}", new object[]
			{
				text,
				text2
			});
			hotmailSubscriptionDetailsUrl = EcpUtilities.AppendRealmForEcpUrl(subscriptionExchangePrincipal, hotmailSubscriptionDetailsUrl, syncLogSession);
			return true;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00006D64 File Offset: 0x00004F64
		public static bool TryGetImapSubscriptionDetailsUrl(IExchangePrincipal subscriptionExchangePrincipal, IMAPAggregationSubscription imapSubscription, SyncLogSession syncLogSession, out string imapSubscriptionDetailsUrl)
		{
			imapSubscriptionDetailsUrl = null;
			SyncUtilities.ThrowIfArgumentNull("subscriptionExchangePrincipal", subscriptionExchangePrincipal);
			SyncUtilities.ThrowIfArgumentNull("imapSubscription", imapSubscription);
			string text;
			if (!EcpUtilities.TryGetBaseEcpUrl(subscriptionExchangePrincipal, syncLogSession, out text))
			{
				return false;
			}
			string text2 = HttpUtility.UrlEncode(imapSubscription.SubscriptionIdentity.ToString());
			imapSubscriptionDetailsUrl = string.Format(CultureInfo.InvariantCulture, "{0}PersonalSettings/EditImapSubscription.aspx?exsvurl=1&id={1}", new object[]
			{
				text,
				text2
			});
			imapSubscriptionDetailsUrl = EcpUtilities.AppendRealmForEcpUrl(subscriptionExchangePrincipal, imapSubscriptionDetailsUrl, syncLogSession);
			return true;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public static bool TryGetSendAsVerificationUrl(IExchangePrincipal subscriptionExchangePrincipal, int subscriptionTypeCode, Guid subscriptionGuid, Guid sharedSecret, SyncLogSession syncLogSession, out string sendAsVerificationUrl)
		{
			sendAsVerificationUrl = null;
			SyncUtilities.ThrowIfArgumentNull("subscriptionExchangePrincipal", subscriptionExchangePrincipal);
			string text;
			if (!EcpUtilities.TryGetBaseEcpUrl(subscriptionExchangePrincipal, syncLogSession, out text))
			{
				return false;
			}
			sendAsVerificationUrl = string.Format(CultureInfo.InvariantCulture, "{0}PersonalSettings/VerifySendAs.aspx?exsvurl=1&st={1}&su={2}&ss={3}", new object[]
			{
				text,
				HttpUtility.UrlEncode(subscriptionTypeCode.ToString()),
				HttpUtility.UrlEncode(subscriptionGuid.ToString()),
				HttpUtility.UrlEncode(sharedSecret.ToString())
			});
			sendAsVerificationUrl = EcpUtilities.AppendRealmForEcpUrl(subscriptionExchangePrincipal, sendAsVerificationUrl, syncLogSession);
			return true;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00006E68 File Offset: 0x00005068
		private static bool TryGetBaseEcpUrl(IExchangePrincipal subscriptionExchangePrincipal, SyncLogSession syncLogSession, out string baseEcpUrl)
		{
			baseEcpUrl = null;
			Uri uri = null;
			try
			{
				uri = FrontEndLocator.GetFrontEndEcpUrl(subscriptionExchangePrincipal);
			}
			catch (ServerNotFoundException)
			{
			}
			if (uri == null)
			{
				syncLogSession.LogError((TSLID)1UL, "Unable to retrieve an ECP base URL", new object[0]);
				return false;
			}
			baseEcpUrl = uri.ToString();
			return true;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00006EC4 File Offset: 0x000050C4
		private static string AppendRealmForEcpUrl(IExchangePrincipal subscriptionExchangePrincipal, string ecpUrl, SyncLogSession syncLogSession)
		{
			Guid externalDirectoryOrganizationId = new Guid(subscriptionExchangePrincipal.MailboxInfo.OrganizationId.ToExternalDirectoryOrganizationId());
			ADSessionSettings sessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 281, "AppendRealmForEcpUrl", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Common\\EcpUtilities.cs");
			ADUser aduser = tenantOrRootOrgRecipientSession.FindADUserByObjectId(subscriptionExchangePrincipal.ObjectId);
			if (aduser != null)
			{
				SmtpAddress smtpAddress = new SmtpAddress(aduser.UserPrincipalName);
				if (smtpAddress.IsValidAddress)
				{
					ecpUrl = ecpUrl.Replace("exsvurl=1", "exsvurl=1&realm=" + HttpUtility.UrlEncode(smtpAddress.Domain));
				}
			}
			return ecpUrl;
		}

		// Token: 0x04000114 RID: 276
		private const string RealmParameter = "&realm=";

		// Token: 0x04000115 RID: 277
		private const string ExsvurlParameter = "exsvurl=1";

		// Token: 0x04000116 RID: 278
		private const string PopSubscriptionDetailsUrl = "{0}PersonalSettings/EditPopSubscription.aspx?exsvurl=1&id={1}";

		// Token: 0x04000117 RID: 279
		private const string HotmailSubscriptionDetailsUrl = "{0}PersonalSettings/EditHotmailSubscription.aspx?exsvurl=1&id={1}";

		// Token: 0x04000118 RID: 280
		private const string ImapSubscriptionDetailsUrl = "{0}PersonalSettings/EditImapSubscription.aspx?exsvurl=1&id={1}";

		// Token: 0x04000119 RID: 281
		private const string SendAsVerificationUrl = "{0}PersonalSettings/VerifySendAs.aspx?exsvurl=1&st={1}&su={2}&ss={3}";
	}
}
