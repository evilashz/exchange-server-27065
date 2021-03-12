using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003CB RID: 971
	public class GetSPOActiveUserReportCommand : SyntheticCommandWithPipelineInput<SPOActiveUserReport, SPOActiveUserReport>
	{
		// Token: 0x06003AC1 RID: 15041 RVA: 0x000640B7 File Offset: 0x000622B7
		private GetSPOActiveUserReportCommand() : base("Get-SPOActiveUserReport")
		{
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000640C4 File Offset: 0x000622C4
		public GetSPOActiveUserReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000640D3 File Offset: 0x000622D3
		public virtual GetSPOActiveUserReportCommand SetParameters(GetSPOActiveUserReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003CC RID: 972
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EE4 RID: 7908
			// (set) Token: 0x06003AC4 RID: 15044 RVA: 0x000640DD File Offset: 0x000622DD
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001EE5 RID: 7909
			// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x000640F5 File Offset: 0x000622F5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EE6 RID: 7910
			// (set) Token: 0x06003AC6 RID: 15046 RVA: 0x00064113 File Offset: 0x00062313
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001EE7 RID: 7911
			// (set) Token: 0x06003AC7 RID: 15047 RVA: 0x0006412B File Offset: 0x0006232B
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001EE8 RID: 7912
			// (set) Token: 0x06003AC8 RID: 15048 RVA: 0x00064143 File Offset: 0x00062343
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001EE9 RID: 7913
			// (set) Token: 0x06003AC9 RID: 15049 RVA: 0x0006415B File Offset: 0x0006235B
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001EEA RID: 7914
			// (set) Token: 0x06003ACA RID: 15050 RVA: 0x0006416E File Offset: 0x0006236E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001EEB RID: 7915
			// (set) Token: 0x06003ACB RID: 15051 RVA: 0x00064186 File Offset: 0x00062386
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EEC RID: 7916
			// (set) Token: 0x06003ACC RID: 15052 RVA: 0x0006419E File Offset: 0x0006239E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EED RID: 7917
			// (set) Token: 0x06003ACD RID: 15053 RVA: 0x000641B6 File Offset: 0x000623B6
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
