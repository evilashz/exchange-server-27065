using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003CF RID: 975
	public class GetSPOOneDriveForBusinessUserStatisticsReportCommand : SyntheticCommandWithPipelineInput<SPOODBUserStatisticsReport, SPOODBUserStatisticsReport>
	{
		// Token: 0x06003ADC RID: 15068 RVA: 0x000642DD File Offset: 0x000624DD
		private GetSPOOneDriveForBusinessUserStatisticsReportCommand() : base("Get-SPOOneDriveForBusinessUserStatisticsReport")
		{
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000642EA File Offset: 0x000624EA
		public GetSPOOneDriveForBusinessUserStatisticsReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000642F9 File Offset: 0x000624F9
		public virtual GetSPOOneDriveForBusinessUserStatisticsReportCommand SetParameters(GetSPOOneDriveForBusinessUserStatisticsReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003D0 RID: 976
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EF7 RID: 7927
			// (set) Token: 0x06003ADF RID: 15071 RVA: 0x00064303 File Offset: 0x00062503
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EF8 RID: 7928
			// (set) Token: 0x06003AE0 RID: 15072 RVA: 0x00064321 File Offset: 0x00062521
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001EF9 RID: 7929
			// (set) Token: 0x06003AE1 RID: 15073 RVA: 0x00064339 File Offset: 0x00062539
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001EFA RID: 7930
			// (set) Token: 0x06003AE2 RID: 15074 RVA: 0x00064351 File Offset: 0x00062551
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001EFB RID: 7931
			// (set) Token: 0x06003AE3 RID: 15075 RVA: 0x00064369 File Offset: 0x00062569
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001EFC RID: 7932
			// (set) Token: 0x06003AE4 RID: 15076 RVA: 0x0006437C File Offset: 0x0006257C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001EFD RID: 7933
			// (set) Token: 0x06003AE5 RID: 15077 RVA: 0x00064394 File Offset: 0x00062594
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EFE RID: 7934
			// (set) Token: 0x06003AE6 RID: 15078 RVA: 0x000643AC File Offset: 0x000625AC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EFF RID: 7935
			// (set) Token: 0x06003AE7 RID: 15079 RVA: 0x000643C4 File Offset: 0x000625C4
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
