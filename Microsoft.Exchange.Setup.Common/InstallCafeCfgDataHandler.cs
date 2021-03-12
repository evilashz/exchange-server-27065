using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallCafeCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00007519 File Offset: 0x00005719
		public InstallCafeCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "CafeRole", "Install-CafeRole", connection)
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000752D File Offset: 0x0000572D
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			SetupLogger.TraceExit();
		}
	}
}
