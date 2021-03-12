using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop
{
	// Token: 0x020000F1 RID: 241
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PopSubscriptionValidator
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0002138C File Offset: 0x0001F58C
		public static ICollection<ValidationError> Validate(PopSubscriptionProxy subscription)
		{
			List<ValidationError> list = new List<ValidationError>();
			list.AddRange(PimSubscriptionValidator.Validate(subscription));
			ValidationError validationError = AggregationSubscriptionConstraints.NameLengthConstraint.Validate(subscription.IncomingUserName, new SubscriptionProxyPropertyDefinition("IncomingUserName", typeof(string)), null);
			if (validationError != null)
			{
				list.Add(validationError);
			}
			ValidationError validationError2 = AggregationSubscriptionConstraints.PortRangeConstraint.Validate(subscription.IncomingPort, new SubscriptionProxyPropertyDefinition("IncomingPort", typeof(int)), null);
			if (validationError2 != null)
			{
				list.Add(validationError2);
			}
			return list;
		}
	}
}
