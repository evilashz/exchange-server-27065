using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000037 RID: 55
	public class ExExceptionPropType : MapiException
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00005AF9 File Offset: 0x00003CF9
		public ExExceptionPropType(LID lid, string message) : base(lid, message, ErrorCodeValue.UnexpectedType)
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005B08 File Offset: 0x00003D08
		public ExExceptionPropType(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.UnexpectedType, innerException)
		{
		}
	}
}
