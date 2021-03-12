using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000B8 RID: 184
	internal class EwsIdConverter : BaseAlternateIdConverter
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001B81C File Offset: 0x00019A1C
		internal override IdFormat IdFormat
		{
			get
			{
				return IdFormat.EwsId;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001B820 File Offset: 0x00019A20
		internal override CanonicalConvertedId Parse(AlternateId altId)
		{
			Util.ValidateSmtpAddress(altId.Mailbox);
			IdHeaderInformation idHeaderInformation = this.ConvertFromConcatenatedId(IdStorageType.MailboxItemMailboxGuidBased, altId.Id);
			StoreObjectId storeId = EwsIdConverter.ConvertIdHeaderToStoreObjectId(idHeaderInformation);
			ExchangePrincipal fromCache = ExchangePrincipalCache.GetFromCache(idHeaderInformation.MailboxId, CallContext.Current.ADRecipientSessionContext);
			return CanonicalConvertedId.CreateFromMailboxStoreId(storeId, fromCache.MailboxInfo.PrimarySmtpAddress.ToString(), altId.IsArchive);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001B888 File Offset: 0x00019A88
		internal override CanonicalConvertedId Parse(AlternatePublicFolderId altId)
		{
			IdHeaderInformation idHeaderInformation = this.ConvertFromConcatenatedId(IdStorageType.PublicFolder, altId.FolderId);
			StoreObjectId folderId = EwsIdConverter.ConvertBytesToStoreObjectId(idHeaderInformation.StoreIdBytes);
			return CanonicalConvertedId.CreateFromPublicFolderId(folderId);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001B8B8 File Offset: 0x00019AB8
		internal override CanonicalConvertedId Parse(AlternatePublicFolderItemId altId)
		{
			IdHeaderInformation idHeaderInformation = this.ConvertFromConcatenatedId(IdStorageType.PublicFolderItem, altId.ItemId);
			StoreObjectId itemId = EwsIdConverter.ConvertIdHeaderToStoreObjectId(idHeaderInformation);
			StoreObjectId folderId = EwsIdConverter.ConvertBytesToStoreObjectId(idHeaderInformation.FolderIdBytes);
			return CanonicalConvertedId.CreateFromPublicFolderItemId(itemId, folderId);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		internal override AlternateIdBase Format(CanonicalConvertedId canonicalId)
		{
			switch (canonicalId.ObjectType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
				return this.FormatAlternateIdEmailAddress(canonicalId);
			case IdStorageType.PublicFolder:
				return this.FormatAlternatePublicFolderId(canonicalId);
			case IdStorageType.PublicFolderItem:
				return this.FormatAlternatePublcFolderItemId(canonicalId);
			default:
				return null;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001B931 File Offset: 0x00019B31
		internal override string ConvertStoreObjectIdToString(StoreObjectId storeObjectId)
		{
			return Convert.ToBase64String(storeObjectId.ProviderLevelItemId);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001B93E File Offset: 0x00019B3E
		private static StoreObjectId ConvertIdHeaderToStoreObjectId(IdHeaderInformation idHeaderInformation)
		{
			return idHeaderInformation.ToStoreObjectId();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001B948 File Offset: 0x00019B48
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

		// Token: 0x0600050A RID: 1290 RVA: 0x0001B984 File Offset: 0x00019B84
		private AlternateId FormatAlternateIdEmailAddress(CanonicalConvertedId canonicalId)
		{
			ExchangePrincipal fromCache = ExchangePrincipalCache.GetFromCache(canonicalId.PrimarySmtpAddress, CallContext.Current.ADRecipientSessionContext, canonicalId.IsArchive);
			AlternateId result;
			try
			{
				MailboxId mailboxId = new MailboxId(fromCache.MailboxInfo.MailboxGuid, canonicalId.IsArchive);
				string id = IdConverter.GetConcatenatedId(canonicalId.StoreObjectId, mailboxId, null).Id;
				result = new AlternateId(id, fromCache.MailboxInfo.PrimarySmtpAddress.ToString(), this.IdFormat, canonicalId.IsArchive);
			}
			catch (NonExistentMailboxGuidException ex)
			{
				throw new NonExistentMailboxException((CoreResources.IDs)3279543955U, ex.MailboxGuid.ToString());
			}
			return result;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001BA44 File Offset: 0x00019C44
		private AlternatePublicFolderId FormatAlternatePublicFolderId(CanonicalConvertedId canonicalId)
		{
			canonicalId.StoreObjectId.UpdateItemType(StoreObjectType.Folder);
			string id = IdConverter.GetConcatenatedIdForPublicFolder(canonicalId.StoreObjectId).Id;
			return new AlternatePublicFolderId(id, this.IdFormat);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001BA80 File Offset: 0x00019C80
		private AlternatePublicFolderItemId FormatAlternatePublcFolderItemId(CanonicalConvertedId canonicalId)
		{
			canonicalId.FolderId.UpdateItemType(StoreObjectType.Folder);
			string id = IdConverter.GetConcatenatedIdForPublicFolderItem(canonicalId.StoreObjectId, canonicalId.FolderId, null).Id;
			return new AlternatePublicFolderItemId(id, string.Empty, this.IdFormat);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001BAC8 File Offset: 0x00019CC8
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
				throw new InvalidStoreIdException(ResponseCodeType.ErrorInvalidIdMalformed, (CoreResources.IDs)3010537222U);
			}
			return idHeaderInformation;
		}
	}
}
