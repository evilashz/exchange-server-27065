using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008E4 RID: 2276
	public class EnableTransportRuleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x06007240 RID: 29248 RVA: 0x000AC018 File Offset: 0x000AA218
		private EnableTransportRuleCommand() : base("Enable-TransportRule")
		{
		}

		// Token: 0x06007241 RID: 29249 RVA: 0x000AC025 File Offset: 0x000AA225
		public EnableTransportRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007242 RID: 29250 RVA: 0x000AC034 File Offset: 0x000AA234
		public virtual EnableTransportRuleCommand SetParameters(EnableTransportRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x000AC03E File Offset: 0x000AA23E
		public virtual EnableTransportRuleCommand SetParameters(EnableTransportRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008E5 RID: 2277
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004C31 RID: 19505
			// (set) Token: 0x06007244 RID: 29252 RVA: 0x000AC048 File Offset: 0x000AA248
			public virtual RuleMode Mode
			{
				set
				{
					base.PowerSharpParameters["Mode"] = value;
				}
			}

			// Token: 0x17004C32 RID: 19506
			// (set) Token: 0x06007245 RID: 29253 RVA: 0x000AC060 File Offset: 0x000AA260
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C33 RID: 19507
			// (set) Token: 0x06007246 RID: 29254 RVA: 0x000AC073 File Offset: 0x000AA273
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C34 RID: 19508
			// (set) Token: 0x06007247 RID: 29255 RVA: 0x000AC08B File Offset: 0x000AA28B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C35 RID: 19509
			// (set) Token: 0x06007248 RID: 29256 RVA: 0x000AC0A3 File Offset: 0x000AA2A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C36 RID: 19510
			// (set) Token: 0x06007249 RID: 29257 RVA: 0x000AC0BB File Offset: 0x000AA2BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C37 RID: 19511
			// (set) Token: 0x0600724A RID: 29258 RVA: 0x000AC0D3 File Offset: 0x000AA2D3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008E6 RID: 2278
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004C38 RID: 19512
			// (set) Token: 0x0600724C RID: 29260 RVA: 0x000AC0F3 File Offset: 0x000AA2F3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004C39 RID: 19513
			// (set) Token: 0x0600724D RID: 29261 RVA: 0x000AC111 File Offset: 0x000AA311
			public virtual RuleMode Mode
			{
				set
				{
					base.PowerSharpParameters["Mode"] = value;
				}
			}

			// Token: 0x17004C3A RID: 19514
			// (set) Token: 0x0600724E RID: 29262 RVA: 0x000AC129 File Offset: 0x000AA329
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C3B RID: 19515
			// (set) Token: 0x0600724F RID: 29263 RVA: 0x000AC13C File Offset: 0x000AA33C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C3C RID: 19516
			// (set) Token: 0x06007250 RID: 29264 RVA: 0x000AC154 File Offset: 0x000AA354
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C3D RID: 19517
			// (set) Token: 0x06007251 RID: 29265 RVA: 0x000AC16C File Offset: 0x000AA36C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C3E RID: 19518
			// (set) Token: 0x06007252 RID: 29266 RVA: 0x000AC184 File Offset: 0x000AA384
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C3F RID: 19519
			// (set) Token: 0x06007253 RID: 29267 RVA: 0x000AC19C File Offset: 0x000AA39C
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
