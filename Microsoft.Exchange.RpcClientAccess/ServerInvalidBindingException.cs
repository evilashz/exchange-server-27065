using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ServerInvalidBindingException : RpcServiceException
	{
		// Token: 0x0600029B RID: 667 RVA: 0x000091A9 File Offset: 0x000073A9
		internal ServerInvalidBindingException(string message, Exception innerException) : base(message, 1702, innerException)
		{
		}
	}
}
