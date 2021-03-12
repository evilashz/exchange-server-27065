using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000032 RID: 50
	public class ExExceptionSendAsDenied : MapiException
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00005A5E File Offset: 0x00003C5E
		public ExExceptionSendAsDenied(LID lid, string message) : base(lid, message, ErrorCodeValue.SendAsDenied)
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005A6D File Offset: 0x00003C6D
		public ExExceptionSendAsDenied(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.SendAsDenied, innerException)
		{
		}
	}
}
