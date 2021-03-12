using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000024 RID: 36
	public class ExExceptionInvalidObject : MapiException
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x000058AC File Offset: 0x00003AAC
		public ExExceptionInvalidObject(LID lid, string message) : base(lid, message, ErrorCodeValue.InvalidObject)
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000058BB File Offset: 0x00003ABB
		public ExExceptionInvalidObject(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.InvalidObject, innerException)
		{
		}
	}
}
