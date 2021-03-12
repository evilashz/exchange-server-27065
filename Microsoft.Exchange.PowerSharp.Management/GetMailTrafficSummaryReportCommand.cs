using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000161 RID: 353
	public class GetMailTrafficSummaryReportCommand : SyntheticCommandWithPipelineInput<MailTrafficSummaryReport, MailTrafficSummaryReport>
	{
		// Token: 0x060021F1 RID: 8689 RVA: 0x000439A6 File Offset: 0x00041BA6
		private GetMailTrafficSummaryReportCommand() : base("Get-MailTrafficSummaryReport")
		{
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000439B3 File Offset: 0x00041BB3
		public GetMailTrafficSummaryReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000439C2 File Offset: 0x00041BC2
		public virtual GetMailTrafficSummaryReportCommand SetParameters(GetMailTrafficSummaryReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000162 RID: 354
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000AE8 RID: 2792
			// (set) Token: 0x060021F4 RID: 8692 RVA: 0x000439CC File Offset: 0x00041BCC
			public virtual string Category
			{
				set
				{
					base.PowerSharpParameters["Category"] = value;
				}
			}

			// Token: 0x17000AE9 RID: 2793
			// (set) Token: 0x060021F5 RID: 8693 RVA: 0x000439DF File Offset: 0x00041BDF
			public virtual MultiValuedProperty<Fqdn> Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000AEA RID: 2794
			// (set) Token: 0x060021F6 RID: 8694 RVA: 0x000439F2 File Offset: 0x00041BF2
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000AEB RID: 2795
			// (set) Token: 0x060021F7 RID: 8695 RVA: 0x00043A0A File Offset: 0x00041C0A
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000AEC RID: 2796
			// (set) Token: 0x060021F8 RID: 8696 RVA: 0x00043A22 File Offset: 0x00041C22
			public virtual MultiValuedProperty<string> DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17000AED RID: 2797
			// (set) Token: 0x060021F9 RID: 8697 RVA: 0x00043A35 File Offset: 0x00041C35
			public virtual MultiValuedProperty<string> TransportRule
			{
				set
				{
					base.PowerSharpParameters["TransportRule"] = value;
				}
			}

			// Token: 0x17000AEE RID: 2798
			// (set) Token: 0x060021FA RID: 8698 RVA: 0x00043A48 File Offset: 0x00041C48
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000AEF RID: 2799
			// (set) Token: 0x060021FB RID: 8699 RVA: 0x00043A60 File Offset: 0x00041C60
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000AF0 RID: 2800
			// (set) Token: 0x060021FC RID: 8700 RVA: 0x00043A78 File Offset: 0x00041C78
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000AF1 RID: 2801
			// (set) Token: 0x060021FD RID: 8701 RVA: 0x00043A96 File Offset: 0x00041C96
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000AF2 RID: 2802
			// (set) Token: 0x060021FE RID: 8702 RVA: 0x00043AA9 File Offset: 0x00041CA9
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000AF3 RID: 2803
			// (set) Token: 0x060021FF RID: 8703 RVA: 0x00043ABC File Offset: 0x00041CBC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000AF4 RID: 2804
			// (set) Token: 0x06002200 RID: 8704 RVA: 0x00043AD4 File Offset: 0x00041CD4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000AF5 RID: 2805
			// (set) Token: 0x06002201 RID: 8705 RVA: 0x00043AEC File Offset: 0x00041CEC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000AF6 RID: 2806
			// (set) Token: 0x06002202 RID: 8706 RVA: 0x00043B04 File Offset: 0x00041D04
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
