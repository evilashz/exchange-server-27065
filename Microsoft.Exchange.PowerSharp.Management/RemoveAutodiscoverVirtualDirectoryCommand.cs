using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000956 RID: 2390
	public class RemoveAutodiscoverVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADAutodiscoverVirtualDirectory, ADAutodiscoverVirtualDirectory>
	{
		// Token: 0x06007812 RID: 30738 RVA: 0x000B39CA File Offset: 0x000B1BCA
		private RemoveAutodiscoverVirtualDirectoryCommand() : base("Remove-AutodiscoverVirtualDirectory")
		{
		}

		// Token: 0x06007813 RID: 30739 RVA: 0x000B39D7 File Offset: 0x000B1BD7
		public RemoveAutodiscoverVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007814 RID: 30740 RVA: 0x000B39E6 File Offset: 0x000B1BE6
		public virtual RemoveAutodiscoverVirtualDirectoryCommand SetParameters(RemoveAutodiscoverVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007815 RID: 30741 RVA: 0x000B39F0 File Offset: 0x000B1BF0
		public virtual RemoveAutodiscoverVirtualDirectoryCommand SetParameters(RemoveAutodiscoverVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000957 RID: 2391
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700511F RID: 20767
			// (set) Token: 0x06007816 RID: 30742 RVA: 0x000B39FA File Offset: 0x000B1BFA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005120 RID: 20768
			// (set) Token: 0x06007817 RID: 30743 RVA: 0x000B3A0D File Offset: 0x000B1C0D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005121 RID: 20769
			// (set) Token: 0x06007818 RID: 30744 RVA: 0x000B3A25 File Offset: 0x000B1C25
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005122 RID: 20770
			// (set) Token: 0x06007819 RID: 30745 RVA: 0x000B3A3D File Offset: 0x000B1C3D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005123 RID: 20771
			// (set) Token: 0x0600781A RID: 30746 RVA: 0x000B3A55 File Offset: 0x000B1C55
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005124 RID: 20772
			// (set) Token: 0x0600781B RID: 30747 RVA: 0x000B3A6D File Offset: 0x000B1C6D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005125 RID: 20773
			// (set) Token: 0x0600781C RID: 30748 RVA: 0x000B3A85 File Offset: 0x000B1C85
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000958 RID: 2392
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005126 RID: 20774
			// (set) Token: 0x0600781E RID: 30750 RVA: 0x000B3AA5 File Offset: 0x000B1CA5
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005127 RID: 20775
			// (set) Token: 0x0600781F RID: 30751 RVA: 0x000B3AB8 File Offset: 0x000B1CB8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005128 RID: 20776
			// (set) Token: 0x06007820 RID: 30752 RVA: 0x000B3ACB File Offset: 0x000B1CCB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005129 RID: 20777
			// (set) Token: 0x06007821 RID: 30753 RVA: 0x000B3AE3 File Offset: 0x000B1CE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700512A RID: 20778
			// (set) Token: 0x06007822 RID: 30754 RVA: 0x000B3AFB File Offset: 0x000B1CFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700512B RID: 20779
			// (set) Token: 0x06007823 RID: 30755 RVA: 0x000B3B13 File Offset: 0x000B1D13
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700512C RID: 20780
			// (set) Token: 0x06007824 RID: 30756 RVA: 0x000B3B2B File Offset: 0x000B1D2B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700512D RID: 20781
			// (set) Token: 0x06007825 RID: 30757 RVA: 0x000B3B43 File Offset: 0x000B1D43
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
