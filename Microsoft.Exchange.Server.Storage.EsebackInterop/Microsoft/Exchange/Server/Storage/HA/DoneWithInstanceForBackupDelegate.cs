using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000003 RID: 3
	// (Invoke) Token: 0x06000068 RID: 104
	internal unsafe delegate int DoneWithInstanceForBackupDelegate(_ESEBACK_CONTEXT* pBackupContext, ulong ulInstanceId, uint fComplete, void* pvReserved);
}
