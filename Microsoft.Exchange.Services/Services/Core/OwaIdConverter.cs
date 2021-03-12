using System;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000BE RID: 190
	internal class OwaIdConverter : BaseAlternateIdConverter
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0001C13C File Offset: 0x0001A33C
		internal override CanonicalConvertedId Parse(AlternateId altId)
		{
			Util.ValidateSmtpAddress(altId.Mailbox);
			CanonicalConvertedId canonicalConvertedId = this.ParseOwaString(IdStorageType.MailboxItemSmtpAddressBased, altId.Id);
			canonicalConvertedId.PrimarySmtpAddress = altId.Mailbox;
			canonicalConvertedId.IsArchive = altId.IsArchive;
			return canonicalConvertedId;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001C17B File Offset: 0x0001A37B
		internal override CanonicalConvertedId Parse(AlternatePublicFolderId altId)
		{
			return this.ParseOwaString(IdStorageType.PublicFolder, altId.FolderId);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001C18A File Offset: 0x0001A38A
		internal override CanonicalConvertedId Parse(AlternatePublicFolderItemId altId)
		{
			return this.ParseOwaString(IdStorageType.PublicFolderItem, altId.ItemId);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001C19C File Offset: 0x0001A39C
		internal override AlternateIdBase Format(CanonicalConvertedId canonicalId)
		{
			switch (canonicalId.ObjectType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
				return new AlternateId(this.ConvertStoreObjectIdToString(canonicalId.StoreObjectId), canonicalId.PrimarySmtpAddress, this.IdFormat, canonicalId.IsArchive);
			case IdStorageType.PublicFolder:
				return this.FormatPublicFolderId(canonicalId);
			case IdStorageType.PublicFolderItem:
				return this.FormatPublicFolderItemId(canonicalId);
			default:
				return null;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		internal override string ConvertStoreObjectIdToString(StoreObjectId storeObjectId)
		{
			string value = storeObjectId.ToBase64String();
			return WebUtility.UrlEncode(value);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001C216 File Offset: 0x0001A416
		internal override IdFormat IdFormat
		{
			get
			{
				return IdFormat.OwaId;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001C21C File Offset: 0x0001A41C
		internal CanonicalConvertedId ParseOwaString(IdStorageType expectedIdType, string owaString)
		{
			owaString = WebUtility.UrlDecode(owaString);
			IdStorageType idStorageType = OwaIdConverter.DetermineIdTypeFromOwaString(owaString);
			if (idStorageType != expectedIdType)
			{
				throw new InvalidStoreIdException(ResponseCodeType.ErrorInvalidIdMalformed, (CoreResources.IDs)3010537222U);
			}
			switch (idStorageType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
			{
				bool flag = OwaIdConverter.IsArchiveIdFromOwaString(owaString);
				if (flag)
				{
					owaString = owaString.Substring("AMB".Length + 1);
				}
				StoreObjectId storeId = this.ConvertStringToStoreObjectId(owaString);
				return CanonicalConvertedId.CreateFromMailboxStoreId(storeId, string.Empty, flag);
			}
			case IdStorageType.PublicFolder:
				return this.ParsePublicFolderIdString(owaString);
			case IdStorageType.PublicFolderItem:
				return this.ParsePublicFolderItemIdString(owaString);
			default:
				return null;
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001C2AB File Offset: 0x0001A4AB
		private static IdStorageType DetermineIdTypeFromOwaString(string owaString)
		{
			if (owaString.StartsWith("PSF."))
			{
				return IdStorageType.PublicFolder;
			}
			if (owaString.StartsWith("PSI."))
			{
				return IdStorageType.PublicFolderItem;
			}
			return IdStorageType.MailboxItemSmtpAddressBased;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001C2CC File Offset: 0x0001A4CC
		private static bool IsArchiveIdFromOwaString(string owaString)
		{
			return owaString.StartsWith("AMB.");
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001C2DC File Offset: 0x0001A4DC
		private CanonicalConvertedId ParsePublicFolderItemIdString(string owaString)
		{
			int num = owaString.LastIndexOf(".");
			if (num == "PSI".Length)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U);
			}
			string idValue = owaString.Substring("PSI".Length + 1, num - "PSI".Length - 1);
			string idValue2 = owaString.Substring(num + 1);
			StoreObjectId folderId = this.ConvertStringToStoreObjectId(idValue);
			StoreObjectId itemId = this.ConvertStringToStoreObjectId(idValue2);
			return CanonicalConvertedId.CreateFromPublicFolderItemId(itemId, folderId);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001C358 File Offset: 0x0001A558
		private CanonicalConvertedId ParsePublicFolderIdString(string owaString)
		{
			string idValue = owaString.Substring("PSF".Length + 1);
			StoreObjectId folderId = this.ConvertStringToStoreObjectId(idValue);
			return CanonicalConvertedId.CreateFromPublicFolderId(folderId);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001C388 File Offset: 0x0001A588
		private StoreObjectId ConvertStringToStoreObjectId(string idValue)
		{
			if (string.IsNullOrEmpty(idValue))
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U);
			}
			StoreObjectId result;
			try
			{
				result = StoreObjectId.Deserialize(idValue);
			}
			catch (FormatException innerException)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U, innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U, innerException2);
			}
			return result;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001C3F8 File Offset: 0x0001A5F8
		private AlternatePublicFolderId FormatPublicFolderId(CanonicalConvertedId id)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PSF");
			stringBuilder.Append(".");
			stringBuilder.Append(this.ConvertStoreObjectIdToString(id.StoreObjectId));
			return new AlternatePublicFolderId(stringBuilder.ToString(), this.IdFormat);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001C448 File Offset: 0x0001A648
		private AlternatePublicFolderItemId FormatPublicFolderItemId(CanonicalConvertedId id)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PSI");
			stringBuilder.Append(".");
			stringBuilder.Append(this.ConvertStoreObjectIdToString(id.FolderId));
			stringBuilder.Append(".");
			stringBuilder.Append(this.ConvertStoreObjectIdToString(id.StoreObjectId));
			return new AlternatePublicFolderItemId(stringBuilder.ToString(), string.Empty, this.IdFormat);
		}

		// Token: 0x04000675 RID: 1653
		private const string PublicFolderFolderPrefix = "PSF";

		// Token: 0x04000676 RID: 1654
		private const string PublicFolderItemPrefix = "PSI";

		// Token: 0x04000677 RID: 1655
		private const string ArchiveMailBoxObjectPrefix = "AMB";

		// Token: 0x04000678 RID: 1656
		private const string SeparatorChar = ".";
	}
}
