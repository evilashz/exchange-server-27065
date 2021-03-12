using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008EA RID: 2282
	public class GetTransportRuleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x0600726C RID: 29292 RVA: 0x000AC39C File Offset: 0x000AA59C
		private GetTransportRuleCommand() : base("Get-TransportRule")
		{
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x000AC3A9 File Offset: 0x000AA5A9
		public GetTransportRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600726E RID: 29294 RVA: 0x000AC3B8 File Offset: 0x000AA5B8
		public virtual GetTransportRuleCommand SetParameters(GetTransportRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600726F RID: 29295 RVA: 0x000AC3C2 File Offset: 0x000AA5C2
		public virtual GetTransportRuleCommand SetParameters(GetTransportRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008EB RID: 2283
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004C51 RID: 19537
			// (set) Token: 0x06007270 RID: 29296 RVA: 0x000AC3CC File Offset: 0x000AA5CC
			public virtual RuleState State
			{
				set
				{
					base.PowerSharpParameters["State"] = value;
				}
			}

			// Token: 0x17004C52 RID: 19538
			// (set) Token: 0x06007271 RID: 29297 RVA: 0x000AC3E4 File Offset: 0x000AA5E4
			public virtual string DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17004C53 RID: 19539
			// (set) Token: 0x06007272 RID: 29298 RVA: 0x000AC3F7 File Offset: 0x000AA5F7
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17004C54 RID: 19540
			// (set) Token: 0x06007273 RID: 29299 RVA: 0x000AC40A File Offset: 0x000AA60A
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17004C55 RID: 19541
			// (set) Token: 0x06007274 RID: 29300 RVA: 0x000AC422 File Offset: 0x000AA622
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004C56 RID: 19542
			// (set) Token: 0x06007275 RID: 29301 RVA: 0x000AC440 File Offset: 0x000AA640
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C57 RID: 19543
			// (set) Token: 0x06007276 RID: 29302 RVA: 0x000AC453 File Offset: 0x000AA653
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C58 RID: 19544
			// (set) Token: 0x06007277 RID: 29303 RVA: 0x000AC46B File Offset: 0x000AA66B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C59 RID: 19545
			// (set) Token: 0x06007278 RID: 29304 RVA: 0x000AC483 File Offset: 0x000AA683
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C5A RID: 19546
			// (set) Token: 0x06007279 RID: 29305 RVA: 0x000AC49B File Offset: 0x000AA69B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020008EC RID: 2284
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004C5B RID: 19547
			// (set) Token: 0x0600727B RID: 29307 RVA: 0x000AC4BB File Offset: 0x000AA6BB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004C5C RID: 19548
			// (set) Token: 0x0600727C RID: 29308 RVA: 0x000AC4D9 File Offset: 0x000AA6D9
			public virtual RuleState State
			{
				set
				{
					base.PowerSharpParameters["State"] = value;
				}
			}

			// Token: 0x17004C5D RID: 19549
			// (set) Token: 0x0600727D RID: 29309 RVA: 0x000AC4F1 File Offset: 0x000AA6F1
			public virtual string DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17004C5E RID: 19550
			// (set) Token: 0x0600727E RID: 29310 RVA: 0x000AC504 File Offset: 0x000AA704
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17004C5F RID: 19551
			// (set) Token: 0x0600727F RID: 29311 RVA: 0x000AC517 File Offset: 0x000AA717
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17004C60 RID: 19552
			// (set) Token: 0x06007280 RID: 29312 RVA: 0x000AC52F File Offset: 0x000AA72F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004C61 RID: 19553
			// (set) Token: 0x06007281 RID: 29313 RVA: 0x000AC54D File Offset: 0x000AA74D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C62 RID: 19554
			// (set) Token: 0x06007282 RID: 29314 RVA: 0x000AC560 File Offset: 0x000AA760
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C63 RID: 19555
			// (set) Token: 0x06007283 RID: 29315 RVA: 0x000AC578 File Offset: 0x000AA778
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C64 RID: 19556
			// (set) Token: 0x06007284 RID: 29316 RVA: 0x000AC590 File Offset: 0x000AA790
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C65 RID: 19557
			// (set) Token: 0x06007285 RID: 29317 RVA: 0x000AC5A8 File Offset: 0x000AA7A8
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
