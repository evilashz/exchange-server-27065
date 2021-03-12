using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap
{
	// Token: 0x020000E7 RID: 231
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IMAPSubscriptionValidator
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x00020E64 File Offset: 0x0001F064
		public static ICollection<ValidationError> Validate(IMAPSubscriptionProxy subscription)
		{
			List<ValidationError> list = new List<ValidationError>(1);
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
