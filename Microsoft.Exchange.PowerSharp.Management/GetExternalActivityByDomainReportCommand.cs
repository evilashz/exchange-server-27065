using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003AD RID: 941
	public class GetExternalActivityByDomainReportCommand : SyntheticCommandWithPipelineInput<ExternalActivityByDomainReport, ExternalActivityByDomainReport>
	{
		// Token: 0x060039F5 RID: 14837 RVA: 0x00063076 File Offset: 0x00061276
		private GetExternalActivityByDomainReportCommand() : base("Get-ExternalActivityByDomainReport")
		{
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x00063083 File Offset: 0x00061283
		public GetExternalActivityByDomainReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x00063092 File Offset: 0x00061292
		public virtual GetExternalActivityByDomainReportCommand SetParameters(GetExternalActivityByDomainReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003AE RID: 942
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E54 RID: 7764
			// (set) Token: 0x060039F8 RID: 14840 RVA: 0x0006309C File Offset: 0x0006129C
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E55 RID: 7765
			// (set) Token: 0x060039F9 RID: 14841 RVA: 0x000630B4 File Offset: 0x000612B4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E56 RID: 7766
			// (set) Token: 0x060039FA RID: 14842 RVA: 0x000630D2 File Offset: 0x000612D2
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E57 RID: 7767
			// (set) Token: 0x060039FB RID: 14843 RVA: 0x000630EA File Offset: 0x000612EA
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E58 RID: 7768
			// (set) Token: 0x060039FC RID: 14844 RVA: 0x00063102 File Offset: 0x00061302
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E59 RID: 7769
			// (set) Token: 0x060039FD RID: 14845 RVA: 0x0006311A File Offset: 0x0006131A
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E5A RID: 7770
			// (set) Token: 0x060039FE RID: 14846 RVA: 0x0006312D File Offset: 0x0006132D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E5B RID: 7771
			// (set) Token: 0x060039FF RID: 14847 RVA: 0x00063145 File Offset: 0x00061345
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E5C RID: 7772
			// (set) Token: 0x06003A00 RID: 14848 RVA: 0x0006315D File Offset: 0x0006135D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E5D RID: 7773
			// (set) Token: 0x06003A01 RID: 14849 RVA: 0x00063175 File Offset: 0x00061375
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
