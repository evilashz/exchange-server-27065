using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003C3 RID: 963
	public class GetScorecardClientDeviceReportCommand : SyntheticCommandWithPipelineInput<ScorecardClientDeviceReport, ScorecardClientDeviceReport>
	{
		// Token: 0x06003A8A RID: 14986 RVA: 0x00063C53 File Offset: 0x00061E53
		private GetScorecardClientDeviceReportCommand() : base("Get-ScorecardClientDeviceReport")
		{
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x00063C60 File Offset: 0x00061E60
		public GetScorecardClientDeviceReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x00063C6F File Offset: 0x00061E6F
		public virtual GetScorecardClientDeviceReportCommand SetParameters(GetScorecardClientDeviceReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003C4 RID: 964
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EBD RID: 7869
			// (set) Token: 0x06003A8D RID: 14989 RVA: 0x00063C79 File Offset: 0x00061E79
			public virtual DataCategory Category
			{
				set
				{
					base.PowerSharpParameters["Category"] = value;
				}
			}

			// Token: 0x17001EBE RID: 7870
			// (set) Token: 0x06003A8E RID: 14990 RVA: 0x00063C91 File Offset: 0x00061E91
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EBF RID: 7871
			// (set) Token: 0x06003A8F RID: 14991 RVA: 0x00063CAF File Offset: 0x00061EAF
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001EC0 RID: 7872
			// (set) Token: 0x06003A90 RID: 14992 RVA: 0x00063CC7 File Offset: 0x00061EC7
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001EC1 RID: 7873
			// (set) Token: 0x06003A91 RID: 14993 RVA: 0x00063CDF File Offset: 0x00061EDF
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001EC2 RID: 7874
			// (set) Token: 0x06003A92 RID: 14994 RVA: 0x00063CF7 File Offset: 0x00061EF7
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001EC3 RID: 7875
			// (set) Token: 0x06003A93 RID: 14995 RVA: 0x00063D0A File Offset: 0x00061F0A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001EC4 RID: 7876
			// (set) Token: 0x06003A94 RID: 14996 RVA: 0x00063D22 File Offset: 0x00061F22
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EC5 RID: 7877
			// (set) Token: 0x06003A95 RID: 14997 RVA: 0x00063D3A File Offset: 0x00061F3A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EC6 RID: 7878
			// (set) Token: 0x06003A96 RID: 14998 RVA: 0x00063D52 File Offset: 0x00061F52
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
