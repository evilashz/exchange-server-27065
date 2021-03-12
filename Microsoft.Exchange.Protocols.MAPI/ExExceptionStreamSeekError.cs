using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000039 RID: 57
	public class ExExceptionStreamSeekError : MapiException
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00005B37 File Offset: 0x00003D37
		public ExExceptionStreamSeekError(LID lid, string message) : base(lid, message, ErrorCodeValue.StreamSeekError)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005B46 File Offset: 0x00003D46
		public ExExceptionStreamSeekError(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.StreamSeekError, innerException)
		{
		}
	}
}
