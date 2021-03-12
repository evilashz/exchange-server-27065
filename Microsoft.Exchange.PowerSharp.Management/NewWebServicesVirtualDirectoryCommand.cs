using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000954 RID: 2388
	public class NewWebServicesVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADWebServicesVirtualDirectory, ADWebServicesVirtualDirectory>
	{
		// Token: 0x060077F3 RID: 30707 RVA: 0x000B3750 File Offset: 0x000B1950
		private NewWebServicesVirtualDirectoryCommand() : base("New-WebServicesVirtualDirectory")
		{
		}

		// Token: 0x060077F4 RID: 30708 RVA: 0x000B375D File Offset: 0x000B195D
		public NewWebServicesVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060077F5 RID: 30709 RVA: 0x000B376C File Offset: 0x000B196C
		public virtual NewWebServicesVirtualDirectoryCommand SetParameters(NewWebServicesVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000955 RID: 2389
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005104 RID: 20740
			// (set) Token: 0x060077F6 RID: 30710 RVA: 0x000B3776 File Offset: 0x000B1976
			public virtual Uri InternalNLBBypassUrl
			{
				set
				{
					base.PowerSharpParameters["InternalNLBBypassUrl"] = value;
				}
			}

			// Token: 0x17005105 RID: 20741
			// (set) Token: 0x060077F7 RID: 30711 RVA: 0x000B3789 File Offset: 0x000B1989
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x17005106 RID: 20742
			// (set) Token: 0x060077F8 RID: 30712 RVA: 0x000B37A1 File Offset: 0x000B19A1
			public virtual string AppPoolIdForManagement
			{
				set
				{
					base.PowerSharpParameters["AppPoolIdForManagement"] = value;
				}
			}

			// Token: 0x17005107 RID: 20743
			// (set) Token: 0x060077F9 RID: 30713 RVA: 0x000B37B4 File Offset: 0x000B19B4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17005108 RID: 20744
			// (set) Token: 0x060077FA RID: 30714 RVA: 0x000B37CC File Offset: 0x000B19CC
			public virtual bool WSSecurityAuthentication
			{
				set
				{
					base.PowerSharpParameters["WSSecurityAuthentication"] = value;
				}
			}

			// Token: 0x17005109 RID: 20745
			// (set) Token: 0x060077FB RID: 30715 RVA: 0x000B37E4 File Offset: 0x000B19E4
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x1700510A RID: 20746
			// (set) Token: 0x060077FC RID: 30716 RVA: 0x000B37FC File Offset: 0x000B19FC
			public virtual bool MRSProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["MRSProxyEnabled"] = value;
				}
			}

			// Token: 0x1700510B RID: 20747
			// (set) Token: 0x060077FD RID: 30717 RVA: 0x000B3814 File Offset: 0x000B1A14
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x1700510C RID: 20748
			// (set) Token: 0x060077FE RID: 30718 RVA: 0x000B382C File Offset: 0x000B1A2C
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x1700510D RID: 20749
			// (set) Token: 0x060077FF RID: 30719 RVA: 0x000B3844 File Offset: 0x000B1A44
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x1700510E RID: 20750
			// (set) Token: 0x06007800 RID: 30720 RVA: 0x000B385C File Offset: 0x000B1A5C
			public virtual string WebSiteName
			{
				set
				{
					base.PowerSharpParameters["WebSiteName"] = value;
				}
			}

			// Token: 0x1700510F RID: 20751
			// (set) Token: 0x06007801 RID: 30721 RVA: 0x000B386F File Offset: 0x000B1A6F
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x17005110 RID: 20752
			// (set) Token: 0x06007802 RID: 30722 RVA: 0x000B3882 File Offset: 0x000B1A82
			public virtual string AppPoolId
			{
				set
				{
					base.PowerSharpParameters["AppPoolId"] = value;
				}
			}

			// Token: 0x17005111 RID: 20753
			// (set) Token: 0x06007803 RID: 30723 RVA: 0x000B3895 File Offset: 0x000B1A95
			public virtual string ApplicationRoot
			{
				set
				{
					base.PowerSharpParameters["ApplicationRoot"] = value;
				}
			}

			// Token: 0x17005112 RID: 20754
			// (set) Token: 0x06007804 RID: 30724 RVA: 0x000B38A8 File Offset: 0x000B1AA8
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005113 RID: 20755
			// (set) Token: 0x06007805 RID: 30725 RVA: 0x000B38C0 File Offset: 0x000B1AC0
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005114 RID: 20756
			// (set) Token: 0x06007806 RID: 30726 RVA: 0x000B38D3 File Offset: 0x000B1AD3
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005115 RID: 20757
			// (set) Token: 0x06007807 RID: 30727 RVA: 0x000B38E6 File Offset: 0x000B1AE6
			public virtual VirtualDirectoryRole Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x17005116 RID: 20758
			// (set) Token: 0x06007808 RID: 30728 RVA: 0x000B38FE File Offset: 0x000B1AFE
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17005117 RID: 20759
			// (set) Token: 0x06007809 RID: 30729 RVA: 0x000B3911 File Offset: 0x000B1B11
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005118 RID: 20760
			// (set) Token: 0x0600780A RID: 30730 RVA: 0x000B3924 File Offset: 0x000B1B24
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005119 RID: 20761
			// (set) Token: 0x0600780B RID: 30731 RVA: 0x000B3937 File Offset: 0x000B1B37
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700511A RID: 20762
			// (set) Token: 0x0600780C RID: 30732 RVA: 0x000B394A File Offset: 0x000B1B4A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700511B RID: 20763
			// (set) Token: 0x0600780D RID: 30733 RVA: 0x000B3962 File Offset: 0x000B1B62
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700511C RID: 20764
			// (set) Token: 0x0600780E RID: 30734 RVA: 0x000B397A File Offset: 0x000B1B7A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700511D RID: 20765
			// (set) Token: 0x0600780F RID: 30735 RVA: 0x000B3992 File Offset: 0x000B1B92
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700511E RID: 20766
			// (set) Token: 0x06007810 RID: 30736 RVA: 0x000B39AA File Offset: 0x000B1BAA
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
