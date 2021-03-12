using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200026B RID: 619
	internal class RecipientInfoCacheSyncProviderFactory : ISyncProviderFactory
	{
		// Token: 0x06001706 RID: 5894 RVA: 0x0008A244 File Offset: 0x00088444
		public RecipientInfoCacheSyncProviderFactory(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
			this.LastModifiedTime = ExDateTime.MinValue;
			try
			{
				using (UserConfiguration mailboxConfiguration = mailboxSession.UserConfigurationManager.GetMailboxConfiguration("OWA.AutocompleteCache", UserConfigurationTypes.XML))
				{
					this.LastModifiedTime = mailboxConfiguration.LastModifiedTime;
					this.NativeStoreObjectId = mailboxConfiguration.Id;
				}
			}
			catch (ObjectNotFoundException)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Failed to find the cache and set the last modified time!");
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0008A2DC File Offset: 0x000884DC
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0008A2E4 File Offset: 0x000884E4
		public int MaxEntries
		{
			get
			{
				return this.maxEntries;
			}
			set
			{
				this.maxEntries = value;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0008A2ED File Offset: 0x000884ED
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x0008A2F5 File Offset: 0x000884F5
		public ExDateTime LastModifiedTime { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0008A2FE File Offset: 0x000884FE
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x0008A306 File Offset: 0x00088506
		public StoreObjectId NativeStoreObjectId { get; private set; }

		// Token: 0x0600170D RID: 5901 RVA: 0x0008A30F File Offset: 0x0008850F
		public ISyncProvider CreateSyncProvider(ISyncLogger syncLogger = null)
		{
			return new RecipientInfoCacheSyncProvider(this.mailboxSession, this.maxEntries);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0008A324 File Offset: 0x00088524
		public byte[] GetCollectionIdBytes()
		{
			if (this.folderId == null)
			{
				using (RecipientInfoCacheSyncProvider recipientInfoCacheSyncProvider = (RecipientInfoCacheSyncProvider)this.CreateSyncProvider(null))
				{
					this.folderId = recipientInfoCacheSyncProvider.ItemId;
				}
			}
			if (this.folderId == null)
			{
				return null;
			}
			return this.folderId.GetBytes();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0008A384 File Offset: 0x00088584
		public void SetCollectionIdFromBytes(byte[] collectionBytes)
		{
			this.folderId = StoreObjectId.Deserialize(collectionBytes);
		}

		// Token: 0x04000E23 RID: 3619
		private MailboxSession mailboxSession;

		// Token: 0x04000E24 RID: 3620
		private int maxEntries = int.MaxValue;

		// Token: 0x04000E25 RID: 3621
		private StoreObjectId folderId;

		// Token: 0x0200026C RID: 620
		private enum PropertiesToFetchEnum
		{
			// Token: 0x04000E29 RID: 3625
			LastModifiedTime,
			// Token: 0x04000E2A RID: 3626
			ItemClass
		}
	}
}
