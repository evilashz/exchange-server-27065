using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000019 RID: 25
	// (Invoke) Token: 0x06000054 RID: 84
	internal delegate JET_err NATIVE_PfnErrESECBGetDatabasesInfo(IntPtr pBackupContext, out uint pcInfo, out IntPtr prgInfo, uint fReserved);
}
