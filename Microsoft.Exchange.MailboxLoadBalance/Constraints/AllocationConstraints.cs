using System;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x02000038 RID: 56
	internal static class AllocationConstraints
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00007ADE File Offset: 0x00005CDE
		public static IAllocationConstraint Or(params IAllocationConstraint[] constraints)
		{
			return new AnyAcceptConstraint(constraints);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007AE6 File Offset: 0x00005CE6
		public static IAllocationConstraint And(params IAllocationConstraint[] constraints)
		{
			return new AllAcceptConstraint(constraints);
		}
	}
}
