using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009A9 RID: 2473
	public class UpdateWebServicesVirtualDirectoryVersionCommand : SyntheticCommandWithPipelineInput<ADWebServicesVirtualDirectory, ADWebServicesVirtualDirectory>
	{
		// Token: 0x06007C6D RID: 31853 RVA: 0x000B9451 File Offset: 0x000B7651
		private UpdateWebServicesVirtualDirectoryVersionCommand() : base("Update-WebServicesVirtualDirectoryVersion")
		{
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x000B945E File Offset: 0x000B765E
		public UpdateWebServicesVirtualDirectoryVersionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x000B946D File Offset: 0x000B766D
		public virtual UpdateWebServicesVirtualDirectoryVersionCommand SetParameters(UpdateWebServicesVirtualDirectoryVersionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009AA RID: 2474
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170054D4 RID: 21716
			// (set) Token: 0x06007C70 RID: 31856 RVA: 0x000B9477 File Offset: 0x000B7677
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054D5 RID: 21717
			// (set) Token: 0x06007C71 RID: 31857 RVA: 0x000B948A File Offset: 0x000B768A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054D6 RID: 21718
			// (set) Token: 0x06007C72 RID: 31858 RVA: 0x000B94A2 File Offset: 0x000B76A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054D7 RID: 21719
			// (set) Token: 0x06007C73 RID: 31859 RVA: 0x000B94BA File Offset: 0x000B76BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054D8 RID: 21720
			// (set) Token: 0x06007C74 RID: 31860 RVA: 0x000B94D2 File Offset: 0x000B76D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170054D9 RID: 21721
			// (set) Token: 0x06007C75 RID: 31861 RVA: 0x000B94EA File Offset: 0x000B76EA
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
