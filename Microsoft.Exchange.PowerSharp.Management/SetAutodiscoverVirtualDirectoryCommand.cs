using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200097D RID: 2429
	public class SetAutodiscoverVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADAutodiscoverVirtualDirectory>
	{
		// Token: 0x06007927 RID: 31015 RVA: 0x000B4EEF File Offset: 0x000B30EF
		private SetAutodiscoverVirtualDirectoryCommand() : base("Set-AutodiscoverVirtualDirectory")
		{
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x000B4EFC File Offset: 0x000B30FC
		public SetAutodiscoverVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x000B4F0B File Offset: 0x000B310B
		public virtual SetAutodiscoverVirtualDirectoryCommand SetParameters(SetAutodiscoverVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x000B4F15 File Offset: 0x000B3115
		public virtual SetAutodiscoverVirtualDirectoryCommand SetParameters(SetAutodiscoverVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200097E RID: 2430
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170051E6 RID: 20966
			// (set) Token: 0x0600792B RID: 31019 RVA: 0x000B4F1F File Offset: 0x000B311F
			public virtual bool WSSecurityAuthentication
			{
				set
				{
					base.PowerSharpParameters["WSSecurityAuthentication"] = value;
				}
			}

			// Token: 0x170051E7 RID: 20967
			// (set) Token: 0x0600792C RID: 31020 RVA: 0x000B4F37 File Offset: 0x000B3137
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x170051E8 RID: 20968
			// (set) Token: 0x0600792D RID: 31021 RVA: 0x000B4F4F File Offset: 0x000B314F
			public virtual bool LiveIdBasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthentication"] = value;
				}
			}

			// Token: 0x170051E9 RID: 20969
			// (set) Token: 0x0600792E RID: 31022 RVA: 0x000B4F67 File Offset: 0x000B3167
			public virtual bool LiveIdNegotiateAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdNegotiateAuthentication"] = value;
				}
			}

			// Token: 0x170051EA RID: 20970
			// (set) Token: 0x0600792F RID: 31023 RVA: 0x000B4F7F File Offset: 0x000B317F
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x170051EB RID: 20971
			// (set) Token: 0x06007930 RID: 31024 RVA: 0x000B4F97 File Offset: 0x000B3197
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x170051EC RID: 20972
			// (set) Token: 0x06007931 RID: 31025 RVA: 0x000B4FAF File Offset: 0x000B31AF
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x170051ED RID: 20973
			// (set) Token: 0x06007932 RID: 31026 RVA: 0x000B4FC7 File Offset: 0x000B31C7
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x170051EE RID: 20974
			// (set) Token: 0x06007933 RID: 31027 RVA: 0x000B4FDF File Offset: 0x000B31DF
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x170051EF RID: 20975
			// (set) Token: 0x06007934 RID: 31028 RVA: 0x000B4FF2 File Offset: 0x000B31F2
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x170051F0 RID: 20976
			// (set) Token: 0x06007935 RID: 31029 RVA: 0x000B5005 File Offset: 0x000B3205
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170051F1 RID: 20977
			// (set) Token: 0x06007936 RID: 31030 RVA: 0x000B5018 File Offset: 0x000B3218
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170051F2 RID: 20978
			// (set) Token: 0x06007937 RID: 31031 RVA: 0x000B502B File Offset: 0x000B322B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170051F3 RID: 20979
			// (set) Token: 0x06007938 RID: 31032 RVA: 0x000B503E File Offset: 0x000B323E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170051F4 RID: 20980
			// (set) Token: 0x06007939 RID: 31033 RVA: 0x000B5056 File Offset: 0x000B3256
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170051F5 RID: 20981
			// (set) Token: 0x0600793A RID: 31034 RVA: 0x000B506E File Offset: 0x000B326E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170051F6 RID: 20982
			// (set) Token: 0x0600793B RID: 31035 RVA: 0x000B5086 File Offset: 0x000B3286
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170051F7 RID: 20983
			// (set) Token: 0x0600793C RID: 31036 RVA: 0x000B509E File Offset: 0x000B329E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200097F RID: 2431
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170051F8 RID: 20984
			// (set) Token: 0x0600793E RID: 31038 RVA: 0x000B50BE File Offset: 0x000B32BE
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170051F9 RID: 20985
			// (set) Token: 0x0600793F RID: 31039 RVA: 0x000B50D1 File Offset: 0x000B32D1
			public virtual bool WSSecurityAuthentication
			{
				set
				{
					base.PowerSharpParameters["WSSecurityAuthentication"] = value;
				}
			}

			// Token: 0x170051FA RID: 20986
			// (set) Token: 0x06007940 RID: 31040 RVA: 0x000B50E9 File Offset: 0x000B32E9
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x170051FB RID: 20987
			// (set) Token: 0x06007941 RID: 31041 RVA: 0x000B5101 File Offset: 0x000B3301
			public virtual bool LiveIdBasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthentication"] = value;
				}
			}

			// Token: 0x170051FC RID: 20988
			// (set) Token: 0x06007942 RID: 31042 RVA: 0x000B5119 File Offset: 0x000B3319
			public virtual bool LiveIdNegotiateAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdNegotiateAuthentication"] = value;
				}
			}

			// Token: 0x170051FD RID: 20989
			// (set) Token: 0x06007943 RID: 31043 RVA: 0x000B5131 File Offset: 0x000B3331
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x170051FE RID: 20990
			// (set) Token: 0x06007944 RID: 31044 RVA: 0x000B5149 File Offset: 0x000B3349
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x170051FF RID: 20991
			// (set) Token: 0x06007945 RID: 31045 RVA: 0x000B5161 File Offset: 0x000B3361
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005200 RID: 20992
			// (set) Token: 0x06007946 RID: 31046 RVA: 0x000B5179 File Offset: 0x000B3379
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005201 RID: 20993
			// (set) Token: 0x06007947 RID: 31047 RVA: 0x000B5191 File Offset: 0x000B3391
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005202 RID: 20994
			// (set) Token: 0x06007948 RID: 31048 RVA: 0x000B51A4 File Offset: 0x000B33A4
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005203 RID: 20995
			// (set) Token: 0x06007949 RID: 31049 RVA: 0x000B51B7 File Offset: 0x000B33B7
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005204 RID: 20996
			// (set) Token: 0x0600794A RID: 31050 RVA: 0x000B51CA File Offset: 0x000B33CA
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005205 RID: 20997
			// (set) Token: 0x0600794B RID: 31051 RVA: 0x000B51DD File Offset: 0x000B33DD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005206 RID: 20998
			// (set) Token: 0x0600794C RID: 31052 RVA: 0x000B51F0 File Offset: 0x000B33F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005207 RID: 20999
			// (set) Token: 0x0600794D RID: 31053 RVA: 0x000B5208 File Offset: 0x000B3408
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005208 RID: 21000
			// (set) Token: 0x0600794E RID: 31054 RVA: 0x000B5220 File Offset: 0x000B3420
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005209 RID: 21001
			// (set) Token: 0x0600794F RID: 31055 RVA: 0x000B5238 File Offset: 0x000B3438
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700520A RID: 21002
			// (set) Token: 0x06007950 RID: 31056 RVA: 0x000B5250 File Offset: 0x000B3450
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
