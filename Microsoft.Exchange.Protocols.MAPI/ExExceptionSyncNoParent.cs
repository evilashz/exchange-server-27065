using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200002F RID: 47
	public class ExExceptionSyncNoParent : MapiException
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00005A01 File Offset: 0x00003C01
		public ExExceptionSyncNoParent(LID lid, string message) : base(lid, message, ErrorCodeValue.SyncNoParent)
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005A10 File Offset: 0x00003C10
		public ExExceptionSyncNoParent(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.SyncNoParent, innerException)
		{
		}
	}
}
