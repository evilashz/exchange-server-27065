using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InstallOSPCfgDataHandler : InstallRoleBaseDataHandler
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000A0FA File Offset: 0x000082FA
		public InstallOSPCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "OSPRole", "Install-OSPRole", connection)
		{
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A10E File Offset: 0x0000830E
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			SetupLogger.TraceExit();
		}
	}
}
