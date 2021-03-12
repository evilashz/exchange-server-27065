using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200098C RID: 2444
	public class SetOabVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADOabVirtualDirectory>
	{
		// Token: 0x06007A3C RID: 31292 RVA: 0x000B6572 File Offset: 0x000B4772
		private SetOabVirtualDirectoryCommand() : base("Set-OabVirtualDirectory")
		{
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x000B657F File Offset: 0x000B477F
		public SetOabVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x000B658E File Offset: 0x000B478E
		public virtual SetOabVirtualDirectoryCommand SetParameters(SetOabVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007A3F RID: 31295 RVA: 0x000B6598 File Offset: 0x000B4798
		public virtual SetOabVirtualDirectoryCommand SetParameters(SetOabVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200098D RID: 2445
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170052DD RID: 21213
			// (set) Token: 0x06007A40 RID: 31296 RVA: 0x000B65A2 File Offset: 0x000B47A2
			public virtual bool RequireSSL
			{
				set
				{
					base.PowerSharpParameters["RequireSSL"] = value;
				}
			}

			// Token: 0x170052DE RID: 21214
			// (set) Token: 0x06007A41 RID: 31297 RVA: 0x000B65BA File Offset: 0x000B47BA
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x170052DF RID: 21215
			// (set) Token: 0x06007A42 RID: 31298 RVA: 0x000B65D2 File Offset: 0x000B47D2
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x170052E0 RID: 21216
			// (set) Token: 0x06007A43 RID: 31299 RVA: 0x000B65EA File Offset: 0x000B47EA
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x170052E1 RID: 21217
			// (set) Token: 0x06007A44 RID: 31300 RVA: 0x000B6602 File Offset: 0x000B4802
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x170052E2 RID: 21218
			// (set) Token: 0x06007A45 RID: 31301 RVA: 0x000B661A File Offset: 0x000B481A
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x170052E3 RID: 21219
			// (set) Token: 0x06007A46 RID: 31302 RVA: 0x000B662D File Offset: 0x000B482D
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x170052E4 RID: 21220
			// (set) Token: 0x06007A47 RID: 31303 RVA: 0x000B6640 File Offset: 0x000B4840
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170052E5 RID: 21221
			// (set) Token: 0x06007A48 RID: 31304 RVA: 0x000B6653 File Offset: 0x000B4853
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170052E6 RID: 21222
			// (set) Token: 0x06007A49 RID: 31305 RVA: 0x000B6666 File Offset: 0x000B4866
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170052E7 RID: 21223
			// (set) Token: 0x06007A4A RID: 31306 RVA: 0x000B6679 File Offset: 0x000B4879
			public virtual int PollInterval
			{
				set
				{
					base.PowerSharpParameters["PollInterval"] = value;
				}
			}

			// Token: 0x170052E8 RID: 21224
			// (set) Token: 0x06007A4B RID: 31307 RVA: 0x000B6691 File Offset: 0x000B4891
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170052E9 RID: 21225
			// (set) Token: 0x06007A4C RID: 31308 RVA: 0x000B66A9 File Offset: 0x000B48A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170052EA RID: 21226
			// (set) Token: 0x06007A4D RID: 31309 RVA: 0x000B66C1 File Offset: 0x000B48C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170052EB RID: 21227
			// (set) Token: 0x06007A4E RID: 31310 RVA: 0x000B66D9 File Offset: 0x000B48D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170052EC RID: 21228
			// (set) Token: 0x06007A4F RID: 31311 RVA: 0x000B66F1 File Offset: 0x000B48F1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200098E RID: 2446
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170052ED RID: 21229
			// (set) Token: 0x06007A51 RID: 31313 RVA: 0x000B6711 File Offset: 0x000B4911
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170052EE RID: 21230
			// (set) Token: 0x06007A52 RID: 31314 RVA: 0x000B6724 File Offset: 0x000B4924
			public virtual bool RequireSSL
			{
				set
				{
					base.PowerSharpParameters["RequireSSL"] = value;
				}
			}

			// Token: 0x170052EF RID: 21231
			// (set) Token: 0x06007A53 RID: 31315 RVA: 0x000B673C File Offset: 0x000B493C
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x170052F0 RID: 21232
			// (set) Token: 0x06007A54 RID: 31316 RVA: 0x000B6754 File Offset: 0x000B4954
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x170052F1 RID: 21233
			// (set) Token: 0x06007A55 RID: 31317 RVA: 0x000B676C File Offset: 0x000B496C
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x170052F2 RID: 21234
			// (set) Token: 0x06007A56 RID: 31318 RVA: 0x000B6784 File Offset: 0x000B4984
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x170052F3 RID: 21235
			// (set) Token: 0x06007A57 RID: 31319 RVA: 0x000B679C File Offset: 0x000B499C
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x170052F4 RID: 21236
			// (set) Token: 0x06007A58 RID: 31320 RVA: 0x000B67AF File Offset: 0x000B49AF
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x170052F5 RID: 21237
			// (set) Token: 0x06007A59 RID: 31321 RVA: 0x000B67C2 File Offset: 0x000B49C2
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x170052F6 RID: 21238
			// (set) Token: 0x06007A5A RID: 31322 RVA: 0x000B67D5 File Offset: 0x000B49D5
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x170052F7 RID: 21239
			// (set) Token: 0x06007A5B RID: 31323 RVA: 0x000B67E8 File Offset: 0x000B49E8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170052F8 RID: 21240
			// (set) Token: 0x06007A5C RID: 31324 RVA: 0x000B67FB File Offset: 0x000B49FB
			public virtual int PollInterval
			{
				set
				{
					base.PowerSharpParameters["PollInterval"] = value;
				}
			}

			// Token: 0x170052F9 RID: 21241
			// (set) Token: 0x06007A5D RID: 31325 RVA: 0x000B6813 File Offset: 0x000B4A13
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170052FA RID: 21242
			// (set) Token: 0x06007A5E RID: 31326 RVA: 0x000B682B File Offset: 0x000B4A2B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170052FB RID: 21243
			// (set) Token: 0x06007A5F RID: 31327 RVA: 0x000B6843 File Offset: 0x000B4A43
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170052FC RID: 21244
			// (set) Token: 0x06007A60 RID: 31328 RVA: 0x000B685B File Offset: 0x000B4A5B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170052FD RID: 21245
			// (set) Token: 0x06007A61 RID: 31329 RVA: 0x000B6873 File Offset: 0x000B4A73
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
