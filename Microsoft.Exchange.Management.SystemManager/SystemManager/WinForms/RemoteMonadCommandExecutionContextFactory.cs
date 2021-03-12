using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000EA RID: 234
	internal class RemoteMonadCommandExecutionContextFactory : ICommandExecutionContextFactory
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0001D307 File Offset: 0x0001B507
		public CommandExecutionContext CreateExecutionContext()
		{
			return new RemoteMonadCommandExecutionContext();
		}
	}
}
