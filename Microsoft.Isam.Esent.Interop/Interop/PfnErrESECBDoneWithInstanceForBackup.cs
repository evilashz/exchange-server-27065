using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000014 RID: 20
	// (Invoke) Token: 0x06000040 RID: 64
	public delegate JET_err PfnErrESECBDoneWithInstanceForBackup(ESEBACK_CONTEXT pBackupContext, JET_INSTANCE ulInstanceId, BackupDone fComplete);
}
