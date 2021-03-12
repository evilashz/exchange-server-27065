using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x0200027B RID: 635
	internal class MailboxUtilityQueryResult
	{
		// Token: 0x0600177A RID: 6010 RVA: 0x0008B999 File Offset: 0x00089B99
		internal MailboxUtilityQueryResult(string displayName, StoreObjectId storeId, StoreObjectId parentStoreId, ExDateTime lastModifiedTime)
		{
			this.displayName = displayName;
			this.storeId = storeId;
			this.parentStoreId = parentStoreId;
			this.lastModifiedTime = lastModifiedTime;
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0008B9BE File Offset: 0x00089BBE
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x0008B9C6 File Offset: 0x00089BC6
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x0008B9CF File Offset: 0x00089BCF
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x0008B9D7 File Offset: 0x00089BD7
		internal StoreObjectId StoreId
		{
			get
			{
				return this.storeId;
			}
			set
			{
				this.storeId = value;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0008B9E0 File Offset: 0x00089BE0
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x0008B9E8 File Offset: 0x00089BE8
		internal StoreObjectId ParentStoreId
		{
			get
			{
				return this.parentStoreId;
			}
			set
			{
				this.parentStoreId = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0008B9F1 File Offset: 0x00089BF1
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x0008B9F9 File Offset: 0x00089BF9
		internal ExDateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
			set
			{
				this.lastModifiedTime = value;
			}
		}

		// Token: 0x04000E5E RID: 3678
		private string displayName;

		// Token: 0x04000E5F RID: 3679
		private StoreObjectId storeId;

		// Token: 0x04000E60 RID: 3680
		private StoreObjectId parentStoreId;

		// Token: 0x04000E61 RID: 3681
		private ExDateTime lastModifiedTime;
	}
}
