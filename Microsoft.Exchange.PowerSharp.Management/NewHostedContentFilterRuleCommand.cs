using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000723 RID: 1827
	public class NewHostedContentFilterRuleCommand : SyntheticCommandWithPipelineInputNoOutput<HostedContentFilterPolicyIdParameter>
	{
		// Token: 0x06005E3D RID: 24125 RVA: 0x00091EDB File Offset: 0x000900DB
		private NewHostedContentFilterRuleCommand() : base("New-HostedContentFilterRule")
		{
		}

		// Token: 0x06005E3E RID: 24126 RVA: 0x00091EE8 File Offset: 0x000900E8
		public NewHostedContentFilterRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x00091EF7 File Offset: 0x000900F7
		public virtual NewHostedContentFilterRuleCommand SetParameters(NewHostedContentFilterRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000724 RID: 1828
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003BB0 RID: 15280
			// (set) Token: 0x06005E40 RID: 24128 RVA: 0x00091F01 File Offset: 0x00090101
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17003BB1 RID: 15281
			// (set) Token: 0x06005E41 RID: 24129 RVA: 0x00091F14 File Offset: 0x00090114
			public virtual RecipientIdParameter SentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["SentToMemberOf"] = value;
				}
			}

			// Token: 0x17003BB2 RID: 15282
			// (set) Token: 0x06005E42 RID: 24130 RVA: 0x00091F27 File Offset: 0x00090127
			public virtual Word RecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["RecipientDomainIs"] = value;
				}
			}

			// Token: 0x17003BB3 RID: 15283
			// (set) Token: 0x06005E43 RID: 24131 RVA: 0x00091F3F File Offset: 0x0009013F
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17003BB4 RID: 15284
			// (set) Token: 0x06005E44 RID: 24132 RVA: 0x00091F52 File Offset: 0x00090152
			public virtual RecipientIdParameter ExceptIfSentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToMemberOf"] = value;
				}
			}

			// Token: 0x17003BB5 RID: 15285
			// (set) Token: 0x06005E45 RID: 24133 RVA: 0x00091F65 File Offset: 0x00090165
			public virtual Word ExceptIfRecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientDomainIs"] = value;
				}
			}

			// Token: 0x17003BB6 RID: 15286
			// (set) Token: 0x06005E46 RID: 24134 RVA: 0x00091F7D File Offset: 0x0009017D
			public virtual string HostedContentFilterPolicy
			{
				set
				{
					base.PowerSharpParameters["HostedContentFilterPolicy"] = ((value != null) ? new HostedContentFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003BB7 RID: 15287
			// (set) Token: 0x06005E47 RID: 24135 RVA: 0x00091F9B File Offset: 0x0009019B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003BB8 RID: 15288
			// (set) Token: 0x06005E48 RID: 24136 RVA: 0x00091FAE File Offset: 0x000901AE
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003BB9 RID: 15289
			// (set) Token: 0x06005E49 RID: 24137 RVA: 0x00091FC6 File Offset: 0x000901C6
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003BBA RID: 15290
			// (set) Token: 0x06005E4A RID: 24138 RVA: 0x00091FDE File Offset: 0x000901DE
			public virtual string Comments
			{
				set
				{
					base.PowerSharpParameters["Comments"] = value;
				}
			}

			// Token: 0x17003BBB RID: 15291
			// (set) Token: 0x06005E4B RID: 24139 RVA: 0x00091FF1 File Offset: 0x000901F1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003BBC RID: 15292
			// (set) Token: 0x06005E4C RID: 24140 RVA: 0x0009200F File Offset: 0x0009020F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BBD RID: 15293
			// (set) Token: 0x06005E4D RID: 24141 RVA: 0x00092022 File Offset: 0x00090222
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BBE RID: 15294
			// (set) Token: 0x06005E4E RID: 24142 RVA: 0x0009203A File Offset: 0x0009023A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BBF RID: 15295
			// (set) Token: 0x06005E4F RID: 24143 RVA: 0x00092052 File Offset: 0x00090252
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003BC0 RID: 15296
			// (set) Token: 0x06005E50 RID: 24144 RVA: 0x0009206A File Offset: 0x0009026A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003BC1 RID: 15297
			// (set) Token: 0x06005E51 RID: 24145 RVA: 0x00092082 File Offset: 0x00090282
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
