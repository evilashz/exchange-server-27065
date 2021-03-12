using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003E3 RID: 995
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	[__DynamicallyInvokable]
	public sealed class ContractVerificationAttribute : Attribute
	{
		// Token: 0x060032D5 RID: 13013 RVA: 0x000C1CB5 File Offset: 0x000BFEB5
		[__DynamicallyInvokable]
		public ContractVerificationAttribute(bool value)
		{
			this._value = value;
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000C1CC4 File Offset: 0x000BFEC4
		[__DynamicallyInvokable]
		public bool Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
		}

		// Token: 0x0400164E RID: 5710
		private bool _value;
	}
}
