using System;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000202 RID: 514
	internal enum ItemIndexError
	{
		// Token: 0x0400097D RID: 2429
		None,
		// Token: 0x0400097E RID: 2430
		GenericError,
		// Token: 0x0400097F RID: 2431
		Timeout,
		// Token: 0x04000980 RID: 2432
		StaleEvent,
		// Token: 0x04000981 RID: 2433
		MailboxOffline,
		// Token: 0x04000982 RID: 2434
		AttachmentLimitReached,
		// Token: 0x04000983 RID: 2435
		MarsWriterTruncation,
		// Token: 0x04000984 RID: 2436
		DocumentParserFailure
	}
}
