using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000122 RID: 290
	internal interface IMessageProxy : IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000A15 RID: 2581
		void SaveChanges();

		// Token: 0x06000A16 RID: 2582
		void WriteToMime(byte[] buffer);
	}
}
