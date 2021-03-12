using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003E9 RID: 1001
	[__DynamicallyInvokable]
	public enum ContractFailureKind
	{
		// Token: 0x04001656 RID: 5718
		[__DynamicallyInvokable]
		Precondition,
		// Token: 0x04001657 RID: 5719
		[__DynamicallyInvokable]
		Postcondition,
		// Token: 0x04001658 RID: 5720
		[__DynamicallyInvokable]
		PostconditionOnException,
		// Token: 0x04001659 RID: 5721
		[__DynamicallyInvokable]
		Invariant,
		// Token: 0x0400165A RID: 5722
		[__DynamicallyInvokable]
		Assert,
		// Token: 0x0400165B RID: 5723
		[__DynamicallyInvokable]
		Assume
	}
}
