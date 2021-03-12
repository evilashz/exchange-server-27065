using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RpcExecutionException : RpcServerException
	{
		// Token: 0x06000291 RID: 657 RVA: 0x00009136 File Offset: 0x00007336
		protected RpcExecutionException(string message, RpcErrorCode storeError) : base(message, storeError)
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009140 File Offset: 0x00007340
		protected RpcExecutionException(string message, RpcErrorCode storeError, Exception innerException) : base(message, storeError, innerException)
		{
		}
	}
}
