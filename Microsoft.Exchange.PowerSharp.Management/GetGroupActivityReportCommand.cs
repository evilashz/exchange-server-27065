using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003B5 RID: 949
	public class GetGroupActivityReportCommand : SyntheticCommandWithPipelineInput<GroupActivityReport, GroupActivityReport>
	{
		// Token: 0x06003A2D RID: 14893 RVA: 0x000634F2 File Offset: 0x000616F2
		private GetGroupActivityReportCommand() : base("Get-GroupActivityReport")
		{
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000634FF File Offset: 0x000616FF
		public GetGroupActivityReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x0006350E File Offset: 0x0006170E
		public virtual GetGroupActivityReportCommand SetParameters(GetGroupActivityReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003B6 RID: 950
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E7C RID: 7804
			// (set) Token: 0x06003A30 RID: 14896 RVA: 0x00063518 File Offset: 0x00061718
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E7D RID: 7805
			// (set) Token: 0x06003A31 RID: 14897 RVA: 0x00063530 File Offset: 0x00061730
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E7E RID: 7806
			// (set) Token: 0x06003A32 RID: 14898 RVA: 0x0006354E File Offset: 0x0006174E
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E7F RID: 7807
			// (set) Token: 0x06003A33 RID: 14899 RVA: 0x00063566 File Offset: 0x00061766
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E80 RID: 7808
			// (set) Token: 0x06003A34 RID: 14900 RVA: 0x0006357E File Offset: 0x0006177E
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E81 RID: 7809
			// (set) Token: 0x06003A35 RID: 14901 RVA: 0x00063596 File Offset: 0x00061796
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E82 RID: 7810
			// (set) Token: 0x06003A36 RID: 14902 RVA: 0x000635A9 File Offset: 0x000617A9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E83 RID: 7811
			// (set) Token: 0x06003A37 RID: 14903 RVA: 0x000635C1 File Offset: 0x000617C1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E84 RID: 7812
			// (set) Token: 0x06003A38 RID: 14904 RVA: 0x000635D9 File Offset: 0x000617D9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E85 RID: 7813
			// (set) Token: 0x06003A39 RID: 14905 RVA: 0x000635F1 File Offset: 0x000617F1
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
