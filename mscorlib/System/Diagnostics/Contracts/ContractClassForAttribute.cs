using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003DF RID: 991
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ContractClassForAttribute : Attribute
	{
		// Token: 0x060032D0 RID: 13008 RVA: 0x000C1C86 File Offset: 0x000BFE86
		[__DynamicallyInvokable]
		public ContractClassForAttribute(Type typeContractsAreFor)
		{
			this._typeIAmAContractFor = typeContractsAreFor;
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x000C1C95 File Offset: 0x000BFE95
		[__DynamicallyInvokable]
		public Type TypeContractsAreFor
		{
			[__DynamicallyInvokable]
			get
			{
				return this._typeIAmAContractFor;
			}
		}

		// Token: 0x0400164D RID: 5709
		private Type _typeIAmAContractFor;
	}
}
