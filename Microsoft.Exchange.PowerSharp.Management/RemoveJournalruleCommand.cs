using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006E7 RID: 1767
	public class RemoveJournalruleCommand : SyntheticCommandWithPipelineInputNoOutput<RuleIdParameter>
	{
		// Token: 0x06005BF9 RID: 23545 RVA: 0x0008EFC4 File Offset: 0x0008D1C4
		private RemoveJournalruleCommand() : base("Remove-Journalrule")
		{
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x0008EFD1 File Offset: 0x0008D1D1
		public RemoveJournalruleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x0008EFE0 File Offset: 0x0008D1E0
		public virtual RemoveJournalruleCommand SetParameters(RemoveJournalruleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x0008EFEA File Offset: 0x0008D1EA
		public virtual RemoveJournalruleCommand SetParameters(RemoveJournalruleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006E8 RID: 1768
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170039E4 RID: 14820
			// (set) Token: 0x06005BFD RID: 23549 RVA: 0x0008EFF4 File Offset: 0x0008D1F4
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x170039E5 RID: 14821
			// (set) Token: 0x06005BFE RID: 23550 RVA: 0x0008F00C File Offset: 0x0008D20C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039E6 RID: 14822
			// (set) Token: 0x06005BFF RID: 23551 RVA: 0x0008F01F File Offset: 0x0008D21F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039E7 RID: 14823
			// (set) Token: 0x06005C00 RID: 23552 RVA: 0x0008F037 File Offset: 0x0008D237
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039E8 RID: 14824
			// (set) Token: 0x06005C01 RID: 23553 RVA: 0x0008F04F File Offset: 0x0008D24F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039E9 RID: 14825
			// (set) Token: 0x06005C02 RID: 23554 RVA: 0x0008F067 File Offset: 0x0008D267
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039EA RID: 14826
			// (set) Token: 0x06005C03 RID: 23555 RVA: 0x0008F07F File Offset: 0x0008D27F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170039EB RID: 14827
			// (set) Token: 0x06005C04 RID: 23556 RVA: 0x0008F097 File Offset: 0x0008D297
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020006E9 RID: 1769
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170039EC RID: 14828
			// (set) Token: 0x06005C06 RID: 23558 RVA: 0x0008F0B7 File Offset: 0x0008D2B7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x170039ED RID: 14829
			// (set) Token: 0x06005C07 RID: 23559 RVA: 0x0008F0D5 File Offset: 0x0008D2D5
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x170039EE RID: 14830
			// (set) Token: 0x06005C08 RID: 23560 RVA: 0x0008F0ED File Offset: 0x0008D2ED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039EF RID: 14831
			// (set) Token: 0x06005C09 RID: 23561 RVA: 0x0008F100 File Offset: 0x0008D300
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039F0 RID: 14832
			// (set) Token: 0x06005C0A RID: 23562 RVA: 0x0008F118 File Offset: 0x0008D318
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039F1 RID: 14833
			// (set) Token: 0x06005C0B RID: 23563 RVA: 0x0008F130 File Offset: 0x0008D330
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039F2 RID: 14834
			// (set) Token: 0x06005C0C RID: 23564 RVA: 0x0008F148 File Offset: 0x0008D348
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039F3 RID: 14835
			// (set) Token: 0x06005C0D RID: 23565 RVA: 0x0008F160 File Offset: 0x0008D360
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170039F4 RID: 14836
			// (set) Token: 0x06005C0E RID: 23566 RVA: 0x0008F178 File Offset: 0x0008D378
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
