using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200005E RID: 94
	public class InvalidRecipientsException : StoreException
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x00045A4B File Offset: 0x00043C4B
		public InvalidRecipientsException(LID lid, string message) : base(lid, ErrorCodeValue.InvalidRecipients, message)
		{
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00045A5A File Offset: 0x00043C5A
		public InvalidRecipientsException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.InvalidRecipients, message, innerException)
		{
		}
	}
}
