using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200094C RID: 2380
	public class NewPowerShellVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADPowerShellVirtualDirectory, ADPowerShellVirtualDirectory>
	{
		// Token: 0x060077A7 RID: 30631 RVA: 0x000B3170 File Offset: 0x000B1370
		private NewPowerShellVirtualDirectoryCommand() : base("New-PowerShellVirtualDirectory")
		{
		}

		// Token: 0x060077A8 RID: 30632 RVA: 0x000B317D File Offset: 0x000B137D
		public NewPowerShellVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x000B318C File Offset: 0x000B138C
		public virtual NewPowerShellVirtualDirectoryCommand SetParameters(NewPowerShellVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200094D RID: 2381
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170050C8 RID: 20680
			// (set) Token: 0x060077AA RID: 30634 RVA: 0x000B3196 File Offset: 0x000B1396
			public virtual bool RequireSSL
			{
				set
				{
					base.PowerSharpParameters["RequireSSL"] = value;
				}
			}

			// Token: 0x170050C9 RID: 20681
			// (set) Token: 0x060077AB RID: 30635 RVA: 0x000B31AE File Offset: 0x000B13AE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170050CA RID: 20682
			// (set) Token: 0x060077AC RID: 30636 RVA: 0x000B31C1 File Offset: 0x000B13C1
			public virtual bool CertificateAuthentication
			{
				set
				{
					base.PowerSharpParameters["CertificateAuthentication"] = value;
				}
			}

			// Token: 0x170050CB RID: 20683
			// (set) Token: 0x060077AD RID: 30637 RVA: 0x000B31D9 File Offset: 0x000B13D9
			public virtual bool LimitMaximumMemory
			{
				set
				{
					base.PowerSharpParameters["LimitMaximumMemory"] = value;
				}
			}

			// Token: 0x170050CC RID: 20684
			// (set) Token: 0x060077AE RID: 30638 RVA: 0x000B31F1 File Offset: 0x000B13F1
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x170050CD RID: 20685
			// (set) Token: 0x060077AF RID: 30639 RVA: 0x000B3209 File Offset: 0x000B1409
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x170050CE RID: 20686
			// (set) Token: 0x060077B0 RID: 30640 RVA: 0x000B3221 File Offset: 0x000B1421
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x170050CF RID: 20687
			// (set) Token: 0x060077B1 RID: 30641 RVA: 0x000B3234 File Offset: 0x000B1434
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x170050D0 RID: 20688
			// (set) Token: 0x060077B2 RID: 30642 RVA: 0x000B3247 File Offset: 0x000B1447
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x170050D1 RID: 20689
			// (set) Token: 0x060077B3 RID: 30643 RVA: 0x000B325A File Offset: 0x000B145A
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x170050D2 RID: 20690
			// (set) Token: 0x060077B4 RID: 30644 RVA: 0x000B3272 File Offset: 0x000B1472
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170050D3 RID: 20691
			// (set) Token: 0x060077B5 RID: 30645 RVA: 0x000B3285 File Offset: 0x000B1485
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170050D4 RID: 20692
			// (set) Token: 0x060077B6 RID: 30646 RVA: 0x000B3298 File Offset: 0x000B1498
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170050D5 RID: 20693
			// (set) Token: 0x060077B7 RID: 30647 RVA: 0x000B32AB File Offset: 0x000B14AB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170050D6 RID: 20694
			// (set) Token: 0x060077B8 RID: 30648 RVA: 0x000B32BE File Offset: 0x000B14BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170050D7 RID: 20695
			// (set) Token: 0x060077B9 RID: 30649 RVA: 0x000B32D6 File Offset: 0x000B14D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170050D8 RID: 20696
			// (set) Token: 0x060077BA RID: 30650 RVA: 0x000B32EE File Offset: 0x000B14EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170050D9 RID: 20697
			// (set) Token: 0x060077BB RID: 30651 RVA: 0x000B3306 File Offset: 0x000B1506
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170050DA RID: 20698
			// (set) Token: 0x060077BC RID: 30652 RVA: 0x000B331E File Offset: 0x000B151E
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
