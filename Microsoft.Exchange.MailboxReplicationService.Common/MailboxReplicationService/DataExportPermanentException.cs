using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000105 RID: 261
	internal class DataExportPermanentException : MapiFxProxyPermanentException
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x00012A0D File Offset: 0x00010C0D
		public DataExportPermanentException(Exception inner) : base(inner)
		{
		}
	}
}
