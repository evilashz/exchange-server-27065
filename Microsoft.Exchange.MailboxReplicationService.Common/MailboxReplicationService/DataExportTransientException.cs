using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000106 RID: 262
	internal class DataExportTransientException : MapiFxProxyRetryableException
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x00012A16 File Offset: 0x00010C16
		public DataExportTransientException(Exception inner) : base(inner)
		{
		}
	}
}
