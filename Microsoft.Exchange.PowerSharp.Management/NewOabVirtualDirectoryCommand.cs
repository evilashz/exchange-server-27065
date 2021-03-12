using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000948 RID: 2376
	public class NewOabVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADOabVirtualDirectory, ADOabVirtualDirectory>
	{
		// Token: 0x0600777A RID: 30586 RVA: 0x000B2E00 File Offset: 0x000B1000
		private NewOabVirtualDirectoryCommand() : base("New-OabVirtualDirectory")
		{
		}

		// Token: 0x0600777B RID: 30587 RVA: 0x000B2E0D File Offset: 0x000B100D
		public NewOabVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600777C RID: 30588 RVA: 0x000B2E1C File Offset: 0x000B101C
		public virtual NewOabVirtualDirectoryCommand SetParameters(NewOabVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000949 RID: 2377
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170050A3 RID: 20643
			// (set) Token: 0x0600777D RID: 30589 RVA: 0x000B2E26 File Offset: 0x000B1026
			public virtual int PollInterval
			{
				set
				{
					base.PowerSharpParameters["PollInterval"] = value;
				}
			}

			// Token: 0x170050A4 RID: 20644
			// (set) Token: 0x0600777E RID: 30590 RVA: 0x000B2E3E File Offset: 0x000B103E
			public virtual bool RequireSSL
			{
				set
				{
					base.PowerSharpParameters["RequireSSL"] = value;
				}
			}

			// Token: 0x170050A5 RID: 20645
			// (set) Token: 0x0600777F RID: 30591 RVA: 0x000B2E56 File Offset: 0x000B1056
			public virtual SwitchParameter Recovery
			{
				set
				{
					base.PowerSharpParameters["Recovery"] = value;
				}
			}

			// Token: 0x170050A6 RID: 20646
			// (set) Token: 0x06007780 RID: 30592 RVA: 0x000B2E6E File Offset: 0x000B106E
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x170050A7 RID: 20647
			// (set) Token: 0x06007781 RID: 30593 RVA: 0x000B2E81 File Offset: 0x000B1081
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x170050A8 RID: 20648
			// (set) Token: 0x06007782 RID: 30594 RVA: 0x000B2E94 File Offset: 0x000B1094
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x170050A9 RID: 20649
			// (set) Token: 0x06007783 RID: 30595 RVA: 0x000B2EA7 File Offset: 0x000B10A7
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x170050AA RID: 20650
			// (set) Token: 0x06007784 RID: 30596 RVA: 0x000B2EBF File Offset: 0x000B10BF
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x170050AB RID: 20651
			// (set) Token: 0x06007785 RID: 30597 RVA: 0x000B2ED2 File Offset: 0x000B10D2
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x170050AC RID: 20652
			// (set) Token: 0x06007786 RID: 30598 RVA: 0x000B2EE5 File Offset: 0x000B10E5
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x170050AD RID: 20653
			// (set) Token: 0x06007787 RID: 30599 RVA: 0x000B2EFD File Offset: 0x000B10FD
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170050AE RID: 20654
			// (set) Token: 0x06007788 RID: 30600 RVA: 0x000B2F10 File Offset: 0x000B1110
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170050AF RID: 20655
			// (set) Token: 0x06007789 RID: 30601 RVA: 0x000B2F23 File Offset: 0x000B1123
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170050B0 RID: 20656
			// (set) Token: 0x0600778A RID: 30602 RVA: 0x000B2F36 File Offset: 0x000B1136
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170050B1 RID: 20657
			// (set) Token: 0x0600778B RID: 30603 RVA: 0x000B2F49 File Offset: 0x000B1149
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170050B2 RID: 20658
			// (set) Token: 0x0600778C RID: 30604 RVA: 0x000B2F61 File Offset: 0x000B1161
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170050B3 RID: 20659
			// (set) Token: 0x0600778D RID: 30605 RVA: 0x000B2F79 File Offset: 0x000B1179
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170050B4 RID: 20660
			// (set) Token: 0x0600778E RID: 30606 RVA: 0x000B2F91 File Offset: 0x000B1191
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170050B5 RID: 20661
			// (set) Token: 0x0600778F RID: 30607 RVA: 0x000B2FA9 File Offset: 0x000B11A9
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
