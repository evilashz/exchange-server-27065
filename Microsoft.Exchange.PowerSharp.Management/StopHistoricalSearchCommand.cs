using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000177 RID: 375
	public class StopHistoricalSearchCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x060022A6 RID: 8870 RVA: 0x000447C0 File Offset: 0x000429C0
		private StopHistoricalSearchCommand() : base("Stop-HistoricalSearch")
		{
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000447CD File Offset: 0x000429CD
		public StopHistoricalSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000447DC File Offset: 0x000429DC
		public virtual StopHistoricalSearchCommand SetParameters(StopHistoricalSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000178 RID: 376
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B71 RID: 2929
			// (set) Token: 0x060022A9 RID: 8873 RVA: 0x000447E6 File Offset: 0x000429E6
			public virtual Guid JobId
			{
				set
				{
					base.PowerSharpParameters["JobId"] = value;
				}
			}

			// Token: 0x17000B72 RID: 2930
			// (set) Token: 0x060022AA RID: 8874 RVA: 0x000447FE File Offset: 0x000429FE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B73 RID: 2931
			// (set) Token: 0x060022AB RID: 8875 RVA: 0x0004481C File Offset: 0x00042A1C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B74 RID: 2932
			// (set) Token: 0x060022AC RID: 8876 RVA: 0x00044834 File Offset: 0x00042A34
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B75 RID: 2933
			// (set) Token: 0x060022AD RID: 8877 RVA: 0x0004484C File Offset: 0x00042A4C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B76 RID: 2934
			// (set) Token: 0x060022AE RID: 8878 RVA: 0x00044864 File Offset: 0x00042A64
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
