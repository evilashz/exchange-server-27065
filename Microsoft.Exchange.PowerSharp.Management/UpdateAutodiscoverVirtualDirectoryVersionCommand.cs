using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009A1 RID: 2465
	public class UpdateAutodiscoverVirtualDirectoryVersionCommand : SyntheticCommandWithPipelineInput<ADAutodiscoverVirtualDirectory, ADAutodiscoverVirtualDirectory>
	{
		// Token: 0x06007C45 RID: 31813 RVA: 0x000B916D File Offset: 0x000B736D
		private UpdateAutodiscoverVirtualDirectoryVersionCommand() : base("Update-AutodiscoverVirtualDirectoryVersion")
		{
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x000B917A File Offset: 0x000B737A
		public UpdateAutodiscoverVirtualDirectoryVersionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007C47 RID: 31815 RVA: 0x000B9189 File Offset: 0x000B7389
		public virtual UpdateAutodiscoverVirtualDirectoryVersionCommand SetParameters(UpdateAutodiscoverVirtualDirectoryVersionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009A2 RID: 2466
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170054BC RID: 21692
			// (set) Token: 0x06007C48 RID: 31816 RVA: 0x000B9193 File Offset: 0x000B7393
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054BD RID: 21693
			// (set) Token: 0x06007C49 RID: 31817 RVA: 0x000B91A6 File Offset: 0x000B73A6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054BE RID: 21694
			// (set) Token: 0x06007C4A RID: 31818 RVA: 0x000B91BE File Offset: 0x000B73BE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054BF RID: 21695
			// (set) Token: 0x06007C4B RID: 31819 RVA: 0x000B91D6 File Offset: 0x000B73D6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054C0 RID: 21696
			// (set) Token: 0x06007C4C RID: 31820 RVA: 0x000B91EE File Offset: 0x000B73EE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170054C1 RID: 21697
			// (set) Token: 0x06007C4D RID: 31821 RVA: 0x000B9206 File Offset: 0x000B7406
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
