using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000181 RID: 385
	public class RemoveReportScheduleCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x060022E9 RID: 8937 RVA: 0x00044CF0 File Offset: 0x00042EF0
		private RemoveReportScheduleCommand() : base("Remove-ReportSchedule")
		{
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x00044CFD File Offset: 0x00042EFD
		public RemoveReportScheduleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x00044D0C File Offset: 0x00042F0C
		public virtual RemoveReportScheduleCommand SetParameters(RemoveReportScheduleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000182 RID: 386
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000BA0 RID: 2976
			// (set) Token: 0x060022EC RID: 8940 RVA: 0x00044D16 File Offset: 0x00042F16
			public virtual Guid ScheduleID
			{
				set
				{
					base.PowerSharpParameters["ScheduleID"] = value;
				}
			}

			// Token: 0x17000BA1 RID: 2977
			// (set) Token: 0x060022ED RID: 8941 RVA: 0x00044D2E File Offset: 0x00042F2E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000BA2 RID: 2978
			// (set) Token: 0x060022EE RID: 8942 RVA: 0x00044D4C File Offset: 0x00042F4C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BA3 RID: 2979
			// (set) Token: 0x060022EF RID: 8943 RVA: 0x00044D64 File Offset: 0x00042F64
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BA4 RID: 2980
			// (set) Token: 0x060022F0 RID: 8944 RVA: 0x00044D7C File Offset: 0x00042F7C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BA5 RID: 2981
			// (set) Token: 0x060022F1 RID: 8945 RVA: 0x00044D94 File Offset: 0x00042F94
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
