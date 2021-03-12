using System;
using System.Management.Automation;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008EF RID: 2287
	public class GetTransportRulePredicateCommand : SyntheticCommandWithPipelineInput<TransportRulePredicate, TransportRulePredicate>
	{
		// Token: 0x06007290 RID: 29328 RVA: 0x000AC669 File Offset: 0x000AA869
		private GetTransportRulePredicateCommand() : base("Get-TransportRulePredicate")
		{
		}

		// Token: 0x06007291 RID: 29329 RVA: 0x000AC676 File Offset: 0x000AA876
		public GetTransportRulePredicateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007292 RID: 29330 RVA: 0x000AC685 File Offset: 0x000AA885
		public virtual GetTransportRulePredicateCommand SetParameters(GetTransportRulePredicateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008F0 RID: 2288
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004C6B RID: 19563
			// (set) Token: 0x06007293 RID: 29331 RVA: 0x000AC68F File Offset: 0x000AA88F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004C6C RID: 19564
			// (set) Token: 0x06007294 RID: 29332 RVA: 0x000AC6A2 File Offset: 0x000AA8A2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C6D RID: 19565
			// (set) Token: 0x06007295 RID: 29333 RVA: 0x000AC6BA File Offset: 0x000AA8BA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C6E RID: 19566
			// (set) Token: 0x06007296 RID: 29334 RVA: 0x000AC6D2 File Offset: 0x000AA8D2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C6F RID: 19567
			// (set) Token: 0x06007297 RID: 29335 RVA: 0x000AC6EA File Offset: 0x000AA8EA
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
