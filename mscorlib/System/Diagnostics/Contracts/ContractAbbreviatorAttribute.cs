using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003E6 RID: 998
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	[__DynamicallyInvokable]
	public sealed class ContractAbbreviatorAttribute : Attribute
	{
		// Token: 0x060032DA RID: 13018 RVA: 0x000C1CEB File Offset: 0x000BFEEB
		[__DynamicallyInvokable]
		public ContractAbbreviatorAttribute()
		{
		}
	}
}
