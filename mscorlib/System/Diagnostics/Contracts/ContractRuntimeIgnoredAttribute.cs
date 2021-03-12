using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003E2 RID: 994
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[__DynamicallyInvokable]
	public sealed class ContractRuntimeIgnoredAttribute : Attribute
	{
		// Token: 0x060032D4 RID: 13012 RVA: 0x000C1CAD File Offset: 0x000BFEAD
		[__DynamicallyInvokable]
		public ContractRuntimeIgnoredAttribute()
		{
		}
	}
}
