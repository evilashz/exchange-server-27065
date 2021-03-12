using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DisasterRecoveryBhdCfgDataHandler : DisasterRecoveryCfgDataHandler
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00006162 File Offset: 0x00004362
		public bool StartTransportService
		{
			get
			{
				return this.bridgeheadRoleConfigurationInfo.StartTransportService;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000616F File Offset: 0x0000436F
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000617C File Offset: 0x0000437C
		public bool DisableAMFiltering
		{
			get
			{
				return this.bridgeheadRoleConfigurationInfo.DisableAMFiltering;
			}
			set
			{
				this.bridgeheadRoleConfigurationInfo.DisableAMFiltering = value;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000618A File Offset: 0x0000438A
		public DisasterRecoveryBhdCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, RoleManager.GetRoleByName("BridgeheadRole"), connection)
		{
			this.bridgeheadRoleConfigurationInfo = (base.InstallableUnitConfigurationInfo as BridgeheadRoleConfigurationInfo);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000061B0 File Offset: 0x000043B0
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("StartTransportService", this.StartTransportService);
			base.Parameters.AddWithValue("DisableAMFiltering", this.DisableAMFiltering);
			SetupLogger.TraceExit();
		}

		// Token: 0x04000057 RID: 87
		private BridgeheadRoleConfigurationInfo bridgeheadRoleConfigurationInfo;
	}
}
