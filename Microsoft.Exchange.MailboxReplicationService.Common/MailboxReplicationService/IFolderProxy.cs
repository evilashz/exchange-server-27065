using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000120 RID: 288
	internal interface IFolderProxy : IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000A0A RID: 2570
		IMessageProxy OpenMessage(byte[] entryId);

		// Token: 0x06000A0B RID: 2571
		IMessageProxy CreateMessage(bool isAssociated);

		// Token: 0x06000A0C RID: 2572
		void DeleteMessage(byte[] entryId);
	}
}
