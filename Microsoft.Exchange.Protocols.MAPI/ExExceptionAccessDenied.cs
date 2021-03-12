using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200001F RID: 31
	public class ExExceptionAccessDenied : MapiException
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005811 File Offset: 0x00003A11
		public ExExceptionAccessDenied(LID lid, string message) : base(lid, message, ErrorCodeValue.NoAccess)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005820 File Offset: 0x00003A20
		public ExExceptionAccessDenied(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.NoAccess, innerException)
		{
		}
	}
}
