using System;

namespace Microsoft.Exchange.Assistants.Logging
{
	// Token: 0x020000C5 RID: 197
	internal enum DatabaseSlaEventType
	{
		// Token: 0x04000394 RID: 916
		StartDatabase,
		// Token: 0x04000395 RID: 917
		StopDatabase,
		// Token: 0x04000396 RID: 918
		StartMailboxTableQuery,
		// Token: 0x04000397 RID: 919
		EndMailboxTableQuery,
		// Token: 0x04000398 RID: 920
		ErrorMailboxTableQuery,
		// Token: 0x04000399 RID: 921
		DatabaseIsStopped,
		// Token: 0x0400039A RID: 922
		ErrorProcessingDatabase
	}
}
