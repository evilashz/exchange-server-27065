using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DisasterRecoveryFetCfgDataHandler : DisasterRecoveryCfgDataHandler
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000620B File Offset: 0x0000440B
		public bool StartTransportService
		{
			get
			{
				return this.frontendTransportRoleConfigurationInfo.StartTransportService;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006218 File Offset: 0x00004418
		public DisasterRecoveryFetCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, RoleManager.GetRoleByName("FrontendTransportRole"), connection)
		{
			this.frontendTransportRoleConfigurationInfo = (base.InstallableUnitConfigurationInfo as FrontendTransportRoleConfigurationInfo);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000623D File Offset: 0x0000443D
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("StartTransportService", this.StartTransportService);
			SetupLogger.TraceExit();
		}

		// Token: 0x04000058 RID: 88
		private FrontendTransportRoleConfigurationInfo frontendTransportRoleConfigurationInfo;
	}
}
