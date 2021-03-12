using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000940 RID: 2368
	public class NewHostedEncryptionVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADE4eVirtualDirectory, ADE4eVirtualDirectory>
	{
		// Token: 0x0600772C RID: 30508 RVA: 0x000B2818 File Offset: 0x000B0A18
		private NewHostedEncryptionVirtualDirectoryCommand() : base("New-HostedEncryptionVirtualDirectory")
		{
		}

		// Token: 0x0600772D RID: 30509 RVA: 0x000B2825 File Offset: 0x000B0A25
		public NewHostedEncryptionVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600772E RID: 30510 RVA: 0x000B2834 File Offset: 0x000B0A34
		public virtual NewHostedEncryptionVirtualDirectoryCommand SetParameters(NewHostedEncryptionVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000941 RID: 2369
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005065 RID: 20581
			// (set) Token: 0x0600772F RID: 30511 RVA: 0x000B283E File Offset: 0x000B0A3E
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x17005066 RID: 20582
			// (set) Token: 0x06007730 RID: 30512 RVA: 0x000B2851 File Offset: 0x000B0A51
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x17005067 RID: 20583
			// (set) Token: 0x06007731 RID: 30513 RVA: 0x000B2864 File Offset: 0x000B0A64
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x17005068 RID: 20584
			// (set) Token: 0x06007732 RID: 30514 RVA: 0x000B2877 File Offset: 0x000B0A77
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005069 RID: 20585
			// (set) Token: 0x06007733 RID: 30515 RVA: 0x000B288F File Offset: 0x000B0A8F
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700506A RID: 20586
			// (set) Token: 0x06007734 RID: 30516 RVA: 0x000B28A2 File Offset: 0x000B0AA2
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700506B RID: 20587
			// (set) Token: 0x06007735 RID: 30517 RVA: 0x000B28B5 File Offset: 0x000B0AB5
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x1700506C RID: 20588
			// (set) Token: 0x06007736 RID: 30518 RVA: 0x000B28CD File Offset: 0x000B0ACD
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700506D RID: 20589
			// (set) Token: 0x06007737 RID: 30519 RVA: 0x000B28E0 File Offset: 0x000B0AE0
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700506E RID: 20590
			// (set) Token: 0x06007738 RID: 30520 RVA: 0x000B28F3 File Offset: 0x000B0AF3
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700506F RID: 20591
			// (set) Token: 0x06007739 RID: 30521 RVA: 0x000B2906 File Offset: 0x000B0B06
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005070 RID: 20592
			// (set) Token: 0x0600773A RID: 30522 RVA: 0x000B2919 File Offset: 0x000B0B19
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005071 RID: 20593
			// (set) Token: 0x0600773B RID: 30523 RVA: 0x000B2931 File Offset: 0x000B0B31
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005072 RID: 20594
			// (set) Token: 0x0600773C RID: 30524 RVA: 0x000B2949 File Offset: 0x000B0B49
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005073 RID: 20595
			// (set) Token: 0x0600773D RID: 30525 RVA: 0x000B2961 File Offset: 0x000B0B61
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005074 RID: 20596
			// (set) Token: 0x0600773E RID: 30526 RVA: 0x000B2979 File Offset: 0x000B0B79
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
