using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003A9 RID: 937
	public class GetCsP2PAVTimeReportCommand : SyntheticCommandWithPipelineInput<CsP2PAVTimeReport, CsP2PAVTimeReport>
	{
		// Token: 0x060039D9 RID: 14809 RVA: 0x00062E38 File Offset: 0x00061038
		private GetCsP2PAVTimeReportCommand() : base("Get-CsP2PAVTimeReport")
		{
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x00062E45 File Offset: 0x00061045
		public GetCsP2PAVTimeReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x00062E54 File Offset: 0x00061054
		public virtual GetCsP2PAVTimeReportCommand SetParameters(GetCsP2PAVTimeReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003AA RID: 938
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E40 RID: 7744
			// (set) Token: 0x060039DC RID: 14812 RVA: 0x00062E5E File Offset: 0x0006105E
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E41 RID: 7745
			// (set) Token: 0x060039DD RID: 14813 RVA: 0x00062E76 File Offset: 0x00061076
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E42 RID: 7746
			// (set) Token: 0x060039DE RID: 14814 RVA: 0x00062E94 File Offset: 0x00061094
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E43 RID: 7747
			// (set) Token: 0x060039DF RID: 14815 RVA: 0x00062EAC File Offset: 0x000610AC
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E44 RID: 7748
			// (set) Token: 0x060039E0 RID: 14816 RVA: 0x00062EC4 File Offset: 0x000610C4
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E45 RID: 7749
			// (set) Token: 0x060039E1 RID: 14817 RVA: 0x00062EDC File Offset: 0x000610DC
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E46 RID: 7750
			// (set) Token: 0x060039E2 RID: 14818 RVA: 0x00062EEF File Offset: 0x000610EF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E47 RID: 7751
			// (set) Token: 0x060039E3 RID: 14819 RVA: 0x00062F07 File Offset: 0x00061107
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E48 RID: 7752
			// (set) Token: 0x060039E4 RID: 14820 RVA: 0x00062F1F File Offset: 0x0006111F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E49 RID: 7753
			// (set) Token: 0x060039E5 RID: 14821 RVA: 0x00062F37 File Offset: 0x00061137
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
