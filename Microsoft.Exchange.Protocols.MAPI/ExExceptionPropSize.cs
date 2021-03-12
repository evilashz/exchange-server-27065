using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000036 RID: 54
	public class ExExceptionPropSize : MapiException
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005ADA File Offset: 0x00003CDA
		public ExExceptionPropSize(LID lid, string message) : base(lid, message, ErrorCodeValue.NotEnoughMemory)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public ExExceptionPropSize(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.NotEnoughMemory, innerException)
		{
		}
	}
}
