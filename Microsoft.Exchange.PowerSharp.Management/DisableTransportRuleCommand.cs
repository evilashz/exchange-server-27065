using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008E1 RID: 2273
	public class DisableTransportRuleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x0600722B RID: 29227 RVA: 0x000ABE74 File Offset: 0x000AA074
		private DisableTransportRuleCommand() : base("Disable-TransportRule")
		{
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x000ABE81 File Offset: 0x000AA081
		public DisableTransportRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x000ABE90 File Offset: 0x000AA090
		public virtual DisableTransportRuleCommand SetParameters(DisableTransportRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x000ABE9A File Offset: 0x000AA09A
		public virtual DisableTransportRuleCommand SetParameters(DisableTransportRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008E2 RID: 2274
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004C22 RID: 19490
			// (set) Token: 0x0600722F RID: 29231 RVA: 0x000ABEA4 File Offset: 0x000AA0A4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C23 RID: 19491
			// (set) Token: 0x06007230 RID: 29232 RVA: 0x000ABEB7 File Offset: 0x000AA0B7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C24 RID: 19492
			// (set) Token: 0x06007231 RID: 29233 RVA: 0x000ABECF File Offset: 0x000AA0CF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C25 RID: 19493
			// (set) Token: 0x06007232 RID: 29234 RVA: 0x000ABEE7 File Offset: 0x000AA0E7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C26 RID: 19494
			// (set) Token: 0x06007233 RID: 29235 RVA: 0x000ABEFF File Offset: 0x000AA0FF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C27 RID: 19495
			// (set) Token: 0x06007234 RID: 29236 RVA: 0x000ABF17 File Offset: 0x000AA117
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004C28 RID: 19496
			// (set) Token: 0x06007235 RID: 29237 RVA: 0x000ABF2F File Offset: 0x000AA12F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008E3 RID: 2275
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004C29 RID: 19497
			// (set) Token: 0x06007237 RID: 29239 RVA: 0x000ABF4F File Offset: 0x000AA14F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004C2A RID: 19498
			// (set) Token: 0x06007238 RID: 29240 RVA: 0x000ABF6D File Offset: 0x000AA16D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C2B RID: 19499
			// (set) Token: 0x06007239 RID: 29241 RVA: 0x000ABF80 File Offset: 0x000AA180
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C2C RID: 19500
			// (set) Token: 0x0600723A RID: 29242 RVA: 0x000ABF98 File Offset: 0x000AA198
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C2D RID: 19501
			// (set) Token: 0x0600723B RID: 29243 RVA: 0x000ABFB0 File Offset: 0x000AA1B0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C2E RID: 19502
			// (set) Token: 0x0600723C RID: 29244 RVA: 0x000ABFC8 File Offset: 0x000AA1C8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C2F RID: 19503
			// (set) Token: 0x0600723D RID: 29245 RVA: 0x000ABFE0 File Offset: 0x000AA1E0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004C30 RID: 19504
			// (set) Token: 0x0600723E RID: 29246 RVA: 0x000ABFF8 File Offset: 0x000AA1F8
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
