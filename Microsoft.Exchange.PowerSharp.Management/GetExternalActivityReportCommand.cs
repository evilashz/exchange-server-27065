using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003B1 RID: 945
	public class GetExternalActivityReportCommand : SyntheticCommandWithPipelineInput<ExternalActivityReport, ExternalActivityReport>
	{
		// Token: 0x06003A11 RID: 14865 RVA: 0x000632B4 File Offset: 0x000614B4
		private GetExternalActivityReportCommand() : base("Get-ExternalActivityReport")
		{
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000632C1 File Offset: 0x000614C1
		public GetExternalActivityReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000632D0 File Offset: 0x000614D0
		public virtual GetExternalActivityReportCommand SetParameters(GetExternalActivityReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003B2 RID: 946
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E68 RID: 7784
			// (set) Token: 0x06003A14 RID: 14868 RVA: 0x000632DA File Offset: 0x000614DA
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E69 RID: 7785
			// (set) Token: 0x06003A15 RID: 14869 RVA: 0x000632F2 File Offset: 0x000614F2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E6A RID: 7786
			// (set) Token: 0x06003A16 RID: 14870 RVA: 0x00063310 File Offset: 0x00061510
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E6B RID: 7787
			// (set) Token: 0x06003A17 RID: 14871 RVA: 0x00063328 File Offset: 0x00061528
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E6C RID: 7788
			// (set) Token: 0x06003A18 RID: 14872 RVA: 0x00063340 File Offset: 0x00061540
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E6D RID: 7789
			// (set) Token: 0x06003A19 RID: 14873 RVA: 0x00063358 File Offset: 0x00061558
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E6E RID: 7790
			// (set) Token: 0x06003A1A RID: 14874 RVA: 0x0006336B File Offset: 0x0006156B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E6F RID: 7791
			// (set) Token: 0x06003A1B RID: 14875 RVA: 0x00063383 File Offset: 0x00061583
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E70 RID: 7792
			// (set) Token: 0x06003A1C RID: 14876 RVA: 0x0006339B File Offset: 0x0006159B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E71 RID: 7793
			// (set) Token: 0x06003A1D RID: 14877 RVA: 0x000633B3 File Offset: 0x000615B3
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
