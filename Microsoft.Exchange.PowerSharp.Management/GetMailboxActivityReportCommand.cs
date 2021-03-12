using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003B9 RID: 953
	public class GetMailboxActivityReportCommand : SyntheticCommandWithPipelineInput<MailboxActivityReport, MailboxActivityReport>
	{
		// Token: 0x06003A48 RID: 14920 RVA: 0x00063718 File Offset: 0x00061918
		private GetMailboxActivityReportCommand() : base("Get-MailboxActivityReport")
		{
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x00063725 File Offset: 0x00061925
		public GetMailboxActivityReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x00063734 File Offset: 0x00061934
		public virtual GetMailboxActivityReportCommand SetParameters(GetMailboxActivityReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003BA RID: 954
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E8F RID: 7823
			// (set) Token: 0x06003A4B RID: 14923 RVA: 0x0006373E File Offset: 0x0006193E
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E90 RID: 7824
			// (set) Token: 0x06003A4C RID: 14924 RVA: 0x00063756 File Offset: 0x00061956
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E91 RID: 7825
			// (set) Token: 0x06003A4D RID: 14925 RVA: 0x00063774 File Offset: 0x00061974
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E92 RID: 7826
			// (set) Token: 0x06003A4E RID: 14926 RVA: 0x0006378C File Offset: 0x0006198C
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E93 RID: 7827
			// (set) Token: 0x06003A4F RID: 14927 RVA: 0x000637A4 File Offset: 0x000619A4
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E94 RID: 7828
			// (set) Token: 0x06003A50 RID: 14928 RVA: 0x000637BC File Offset: 0x000619BC
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E95 RID: 7829
			// (set) Token: 0x06003A51 RID: 14929 RVA: 0x000637CF File Offset: 0x000619CF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E96 RID: 7830
			// (set) Token: 0x06003A52 RID: 14930 RVA: 0x000637E7 File Offset: 0x000619E7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E97 RID: 7831
			// (set) Token: 0x06003A53 RID: 14931 RVA: 0x000637FF File Offset: 0x000619FF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E98 RID: 7832
			// (set) Token: 0x06003A54 RID: 14932 RVA: 0x00063817 File Offset: 0x00061A17
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
