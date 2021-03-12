using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009A7 RID: 2471
	public class UpdatePswsVirtualDirectoryVersionCommand : SyntheticCommandWithPipelineInput<ADPswsVirtualDirectory, ADPswsVirtualDirectory>
	{
		// Token: 0x06007C63 RID: 31843 RVA: 0x000B9398 File Offset: 0x000B7598
		private UpdatePswsVirtualDirectoryVersionCommand() : base("Update-PswsVirtualDirectoryVersion")
		{
		}

		// Token: 0x06007C64 RID: 31844 RVA: 0x000B93A5 File Offset: 0x000B75A5
		public UpdatePswsVirtualDirectoryVersionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007C65 RID: 31845 RVA: 0x000B93B4 File Offset: 0x000B75B4
		public virtual UpdatePswsVirtualDirectoryVersionCommand SetParameters(UpdatePswsVirtualDirectoryVersionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009A8 RID: 2472
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170054CE RID: 21710
			// (set) Token: 0x06007C66 RID: 31846 RVA: 0x000B93BE File Offset: 0x000B75BE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054CF RID: 21711
			// (set) Token: 0x06007C67 RID: 31847 RVA: 0x000B93D1 File Offset: 0x000B75D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054D0 RID: 21712
			// (set) Token: 0x06007C68 RID: 31848 RVA: 0x000B93E9 File Offset: 0x000B75E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054D1 RID: 21713
			// (set) Token: 0x06007C69 RID: 31849 RVA: 0x000B9401 File Offset: 0x000B7601
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054D2 RID: 21714
			// (set) Token: 0x06007C6A RID: 31850 RVA: 0x000B9419 File Offset: 0x000B7619
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170054D3 RID: 21715
			// (set) Token: 0x06007C6B RID: 31851 RVA: 0x000B9431 File Offset: 0x000B7631
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
