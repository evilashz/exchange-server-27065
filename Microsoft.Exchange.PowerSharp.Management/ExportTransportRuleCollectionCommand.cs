using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008E7 RID: 2279
	public class ExportTransportRuleCollectionCommand : SyntheticCommand<object>
	{
		// Token: 0x06007255 RID: 29269 RVA: 0x000AC1BC File Offset: 0x000AA3BC
		private ExportTransportRuleCollectionCommand() : base("Export-TransportRuleCollection")
		{
		}

		// Token: 0x06007256 RID: 29270 RVA: 0x000AC1C9 File Offset: 0x000AA3C9
		public ExportTransportRuleCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007257 RID: 29271 RVA: 0x000AC1D8 File Offset: 0x000AA3D8
		public virtual ExportTransportRuleCollectionCommand SetParameters(ExportTransportRuleCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007258 RID: 29272 RVA: 0x000AC1E2 File Offset: 0x000AA3E2
		public virtual ExportTransportRuleCollectionCommand SetParameters(ExportTransportRuleCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008E8 RID: 2280
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004C40 RID: 19520
			// (set) Token: 0x06007259 RID: 29273 RVA: 0x000AC1EC File Offset: 0x000AA3EC
			public virtual RuleCollectionFormat Format
			{
				set
				{
					base.PowerSharpParameters["Format"] = value;
				}
			}

			// Token: 0x17004C41 RID: 19521
			// (set) Token: 0x0600725A RID: 29274 RVA: 0x000AC204 File Offset: 0x000AA404
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004C42 RID: 19522
			// (set) Token: 0x0600725B RID: 29275 RVA: 0x000AC222 File Offset: 0x000AA422
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C43 RID: 19523
			// (set) Token: 0x0600725C RID: 29276 RVA: 0x000AC235 File Offset: 0x000AA435
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C44 RID: 19524
			// (set) Token: 0x0600725D RID: 29277 RVA: 0x000AC24D File Offset: 0x000AA44D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C45 RID: 19525
			// (set) Token: 0x0600725E RID: 29278 RVA: 0x000AC265 File Offset: 0x000AA465
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C46 RID: 19526
			// (set) Token: 0x0600725F RID: 29279 RVA: 0x000AC27D File Offset: 0x000AA47D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C47 RID: 19527
			// (set) Token: 0x06007260 RID: 29280 RVA: 0x000AC295 File Offset: 0x000AA495
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008E9 RID: 2281
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004C48 RID: 19528
			// (set) Token: 0x06007262 RID: 29282 RVA: 0x000AC2B5 File Offset: 0x000AA4B5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004C49 RID: 19529
			// (set) Token: 0x06007263 RID: 29283 RVA: 0x000AC2D3 File Offset: 0x000AA4D3
			public virtual RuleCollectionFormat Format
			{
				set
				{
					base.PowerSharpParameters["Format"] = value;
				}
			}

			// Token: 0x17004C4A RID: 19530
			// (set) Token: 0x06007264 RID: 29284 RVA: 0x000AC2EB File Offset: 0x000AA4EB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004C4B RID: 19531
			// (set) Token: 0x06007265 RID: 29285 RVA: 0x000AC309 File Offset: 0x000AA509
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C4C RID: 19532
			// (set) Token: 0x06007266 RID: 29286 RVA: 0x000AC31C File Offset: 0x000AA51C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C4D RID: 19533
			// (set) Token: 0x06007267 RID: 29287 RVA: 0x000AC334 File Offset: 0x000AA534
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C4E RID: 19534
			// (set) Token: 0x06007268 RID: 29288 RVA: 0x000AC34C File Offset: 0x000AA54C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C4F RID: 19535
			// (set) Token: 0x06007269 RID: 29289 RVA: 0x000AC364 File Offset: 0x000AA564
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C50 RID: 19536
			// (set) Token: 0x0600726A RID: 29290 RVA: 0x000AC37C File Offset: 0x000AA57C
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
