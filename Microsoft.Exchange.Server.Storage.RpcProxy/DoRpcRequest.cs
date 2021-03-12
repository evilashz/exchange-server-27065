using System;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x0200000F RID: 15
	internal struct DoRpcRequest
	{
		// Token: 0x04000038 RID: 56
		public uint Flags;

		// Token: 0x04000039 RID: 57
		public uint MaximumResponseSize;

		// Token: 0x0400003A RID: 58
		public ArraySegment<byte> Request;

		// Token: 0x0400003B RID: 59
		public ArraySegment<byte> AuxiliaryIn;

		// Token: 0x0400003C RID: 60
		public DoRpcCompleteCallback CompletionCallback;

		// Token: 0x0400003D RID: 61
		public Action<RpcException> ExceptionCallback;
	}
}
