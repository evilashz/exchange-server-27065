using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200002D RID: 45
	public class ExExceptionSyncConflict : MapiException
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000059C3 File Offset: 0x00003BC3
		public ExExceptionSyncConflict(LID lid, string message) : base(lid, message, ErrorCodeValue.SyncConflict)
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000059D2 File Offset: 0x00003BD2
		public ExExceptionSyncConflict(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.SyncConflict, innerException)
		{
		}
	}
}
