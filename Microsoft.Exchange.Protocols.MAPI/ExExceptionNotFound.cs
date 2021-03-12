using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200001E RID: 30
	public class ExExceptionNotFound : MapiException
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000057F2 File Offset: 0x000039F2
		public ExExceptionNotFound(LID lid, string message) : base(lid, message, ErrorCodeValue.NotFound)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005801 File Offset: 0x00003A01
		public ExExceptionNotFound(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.NotFound, innerException)
		{
		}
	}
}
