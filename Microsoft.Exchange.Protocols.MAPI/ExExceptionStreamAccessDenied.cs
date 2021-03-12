using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000020 RID: 32
	public class ExExceptionStreamAccessDenied : MapiException
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00005830 File Offset: 0x00003A30
		public ExExceptionStreamAccessDenied(LID lid, string message) : base(lid, message, ErrorCodeValue.StreamAccessDenied)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000583F File Offset: 0x00003A3F
		public ExExceptionStreamAccessDenied(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.StreamAccessDenied, innerException)
		{
		}
	}
}
