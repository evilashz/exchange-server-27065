using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000808 RID: 2056
	public class RemoveOnPremisesOrganizationCommand : SyntheticCommandWithPipelineInput<OnPremisesOrganization, OnPremisesOrganization>
	{
		// Token: 0x060065D8 RID: 26072 RVA: 0x0009B7D7 File Offset: 0x000999D7
		private RemoveOnPremisesOrganizationCommand() : base("Remove-OnPremisesOrganization")
		{
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x0009B7E4 File Offset: 0x000999E4
		public RemoveOnPremisesOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x0009B7F3 File Offset: 0x000999F3
		public virtual RemoveOnPremisesOrganizationCommand SetParameters(RemoveOnPremisesOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060065DB RID: 26075 RVA: 0x0009B7FD File Offset: 0x000999FD
		public virtual RemoveOnPremisesOrganizationCommand SetParameters(RemoveOnPremisesOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000809 RID: 2057
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004181 RID: 16769
			// (set) Token: 0x060065DC RID: 26076 RVA: 0x0009B807 File Offset: 0x00099A07
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004182 RID: 16770
			// (set) Token: 0x060065DD RID: 26077 RVA: 0x0009B81A File Offset: 0x00099A1A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004183 RID: 16771
			// (set) Token: 0x060065DE RID: 26078 RVA: 0x0009B832 File Offset: 0x00099A32
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004184 RID: 16772
			// (set) Token: 0x060065DF RID: 26079 RVA: 0x0009B84A File Offset: 0x00099A4A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004185 RID: 16773
			// (set) Token: 0x060065E0 RID: 26080 RVA: 0x0009B862 File Offset: 0x00099A62
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004186 RID: 16774
			// (set) Token: 0x060065E1 RID: 26081 RVA: 0x0009B87A File Offset: 0x00099A7A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004187 RID: 16775
			// (set) Token: 0x060065E2 RID: 26082 RVA: 0x0009B892 File Offset: 0x00099A92
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200080A RID: 2058
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004188 RID: 16776
			// (set) Token: 0x060065E4 RID: 26084 RVA: 0x0009B8B2 File Offset: 0x00099AB2
			public virtual OnPremisesOrganizationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004189 RID: 16777
			// (set) Token: 0x060065E5 RID: 26085 RVA: 0x0009B8C5 File Offset: 0x00099AC5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700418A RID: 16778
			// (set) Token: 0x060065E6 RID: 26086 RVA: 0x0009B8D8 File Offset: 0x00099AD8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700418B RID: 16779
			// (set) Token: 0x060065E7 RID: 26087 RVA: 0x0009B8F0 File Offset: 0x00099AF0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700418C RID: 16780
			// (set) Token: 0x060065E8 RID: 26088 RVA: 0x0009B908 File Offset: 0x00099B08
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700418D RID: 16781
			// (set) Token: 0x060065E9 RID: 26089 RVA: 0x0009B920 File Offset: 0x00099B20
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700418E RID: 16782
			// (set) Token: 0x060065EA RID: 26090 RVA: 0x0009B938 File Offset: 0x00099B38
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700418F RID: 16783
			// (set) Token: 0x060065EB RID: 26091 RVA: 0x0009B950 File Offset: 0x00099B50
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
