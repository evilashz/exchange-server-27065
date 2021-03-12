using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000266 RID: 614
	internal class FirstTimeSyncProviderFactory : MailboxSyncProviderFactory
	{
		// Token: 0x060016BC RID: 5820 RVA: 0x000895EA File Offset: 0x000877EA
		public FirstTimeSyncProviderFactory(StoreSession storeSession) : base(storeSession)
		{
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000895F3 File Offset: 0x000877F3
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x000895FB File Offset: 0x000877FB
		public bool UseNewProvider { get; set; }

		// Token: 0x060016BF RID: 5823 RVA: 0x00089604 File Offset: 0x00087804
		public override ISyncProvider CreateSyncProvider(ISyncLogger syncLogger = null)
		{
			Folder folder;
			if (this.folder != null)
			{
				folder = this.folder;
				this.folder = null;
			}
			else
			{
				folder = Folder.Bind(this.storeSession, this.folderId);
			}
			if (this.UseNewProvider)
			{
				return new FirstTimeSyncProvider(folder, this.trackReadFlagChanges, this.trackAssociatedMessageChanges, this.trackConversations, this.allowTableRestrict);
			}
			return new MailboxSyncProvider(folder, this.trackReadFlagChanges, this.trackAssociatedMessageChanges, false, this.trackConversations, this.allowTableRestrict, AirSyncDiagnostics.GetSyncLogger());
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00089688 File Offset: 0x00087888
		public override ISyncProvider CreateSyncProvider(Folder folder, ISyncLogger syncLogger = null)
		{
			if (this.UseNewProvider)
			{
				return new FirstTimeSyncProvider(folder, this.trackReadFlagChanges, this.trackAssociatedMessageChanges, this.trackConversations, this.allowTableRestrict, false);
			}
			return new MailboxSyncProvider(folder, this.trackReadFlagChanges, this.trackAssociatedMessageChanges, false, this.trackConversations, this.allowTableRestrict, false, AirSyncDiagnostics.GetSyncLogger());
		}
	}
}
