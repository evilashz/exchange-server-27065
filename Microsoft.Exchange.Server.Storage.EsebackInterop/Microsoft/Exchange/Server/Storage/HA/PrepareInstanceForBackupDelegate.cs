using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000002 RID: 2
	// (Invoke) Token: 0x06000064 RID: 100
	internal unsafe delegate int PrepareInstanceForBackupDelegate(_ESEBACK_CONTEXT* pBackupContext, ulong ulInstanceId, void* pvReserved);
}
