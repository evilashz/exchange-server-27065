using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000200 RID: 512
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FailedItemEnumeratorInCloudFolder : EnumeratorInFolder<string, KeyValuePair<string, string>>
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x00038763 File Offset: 0x00036963
		internal FailedItemEnumeratorInCloudFolder(IEnumerator<KeyValuePair<string, string>> underlyingEnumerator, string cloudFolderId) : base(underlyingEnumerator, cloudFolderId)
		{
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003876D File Offset: 0x0003696D
		protected override bool SkipCurrent(KeyValuePair<string, string> item)
		{
			return item.Value == null;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0003877B File Offset: 0x0003697B
		protected override string GetCurrent(KeyValuePair<string, string> item)
		{
			return item.Key;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00038784 File Offset: 0x00036984
		protected override string GetCurrentFolder(KeyValuePair<string, string> item)
		{
			return item.Value;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0003878D File Offset: 0x0003698D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FailedItemEnumeratorInCloudFolder>(this);
		}
	}
}
