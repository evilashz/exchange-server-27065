using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000980 RID: 2432
	public class SetEcpVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADEcpVirtualDirectory>
	{
		// Token: 0x06007952 RID: 31058 RVA: 0x000B5270 File Offset: 0x000B3470
		private SetEcpVirtualDirectoryCommand() : base("Set-EcpVirtualDirectory")
		{
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x000B527D File Offset: 0x000B347D
		public SetEcpVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x000B528C File Offset: 0x000B348C
		public virtual SetEcpVirtualDirectoryCommand SetParameters(SetEcpVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007955 RID: 31061 RVA: 0x000B5296 File Offset: 0x000B3496
		public virtual SetEcpVirtualDirectoryCommand SetParameters(SetEcpVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000981 RID: 2433
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700520B RID: 21003
			// (set) Token: 0x06007956 RID: 31062 RVA: 0x000B52A0 File Offset: 0x000B34A0
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x1700520C RID: 21004
			// (set) Token: 0x06007957 RID: 31063 RVA: 0x000B52B8 File Offset: 0x000B34B8
			public virtual bool FormsAuthentication
			{
				set
				{
					base.PowerSharpParameters["FormsAuthentication"] = value;
				}
			}

			// Token: 0x1700520D RID: 21005
			// (set) Token: 0x06007958 RID: 31064 RVA: 0x000B52D0 File Offset: 0x000B34D0
			public virtual bool AdfsAuthentication
			{
				set
				{
					base.PowerSharpParameters["AdfsAuthentication"] = value;
				}
			}

			// Token: 0x1700520E RID: 21006
			// (set) Token: 0x06007959 RID: 31065 RVA: 0x000B52E8 File Offset: 0x000B34E8
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x1700520F RID: 21007
			// (set) Token: 0x0600795A RID: 31066 RVA: 0x000B5300 File Offset: 0x000B3500
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005210 RID: 21008
			// (set) Token: 0x0600795B RID: 31067 RVA: 0x000B5318 File Offset: 0x000B3518
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x17005211 RID: 21009
			// (set) Token: 0x0600795C RID: 31068 RVA: 0x000B5330 File Offset: 0x000B3530
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x17005212 RID: 21010
			// (set) Token: 0x0600795D RID: 31069 RVA: 0x000B5348 File Offset: 0x000B3548
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005213 RID: 21011
			// (set) Token: 0x0600795E RID: 31070 RVA: 0x000B5360 File Offset: 0x000B3560
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005214 RID: 21012
			// (set) Token: 0x0600795F RID: 31071 RVA: 0x000B5373 File Offset: 0x000B3573
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005215 RID: 21013
			// (set) Token: 0x06007960 RID: 31072 RVA: 0x000B5386 File Offset: 0x000B3586
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005216 RID: 21014
			// (set) Token: 0x06007961 RID: 31073 RVA: 0x000B5399 File Offset: 0x000B3599
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005217 RID: 21015
			// (set) Token: 0x06007962 RID: 31074 RVA: 0x000B53AC File Offset: 0x000B35AC
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005218 RID: 21016
			// (set) Token: 0x06007963 RID: 31075 RVA: 0x000B53BF File Offset: 0x000B35BF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005219 RID: 21017
			// (set) Token: 0x06007964 RID: 31076 RVA: 0x000B53D2 File Offset: 0x000B35D2
			public virtual bool AdminEnabled
			{
				set
				{
					base.PowerSharpParameters["AdminEnabled"] = value;
				}
			}

			// Token: 0x1700521A RID: 21018
			// (set) Token: 0x06007965 RID: 31077 RVA: 0x000B53EA File Offset: 0x000B35EA
			public virtual bool OwaOptionsEnabled
			{
				set
				{
					base.PowerSharpParameters["OwaOptionsEnabled"] = value;
				}
			}

			// Token: 0x1700521B RID: 21019
			// (set) Token: 0x06007966 RID: 31078 RVA: 0x000B5402 File Offset: 0x000B3602
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700521C RID: 21020
			// (set) Token: 0x06007967 RID: 31079 RVA: 0x000B541A File Offset: 0x000B361A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700521D RID: 21021
			// (set) Token: 0x06007968 RID: 31080 RVA: 0x000B5432 File Offset: 0x000B3632
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700521E RID: 21022
			// (set) Token: 0x06007969 RID: 31081 RVA: 0x000B544A File Offset: 0x000B364A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700521F RID: 21023
			// (set) Token: 0x0600796A RID: 31082 RVA: 0x000B5462 File Offset: 0x000B3662
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000982 RID: 2434
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005220 RID: 21024
			// (set) Token: 0x0600796C RID: 31084 RVA: 0x000B5482 File Offset: 0x000B3682
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005221 RID: 21025
			// (set) Token: 0x0600796D RID: 31085 RVA: 0x000B5495 File Offset: 0x000B3695
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x17005222 RID: 21026
			// (set) Token: 0x0600796E RID: 31086 RVA: 0x000B54AD File Offset: 0x000B36AD
			public virtual bool FormsAuthentication
			{
				set
				{
					base.PowerSharpParameters["FormsAuthentication"] = value;
				}
			}

			// Token: 0x17005223 RID: 21027
			// (set) Token: 0x0600796F RID: 31087 RVA: 0x000B54C5 File Offset: 0x000B36C5
			public virtual bool AdfsAuthentication
			{
				set
				{
					base.PowerSharpParameters["AdfsAuthentication"] = value;
				}
			}

			// Token: 0x17005224 RID: 21028
			// (set) Token: 0x06007970 RID: 31088 RVA: 0x000B54DD File Offset: 0x000B36DD
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005225 RID: 21029
			// (set) Token: 0x06007971 RID: 31089 RVA: 0x000B54F5 File Offset: 0x000B36F5
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005226 RID: 21030
			// (set) Token: 0x06007972 RID: 31090 RVA: 0x000B550D File Offset: 0x000B370D
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x17005227 RID: 21031
			// (set) Token: 0x06007973 RID: 31091 RVA: 0x000B5525 File Offset: 0x000B3725
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x17005228 RID: 21032
			// (set) Token: 0x06007974 RID: 31092 RVA: 0x000B553D File Offset: 0x000B373D
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005229 RID: 21033
			// (set) Token: 0x06007975 RID: 31093 RVA: 0x000B5555 File Offset: 0x000B3755
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700522A RID: 21034
			// (set) Token: 0x06007976 RID: 31094 RVA: 0x000B5568 File Offset: 0x000B3768
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700522B RID: 21035
			// (set) Token: 0x06007977 RID: 31095 RVA: 0x000B557B File Offset: 0x000B377B
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700522C RID: 21036
			// (set) Token: 0x06007978 RID: 31096 RVA: 0x000B558E File Offset: 0x000B378E
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700522D RID: 21037
			// (set) Token: 0x06007979 RID: 31097 RVA: 0x000B55A1 File Offset: 0x000B37A1
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x1700522E RID: 21038
			// (set) Token: 0x0600797A RID: 31098 RVA: 0x000B55B4 File Offset: 0x000B37B4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700522F RID: 21039
			// (set) Token: 0x0600797B RID: 31099 RVA: 0x000B55C7 File Offset: 0x000B37C7
			public virtual bool AdminEnabled
			{
				set
				{
					base.PowerSharpParameters["AdminEnabled"] = value;
				}
			}

			// Token: 0x17005230 RID: 21040
			// (set) Token: 0x0600797C RID: 31100 RVA: 0x000B55DF File Offset: 0x000B37DF
			public virtual bool OwaOptionsEnabled
			{
				set
				{
					base.PowerSharpParameters["OwaOptionsEnabled"] = value;
				}
			}

			// Token: 0x17005231 RID: 21041
			// (set) Token: 0x0600797D RID: 31101 RVA: 0x000B55F7 File Offset: 0x000B37F7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005232 RID: 21042
			// (set) Token: 0x0600797E RID: 31102 RVA: 0x000B560F File Offset: 0x000B380F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005233 RID: 21043
			// (set) Token: 0x0600797F RID: 31103 RVA: 0x000B5627 File Offset: 0x000B3827
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005234 RID: 21044
			// (set) Token: 0x06007980 RID: 31104 RVA: 0x000B563F File Offset: 0x000B383F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005235 RID: 21045
			// (set) Token: 0x06007981 RID: 31105 RVA: 0x000B5657 File Offset: 0x000B3857
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
