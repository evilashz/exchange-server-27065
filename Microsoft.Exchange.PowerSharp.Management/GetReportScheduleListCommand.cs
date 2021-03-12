using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200017D RID: 381
	public class GetReportScheduleListCommand : SyntheticCommandWithPipelineInput<ReportSchedule, ReportSchedule>
	{
		// Token: 0x060022C4 RID: 8900 RVA: 0x00044A0C File Offset: 0x00042C0C
		private GetReportScheduleListCommand() : base("Get-ReportScheduleList")
		{
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x00044A19 File Offset: 0x00042C19
		public GetReportScheduleListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00044A28 File Offset: 0x00042C28
		public virtual GetReportScheduleListCommand SetParameters(GetReportScheduleListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200017E RID: 382
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B83 RID: 2947
			// (set) Token: 0x060022C7 RID: 8903 RVA: 0x00044A32 File Offset: 0x00042C32
			public virtual Guid? ScheduleID
			{
				set
				{
					base.PowerSharpParameters["ScheduleID"] = value;
				}
			}

			// Token: 0x17000B84 RID: 2948
			// (set) Token: 0x060022C8 RID: 8904 RVA: 0x00044A4A File Offset: 0x00042C4A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B85 RID: 2949
			// (set) Token: 0x060022C9 RID: 8905 RVA: 0x00044A68 File Offset: 0x00042C68
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B86 RID: 2950
			// (set) Token: 0x060022CA RID: 8906 RVA: 0x00044A80 File Offset: 0x00042C80
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B87 RID: 2951
			// (set) Token: 0x060022CB RID: 8907 RVA: 0x00044A98 File Offset: 0x00042C98
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B88 RID: 2952
			// (set) Token: 0x060022CC RID: 8908 RVA: 0x00044AB0 File Offset: 0x00042CB0
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
