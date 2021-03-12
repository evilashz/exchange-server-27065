using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200095C RID: 2396
	public class RemoveHostedEncryptionVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADE4eVirtualDirectory, ADE4eVirtualDirectory>
	{
		// Token: 0x0600783C RID: 30780 RVA: 0x000B3CFC File Offset: 0x000B1EFC
		private RemoveHostedEncryptionVirtualDirectoryCommand() : base("Remove-HostedEncryptionVirtualDirectory")
		{
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x000B3D09 File Offset: 0x000B1F09
		public RemoveHostedEncryptionVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600783E RID: 30782 RVA: 0x000B3D18 File Offset: 0x000B1F18
		public virtual RemoveHostedEncryptionVirtualDirectoryCommand SetParameters(RemoveHostedEncryptionVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x000B3D22 File Offset: 0x000B1F22
		public virtual RemoveHostedEncryptionVirtualDirectoryCommand SetParameters(RemoveHostedEncryptionVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200095D RID: 2397
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700513D RID: 20797
			// (set) Token: 0x06007840 RID: 30784 RVA: 0x000B3D2C File Offset: 0x000B1F2C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700513E RID: 20798
			// (set) Token: 0x06007841 RID: 30785 RVA: 0x000B3D3F File Offset: 0x000B1F3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700513F RID: 20799
			// (set) Token: 0x06007842 RID: 30786 RVA: 0x000B3D57 File Offset: 0x000B1F57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005140 RID: 20800
			// (set) Token: 0x06007843 RID: 30787 RVA: 0x000B3D6F File Offset: 0x000B1F6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005141 RID: 20801
			// (set) Token: 0x06007844 RID: 30788 RVA: 0x000B3D87 File Offset: 0x000B1F87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005142 RID: 20802
			// (set) Token: 0x06007845 RID: 30789 RVA: 0x000B3D9F File Offset: 0x000B1F9F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005143 RID: 20803
			// (set) Token: 0x06007846 RID: 30790 RVA: 0x000B3DB7 File Offset: 0x000B1FB7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200095E RID: 2398
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005144 RID: 20804
			// (set) Token: 0x06007848 RID: 30792 RVA: 0x000B3DD7 File Offset: 0x000B1FD7
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005145 RID: 20805
			// (set) Token: 0x06007849 RID: 30793 RVA: 0x000B3DEA File Offset: 0x000B1FEA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005146 RID: 20806
			// (set) Token: 0x0600784A RID: 30794 RVA: 0x000B3DFD File Offset: 0x000B1FFD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005147 RID: 20807
			// (set) Token: 0x0600784B RID: 30795 RVA: 0x000B3E15 File Offset: 0x000B2015
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005148 RID: 20808
			// (set) Token: 0x0600784C RID: 30796 RVA: 0x000B3E2D File Offset: 0x000B202D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005149 RID: 20809
			// (set) Token: 0x0600784D RID: 30797 RVA: 0x000B3E45 File Offset: 0x000B2045
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700514A RID: 20810
			// (set) Token: 0x0600784E RID: 30798 RVA: 0x000B3E5D File Offset: 0x000B205D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700514B RID: 20811
			// (set) Token: 0x0600784F RID: 30799 RVA: 0x000B3E75 File Offset: 0x000B2075
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
