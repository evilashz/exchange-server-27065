using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallBhdCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007418 File Offset: 0x00005618
		public bool StartTransportService
		{
			get
			{
				return this.bridgeheadRoleConfigurationInfo.StartTransportService;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007425 File Offset: 0x00005625
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00007432 File Offset: 0x00005632
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

		// Token: 0x060001D0 RID: 464 RVA: 0x00007440 File Offset: 0x00005640
		public InstallBhdCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "BridgeheadRole", "Install-BridgeheadRole", connection)
		{
			this.bridgeheadRoleConfigurationInfo = (BridgeheadRoleConfigurationInfo)base.InstallableUnitConfigurationInfo;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007468 File Offset: 0x00005668
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("StartTransportService", this.StartTransportService);
			base.Parameters.AddWithValue("DisableAMFiltering", this.DisableAMFiltering);
			SetupLogger.TraceExit();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000074C3 File Offset: 0x000056C3
		public override void UpdatePreCheckTaskDataHandler()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.UpdatePreCheckTaskDataHandler();
			PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			SetupLogger.TraceExit();
		}

		// Token: 0x04000073 RID: 115
		private BridgeheadRoleConfigurationInfo bridgeheadRoleConfigurationInfo;
	}
}
