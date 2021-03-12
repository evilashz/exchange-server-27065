using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim
{
	// Token: 0x020000EB RID: 235
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PimSubscriptionValidator
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x00020F2C File Offset: 0x0001F12C
		public static ICollection<ValidationError> Validate(PimSubscriptionProxy subscription)
		{
			List<ValidationError> list = new List<ValidationError>();
			ValidationError validationError = AggregationSubscriptionConstraints.NameLengthConstraint.Validate(subscription.Name, new SubscriptionProxyPropertyDefinition("Name", typeof(string)), null);
			if (validationError != null)
			{
				list.Add(validationError);
			}
			ValidationError validationError2 = AggregationSubscriptionConstraints.NameLengthConstraint.Validate(subscription.DisplayName, new SubscriptionProxyPropertyDefinition("DisplayName", typeof(string)), null);
			if (validationError2 != null)
			{
				list.Add(validationError2);
			}
			return list;
		}
	}
}
