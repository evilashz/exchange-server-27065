using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000012 RID: 18
	public interface IIntegrityCheckTaskWithContinuation
	{
		// Token: 0x06000055 RID: 85
		bool ContinueExecuteOnFolder(Context context, MailboxEntry mailboxEntry, FolderEntry folderEntry);
	}
}
