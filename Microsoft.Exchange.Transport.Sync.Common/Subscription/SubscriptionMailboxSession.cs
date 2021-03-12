using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000C5 RID: 197
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SubscriptionMailboxSession
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0001BA38 File Offset: 0x00019C38
		public SubscriptionMailboxSession(MailboxSession mailboxSession)
		{
			SyncUtilities.ThrowIfArgumentNull("mailboxSession", mailboxSession);
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001BA52 File Offset: 0x00019C52
		protected SubscriptionMailboxSession()
		{
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001BA5A File Offset: 0x00019C5A
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001BA64 File Offset: 0x00019C64
		internal virtual void SetPropertiesOfSubscription(ISyncWorkerData subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			subscription.SubscriptionIdentity.LegacyDN = this.mailboxSession.MailboxOwnerLegacyDN;
			subscription.SubscriptionIdentity.PrimaryMailboxLegacyDN = this.mailboxSession.MailboxOwner.LegacyDn;
			subscription.SubscriptionIdentity.AdUserId = this.mailboxSession.MailboxOwner.ObjectId;
			subscription.UserExchangeMailboxSmtpAddress = this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			subscription.UserExchangeMailboxDisplayName = this.mailboxSession.MailboxOwner.MailboxInfo.DisplayName;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001BB0C File Offset: 0x00019D0C
		internal virtual string GetMailboxServerName()
		{
			return this.mailboxSession.ServerFullyQualifiedDomainName;
		}

		// Token: 0x04000320 RID: 800
		private MailboxSession mailboxSession;
	}
}
