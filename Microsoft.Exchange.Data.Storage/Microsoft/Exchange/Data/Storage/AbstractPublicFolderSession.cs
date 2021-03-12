using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002DC RID: 732
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractPublicFolderSession : AbstractStoreSession, IPublicFolderSession, IStoreSession, IDisposable
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x00086527 File Offset: 0x00084727
		public virtual bool IsPrimaryHierarchySession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0008652E File Offset: 0x0008472E
		public virtual IExchangePrincipal MailboxPrincipal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00086535 File Offset: 0x00084735
		public virtual StoreObjectId GetPublicFolderRootId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0008653C File Offset: 0x0008473C
		public virtual StoreObjectId GetTombstonesRootFolderId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00086543 File Offset: 0x00084743
		public virtual StoreObjectId GetAsyncDeleteStateFolderId()
		{
			throw new NotImplementedException();
		}
	}
}
