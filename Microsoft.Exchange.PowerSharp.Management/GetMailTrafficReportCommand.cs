using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200015F RID: 351
	public class GetMailTrafficReportCommand : SyntheticCommandWithPipelineInput<MailTrafficReport, MailTrafficReport>
	{
		// Token: 0x060021DC RID: 8668 RVA: 0x00043802 File Offset: 0x00041A02
		private GetMailTrafficReportCommand() : base("Get-MailTrafficReport")
		{
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0004380F File Offset: 0x00041A0F
		public GetMailTrafficReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0004381E File Offset: 0x00041A1E
		public virtual GetMailTrafficReportCommand SetParameters(GetMailTrafficReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000160 RID: 352
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000AD7 RID: 2775
			// (set) Token: 0x060021DF RID: 8671 RVA: 0x00043828 File Offset: 0x00041A28
			public virtual MultiValuedProperty<string> EventType
			{
				set
				{
					base.PowerSharpParameters["EventType"] = value;
				}
			}

			// Token: 0x17000AD8 RID: 2776
			// (set) Token: 0x060021E0 RID: 8672 RVA: 0x0004383B File Offset: 0x00041A3B
			public virtual MultiValuedProperty<string> Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17000AD9 RID: 2777
			// (set) Token: 0x060021E1 RID: 8673 RVA: 0x0004384E File Offset: 0x00041A4E
			public virtual MultiValuedProperty<string> SummarizeBy
			{
				set
				{
					base.PowerSharpParameters["SummarizeBy"] = value;
				}
			}

			// Token: 0x17000ADA RID: 2778
			// (set) Token: 0x060021E2 RID: 8674 RVA: 0x00043861 File Offset: 0x00041A61
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000ADB RID: 2779
			// (set) Token: 0x060021E3 RID: 8675 RVA: 0x00043874 File Offset: 0x00041A74
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000ADC RID: 2780
			// (set) Token: 0x060021E4 RID: 8676 RVA: 0x0004388C File Offset: 0x00041A8C
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000ADD RID: 2781
			// (set) Token: 0x060021E5 RID: 8677 RVA: 0x000438A4 File Offset: 0x00041AA4
			public virtual MultiValuedProperty<string> Direction
			{
				set
				{
					base.PowerSharpParameters["Direction"] = value;
				}
			}

			// Token: 0x17000ADE RID: 2782
			// (set) Token: 0x060021E6 RID: 8678 RVA: 0x000438B7 File Offset: 0x00041AB7
			public virtual string AggregateBy
			{
				set
				{
					base.PowerSharpParameters["AggregateBy"] = value;
				}
			}

			// Token: 0x17000ADF RID: 2783
			// (set) Token: 0x060021E7 RID: 8679 RVA: 0x000438CA File Offset: 0x00041ACA
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000AE0 RID: 2784
			// (set) Token: 0x060021E8 RID: 8680 RVA: 0x000438E2 File Offset: 0x00041AE2
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000AE1 RID: 2785
			// (set) Token: 0x060021E9 RID: 8681 RVA: 0x000438FA File Offset: 0x00041AFA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000AE2 RID: 2786
			// (set) Token: 0x060021EA RID: 8682 RVA: 0x00043918 File Offset: 0x00041B18
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000AE3 RID: 2787
			// (set) Token: 0x060021EB RID: 8683 RVA: 0x0004392B File Offset: 0x00041B2B
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000AE4 RID: 2788
			// (set) Token: 0x060021EC RID: 8684 RVA: 0x0004393E File Offset: 0x00041B3E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000AE5 RID: 2789
			// (set) Token: 0x060021ED RID: 8685 RVA: 0x00043956 File Offset: 0x00041B56
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000AE6 RID: 2790
			// (set) Token: 0x060021EE RID: 8686 RVA: 0x0004396E File Offset: 0x00041B6E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000AE7 RID: 2791
			// (set) Token: 0x060021EF RID: 8687 RVA: 0x00043986 File Offset: 0x00041B86
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
