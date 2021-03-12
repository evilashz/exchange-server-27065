using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008F6 RID: 2294
	public class RemoveTransportRuleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x06007369 RID: 29545 RVA: 0x000AD946 File Offset: 0x000ABB46
		private RemoveTransportRuleCommand() : base("Remove-TransportRule")
		{
		}

		// Token: 0x0600736A RID: 29546 RVA: 0x000AD953 File Offset: 0x000ABB53
		public RemoveTransportRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600736B RID: 29547 RVA: 0x000AD962 File Offset: 0x000ABB62
		public virtual RemoveTransportRuleCommand SetParameters(RemoveTransportRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600736C RID: 29548 RVA: 0x000AD96C File Offset: 0x000ABB6C
		public virtual RemoveTransportRuleCommand SetParameters(RemoveTransportRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008F7 RID: 2295
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004D36 RID: 19766
			// (set) Token: 0x0600736D RID: 29549 RVA: 0x000AD976 File Offset: 0x000ABB76
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004D37 RID: 19767
			// (set) Token: 0x0600736E RID: 29550 RVA: 0x000AD989 File Offset: 0x000ABB89
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004D38 RID: 19768
			// (set) Token: 0x0600736F RID: 29551 RVA: 0x000AD9A1 File Offset: 0x000ABBA1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004D39 RID: 19769
			// (set) Token: 0x06007370 RID: 29552 RVA: 0x000AD9B9 File Offset: 0x000ABBB9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004D3A RID: 19770
			// (set) Token: 0x06007371 RID: 29553 RVA: 0x000AD9D1 File Offset: 0x000ABBD1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004D3B RID: 19771
			// (set) Token: 0x06007372 RID: 29554 RVA: 0x000AD9E9 File Offset: 0x000ABBE9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004D3C RID: 19772
			// (set) Token: 0x06007373 RID: 29555 RVA: 0x000ADA01 File Offset: 0x000ABC01
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008F8 RID: 2296
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004D3D RID: 19773
			// (set) Token: 0x06007375 RID: 29557 RVA: 0x000ADA21 File Offset: 0x000ABC21
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004D3E RID: 19774
			// (set) Token: 0x06007376 RID: 29558 RVA: 0x000ADA3F File Offset: 0x000ABC3F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004D3F RID: 19775
			// (set) Token: 0x06007377 RID: 29559 RVA: 0x000ADA52 File Offset: 0x000ABC52
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004D40 RID: 19776
			// (set) Token: 0x06007378 RID: 29560 RVA: 0x000ADA6A File Offset: 0x000ABC6A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004D41 RID: 19777
			// (set) Token: 0x06007379 RID: 29561 RVA: 0x000ADA82 File Offset: 0x000ABC82
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004D42 RID: 19778
			// (set) Token: 0x0600737A RID: 29562 RVA: 0x000ADA9A File Offset: 0x000ABC9A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004D43 RID: 19779
			// (set) Token: 0x0600737B RID: 29563 RVA: 0x000ADAB2 File Offset: 0x000ABCB2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004D44 RID: 19780
			// (set) Token: 0x0600737C RID: 29564 RVA: 0x000ADACA File Offset: 0x000ABCCA
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
