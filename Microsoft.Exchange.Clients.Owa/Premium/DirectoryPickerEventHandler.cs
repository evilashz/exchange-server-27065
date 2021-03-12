using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000495 RID: 1173
	[OwaEventObjectId(typeof(ADObjectId))]
	[OwaEventNamespace("DP")]
	internal sealed class DirectoryPickerEventHandler : DirectoryVirtualListViewEventHandler
	{
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000FE6B5 File Offset: 0x000FC8B5
		protected override ViewType ViewType
		{
			get
			{
				return ViewType.DirectoryPicker;
			}
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000FE6B8 File Offset: 0x000FC8B8
		protected override void PersistMultiLineState()
		{
			AddressBookViewState.PersistMultiLineState(base.UserContext, this.ListViewState.IsMultiLine, true);
		}

		// Token: 0x04001DF2 RID: 7666
		public const string EventNamespace = "DP";
	}
}
