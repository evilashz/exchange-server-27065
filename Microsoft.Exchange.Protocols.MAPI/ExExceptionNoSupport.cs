using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000027 RID: 39
	public class ExExceptionNoSupport : MapiException
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00005909 File Offset: 0x00003B09
		public ExExceptionNoSupport(LID lid, string message) : base(lid, message, ErrorCodeValue.NotSupported)
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005918 File Offset: 0x00003B18
		public ExExceptionNoSupport(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.NotSupported, innerException)
		{
		}
	}
}
