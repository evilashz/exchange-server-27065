using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BF5 RID: 3061
	public class DisableDistributionGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x0600948B RID: 38027 RVA: 0x000D883F File Offset: 0x000D6A3F
		private DisableDistributionGroupCommand() : base("Disable-DistributionGroup")
		{
		}

		// Token: 0x0600948C RID: 38028 RVA: 0x000D884C File Offset: 0x000D6A4C
		public DisableDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600948D RID: 38029 RVA: 0x000D885B File Offset: 0x000D6A5B
		public virtual DisableDistributionGroupCommand SetParameters(DisableDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600948E RID: 38030 RVA: 0x000D8865 File Offset: 0x000D6A65
		public virtual DisableDistributionGroupCommand SetParameters(DisableDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BF6 RID: 3062
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700685A RID: 26714
			// (set) Token: 0x0600948F RID: 38031 RVA: 0x000D886F File Offset: 0x000D6A6F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700685B RID: 26715
			// (set) Token: 0x06009490 RID: 38032 RVA: 0x000D8887 File Offset: 0x000D6A87
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700685C RID: 26716
			// (set) Token: 0x06009491 RID: 38033 RVA: 0x000D889A File Offset: 0x000D6A9A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700685D RID: 26717
			// (set) Token: 0x06009492 RID: 38034 RVA: 0x000D88B2 File Offset: 0x000D6AB2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700685E RID: 26718
			// (set) Token: 0x06009493 RID: 38035 RVA: 0x000D88CA File Offset: 0x000D6ACA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700685F RID: 26719
			// (set) Token: 0x06009494 RID: 38036 RVA: 0x000D88E2 File Offset: 0x000D6AE2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006860 RID: 26720
			// (set) Token: 0x06009495 RID: 38037 RVA: 0x000D88FA File Offset: 0x000D6AFA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006861 RID: 26721
			// (set) Token: 0x06009496 RID: 38038 RVA: 0x000D8912 File Offset: 0x000D6B12
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BF7 RID: 3063
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006862 RID: 26722
			// (set) Token: 0x06009498 RID: 38040 RVA: 0x000D8932 File Offset: 0x000D6B32
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006863 RID: 26723
			// (set) Token: 0x06009499 RID: 38041 RVA: 0x000D8950 File Offset: 0x000D6B50
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006864 RID: 26724
			// (set) Token: 0x0600949A RID: 38042 RVA: 0x000D8968 File Offset: 0x000D6B68
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006865 RID: 26725
			// (set) Token: 0x0600949B RID: 38043 RVA: 0x000D897B File Offset: 0x000D6B7B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006866 RID: 26726
			// (set) Token: 0x0600949C RID: 38044 RVA: 0x000D8993 File Offset: 0x000D6B93
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006867 RID: 26727
			// (set) Token: 0x0600949D RID: 38045 RVA: 0x000D89AB File Offset: 0x000D6BAB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006868 RID: 26728
			// (set) Token: 0x0600949E RID: 38046 RVA: 0x000D89C3 File Offset: 0x000D6BC3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006869 RID: 26729
			// (set) Token: 0x0600949F RID: 38047 RVA: 0x000D89DB File Offset: 0x000D6BDB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700686A RID: 26730
			// (set) Token: 0x060094A0 RID: 38048 RVA: 0x000D89F3 File Offset: 0x000D6BF3
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
