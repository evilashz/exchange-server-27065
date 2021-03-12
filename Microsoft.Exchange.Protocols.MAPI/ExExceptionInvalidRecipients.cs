using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000034 RID: 52
	public class ExExceptionInvalidRecipients : MapiException
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00005A9C File Offset: 0x00003C9C
		public ExExceptionInvalidRecipients(LID lid, string message) : base(lid, message, ErrorCodeValue.InvalidRecipients)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005AAB File Offset: 0x00003CAB
		public ExExceptionInvalidRecipients(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.InvalidRecipients, innerException)
		{
		}
	}
}
