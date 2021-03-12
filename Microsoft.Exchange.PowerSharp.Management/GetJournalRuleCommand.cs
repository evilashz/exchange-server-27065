using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006F0 RID: 1776
	public class GetJournalRuleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06005C3C RID: 23612 RVA: 0x0008F510 File Offset: 0x0008D710
		private GetJournalRuleCommand() : base("Get-JournalRule")
		{
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x0008F51D File Offset: 0x0008D71D
		public GetJournalRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x0008F52C File Offset: 0x0008D72C
		public virtual GetJournalRuleCommand SetParameters(GetJournalRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x0008F536 File Offset: 0x0008D736
		public virtual GetJournalRuleCommand SetParameters(GetJournalRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006F1 RID: 1777
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A15 RID: 14869
			// (set) Token: 0x06005C40 RID: 23616 RVA: 0x0008F540 File Offset: 0x0008D740
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x17003A16 RID: 14870
			// (set) Token: 0x06005C41 RID: 23617 RVA: 0x0008F558 File Offset: 0x0008D758
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003A17 RID: 14871
			// (set) Token: 0x06005C42 RID: 23618 RVA: 0x0008F576 File Offset: 0x0008D776
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A18 RID: 14872
			// (set) Token: 0x06005C43 RID: 23619 RVA: 0x0008F589 File Offset: 0x0008D789
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A19 RID: 14873
			// (set) Token: 0x06005C44 RID: 23620 RVA: 0x0008F5A1 File Offset: 0x0008D7A1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A1A RID: 14874
			// (set) Token: 0x06005C45 RID: 23621 RVA: 0x0008F5B9 File Offset: 0x0008D7B9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A1B RID: 14875
			// (set) Token: 0x06005C46 RID: 23622 RVA: 0x0008F5D1 File Offset: 0x0008D7D1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020006F2 RID: 1778
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003A1C RID: 14876
			// (set) Token: 0x06005C48 RID: 23624 RVA: 0x0008F5F1 File Offset: 0x0008D7F1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17003A1D RID: 14877
			// (set) Token: 0x06005C49 RID: 23625 RVA: 0x0008F60F File Offset: 0x0008D80F
			public virtual SwitchParameter LawfulInterception
			{
				set
				{
					base.PowerSharpParameters["LawfulInterception"] = value;
				}
			}

			// Token: 0x17003A1E RID: 14878
			// (set) Token: 0x06005C4A RID: 23626 RVA: 0x0008F627 File Offset: 0x0008D827
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003A1F RID: 14879
			// (set) Token: 0x06005C4B RID: 23627 RVA: 0x0008F645 File Offset: 0x0008D845
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A20 RID: 14880
			// (set) Token: 0x06005C4C RID: 23628 RVA: 0x0008F658 File Offset: 0x0008D858
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A21 RID: 14881
			// (set) Token: 0x06005C4D RID: 23629 RVA: 0x0008F670 File Offset: 0x0008D870
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A22 RID: 14882
			// (set) Token: 0x06005C4E RID: 23630 RVA: 0x0008F688 File Offset: 0x0008D888
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A23 RID: 14883
			// (set) Token: 0x06005C4F RID: 23631 RVA: 0x0008F6A0 File Offset: 0x0008D8A0
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
