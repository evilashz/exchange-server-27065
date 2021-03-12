using System;
using System.Management.Automation.Runspaces;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001DC RID: 476
	internal class MonadRunspaceConfigurationFactory : RunspaceConfigurationFactory
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x00034A2E File Offset: 0x00032C2E
		public static MonadRunspaceConfigurationFactory GetInstance()
		{
			if (MonadRunspaceConfigurationFactory.instance == null)
			{
				MonadRunspaceConfigurationFactory.instance = new MonadRunspaceConfigurationFactory();
			}
			return MonadRunspaceConfigurationFactory.instance;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00034A48 File Offset: 0x00032C48
		public override RunspaceConfiguration GetRunspaceConfiguration()
		{
			if (this.runspaceConfiguration == null)
			{
				lock (this.syncInstance)
				{
					if (this.runspaceConfiguration == null)
					{
						this.runspaceConfiguration = this.CreateRunspaceConfiguration();
					}
				}
			}
			return this.runspaceConfiguration;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00034AA4 File Offset: 0x00032CA4
		public override RunspaceConfiguration CreateRunspaceConfiguration()
		{
			return MonadRunspaceConfiguration.Create();
		}

		// Token: 0x040003CC RID: 972
		private static MonadRunspaceConfigurationFactory instance;

		// Token: 0x040003CD RID: 973
		private object syncInstance = new object();

		// Token: 0x040003CE RID: 974
		private RunspaceConfiguration runspaceConfiguration;
	}
}
