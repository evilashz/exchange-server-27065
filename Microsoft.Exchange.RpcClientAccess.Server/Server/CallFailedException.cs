using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CallFailedException : RpcServerException
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004BED File Offset: 0x00002DED
		internal CallFailedException(string message) : base(message, RpcErrorCode.Error)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004BFB File Offset: 0x00002DFB
		internal CallFailedException(string message, Exception innerException) : base(message, RpcErrorCode.Error, innerException)
		{
		}
	}
}
