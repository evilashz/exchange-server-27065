using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LoginFailureException : RpcServerException
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00004C52 File Offset: 0x00002E52
		internal LoginFailureException(string message) : base(message, RpcErrorCode.LoginFailure)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004C60 File Offset: 0x00002E60
		internal LoginFailureException(string message, Exception innerException) : base(message, RpcErrorCode.LoginFailure, innerException)
		{
		}
	}
}
