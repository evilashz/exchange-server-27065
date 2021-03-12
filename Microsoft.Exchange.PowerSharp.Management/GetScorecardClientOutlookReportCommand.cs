using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003C7 RID: 967
	public class GetScorecardClientOutlookReportCommand : SyntheticCommandWithPipelineInput<ScorecardClientOutlookReport, ScorecardClientOutlookReport>
	{
		// Token: 0x06003AA6 RID: 15014 RVA: 0x00063E91 File Offset: 0x00062091
		private GetScorecardClientOutlookReportCommand() : base("Get-ScorecardClientOutlookReport")
		{
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x00063E9E File Offset: 0x0006209E
		public GetScorecardClientOutlookReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x00063EAD File Offset: 0x000620AD
		public virtual GetScorecardClientOutlookReportCommand SetParameters(GetScorecardClientOutlookReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003C8 RID: 968
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001ED1 RID: 7889
			// (set) Token: 0x06003AA9 RID: 15017 RVA: 0x00063EB7 File Offset: 0x000620B7
			public virtual DataCategory Category
			{
				set
				{
					base.PowerSharpParameters["Category"] = value;
				}
			}

			// Token: 0x17001ED2 RID: 7890
			// (set) Token: 0x06003AAA RID: 15018 RVA: 0x00063ECF File Offset: 0x000620CF
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001ED3 RID: 7891
			// (set) Token: 0x06003AAB RID: 15019 RVA: 0x00063EED File Offset: 0x000620ED
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001ED4 RID: 7892
			// (set) Token: 0x06003AAC RID: 15020 RVA: 0x00063F05 File Offset: 0x00062105
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001ED5 RID: 7893
			// (set) Token: 0x06003AAD RID: 15021 RVA: 0x00063F1D File Offset: 0x0006211D
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001ED6 RID: 7894
			// (set) Token: 0x06003AAE RID: 15022 RVA: 0x00063F35 File Offset: 0x00062135
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001ED7 RID: 7895
			// (set) Token: 0x06003AAF RID: 15023 RVA: 0x00063F48 File Offset: 0x00062148
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001ED8 RID: 7896
			// (set) Token: 0x06003AB0 RID: 15024 RVA: 0x00063F60 File Offset: 0x00062160
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001ED9 RID: 7897
			// (set) Token: 0x06003AB1 RID: 15025 RVA: 0x00063F78 File Offset: 0x00062178
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EDA RID: 7898
			// (set) Token: 0x06003AB2 RID: 15026 RVA: 0x00063F90 File Offset: 0x00062190
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
