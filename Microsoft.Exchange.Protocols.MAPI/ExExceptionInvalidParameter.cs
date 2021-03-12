using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000022 RID: 34
	public class ExExceptionInvalidParameter : MapiException
	{
		// Token: 0x060000CE RID: 206 RVA: 0x0000586E File Offset: 0x00003A6E
		public ExExceptionInvalidParameter(LID lid, string message) : base(lid, message, ErrorCodeValue.InvalidParameter)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000587D File Offset: 0x00003A7D
		public ExExceptionInvalidParameter(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.InvalidParameter, innerException)
		{
		}
	}
}
