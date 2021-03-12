using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200012A RID: 298
	internal interface IFxProxy : IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000A48 RID: 2632
		void Flush();
	}
}
