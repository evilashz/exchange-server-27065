using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000064 RID: 100
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UninstallMbxCfgDataHandler : UninstallCfgDataHandler
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public UninstallMbxCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo, connection)
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000FFEB File Offset: 0x0000E1EB
		public override void UpdatePreCheckTaskDataHandler()
		{
			base.UpdatePreCheckTaskDataHandler();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000FFF3 File Offset: 0x0000E1F3
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			SetupLogger.TraceExit();
		}
	}
}
