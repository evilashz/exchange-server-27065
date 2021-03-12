using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000908 RID: 2312
	public class GetEcpVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADEcpVirtualDirectory, ADEcpVirtualDirectory>
	{
		// Token: 0x06007544 RID: 30020 RVA: 0x000B01F6 File Offset: 0x000AE3F6
		private GetEcpVirtualDirectoryCommand() : base("Get-EcpVirtualDirectory")
		{
		}

		// Token: 0x06007545 RID: 30021 RVA: 0x000B0203 File Offset: 0x000AE403
		public GetEcpVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007546 RID: 30022 RVA: 0x000B0212 File Offset: 0x000AE412
		public virtual GetEcpVirtualDirectoryCommand SetParameters(GetEcpVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007547 RID: 30023 RVA: 0x000B021C File Offset: 0x000AE41C
		public virtual GetEcpVirtualDirectoryCommand SetParameters(GetEcpVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007548 RID: 30024 RVA: 0x000B0226 File Offset: 0x000AE426
		public virtual GetEcpVirtualDirectoryCommand SetParameters(GetEcpVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000909 RID: 2313
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004EED RID: 20205
			// (set) Token: 0x06007549 RID: 30025 RVA: 0x000B0230 File Offset: 0x000AE430
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004EEE RID: 20206
			// (set) Token: 0x0600754A RID: 30026 RVA: 0x000B0248 File Offset: 0x000AE448
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004EEF RID: 20207
			// (set) Token: 0x0600754B RID: 30027 RVA: 0x000B0260 File Offset: 0x000AE460
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004EF0 RID: 20208
			// (set) Token: 0x0600754C RID: 30028 RVA: 0x000B0278 File Offset: 0x000AE478
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004EF1 RID: 20209
			// (set) Token: 0x0600754D RID: 30029 RVA: 0x000B028B File Offset: 0x000AE48B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004EF2 RID: 20210
			// (set) Token: 0x0600754E RID: 30030 RVA: 0x000B02A3 File Offset: 0x000AE4A3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004EF3 RID: 20211
			// (set) Token: 0x0600754F RID: 30031 RVA: 0x000B02BB File Offset: 0x000AE4BB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EF4 RID: 20212
			// (set) Token: 0x06007550 RID: 30032 RVA: 0x000B02D3 File Offset: 0x000AE4D3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200090A RID: 2314
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004EF5 RID: 20213
			// (set) Token: 0x06007552 RID: 30034 RVA: 0x000B02F3 File Offset: 0x000AE4F3
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004EF6 RID: 20214
			// (set) Token: 0x06007553 RID: 30035 RVA: 0x000B0306 File Offset: 0x000AE506
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004EF7 RID: 20215
			// (set) Token: 0x06007554 RID: 30036 RVA: 0x000B031E File Offset: 0x000AE51E
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004EF8 RID: 20216
			// (set) Token: 0x06007555 RID: 30037 RVA: 0x000B0336 File Offset: 0x000AE536
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004EF9 RID: 20217
			// (set) Token: 0x06007556 RID: 30038 RVA: 0x000B034E File Offset: 0x000AE54E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004EFA RID: 20218
			// (set) Token: 0x06007557 RID: 30039 RVA: 0x000B0361 File Offset: 0x000AE561
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004EFB RID: 20219
			// (set) Token: 0x06007558 RID: 30040 RVA: 0x000B0379 File Offset: 0x000AE579
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004EFC RID: 20220
			// (set) Token: 0x06007559 RID: 30041 RVA: 0x000B0391 File Offset: 0x000AE591
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EFD RID: 20221
			// (set) Token: 0x0600755A RID: 30042 RVA: 0x000B03A9 File Offset: 0x000AE5A9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200090B RID: 2315
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004EFE RID: 20222
			// (set) Token: 0x0600755C RID: 30044 RVA: 0x000B03C9 File Offset: 0x000AE5C9
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004EFF RID: 20223
			// (set) Token: 0x0600755D RID: 30045 RVA: 0x000B03DC File Offset: 0x000AE5DC
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F00 RID: 20224
			// (set) Token: 0x0600755E RID: 30046 RVA: 0x000B03F4 File Offset: 0x000AE5F4
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F01 RID: 20225
			// (set) Token: 0x0600755F RID: 30047 RVA: 0x000B040C File Offset: 0x000AE60C
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F02 RID: 20226
			// (set) Token: 0x06007560 RID: 30048 RVA: 0x000B0424 File Offset: 0x000AE624
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F03 RID: 20227
			// (set) Token: 0x06007561 RID: 30049 RVA: 0x000B0437 File Offset: 0x000AE637
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F04 RID: 20228
			// (set) Token: 0x06007562 RID: 30050 RVA: 0x000B044F File Offset: 0x000AE64F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F05 RID: 20229
			// (set) Token: 0x06007563 RID: 30051 RVA: 0x000B0467 File Offset: 0x000AE667
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F06 RID: 20230
			// (set) Token: 0x06007564 RID: 30052 RVA: 0x000B047F File Offset: 0x000AE67F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
