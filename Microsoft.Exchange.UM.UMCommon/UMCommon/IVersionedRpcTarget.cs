using System;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000034 RID: 52
	internal interface IVersionedRpcTarget : IRpcTarget
	{
		// Token: 0x060002B7 RID: 695
		UMRpcResponse ExecuteRequest(UMVersionedRpcRequest request);
	}
}
