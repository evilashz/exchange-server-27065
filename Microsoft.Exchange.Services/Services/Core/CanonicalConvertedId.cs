using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000B7 RID: 183
	internal class CanonicalConvertedId
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x0001B791 File Offset: 0x00019991
		private CanonicalConvertedId(IdStorageType objectType, StoreObjectId storeObjectId, StoreObjectId folderId, string primarySmtpAddress, bool isArchive)
		{
			this.objectType = objectType;
			this.storeObjectId = storeObjectId;
			this.folderId = folderId;
			this.primarySmtpAddress = primarySmtpAddress;
			this.isArchive = isArchive;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001B7BE File Offset: 0x000199BE
		internal static CanonicalConvertedId CreateFromPublicFolderId(StoreObjectId folderId)
		{
			return new CanonicalConvertedId(IdStorageType.PublicFolder, folderId, null, null, false);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001B7CA File Offset: 0x000199CA
		internal static CanonicalConvertedId CreateFromPublicFolderItemId(StoreObjectId itemId, StoreObjectId folderId)
		{
			return new CanonicalConvertedId(IdStorageType.PublicFolderItem, itemId, folderId, null, false);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001B7D6 File Offset: 0x000199D6
		internal static CanonicalConvertedId CreateFromMailboxStoreId(StoreObjectId storeId, string primarySmtpAddress, bool isArchive)
		{
			return new CanonicalConvertedId(IdStorageType.MailboxItemSmtpAddressBased, storeId, null, primarySmtpAddress, isArchive);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001B7E2 File Offset: 0x000199E2
		internal IdStorageType ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001B7EA File Offset: 0x000199EA
		internal StoreObjectId StoreObjectId
		{
			get
			{
				return this.storeObjectId;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001B7F2 File Offset: 0x000199F2
		internal StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001B7FA File Offset: 0x000199FA
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x0001B802 File Offset: 0x00019A02
		internal string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddress;
			}
			set
			{
				this.primarySmtpAddress = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001B80B File Offset: 0x00019A0B
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0001B813 File Offset: 0x00019A13
		internal bool IsArchive
		{
			get
			{
				return this.isArchive;
			}
			set
			{
				this.isArchive = value;
			}
		}

		// Token: 0x0400066E RID: 1646
		private IdStorageType objectType;

		// Token: 0x0400066F RID: 1647
		private StoreObjectId storeObjectId;

		// Token: 0x04000670 RID: 1648
		private StoreObjectId folderId;

		// Token: 0x04000671 RID: 1649
		private string primarySmtpAddress;

		// Token: 0x04000672 RID: 1650
		private bool isArchive;
	}
}
