using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200013D RID: 317
	internal interface ISourceFolder : IFolder, IDisposable
	{
		// Token: 0x06000A72 RID: 2674
		void CopyTo(IFxProxy destFolder, CopyPropertiesFlags flags, PropTag[] excludeTags);

		// Token: 0x06000A73 RID: 2675
		void ExportMessages(IFxProxy destFolderProxy, CopyMessagesFlags flags, byte[][] entryIds);

		// Token: 0x06000A74 RID: 2676
		FolderChangesManifest EnumerateChanges(EnumerateContentChangesFlags flags, int maxChanges);

		// Token: 0x06000A75 RID: 2677
		List<MessageRec> EnumerateMessagesPaged(int maxPageSize);

		// Token: 0x06000A76 RID: 2678
		int GetEstimatedItemCount();
	}
}
