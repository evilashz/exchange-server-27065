using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200037E RID: 894
	public interface IPoolCreateSessionCompletion : IRpcAsyncCompletion
	{
		// Token: 0x06000FF5 RID: 4085
		void CompleteAsyncCall(uint sessionHandle, string displayName, uint maximumPolls, uint retryCount, uint retryDelay, ushort sessionId, ArraySegment<byte> auxiliaryOut);
	}
}
