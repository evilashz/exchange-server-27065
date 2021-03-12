using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200048A RID: 1162
	[OwaEventSegmentation(Feature.Contacts)]
	[OwaEventNamespace("CB")]
	[OwaEventObjectId(typeof(OwaStoreObjectId))]
	internal sealed class ContactBrowserEventHandler : ContactVirtualListViewEventHandler
	{
		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000FD4D9 File Offset: 0x000FB6D9
		protected override ViewType ViewType
		{
			get
			{
				return ViewType.ContactBrowser;
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000FD4DC File Offset: 0x000FB6DC
		[OwaEvent("PersistReadingPane")]
		[OwaEventParameter("s", typeof(ReadingPanePosition))]
		public override void PersistReadingPane()
		{
			AddressBookViewState.PersistReadingPane(base.UserContext, (ReadingPanePosition)base.GetParameter("s"));
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000FD4F9 File Offset: 0x000FB6F9
		protected override void PersistMultiLineState()
		{
			AddressBookViewState.PersistMultiLineState(base.UserContext, this.ListViewState.IsMultiLine, false);
		}

		// Token: 0x04001DD3 RID: 7635
		public const string EventNamespace = "CB";
	}
}
