using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000076 RID: 118
	public class CannotPreserveMailboxSignature : StoreException
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x0001CEE5 File Offset: 0x0001B0E5
		public CannotPreserveMailboxSignature(LID lid, string message) : base(lid, ErrorCodeValue.CannotPreserveMailboxSignature, message)
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		public CannotPreserveMailboxSignature(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.CannotPreserveMailboxSignature, message, innerException)
		{
		}
	}
}
