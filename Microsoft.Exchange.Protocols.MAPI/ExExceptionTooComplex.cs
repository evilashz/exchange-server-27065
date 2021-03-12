using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000028 RID: 40
	public class ExExceptionTooComplex : MapiException
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00005928 File Offset: 0x00003B28
		public ExExceptionTooComplex(LID lid, string message) : base(lid, message, ErrorCodeValue.TooComplex)
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005937 File Offset: 0x00003B37
		public ExExceptionTooComplex(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.TooComplex, innerException)
		{
		}
	}
}
