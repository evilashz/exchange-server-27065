using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003E5 RID: 997
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	[__DynamicallyInvokable]
	public sealed class ContractArgumentValidatorAttribute : Attribute
	{
		// Token: 0x060032D9 RID: 13017 RVA: 0x000C1CE3 File Offset: 0x000BFEE3
		[__DynamicallyInvokable]
		public ContractArgumentValidatorAttribute()
		{
		}
	}
}
