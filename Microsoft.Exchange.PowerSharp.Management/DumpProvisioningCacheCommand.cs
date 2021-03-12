using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200030B RID: 779
	public class DumpProvisioningCacheCommand : SyntheticCommandWithPipelineInputNoOutput<Fqdn>
	{
		// Token: 0x060033B6 RID: 13238 RVA: 0x0005AEE0 File Offset: 0x000590E0
		private DumpProvisioningCacheCommand() : base("Dump-ProvisioningCache")
		{
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x0005AEED File Offset: 0x000590ED
		public DumpProvisioningCacheCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x0005AEFC File Offset: 0x000590FC
		public virtual DumpProvisioningCacheCommand SetParameters(DumpProvisioningCacheCommand.OrganizationCacheParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x0005AF06 File Offset: 0x00059106
		public virtual DumpProvisioningCacheCommand SetParameters(DumpProvisioningCacheCommand.GlobalCacheParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x0005AF10 File Offset: 0x00059110
		public virtual DumpProvisioningCacheCommand SetParameters(DumpProvisioningCacheCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200030C RID: 780
		public class OrganizationCacheParameters : ParametersBase
		{
			// Token: 0x17001959 RID: 6489
			// (set) Token: 0x060033BB RID: 13243 RVA: 0x0005AF1A File Offset: 0x0005911A
			public virtual Fqdn Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700195A RID: 6490
			// (set) Token: 0x060033BC RID: 13244 RVA: 0x0005AF2D File Offset: 0x0005912D
			public virtual string Application
			{
				set
				{
					base.PowerSharpParameters["Application"] = value;
				}
			}

			// Token: 0x1700195B RID: 6491
			// (set) Token: 0x060033BD RID: 13245 RVA: 0x0005AF40 File Offset: 0x00059140
			public virtual MultiValuedProperty<OrganizationIdParameter> Organizations
			{
				set
				{
					base.PowerSharpParameters["Organizations"] = value;
				}
			}

			// Token: 0x1700195C RID: 6492
			// (set) Token: 0x060033BE RID: 13246 RVA: 0x0005AF53 File Offset: 0x00059153
			public virtual SwitchParameter CurrentOrganization
			{
				set
				{
					base.PowerSharpParameters["CurrentOrganization"] = value;
				}
			}

			// Token: 0x1700195D RID: 6493
			// (set) Token: 0x060033BF RID: 13247 RVA: 0x0005AF6B File Offset: 0x0005916B
			public virtual MultiValuedProperty<Guid> CacheKeys
			{
				set
				{
					base.PowerSharpParameters["CacheKeys"] = value;
				}
			}

			// Token: 0x1700195E RID: 6494
			// (set) Token: 0x060033C0 RID: 13248 RVA: 0x0005AF7E File Offset: 0x0005917E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700195F RID: 6495
			// (set) Token: 0x060033C1 RID: 13249 RVA: 0x0005AF96 File Offset: 0x00059196
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001960 RID: 6496
			// (set) Token: 0x060033C2 RID: 13250 RVA: 0x0005AFAE File Offset: 0x000591AE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001961 RID: 6497
			// (set) Token: 0x060033C3 RID: 13251 RVA: 0x0005AFC6 File Offset: 0x000591C6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001962 RID: 6498
			// (set) Token: 0x060033C4 RID: 13252 RVA: 0x0005AFDE File Offset: 0x000591DE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200030D RID: 781
		public class GlobalCacheParameters : ParametersBase
		{
			// Token: 0x17001963 RID: 6499
			// (set) Token: 0x060033C6 RID: 13254 RVA: 0x0005AFFE File Offset: 0x000591FE
			public virtual Fqdn Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17001964 RID: 6500
			// (set) Token: 0x060033C7 RID: 13255 RVA: 0x0005B011 File Offset: 0x00059211
			public virtual string Application
			{
				set
				{
					base.PowerSharpParameters["Application"] = value;
				}
			}

			// Token: 0x17001965 RID: 6501
			// (set) Token: 0x060033C8 RID: 13256 RVA: 0x0005B024 File Offset: 0x00059224
			public virtual SwitchParameter GlobalCache
			{
				set
				{
					base.PowerSharpParameters["GlobalCache"] = value;
				}
			}

			// Token: 0x17001966 RID: 6502
			// (set) Token: 0x060033C9 RID: 13257 RVA: 0x0005B03C File Offset: 0x0005923C
			public virtual MultiValuedProperty<Guid> CacheKeys
			{
				set
				{
					base.PowerSharpParameters["CacheKeys"] = value;
				}
			}

			// Token: 0x17001967 RID: 6503
			// (set) Token: 0x060033CA RID: 13258 RVA: 0x0005B04F File Offset: 0x0005924F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001968 RID: 6504
			// (set) Token: 0x060033CB RID: 13259 RVA: 0x0005B067 File Offset: 0x00059267
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001969 RID: 6505
			// (set) Token: 0x060033CC RID: 13260 RVA: 0x0005B07F File Offset: 0x0005927F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700196A RID: 6506
			// (set) Token: 0x060033CD RID: 13261 RVA: 0x0005B097 File Offset: 0x00059297
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700196B RID: 6507
			// (set) Token: 0x060033CE RID: 13262 RVA: 0x0005B0AF File Offset: 0x000592AF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200030E RID: 782
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700196C RID: 6508
			// (set) Token: 0x060033D0 RID: 13264 RVA: 0x0005B0CF File Offset: 0x000592CF
			public virtual MultiValuedProperty<Guid> CacheKeys
			{
				set
				{
					base.PowerSharpParameters["CacheKeys"] = value;
				}
			}

			// Token: 0x1700196D RID: 6509
			// (set) Token: 0x060033D1 RID: 13265 RVA: 0x0005B0E2 File Offset: 0x000592E2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700196E RID: 6510
			// (set) Token: 0x060033D2 RID: 13266 RVA: 0x0005B0FA File Offset: 0x000592FA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700196F RID: 6511
			// (set) Token: 0x060033D3 RID: 13267 RVA: 0x0005B112 File Offset: 0x00059312
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001970 RID: 6512
			// (set) Token: 0x060033D4 RID: 13268 RVA: 0x0005B12A File Offset: 0x0005932A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001971 RID: 6513
			// (set) Token: 0x060033D5 RID: 13269 RVA: 0x0005B142 File Offset: 0x00059342
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
