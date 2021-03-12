using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Journaling;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006E5 RID: 1765
	public class NewJournalruleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06005BE6 RID: 23526 RVA: 0x0008EE2C File Offset: 0x0008D02C
		private NewJournalruleCommand() : base("New-Journalrule")
		{
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x0008EE39 File Offset: 0x0008D039
		public NewJournalruleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x0008EE48 File Offset: 0x0008D048
		public virtual NewJournalruleCommand SetParameters(NewJournalruleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006E6 RID: 1766
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170039D5 RID: 14805
			// (set) Token: 0x06005BE9 RID: 23529 RVA: 0x0008EE52 File Offset: 0x0008D052
			public virtual JournalRuleScope Scope
			{
				set
				{
					base.PowerSharpParameters["Scope"] = value;
				}
			}

			// Token: 0x170039D6 RID: 14806
			// (set) Token: 0x06005BEA RID: 23530 RVA: 0x0008EE6A File Offset: 0x0008D06A
			public virtual SmtpAddress? Recipient
			{
				set
				{
					base.PowerSharpParameters["Recipient"] = value;
				}
			}

			// Token: 0x170039D7 RID: 14807
			// (set) Token: 0x06005BEB RID: 23531 RVA: 0x0008EE82 File Offset: 0x0008D082
			public virtual string JournalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["JournalEmailAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170039D8 RID: 14808
			// (set) Token: 0x06005BEC RID: 23532 RVA: 0x0008EEA0 File Offset: 0x0008D0A0
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170039D9 RID: 14809
			// (set) Token: 0x06005BED RID: 23533 RVA: 0x0008EEB8 File Offset: 0x0008D0B8
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x170039DA RID: 14810
			// (set) Token: 0x06005BEE RID: 23534 RVA: 0x0008EED0 File Offset: 0x0008D0D0
			public virtual bool FullReport
			{
				set
				{
					base.PowerSharpParameters["FullReport"] = value;
				}
			}

			// Token: 0x170039DB RID: 14811
			// (set) Token: 0x06005BEF RID: 23535 RVA: 0x0008EEE8 File Offset: 0x0008D0E8
			public virtual DateTime? ExpiryDate
			{
				set
				{
					base.PowerSharpParameters["ExpiryDate"] = value;
				}
			}

			// Token: 0x170039DC RID: 14812
			// (set) Token: 0x06005BF0 RID: 23536 RVA: 0x0008EF00 File Offset: 0x0008D100
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170039DD RID: 14813
			// (set) Token: 0x06005BF1 RID: 23537 RVA: 0x0008EF1E File Offset: 0x0008D11E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170039DE RID: 14814
			// (set) Token: 0x06005BF2 RID: 23538 RVA: 0x0008EF31 File Offset: 0x0008D131
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039DF RID: 14815
			// (set) Token: 0x06005BF3 RID: 23539 RVA: 0x0008EF44 File Offset: 0x0008D144
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039E0 RID: 14816
			// (set) Token: 0x06005BF4 RID: 23540 RVA: 0x0008EF5C File Offset: 0x0008D15C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039E1 RID: 14817
			// (set) Token: 0x06005BF5 RID: 23541 RVA: 0x0008EF74 File Offset: 0x0008D174
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039E2 RID: 14818
			// (set) Token: 0x06005BF6 RID: 23542 RVA: 0x0008EF8C File Offset: 0x0008D18C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039E3 RID: 14819
			// (set) Token: 0x06005BF7 RID: 23543 RVA: 0x0008EFA4 File Offset: 0x0008D1A4
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
