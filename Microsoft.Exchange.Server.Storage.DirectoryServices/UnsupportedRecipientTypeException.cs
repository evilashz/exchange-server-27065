using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x0200000E RID: 14
	public class UnsupportedRecipientTypeException : StoreException
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000023A7 File Offset: 0x000005A7
		public UnsupportedRecipientTypeException(LID lid, string message) : base(lid, ErrorCodeValue.ADPropertyError, message)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000023B6 File Offset: 0x000005B6
		public UnsupportedRecipientTypeException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.ADPropertyError, message, innerException)
		{
		}
	}
}
