using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003C9 RID: 969
	public class GetScorecardMetricsReportCommand : SyntheticCommandWithPipelineInput<ScorecardMetricsReport, ScorecardMetricsReport>
	{
		// Token: 0x06003AB4 RID: 15028 RVA: 0x00063FB0 File Offset: 0x000621B0
		private GetScorecardMetricsReportCommand() : base("Get-ScorecardMetricsReport")
		{
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x00063FBD File Offset: 0x000621BD
		public GetScorecardMetricsReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x00063FCC File Offset: 0x000621CC
		public virtual GetScorecardMetricsReportCommand SetParameters(GetScorecardMetricsReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003CA RID: 970
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EDB RID: 7899
			// (set) Token: 0x06003AB7 RID: 15031 RVA: 0x00063FD6 File Offset: 0x000621D6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EDC RID: 7900
			// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x00063FF4 File Offset: 0x000621F4
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001EDD RID: 7901
			// (set) Token: 0x06003AB9 RID: 15033 RVA: 0x0006400C File Offset: 0x0006220C
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001EDE RID: 7902
			// (set) Token: 0x06003ABA RID: 15034 RVA: 0x00064024 File Offset: 0x00062224
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001EDF RID: 7903
			// (set) Token: 0x06003ABB RID: 15035 RVA: 0x0006403C File Offset: 0x0006223C
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001EE0 RID: 7904
			// (set) Token: 0x06003ABC RID: 15036 RVA: 0x0006404F File Offset: 0x0006224F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001EE1 RID: 7905
			// (set) Token: 0x06003ABD RID: 15037 RVA: 0x00064067 File Offset: 0x00062267
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EE2 RID: 7906
			// (set) Token: 0x06003ABE RID: 15038 RVA: 0x0006407F File Offset: 0x0006227F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EE3 RID: 7907
			// (set) Token: 0x06003ABF RID: 15039 RVA: 0x00064097 File Offset: 0x00062297
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
