using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000BA RID: 186
	internal abstract class SimpleIdConverterBase : BaseAlternateIdConverter
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x0001BDAC File Offset: 0x00019FAC
		internal override CanonicalConvertedId Parse(AlternateId altId)
		{
			Util.ValidateSmtpAddress(altId.Mailbox);
			StoreObjectId storeId = this.ConvertStringToStoreObjectId(altId.Id);
			return CanonicalConvertedId.CreateFromMailboxStoreId(storeId, altId.Mailbox, altId.IsArchive);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		internal override CanonicalConvertedId Parse(AlternatePublicFolderId altId)
		{
			StoreObjectId folderId = this.ConvertStringToStoreObjectId(altId.FolderId);
			return CanonicalConvertedId.CreateFromPublicFolderId(folderId);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001BE04 File Offset: 0x0001A004
		internal override CanonicalConvertedId Parse(AlternatePublicFolderItemId altId)
		{
			StoreObjectId itemId = this.ConvertStringToStoreObjectId(altId.ItemId);
			StoreObjectId folderId = this.ConvertStringToStoreObjectId(altId.FolderId);
			return CanonicalConvertedId.CreateFromPublicFolderItemId(itemId, folderId);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001BE34 File Offset: 0x0001A034
		internal override AlternateIdBase Format(CanonicalConvertedId canonicalId)
		{
			switch (canonicalId.ObjectType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
				return new AlternateId(this.ConvertStoreObjectIdToString(canonicalId.StoreObjectId), canonicalId.PrimarySmtpAddress, this.IdFormat, canonicalId.IsArchive);
			case IdStorageType.PublicFolder:
				return new AlternatePublicFolderId(this.ConvertStoreObjectIdToString(canonicalId.StoreObjectId), this.IdFormat);
			case IdStorageType.PublicFolderItem:
				return new AlternatePublicFolderItemId(this.ConvertStoreObjectIdToString(canonicalId.StoreObjectId), this.ConvertStoreObjectIdToString(canonicalId.FolderId), this.IdFormat);
			default:
				return null;
			}
		}

		// Token: 0x06000520 RID: 1312
		internal abstract StoreObjectId ConvertStringToStoreObjectId(string idValue);
	}
}
