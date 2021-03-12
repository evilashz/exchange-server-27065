using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NotFoundException : RpcServerException
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004C9A File Offset: 0x00002E9A
		internal NotFoundException(string message) : base(message, RpcErrorCode.NotFound)
		{
		}
	}
}
