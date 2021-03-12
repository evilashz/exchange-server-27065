using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000944 RID: 2372
	public class NewActiveSyncVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADMobileVirtualDirectory, ADMobileVirtualDirectory>
	{
		// Token: 0x06007752 RID: 30546 RVA: 0x000B2AF4 File Offset: 0x000B0CF4
		private NewActiveSyncVirtualDirectoryCommand() : base("New-ActiveSyncVirtualDirectory")
		{
		}

		// Token: 0x06007753 RID: 30547 RVA: 0x000B2B01 File Offset: 0x000B0D01
		public NewActiveSyncVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007754 RID: 30548 RVA: 0x000B2B10 File Offset: 0x000B0D10
		public virtual NewActiveSyncVirtualDirectoryCommand SetParameters(NewActiveSyncVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000945 RID: 2373
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005083 RID: 20611
			// (set) Token: 0x06007755 RID: 30549 RVA: 0x000B2B1A File Offset: 0x000B0D1A
			public virtual bool InstallProxySubDirectory
			{
				set
				{
					base.PowerSharpParameters["InstallProxySubDirectory"] = value;
				}
			}

			// Token: 0x17005084 RID: 20612
			// (set) Token: 0x06007756 RID: 30550 RVA: 0x000B2B32 File Offset: 0x000B0D32
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x17005085 RID: 20613
			// (set) Token: 0x06007757 RID: 30551 RVA: 0x000B2B45 File Offset: 0x000B0D45
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x17005086 RID: 20614
			// (set) Token: 0x06007758 RID: 30552 RVA: 0x000B2B58 File Offset: 0x000B0D58
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x17005087 RID: 20615
			// (set) Token: 0x06007759 RID: 30553 RVA: 0x000B2B6B File Offset: 0x000B0D6B
			public virtual string ApplicationRoot
			{
				set
				{
					base.PowerSharpParameters["ApplicationRoot"] = value;
				}
			}

			// Token: 0x17005088 RID: 20616
			// (set) Token: 0x0600775A RID: 30554 RVA: 0x000B2B7E File Offset: 0x000B0D7E
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005089 RID: 20617
			// (set) Token: 0x0600775B RID: 30555 RVA: 0x000B2B96 File Offset: 0x000B0D96
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700508A RID: 20618
			// (set) Token: 0x0600775C RID: 30556 RVA: 0x000B2BA9 File Offset: 0x000B0DA9
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700508B RID: 20619
			// (set) Token: 0x0600775D RID: 30557 RVA: 0x000B2BBC File Offset: 0x000B0DBC
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x1700508C RID: 20620
			// (set) Token: 0x0600775E RID: 30558 RVA: 0x000B2BD4 File Offset: 0x000B0DD4
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700508D RID: 20621
			// (set) Token: 0x0600775F RID: 30559 RVA: 0x000B2BE7 File Offset: 0x000B0DE7
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700508E RID: 20622
			// (set) Token: 0x06007760 RID: 30560 RVA: 0x000B2BFA File Offset: 0x000B0DFA
			public virtual MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["InternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x1700508F RID: 20623
			// (set) Token: 0x06007761 RID: 30561 RVA: 0x000B2C0D File Offset: 0x000B0E0D
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005090 RID: 20624
			// (set) Token: 0x06007762 RID: 30562 RVA: 0x000B2C20 File Offset: 0x000B0E20
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005091 RID: 20625
			// (set) Token: 0x06007763 RID: 30563 RVA: 0x000B2C33 File Offset: 0x000B0E33
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005092 RID: 20626
			// (set) Token: 0x06007764 RID: 30564 RVA: 0x000B2C46 File Offset: 0x000B0E46
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005093 RID: 20627
			// (set) Token: 0x06007765 RID: 30565 RVA: 0x000B2C5E File Offset: 0x000B0E5E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005094 RID: 20628
			// (set) Token: 0x06007766 RID: 30566 RVA: 0x000B2C76 File Offset: 0x000B0E76
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005095 RID: 20629
			// (set) Token: 0x06007767 RID: 30567 RVA: 0x000B2C8E File Offset: 0x000B0E8E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005096 RID: 20630
			// (set) Token: 0x06007768 RID: 30568 RVA: 0x000B2CA6 File Offset: 0x000B0EA6
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
