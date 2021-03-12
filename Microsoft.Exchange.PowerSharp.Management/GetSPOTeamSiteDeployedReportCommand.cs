using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003D5 RID: 981
	public class GetSPOTeamSiteDeployedReportCommand : SyntheticCommandWithPipelineInput<SPOTeamSiteDeployedReport, SPOTeamSiteDeployedReport>
	{
		// Token: 0x06003B05 RID: 15109 RVA: 0x00064622 File Offset: 0x00062822
		private GetSPOTeamSiteDeployedReportCommand() : base("Get-SPOTeamSiteDeployedReport")
		{
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x0006462F File Offset: 0x0006282F
		public GetSPOTeamSiteDeployedReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x0006463E File Offset: 0x0006283E
		public virtual GetSPOTeamSiteDeployedReportCommand SetParameters(GetSPOTeamSiteDeployedReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003D6 RID: 982
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F14 RID: 7956
			// (set) Token: 0x06003B08 RID: 15112 RVA: 0x00064648 File Offset: 0x00062848
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001F15 RID: 7957
			// (set) Token: 0x06003B09 RID: 15113 RVA: 0x00064660 File Offset: 0x00062860
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F16 RID: 7958
			// (set) Token: 0x06003B0A RID: 15114 RVA: 0x0006467E File Offset: 0x0006287E
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001F17 RID: 7959
			// (set) Token: 0x06003B0B RID: 15115 RVA: 0x00064696 File Offset: 0x00062896
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001F18 RID: 7960
			// (set) Token: 0x06003B0C RID: 15116 RVA: 0x000646AE File Offset: 0x000628AE
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F19 RID: 7961
			// (set) Token: 0x06003B0D RID: 15117 RVA: 0x000646C6 File Offset: 0x000628C6
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001F1A RID: 7962
			// (set) Token: 0x06003B0E RID: 15118 RVA: 0x000646D9 File Offset: 0x000628D9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F1B RID: 7963
			// (set) Token: 0x06003B0F RID: 15119 RVA: 0x000646F1 File Offset: 0x000628F1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F1C RID: 7964
			// (set) Token: 0x06003B10 RID: 15120 RVA: 0x00064709 File Offset: 0x00062909
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F1D RID: 7965
			// (set) Token: 0x06003B11 RID: 15121 RVA: 0x00064721 File Offset: 0x00062921
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
