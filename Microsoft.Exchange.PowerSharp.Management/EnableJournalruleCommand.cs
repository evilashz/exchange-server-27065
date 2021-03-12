using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006EA RID: 1770
	public class EnableJournalruleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x06005C10 RID: 23568 RVA: 0x0008F198 File Offset: 0x0008D398
		private EnableJournalruleCommand() : base("Enable-Journalrule")
		{
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x0008F1A5 File Offset: 0x0008D3A5
		public EnableJournalruleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x0008F1B4 File Offset: 0x0008D3B4
		public virtual EnableJournalruleCommand SetParameters(EnableJournalruleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x0008F1BE File Offset: 0x0008D3BE
		public virtual EnableJournalruleCommand SetParameters(EnableJournalruleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006EB RID: 1771
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170039F5 RID: 14837
			// (set) Token: 0x06005C14 RID: 23572 RVA: 0x0008F1C8 File Offset: 0x0008D3C8
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x170039F6 RID: 14838
			// (set) Token: 0x06005C15 RID: 23573 RVA: 0x0008F1E0 File Offset: 0x0008D3E0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039F7 RID: 14839
			// (set) Token: 0x06005C16 RID: 23574 RVA: 0x0008F1F3 File Offset: 0x0008D3F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039F8 RID: 14840
			// (set) Token: 0x06005C17 RID: 23575 RVA: 0x0008F20B File Offset: 0x0008D40B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039F9 RID: 14841
			// (set) Token: 0x06005C18 RID: 23576 RVA: 0x0008F223 File Offset: 0x0008D423
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039FA RID: 14842
			// (set) Token: 0x06005C19 RID: 23577 RVA: 0x0008F23B File Offset: 0x0008D43B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039FB RID: 14843
			// (set) Token: 0x06005C1A RID: 23578 RVA: 0x0008F253 File Offset: 0x0008D453
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006EC RID: 1772
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170039FC RID: 14844
			// (set) Token: 0x06005C1C RID: 23580 RVA: 0x0008F273 File Offset: 0x0008D473
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x170039FD RID: 14845
			// (set) Token: 0x06005C1D RID: 23581 RVA: 0x0008F291 File Offset: 0x0008D491
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x170039FE RID: 14846
			// (set) Token: 0x06005C1E RID: 23582 RVA: 0x0008F2A9 File Offset: 0x0008D4A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039FF RID: 14847
			// (set) Token: 0x06005C1F RID: 23583 RVA: 0x0008F2BC File Offset: 0x0008D4BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A00 RID: 14848
			// (set) Token: 0x06005C20 RID: 23584 RVA: 0x0008F2D4 File Offset: 0x0008D4D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A01 RID: 14849
			// (set) Token: 0x06005C21 RID: 23585 RVA: 0x0008F2EC File Offset: 0x0008D4EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A02 RID: 14850
			// (set) Token: 0x06005C22 RID: 23586 RVA: 0x0008F304 File Offset: 0x0008D504
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A03 RID: 14851
			// (set) Token: 0x06005C23 RID: 23587 RVA: 0x0008F31C File Offset: 0x0008D51C
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
