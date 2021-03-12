using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public abstract class FolderId : MapiObjectId
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000028AD File Offset: 0x00000AAD
		public string LegacyDistinguishedName
		{
			get
			{
				return this.legacyDistinguishedName;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000028B5 File Offset: 0x00000AB5
		public MessageStoreId MessageStoreId
		{
			get
			{
				return this.messageStoreId;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000028BD File Offset: 0x00000ABD
		public MapiFolderPath MapiFolderPath
		{
			get
			{
				return this.mapiFolderPath;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028C5 File Offset: 0x00000AC5
		public FolderId()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028CD File Offset: 0x00000ACD
		public FolderId(byte[] bytes) : base(bytes)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028D6 File Offset: 0x00000AD6
		public FolderId(MessageStoreId storeId, string legacyDn)
		{
			this.messageStoreId = storeId;
			this.legacyDistinguishedName = legacyDn;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028EC File Offset: 0x00000AEC
		public FolderId(MessageStoreId storeId, MapiEntryId entryId) : base(entryId)
		{
			this.messageStoreId = storeId;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028FC File Offset: 0x00000AFC
		public FolderId(MessageStoreId storeId, MapiFolderPath folderPath)
		{
			this.messageStoreId = storeId;
			this.mapiFolderPath = folderPath;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002912 File Offset: 0x00000B12
		internal FolderId(MessageStoreId storeId, MapiEntryId entryId, MapiFolderPath folderPath, string legacyDn) : base(entryId)
		{
			this.messageStoreId = storeId;
			this.mapiFolderPath = folderPath;
			this.legacyDistinguishedName = legacyDn;
		}

		// Token: 0x0400000C RID: 12
		private readonly string legacyDistinguishedName;

		// Token: 0x0400000D RID: 13
		private readonly MessageStoreId messageStoreId;

		// Token: 0x0400000E RID: 14
		private readonly MapiFolderPath mapiFolderPath;
	}
}
