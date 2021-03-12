using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200008A RID: 138
	public class StoreOperationCanceledException : StoreException
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00014700 File Offset: 0x00012900
		public StoreOperationCanceledException(LID lid, string message) : base(lid, ErrorCodeValue.Cancel, message)
		{
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001470F File Offset: 0x0001290F
		public StoreOperationCanceledException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.Cancel, message, innerException)
		{
		}
	}
}
