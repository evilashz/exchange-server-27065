using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DisasterRecoveryGwyCfgDataHandler : DisasterRecoveryCfgDataHandler
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006271 File Offset: 0x00004471
		public bool StartTransportService
		{
			get
			{
				return this.gatewayRoleConfigurationInfo.StartTransportService;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000627E File Offset: 0x0000447E
		public DisasterRecoveryGwyCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, RoleManager.GetRoleByName("GatewayRole"), connection)
		{
			this.gatewayRoleConfigurationInfo = (base.InstallableUnitConfigurationInfo as GatewayRoleConfigurationInfo);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000062A3 File Offset: 0x000044A3
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("StartTransportService", this.StartTransportService);
			SetupLogger.TraceExit();
		}

		// Token: 0x04000059 RID: 89
		private GatewayRoleConfigurationInfo gatewayRoleConfigurationInfo;
	}
}
