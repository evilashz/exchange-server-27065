using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallFetCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007594 File Offset: 0x00005794
		public bool StartTransportService
		{
			get
			{
				return this.frontendTransportRoleConfigurationInfo.StartTransportService;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000075A1 File Offset: 0x000057A1
		public InstallFetCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "FrontendTransportRole", "Install-FrontendTransportRole", connection)
		{
			this.frontendTransportRoleConfigurationInfo = (FrontendTransportRoleConfigurationInfo)base.InstallableUnitConfigurationInfo;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000075C6 File Offset: 0x000057C6
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("StartTransportService", this.StartTransportService);
			SetupLogger.TraceExit();
		}

		// Token: 0x04000074 RID: 116
		private FrontendTransportRoleConfigurationInfo frontendTransportRoleConfigurationInfo;
	}
}
