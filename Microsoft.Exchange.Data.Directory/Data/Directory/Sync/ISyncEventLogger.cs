using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007C0 RID: 1984
	internal interface ISyncEventLogger
	{
		// Token: 0x0600628F RID: 25231
		void LogSerializationFailedEvent(string objectId, int errorCount);

		// Token: 0x06006290 RID: 25232
		void LogTooManyObjectReadRestartsEvent(string objectId, int pageLinkReadRestartsLimit);

		// Token: 0x06006291 RID: 25233
		void LogFullSyncFallbackDetectedEvent(BackSyncCookie previousCookie, BackSyncCookie currentCookie);
	}
}
