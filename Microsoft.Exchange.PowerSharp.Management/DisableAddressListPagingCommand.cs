using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000812 RID: 2066
	public class DisableAddressListPagingCommand : SyntheticCommand<object>
	{
		// Token: 0x06006625 RID: 26149 RVA: 0x0009BD99 File Offset: 0x00099F99
		private DisableAddressListPagingCommand() : base("Disable-AddressListPaging")
		{
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x0009BDA6 File Offset: 0x00099FA6
		public DisableAddressListPagingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x0009BDB5 File Offset: 0x00099FB5
		public virtual DisableAddressListPagingCommand SetParameters(DisableAddressListPagingCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x0009BDBF File Offset: 0x00099FBF
		public virtual DisableAddressListPagingCommand SetParameters(DisableAddressListPagingCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000813 RID: 2067
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170041BA RID: 16826
			// (set) Token: 0x06006629 RID: 26153 RVA: 0x0009BDC9 File Offset: 0x00099FC9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170041BB RID: 16827
			// (set) Token: 0x0600662A RID: 26154 RVA: 0x0009BDE7 File Offset: 0x00099FE7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041BC RID: 16828
			// (set) Token: 0x0600662B RID: 26155 RVA: 0x0009BDFA File Offset: 0x00099FFA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041BD RID: 16829
			// (set) Token: 0x0600662C RID: 26156 RVA: 0x0009BE12 File Offset: 0x0009A012
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041BE RID: 16830
			// (set) Token: 0x0600662D RID: 26157 RVA: 0x0009BE2A File Offset: 0x0009A02A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041BF RID: 16831
			// (set) Token: 0x0600662E RID: 26158 RVA: 0x0009BE42 File Offset: 0x0009A042
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170041C0 RID: 16832
			// (set) Token: 0x0600662F RID: 26159 RVA: 0x0009BE5A File Offset: 0x0009A05A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170041C1 RID: 16833
			// (set) Token: 0x06006630 RID: 26160 RVA: 0x0009BE72 File Offset: 0x0009A072
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000814 RID: 2068
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170041C2 RID: 16834
			// (set) Token: 0x06006632 RID: 26162 RVA: 0x0009BE92 File Offset: 0x0009A092
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041C3 RID: 16835
			// (set) Token: 0x06006633 RID: 26163 RVA: 0x0009BEA5 File Offset: 0x0009A0A5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041C4 RID: 16836
			// (set) Token: 0x06006634 RID: 26164 RVA: 0x0009BEBD File Offset: 0x0009A0BD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041C5 RID: 16837
			// (set) Token: 0x06006635 RID: 26165 RVA: 0x0009BED5 File Offset: 0x0009A0D5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041C6 RID: 16838
			// (set) Token: 0x06006636 RID: 26166 RVA: 0x0009BEED File Offset: 0x0009A0ED
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170041C7 RID: 16839
			// (set) Token: 0x06006637 RID: 26167 RVA: 0x0009BF05 File Offset: 0x0009A105
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170041C8 RID: 16840
			// (set) Token: 0x06006638 RID: 26168 RVA: 0x0009BF1D File Offset: 0x0009A11D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
