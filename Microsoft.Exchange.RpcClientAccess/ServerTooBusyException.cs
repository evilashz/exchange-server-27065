using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ServerTooBusyException : RpcServiceException
	{
		// Token: 0x0600029C RID: 668 RVA: 0x000091B8 File Offset: 0x000073B8
		internal ServerTooBusyException(string message, Exception innerException) : base(message, 1723, innerException)
		{
		}
	}
}
