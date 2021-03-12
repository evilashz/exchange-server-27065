using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x02000381 RID: 897
	public interface IPoolWaitForNotificationsCompletion : IRpcAsyncCompletion
	{
		// Token: 0x06000FF8 RID: 4088
		void CompleteAsyncCall(uint[] sessionHandles);
	}
}
