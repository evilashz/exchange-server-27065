using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000183 RID: 387
	public class SetReportScheduleCommand : SyntheticCommandWithPipelineInputNoOutput<ReportSchedule>
	{
		// Token: 0x060022F3 RID: 8947 RVA: 0x00044DB4 File Offset: 0x00042FB4
		private SetReportScheduleCommand() : base("Set-ReportSchedule")
		{
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x00044DC1 File Offset: 0x00042FC1
		public SetReportScheduleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x00044DD0 File Offset: 0x00042FD0
		public virtual SetReportScheduleCommand SetParameters(SetReportScheduleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000184 RID: 388
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000BA6 RID: 2982
			// (set) Token: 0x060022F6 RID: 8950 RVA: 0x00044DDA File Offset: 0x00042FDA
			public virtual Guid? ScheduleID
			{
				set
				{
					base.PowerSharpParameters["ScheduleID"] = value;
				}
			}

			// Token: 0x17000BA7 RID: 2983
			// (set) Token: 0x060022F7 RID: 8951 RVA: 0x00044DF2 File Offset: 0x00042FF2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000BA8 RID: 2984
			// (set) Token: 0x060022F8 RID: 8952 RVA: 0x00044E10 File Offset: 0x00043010
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BA9 RID: 2985
			// (set) Token: 0x060022F9 RID: 8953 RVA: 0x00044E28 File Offset: 0x00043028
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BAA RID: 2986
			// (set) Token: 0x060022FA RID: 8954 RVA: 0x00044E40 File Offset: 0x00043040
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BAB RID: 2987
			// (set) Token: 0x060022FB RID: 8955 RVA: 0x00044E58 File Offset: 0x00043058
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
