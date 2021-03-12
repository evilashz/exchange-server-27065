using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E9 RID: 233
	internal class MonadCommandExecutionContextForPropertyPageFactory : ICommandExecutionContextFactory
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		public CommandExecutionContext CreateExecutionContext()
		{
			return new MonadCommandExecutionContext
			{
				IsPropertyPage = true
			};
		}
	}
}
