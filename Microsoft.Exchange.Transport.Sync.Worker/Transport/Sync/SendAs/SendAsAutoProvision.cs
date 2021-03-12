using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.SendAsVerification;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.SendAs
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SendAsAutoProvision
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000F1D2 File Offset: 0x0000D3D2
		public SendAsAutoProvision() : this(new SendAsManager(), new AutoProvisionOverrideProvider())
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F1E4 File Offset: 0x0000D3E4
		public SendAsAutoProvision(SendAsManager sendAsManager, IAutoProvisionOverrideProvider autoProvisionOverrideProvider)
		{
			this.sendAsManager = sendAsManager;
			this.autoProvisionOverrideProvider = autoProvisionOverrideProvider;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F1FC File Offset: 0x0000D3FC
		public void SetAppropriateSendAsState(PimAggregationSubscription subscription, IEmailSender emailSender)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("emailSender", emailSender);
			bool meetsEnableCriteria = this.MeetsEnableCriteria(subscription);
			this.sendAsManager.EnableSendAs(subscription, meetsEnableCriteria, emailSender);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F238 File Offset: 0x0000D438
		internal bool DoesIncomingServerMatchEmailAddress(PimAggregationSubscription subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			if (!subscription.SendAsNeedsVerification)
			{
				throw new ArgumentException("subscription does not need send as verification.  Type: " + subscription.SubscriptionType.ToString(), "subscription");
			}
			string domain = subscription.Email.Domain;
			string verifiedIncomingServer = subscription.VerifiedIncomingServer;
			return verifiedIncomingServer.Length >= domain.Length && (string.Equals(domain, verifiedIncomingServer, StringComparison.OrdinalIgnoreCase) || verifiedIncomingServer.EndsWith("." + domain, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		private bool MeetsEnableCriteria(PimAggregationSubscription subscription)
		{
			if (!subscription.SendAsCapable)
			{
				return false;
			}
			if (!subscription.SendAsNeedsVerification)
			{
				return true;
			}
			if (!string.Equals(subscription.Email.ToString(), subscription.VerifiedUserName, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			string[] collection;
			if (this.TryGetSendAsOverrides(subscription, out collection))
			{
				return SyncUtilities.ExistsInCollection<string>(subscription.VerifiedIncomingServer, collection, StringComparer.OrdinalIgnoreCase);
			}
			return this.DoesIncomingServerMatchEmailAddress(subscription);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F330 File Offset: 0x0000D530
		private bool TryGetSendAsOverrides(PimAggregationSubscription subscription, out string[] overrideHosts)
		{
			overrideHosts = null;
			string domain = subscription.Email.Domain;
			bool flag;
			return this.autoProvisionOverrideProvider.TryGetOverrides(domain, subscription.SubscriptionType, out overrideHosts, out flag) && flag;
		}

		// Token: 0x040001B5 RID: 437
		private SendAsManager sendAsManager;

		// Token: 0x040001B6 RID: 438
		private IAutoProvisionOverrideProvider autoProvisionOverrideProvider;
	}
}
