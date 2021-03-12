using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000017 RID: 23
	// (Invoke) Token: 0x0600004C RID: 76
	internal delegate JET_err NATIVE_PfnErrESECBDoneWithInstanceForBackup(IntPtr pBackupContext, IntPtr ulInstanceId, uint fComplete, IntPtr pvReserved);
}
