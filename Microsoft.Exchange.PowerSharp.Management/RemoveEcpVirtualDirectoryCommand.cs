using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000959 RID: 2393
	public class RemoveEcpVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADEcpVirtualDirectory, ADEcpVirtualDirectory>
	{
		// Token: 0x06007827 RID: 30759 RVA: 0x000B3B63 File Offset: 0x000B1D63
		private RemoveEcpVirtualDirectoryCommand() : base("Remove-EcpVirtualDirectory")
		{
		}

		// Token: 0x06007828 RID: 30760 RVA: 0x000B3B70 File Offset: 0x000B1D70
		public RemoveEcpVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007829 RID: 30761 RVA: 0x000B3B7F File Offset: 0x000B1D7F
		public virtual RemoveEcpVirtualDirectoryCommand SetParameters(RemoveEcpVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600782A RID: 30762 RVA: 0x000B3B89 File Offset: 0x000B1D89
		public virtual RemoveEcpVirtualDirectoryCommand SetParameters(RemoveEcpVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200095A RID: 2394
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700512E RID: 20782
			// (set) Token: 0x0600782B RID: 30763 RVA: 0x000B3B93 File Offset: 0x000B1D93
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700512F RID: 20783
			// (set) Token: 0x0600782C RID: 30764 RVA: 0x000B3BA6 File Offset: 0x000B1DA6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005130 RID: 20784
			// (set) Token: 0x0600782D RID: 30765 RVA: 0x000B3BBE File Offset: 0x000B1DBE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005131 RID: 20785
			// (set) Token: 0x0600782E RID: 30766 RVA: 0x000B3BD6 File Offset: 0x000B1DD6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005132 RID: 20786
			// (set) Token: 0x0600782F RID: 30767 RVA: 0x000B3BEE File Offset: 0x000B1DEE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005133 RID: 20787
			// (set) Token: 0x06007830 RID: 30768 RVA: 0x000B3C06 File Offset: 0x000B1E06
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005134 RID: 20788
			// (set) Token: 0x06007831 RID: 30769 RVA: 0x000B3C1E File Offset: 0x000B1E1E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200095B RID: 2395
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005135 RID: 20789
			// (set) Token: 0x06007833 RID: 30771 RVA: 0x000B3C3E File Offset: 0x000B1E3E
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005136 RID: 20790
			// (set) Token: 0x06007834 RID: 30772 RVA: 0x000B3C51 File Offset: 0x000B1E51
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005137 RID: 20791
			// (set) Token: 0x06007835 RID: 30773 RVA: 0x000B3C64 File Offset: 0x000B1E64
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005138 RID: 20792
			// (set) Token: 0x06007836 RID: 30774 RVA: 0x000B3C7C File Offset: 0x000B1E7C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005139 RID: 20793
			// (set) Token: 0x06007837 RID: 30775 RVA: 0x000B3C94 File Offset: 0x000B1E94
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700513A RID: 20794
			// (set) Token: 0x06007838 RID: 30776 RVA: 0x000B3CAC File Offset: 0x000B1EAC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700513B RID: 20795
			// (set) Token: 0x06007839 RID: 30777 RVA: 0x000B3CC4 File Offset: 0x000B1EC4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700513C RID: 20796
			// (set) Token: 0x0600783A RID: 30778 RVA: 0x000B3CDC File Offset: 0x000B1EDC
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
