using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200002C RID: 44
	public class ExExceptionObjectChanged : MapiException
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x000059A4 File Offset: 0x00003BA4
		public ExExceptionObjectChanged(LID lid, string message) : base(lid, message, ErrorCodeValue.ObjectChanged)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000059B3 File Offset: 0x00003BB3
		public ExExceptionObjectChanged(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.ObjectChanged, innerException)
		{
		}
	}
}
