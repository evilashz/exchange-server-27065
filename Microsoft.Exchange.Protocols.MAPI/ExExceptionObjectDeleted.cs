using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000030 RID: 48
	public class ExExceptionObjectDeleted : MapiException
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00005A20 File Offset: 0x00003C20
		public ExExceptionObjectDeleted(LID lid, string message) : base(lid, message, ErrorCodeValue.ObjectDeleted)
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005A2F File Offset: 0x00003C2F
		public ExExceptionObjectDeleted(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.ObjectDeleted, innerException)
		{
		}
	}
}
