using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000004 RID: 4
	// (Invoke) Token: 0x0600006C RID: 108
	internal unsafe delegate int GetDatabasesInfoDelegate(_ESEBACK_CONTEXT* pBackupContext, uint* pcInfo, _INSTANCE_BACKUP_INFO** prgInfo, uint fReserved);
}
