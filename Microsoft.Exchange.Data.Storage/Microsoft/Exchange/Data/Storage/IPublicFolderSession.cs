using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPublicFolderSession : IStoreSession, IDisposable
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000288 RID: 648
		bool IsPrimaryHierarchySession { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000289 RID: 649
		IExchangePrincipal MailboxPrincipal { get; }

		// Token: 0x0600028A RID: 650
		StoreObjectId GetPublicFolderRootId();

		// Token: 0x0600028B RID: 651
		StoreObjectId GetTombstonesRootFolderId();

		// Token: 0x0600028C RID: 652
		StoreObjectId GetAsyncDeleteStateFolderId();
	}
}
