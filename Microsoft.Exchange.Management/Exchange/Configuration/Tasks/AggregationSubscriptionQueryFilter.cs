using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000CD0 RID: 3280
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AggregationSubscriptionQueryFilter : QueryFilter
	{
		// Token: 0x06007E8C RID: 32396 RVA: 0x0020525A File Offset: 0x0020345A
		public AggregationSubscriptionQueryFilter(AggregationSubscriptionIdParameter subscriptionIdParameter)
		{
			this.subscriptionIdParameter = subscriptionIdParameter;
		}

		// Token: 0x17002756 RID: 10070
		// (get) Token: 0x06007E8D RID: 32397 RVA: 0x00205269 File Offset: 0x00203469
		internal AggregationSubscriptionIdParameter SubscriptionIdParameter
		{
			get
			{
				return this.subscriptionIdParameter;
			}
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x00205271 File Offset: 0x00203471
		public override void ToString(StringBuilder sb)
		{
			sb.Append(this.subscriptionIdParameter.ToString());
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x00205288 File Offset: 0x00203488
		public bool Match(AggregationSubscription subscription)
		{
			if (subscription == null)
			{
				return false;
			}
			if (this.subscriptionIdParameter.SubscriptionIdentity != null && !this.subscriptionIdParameter.SubscriptionIdentity.Equals(subscription.SubscriptionIdentity))
			{
				CommonLoggingHelper.SyncLogSession.LogVerbose((TSLID)1252UL, "Match: Ignoring subscription as identity {0} does not match desired value: {1}.", new object[]
				{
					subscription.SubscriptionIdentity,
					this.subscriptionIdParameter.SubscriptionIdentity
				});
				return false;
			}
			if ((subscription.SubscriptionType & this.subscriptionIdParameter.SubscriptionType) == AggregationSubscriptionType.Unknown)
			{
				CommonLoggingHelper.SyncLogSession.LogVerbose((TSLID)1253UL, "Match: Ignoring subscription as type {0} does not match desired value: {1}.", new object[]
				{
					subscription.SubscriptionType,
					this.subscriptionIdParameter.SubscriptionType
				});
				return false;
			}
			if (this.subscriptionIdParameter.AggregationType != null && this.subscriptionIdParameter.AggregationType != AggregationType.All && subscription.AggregationType != this.subscriptionIdParameter.AggregationType)
			{
				CommonLoggingHelper.SyncLogSession.LogVerbose((TSLID)1254UL, "Match: Ignoring subscription as aggregation type {0} does not match desired value: {1}.", new object[]
				{
					subscription.AggregationType,
					this.subscriptionIdParameter.AggregationType
				});
				return false;
			}
			if (!string.IsNullOrEmpty(this.subscriptionIdParameter.Name) && !this.subscriptionIdParameter.Name.Equals(subscription.Name, StringComparison.OrdinalIgnoreCase))
			{
				CommonLoggingHelper.SyncLogSession.LogVerbose((TSLID)1255UL, "Match: Ignoring subscription as name {0} does not match desired value: {1}.", new object[]
				{
					subscription.Name,
					this.subscriptionIdParameter.Name
				});
				return false;
			}
			PimAggregationSubscription pimAggregationSubscription = subscription as PimAggregationSubscription;
			if (pimAggregationSubscription != null && !this.subscriptionIdParameter.EmailAddress.Equals(SmtpAddress.Empty) && !this.subscriptionIdParameter.EmailAddress.Equals(pimAggregationSubscription.UserEmailAddress))
			{
				CommonLoggingHelper.SyncLogSession.LogVerbose((TSLID)1256UL, "Match: Ignoring subscription as UserEmailAddress {0} does not match desired value: {1}.", new object[]
				{
					pimAggregationSubscription.UserEmailAddress,
					this.subscriptionIdParameter.EmailAddress
				});
				return false;
			}
			if (!this.subscriptionIdParameter.SubscriptionId.Equals(Guid.Empty) && !this.subscriptionIdParameter.SubscriptionId.Equals(subscription.SubscriptionGuid))
			{
				CommonLoggingHelper.SyncLogSession.LogVerbose((TSLID)1257UL, "Match: Ignoring subscription as subscriptionGuid {0} does not match desired value: {1}.", new object[]
				{
					subscription.SubscriptionGuid,
					this.subscriptionIdParameter.SubscriptionId
				});
				return false;
			}
			return true;
		}

		// Token: 0x04003E36 RID: 15926
		private AggregationSubscriptionIdParameter subscriptionIdParameter;
	}
}
