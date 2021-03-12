using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000026 RID: 38
	public class ExExceptionOutOfMemory : MapiException
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000058EA File Offset: 0x00003AEA
		public ExExceptionOutOfMemory(LID lid, string message) : base(lid, message, ErrorCodeValue.NotEnoughMemory)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000058F9 File Offset: 0x00003AF9
		public ExExceptionOutOfMemory(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.NotEnoughMemory, innerException)
		{
		}
	}
}
