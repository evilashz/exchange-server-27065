using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200001C RID: 28
	public abstract class MapiException : StoreException
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000057BB File Offset: 0x000039BB
		public MapiException(LID lid, string message, ErrorCodeValue errorCode) : base(lid, errorCode, message)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000057C6 File Offset: 0x000039C6
		public MapiException(LID lid, string message, ErrorCodeValue errorCode, Exception innerException) : base(lid, errorCode, message, innerException)
		{
		}
	}
}
