using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E7 RID: 231
	internal class MonadDataAdapterExecutionContextFactory : IDataAdapterExecutionContextFactory
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x0001D2C6 File Offset: 0x0001B4C6
		public DataAdapterExecutionContext CreateExecutionContext()
		{
			return new MonadDataAdapterExecutionContext();
		}
	}
}
