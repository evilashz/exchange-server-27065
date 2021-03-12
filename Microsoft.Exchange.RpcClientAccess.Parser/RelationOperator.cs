using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000200 RID: 512
	internal enum RelationOperator : uint
	{
		// Token: 0x0400064E RID: 1614
		LessThan,
		// Token: 0x0400064F RID: 1615
		LessThanEquals,
		// Token: 0x04000650 RID: 1616
		GreaterThan,
		// Token: 0x04000651 RID: 1617
		GreaterThanEquals,
		// Token: 0x04000652 RID: 1618
		Equals,
		// Token: 0x04000653 RID: 1619
		NotEquals,
		// Token: 0x04000654 RID: 1620
		RegularExpression,
		// Token: 0x04000655 RID: 1621
		MemberOfDistributionList = 100U
	}
}
