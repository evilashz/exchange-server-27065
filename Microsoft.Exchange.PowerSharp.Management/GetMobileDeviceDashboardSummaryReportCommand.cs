using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000169 RID: 361
	public class GetMobileDeviceDashboardSummaryReportCommand : SyntheticCommandWithPipelineInput<MobileDeviceDashboardSummaryReport, MobileDeviceDashboardSummaryReport>
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x0004401A File Offset: 0x0004221A
		private GetMobileDeviceDashboardSummaryReportCommand() : base("Get-MobileDeviceDashboardSummaryReport")
		{
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00044027 File Offset: 0x00042227
		public GetMobileDeviceDashboardSummaryReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00044036 File Offset: 0x00042236
		public virtual GetMobileDeviceDashboardSummaryReportCommand SetParameters(GetMobileDeviceDashboardSummaryReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200016A RID: 362
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B2A RID: 2858
			// (set) Token: 0x06002246 RID: 8774 RVA: 0x00044040 File Offset: 0x00042240
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000B2B RID: 2859
			// (set) Token: 0x06002247 RID: 8775 RVA: 0x00044058 File Offset: 0x00042258
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000B2C RID: 2860
			// (set) Token: 0x06002248 RID: 8776 RVA: 0x00044070 File Offset: 0x00042270
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000B2D RID: 2861
			// (set) Token: 0x06002249 RID: 8777 RVA: 0x00044088 File Offset: 0x00042288
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000B2E RID: 2862
			// (set) Token: 0x0600224A RID: 8778 RVA: 0x000440A0 File Offset: 0x000422A0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B2F RID: 2863
			// (set) Token: 0x0600224B RID: 8779 RVA: 0x000440BE File Offset: 0x000422BE
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000B30 RID: 2864
			// (set) Token: 0x0600224C RID: 8780 RVA: 0x000440D1 File Offset: 0x000422D1
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000B31 RID: 2865
			// (set) Token: 0x0600224D RID: 8781 RVA: 0x000440E4 File Offset: 0x000422E4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B32 RID: 2866
			// (set) Token: 0x0600224E RID: 8782 RVA: 0x000440FC File Offset: 0x000422FC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B33 RID: 2867
			// (set) Token: 0x0600224F RID: 8783 RVA: 0x00044114 File Offset: 0x00042314
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B34 RID: 2868
			// (set) Token: 0x06002250 RID: 8784 RVA: 0x0004412C File Offset: 0x0004232C
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
