using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200002E RID: 46
	public class ExExceptionSyncIgnore : MapiException
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000059E2 File Offset: 0x00003BE2
		public ExExceptionSyncIgnore(LID lid, string message) : base(lid, message, ErrorCodeValue.SyncIgnore)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000059F1 File Offset: 0x00003BF1
		public ExExceptionSyncIgnore(LID lid, string message, Exception innerException) : base(lid, message, ErrorCodeValue.SyncIgnore, innerException)
		{
		}
	}
}
