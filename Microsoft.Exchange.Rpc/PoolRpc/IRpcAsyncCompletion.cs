using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200037C RID: 892
	public interface IRpcAsyncCompletion
	{
		// Token: 0x06000FF2 RID: 4082
		void AbortAsyncCall(uint exceptionCode);

		// Token: 0x06000FF3 RID: 4083
		void FailAsyncCall(int errorCode, ArraySegment<byte> auxiliaryOut);
	}
}
