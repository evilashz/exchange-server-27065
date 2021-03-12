using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ServerUnavailableException : RpcServiceException
	{
		// Token: 0x0600029D RID: 669 RVA: 0x000091C7 File Offset: 0x000073C7
		internal ServerUnavailableException(string message, Exception innerException) : base(message, 1722, innerException)
		{
		}
	}
}
