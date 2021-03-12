using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000011 RID: 17
	internal interface ITargetMailbox : ITarget, IDisposable
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F2 RID: 242
		bool ExportLocationExist { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F3 RID: 243
		bool WorkingLocationExist { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F4 RID: 244
		string PrimarySmtpAddress { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F5 RID: 245
		string LegacyDistinguishedName { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F6 RID: 246
		IEwsClient EwsClientInstance { get; }

		// Token: 0x060000F7 RID: 247
		string CreateResultFolder(string resultFolderName);

		// Token: 0x060000F8 RID: 248
		void PreRemoveSearchResults(bool removeLogs);

		// Token: 0x060000F9 RID: 249
		void RemoveSearchResults();

		// Token: 0x060000FA RID: 250
		BaseFolderType GetFolder(string folderId);

		// Token: 0x060000FB RID: 251
		BaseFolderType GetFolderByName(BaseFolderIdType parentFolderId, string folderName);

		// Token: 0x060000FC RID: 252
		BaseFolderType CreateFolder(BaseFolderIdType parentFolderId, string newFolderName, bool isHidden);

		// Token: 0x060000FD RID: 253
		List<ItemInformation> CopyItems(string parentFolderId, IList<ItemInformation> items);

		// Token: 0x060000FE RID: 254
		void CreateOrUpdateSearchLogEmail(MailboxDiscoverySearch searchObject, List<string> successfulMailboxes, List<string> unsuccessfulMailboxes);

		// Token: 0x060000FF RID: 255
		void WriteExportRecordLog(MailboxDiscoverySearch searchObject, IEnumerable<ExportRecord> records);

		// Token: 0x06000100 RID: 256
		void AttachDiscoveryLogFiles();
	}
}
