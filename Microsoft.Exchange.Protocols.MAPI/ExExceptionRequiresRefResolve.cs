using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200003B RID: 59
	public class ExExceptionRequiresRefResolve : MapiException
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00005B75 File Offset: 0x00003D75
		public ExExceptionRequiresRefResolve(LID lid, string message) : base(lid, message, ErrorCodeValue.RequiresRefResolve)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005B84 File Offset: 0x00003D84
		public ExExceptionRequiresRefResolve(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.RequiresRefResolve, innerException)
		{
		}
	}
}
