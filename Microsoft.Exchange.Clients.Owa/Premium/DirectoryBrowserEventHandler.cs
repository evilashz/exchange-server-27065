using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000493 RID: 1171
	[OwaEventNamespace("DB")]
	[OwaEventObjectId(typeof(ADObjectId))]
	internal sealed class DirectoryBrowserEventHandler : DirectoryVirtualListViewEventHandler
	{
		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x000FE151 File Offset: 0x000FC351
		protected override ViewType ViewType
		{
			get
			{
				return ViewType.DirectoryBrowser;
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000FE154 File Offset: 0x000FC354
		protected override void PersistReadingPane(ReadingPanePosition readingPanePosition)
		{
			AddressBookViewState.PersistReadingPane(base.UserContext, readingPanePosition);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000FE162 File Offset: 0x000FC362
		protected override void PersistMultiLineState()
		{
			AddressBookViewState.PersistMultiLineState(base.UserContext, this.ListViewState.IsMultiLine, false);
		}

		// Token: 0x04001DE9 RID: 7657
		public const string EventNamespace = "DB";
	}
}
