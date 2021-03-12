using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003BB RID: 955
	public class GetMailboxUsageDetailReportCommand : SyntheticCommandWithPipelineInput<MailboxUsageDetailReport, MailboxUsageDetailReport>
	{
		// Token: 0x06003A56 RID: 14934 RVA: 0x00063837 File Offset: 0x00061A37
		private GetMailboxUsageDetailReportCommand() : base("Get-MailboxUsageDetailReport")
		{
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x00063844 File Offset: 0x00061A44
		public GetMailboxUsageDetailReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x00063853 File Offset: 0x00061A53
		public virtual GetMailboxUsageDetailReportCommand SetParameters(GetMailboxUsageDetailReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003BC RID: 956
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E99 RID: 7833
			// (set) Token: 0x06003A59 RID: 14937 RVA: 0x0006385D File Offset: 0x00061A5D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E9A RID: 7834
			// (set) Token: 0x06003A5A RID: 14938 RVA: 0x0006387B File Offset: 0x00061A7B
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E9B RID: 7835
			// (set) Token: 0x06003A5B RID: 14939 RVA: 0x00063893 File Offset: 0x00061A93
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E9C RID: 7836
			// (set) Token: 0x06003A5C RID: 14940 RVA: 0x000638AB File Offset: 0x00061AAB
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E9D RID: 7837
			// (set) Token: 0x06003A5D RID: 14941 RVA: 0x000638C3 File Offset: 0x00061AC3
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E9E RID: 7838
			// (set) Token: 0x06003A5E RID: 14942 RVA: 0x000638D6 File Offset: 0x00061AD6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E9F RID: 7839
			// (set) Token: 0x06003A5F RID: 14943 RVA: 0x000638EE File Offset: 0x00061AEE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EA0 RID: 7840
			// (set) Token: 0x06003A60 RID: 14944 RVA: 0x00063906 File Offset: 0x00061B06
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EA1 RID: 7841
			// (set) Token: 0x06003A61 RID: 14945 RVA: 0x0006391E File Offset: 0x00061B1E
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
