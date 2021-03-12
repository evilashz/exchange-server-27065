using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000815 RID: 2069
	public class EnableAddressListPagingCommand : SyntheticCommand<object>
	{
		// Token: 0x0600663A RID: 26170 RVA: 0x0009BF3D File Offset: 0x0009A13D
		private EnableAddressListPagingCommand() : base("Enable-AddressListPaging")
		{
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x0009BF4A File Offset: 0x0009A14A
		public EnableAddressListPagingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x0009BF59 File Offset: 0x0009A159
		public virtual EnableAddressListPagingCommand SetParameters(EnableAddressListPagingCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x0009BF63 File Offset: 0x0009A163
		public virtual EnableAddressListPagingCommand SetParameters(EnableAddressListPagingCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000816 RID: 2070
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170041C9 RID: 16841
			// (set) Token: 0x0600663E RID: 26174 RVA: 0x0009BF6D File Offset: 0x0009A16D
			public virtual SwitchParameter DoNotUpdateRecipients
			{
				set
				{
					base.PowerSharpParameters["DoNotUpdateRecipients"] = value;
				}
			}

			// Token: 0x170041CA RID: 16842
			// (set) Token: 0x0600663F RID: 26175 RVA: 0x0009BF85 File Offset: 0x0009A185
			public virtual SwitchParameter ForceUpdateOfRecipients
			{
				set
				{
					base.PowerSharpParameters["ForceUpdateOfRecipients"] = value;
				}
			}

			// Token: 0x170041CB RID: 16843
			// (set) Token: 0x06006640 RID: 26176 RVA: 0x0009BF9D File Offset: 0x0009A19D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041CC RID: 16844
			// (set) Token: 0x06006641 RID: 26177 RVA: 0x0009BFB0 File Offset: 0x0009A1B0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041CD RID: 16845
			// (set) Token: 0x06006642 RID: 26178 RVA: 0x0009BFC8 File Offset: 0x0009A1C8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041CE RID: 16846
			// (set) Token: 0x06006643 RID: 26179 RVA: 0x0009BFE0 File Offset: 0x0009A1E0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041CF RID: 16847
			// (set) Token: 0x06006644 RID: 26180 RVA: 0x0009BFF8 File Offset: 0x0009A1F8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170041D0 RID: 16848
			// (set) Token: 0x06006645 RID: 26181 RVA: 0x0009C010 File Offset: 0x0009A210
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000817 RID: 2071
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170041D1 RID: 16849
			// (set) Token: 0x06006647 RID: 26183 RVA: 0x0009C030 File Offset: 0x0009A230
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170041D2 RID: 16850
			// (set) Token: 0x06006648 RID: 26184 RVA: 0x0009C04E File Offset: 0x0009A24E
			public virtual SwitchParameter DoNotUpdateRecipients
			{
				set
				{
					base.PowerSharpParameters["DoNotUpdateRecipients"] = value;
				}
			}

			// Token: 0x170041D3 RID: 16851
			// (set) Token: 0x06006649 RID: 26185 RVA: 0x0009C066 File Offset: 0x0009A266
			public virtual SwitchParameter ForceUpdateOfRecipients
			{
				set
				{
					base.PowerSharpParameters["ForceUpdateOfRecipients"] = value;
				}
			}

			// Token: 0x170041D4 RID: 16852
			// (set) Token: 0x0600664A RID: 26186 RVA: 0x0009C07E File Offset: 0x0009A27E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041D5 RID: 16853
			// (set) Token: 0x0600664B RID: 26187 RVA: 0x0009C091 File Offset: 0x0009A291
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041D6 RID: 16854
			// (set) Token: 0x0600664C RID: 26188 RVA: 0x0009C0A9 File Offset: 0x0009A2A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041D7 RID: 16855
			// (set) Token: 0x0600664D RID: 26189 RVA: 0x0009C0C1 File Offset: 0x0009A2C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041D8 RID: 16856
			// (set) Token: 0x0600664E RID: 26190 RVA: 0x0009C0D9 File Offset: 0x0009A2D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170041D9 RID: 16857
			// (set) Token: 0x0600664F RID: 26191 RVA: 0x0009C0F1 File Offset: 0x0009A2F1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
