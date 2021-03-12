using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallCacCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x000074ED File Offset: 0x000056ED
		public InstallCacCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "ClientAccessRole", "Install-ClientAccessRole", connection)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007501 File Offset: 0x00005701
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			SetupLogger.TraceExit();
		}
	}
}
