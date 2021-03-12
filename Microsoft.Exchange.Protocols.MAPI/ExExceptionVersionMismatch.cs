using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200002B RID: 43
	public class ExExceptionVersionMismatch : MapiException
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00005985 File Offset: 0x00003B85
		public ExExceptionVersionMismatch(LID lid, string message) : base(lid, message, ErrorCodeValue.VersionMismatch)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005994 File Offset: 0x00003B94
		public ExExceptionVersionMismatch(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.VersionMismatch, innerException)
		{
		}
	}
}
