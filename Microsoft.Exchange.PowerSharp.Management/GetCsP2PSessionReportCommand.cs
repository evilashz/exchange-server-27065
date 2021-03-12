using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003AB RID: 939
	public class GetCsP2PSessionReportCommand : SyntheticCommandWithPipelineInput<CsP2PSessionReport, CsP2PSessionReport>
	{
		// Token: 0x060039E7 RID: 14823 RVA: 0x00062F57 File Offset: 0x00061157
		private GetCsP2PSessionReportCommand() : base("Get-CsP2PSessionReport")
		{
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x00062F64 File Offset: 0x00061164
		public GetCsP2PSessionReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x00062F73 File Offset: 0x00061173
		public virtual GetCsP2PSessionReportCommand SetParameters(GetCsP2PSessionReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003AC RID: 940
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E4A RID: 7754
			// (set) Token: 0x060039EA RID: 14826 RVA: 0x00062F7D File Offset: 0x0006117D
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E4B RID: 7755
			// (set) Token: 0x060039EB RID: 14827 RVA: 0x00062F95 File Offset: 0x00061195
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E4C RID: 7756
			// (set) Token: 0x060039EC RID: 14828 RVA: 0x00062FB3 File Offset: 0x000611B3
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E4D RID: 7757
			// (set) Token: 0x060039ED RID: 14829 RVA: 0x00062FCB File Offset: 0x000611CB
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E4E RID: 7758
			// (set) Token: 0x060039EE RID: 14830 RVA: 0x00062FE3 File Offset: 0x000611E3
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E4F RID: 7759
			// (set) Token: 0x060039EF RID: 14831 RVA: 0x00062FFB File Offset: 0x000611FB
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E50 RID: 7760
			// (set) Token: 0x060039F0 RID: 14832 RVA: 0x0006300E File Offset: 0x0006120E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E51 RID: 7761
			// (set) Token: 0x060039F1 RID: 14833 RVA: 0x00063026 File Offset: 0x00061226
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E52 RID: 7762
			// (set) Token: 0x060039F2 RID: 14834 RVA: 0x0006303E File Offset: 0x0006123E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E53 RID: 7763
			// (set) Token: 0x060039F3 RID: 14835 RVA: 0x00063056 File Offset: 0x00061256
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
