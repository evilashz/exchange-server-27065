using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200039D RID: 925
	public class GetConnectionByClientTypeDetailReportCommand : SyntheticCommandWithPipelineInput<ConnectionByClientTypeDetailReport, ConnectionByClientTypeDetailReport>
	{
		// Token: 0x06003986 RID: 14726 RVA: 0x00062796 File Offset: 0x00060996
		private GetConnectionByClientTypeDetailReportCommand() : base("Get-ConnectionByClientTypeDetailReport")
		{
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000627A3 File Offset: 0x000609A3
		public GetConnectionByClientTypeDetailReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000627B2 File Offset: 0x000609B2
		public virtual GetConnectionByClientTypeDetailReportCommand SetParameters(GetConnectionByClientTypeDetailReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200039E RID: 926
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E05 RID: 7685
			// (set) Token: 0x06003989 RID: 14729 RVA: 0x000627BC File Offset: 0x000609BC
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E06 RID: 7686
			// (set) Token: 0x0600398A RID: 14730 RVA: 0x000627D4 File Offset: 0x000609D4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E07 RID: 7687
			// (set) Token: 0x0600398B RID: 14731 RVA: 0x000627F2 File Offset: 0x000609F2
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E08 RID: 7688
			// (set) Token: 0x0600398C RID: 14732 RVA: 0x0006280A File Offset: 0x00060A0A
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E09 RID: 7689
			// (set) Token: 0x0600398D RID: 14733 RVA: 0x00062822 File Offset: 0x00060A22
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E0A RID: 7690
			// (set) Token: 0x0600398E RID: 14734 RVA: 0x0006283A File Offset: 0x00060A3A
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E0B RID: 7691
			// (set) Token: 0x0600398F RID: 14735 RVA: 0x0006284D File Offset: 0x00060A4D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E0C RID: 7692
			// (set) Token: 0x06003990 RID: 14736 RVA: 0x00062865 File Offset: 0x00060A65
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E0D RID: 7693
			// (set) Token: 0x06003991 RID: 14737 RVA: 0x0006287D File Offset: 0x00060A7D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E0E RID: 7694
			// (set) Token: 0x06003992 RID: 14738 RVA: 0x00062895 File Offset: 0x00060A95
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
