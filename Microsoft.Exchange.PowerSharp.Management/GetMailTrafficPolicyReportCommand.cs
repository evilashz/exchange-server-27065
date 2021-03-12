using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200015D RID: 349
	public class GetMailTrafficPolicyReportCommand : SyntheticCommandWithPipelineInput<MailTrafficPolicyReport, MailTrafficPolicyReport>
	{
		// Token: 0x060021C5 RID: 8645 RVA: 0x00043638 File Offset: 0x00041838
		private GetMailTrafficPolicyReportCommand() : base("Get-MailTrafficPolicyReport")
		{
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00043645 File Offset: 0x00041845
		public GetMailTrafficPolicyReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x00043654 File Offset: 0x00041854
		public virtual GetMailTrafficPolicyReportCommand SetParameters(GetMailTrafficPolicyReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200015E RID: 350
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000AC4 RID: 2756
			// (set) Token: 0x060021C8 RID: 8648 RVA: 0x0004365E File Offset: 0x0004185E
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000AC5 RID: 2757
			// (set) Token: 0x060021C9 RID: 8649 RVA: 0x00043671 File Offset: 0x00041871
			public virtual MultiValuedProperty<string> DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17000AC6 RID: 2758
			// (set) Token: 0x060021CA RID: 8650 RVA: 0x00043684 File Offset: 0x00041884
			public virtual MultiValuedProperty<string> TransportRule
			{
				set
				{
					base.PowerSharpParameters["TransportRule"] = value;
				}
			}

			// Token: 0x17000AC7 RID: 2759
			// (set) Token: 0x060021CB RID: 8651 RVA: 0x00043697 File Offset: 0x00041897
			public virtual MultiValuedProperty<string> Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17000AC8 RID: 2760
			// (set) Token: 0x060021CC RID: 8652 RVA: 0x000436AA File Offset: 0x000418AA
			public virtual MultiValuedProperty<string> SummarizeBy
			{
				set
				{
					base.PowerSharpParameters["SummarizeBy"] = value;
				}
			}

			// Token: 0x17000AC9 RID: 2761
			// (set) Token: 0x060021CD RID: 8653 RVA: 0x000436BD File Offset: 0x000418BD
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000ACA RID: 2762
			// (set) Token: 0x060021CE RID: 8654 RVA: 0x000436D0 File Offset: 0x000418D0
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000ACB RID: 2763
			// (set) Token: 0x060021CF RID: 8655 RVA: 0x000436E8 File Offset: 0x000418E8
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000ACC RID: 2764
			// (set) Token: 0x060021D0 RID: 8656 RVA: 0x00043700 File Offset: 0x00041900
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000ACD RID: 2765
			// (set) Token: 0x060021D1 RID: 8657 RVA: 0x00043713 File Offset: 0x00041913
			public virtual string AggregateBy
			{
				set
				{
					base.PowerSharpParameters["AggregateBy"] = value;
				}
			}

			// Token: 0x17000ACE RID: 2766
			// (set) Token: 0x060021D2 RID: 8658 RVA: 0x00043726 File Offset: 0x00041926
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000ACF RID: 2767
			// (set) Token: 0x060021D3 RID: 8659 RVA: 0x0004373E File Offset: 0x0004193E
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000AD0 RID: 2768
			// (set) Token: 0x060021D4 RID: 8660 RVA: 0x00043756 File Offset: 0x00041956
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000AD1 RID: 2769
			// (set) Token: 0x060021D5 RID: 8661 RVA: 0x00043774 File Offset: 0x00041974
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000AD2 RID: 2770
			// (set) Token: 0x060021D6 RID: 8662 RVA: 0x00043787 File Offset: 0x00041987
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000AD3 RID: 2771
			// (set) Token: 0x060021D7 RID: 8663 RVA: 0x0004379A File Offset: 0x0004199A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000AD4 RID: 2772
			// (set) Token: 0x060021D8 RID: 8664 RVA: 0x000437B2 File Offset: 0x000419B2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000AD5 RID: 2773
			// (set) Token: 0x060021D9 RID: 8665 RVA: 0x000437CA File Offset: 0x000419CA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000AD6 RID: 2774
			// (set) Token: 0x060021DA RID: 8666 RVA: 0x000437E2 File Offset: 0x000419E2
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
