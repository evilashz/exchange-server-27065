using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003E0 RID: 992
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ContractInvariantMethodAttribute : Attribute
	{
		// Token: 0x060032D2 RID: 13010 RVA: 0x000C1C9D File Offset: 0x000BFE9D
		[__DynamicallyInvokable]
		public ContractInvariantMethodAttribute()
		{
		}
	}
}
