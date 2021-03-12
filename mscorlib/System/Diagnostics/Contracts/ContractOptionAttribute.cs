using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020003E7 RID: 999
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Conditional("CONTRACTS_FULL")]
	[__DynamicallyInvokable]
	public sealed class ContractOptionAttribute : Attribute
	{
		// Token: 0x060032DB RID: 13019 RVA: 0x000C1CF3 File Offset: 0x000BFEF3
		[__DynamicallyInvokable]
		public ContractOptionAttribute(string category, string setting, bool enabled)
		{
			this._category = category;
			this._setting = setting;
			this._enabled = enabled;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000C1D10 File Offset: 0x000BFF10
		[__DynamicallyInvokable]
		public ContractOptionAttribute(string category, string setting, string value)
		{
			this._category = category;
			this._setting = setting;
			this._value = value;
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000C1D2D File Offset: 0x000BFF2D
		[__DynamicallyInvokable]
		public string Category
		{
			[__DynamicallyInvokable]
			get
			{
				return this._category;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x000C1D35 File Offset: 0x000BFF35
		[__DynamicallyInvokable]
		public string Setting
		{
			[__DynamicallyInvokable]
			get
			{
				return this._setting;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000C1D3D File Offset: 0x000BFF3D
		[__DynamicallyInvokable]
		public bool Enabled
		{
			[__DynamicallyInvokable]
			get
			{
				return this._enabled;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x000C1D45 File Offset: 0x000BFF45
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001650 RID: 5712
		private string _category;

		// Token: 0x04001651 RID: 5713
		private string _setting;

		// Token: 0x04001652 RID: 5714
		private bool _enabled;

		// Token: 0x04001653 RID: 5715
		private string _value;
	}
}
