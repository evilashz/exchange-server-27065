using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003B3 RID: 947
	public class GetExternalActivitySummaryReportCommand : SyntheticCommandWithPipelineInput<ExternalActivitySummaryReport, ExternalActivitySummaryReport>
	{
		// Token: 0x06003A1F RID: 14879 RVA: 0x000633D3 File Offset: 0x000615D3
		private GetExternalActivitySummaryReportCommand() : base("Get-ExternalActivitySummaryReport")
		{
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000633E0 File Offset: 0x000615E0
		public GetExternalActivitySummaryReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000633EF File Offset: 0x000615EF
		public virtual GetExternalActivitySummaryReportCommand SetParameters(GetExternalActivitySummaryReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003B4 RID: 948
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E72 RID: 7794
			// (set) Token: 0x06003A22 RID: 14882 RVA: 0x000633F9 File Offset: 0x000615F9
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E73 RID: 7795
			// (set) Token: 0x06003A23 RID: 14883 RVA: 0x00063411 File Offset: 0x00061611
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E74 RID: 7796
			// (set) Token: 0x06003A24 RID: 14884 RVA: 0x0006342F File Offset: 0x0006162F
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E75 RID: 7797
			// (set) Token: 0x06003A25 RID: 14885 RVA: 0x00063447 File Offset: 0x00061647
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E76 RID: 7798
			// (set) Token: 0x06003A26 RID: 14886 RVA: 0x0006345F File Offset: 0x0006165F
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E77 RID: 7799
			// (set) Token: 0x06003A27 RID: 14887 RVA: 0x00063477 File Offset: 0x00061677
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E78 RID: 7800
			// (set) Token: 0x06003A28 RID: 14888 RVA: 0x0006348A File Offset: 0x0006168A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E79 RID: 7801
			// (set) Token: 0x06003A29 RID: 14889 RVA: 0x000634A2 File Offset: 0x000616A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E7A RID: 7802
			// (set) Token: 0x06003A2A RID: 14890 RVA: 0x000634BA File Offset: 0x000616BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E7B RID: 7803
			// (set) Token: 0x06003A2B RID: 14891 RVA: 0x000634D2 File Offset: 0x000616D2
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
