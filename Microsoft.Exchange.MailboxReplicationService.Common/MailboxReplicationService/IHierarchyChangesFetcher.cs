using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200028E RID: 654
	internal interface IHierarchyChangesFetcher : IDisposable
	{
		// Token: 0x06002003 RID: 8195
		MailboxChangesManifest EnumerateHierarchyChanges(SyncHierarchyManifestState hierState, EnumerateHierarchyChangesFlags flags, int maxChanges);
	}
}
