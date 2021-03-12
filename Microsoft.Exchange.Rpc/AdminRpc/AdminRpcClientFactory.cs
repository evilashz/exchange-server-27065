using System;

namespace Microsoft.Exchange.Rpc.AdminRpc
{
	// Token: 0x020001C0 RID: 448
	public class AdminRpcClientFactory
	{
		// Token: 0x06000988 RID: 2440 RVA: 0x00015D90 File Offset: 0x00015190
		public static object CreateLocal()
		{
			return new AdminRpcClient("localhost", null);
		}
	}
}
