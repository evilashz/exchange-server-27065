using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003C1 RID: 961
	public class GetPartnerCustomerUserReportCommand : SyntheticCommandWithPipelineInput<PartnerCustomerUserReport, PartnerCustomerUserReport>
	{
		// Token: 0x06003A7D RID: 14973 RVA: 0x00063B4C File Offset: 0x00061D4C
		private GetPartnerCustomerUserReportCommand() : base("Get-PartnerCustomerUserReport")
		{
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x00063B59 File Offset: 0x00061D59
		public GetPartnerCustomerUserReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x00063B68 File Offset: 0x00061D68
		public virtual GetPartnerCustomerUserReportCommand SetParameters(GetPartnerCustomerUserReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003C2 RID: 962
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EB4 RID: 7860
			// (set) Token: 0x06003A80 RID: 14976 RVA: 0x00063B72 File Offset: 0x00061D72
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EB5 RID: 7861
			// (set) Token: 0x06003A81 RID: 14977 RVA: 0x00063B90 File Offset: 0x00061D90
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001EB6 RID: 7862
			// (set) Token: 0x06003A82 RID: 14978 RVA: 0x00063BA8 File Offset: 0x00061DA8
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001EB7 RID: 7863
			// (set) Token: 0x06003A83 RID: 14979 RVA: 0x00063BC0 File Offset: 0x00061DC0
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001EB8 RID: 7864
			// (set) Token: 0x06003A84 RID: 14980 RVA: 0x00063BD8 File Offset: 0x00061DD8
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001EB9 RID: 7865
			// (set) Token: 0x06003A85 RID: 14981 RVA: 0x00063BEB File Offset: 0x00061DEB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001EBA RID: 7866
			// (set) Token: 0x06003A86 RID: 14982 RVA: 0x00063C03 File Offset: 0x00061E03
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EBB RID: 7867
			// (set) Token: 0x06003A87 RID: 14983 RVA: 0x00063C1B File Offset: 0x00061E1B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EBC RID: 7868
			// (set) Token: 0x06003A88 RID: 14984 RVA: 0x00063C33 File Offset: 0x00061E33
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
