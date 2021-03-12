using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200011F RID: 287
	internal interface IMapiFxProxyEx : IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000A08 RID: 2568
		void SetProps(PropValueData[] pvda);

		// Token: 0x06000A09 RID: 2569
		void SetItemProperties(ItemPropertiesBase props);
	}
}
