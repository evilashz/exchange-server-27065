using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000060 RID: 96
	internal interface ICalendarInteropLog
	{
		// Token: 0x0600027F RID: 639
		void LogEntry(IStoreSession session, string entry, params object[] args);

		// Token: 0x06000280 RID: 640
		void LogEntry(IStoreSession session, Exception e, bool logWatsonReport, string entry, params object[] args);

		// Token: 0x06000281 RID: 641
		void SafeLogEntry(IStoreSession session, Exception exceptionToReport, bool logWatsonReport, string entry, params object[] args);
	}
}
