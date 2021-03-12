using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000942 RID: 2370
	public class NewMapiVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADMapiVirtualDirectory, ADMapiVirtualDirectory>
	{
		// Token: 0x06007740 RID: 30528 RVA: 0x000B2999 File Offset: 0x000B0B99
		private NewMapiVirtualDirectoryCommand() : base("New-MapiVirtualDirectory")
		{
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x000B29A6 File Offset: 0x000B0BA6
		public NewMapiVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007742 RID: 30530 RVA: 0x000B29B5 File Offset: 0x000B0BB5
		public virtual NewMapiVirtualDirectoryCommand SetParameters(NewMapiVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000943 RID: 2371
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005075 RID: 20597
			// (set) Token: 0x06007743 RID: 30531 RVA: 0x000B29BF File Offset: 0x000B0BBF
			public virtual MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["IISAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005076 RID: 20598
			// (set) Token: 0x06007744 RID: 30532 RVA: 0x000B29D2 File Offset: 0x000B0BD2
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005077 RID: 20599
			// (set) Token: 0x06007745 RID: 30533 RVA: 0x000B29EA File Offset: 0x000B0BEA
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005078 RID: 20600
			// (set) Token: 0x06007746 RID: 30534 RVA: 0x000B29FD File Offset: 0x000B0BFD
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005079 RID: 20601
			// (set) Token: 0x06007747 RID: 30535 RVA: 0x000B2A10 File Offset: 0x000B0C10
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x1700507A RID: 20602
			// (set) Token: 0x06007748 RID: 30536 RVA: 0x000B2A28 File Offset: 0x000B0C28
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700507B RID: 20603
			// (set) Token: 0x06007749 RID: 30537 RVA: 0x000B2A3B File Offset: 0x000B0C3B
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700507C RID: 20604
			// (set) Token: 0x0600774A RID: 30538 RVA: 0x000B2A4E File Offset: 0x000B0C4E
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700507D RID: 20605
			// (set) Token: 0x0600774B RID: 30539 RVA: 0x000B2A61 File Offset: 0x000B0C61
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700507E RID: 20606
			// (set) Token: 0x0600774C RID: 30540 RVA: 0x000B2A74 File Offset: 0x000B0C74
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700507F RID: 20607
			// (set) Token: 0x0600774D RID: 30541 RVA: 0x000B2A8C File Offset: 0x000B0C8C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005080 RID: 20608
			// (set) Token: 0x0600774E RID: 30542 RVA: 0x000B2AA4 File Offset: 0x000B0CA4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005081 RID: 20609
			// (set) Token: 0x0600774F RID: 30543 RVA: 0x000B2ABC File Offset: 0x000B0CBC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005082 RID: 20610
			// (set) Token: 0x06007750 RID: 30544 RVA: 0x000B2AD4 File Offset: 0x000B0CD4
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
