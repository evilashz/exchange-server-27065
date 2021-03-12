using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200093E RID: 2366
	public class NewEcpVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADEcpVirtualDirectory, ADEcpVirtualDirectory>
	{
		// Token: 0x06007718 RID: 30488 RVA: 0x000B2697 File Offset: 0x000B0897
		private NewEcpVirtualDirectoryCommand() : base("New-EcpVirtualDirectory")
		{
		}

		// Token: 0x06007719 RID: 30489 RVA: 0x000B26A4 File Offset: 0x000B08A4
		public NewEcpVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x000B26B3 File Offset: 0x000B08B3
		public virtual NewEcpVirtualDirectoryCommand SetParameters(NewEcpVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200093F RID: 2367
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005055 RID: 20565
			// (set) Token: 0x0600771B RID: 30491 RVA: 0x000B26BD File Offset: 0x000B08BD
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x17005056 RID: 20566
			// (set) Token: 0x0600771C RID: 30492 RVA: 0x000B26D0 File Offset: 0x000B08D0
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x17005057 RID: 20567
			// (set) Token: 0x0600771D RID: 30493 RVA: 0x000B26E3 File Offset: 0x000B08E3
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x17005058 RID: 20568
			// (set) Token: 0x0600771E RID: 30494 RVA: 0x000B26F6 File Offset: 0x000B08F6
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005059 RID: 20569
			// (set) Token: 0x0600771F RID: 30495 RVA: 0x000B270E File Offset: 0x000B090E
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700505A RID: 20570
			// (set) Token: 0x06007720 RID: 30496 RVA: 0x000B2721 File Offset: 0x000B0921
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700505B RID: 20571
			// (set) Token: 0x06007721 RID: 30497 RVA: 0x000B2734 File Offset: 0x000B0934
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x1700505C RID: 20572
			// (set) Token: 0x06007722 RID: 30498 RVA: 0x000B274C File Offset: 0x000B094C
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700505D RID: 20573
			// (set) Token: 0x06007723 RID: 30499 RVA: 0x000B275F File Offset: 0x000B095F
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700505E RID: 20574
			// (set) Token: 0x06007724 RID: 30500 RVA: 0x000B2772 File Offset: 0x000B0972
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700505F RID: 20575
			// (set) Token: 0x06007725 RID: 30501 RVA: 0x000B2785 File Offset: 0x000B0985
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005060 RID: 20576
			// (set) Token: 0x06007726 RID: 30502 RVA: 0x000B2798 File Offset: 0x000B0998
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005061 RID: 20577
			// (set) Token: 0x06007727 RID: 30503 RVA: 0x000B27B0 File Offset: 0x000B09B0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005062 RID: 20578
			// (set) Token: 0x06007728 RID: 30504 RVA: 0x000B27C8 File Offset: 0x000B09C8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005063 RID: 20579
			// (set) Token: 0x06007729 RID: 30505 RVA: 0x000B27E0 File Offset: 0x000B09E0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005064 RID: 20580
			// (set) Token: 0x0600772A RID: 30506 RVA: 0x000B27F8 File Offset: 0x000B09F8
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
