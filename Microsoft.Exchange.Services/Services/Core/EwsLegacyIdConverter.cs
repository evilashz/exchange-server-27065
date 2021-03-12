using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000B9 RID: 185
	internal class EwsLegacyIdConverter : BaseAlternateIdConverter
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001BB34 File Offset: 0x00019D34
		internal override IdFormat IdFormat
		{
			get
			{
				return IdFormat.EwsLegacyId;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001BB38 File Offset: 0x00019D38
		internal override CanonicalConvertedId Parse(AlternateId altId)
		{
			Util.ValidateSmtpAddress(altId.Mailbox);
			IdHeaderInformation idHeaderInformation = this.ConvertFromConcatenatedId(IdStorageType.MailboxItemSmtpAddressBased, altId.Id);
			StoreObjectId storeId = EwsLegacyIdConverter.ConvertIdHeaderToStoreObjectId(idHeaderInformation);
			return CanonicalConvertedId.CreateFromMailboxStoreId(storeId, altId.Mailbox, altId.IsArchive);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001BB78 File Offset: 0x00019D78
		internal override CanonicalConvertedId Parse(AlternatePublicFolderId altId)
		{
			IdHeaderInformation idHeaderInformation = this.ConvertFromConcatenatedId(IdStorageType.PublicFolder, altId.FolderId);
			StoreObjectId folderId = EwsLegacyIdConverter.ConvertBytesToStoreObjectId(idHeaderInformation.StoreIdBytes);
			return CanonicalConvertedId.CreateFromPublicFolderId(folderId);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		internal override CanonicalConvertedId Parse(AlternatePublicFolderItemId altId)
		{
			IdHeaderInformation idHeaderInformation = this.ConvertFromConcatenatedId(IdStorageType.PublicFolderItem, altId.ItemId);
			StoreObjectId itemId = EwsLegacyIdConverter.ConvertIdHeaderToStoreObjectId(idHeaderInformation);
			StoreObjectId folderId = EwsLegacyIdConverter.ConvertBytesToStoreObjectId(idHeaderInformation.FolderIdBytes);
			return CanonicalConvertedId.CreateFromPublicFolderItemId(itemId, folderId);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		internal override AlternateIdBase Format(CanonicalConvertedId canonicalId)
		{
			switch (canonicalId.ObjectType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
				return this.FormatAlternateId(canonicalId);
			case IdStorageType.PublicFolder:
				return this.FormatAlternatePublicFolderId(canonicalId);
			case IdStorageType.PublicFolderItem:
				return this.FormatAlternatePublcFolderItemId(canonicalId);
			default:
				return null;
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001BC21 File Offset: 0x00019E21
		internal override string ConvertStoreObjectIdToString(StoreObjectId storeObjectId)
		{
			return Convert.ToBase64String(storeObjectId.ProviderLevelItemId);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001BC2E File Offset: 0x00019E2E
		private static StoreObjectId ConvertIdHeaderToStoreObjectId(IdHeaderInformation idHeaderInformation)
		{
			return idHeaderInformation.ToStoreObjectId();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001BC38 File Offset: 0x00019E38
		private static StoreObjectId ConvertBytesToStoreObjectId(byte[] entryIdBytes)
		{
			StoreObjectId result;
			try
			{
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(entryIdBytes);
				result = storeObjectId;
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U, innerException);
			}
			return result;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001BC74 File Offset: 0x00019E74
		private AlternateId FormatAlternateId(CanonicalConvertedId canonicalId)
		{
			string id = IdConverter.GetConcatenatedId(canonicalId.StoreObjectId, new MailboxId(canonicalId.PrimarySmtpAddress, canonicalId.IsArchive), null).Id;
			return new AlternateId(id, canonicalId.PrimarySmtpAddress, this.IdFormat, canonicalId.IsArchive);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001BCC0 File Offset: 0x00019EC0
		private AlternatePublicFolderId FormatAlternatePublicFolderId(CanonicalConvertedId canonicalId)
		{
			canonicalId.StoreObjectId.UpdateItemType(StoreObjectType.Folder);
			string id = IdConverter.GetConcatenatedIdForPublicFolder(canonicalId.StoreObjectId).Id;
			return new AlternatePublicFolderId(id, this.IdFormat);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001BCFC File Offset: 0x00019EFC
		private AlternatePublicFolderItemId FormatAlternatePublcFolderItemId(CanonicalConvertedId canonicalId)
		{
			canonicalId.FolderId.UpdateItemType(StoreObjectType.Folder);
			string id = IdConverter.GetConcatenatedIdForPublicFolderItem(canonicalId.StoreObjectId, canonicalId.FolderId, null).Id;
			return new AlternatePublicFolderItemId(id, string.Empty, this.IdFormat);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001BD44 File Offset: 0x00019F44
		private IdHeaderInformation ConvertFromConcatenatedId(IdStorageType expectedIdType, string encodedId)
		{
			if (string.IsNullOrEmpty(encodedId))
			{
				ExTraceGlobals.ConvertIdCallTracer.TraceDebug((long)this.GetHashCode(), "[EwsIdConverter::ConvertFromConcatenatedId] string encodedId passed in was either null or empty");
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U);
			}
			IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(encodedId, BasicTypes.Item, null, true);
			if (expectedIdType != idHeaderInformation.IdStorageType)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U);
			}
			return idHeaderInformation;
		}
	}
}
