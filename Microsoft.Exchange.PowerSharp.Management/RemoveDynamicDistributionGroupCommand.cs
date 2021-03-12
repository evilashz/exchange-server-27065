using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C12 RID: 3090
	public class RemoveDynamicDistributionGroupCommand : SyntheticCommandWithPipelineInput<ADDynamicGroup, ADDynamicGroup>
	{
		// Token: 0x06009617 RID: 38423 RVA: 0x000DA918 File Offset: 0x000D8B18
		private RemoveDynamicDistributionGroupCommand() : base("Remove-DynamicDistributionGroup")
		{
		}

		// Token: 0x06009618 RID: 38424 RVA: 0x000DA925 File Offset: 0x000D8B25
		public RemoveDynamicDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009619 RID: 38425 RVA: 0x000DA934 File Offset: 0x000D8B34
		public virtual RemoveDynamicDistributionGroupCommand SetParameters(RemoveDynamicDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600961A RID: 38426 RVA: 0x000DA93E File Offset: 0x000D8B3E
		public virtual RemoveDynamicDistributionGroupCommand SetParameters(RemoveDynamicDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C13 RID: 3091
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170069AC RID: 27052
			// (set) Token: 0x0600961B RID: 38427 RVA: 0x000DA948 File Offset: 0x000D8B48
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170069AD RID: 27053
			// (set) Token: 0x0600961C RID: 38428 RVA: 0x000DA960 File Offset: 0x000D8B60
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170069AE RID: 27054
			// (set) Token: 0x0600961D RID: 38429 RVA: 0x000DA973 File Offset: 0x000D8B73
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170069AF RID: 27055
			// (set) Token: 0x0600961E RID: 38430 RVA: 0x000DA98B File Offset: 0x000D8B8B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170069B0 RID: 27056
			// (set) Token: 0x0600961F RID: 38431 RVA: 0x000DA9A3 File Offset: 0x000D8BA3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170069B1 RID: 27057
			// (set) Token: 0x06009620 RID: 38432 RVA: 0x000DA9BB File Offset: 0x000D8BBB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170069B2 RID: 27058
			// (set) Token: 0x06009621 RID: 38433 RVA: 0x000DA9D3 File Offset: 0x000D8BD3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170069B3 RID: 27059
			// (set) Token: 0x06009622 RID: 38434 RVA: 0x000DA9EB File Offset: 0x000D8BEB
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C14 RID: 3092
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170069B4 RID: 27060
			// (set) Token: 0x06009624 RID: 38436 RVA: 0x000DAA0B File Offset: 0x000D8C0B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DynamicGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170069B5 RID: 27061
			// (set) Token: 0x06009625 RID: 38437 RVA: 0x000DAA29 File Offset: 0x000D8C29
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170069B6 RID: 27062
			// (set) Token: 0x06009626 RID: 38438 RVA: 0x000DAA41 File Offset: 0x000D8C41
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170069B7 RID: 27063
			// (set) Token: 0x06009627 RID: 38439 RVA: 0x000DAA54 File Offset: 0x000D8C54
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170069B8 RID: 27064
			// (set) Token: 0x06009628 RID: 38440 RVA: 0x000DAA6C File Offset: 0x000D8C6C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170069B9 RID: 27065
			// (set) Token: 0x06009629 RID: 38441 RVA: 0x000DAA84 File Offset: 0x000D8C84
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170069BA RID: 27066
			// (set) Token: 0x0600962A RID: 38442 RVA: 0x000DAA9C File Offset: 0x000D8C9C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170069BB RID: 27067
			// (set) Token: 0x0600962B RID: 38443 RVA: 0x000DAAB4 File Offset: 0x000D8CB4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170069BC RID: 27068
			// (set) Token: 0x0600962C RID: 38444 RVA: 0x000DAACC File Offset: 0x000D8CCC
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
