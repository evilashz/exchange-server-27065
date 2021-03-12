using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AccessDeniedException : RpcServerException
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00004C0A File Offset: 0x00002E0A
		internal AccessDeniedException(string message) : base(message, RpcErrorCode.AccessDenied)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004C18 File Offset: 0x00002E18
		internal AccessDeniedException(string message, Exception innerException) : base(message, RpcErrorCode.AccessDenied, innerException)
		{
		}
	}
}
