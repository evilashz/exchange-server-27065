using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003AF RID: 943
	public class GetExternalActivityByUserReportCommand : SyntheticCommandWithPipelineInput<ExternalActivityByUserReport, ExternalActivityByUserReport>
	{
		// Token: 0x06003A03 RID: 14851 RVA: 0x00063195 File Offset: 0x00061395
		private GetExternalActivityByUserReportCommand() : base("Get-ExternalActivityByUserReport")
		{
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000631A2 File Offset: 0x000613A2
		public GetExternalActivityByUserReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000631B1 File Offset: 0x000613B1
		public virtual GetExternalActivityByUserReportCommand SetParameters(GetExternalActivityByUserReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003B0 RID: 944
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E5E RID: 7774
			// (set) Token: 0x06003A06 RID: 14854 RVA: 0x000631BB File Offset: 0x000613BB
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E5F RID: 7775
			// (set) Token: 0x06003A07 RID: 14855 RVA: 0x000631D3 File Offset: 0x000613D3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E60 RID: 7776
			// (set) Token: 0x06003A08 RID: 14856 RVA: 0x000631F1 File Offset: 0x000613F1
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E61 RID: 7777
			// (set) Token: 0x06003A09 RID: 14857 RVA: 0x00063209 File Offset: 0x00061409
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E62 RID: 7778
			// (set) Token: 0x06003A0A RID: 14858 RVA: 0x00063221 File Offset: 0x00061421
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E63 RID: 7779
			// (set) Token: 0x06003A0B RID: 14859 RVA: 0x00063239 File Offset: 0x00061439
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E64 RID: 7780
			// (set) Token: 0x06003A0C RID: 14860 RVA: 0x0006324C File Offset: 0x0006144C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E65 RID: 7781
			// (set) Token: 0x06003A0D RID: 14861 RVA: 0x00063264 File Offset: 0x00061464
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E66 RID: 7782
			// (set) Token: 0x06003A0E RID: 14862 RVA: 0x0006327C File Offset: 0x0006147C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E67 RID: 7783
			// (set) Token: 0x06003A0F RID: 14863 RVA: 0x00063294 File Offset: 0x00061494
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
