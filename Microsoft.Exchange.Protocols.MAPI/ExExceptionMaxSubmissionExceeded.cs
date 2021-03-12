using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200003C RID: 60
	public class ExExceptionMaxSubmissionExceeded : MapiException
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00005B94 File Offset: 0x00003D94
		public ExExceptionMaxSubmissionExceeded(LID lid, string message) : base(lid, message, ErrorCodeValue.MaxSubmissionExceeded)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005BA3 File Offset: 0x00003DA3
		public ExExceptionMaxSubmissionExceeded(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.MaxSubmissionExceeded, innerException)
		{
		}
	}
}
