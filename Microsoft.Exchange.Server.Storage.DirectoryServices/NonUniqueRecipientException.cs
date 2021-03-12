using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x0200000D RID: 13
	public class NonUniqueRecipientException : StoreException
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002388 File Offset: 0x00000588
		public NonUniqueRecipientException(LID lid, string message) : base(lid, ErrorCodeValue.ADPropertyError, message)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002397 File Offset: 0x00000597
		public NonUniqueRecipientException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.ADPropertyError, message, innerException)
		{
		}
	}
}
