using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003D9 RID: 985
	public class GetSPOTenantStorageMetricReportCommand : SyntheticCommandWithPipelineInput<SPOTenantStorageMetricReport, SPOTenantStorageMetricReport>
	{
		// Token: 0x06003B21 RID: 15137 RVA: 0x00064860 File Offset: 0x00062A60
		private GetSPOTenantStorageMetricReportCommand() : base("Get-SPOTenantStorageMetricReport")
		{
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x0006486D File Offset: 0x00062A6D
		public GetSPOTenantStorageMetricReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x0006487C File Offset: 0x00062A7C
		public virtual GetSPOTenantStorageMetricReportCommand SetParameters(GetSPOTenantStorageMetricReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003DA RID: 986
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F28 RID: 7976
			// (set) Token: 0x06003B24 RID: 15140 RVA: 0x00064886 File Offset: 0x00062A86
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001F29 RID: 7977
			// (set) Token: 0x06003B25 RID: 15141 RVA: 0x0006489E File Offset: 0x00062A9E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F2A RID: 7978
			// (set) Token: 0x06003B26 RID: 15142 RVA: 0x000648BC File Offset: 0x00062ABC
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001F2B RID: 7979
			// (set) Token: 0x06003B27 RID: 15143 RVA: 0x000648D4 File Offset: 0x00062AD4
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001F2C RID: 7980
			// (set) Token: 0x06003B28 RID: 15144 RVA: 0x000648EC File Offset: 0x00062AEC
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F2D RID: 7981
			// (set) Token: 0x06003B29 RID: 15145 RVA: 0x00064904 File Offset: 0x00062B04
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001F2E RID: 7982
			// (set) Token: 0x06003B2A RID: 15146 RVA: 0x00064917 File Offset: 0x00062B17
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F2F RID: 7983
			// (set) Token: 0x06003B2B RID: 15147 RVA: 0x0006492F File Offset: 0x00062B2F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F30 RID: 7984
			// (set) Token: 0x06003B2C RID: 15148 RVA: 0x00064947 File Offset: 0x00062B47
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F31 RID: 7985
			// (set) Token: 0x06003B2D RID: 15149 RVA: 0x0006495F File Offset: 0x00062B5F
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
