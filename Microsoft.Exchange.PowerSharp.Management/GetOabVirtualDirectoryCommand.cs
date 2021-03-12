using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200091C RID: 2332
	public class GetOabVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADOabVirtualDirectory, ADOabVirtualDirectory>
	{
		// Token: 0x060075EE RID: 30190 RVA: 0x000B0F43 File Offset: 0x000AF143
		private GetOabVirtualDirectoryCommand() : base("Get-OabVirtualDirectory")
		{
		}

		// Token: 0x060075EF RID: 30191 RVA: 0x000B0F50 File Offset: 0x000AF150
		public GetOabVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060075F0 RID: 30192 RVA: 0x000B0F5F File Offset: 0x000AF15F
		public virtual GetOabVirtualDirectoryCommand SetParameters(GetOabVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060075F1 RID: 30193 RVA: 0x000B0F69 File Offset: 0x000AF169
		public virtual GetOabVirtualDirectoryCommand SetParameters(GetOabVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060075F2 RID: 30194 RVA: 0x000B0F73 File Offset: 0x000AF173
		public virtual GetOabVirtualDirectoryCommand SetParameters(GetOabVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200091D RID: 2333
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004F6F RID: 20335
			// (set) Token: 0x060075F3 RID: 30195 RVA: 0x000B0F7D File Offset: 0x000AF17D
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F70 RID: 20336
			// (set) Token: 0x060075F4 RID: 30196 RVA: 0x000B0F95 File Offset: 0x000AF195
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F71 RID: 20337
			// (set) Token: 0x060075F5 RID: 30197 RVA: 0x000B0FAD File Offset: 0x000AF1AD
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F72 RID: 20338
			// (set) Token: 0x060075F6 RID: 30198 RVA: 0x000B0FC5 File Offset: 0x000AF1C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F73 RID: 20339
			// (set) Token: 0x060075F7 RID: 30199 RVA: 0x000B0FD8 File Offset: 0x000AF1D8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F74 RID: 20340
			// (set) Token: 0x060075F8 RID: 30200 RVA: 0x000B0FF0 File Offset: 0x000AF1F0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F75 RID: 20341
			// (set) Token: 0x060075F9 RID: 30201 RVA: 0x000B1008 File Offset: 0x000AF208
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F76 RID: 20342
			// (set) Token: 0x060075FA RID: 30202 RVA: 0x000B1020 File Offset: 0x000AF220
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200091E RID: 2334
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004F77 RID: 20343
			// (set) Token: 0x060075FC RID: 30204 RVA: 0x000B1040 File Offset: 0x000AF240
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004F78 RID: 20344
			// (set) Token: 0x060075FD RID: 30205 RVA: 0x000B1053 File Offset: 0x000AF253
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F79 RID: 20345
			// (set) Token: 0x060075FE RID: 30206 RVA: 0x000B106B File Offset: 0x000AF26B
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F7A RID: 20346
			// (set) Token: 0x060075FF RID: 30207 RVA: 0x000B1083 File Offset: 0x000AF283
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F7B RID: 20347
			// (set) Token: 0x06007600 RID: 30208 RVA: 0x000B109B File Offset: 0x000AF29B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F7C RID: 20348
			// (set) Token: 0x06007601 RID: 30209 RVA: 0x000B10AE File Offset: 0x000AF2AE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F7D RID: 20349
			// (set) Token: 0x06007602 RID: 30210 RVA: 0x000B10C6 File Offset: 0x000AF2C6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F7E RID: 20350
			// (set) Token: 0x06007603 RID: 30211 RVA: 0x000B10DE File Offset: 0x000AF2DE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F7F RID: 20351
			// (set) Token: 0x06007604 RID: 30212 RVA: 0x000B10F6 File Offset: 0x000AF2F6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200091F RID: 2335
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004F80 RID: 20352
			// (set) Token: 0x06007606 RID: 30214 RVA: 0x000B1116 File Offset: 0x000AF316
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004F81 RID: 20353
			// (set) Token: 0x06007607 RID: 30215 RVA: 0x000B1129 File Offset: 0x000AF329
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004F82 RID: 20354
			// (set) Token: 0x06007608 RID: 30216 RVA: 0x000B1141 File Offset: 0x000AF341
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F83 RID: 20355
			// (set) Token: 0x06007609 RID: 30217 RVA: 0x000B1159 File Offset: 0x000AF359
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004F84 RID: 20356
			// (set) Token: 0x0600760A RID: 30218 RVA: 0x000B1171 File Offset: 0x000AF371
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004F85 RID: 20357
			// (set) Token: 0x0600760B RID: 30219 RVA: 0x000B1184 File Offset: 0x000AF384
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004F86 RID: 20358
			// (set) Token: 0x0600760C RID: 30220 RVA: 0x000B119C File Offset: 0x000AF39C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004F87 RID: 20359
			// (set) Token: 0x0600760D RID: 30221 RVA: 0x000B11B4 File Offset: 0x000AF3B4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004F88 RID: 20360
			// (set) Token: 0x0600760E RID: 30222 RVA: 0x000B11CC File Offset: 0x000AF3CC
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
