using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200003D RID: 61
	public class ExExceptionMailboxQuarantined : MapiException
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00005BB3 File Offset: 0x00003DB3
		public ExExceptionMailboxQuarantined(LID lid, string message) : base(lid, message, ErrorCodeValue.MailboxQuarantined)
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005BC2 File Offset: 0x00003DC2
		public ExExceptionMailboxQuarantined(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.MailboxQuarantined, innerException)
		{
		}
	}
}
