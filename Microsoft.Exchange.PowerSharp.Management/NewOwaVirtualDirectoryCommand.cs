using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200094A RID: 2378
	public class NewOwaVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADOwaVirtualDirectory, ADOwaVirtualDirectory>
	{
		// Token: 0x06007791 RID: 30609 RVA: 0x000B2FC9 File Offset: 0x000B11C9
		private NewOwaVirtualDirectoryCommand() : base("New-OwaVirtualDirectory")
		{
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x000B2FD6 File Offset: 0x000B11D6
		public NewOwaVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x000B2FE5 File Offset: 0x000B11E5
		public virtual NewOwaVirtualDirectoryCommand SetParameters(NewOwaVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200094B RID: 2379
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170050B6 RID: 20662
			// (set) Token: 0x06007794 RID: 30612 RVA: 0x000B2FEF File Offset: 0x000B11EF
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x170050B7 RID: 20663
			// (set) Token: 0x06007795 RID: 30613 RVA: 0x000B3002 File Offset: 0x000B1202
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x170050B8 RID: 20664
			// (set) Token: 0x06007796 RID: 30614 RVA: 0x000B3015 File Offset: 0x000B1215
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x170050B9 RID: 20665
			// (set) Token: 0x06007797 RID: 30615 RVA: 0x000B3028 File Offset: 0x000B1228
			public virtual string ApplicationRoot
			{
				set
				{
					base.PowerSharpParameters["ApplicationRoot"] = value;
				}
			}

			// Token: 0x170050BA RID: 20666
			// (set) Token: 0x06007798 RID: 30616 RVA: 0x000B303B File Offset: 0x000B123B
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x170050BB RID: 20667
			// (set) Token: 0x06007799 RID: 30617 RVA: 0x000B3053 File Offset: 0x000B1253
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x170050BC RID: 20668
			// (set) Token: 0x0600779A RID: 30618 RVA: 0x000B3066 File Offset: 0x000B1266
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x170050BD RID: 20669
			// (set) Token: 0x0600779B RID: 30619 RVA: 0x000B3079 File Offset: 0x000B1279
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x170050BE RID: 20670
			// (set) Token: 0x0600779C RID: 30620 RVA: 0x000B3091 File Offset: 0x000B1291
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170050BF RID: 20671
			// (set) Token: 0x0600779D RID: 30621 RVA: 0x000B30A4 File Offset: 0x000B12A4
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170050C0 RID: 20672
			// (set) Token: 0x0600779E RID: 30622 RVA: 0x000B30B7 File Offset: 0x000B12B7
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170050C1 RID: 20673
			// (set) Token: 0x0600779F RID: 30623 RVA: 0x000B30CA File Offset: 0x000B12CA
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x170050C2 RID: 20674
			// (set) Token: 0x060077A0 RID: 30624 RVA: 0x000B30DD File Offset: 0x000B12DD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170050C3 RID: 20675
			// (set) Token: 0x060077A1 RID: 30625 RVA: 0x000B30F0 File Offset: 0x000B12F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170050C4 RID: 20676
			// (set) Token: 0x060077A2 RID: 30626 RVA: 0x000B3108 File Offset: 0x000B1308
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170050C5 RID: 20677
			// (set) Token: 0x060077A3 RID: 30627 RVA: 0x000B3120 File Offset: 0x000B1320
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170050C6 RID: 20678
			// (set) Token: 0x060077A4 RID: 30628 RVA: 0x000B3138 File Offset: 0x000B1338
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170050C7 RID: 20679
			// (set) Token: 0x060077A5 RID: 30629 RVA: 0x000B3150 File Offset: 0x000B1350
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
