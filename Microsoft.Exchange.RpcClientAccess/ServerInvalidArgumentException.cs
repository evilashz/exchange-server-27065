using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004C RID: 76
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ServerInvalidArgumentException : RpcServiceException
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000919D File Offset: 0x0000739D
		internal ServerInvalidArgumentException(string message, Exception innerException) : base(message, 87, innerException)
		{
		}
	}
}
