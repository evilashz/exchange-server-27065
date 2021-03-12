using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class UnknownUserException : RpcServerException
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00004C27 File Offset: 0x00002E27
		internal UnknownUserException(string message) : base(message, RpcErrorCode.UnknownUser)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004C35 File Offset: 0x00002E35
		internal UnknownUserException(string message, Exception innerException) : base(message, RpcErrorCode.UnknownUser, innerException)
		{
		}
	}
}
