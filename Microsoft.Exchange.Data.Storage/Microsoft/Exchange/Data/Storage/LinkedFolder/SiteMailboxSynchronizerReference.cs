using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000992 RID: 2450
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SiteMailboxSynchronizerReference : DisposeTrackableBase
	{
		// Token: 0x06005A5A RID: 23130 RVA: 0x00176F9B File Offset: 0x0017519B
		public SiteMailboxSynchronizerReference(SiteMailboxSynchronizer siteMailboxSynchronizer, Action<SiteMailboxSynchronizer> onDispose)
		{
			this.siteMailboxSynchronizer = siteMailboxSynchronizer;
			this.onDispose = onDispose;
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x00176FB1 File Offset: 0x001751B1
		public bool TryToSyncNow()
		{
			return this.siteMailboxSynchronizer.TryToSyncNow();
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x00176FBE File Offset: 0x001751BE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SiteMailboxSynchronizerReference>(this);
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x00176FC6 File Offset: 0x001751C6
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.onDispose(this.siteMailboxSynchronizer);
				this.siteMailboxSynchronizer = null;
			}
		}

		// Token: 0x040031E7 RID: 12775
		private readonly Action<SiteMailboxSynchronizer> onDispose;

		// Token: 0x040031E8 RID: 12776
		private SiteMailboxSynchronizer siteMailboxSynchronizer;
	}
}
