using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004D7 RID: 1239
	[OwaEventObjectId(typeof(ADObjectId))]
	[OwaEventNamespace("RB")]
	internal sealed class RoomBrowserEventHandler : DirectoryVirtualListViewEventHandler
	{
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x00110680 File Offset: 0x0010E880
		protected override ViewType ViewType
		{
			get
			{
				return ViewType.RoomBrowser;
			}
		}

		// Token: 0x04002121 RID: 8481
		public const string EventNamespace = "RB";
	}
}
