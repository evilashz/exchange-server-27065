using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200002A RID: 42
	public class ExExceptionLogonFailed : MapiException
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00005966 File Offset: 0x00003B66
		public ExExceptionLogonFailed(LID lid, string message) : base(lid, message, ErrorCodeValue.LogonFailed)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005975 File Offset: 0x00003B75
		public ExExceptionLogonFailed(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.LogonFailed, innerException)
		{
		}
	}
}
