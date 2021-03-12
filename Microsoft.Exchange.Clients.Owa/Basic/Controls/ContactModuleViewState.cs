using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000028 RID: 40
	internal class ContactModuleViewState : ListViewViewState
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00009474 File Offset: 0x00007674
		public ContactModuleViewState(StoreObjectId folderId, string folderType, int pageNumber) : base(NavigationModule.Contacts, folderId, folderType, pageNumber)
		{
		}
	}
}
