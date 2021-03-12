using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000013 RID: 19
	// (Invoke) Token: 0x06000096 RID: 150
	internal delegate void DoRpcCompleteCallback(ErrorCode result, uint flags, ArraySegment<byte> response, ArraySegment<byte> auxOut);
}
