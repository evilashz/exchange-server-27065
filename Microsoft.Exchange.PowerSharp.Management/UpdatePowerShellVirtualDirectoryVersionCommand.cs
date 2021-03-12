using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009A5 RID: 2469
	public class UpdatePowerShellVirtualDirectoryVersionCommand : SyntheticCommandWithPipelineInput<ADPowerShellVirtualDirectory, ADPowerShellVirtualDirectory>
	{
		// Token: 0x06007C59 RID: 31833 RVA: 0x000B92DF File Offset: 0x000B74DF
		private UpdatePowerShellVirtualDirectoryVersionCommand() : base("Update-PowerShellVirtualDirectoryVersion")
		{
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x000B92EC File Offset: 0x000B74EC
		public UpdatePowerShellVirtualDirectoryVersionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007C5B RID: 31835 RVA: 0x000B92FB File Offset: 0x000B74FB
		public virtual UpdatePowerShellVirtualDirectoryVersionCommand SetParameters(UpdatePowerShellVirtualDirectoryVersionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009A6 RID: 2470
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170054C8 RID: 21704
			// (set) Token: 0x06007C5C RID: 31836 RVA: 0x000B9305 File Offset: 0x000B7505
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054C9 RID: 21705
			// (set) Token: 0x06007C5D RID: 31837 RVA: 0x000B9318 File Offset: 0x000B7518
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054CA RID: 21706
			// (set) Token: 0x06007C5E RID: 31838 RVA: 0x000B9330 File Offset: 0x000B7530
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054CB RID: 21707
			// (set) Token: 0x06007C5F RID: 31839 RVA: 0x000B9348 File Offset: 0x000B7548
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054CC RID: 21708
			// (set) Token: 0x06007C60 RID: 31840 RVA: 0x000B9360 File Offset: 0x000B7560
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170054CD RID: 21709
			// (set) Token: 0x06007C61 RID: 31841 RVA: 0x000B9378 File Offset: 0x000B7578
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
