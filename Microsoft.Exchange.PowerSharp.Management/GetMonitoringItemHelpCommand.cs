using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002B3 RID: 691
	public class GetMonitoringItemHelpCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x060030D5 RID: 12501 RVA: 0x00057599 File Offset: 0x00055799
		private GetMonitoringItemHelpCommand() : base("Get-MonitoringItemHelp")
		{
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000575A6 File Offset: 0x000557A6
		public GetMonitoringItemHelpCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000575B5 File Offset: 0x000557B5
		public virtual GetMonitoringItemHelpCommand SetParameters(GetMonitoringItemHelpCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002B4 RID: 692
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001728 RID: 5928
			// (set) Token: 0x060030D8 RID: 12504 RVA: 0x000575BF File Offset: 0x000557BF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001729 RID: 5929
			// (set) Token: 0x060030D9 RID: 12505 RVA: 0x000575D2 File Offset: 0x000557D2
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700172A RID: 5930
			// (set) Token: 0x060030DA RID: 12506 RVA: 0x000575E5 File Offset: 0x000557E5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700172B RID: 5931
			// (set) Token: 0x060030DB RID: 12507 RVA: 0x000575FD File Offset: 0x000557FD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700172C RID: 5932
			// (set) Token: 0x060030DC RID: 12508 RVA: 0x00057615 File Offset: 0x00055815
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700172D RID: 5933
			// (set) Token: 0x060030DD RID: 12509 RVA: 0x0005762D File Offset: 0x0005582D
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
