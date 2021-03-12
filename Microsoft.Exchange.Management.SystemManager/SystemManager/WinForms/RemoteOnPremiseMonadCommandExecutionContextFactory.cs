using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000EB RID: 235
	internal class RemoteOnPremiseMonadCommandExecutionContextFactory : ICommandExecutionContextFactory
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x0001D316 File Offset: 0x0001B516
		public RemoteOnPremiseMonadCommandExecutionContextFactory(string serverName)
		{
			this.serverName = serverName;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001D328 File Offset: 0x0001B528
		public CommandExecutionContext CreateExecutionContext()
		{
			return new RemoteOnPremiseMonadCommandExecutionContext
			{
				ServerName = this.serverName
			};
		}

		// Token: 0x040003F0 RID: 1008
		private string serverName;
	}
}
