using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003D1 RID: 977
	public class GetSPOSkyDriveProDeployedReportCommand : SyntheticCommandWithPipelineInput<SPOSkyDriveProDeployedReport, SPOSkyDriveProDeployedReport>
	{
		// Token: 0x06003AE9 RID: 15081 RVA: 0x000643E4 File Offset: 0x000625E4
		private GetSPOSkyDriveProDeployedReportCommand() : base("Get-SPOSkyDriveProDeployedReport")
		{
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x000643F1 File Offset: 0x000625F1
		public GetSPOSkyDriveProDeployedReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x00064400 File Offset: 0x00062600
		public virtual GetSPOSkyDriveProDeployedReportCommand SetParameters(GetSPOSkyDriveProDeployedReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003D2 RID: 978
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F00 RID: 7936
			// (set) Token: 0x06003AEC RID: 15084 RVA: 0x0006440A File Offset: 0x0006260A
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001F01 RID: 7937
			// (set) Token: 0x06003AED RID: 15085 RVA: 0x00064422 File Offset: 0x00062622
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F02 RID: 7938
			// (set) Token: 0x06003AEE RID: 15086 RVA: 0x00064440 File Offset: 0x00062640
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001F03 RID: 7939
			// (set) Token: 0x06003AEF RID: 15087 RVA: 0x00064458 File Offset: 0x00062658
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001F04 RID: 7940
			// (set) Token: 0x06003AF0 RID: 15088 RVA: 0x00064470 File Offset: 0x00062670
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F05 RID: 7941
			// (set) Token: 0x06003AF1 RID: 15089 RVA: 0x00064488 File Offset: 0x00062688
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001F06 RID: 7942
			// (set) Token: 0x06003AF2 RID: 15090 RVA: 0x0006449B File Offset: 0x0006269B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F07 RID: 7943
			// (set) Token: 0x06003AF3 RID: 15091 RVA: 0x000644B3 File Offset: 0x000626B3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F08 RID: 7944
			// (set) Token: 0x06003AF4 RID: 15092 RVA: 0x000644CB File Offset: 0x000626CB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F09 RID: 7945
			// (set) Token: 0x06003AF5 RID: 15093 RVA: 0x000644E3 File Offset: 0x000626E3
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
