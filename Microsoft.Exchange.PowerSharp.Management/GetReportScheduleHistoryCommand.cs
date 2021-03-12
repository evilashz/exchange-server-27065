using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200017B RID: 379
	public class GetReportScheduleHistoryCommand : SyntheticCommandWithPipelineInput<ReportSchedule, ReportSchedule>
	{
		// Token: 0x060022BA RID: 8890 RVA: 0x00044948 File Offset: 0x00042B48
		private GetReportScheduleHistoryCommand() : base("Get-ReportScheduleHistory")
		{
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00044955 File Offset: 0x00042B55
		public GetReportScheduleHistoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00044964 File Offset: 0x00042B64
		public virtual GetReportScheduleHistoryCommand SetParameters(GetReportScheduleHistoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200017C RID: 380
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B7D RID: 2941
			// (set) Token: 0x060022BD RID: 8893 RVA: 0x0004496E File Offset: 0x00042B6E
			public virtual Guid? ScheduleID
			{
				set
				{
					base.PowerSharpParameters["ScheduleID"] = value;
				}
			}

			// Token: 0x17000B7E RID: 2942
			// (set) Token: 0x060022BE RID: 8894 RVA: 0x00044986 File Offset: 0x00042B86
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B7F RID: 2943
			// (set) Token: 0x060022BF RID: 8895 RVA: 0x000449A4 File Offset: 0x00042BA4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B80 RID: 2944
			// (set) Token: 0x060022C0 RID: 8896 RVA: 0x000449BC File Offset: 0x00042BBC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B81 RID: 2945
			// (set) Token: 0x060022C1 RID: 8897 RVA: 0x000449D4 File Offset: 0x00042BD4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B82 RID: 2946
			// (set) Token: 0x060022C2 RID: 8898 RVA: 0x000449EC File Offset: 0x00042BEC
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
