using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200086B RID: 2155
	public class GetSyncDaemonArbitrationConfigCommand : SyntheticCommandWithPipelineInput<SyncDaemonArbitrationConfig, SyncDaemonArbitrationConfig>
	{
		// Token: 0x06006AC7 RID: 27335 RVA: 0x000A1F17 File Offset: 0x000A0117
		private GetSyncDaemonArbitrationConfigCommand() : base("Get-SyncDaemonArbitrationConfig")
		{
		}

		// Token: 0x06006AC8 RID: 27336 RVA: 0x000A1F24 File Offset: 0x000A0124
		public GetSyncDaemonArbitrationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x000A1F33 File Offset: 0x000A0133
		public virtual GetSyncDaemonArbitrationConfigCommand SetParameters(GetSyncDaemonArbitrationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200086C RID: 2156
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170045AA RID: 17834
			// (set) Token: 0x06006ACA RID: 27338 RVA: 0x000A1F3D File Offset: 0x000A013D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170045AB RID: 17835
			// (set) Token: 0x06006ACB RID: 27339 RVA: 0x000A1F50 File Offset: 0x000A0150
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170045AC RID: 17836
			// (set) Token: 0x06006ACC RID: 27340 RVA: 0x000A1F68 File Offset: 0x000A0168
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170045AD RID: 17837
			// (set) Token: 0x06006ACD RID: 27341 RVA: 0x000A1F80 File Offset: 0x000A0180
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170045AE RID: 17838
			// (set) Token: 0x06006ACE RID: 27342 RVA: 0x000A1F98 File Offset: 0x000A0198
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
