using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000946 RID: 2374
	public class NewO365SuiteServiceVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADO365SuiteServiceVirtualDirectory, ADO365SuiteServiceVirtualDirectory>
	{
		// Token: 0x0600776A RID: 30570 RVA: 0x000B2CC6 File Offset: 0x000B0EC6
		private NewO365SuiteServiceVirtualDirectoryCommand() : base("New-O365SuiteServiceVirtualDirectory")
		{
		}

		// Token: 0x0600776B RID: 30571 RVA: 0x000B2CD3 File Offset: 0x000B0ED3
		public NewO365SuiteServiceVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600776C RID: 30572 RVA: 0x000B2CE2 File Offset: 0x000B0EE2
		public virtual NewO365SuiteServiceVirtualDirectoryCommand SetParameters(NewO365SuiteServiceVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000947 RID: 2375
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005097 RID: 20631
			// (set) Token: 0x0600776D RID: 30573 RVA: 0x000B2CEC File Offset: 0x000B0EEC
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x17005098 RID: 20632
			// (set) Token: 0x0600776E RID: 30574 RVA: 0x000B2D04 File Offset: 0x000B0F04
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x17005099 RID: 20633
			// (set) Token: 0x0600776F RID: 30575 RVA: 0x000B2D1C File Offset: 0x000B0F1C
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x1700509A RID: 20634
			// (set) Token: 0x06007770 RID: 30576 RVA: 0x000B2D34 File Offset: 0x000B0F34
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700509B RID: 20635
			// (set) Token: 0x06007771 RID: 30577 RVA: 0x000B2D47 File Offset: 0x000B0F47
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700509C RID: 20636
			// (set) Token: 0x06007772 RID: 30578 RVA: 0x000B2D5A File Offset: 0x000B0F5A
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700509D RID: 20637
			// (set) Token: 0x06007773 RID: 30579 RVA: 0x000B2D6D File Offset: 0x000B0F6D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700509E RID: 20638
			// (set) Token: 0x06007774 RID: 30580 RVA: 0x000B2D80 File Offset: 0x000B0F80
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700509F RID: 20639
			// (set) Token: 0x06007775 RID: 30581 RVA: 0x000B2D98 File Offset: 0x000B0F98
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170050A0 RID: 20640
			// (set) Token: 0x06007776 RID: 30582 RVA: 0x000B2DB0 File Offset: 0x000B0FB0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170050A1 RID: 20641
			// (set) Token: 0x06007777 RID: 30583 RVA: 0x000B2DC8 File Offset: 0x000B0FC8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170050A2 RID: 20642
			// (set) Token: 0x06007778 RID: 30584 RVA: 0x000B2DE0 File Offset: 0x000B0FE0
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
