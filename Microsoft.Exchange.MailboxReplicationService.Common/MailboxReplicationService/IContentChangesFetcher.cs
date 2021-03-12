using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200028C RID: 652
	internal interface IContentChangesFetcher : IDisposable
	{
		// Token: 0x06002000 RID: 8192
		FolderChangesManifest EnumerateContentChanges(SyncContentsManifestState syncState, EnumerateContentChangesFlags flags, int maxChanges);
	}
}
