using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C45 RID: 3141
	public class DisableInboxRuleCommand : SyntheticCommandWithPipelineInputNoOutput<InboxRuleIdParameter>
	{
		// Token: 0x06009997 RID: 39319 RVA: 0x000DF1A5 File Offset: 0x000DD3A5
		private DisableInboxRuleCommand() : base("Disable-InboxRule")
		{
		}

		// Token: 0x06009998 RID: 39320 RVA: 0x000DF1B2 File Offset: 0x000DD3B2
		public DisableInboxRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009999 RID: 39321 RVA: 0x000DF1C1 File Offset: 0x000DD3C1
		public virtual DisableInboxRuleCommand SetParameters(DisableInboxRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600999A RID: 39322 RVA: 0x000DF1CB File Offset: 0x000DD3CB
		public virtual DisableInboxRuleCommand SetParameters(DisableInboxRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C46 RID: 3142
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006CC6 RID: 27846
			// (set) Token: 0x0600999B RID: 39323 RVA: 0x000DF1D5 File Offset: 0x000DD3D5
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006CC7 RID: 27847
			// (set) Token: 0x0600999C RID: 39324 RVA: 0x000DF1ED File Offset: 0x000DD3ED
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006CC8 RID: 27848
			// (set) Token: 0x0600999D RID: 39325 RVA: 0x000DF205 File Offset: 0x000DD405
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006CC9 RID: 27849
			// (set) Token: 0x0600999E RID: 39326 RVA: 0x000DF223 File Offset: 0x000DD423
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006CCA RID: 27850
			// (set) Token: 0x0600999F RID: 39327 RVA: 0x000DF236 File Offset: 0x000DD436
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006CCB RID: 27851
			// (set) Token: 0x060099A0 RID: 39328 RVA: 0x000DF24E File Offset: 0x000DD44E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006CCC RID: 27852
			// (set) Token: 0x060099A1 RID: 39329 RVA: 0x000DF266 File Offset: 0x000DD466
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006CCD RID: 27853
			// (set) Token: 0x060099A2 RID: 39330 RVA: 0x000DF27E File Offset: 0x000DD47E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006CCE RID: 27854
			// (set) Token: 0x060099A3 RID: 39331 RVA: 0x000DF296 File Offset: 0x000DD496
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006CCF RID: 27855
			// (set) Token: 0x060099A4 RID: 39332 RVA: 0x000DF2AE File Offset: 0x000DD4AE
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C47 RID: 3143
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006CD0 RID: 27856
			// (set) Token: 0x060099A6 RID: 39334 RVA: 0x000DF2CE File Offset: 0x000DD4CE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new InboxRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17006CD1 RID: 27857
			// (set) Token: 0x060099A7 RID: 39335 RVA: 0x000DF2EC File Offset: 0x000DD4EC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006CD2 RID: 27858
			// (set) Token: 0x060099A8 RID: 39336 RVA: 0x000DF304 File Offset: 0x000DD504
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006CD3 RID: 27859
			// (set) Token: 0x060099A9 RID: 39337 RVA: 0x000DF31C File Offset: 0x000DD51C
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006CD4 RID: 27860
			// (set) Token: 0x060099AA RID: 39338 RVA: 0x000DF33A File Offset: 0x000DD53A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006CD5 RID: 27861
			// (set) Token: 0x060099AB RID: 39339 RVA: 0x000DF34D File Offset: 0x000DD54D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006CD6 RID: 27862
			// (set) Token: 0x060099AC RID: 39340 RVA: 0x000DF365 File Offset: 0x000DD565
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006CD7 RID: 27863
			// (set) Token: 0x060099AD RID: 39341 RVA: 0x000DF37D File Offset: 0x000DD57D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006CD8 RID: 27864
			// (set) Token: 0x060099AE RID: 39342 RVA: 0x000DF395 File Offset: 0x000DD595
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006CD9 RID: 27865
			// (set) Token: 0x060099AF RID: 39343 RVA: 0x000DF3AD File Offset: 0x000DD5AD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006CDA RID: 27866
			// (set) Token: 0x060099B0 RID: 39344 RVA: 0x000DF3C5 File Offset: 0x000DD5C5
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
