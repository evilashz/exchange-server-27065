using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003D7 RID: 983
	public class GetSPOTeamSiteStorageReportCommand : SyntheticCommandWithPipelineInput<SPOTeamSiteStorageReport, SPOTeamSiteStorageReport>
	{
		// Token: 0x06003B13 RID: 15123 RVA: 0x00064741 File Offset: 0x00062941
		private GetSPOTeamSiteStorageReportCommand() : base("Get-SPOTeamSiteStorageReport")
		{
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x0006474E File Offset: 0x0006294E
		public GetSPOTeamSiteStorageReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x0006475D File Offset: 0x0006295D
		public virtual GetSPOTeamSiteStorageReportCommand SetParameters(GetSPOTeamSiteStorageReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003D8 RID: 984
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F1E RID: 7966
			// (set) Token: 0x06003B16 RID: 15126 RVA: 0x00064767 File Offset: 0x00062967
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001F1F RID: 7967
			// (set) Token: 0x06003B17 RID: 15127 RVA: 0x0006477F File Offset: 0x0006297F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F20 RID: 7968
			// (set) Token: 0x06003B18 RID: 15128 RVA: 0x0006479D File Offset: 0x0006299D
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001F21 RID: 7969
			// (set) Token: 0x06003B19 RID: 15129 RVA: 0x000647B5 File Offset: 0x000629B5
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001F22 RID: 7970
			// (set) Token: 0x06003B1A RID: 15130 RVA: 0x000647CD File Offset: 0x000629CD
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F23 RID: 7971
			// (set) Token: 0x06003B1B RID: 15131 RVA: 0x000647E5 File Offset: 0x000629E5
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001F24 RID: 7972
			// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000647F8 File Offset: 0x000629F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F25 RID: 7973
			// (set) Token: 0x06003B1D RID: 15133 RVA: 0x00064810 File Offset: 0x00062A10
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F26 RID: 7974
			// (set) Token: 0x06003B1E RID: 15134 RVA: 0x00064828 File Offset: 0x00062A28
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F27 RID: 7975
			// (set) Token: 0x06003B1F RID: 15135 RVA: 0x00064840 File Offset: 0x00062A40
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
