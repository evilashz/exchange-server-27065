using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000BD RID: 189
	internal sealed class IdConverterWithCommandSettings
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		internal IdConverterWithCommandSettings(ToServiceObjectCommandSettingsBase commandSettings, CallContext callContext)
		{
			this.commandSettings = commandSettings;
			this.callContext = callContext;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001C014 File Offset: 0x0001A214
		internal ConcatenatedIdAndChangeKey GetConcatenatedId(StoreObjectId storeobjectId)
		{
			StoreSession storeSession = this.GetStoreSession();
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession != null)
			{
				MailboxId mailboxId = new MailboxId(mailboxSession);
				return IdConverter.GetConcatenatedId(storeobjectId, mailboxId, null);
			}
			if (!(storeSession is PublicFolderSession))
			{
				throw new NotImplementedException();
			}
			if (IdConverter.IsFolderObjectType(storeobjectId.ObjectType))
			{
				return IdConverter.GetConcatenatedIdForPublicFolder(storeobjectId);
			}
			StoreObjectId parentId = this.getParentId();
			return IdConverter.GetConcatenatedIdForPublicFolderItem(storeobjectId, parentId, null);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001C074 File Offset: 0x0001A274
		internal ItemId PersonaIdFromStoreId(StoreId storeId)
		{
			StoreSession storeSession = this.GetStoreSession();
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession != null)
			{
				MailboxId mailboxId = new MailboxId(mailboxSession);
				return IdConverter.PersonaIdFromStoreId(storeId, mailboxId);
			}
			if (storeSession is PublicFolderSession)
			{
				StoreObjectId parentId = this.getParentId();
				return IdConverter.PersonaIdFromPublicFolderItemId(storeId, parentId);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		private StoreSession GetStoreSession()
		{
			MailboxSession mailboxSession = null;
			if (this.commandSettings.IdAndSession != null)
			{
				mailboxSession = (this.commandSettings.IdAndSession.Session as MailboxSession);
			}
			if (mailboxSession == null && this.callContext != null)
			{
				mailboxSession = this.callContext.SessionCache.GetMailboxIdentityMailboxSession();
			}
			if (mailboxSession != null)
			{
				return mailboxSession;
			}
			return this.commandSettings.IdAndSession.Session;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001C123 File Offset: 0x0001A323
		private StoreObjectId getParentId()
		{
			return StoreId.GetStoreObjectId(this.commandSettings.IdAndSession.ParentFolderId);
		}

		// Token: 0x04000673 RID: 1651
		private readonly ToServiceObjectCommandSettingsBase commandSettings;

		// Token: 0x04000674 RID: 1652
		private readonly CallContext callContext;
	}
}
