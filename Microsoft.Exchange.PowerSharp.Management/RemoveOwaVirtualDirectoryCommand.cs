using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200096B RID: 2411
	public class RemoveOwaVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADOwaVirtualDirectory, ADOwaVirtualDirectory>
	{
		// Token: 0x060078A7 RID: 30887 RVA: 0x000B4529 File Offset: 0x000B2729
		private RemoveOwaVirtualDirectoryCommand() : base("Remove-OwaVirtualDirectory")
		{
		}

		// Token: 0x060078A8 RID: 30888 RVA: 0x000B4536 File Offset: 0x000B2736
		public RemoveOwaVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060078A9 RID: 30889 RVA: 0x000B4545 File Offset: 0x000B2745
		public virtual RemoveOwaVirtualDirectoryCommand SetParameters(RemoveOwaVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060078AA RID: 30890 RVA: 0x000B454F File Offset: 0x000B274F
		public virtual RemoveOwaVirtualDirectoryCommand SetParameters(RemoveOwaVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200096C RID: 2412
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700518A RID: 20874
			// (set) Token: 0x060078AB RID: 30891 RVA: 0x000B4559 File Offset: 0x000B2759
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700518B RID: 20875
			// (set) Token: 0x060078AC RID: 30892 RVA: 0x000B456C File Offset: 0x000B276C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700518C RID: 20876
			// (set) Token: 0x060078AD RID: 30893 RVA: 0x000B4584 File Offset: 0x000B2784
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700518D RID: 20877
			// (set) Token: 0x060078AE RID: 30894 RVA: 0x000B459C File Offset: 0x000B279C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700518E RID: 20878
			// (set) Token: 0x060078AF RID: 30895 RVA: 0x000B45B4 File Offset: 0x000B27B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700518F RID: 20879
			// (set) Token: 0x060078B0 RID: 30896 RVA: 0x000B45CC File Offset: 0x000B27CC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005190 RID: 20880
			// (set) Token: 0x060078B1 RID: 30897 RVA: 0x000B45E4 File Offset: 0x000B27E4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200096D RID: 2413
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005191 RID: 20881
			// (set) Token: 0x060078B3 RID: 30899 RVA: 0x000B4604 File Offset: 0x000B2804
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005192 RID: 20882
			// (set) Token: 0x060078B4 RID: 30900 RVA: 0x000B4617 File Offset: 0x000B2817
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005193 RID: 20883
			// (set) Token: 0x060078B5 RID: 30901 RVA: 0x000B462A File Offset: 0x000B282A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005194 RID: 20884
			// (set) Token: 0x060078B6 RID: 30902 RVA: 0x000B4642 File Offset: 0x000B2842
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005195 RID: 20885
			// (set) Token: 0x060078B7 RID: 30903 RVA: 0x000B465A File Offset: 0x000B285A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005196 RID: 20886
			// (set) Token: 0x060078B8 RID: 30904 RVA: 0x000B4672 File Offset: 0x000B2872
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005197 RID: 20887
			// (set) Token: 0x060078B9 RID: 30905 RVA: 0x000B468A File Offset: 0x000B288A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005198 RID: 20888
			// (set) Token: 0x060078BA RID: 30906 RVA: 0x000B46A2 File Offset: 0x000B28A2
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
