using System;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001DB RID: 475
	internal class MonadRunspaceFactory : RunspaceFactory
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x00034A04 File Offset: 0x00032C04
		public static MonadRunspaceFactory GetInstance()
		{
			if (MonadRunspaceFactory.instance == null)
			{
				MonadRunspaceFactory.instance = new MonadRunspaceFactory();
			}
			return MonadRunspaceFactory.instance;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00034A1C File Offset: 0x00032C1C
		internal MonadRunspaceFactory() : base(MonadRunspaceConfigurationFactory.GetInstance(), MonadHostFactory.GetInstance())
		{
		}

		// Token: 0x040003CB RID: 971
		private static MonadRunspaceFactory instance;
	}
}
