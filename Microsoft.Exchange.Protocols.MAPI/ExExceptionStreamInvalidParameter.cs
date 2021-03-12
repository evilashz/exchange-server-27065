using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000023 RID: 35
	public class ExExceptionStreamInvalidParameter : MapiException
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x0000588D File Offset: 0x00003A8D
		public ExExceptionStreamInvalidParameter(LID lid, string message) : base(lid, message, ErrorCodeValue.StreamInvalidParam)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000589C File Offset: 0x00003A9C
		public ExExceptionStreamInvalidParameter(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.StreamInvalidParam, innerException)
		{
		}
	}
}
