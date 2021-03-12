using System;

namespace Microsoft.Exchange.Rpc.PoolRpc
{
	// Token: 0x0200037D RID: 893
	public interface IPoolConnectCompletion : IRpcAsyncCompletion
	{
		// Token: 0x06000FF4 RID: 4084
		void CompleteAsyncCall(IntPtr contextHandle, uint flags, uint maxPoolSize, Guid poolGuid, ArraySegment<byte> auxiliaryOut);
	}
}
