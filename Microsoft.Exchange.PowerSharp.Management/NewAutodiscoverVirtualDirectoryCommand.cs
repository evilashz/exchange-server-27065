using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200093C RID: 2364
	public class NewAutodiscoverVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADAutodiscoverVirtualDirectory, ADAutodiscoverVirtualDirectory>
	{
		// Token: 0x060076FE RID: 30462 RVA: 0x000B248B File Offset: 0x000B068B
		private NewAutodiscoverVirtualDirectoryCommand() : base("New-AutodiscoverVirtualDirectory")
		{
		}

		// Token: 0x060076FF RID: 30463 RVA: 0x000B2498 File Offset: 0x000B0698
		public NewAutodiscoverVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007700 RID: 30464 RVA: 0x000B24A7 File Offset: 0x000B06A7
		public virtual NewAutodiscoverVirtualDirectoryCommand SetParameters(NewAutodiscoverVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200093D RID: 2365
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700503F RID: 20543
			// (set) Token: 0x06007701 RID: 30465 RVA: 0x000B24B1 File Offset: 0x000B06B1
			public virtual bool WSSecurityAuthentication
			{
				set
				{
					base.PowerSharpParameters["WSSecurityAuthentication"] = value;
				}
			}

			// Token: 0x17005040 RID: 20544
			// (set) Token: 0x06007702 RID: 30466 RVA: 0x000B24C9 File Offset: 0x000B06C9
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x17005041 RID: 20545
			// (set) Token: 0x06007703 RID: 30467 RVA: 0x000B24E1 File Offset: 0x000B06E1
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005042 RID: 20546
			// (set) Token: 0x06007704 RID: 30468 RVA: 0x000B24F9 File Offset: 0x000B06F9
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x17005043 RID: 20547
			// (set) Token: 0x06007705 RID: 30469 RVA: 0x000B2511 File Offset: 0x000B0711
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005044 RID: 20548
			// (set) Token: 0x06007706 RID: 30470 RVA: 0x000B2529 File Offset: 0x000B0729
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x17005045 RID: 20549
			// (set) Token: 0x06007707 RID: 30471 RVA: 0x000B253C File Offset: 0x000B073C
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x17005046 RID: 20550
			// (set) Token: 0x06007708 RID: 30472 RVA: 0x000B254F File Offset: 0x000B074F
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x17005047 RID: 20551
			// (set) Token: 0x06007709 RID: 30473 RVA: 0x000B2562 File Offset: 0x000B0762
			public virtual string ApplicationRoot
			{
				set
				{
					base.PowerSharpParameters["ApplicationRoot"] = value;
				}
			}

			// Token: 0x17005048 RID: 20552
			// (set) Token: 0x0600770A RID: 30474 RVA: 0x000B2575 File Offset: 0x000B0775
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005049 RID: 20553
			// (set) Token: 0x0600770B RID: 30475 RVA: 0x000B258D File Offset: 0x000B078D
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700504A RID: 20554
			// (set) Token: 0x0600770C RID: 30476 RVA: 0x000B25A0 File Offset: 0x000B07A0
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700504B RID: 20555
			// (set) Token: 0x0600770D RID: 30477 RVA: 0x000B25B3 File Offset: 0x000B07B3
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x1700504C RID: 20556
			// (set) Token: 0x0600770E RID: 30478 RVA: 0x000B25CB File Offset: 0x000B07CB
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700504D RID: 20557
			// (set) Token: 0x0600770F RID: 30479 RVA: 0x000B25DE File Offset: 0x000B07DE
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700504E RID: 20558
			// (set) Token: 0x06007710 RID: 30480 RVA: 0x000B25F1 File Offset: 0x000B07F1
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700504F RID: 20559
			// (set) Token: 0x06007711 RID: 30481 RVA: 0x000B2604 File Offset: 0x000B0804
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005050 RID: 20560
			// (set) Token: 0x06007712 RID: 30482 RVA: 0x000B2617 File Offset: 0x000B0817
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005051 RID: 20561
			// (set) Token: 0x06007713 RID: 30483 RVA: 0x000B262F File Offset: 0x000B082F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005052 RID: 20562
			// (set) Token: 0x06007714 RID: 30484 RVA: 0x000B2647 File Offset: 0x000B0847
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005053 RID: 20563
			// (set) Token: 0x06007715 RID: 30485 RVA: 0x000B265F File Offset: 0x000B085F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005054 RID: 20564
			// (set) Token: 0x06007716 RID: 30486 RVA: 0x000B2677 File Offset: 0x000B0877
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
