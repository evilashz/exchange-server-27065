using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200001D RID: 29
	public class ExExceptionMapiRpcFormat : MapiException
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000057D3 File Offset: 0x000039D3
		public ExExceptionMapiRpcFormat(LID lid, string message) : base(lid, message, ErrorCodeValue.RpcFormat)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000057E2 File Offset: 0x000039E2
		public ExExceptionMapiRpcFormat(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.RpcFormat, innerException)
		{
		}
	}
}
