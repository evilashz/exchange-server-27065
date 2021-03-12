using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Journaling;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006F3 RID: 1779
	public class SetJournalRuleCommand : SyntheticCommandWithPipelineInputNoOutput<JournalRuleObject>
	{
		// Token: 0x06005C51 RID: 23633 RVA: 0x0008F6C0 File Offset: 0x0008D8C0
		private SetJournalRuleCommand() : base("Set-JournalRule")
		{
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x0008F6CD File Offset: 0x0008D8CD
		public SetJournalRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x0008F6DC File Offset: 0x0008D8DC
		public virtual SetJournalRuleCommand SetParameters(SetJournalRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005C54 RID: 23636 RVA: 0x0008F6E6 File Offset: 0x0008D8E6
		public virtual SetJournalRuleCommand SetParameters(SetJournalRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006F4 RID: 1780
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A24 RID: 14884
			// (set) Token: 0x06005C55 RID: 23637 RVA: 0x0008F6F0 File Offset: 0x0008D8F0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003A25 RID: 14885
			// (set) Token: 0x06005C56 RID: 23638 RVA: 0x0008F703 File Offset: 0x0008D903
			public virtual SmtpAddress? Recipient
			{
				set
				{
					base.PowerSharpParameters["Recipient"] = value;
				}
			}

			// Token: 0x17003A26 RID: 14886
			// (set) Token: 0x06005C57 RID: 23639 RVA: 0x0008F71B File Offset: 0x0008D91B
			public virtual string JournalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["JournalEmailAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17003A27 RID: 14887
			// (set) Token: 0x06005C58 RID: 23640 RVA: 0x0008F739 File Offset: 0x0008D939
			public virtual JournalRuleScope Scope
			{
				set
				{
					base.PowerSharpParameters["Scope"] = value;
				}
			}

			// Token: 0x17003A28 RID: 14888
			// (set) Token: 0x06005C59 RID: 23641 RVA: 0x0008F751 File Offset: 0x0008D951
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x17003A29 RID: 14889
			// (set) Token: 0x06005C5A RID: 23642 RVA: 0x0008F769 File Offset: 0x0008D969
			public virtual bool FullReport
			{
				set
				{
					base.PowerSharpParameters["FullReport"] = value;
				}
			}

			// Token: 0x17003A2A RID: 14890
			// (set) Token: 0x06005C5B RID: 23643 RVA: 0x0008F781 File Offset: 0x0008D981
			public virtual DateTime? ExpiryDate
			{
				set
				{
					base.PowerSharpParameters["ExpiryDate"] = value;
				}
			}

			// Token: 0x17003A2B RID: 14891
			// (set) Token: 0x06005C5C RID: 23644 RVA: 0x0008F799 File Offset: 0x0008D999
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A2C RID: 14892
			// (set) Token: 0x06005C5D RID: 23645 RVA: 0x0008F7AC File Offset: 0x0008D9AC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A2D RID: 14893
			// (set) Token: 0x06005C5E RID: 23646 RVA: 0x0008F7C4 File Offset: 0x0008D9C4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A2E RID: 14894
			// (set) Token: 0x06005C5F RID: 23647 RVA: 0x0008F7DC File Offset: 0x0008D9DC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A2F RID: 14895
			// (set) Token: 0x06005C60 RID: 23648 RVA: 0x0008F7F4 File Offset: 0x0008D9F4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A30 RID: 14896
			// (set) Token: 0x06005C61 RID: 23649 RVA: 0x0008F80C File Offset: 0x0008DA0C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006F5 RID: 1781
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003A31 RID: 14897
			// (set) Token: 0x06005C63 RID: 23651 RVA: 0x0008F82C File Offset: 0x0008DA2C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17003A32 RID: 14898
			// (set) Token: 0x06005C64 RID: 23652 RVA: 0x0008F84A File Offset: 0x0008DA4A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003A33 RID: 14899
			// (set) Token: 0x06005C65 RID: 23653 RVA: 0x0008F85D File Offset: 0x0008DA5D
			public virtual SmtpAddress? Recipient
			{
				set
				{
					base.PowerSharpParameters["Recipient"] = value;
				}
			}

			// Token: 0x17003A34 RID: 14900
			// (set) Token: 0x06005C66 RID: 23654 RVA: 0x0008F875 File Offset: 0x0008DA75
			public virtual string JournalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["JournalEmailAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17003A35 RID: 14901
			// (set) Token: 0x06005C67 RID: 23655 RVA: 0x0008F893 File Offset: 0x0008DA93
			public virtual JournalRuleScope Scope
			{
				set
				{
					base.PowerSharpParameters["Scope"] = value;
				}
			}

			// Token: 0x17003A36 RID: 14902
			// (set) Token: 0x06005C68 RID: 23656 RVA: 0x0008F8AB File Offset: 0x0008DAAB
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x17003A37 RID: 14903
			// (set) Token: 0x06005C69 RID: 23657 RVA: 0x0008F8C3 File Offset: 0x0008DAC3
			public virtual bool FullReport
			{
				set
				{
					base.PowerSharpParameters["FullReport"] = value;
				}
			}

			// Token: 0x17003A38 RID: 14904
			// (set) Token: 0x06005C6A RID: 23658 RVA: 0x0008F8DB File Offset: 0x0008DADB
			public virtual DateTime? ExpiryDate
			{
				set
				{
					base.PowerSharpParameters["ExpiryDate"] = value;
				}
			}

			// Token: 0x17003A39 RID: 14905
			// (set) Token: 0x06005C6B RID: 23659 RVA: 0x0008F8F3 File Offset: 0x0008DAF3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A3A RID: 14906
			// (set) Token: 0x06005C6C RID: 23660 RVA: 0x0008F906 File Offset: 0x0008DB06
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A3B RID: 14907
			// (set) Token: 0x06005C6D RID: 23661 RVA: 0x0008F91E File Offset: 0x0008DB1E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A3C RID: 14908
			// (set) Token: 0x06005C6E RID: 23662 RVA: 0x0008F936 File Offset: 0x0008DB36
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A3D RID: 14909
			// (set) Token: 0x06005C6F RID: 23663 RVA: 0x0008F94E File Offset: 0x0008DB4E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A3E RID: 14910
			// (set) Token: 0x06005C70 RID: 23664 RVA: 0x0008F966 File Offset: 0x0008DB66
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
