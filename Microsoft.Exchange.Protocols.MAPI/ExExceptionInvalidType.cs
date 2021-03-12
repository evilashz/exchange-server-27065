using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000021 RID: 33
	public class ExExceptionInvalidType : MapiException
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000584F File Offset: 0x00003A4F
		public ExExceptionInvalidType(LID lid, string message) : base(lid, message, ErrorCodeValue.InvalidType)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000585E File Offset: 0x00003A5E
		public ExExceptionInvalidType(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.InvalidType, innerException)
		{
		}
	}
}
