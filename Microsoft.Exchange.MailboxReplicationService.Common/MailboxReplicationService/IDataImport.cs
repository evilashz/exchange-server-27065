using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C6 RID: 198
	internal interface IDataImport : IDisposable
	{
		// Token: 0x060007B6 RID: 1974
		IDataMessage SendMessageAndWaitForReply(IDataMessage message);

		// Token: 0x060007B7 RID: 1975
		void SendMessage(IDataMessage message);
	}
}
