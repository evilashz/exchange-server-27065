using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002B5 RID: 693
	public class GetMonitoringItemIdentityCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x060030DF RID: 12511 RVA: 0x0005764D File Offset: 0x0005584D
		private GetMonitoringItemIdentityCommand() : base("Get-MonitoringItemIdentity")
		{
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0005765A File Offset: 0x0005585A
		public GetMonitoringItemIdentityCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x00057669 File Offset: 0x00055869
		public virtual GetMonitoringItemIdentityCommand SetParameters(GetMonitoringItemIdentityCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002B6 RID: 694
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700172E RID: 5934
			// (set) Token: 0x060030E2 RID: 12514 RVA: 0x00057673 File Offset: 0x00055873
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700172F RID: 5935
			// (set) Token: 0x060030E3 RID: 12515 RVA: 0x00057686 File Offset: 0x00055886
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17001730 RID: 5936
			// (set) Token: 0x060030E4 RID: 12516 RVA: 0x00057699 File Offset: 0x00055899
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001731 RID: 5937
			// (set) Token: 0x060030E5 RID: 12517 RVA: 0x000576B1 File Offset: 0x000558B1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001732 RID: 5938
			// (set) Token: 0x060030E6 RID: 12518 RVA: 0x000576C9 File Offset: 0x000558C9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001733 RID: 5939
			// (set) Token: 0x060030E7 RID: 12519 RVA: 0x000576E1 File Offset: 0x000558E1
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
