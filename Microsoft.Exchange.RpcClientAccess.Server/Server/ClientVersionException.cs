using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ClientVersionException : RpcServerException
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004C7D File Offset: 0x00002E7D
		internal ClientVersionException(string message) : base(message, RpcErrorCode.ClientVerDisallowed)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004C8B File Offset: 0x00002E8B
		internal ClientVersionException(string message, Exception innerException) : base(message, RpcErrorCode.ClientVerDisallowed, innerException)
		{
		}
	}
}
