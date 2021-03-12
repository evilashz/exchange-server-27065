using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x02000380 RID: 896
	public interface IPoolSessionDoRpcCompletion : IRpcAsyncCompletion
	{
		// Token: 0x06000FF7 RID: 4087
		void CompleteAsyncCall(uint flags, ArraySegment<byte> response, ArraySegment<byte> auxiliaryOut);
	}
}
