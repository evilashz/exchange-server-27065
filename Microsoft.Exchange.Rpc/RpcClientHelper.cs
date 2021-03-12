using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x0200000B RID: 11
	public abstract class RpcClientHelper
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x0000124C File Offset: 0x0000064C
		public static object Invoke(RpcClientHelper.InvokeRpc invokeRpc)
		{
			for (int i = 0; i < 3; i++)
			{
				try
				{
					return invokeRpc();
				}
				catch (RpcException)
				{
					if (i == 2)
					{
						throw;
					}
				}
			}
			return null;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00001298 File Offset: 0x00000698
		public RpcClientHelper()
		{
		}

		// Token: 0x04000814 RID: 2068
		private const int RetryCount = 3;

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000596 RID: 1430
		public delegate object InvokeRpc();
	}
}
