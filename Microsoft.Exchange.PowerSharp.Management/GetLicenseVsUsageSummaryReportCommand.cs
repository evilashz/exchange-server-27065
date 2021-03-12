using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003B7 RID: 951
	public class GetLicenseVsUsageSummaryReportCommand : SyntheticCommandWithPipelineInput<LicenseVsUsageSummaryReport, LicenseVsUsageSummaryReport>
	{
		// Token: 0x06003A3B RID: 14907 RVA: 0x00063611 File Offset: 0x00061811
		private GetLicenseVsUsageSummaryReportCommand() : base("Get-LicenseVsUsageSummaryReport")
		{
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x0006361E File Offset: 0x0006181E
		public GetLicenseVsUsageSummaryReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x0006362D File Offset: 0x0006182D
		public virtual GetLicenseVsUsageSummaryReportCommand SetParameters(GetLicenseVsUsageSummaryReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003B8 RID: 952
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E86 RID: 7814
			// (set) Token: 0x06003A3E RID: 14910 RVA: 0x00063637 File Offset: 0x00061837
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E87 RID: 7815
			// (set) Token: 0x06003A3F RID: 14911 RVA: 0x00063655 File Offset: 0x00061855
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E88 RID: 7816
			// (set) Token: 0x06003A40 RID: 14912 RVA: 0x0006366D File Offset: 0x0006186D
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E89 RID: 7817
			// (set) Token: 0x06003A41 RID: 14913 RVA: 0x00063685 File Offset: 0x00061885
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E8A RID: 7818
			// (set) Token: 0x06003A42 RID: 14914 RVA: 0x0006369D File Offset: 0x0006189D
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E8B RID: 7819
			// (set) Token: 0x06003A43 RID: 14915 RVA: 0x000636B0 File Offset: 0x000618B0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E8C RID: 7820
			// (set) Token: 0x06003A44 RID: 14916 RVA: 0x000636C8 File Offset: 0x000618C8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E8D RID: 7821
			// (set) Token: 0x06003A45 RID: 14917 RVA: 0x000636E0 File Offset: 0x000618E0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E8E RID: 7822
			// (set) Token: 0x06003A46 RID: 14918 RVA: 0x000636F8 File Offset: 0x000618F8
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
