using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003A7 RID: 935
	public class GetCsConferenceReportCommand : SyntheticCommandWithPipelineInput<CsConferenceReport, CsConferenceReport>
	{
		// Token: 0x060039CB RID: 14795 RVA: 0x00062D19 File Offset: 0x00060F19
		private GetCsConferenceReportCommand() : base("Get-CsConferenceReport")
		{
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x00062D26 File Offset: 0x00060F26
		public GetCsConferenceReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x00062D35 File Offset: 0x00060F35
		public virtual GetCsConferenceReportCommand SetParameters(GetCsConferenceReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003A8 RID: 936
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E36 RID: 7734
			// (set) Token: 0x060039CE RID: 14798 RVA: 0x00062D3F File Offset: 0x00060F3F
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E37 RID: 7735
			// (set) Token: 0x060039CF RID: 14799 RVA: 0x00062D57 File Offset: 0x00060F57
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E38 RID: 7736
			// (set) Token: 0x060039D0 RID: 14800 RVA: 0x00062D75 File Offset: 0x00060F75
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E39 RID: 7737
			// (set) Token: 0x060039D1 RID: 14801 RVA: 0x00062D8D File Offset: 0x00060F8D
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E3A RID: 7738
			// (set) Token: 0x060039D2 RID: 14802 RVA: 0x00062DA5 File Offset: 0x00060FA5
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E3B RID: 7739
			// (set) Token: 0x060039D3 RID: 14803 RVA: 0x00062DBD File Offset: 0x00060FBD
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E3C RID: 7740
			// (set) Token: 0x060039D4 RID: 14804 RVA: 0x00062DD0 File Offset: 0x00060FD0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E3D RID: 7741
			// (set) Token: 0x060039D5 RID: 14805 RVA: 0x00062DE8 File Offset: 0x00060FE8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E3E RID: 7742
			// (set) Token: 0x060039D6 RID: 14806 RVA: 0x00062E00 File Offset: 0x00061000
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E3F RID: 7743
			// (set) Token: 0x060039D7 RID: 14807 RVA: 0x00062E18 File Offset: 0x00061018
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
