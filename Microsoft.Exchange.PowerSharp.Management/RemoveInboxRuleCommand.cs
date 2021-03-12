using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C52 RID: 3154
	public class RemoveInboxRuleCommand : SyntheticCommandWithPipelineInput<InboxRule, InboxRule>
	{
		// Token: 0x06009A4D RID: 39501 RVA: 0x000E00E2 File Offset: 0x000DE2E2
		private RemoveInboxRuleCommand() : base("Remove-InboxRule")
		{
		}

		// Token: 0x06009A4E RID: 39502 RVA: 0x000E00EF File Offset: 0x000DE2EF
		public RemoveInboxRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009A4F RID: 39503 RVA: 0x000E00FE File Offset: 0x000DE2FE
		public virtual RemoveInboxRuleCommand SetParameters(RemoveInboxRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009A50 RID: 39504 RVA: 0x000E0108 File Offset: 0x000DE308
		public virtual RemoveInboxRuleCommand SetParameters(RemoveInboxRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C53 RID: 3155
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006D62 RID: 28002
			// (set) Token: 0x06009A51 RID: 39505 RVA: 0x000E0112 File Offset: 0x000DE312
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006D63 RID: 28003
			// (set) Token: 0x06009A52 RID: 39506 RVA: 0x000E012A File Offset: 0x000DE32A
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006D64 RID: 28004
			// (set) Token: 0x06009A53 RID: 39507 RVA: 0x000E0142 File Offset: 0x000DE342
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006D65 RID: 28005
			// (set) Token: 0x06009A54 RID: 39508 RVA: 0x000E0160 File Offset: 0x000DE360
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006D66 RID: 28006
			// (set) Token: 0x06009A55 RID: 39509 RVA: 0x000E0173 File Offset: 0x000DE373
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006D67 RID: 28007
			// (set) Token: 0x06009A56 RID: 39510 RVA: 0x000E018B File Offset: 0x000DE38B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006D68 RID: 28008
			// (set) Token: 0x06009A57 RID: 39511 RVA: 0x000E01A3 File Offset: 0x000DE3A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006D69 RID: 28009
			// (set) Token: 0x06009A58 RID: 39512 RVA: 0x000E01BB File Offset: 0x000DE3BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006D6A RID: 28010
			// (set) Token: 0x06009A59 RID: 39513 RVA: 0x000E01D3 File Offset: 0x000DE3D3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006D6B RID: 28011
			// (set) Token: 0x06009A5A RID: 39514 RVA: 0x000E01EB File Offset: 0x000DE3EB
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C54 RID: 3156
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006D6C RID: 28012
			// (set) Token: 0x06009A5C RID: 39516 RVA: 0x000E020B File Offset: 0x000DE40B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new InboxRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17006D6D RID: 28013
			// (set) Token: 0x06009A5D RID: 39517 RVA: 0x000E0229 File Offset: 0x000DE429
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006D6E RID: 28014
			// (set) Token: 0x06009A5E RID: 39518 RVA: 0x000E0241 File Offset: 0x000DE441
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006D6F RID: 28015
			// (set) Token: 0x06009A5F RID: 39519 RVA: 0x000E0259 File Offset: 0x000DE459
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006D70 RID: 28016
			// (set) Token: 0x06009A60 RID: 39520 RVA: 0x000E0277 File Offset: 0x000DE477
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006D71 RID: 28017
			// (set) Token: 0x06009A61 RID: 39521 RVA: 0x000E028A File Offset: 0x000DE48A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006D72 RID: 28018
			// (set) Token: 0x06009A62 RID: 39522 RVA: 0x000E02A2 File Offset: 0x000DE4A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006D73 RID: 28019
			// (set) Token: 0x06009A63 RID: 39523 RVA: 0x000E02BA File Offset: 0x000DE4BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006D74 RID: 28020
			// (set) Token: 0x06009A64 RID: 39524 RVA: 0x000E02D2 File Offset: 0x000DE4D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006D75 RID: 28021
			// (set) Token: 0x06009A65 RID: 39525 RVA: 0x000E02EA File Offset: 0x000DE4EA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006D76 RID: 28022
			// (set) Token: 0x06009A66 RID: 39526 RVA: 0x000E0302 File Offset: 0x000DE502
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
