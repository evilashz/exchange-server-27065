using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E8 RID: 232
	internal class MonadCommandExecutionContextFactory : ICommandExecutionContextFactory
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x0001D2D5 File Offset: 0x0001B4D5
		public CommandExecutionContext CreateExecutionContext()
		{
			return new MonadCommandExecutionContext();
		}
	}
}
