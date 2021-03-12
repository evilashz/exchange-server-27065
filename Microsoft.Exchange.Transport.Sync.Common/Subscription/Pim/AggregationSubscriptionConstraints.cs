using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim
{
	// Token: 0x020000E8 RID: 232
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AggregationSubscriptionConstraints
	{
		// Token: 0x040003BE RID: 958
		public static readonly StringLengthConstraint NameLengthConstraint = new StringLengthConstraint(1, 256);

		// Token: 0x040003BF RID: 959
		public static readonly RangedValueConstraint<int> PortRangeConstraint = new RangedValueConstraint<int>(1, 65535);

		// Token: 0x040003C0 RID: 960
		public static readonly RangedValueConstraint<int> PasswordRangeConstraint = new RangedValueConstraint<int>(1, 256);
	}
}
