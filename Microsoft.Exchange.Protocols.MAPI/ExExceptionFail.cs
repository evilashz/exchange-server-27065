using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000025 RID: 37
	public class ExExceptionFail : MapiException
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000058CB File Offset: 0x00003ACB
		public ExExceptionFail(LID lid, string message) : base(lid, message, ErrorCodeValue.CallFailed)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000058DA File Offset: 0x00003ADA
		public ExExceptionFail(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.CallFailed, innerException)
		{
		}
	}
}
