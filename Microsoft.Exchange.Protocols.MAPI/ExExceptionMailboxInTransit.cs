using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200003A RID: 58
	public class ExExceptionMailboxInTransit : MapiException
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00005B56 File Offset: 0x00003D56
		public ExExceptionMailboxInTransit(LID lid, string message) : base(lid, message, ErrorCodeValue.MailboxInTransit)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005B65 File Offset: 0x00003D65
		public ExExceptionMailboxInTransit(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.MailboxInTransit, innerException)
		{
		}
	}
}
