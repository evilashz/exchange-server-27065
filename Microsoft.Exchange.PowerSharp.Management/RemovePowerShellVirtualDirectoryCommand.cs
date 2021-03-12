using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200096E RID: 2414
	public class RemovePowerShellVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPowerShellVirtualDirectory, ADPowerShellVirtualDirectory>
	{
		// Token: 0x060078BC RID: 30908 RVA: 0x000B46C2 File Offset: 0x000B28C2
		private RemovePowerShellVirtualDirectoryCommand() : base("Remove-PowerShellVirtualDirectory")
		{
		}

		// Token: 0x060078BD RID: 30909 RVA: 0x000B46CF File Offset: 0x000B28CF
		public RemovePowerShellVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060078BE RID: 30910 RVA: 0x000B46DE File Offset: 0x000B28DE
		public virtual RemovePowerShellVirtualDirectoryCommand SetParameters(RemovePowerShellVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x000B46E8 File Offset: 0x000B28E8
		public virtual RemovePowerShellVirtualDirectoryCommand SetParameters(RemovePowerShellVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200096F RID: 2415
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005199 RID: 20889
			// (set) Token: 0x060078C0 RID: 30912 RVA: 0x000B46F2 File Offset: 0x000B28F2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700519A RID: 20890
			// (set) Token: 0x060078C1 RID: 30913 RVA: 0x000B4705 File Offset: 0x000B2905
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700519B RID: 20891
			// (set) Token: 0x060078C2 RID: 30914 RVA: 0x000B471D File Offset: 0x000B291D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700519C RID: 20892
			// (set) Token: 0x060078C3 RID: 30915 RVA: 0x000B4735 File Offset: 0x000B2935
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700519D RID: 20893
			// (set) Token: 0x060078C4 RID: 30916 RVA: 0x000B474D File Offset: 0x000B294D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700519E RID: 20894
			// (set) Token: 0x060078C5 RID: 30917 RVA: 0x000B4765 File Offset: 0x000B2965
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700519F RID: 20895
			// (set) Token: 0x060078C6 RID: 30918 RVA: 0x000B477D File Offset: 0x000B297D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000970 RID: 2416
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170051A0 RID: 20896
			// (set) Token: 0x060078C8 RID: 30920 RVA: 0x000B479D File Offset: 0x000B299D
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170051A1 RID: 20897
			// (set) Token: 0x060078C9 RID: 30921 RVA: 0x000B47B0 File Offset: 0x000B29B0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051A2 RID: 20898
			// (set) Token: 0x060078CA RID: 30922 RVA: 0x000B47C3 File Offset: 0x000B29C3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051A3 RID: 20899
			// (set) Token: 0x060078CB RID: 30923 RVA: 0x000B47DB File Offset: 0x000B29DB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051A4 RID: 20900
			// (set) Token: 0x060078CC RID: 30924 RVA: 0x000B47F3 File Offset: 0x000B29F3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051A5 RID: 20901
			// (set) Token: 0x060078CD RID: 30925 RVA: 0x000B480B File Offset: 0x000B2A0B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051A6 RID: 20902
			// (set) Token: 0x060078CE RID: 30926 RVA: 0x000B4823 File Offset: 0x000B2A23
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170051A7 RID: 20903
			// (set) Token: 0x060078CF RID: 30927 RVA: 0x000B483B File Offset: 0x000B2A3B
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
