using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200006C RID: 108
	internal static class FolderIdHelper
	{
		// Token: 0x0600028B RID: 651 RVA: 0x000071E0 File Offset: 0x000053E0
		internal static string GetIndexForFolderEntryId(byte[] folderEntryId)
		{
			return HexConverter.ByteArrayToHexString(folderEntryId, 22, 24);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000071EC File Offset: 0x000053EC
		internal static bool IsValidFolderEntryId(byte[] folderEntryId)
		{
			return folderEntryId != null && folderEntryId.Length == 46;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000071FC File Offset: 0x000053FC
		internal static StoreObjectId GetStoreObjectIdFromHexString(string folderId, MailboxSession mailboxSession)
		{
			byte[] longTermId = HexConverter.HexStringToByteArray(folderId);
			Array.Resize<byte>(ref longTermId, 22);
			long idFromLongTermId = mailboxSession.IdConverter.GetIdFromLongTermId(longTermId);
			return mailboxSession.IdConverter.CreateFolderId(idFromLongTermId);
		}
	}
}
