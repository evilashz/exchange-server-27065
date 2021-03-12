using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009A3 RID: 2467
	public class UpdateOwaVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADOwaVirtualDirectory, ADOwaVirtualDirectory>
	{
		// Token: 0x06007C4F RID: 31823 RVA: 0x000B9226 File Offset: 0x000B7426
		private UpdateOwaVirtualDirectoryCommand() : base("Update-OwaVirtualDirectory")
		{
		}

		// Token: 0x06007C50 RID: 31824 RVA: 0x000B9233 File Offset: 0x000B7433
		public UpdateOwaVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007C51 RID: 31825 RVA: 0x000B9242 File Offset: 0x000B7442
		public virtual UpdateOwaVirtualDirectoryCommand SetParameters(UpdateOwaVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009A4 RID: 2468
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170054C2 RID: 21698
			// (set) Token: 0x06007C52 RID: 31826 RVA: 0x000B924C File Offset: 0x000B744C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054C3 RID: 21699
			// (set) Token: 0x06007C53 RID: 31827 RVA: 0x000B925F File Offset: 0x000B745F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054C4 RID: 21700
			// (set) Token: 0x06007C54 RID: 31828 RVA: 0x000B9277 File Offset: 0x000B7477
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054C5 RID: 21701
			// (set) Token: 0x06007C55 RID: 31829 RVA: 0x000B928F File Offset: 0x000B748F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054C6 RID: 21702
			// (set) Token: 0x06007C56 RID: 31830 RVA: 0x000B92A7 File Offset: 0x000B74A7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170054C7 RID: 21703
			// (set) Token: 0x06007C57 RID: 31831 RVA: 0x000B92BF File Offset: 0x000B74BF
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
