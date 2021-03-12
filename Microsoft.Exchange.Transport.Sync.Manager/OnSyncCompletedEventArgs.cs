using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OnSyncCompletedEventArgs : EventArgs
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000AC0C File Offset: 0x00008E0C
		internal OnSyncCompletedEventArgs(SubscriptionCompletionData subscriptionCompletionData)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionCompletionData", subscriptionCompletionData);
			this.subscriptionCompletionData = subscriptionCompletionData;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000AC26 File Offset: 0x00008E26
		internal SubscriptionCompletionData SubscriptionCompletionData
		{
			get
			{
				return this.subscriptionCompletionData;
			}
		}

		// Token: 0x04000108 RID: 264
		private readonly SubscriptionCompletionData subscriptionCompletionData;
	}
}
