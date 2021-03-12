using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BAD RID: 2989
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FromSubscriptionCondition : GuidCondition
	{
		// Token: 0x06006ADF RID: 27359 RVA: 0x001C7F64 File Offset: 0x001C6164
		private FromSubscriptionCondition(Rule rule, Guid[] subscriptionGuids) : base(ConditionType.FromSubscriptionCondition, rule, subscriptionGuids)
		{
		}

		// Token: 0x06006AE0 RID: 27360 RVA: 0x001C7F70 File Offset: 0x001C6170
		public static FromSubscriptionCondition Create(Rule rule, params Guid[] subscriptionGuids)
		{
			return new FromSubscriptionCondition(rule, subscriptionGuids);
		}

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x06006AE1 RID: 27361 RVA: 0x001C7F79 File Offset: 0x001C6179
		public override Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.Exchange14;
			}
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x001C7F7C File Offset: 0x001C617C
		internal override Restriction BuildRestriction()
		{
			PropTag propertyTag = base.Rule.PropertyDefinitionToPropTagFromCache(InternalSchema.SharingInstanceGuid);
			return Condition.CreateORGuidContentRestriction(base.Guids, propertyTag);
		}
	}
}
