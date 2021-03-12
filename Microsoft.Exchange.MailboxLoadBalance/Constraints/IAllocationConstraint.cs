using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IAllocationConstraint
	{
		// Token: 0x06000228 RID: 552
		ConstraintValidationResult Accept(LoadEntity entity);

		// Token: 0x06000229 RID: 553
		void ValidateAccepted(LoadEntity entity);

		// Token: 0x0600022A RID: 554
		IAllocationConstraint CloneForContainer(LoadContainer container);
	}
}
