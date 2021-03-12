using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SubscriptionConfigDataProviderFactory
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000BAF6 File Offset: 0x00009CF6
		private SubscriptionConfigDataProviderFactory()
		{
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000BAFE File Offset: 0x00009CFE
		public static SubscriptionConfigDataProviderFactory Instance
		{
			get
			{
				return SubscriptionConfigDataProviderFactory.instance;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BB08 File Offset: 0x00009D08
		public AggregationSubscriptionDataProvider CreateSubscriptionDataProvider(AggregationType aggregationType, AggregationTaskType taskType, IRecipientSession session, ADUser adUser)
		{
			if (aggregationType <= AggregationType.Migration)
			{
				if (aggregationType != AggregationType.Aggregation && aggregationType != AggregationType.Migration)
				{
					goto IL_33;
				}
			}
			else
			{
				if (aggregationType == AggregationType.PeopleConnection)
				{
					return new ConnectSubscriptionDataProvider(taskType, session, adUser);
				}
				if (aggregationType != AggregationType.All)
				{
					goto IL_33;
				}
			}
			return new AggregationSubscriptionDataProvider(taskType, session, adUser);
			IL_33:
			throw new InvalidOperationException("Invalid Aggregation Type:" + aggregationType);
		}

		// Token: 0x040000AF RID: 175
		private static SubscriptionConfigDataProviderFactory instance = new SubscriptionConfigDataProviderFactory();
	}
}
