using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000128 RID: 296
	public class GetSyncServiceInstanceCommand : SyntheticCommandWithPipelineInput<SyncServiceInstance, SyncServiceInstance>
	{
		// Token: 0x06001FD3 RID: 8147 RVA: 0x00040F55 File Offset: 0x0003F155
		private GetSyncServiceInstanceCommand() : base("Get-SyncServiceInstance")
		{
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x00040F62 File Offset: 0x0003F162
		public GetSyncServiceInstanceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x00040F71 File Offset: 0x0003F171
		public virtual GetSyncServiceInstanceCommand SetParameters(GetSyncServiceInstanceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000129 RID: 297
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700093C RID: 2364
			// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x00040F7B File Offset: 0x0003F17B
			public virtual ServiceInstanceIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700093D RID: 2365
			// (set) Token: 0x06001FD7 RID: 8151 RVA: 0x00040F8E File Offset: 0x0003F18E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700093E RID: 2366
			// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x00040FA6 File Offset: 0x0003F1A6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700093F RID: 2367
			// (set) Token: 0x06001FD9 RID: 8153 RVA: 0x00040FBE File Offset: 0x0003F1BE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000940 RID: 2368
			// (set) Token: 0x06001FDA RID: 8154 RVA: 0x00040FD6 File Offset: 0x0003F1D6
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
