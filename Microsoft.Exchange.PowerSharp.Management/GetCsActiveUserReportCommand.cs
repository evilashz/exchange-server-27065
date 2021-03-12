using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003A1 RID: 929
	public class GetCsActiveUserReportCommand : SyntheticCommandWithPipelineInput<CsActiveUserReport, CsActiveUserReport>
	{
		// Token: 0x060039A2 RID: 14754 RVA: 0x000629D4 File Offset: 0x00060BD4
		private GetCsActiveUserReportCommand() : base("Get-CsActiveUserReport")
		{
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000629E1 File Offset: 0x00060BE1
		public GetCsActiveUserReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000629F0 File Offset: 0x00060BF0
		public virtual GetCsActiveUserReportCommand SetParameters(GetCsActiveUserReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003A2 RID: 930
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E19 RID: 7705
			// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000629FA File Offset: 0x00060BFA
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E1A RID: 7706
			// (set) Token: 0x060039A6 RID: 14758 RVA: 0x00062A12 File Offset: 0x00060C12
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E1B RID: 7707
			// (set) Token: 0x060039A7 RID: 14759 RVA: 0x00062A30 File Offset: 0x00060C30
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E1C RID: 7708
			// (set) Token: 0x060039A8 RID: 14760 RVA: 0x00062A48 File Offset: 0x00060C48
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E1D RID: 7709
			// (set) Token: 0x060039A9 RID: 14761 RVA: 0x00062A60 File Offset: 0x00060C60
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E1E RID: 7710
			// (set) Token: 0x060039AA RID: 14762 RVA: 0x00062A78 File Offset: 0x00060C78
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E1F RID: 7711
			// (set) Token: 0x060039AB RID: 14763 RVA: 0x00062A8B File Offset: 0x00060C8B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E20 RID: 7712
			// (set) Token: 0x060039AC RID: 14764 RVA: 0x00062AA3 File Offset: 0x00060CA3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E21 RID: 7713
			// (set) Token: 0x060039AD RID: 14765 RVA: 0x00062ABB File Offset: 0x00060CBB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E22 RID: 7714
			// (set) Token: 0x060039AE RID: 14766 RVA: 0x00062AD3 File Offset: 0x00060CD3
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
