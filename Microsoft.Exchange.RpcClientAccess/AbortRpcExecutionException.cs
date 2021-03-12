using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AbortRpcExecutionException : RpcExecutionException
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000914B File Offset: 0x0000734B
		public AbortRpcExecutionException(string message) : base(message, RpcErrorCode.RpcFailed)
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009159 File Offset: 0x00007359
		public AbortRpcExecutionException(string message, Exception innerException) : base(message, RpcErrorCode.RpcFailed, innerException)
		{
		}
	}
}
