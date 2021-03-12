using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003BD RID: 957
	public class GetMailboxUsageReportCommand : SyntheticCommandWithPipelineInput<MailboxUsageReport, MailboxUsageReport>
	{
		// Token: 0x06003A63 RID: 14947 RVA: 0x0006393E File Offset: 0x00061B3E
		private GetMailboxUsageReportCommand() : base("Get-MailboxUsageReport")
		{
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x0006394B File Offset: 0x00061B4B
		public GetMailboxUsageReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x0006395A File Offset: 0x00061B5A
		public virtual GetMailboxUsageReportCommand SetParameters(GetMailboxUsageReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003BE RID: 958
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EA2 RID: 7842
			// (set) Token: 0x06003A66 RID: 14950 RVA: 0x00063964 File Offset: 0x00061B64
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EA3 RID: 7843
			// (set) Token: 0x06003A67 RID: 14951 RVA: 0x00063982 File Offset: 0x00061B82
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001EA4 RID: 7844
			// (set) Token: 0x06003A68 RID: 14952 RVA: 0x0006399A File Offset: 0x00061B9A
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001EA5 RID: 7845
			// (set) Token: 0x06003A69 RID: 14953 RVA: 0x000639B2 File Offset: 0x00061BB2
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001EA6 RID: 7846
			// (set) Token: 0x06003A6A RID: 14954 RVA: 0x000639CA File Offset: 0x00061BCA
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001EA7 RID: 7847
			// (set) Token: 0x06003A6B RID: 14955 RVA: 0x000639DD File Offset: 0x00061BDD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001EA8 RID: 7848
			// (set) Token: 0x06003A6C RID: 14956 RVA: 0x000639F5 File Offset: 0x00061BF5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EA9 RID: 7849
			// (set) Token: 0x06003A6D RID: 14957 RVA: 0x00063A0D File Offset: 0x00061C0D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EAA RID: 7850
			// (set) Token: 0x06003A6E RID: 14958 RVA: 0x00063A25 File Offset: 0x00061C25
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
