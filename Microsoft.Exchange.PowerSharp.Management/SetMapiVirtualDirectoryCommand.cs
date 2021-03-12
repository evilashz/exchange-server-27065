using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000986 RID: 2438
	public class SetMapiVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADMapiVirtualDirectory>
	{
		// Token: 0x060079D8 RID: 31192 RVA: 0x000B5D8E File Offset: 0x000B3F8E
		private SetMapiVirtualDirectoryCommand() : base("Set-MapiVirtualDirectory")
		{
		}

		// Token: 0x060079D9 RID: 31193 RVA: 0x000B5D9B File Offset: 0x000B3F9B
		public SetMapiVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x000B5DAA File Offset: 0x000B3FAA
		public virtual SetMapiVirtualDirectoryCommand SetParameters(SetMapiVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060079DB RID: 31195 RVA: 0x000B5DB4 File Offset: 0x000B3FB4
		public virtual SetMapiVirtualDirectoryCommand SetParameters(SetMapiVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000987 RID: 2439
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005285 RID: 21125
			// (set) Token: 0x060079DC RID: 31196 RVA: 0x000B5DBE File Offset: 0x000B3FBE
			public virtual MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["IISAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005286 RID: 21126
			// (set) Token: 0x060079DD RID: 31197 RVA: 0x000B5DD1 File Offset: 0x000B3FD1
			public virtual bool ApplyDefaults
			{
				set
				{
					base.PowerSharpParameters["ApplyDefaults"] = value;
				}
			}

			// Token: 0x17005287 RID: 21127
			// (set) Token: 0x060079DE RID: 31198 RVA: 0x000B5DE9 File Offset: 0x000B3FE9
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005288 RID: 21128
			// (set) Token: 0x060079DF RID: 31199 RVA: 0x000B5E01 File Offset: 0x000B4001
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005289 RID: 21129
			// (set) Token: 0x060079E0 RID: 31200 RVA: 0x000B5E14 File Offset: 0x000B4014
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700528A RID: 21130
			// (set) Token: 0x060079E1 RID: 31201 RVA: 0x000B5E27 File Offset: 0x000B4027
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700528B RID: 21131
			// (set) Token: 0x060079E2 RID: 31202 RVA: 0x000B5E3A File Offset: 0x000B403A
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700528C RID: 21132
			// (set) Token: 0x060079E3 RID: 31203 RVA: 0x000B5E4D File Offset: 0x000B404D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700528D RID: 21133
			// (set) Token: 0x060079E4 RID: 31204 RVA: 0x000B5E60 File Offset: 0x000B4060
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700528E RID: 21134
			// (set) Token: 0x060079E5 RID: 31205 RVA: 0x000B5E73 File Offset: 0x000B4073
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700528F RID: 21135
			// (set) Token: 0x060079E6 RID: 31206 RVA: 0x000B5E8B File Offset: 0x000B408B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005290 RID: 21136
			// (set) Token: 0x060079E7 RID: 31207 RVA: 0x000B5EA3 File Offset: 0x000B40A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005291 RID: 21137
			// (set) Token: 0x060079E8 RID: 31208 RVA: 0x000B5EBB File Offset: 0x000B40BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005292 RID: 21138
			// (set) Token: 0x060079E9 RID: 31209 RVA: 0x000B5ED3 File Offset: 0x000B40D3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000988 RID: 2440
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005293 RID: 21139
			// (set) Token: 0x060079EB RID: 31211 RVA: 0x000B5EF3 File Offset: 0x000B40F3
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005294 RID: 21140
			// (set) Token: 0x060079EC RID: 31212 RVA: 0x000B5F06 File Offset: 0x000B4106
			public virtual MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["IISAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005295 RID: 21141
			// (set) Token: 0x060079ED RID: 31213 RVA: 0x000B5F19 File Offset: 0x000B4119
			public virtual bool ApplyDefaults
			{
				set
				{
					base.PowerSharpParameters["ApplyDefaults"] = value;
				}
			}

			// Token: 0x17005296 RID: 21142
			// (set) Token: 0x060079EE RID: 31214 RVA: 0x000B5F31 File Offset: 0x000B4131
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005297 RID: 21143
			// (set) Token: 0x060079EF RID: 31215 RVA: 0x000B5F49 File Offset: 0x000B4149
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005298 RID: 21144
			// (set) Token: 0x060079F0 RID: 31216 RVA: 0x000B5F5C File Offset: 0x000B415C
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x17005299 RID: 21145
			// (set) Token: 0x060079F1 RID: 31217 RVA: 0x000B5F6F File Offset: 0x000B416F
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700529A RID: 21146
			// (set) Token: 0x060079F2 RID: 31218 RVA: 0x000B5F82 File Offset: 0x000B4182
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700529B RID: 21147
			// (set) Token: 0x060079F3 RID: 31219 RVA: 0x000B5F95 File Offset: 0x000B4195
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700529C RID: 21148
			// (set) Token: 0x060079F4 RID: 31220 RVA: 0x000B5FA8 File Offset: 0x000B41A8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700529D RID: 21149
			// (set) Token: 0x060079F5 RID: 31221 RVA: 0x000B5FBB File Offset: 0x000B41BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700529E RID: 21150
			// (set) Token: 0x060079F6 RID: 31222 RVA: 0x000B5FD3 File Offset: 0x000B41D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700529F RID: 21151
			// (set) Token: 0x060079F7 RID: 31223 RVA: 0x000B5FEB File Offset: 0x000B41EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170052A0 RID: 21152
			// (set) Token: 0x060079F8 RID: 31224 RVA: 0x000B6003 File Offset: 0x000B4203
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170052A1 RID: 21153
			// (set) Token: 0x060079F9 RID: 31225 RVA: 0x000B601B File Offset: 0x000B421B
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
