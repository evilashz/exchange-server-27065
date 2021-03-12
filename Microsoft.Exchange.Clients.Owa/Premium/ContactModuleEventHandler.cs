using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200048C RID: 1164
	[OwaEventNamespace("CM")]
	[OwaEventObjectId(typeof(OwaStoreObjectId))]
	[OwaEventSegmentation(Feature.Contacts)]
	internal sealed class ContactModuleEventHandler : ContactVirtualListViewEventHandler
	{
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000FD8B8 File Offset: 0x000FBAB8
		protected override ViewType ViewType
		{
			get
			{
				return ViewType.ContactModule;
			}
		}

		// Token: 0x04001DD5 RID: 7637
		public const string EventNamespace = "CM";
	}
}
