using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200048D RID: 1165
	[OwaEventSegmentation(Feature.Contacts)]
	[OwaEventNamespace("CP")]
	[OwaEventObjectId(typeof(OwaStoreObjectId))]
	internal sealed class ContactPickerEventHandler : ContactVirtualListViewEventHandler
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000FD8C3 File Offset: 0x000FBAC3
		protected override ViewType ViewType
		{
			get
			{
				return ViewType.ContactPicker;
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000FD8C6 File Offset: 0x000FBAC6
		protected override void PersistMultiLineState()
		{
			AddressBookViewState.PersistMultiLineState(base.UserContext, this.ListViewState.IsMultiLine, true);
		}

		// Token: 0x04001DD6 RID: 7638
		public const string EventNamespace = "CP";
	}
}
