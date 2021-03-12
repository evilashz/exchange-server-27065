using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class InvalidParameterException : RpcServerException
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004C6F File Offset: 0x00002E6F
		internal InvalidParameterException(string message) : base(message, RpcErrorCode.InvalidParam)
		{
		}
	}
}
