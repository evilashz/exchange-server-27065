using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200095F RID: 2399
	public class RemoveMapiVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADMapiVirtualDirectory, ADMapiVirtualDirectory>
	{
		// Token: 0x06007851 RID: 30801 RVA: 0x000B3E95 File Offset: 0x000B2095
		private RemoveMapiVirtualDirectoryCommand() : base("Remove-MapiVirtualDirectory")
		{
		}

		// Token: 0x06007852 RID: 30802 RVA: 0x000B3EA2 File Offset: 0x000B20A2
		public RemoveMapiVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007853 RID: 30803 RVA: 0x000B3EB1 File Offset: 0x000B20B1
		public virtual RemoveMapiVirtualDirectoryCommand SetParameters(RemoveMapiVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007854 RID: 30804 RVA: 0x000B3EBB File Offset: 0x000B20BB
		public virtual RemoveMapiVirtualDirectoryCommand SetParameters(RemoveMapiVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000960 RID: 2400
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700514C RID: 20812
			// (set) Token: 0x06007855 RID: 30805 RVA: 0x000B3EC5 File Offset: 0x000B20C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700514D RID: 20813
			// (set) Token: 0x06007856 RID: 30806 RVA: 0x000B3ED8 File Offset: 0x000B20D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700514E RID: 20814
			// (set) Token: 0x06007857 RID: 30807 RVA: 0x000B3EF0 File Offset: 0x000B20F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700514F RID: 20815
			// (set) Token: 0x06007858 RID: 30808 RVA: 0x000B3F08 File Offset: 0x000B2108
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005150 RID: 20816
			// (set) Token: 0x06007859 RID: 30809 RVA: 0x000B3F20 File Offset: 0x000B2120
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005151 RID: 20817
			// (set) Token: 0x0600785A RID: 30810 RVA: 0x000B3F38 File Offset: 0x000B2138
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005152 RID: 20818
			// (set) Token: 0x0600785B RID: 30811 RVA: 0x000B3F50 File Offset: 0x000B2150
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000961 RID: 2401
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005153 RID: 20819
			// (set) Token: 0x0600785D RID: 30813 RVA: 0x000B3F70 File Offset: 0x000B2170
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005154 RID: 20820
			// (set) Token: 0x0600785E RID: 30814 RVA: 0x000B3F83 File Offset: 0x000B2183
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005155 RID: 20821
			// (set) Token: 0x0600785F RID: 30815 RVA: 0x000B3F96 File Offset: 0x000B2196
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005156 RID: 20822
			// (set) Token: 0x06007860 RID: 30816 RVA: 0x000B3FAE File Offset: 0x000B21AE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005157 RID: 20823
			// (set) Token: 0x06007861 RID: 30817 RVA: 0x000B3FC6 File Offset: 0x000B21C6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005158 RID: 20824
			// (set) Token: 0x06007862 RID: 30818 RVA: 0x000B3FDE File Offset: 0x000B21DE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005159 RID: 20825
			// (set) Token: 0x06007863 RID: 30819 RVA: 0x000B3FF6 File Offset: 0x000B21F6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700515A RID: 20826
			// (set) Token: 0x06007864 RID: 30820 RVA: 0x000B400E File Offset: 0x000B220E
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
