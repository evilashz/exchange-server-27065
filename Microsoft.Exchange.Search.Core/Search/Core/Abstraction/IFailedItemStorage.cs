using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200001F RID: 31
	internal interface IFailedItemStorage : IDisposable
	{
		// Token: 0x0600008B RID: 139
		long GetFailedItemsCount(FailedItemParameters parameters);

		// Token: 0x0600008C RID: 140
		ICollection<IFailureEntry> GetFailedItems(FailedItemParameters parameters);

		// Token: 0x0600008D RID: 141
		long GetPermanentFailureCount();

		// Token: 0x0600008E RID: 142
		ICollection<IFailureEntry> GetRetriableItems(FieldSet fields);

		// Token: 0x0600008F RID: 143
		ICollection<IDocEntry> GetDeletionPendingItems(int deletedMailboxNumber);

		// Token: 0x06000090 RID: 144
		long GetItemsCount(Guid filterMailboxGuid);

		// Token: 0x06000091 RID: 145
		ICollection<long> GetPoisonDocuments();
	}
}
