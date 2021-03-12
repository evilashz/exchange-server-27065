using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000033 RID: 51
	public class ExExceptionTooManyRecipients : MapiException
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00005A7D File Offset: 0x00003C7D
		public ExExceptionTooManyRecipients(LID lid, string message) : base(lid, message, ErrorCodeValue.TooManyRecips)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005A8C File Offset: 0x00003C8C
		public ExExceptionTooManyRecipients(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.TooManyRecips, innerException)
		{
		}
	}
}
