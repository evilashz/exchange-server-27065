using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200011D RID: 285
	internal interface IFxProxyPool : IDisposable
	{
		// Token: 0x060009E9 RID: 2537
		IFolderProxy CreateFolder(FolderRec folder);

		// Token: 0x060009EA RID: 2538
		IFolderProxy GetFolderProxy(byte[] folderId);

		// Token: 0x060009EB RID: 2539
		EntryIdMap<byte[]> GetFolderData();

		// Token: 0x060009EC RID: 2540
		void Flush();

		// Token: 0x060009ED RID: 2541
		List<byte[]> GetUploadedMessageIDs();

		// Token: 0x060009EE RID: 2542
		void SetItemProperties(ItemPropertiesBase props);
	}
}
