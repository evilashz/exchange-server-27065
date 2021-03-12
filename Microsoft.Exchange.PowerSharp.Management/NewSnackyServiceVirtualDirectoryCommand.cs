using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000952 RID: 2386
	public class NewSnackyServiceVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADSnackyServiceVirtualDirectory, ADSnackyServiceVirtualDirectory>
	{
		// Token: 0x060077E4 RID: 30692 RVA: 0x000B362E File Offset: 0x000B182E
		private NewSnackyServiceVirtualDirectoryCommand() : base("New-SnackyServiceVirtualDirectory")
		{
		}

		// Token: 0x060077E5 RID: 30693 RVA: 0x000B363B File Offset: 0x000B183B
		public NewSnackyServiceVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060077E6 RID: 30694 RVA: 0x000B364A File Offset: 0x000B184A
		public virtual NewSnackyServiceVirtualDirectoryCommand SetParameters(NewSnackyServiceVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000953 RID: 2387
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170050F9 RID: 20729
			// (set) Token: 0x060077E7 RID: 30695 RVA: 0x000B3654 File Offset: 0x000B1854
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x170050FA RID: 20730
			// (set) Token: 0x060077E8 RID: 30696 RVA: 0x000B366C File Offset: 0x000B186C
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x170050FB RID: 20731
			// (set) Token: 0x060077E9 RID: 30697 RVA: 0x000B3684 File Offset: 0x000B1884
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170050FC RID: 20732
			// (set) Token: 0x060077EA RID: 30698 RVA: 0x000B3697 File Offset: 0x000B1897
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170050FD RID: 20733
			// (set) Token: 0x060077EB RID: 30699 RVA: 0x000B36AA File Offset: 0x000B18AA
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170050FE RID: 20734
			// (set) Token: 0x060077EC RID: 30700 RVA: 0x000B36BD File Offset: 0x000B18BD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170050FF RID: 20735
			// (set) Token: 0x060077ED RID: 30701 RVA: 0x000B36D0 File Offset: 0x000B18D0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005100 RID: 20736
			// (set) Token: 0x060077EE RID: 30702 RVA: 0x000B36E8 File Offset: 0x000B18E8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005101 RID: 20737
			// (set) Token: 0x060077EF RID: 30703 RVA: 0x000B3700 File Offset: 0x000B1900
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005102 RID: 20738
			// (set) Token: 0x060077F0 RID: 30704 RVA: 0x000B3718 File Offset: 0x000B1918
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005103 RID: 20739
			// (set) Token: 0x060077F1 RID: 30705 RVA: 0x000B3730 File Offset: 0x000B1930
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
