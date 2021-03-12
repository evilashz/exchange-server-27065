using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000930 RID: 2352
	public class GetOutlookAnywhereCommand : SyntheticCommandWithPipelineInput<ADRpcHttpVirtualDirectory, ADRpcHttpVirtualDirectory>
	{
		// Token: 0x06007698 RID: 30360 RVA: 0x000B1C90 File Offset: 0x000AFE90
		private GetOutlookAnywhereCommand() : base("Get-OutlookAnywhere")
		{
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x000B1C9D File Offset: 0x000AFE9D
		public GetOutlookAnywhereCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600769A RID: 30362 RVA: 0x000B1CAC File Offset: 0x000AFEAC
		public virtual GetOutlookAnywhereCommand SetParameters(GetOutlookAnywhereCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x000B1CB6 File Offset: 0x000AFEB6
		public virtual GetOutlookAnywhereCommand SetParameters(GetOutlookAnywhereCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600769C RID: 30364 RVA: 0x000B1CC0 File Offset: 0x000AFEC0
		public virtual GetOutlookAnywhereCommand SetParameters(GetOutlookAnywhereCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000931 RID: 2353
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004FF1 RID: 20465
			// (set) Token: 0x0600769D RID: 30365 RVA: 0x000B1CCA File Offset: 0x000AFECA
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FF2 RID: 20466
			// (set) Token: 0x0600769E RID: 30366 RVA: 0x000B1CE2 File Offset: 0x000AFEE2
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FF3 RID: 20467
			// (set) Token: 0x0600769F RID: 30367 RVA: 0x000B1CFA File Offset: 0x000AFEFA
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FF4 RID: 20468
			// (set) Token: 0x060076A0 RID: 30368 RVA: 0x000B1D12 File Offset: 0x000AFF12
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FF5 RID: 20469
			// (set) Token: 0x060076A1 RID: 30369 RVA: 0x000B1D25 File Offset: 0x000AFF25
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FF6 RID: 20470
			// (set) Token: 0x060076A2 RID: 30370 RVA: 0x000B1D3D File Offset: 0x000AFF3D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004FF7 RID: 20471
			// (set) Token: 0x060076A3 RID: 30371 RVA: 0x000B1D55 File Offset: 0x000AFF55
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004FF8 RID: 20472
			// (set) Token: 0x060076A4 RID: 30372 RVA: 0x000B1D6D File Offset: 0x000AFF6D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000932 RID: 2354
		public class ServerParameters : ParametersBase
		{
			// Token: 0x17004FF9 RID: 20473
			// (set) Token: 0x060076A6 RID: 30374 RVA: 0x000B1D8D File Offset: 0x000AFF8D
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004FFA RID: 20474
			// (set) Token: 0x060076A7 RID: 30375 RVA: 0x000B1DA0 File Offset: 0x000AFFA0
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17004FFB RID: 20475
			// (set) Token: 0x060076A8 RID: 30376 RVA: 0x000B1DB8 File Offset: 0x000AFFB8
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FFC RID: 20476
			// (set) Token: 0x060076A9 RID: 30377 RVA: 0x000B1DD0 File Offset: 0x000AFFD0
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17004FFD RID: 20477
			// (set) Token: 0x060076AA RID: 30378 RVA: 0x000B1DE8 File Offset: 0x000AFFE8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004FFE RID: 20478
			// (set) Token: 0x060076AB RID: 30379 RVA: 0x000B1DFB File Offset: 0x000AFFFB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004FFF RID: 20479
			// (set) Token: 0x060076AC RID: 30380 RVA: 0x000B1E13 File Offset: 0x000B0013
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005000 RID: 20480
			// (set) Token: 0x060076AD RID: 30381 RVA: 0x000B1E2B File Offset: 0x000B002B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005001 RID: 20481
			// (set) Token: 0x060076AE RID: 30382 RVA: 0x000B1E43 File Offset: 0x000B0043
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000933 RID: 2355
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005002 RID: 20482
			// (set) Token: 0x060076B0 RID: 30384 RVA: 0x000B1E63 File Offset: 0x000B0063
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005003 RID: 20483
			// (set) Token: 0x060076B1 RID: 30385 RVA: 0x000B1E76 File Offset: 0x000B0076
			public virtual SwitchParameter ADPropertiesOnly
			{
				set
				{
					base.PowerSharpParameters["ADPropertiesOnly"] = value;
				}
			}

			// Token: 0x17005004 RID: 20484
			// (set) Token: 0x060076B2 RID: 30386 RVA: 0x000B1E8E File Offset: 0x000B008E
			public virtual SwitchParameter ShowMailboxVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowMailboxVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005005 RID: 20485
			// (set) Token: 0x060076B3 RID: 30387 RVA: 0x000B1EA6 File Offset: 0x000B00A6
			public virtual SwitchParameter ShowBackEndVirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["ShowBackEndVirtualDirectories"] = value;
				}
			}

			// Token: 0x17005006 RID: 20486
			// (set) Token: 0x060076B4 RID: 30388 RVA: 0x000B1EBE File Offset: 0x000B00BE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005007 RID: 20487
			// (set) Token: 0x060076B5 RID: 30389 RVA: 0x000B1ED1 File Offset: 0x000B00D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005008 RID: 20488
			// (set) Token: 0x060076B6 RID: 30390 RVA: 0x000B1EE9 File Offset: 0x000B00E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005009 RID: 20489
			// (set) Token: 0x060076B7 RID: 30391 RVA: 0x000B1F01 File Offset: 0x000B0101
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700500A RID: 20490
			// (set) Token: 0x060076B8 RID: 30392 RVA: 0x000B1F19 File Offset: 0x000B0119
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
