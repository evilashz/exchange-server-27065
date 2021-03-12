using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000934 RID: 2356
	public class GetSnackyServiceVirtualDirectoryCommand : SyntheticCommandWithPipelineInput<ADSnackyServiceVirtualDirectory, ADSnackyServiceVirtualDirectory>
	{
		// Token: 0x060076BA RID: 30394 RVA: 0x000B1F39 File Offset: 0x000B0139
		private GetSnackyServiceVirtualDirectoryCommand() : base("Get-SnackyServiceVirtualDirectory")
		{
		}

		// Token: 0x060076BB RID: 30395 RVA: 0x000B1F46 File Offset: 0x000B0146
		public GetSnackyServiceVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060076BC RID: 30396 RVA: 0x000B1F55 File Offset: 0x000B0155
		public virtual GetSnackyServiceVirtualDirectoryCommand SetParameters(GetSnackyServiceVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060076BD RID: 30397 RVA: 0x000B1F5F File Offset: 0x000B015F
		public virtual GetSnackyServiceVirtualDirectoryCommand SetParameters(GetSnackyServiceVirtualDirectoryCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060076BE RID: 30398 RVA: 0x000B1F69 File Offset: 0x000B0169
		public virtual GetSnackyServiceVirtualDirectoryCommand SetParameters(GetSnackyServiceVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000935 RID: 2357
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700500B RID: 20491
			// (set) Token: 0x060076BF RID: 30399 RVA: 0x000B1F73 File Offset: 0x000B0173
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x1700500C RID: 20492
			// (set) Token: 0x060076C0 RID: 30400 RVA: 0x000B1F8B File Offset: 0x000B018B
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x1700500D RID: 20493
			// (set) Token: 0x060076C1 RID: 30401 RVA: 0x000B1FA3 File Offset: 0x000B01A3
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x1700500E RID: 20494
			// (set) Token: 0x060076C2 RID: 30402 RVA: 0x000B1FBB File Offset: 0x000B01BB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700500F RID: 20495
			// (set) Token: 0x060076C3 RID: 30403 RVA: 0x000B1FCE File Offset: 0x000B01CE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005010 RID: 20496
			// (set) Token: 0x060076C4 RID: 30404 RVA: 0x000B1FE6 File Offset: 0x000B01E6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005011 RID: 20497
			// (set) Token: 0x060076C5 RID: 30405 RVA: 0x000B1FFE File Offset: 0x000B01FE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005012 RID: 20498
			// (set) Token: 0x060076C6 RID: 30406 RVA: 0x000B2016 File Offset: 0x000B0216
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000936 RID: 2358
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17005013 RID: 20499
			// (set) Token: 0x060076C8 RID: 30408 RVA: 0x000B2036 File Offset: 0x000B0236
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17005014 RID: 20500
			// (set) Token: 0x060076C9 RID: 30409 RVA: 0x000B2049 File Offset: 0x000B0249
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17005015 RID: 20501
			// (set) Token: 0x060076CA RID: 30410 RVA: 0x000B2061 File Offset: 0x000B0261
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005016 RID: 20502
			// (set) Token: 0x060076CB RID: 30411 RVA: 0x000B2079 File Offset: 0x000B0279
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005017 RID: 20503
			// (set) Token: 0x060076CC RID: 30412 RVA: 0x000B2091 File Offset: 0x000B0291
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005018 RID: 20504
			// (set) Token: 0x060076CD RID: 30413 RVA: 0x000B20A4 File Offset: 0x000B02A4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005019 RID: 20505
			// (set) Token: 0x060076CE RID: 30414 RVA: 0x000B20BC File Offset: 0x000B02BC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700501A RID: 20506
			// (set) Token: 0x060076CF RID: 30415 RVA: 0x000B20D4 File Offset: 0x000B02D4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700501B RID: 20507
			// (set) Token: 0x060076D0 RID: 30416 RVA: 0x000B20EC File Offset: 0x000B02EC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000937 RID: 2359
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700501C RID: 20508
			// (set) Token: 0x060076D2 RID: 30418 RVA: 0x000B210C File Offset: 0x000B030C
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700501D RID: 20509
			// (set) Token: 0x060076D3 RID: 30419 RVA: 0x000B211F File Offset: 0x000B031F
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x1700501E RID: 20510
			// (set) Token: 0x060076D4 RID: 30420 RVA: 0x000B2137 File Offset: 0x000B0337
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x1700501F RID: 20511
			// (set) Token: 0x060076D5 RID: 30421 RVA: 0x000B214F File Offset: 0x000B034F
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005020 RID: 20512
			// (set) Token: 0x060076D6 RID: 30422 RVA: 0x000B2167 File Offset: 0x000B0367
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005021 RID: 20513
			// (set) Token: 0x060076D7 RID: 30423 RVA: 0x000B217A File Offset: 0x000B037A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005022 RID: 20514
			// (set) Token: 0x060076D8 RID: 30424 RVA: 0x000B2192 File Offset: 0x000B0392
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005023 RID: 20515
			// (set) Token: 0x060076D9 RID: 30425 RVA: 0x000B21AA File Offset: 0x000B03AA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005024 RID: 20516
			// (set) Token: 0x060076DA RID: 30426 RVA: 0x000B21C2 File Offset: 0x000B03C2
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
