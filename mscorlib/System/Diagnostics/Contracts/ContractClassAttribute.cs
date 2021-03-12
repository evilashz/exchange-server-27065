using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003DE RID: 990
	[Conditional("CONTRACTS_FULL")]
	[Conditional("DEBUG")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ContractClassAttribute : Attribute
	{
		// Token: 0x060032CE RID: 13006 RVA: 0x000C1C6F File Offset: 0x000BFE6F
		[__DynamicallyInvokable]
		public ContractClassAttribute(Type typeContainingContracts)
		{
			this._typeWithContracts = typeContainingContracts;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000C1C7E File Offset: 0x000BFE7E
		[__DynamicallyInvokable]
		public Type TypeContainingContracts
		{
			[__DynamicallyInvokable]
			get
			{
				return this._typeWithContracts;
			}
		}

		// Token: 0x0400164C RID: 5708
		private Type _typeWithContracts;
	}
}
