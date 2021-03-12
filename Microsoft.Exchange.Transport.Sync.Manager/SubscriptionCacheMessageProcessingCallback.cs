using System;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200001E RID: 30
	// (Invoke) Token: 0x060001CF RID: 463
	internal delegate void SubscriptionCacheMessageProcessingCallback(Guid mailboxGuid, SubscriptionCacheMessage cacheMessage, Exception exception);
}
