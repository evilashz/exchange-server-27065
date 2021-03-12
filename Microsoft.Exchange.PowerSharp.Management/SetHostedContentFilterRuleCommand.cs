using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000728 RID: 1832
	public class SetHostedContentFilterRuleCommand : SyntheticCommandWithPipelineInputNoOutput<HostedContentFilterRule>
	{
		// Token: 0x06005E68 RID: 24168 RVA: 0x00092246 File Offset: 0x00090446
		private SetHostedContentFilterRuleCommand() : base("Set-HostedContentFilterRule")
		{
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x00092253 File Offset: 0x00090453
		public SetHostedContentFilterRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x00092262 File Offset: 0x00090462
		public virtual SetHostedContentFilterRuleCommand SetParameters(SetHostedContentFilterRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x0009226C File Offset: 0x0009046C
		public virtual SetHostedContentFilterRuleCommand SetParameters(SetHostedContentFilterRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000729 RID: 1833
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003BD1 RID: 15313
			// (set) Token: 0x06005E6C RID: 24172 RVA: 0x00092276 File Offset: 0x00090476
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17003BD2 RID: 15314
			// (set) Token: 0x06005E6D RID: 24173 RVA: 0x00092289 File Offset: 0x00090489
			public virtual RecipientIdParameter SentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["SentToMemberOf"] = value;
				}
			}

			// Token: 0x17003BD3 RID: 15315
			// (set) Token: 0x06005E6E RID: 24174 RVA: 0x0009229C File Offset: 0x0009049C
			public virtual Word RecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["RecipientDomainIs"] = value;
				}
			}

			// Token: 0x17003BD4 RID: 15316
			// (set) Token: 0x06005E6F RID: 24175 RVA: 0x000922B4 File Offset: 0x000904B4
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17003BD5 RID: 15317
			// (set) Token: 0x06005E70 RID: 24176 RVA: 0x000922C7 File Offset: 0x000904C7
			public virtual RecipientIdParameter ExceptIfSentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToMemberOf"] = value;
				}
			}

			// Token: 0x17003BD6 RID: 15318
			// (set) Token: 0x06005E71 RID: 24177 RVA: 0x000922DA File Offset: 0x000904DA
			public virtual Word ExceptIfRecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientDomainIs"] = value;
				}
			}

			// Token: 0x17003BD7 RID: 15319
			// (set) Token: 0x06005E72 RID: 24178 RVA: 0x000922F2 File Offset: 0x000904F2
			public virtual string HostedContentFilterPolicy
			{
				set
				{
					base.PowerSharpParameters["HostedContentFilterPolicy"] = ((value != null) ? new HostedContentFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003BD8 RID: 15320
			// (set) Token: 0x06005E73 RID: 24179 RVA: 0x00092310 File Offset: 0x00090510
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003BD9 RID: 15321
			// (set) Token: 0x06005E74 RID: 24180 RVA: 0x00092323 File Offset: 0x00090523
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003BDA RID: 15322
			// (set) Token: 0x06005E75 RID: 24181 RVA: 0x0009233B File Offset: 0x0009053B
			public virtual string Comments
			{
				set
				{
					base.PowerSharpParameters["Comments"] = value;
				}
			}

			// Token: 0x17003BDB RID: 15323
			// (set) Token: 0x06005E76 RID: 24182 RVA: 0x0009234E File Offset: 0x0009054E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BDC RID: 15324
			// (set) Token: 0x06005E77 RID: 24183 RVA: 0x00092361 File Offset: 0x00090561
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BDD RID: 15325
			// (set) Token: 0x06005E78 RID: 24184 RVA: 0x00092379 File Offset: 0x00090579
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BDE RID: 15326
			// (set) Token: 0x06005E79 RID: 24185 RVA: 0x00092391 File Offset: 0x00090591
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003BDF RID: 15327
			// (set) Token: 0x06005E7A RID: 24186 RVA: 0x000923A9 File Offset: 0x000905A9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003BE0 RID: 15328
			// (set) Token: 0x06005E7B RID: 24187 RVA: 0x000923C1 File Offset: 0x000905C1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200072A RID: 1834
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003BE1 RID: 15329
			// (set) Token: 0x06005E7D RID: 24189 RVA: 0x000923E1 File Offset: 0x000905E1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17003BE2 RID: 15330
			// (set) Token: 0x06005E7E RID: 24190 RVA: 0x000923FF File Offset: 0x000905FF
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17003BE3 RID: 15331
			// (set) Token: 0x06005E7F RID: 24191 RVA: 0x00092412 File Offset: 0x00090612
			public virtual RecipientIdParameter SentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["SentToMemberOf"] = value;
				}
			}

			// Token: 0x17003BE4 RID: 15332
			// (set) Token: 0x06005E80 RID: 24192 RVA: 0x00092425 File Offset: 0x00090625
			public virtual Word RecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["RecipientDomainIs"] = value;
				}
			}

			// Token: 0x17003BE5 RID: 15333
			// (set) Token: 0x06005E81 RID: 24193 RVA: 0x0009243D File Offset: 0x0009063D
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17003BE6 RID: 15334
			// (set) Token: 0x06005E82 RID: 24194 RVA: 0x00092450 File Offset: 0x00090650
			public virtual RecipientIdParameter ExceptIfSentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToMemberOf"] = value;
				}
			}

			// Token: 0x17003BE7 RID: 15335
			// (set) Token: 0x06005E83 RID: 24195 RVA: 0x00092463 File Offset: 0x00090663
			public virtual Word ExceptIfRecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientDomainIs"] = value;
				}
			}

			// Token: 0x17003BE8 RID: 15336
			// (set) Token: 0x06005E84 RID: 24196 RVA: 0x0009247B File Offset: 0x0009067B
			public virtual string HostedContentFilterPolicy
			{
				set
				{
					base.PowerSharpParameters["HostedContentFilterPolicy"] = ((value != null) ? new HostedContentFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003BE9 RID: 15337
			// (set) Token: 0x06005E85 RID: 24197 RVA: 0x00092499 File Offset: 0x00090699
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003BEA RID: 15338
			// (set) Token: 0x06005E86 RID: 24198 RVA: 0x000924AC File Offset: 0x000906AC
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003BEB RID: 15339
			// (set) Token: 0x06005E87 RID: 24199 RVA: 0x000924C4 File Offset: 0x000906C4
			public virtual string Comments
			{
				set
				{
					base.PowerSharpParameters["Comments"] = value;
				}
			}

			// Token: 0x17003BEC RID: 15340
			// (set) Token: 0x06005E88 RID: 24200 RVA: 0x000924D7 File Offset: 0x000906D7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BED RID: 15341
			// (set) Token: 0x06005E89 RID: 24201 RVA: 0x000924EA File Offset: 0x000906EA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BEE RID: 15342
			// (set) Token: 0x06005E8A RID: 24202 RVA: 0x00092502 File Offset: 0x00090702
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BEF RID: 15343
			// (set) Token: 0x06005E8B RID: 24203 RVA: 0x0009251A File Offset: 0x0009071A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003BF0 RID: 15344
			// (set) Token: 0x06005E8C RID: 24204 RVA: 0x00092532 File Offset: 0x00090732
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003BF1 RID: 15345
			// (set) Token: 0x06005E8D RID: 24205 RVA: 0x0009254A File Offset: 0x0009074A
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
