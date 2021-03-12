using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000041 RID: 65
	internal interface IWatermarkStorage : IDisposable
	{
		// Token: 0x0600014A RID: 330
		VersionInfo GetVersionInfo();

		// Token: 0x0600014B RID: 331
		long GetNotificationsWatermark();

		// Token: 0x0600014C RID: 332
		ICollection<MailboxCrawlerState> GetMailboxesForCrawling();

		// Token: 0x0600014D RID: 333
		ICollection<MailboxState> GetMailboxesForDeleting();

		// Token: 0x0600014E RID: 334
		IAsyncResult BeginSetVersionInfo(VersionInfo version, AsyncCallback callback, object state);

		// Token: 0x0600014F RID: 335
		void EndSetVersionInfo(IAsyncResult asyncResult);

		// Token: 0x06000150 RID: 336
		IAsyncResult BeginSetNotificationsWatermark(long watermark, AsyncCallback callback, object state);

		// Token: 0x06000151 RID: 337
		void EndSetNotificationsWatermark(IAsyncResult asyncResult);

		// Token: 0x06000152 RID: 338
		IAsyncResult BeginSetMailboxCrawlerState(MailboxCrawlerState mailboxState, AsyncCallback callback, object state);

		// Token: 0x06000153 RID: 339
		void EndSetMailboxCrawlerState(IAsyncResult asyncResult);

		// Token: 0x06000154 RID: 340
		IAsyncResult BeginSetMailboxDeletionPending(MailboxState mailboxState, AsyncCallback callback, object state);

		// Token: 0x06000155 RID: 341
		void EndSetMailboxDeletionPending(IAsyncResult asyncResult);

		// Token: 0x06000156 RID: 342
		void ResetWatermarkCache();

		// Token: 0x06000157 RID: 343
		void RefreshCachedCrawlerWatermarks();

		// Token: 0x06000158 RID: 344
		bool HasCrawlerWatermarks();
	}
}
