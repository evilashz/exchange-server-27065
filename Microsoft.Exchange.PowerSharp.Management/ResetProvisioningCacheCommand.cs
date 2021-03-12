using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200030F RID: 783
	public class ResetProvisioningCacheCommand : SyntheticCommandWithPipelineInputNoOutput<Fqdn>
	{
		// Token: 0x060033D7 RID: 13271 RVA: 0x0005B162 File Offset: 0x00059362
		private ResetProvisioningCacheCommand() : base("Reset-ProvisioningCache")
		{
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x0005B16F File Offset: 0x0005936F
		public ResetProvisioningCacheCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x0005B17E File Offset: 0x0005937E
		public virtual ResetProvisioningCacheCommand SetParameters(ResetProvisioningCacheCommand.OrganizationCacheParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x0005B188 File Offset: 0x00059388
		public virtual ResetProvisioningCacheCommand SetParameters(ResetProvisioningCacheCommand.GlobalCacheParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x0005B192 File Offset: 0x00059392
		public virtual ResetProvisioningCacheCommand SetParameters(ResetProvisioningCacheCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000310 RID: 784
		public class OrganizationCacheParameters : ParametersBase
		{
			// Token: 0x17001972 RID: 6514
			// (set) Token: 0x060033DC RID: 13276 RVA: 0x0005B19C File Offset: 0x0005939C
			public virtual Fqdn Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17001973 RID: 6515
			// (set) Token: 0x060033DD RID: 13277 RVA: 0x0005B1AF File Offset: 0x000593AF
			public virtual string Application
			{
				set
				{
					base.PowerSharpParameters["Application"] = value;
				}
			}

			// Token: 0x17001974 RID: 6516
			// (set) Token: 0x060033DE RID: 13278 RVA: 0x0005B1C2 File Offset: 0x000593C2
			public virtual MultiValuedProperty<OrganizationIdParameter> Organizations
			{
				set
				{
					base.PowerSharpParameters["Organizations"] = value;
				}
			}

			// Token: 0x17001975 RID: 6517
			// (set) Token: 0x060033DF RID: 13279 RVA: 0x0005B1D5 File Offset: 0x000593D5
			public virtual SwitchParameter CurrentOrganization
			{
				set
				{
					base.PowerSharpParameters["CurrentOrganization"] = value;
				}
			}

			// Token: 0x17001976 RID: 6518
			// (set) Token: 0x060033E0 RID: 13280 RVA: 0x0005B1ED File Offset: 0x000593ED
			public virtual MultiValuedProperty<Guid> CacheKeys
			{
				set
				{
					base.PowerSharpParameters["CacheKeys"] = value;
				}
			}

			// Token: 0x17001977 RID: 6519
			// (set) Token: 0x060033E1 RID: 13281 RVA: 0x0005B200 File Offset: 0x00059400
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001978 RID: 6520
			// (set) Token: 0x060033E2 RID: 13282 RVA: 0x0005B218 File Offset: 0x00059418
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001979 RID: 6521
			// (set) Token: 0x060033E3 RID: 13283 RVA: 0x0005B230 File Offset: 0x00059430
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700197A RID: 6522
			// (set) Token: 0x060033E4 RID: 13284 RVA: 0x0005B248 File Offset: 0x00059448
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700197B RID: 6523
			// (set) Token: 0x060033E5 RID: 13285 RVA: 0x0005B260 File Offset: 0x00059460
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000311 RID: 785
		public class GlobalCacheParameters : ParametersBase
		{
			// Token: 0x1700197C RID: 6524
			// (set) Token: 0x060033E7 RID: 13287 RVA: 0x0005B280 File Offset: 0x00059480
			public virtual Fqdn Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700197D RID: 6525
			// (set) Token: 0x060033E8 RID: 13288 RVA: 0x0005B293 File Offset: 0x00059493
			public virtual string Application
			{
				set
				{
					base.PowerSharpParameters["Application"] = value;
				}
			}

			// Token: 0x1700197E RID: 6526
			// (set) Token: 0x060033E9 RID: 13289 RVA: 0x0005B2A6 File Offset: 0x000594A6
			public virtual SwitchParameter GlobalCache
			{
				set
				{
					base.PowerSharpParameters["GlobalCache"] = value;
				}
			}

			// Token: 0x1700197F RID: 6527
			// (set) Token: 0x060033EA RID: 13290 RVA: 0x0005B2BE File Offset: 0x000594BE
			public virtual MultiValuedProperty<Guid> CacheKeys
			{
				set
				{
					base.PowerSharpParameters["CacheKeys"] = value;
				}
			}

			// Token: 0x17001980 RID: 6528
			// (set) Token: 0x060033EB RID: 13291 RVA: 0x0005B2D1 File Offset: 0x000594D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001981 RID: 6529
			// (set) Token: 0x060033EC RID: 13292 RVA: 0x0005B2E9 File Offset: 0x000594E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001982 RID: 6530
			// (set) Token: 0x060033ED RID: 13293 RVA: 0x0005B301 File Offset: 0x00059501
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001983 RID: 6531
			// (set) Token: 0x060033EE RID: 13294 RVA: 0x0005B319 File Offset: 0x00059519
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001984 RID: 6532
			// (set) Token: 0x060033EF RID: 13295 RVA: 0x0005B331 File Offset: 0x00059531
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000312 RID: 786
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001985 RID: 6533
			// (set) Token: 0x060033F1 RID: 13297 RVA: 0x0005B351 File Offset: 0x00059551
			public virtual MultiValuedProperty<Guid> CacheKeys
			{
				set
				{
					base.PowerSharpParameters["CacheKeys"] = value;
				}
			}

			// Token: 0x17001986 RID: 6534
			// (set) Token: 0x060033F2 RID: 13298 RVA: 0x0005B364 File Offset: 0x00059564
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001987 RID: 6535
			// (set) Token: 0x060033F3 RID: 13299 RVA: 0x0005B37C File Offset: 0x0005957C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001988 RID: 6536
			// (set) Token: 0x060033F4 RID: 13300 RVA: 0x0005B394 File Offset: 0x00059594
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001989 RID: 6537
			// (set) Token: 0x060033F5 RID: 13301 RVA: 0x0005B3AC File Offset: 0x000595AC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700198A RID: 6538
			// (set) Token: 0x060033F6 RID: 13302 RVA: 0x0005B3C4 File Offset: 0x000595C4
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
