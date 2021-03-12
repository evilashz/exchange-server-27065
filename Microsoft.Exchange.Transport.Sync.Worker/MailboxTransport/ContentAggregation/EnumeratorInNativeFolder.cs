using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000201 RID: 513
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EnumeratorInNativeFolder : EnumeratorInFolder<StoreObjectId, UnifiedCustomSyncStateItem>
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x00038795 File Offset: 0x00036995
		internal EnumeratorInNativeFolder(IEnumerator<UnifiedCustomSyncStateItem> underlyingEnumerator, StoreObjectId nativeFolderId) : base(underlyingEnumerator, nativeFolderId)
		{
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0003879F File Offset: 0x0003699F
		protected override bool SkipCurrent(UnifiedCustomSyncStateItem item)
		{
			return item.NativeFolderId == null;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000387AC File Offset: 0x000369AC
		protected override StoreObjectId GetCurrent(UnifiedCustomSyncStateItem item)
		{
			return item.NativeId;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000387B4 File Offset: 0x000369B4
		protected override StoreObjectId GetCurrentFolder(UnifiedCustomSyncStateItem item)
		{
			return item.NativeFolderId;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000387BC File Offset: 0x000369BC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EnumeratorInNativeFolder>(this);
		}
	}
}
