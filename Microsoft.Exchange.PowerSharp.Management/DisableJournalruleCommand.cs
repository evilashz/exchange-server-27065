using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006ED RID: 1773
	public class DisableJournalruleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x06005C25 RID: 23589 RVA: 0x0008F33C File Offset: 0x0008D53C
		private DisableJournalruleCommand() : base("Disable-Journalrule")
		{
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x0008F349 File Offset: 0x0008D549
		public DisableJournalruleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x0008F358 File Offset: 0x0008D558
		public virtual DisableJournalruleCommand SetParameters(DisableJournalruleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x0008F362 File Offset: 0x0008D562
		public virtual DisableJournalruleCommand SetParameters(DisableJournalruleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006EE RID: 1774
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A04 RID: 14852
			// (set) Token: 0x06005C29 RID: 23593 RVA: 0x0008F36C File Offset: 0x0008D56C
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x17003A05 RID: 14853
			// (set) Token: 0x06005C2A RID: 23594 RVA: 0x0008F384 File Offset: 0x0008D584
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A06 RID: 14854
			// (set) Token: 0x06005C2B RID: 23595 RVA: 0x0008F397 File Offset: 0x0008D597
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A07 RID: 14855
			// (set) Token: 0x06005C2C RID: 23596 RVA: 0x0008F3AF File Offset: 0x0008D5AF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A08 RID: 14856
			// (set) Token: 0x06005C2D RID: 23597 RVA: 0x0008F3C7 File Offset: 0x0008D5C7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A09 RID: 14857
			// (set) Token: 0x06005C2E RID: 23598 RVA: 0x0008F3DF File Offset: 0x0008D5DF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A0A RID: 14858
			// (set) Token: 0x06005C2F RID: 23599 RVA: 0x0008F3F7 File Offset: 0x0008D5F7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003A0B RID: 14859
			// (set) Token: 0x06005C30 RID: 23600 RVA: 0x0008F40F File Offset: 0x0008D60F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020006EF RID: 1775
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003A0C RID: 14860
			// (set) Token: 0x06005C32 RID: 23602 RVA: 0x0008F42F File Offset: 0x0008D62F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17003A0D RID: 14861
			// (set) Token: 0x06005C33 RID: 23603 RVA: 0x0008F44D File Offset: 0x0008D64D
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x17003A0E RID: 14862
			// (set) Token: 0x06005C34 RID: 23604 RVA: 0x0008F465 File Offset: 0x0008D665
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A0F RID: 14863
			// (set) Token: 0x06005C35 RID: 23605 RVA: 0x0008F478 File Offset: 0x0008D678
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A10 RID: 14864
			// (set) Token: 0x06005C36 RID: 23606 RVA: 0x0008F490 File Offset: 0x0008D690
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A11 RID: 14865
			// (set) Token: 0x06005C37 RID: 23607 RVA: 0x0008F4A8 File Offset: 0x0008D6A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A12 RID: 14866
			// (set) Token: 0x06005C38 RID: 23608 RVA: 0x0008F4C0 File Offset: 0x0008D6C0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A13 RID: 14867
			// (set) Token: 0x06005C39 RID: 23609 RVA: 0x0008F4D8 File Offset: 0x0008D6D8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003A14 RID: 14868
			// (set) Token: 0x06005C3A RID: 23610 RVA: 0x0008F4F0 File Offset: 0x0008D6F0
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
