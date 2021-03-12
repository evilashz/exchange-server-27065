using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000179 RID: 377
	public class GetReportScheduleCommand : SyntheticCommandWithPipelineInput<ReportSchedule, ReportSchedule>
	{
		// Token: 0x060022B0 RID: 8880 RVA: 0x00044884 File Offset: 0x00042A84
		private GetReportScheduleCommand() : base("Get-ReportSchedule")
		{
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x00044891 File Offset: 0x00042A91
		public GetReportScheduleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000448A0 File Offset: 0x00042AA0
		public virtual GetReportScheduleCommand SetParameters(GetReportScheduleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200017A RID: 378
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B77 RID: 2935
			// (set) Token: 0x060022B3 RID: 8883 RVA: 0x000448AA File Offset: 0x00042AAA
			public virtual Guid? ScheduleID
			{
				set
				{
					base.PowerSharpParameters["ScheduleID"] = value;
				}
			}

			// Token: 0x17000B78 RID: 2936
			// (set) Token: 0x060022B4 RID: 8884 RVA: 0x000448C2 File Offset: 0x00042AC2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B79 RID: 2937
			// (set) Token: 0x060022B5 RID: 8885 RVA: 0x000448E0 File Offset: 0x00042AE0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B7A RID: 2938
			// (set) Token: 0x060022B6 RID: 8886 RVA: 0x000448F8 File Offset: 0x00042AF8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B7B RID: 2939
			// (set) Token: 0x060022B7 RID: 8887 RVA: 0x00044910 File Offset: 0x00042B10
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B7C RID: 2940
			// (set) Token: 0x060022B8 RID: 8888 RVA: 0x00044928 File Offset: 0x00042B28
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
