using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200005E RID: 94
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UninstallCfgDataHandler : ConfigurationDataHandler
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000FDAE File Offset: 0x0000DFAE
		public UninstallCfgDataHandler(ISetupContext context, Role roleInfo, MonadConnection connection) : base(context, roleInfo.RoleName, "Uninstall-" + roleInfo.RoleName, connection)
		{
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000FDCE File Offset: 0x0000DFCE
		protected override void AddParameters()
		{
			base.AddParameters();
			if (base.SetupContext.IsDatacenter && base.SetupContext.IsFfo)
			{
				base.Parameters.AddWithValue("IsFfo", true);
			}
		}
	}
}
