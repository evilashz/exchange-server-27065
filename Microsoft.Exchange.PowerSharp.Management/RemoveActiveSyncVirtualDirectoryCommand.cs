using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000962 RID: 2402
	public class RemoveActiveSyncVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADMobileVirtualDirectory, ADMobileVirtualDirectory>
	{
		// Token: 0x06007866 RID: 30822 RVA: 0x000B402E File Offset: 0x000B222E
		private RemoveActiveSyncVirtualDirectoryCommand() : base("Remove-ActiveSyncVirtualDirectory")
		{
		}

		// Token: 0x06007867 RID: 30823 RVA: 0x000B403B File Offset: 0x000B223B
		public RemoveActiveSyncVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007868 RID: 30824 RVA: 0x000B404A File Offset: 0x000B224A
		public virtual RemoveActiveSyncVirtualDirectoryCommand SetParameters(RemoveActiveSyncVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007869 RID: 30825 RVA: 0x000B4054 File Offset: 0x000B2254
		public virtual RemoveActiveSyncVirtualDirectoryCommand SetParameters(RemoveActiveSyncVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000963 RID: 2403
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700515B RID: 20827
			// (set) Token: 0x0600786A RID: 30826 RVA: 0x000B405E File Offset: 0x000B225E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700515C RID: 20828
			// (set) Token: 0x0600786B RID: 30827 RVA: 0x000B4071 File Offset: 0x000B2271
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700515D RID: 20829
			// (set) Token: 0x0600786C RID: 30828 RVA: 0x000B4089 File Offset: 0x000B2289
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700515E RID: 20830
			// (set) Token: 0x0600786D RID: 30829 RVA: 0x000B40A1 File Offset: 0x000B22A1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700515F RID: 20831
			// (set) Token: 0x0600786E RID: 30830 RVA: 0x000B40B9 File Offset: 0x000B22B9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005160 RID: 20832
			// (set) Token: 0x0600786F RID: 30831 RVA: 0x000B40D1 File Offset: 0x000B22D1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005161 RID: 20833
			// (set) Token: 0x06007870 RID: 30832 RVA: 0x000B40E9 File Offset: 0x000B22E9
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000964 RID: 2404
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005162 RID: 20834
			// (set) Token: 0x06007872 RID: 30834 RVA: 0x000B4109 File Offset: 0x000B2309
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005163 RID: 20835
			// (set) Token: 0x06007873 RID: 30835 RVA: 0x000B411C File Offset: 0x000B231C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005164 RID: 20836
			// (set) Token: 0x06007874 RID: 30836 RVA: 0x000B412F File Offset: 0x000B232F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005165 RID: 20837
			// (set) Token: 0x06007875 RID: 30837 RVA: 0x000B4147 File Offset: 0x000B2347
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005166 RID: 20838
			// (set) Token: 0x06007876 RID: 30838 RVA: 0x000B415F File Offset: 0x000B235F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005167 RID: 20839
			// (set) Token: 0x06007877 RID: 30839 RVA: 0x000B4177 File Offset: 0x000B2377
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005168 RID: 20840
			// (set) Token: 0x06007878 RID: 30840 RVA: 0x000B418F File Offset: 0x000B238F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005169 RID: 20841
			// (set) Token: 0x06007879 RID: 30841 RVA: 0x000B41A7 File Offset: 0x000B23A7
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
