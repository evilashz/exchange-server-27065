using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001FF RID: 511
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EnumeratorInCloudFolder : EnumeratorInFolder<string, UnifiedCustomSyncStateItem>
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x00038734 File Offset: 0x00036934
		internal EnumeratorInCloudFolder(IEnumerator<UnifiedCustomSyncStateItem> underlyingEnumerator, string cloudFolderId) : base(underlyingEnumerator, cloudFolderId)
		{
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0003873E File Offset: 0x0003693E
		protected override bool SkipCurrent(UnifiedCustomSyncStateItem item)
		{
			return item.CloudFolderId == null;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0003874B File Offset: 0x0003694B
		protected override string GetCurrent(UnifiedCustomSyncStateItem item)
		{
			return item.CloudId;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00038753 File Offset: 0x00036953
		protected override string GetCurrentFolder(UnifiedCustomSyncStateItem item)
		{
			return item.CloudFolderId;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0003875B File Offset: 0x0003695B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EnumeratorInCloudFolder>(this);
		}
	}
}
