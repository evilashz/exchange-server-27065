using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200001A RID: 26
	// (Invoke) Token: 0x06000058 RID: 88
	internal delegate JET_err NATIVE_PfnErrESECBIsSGReplicated(IntPtr pContext, IntPtr ulInstanceId, out int pfReplicated, uint cbSGGuid, IntPtr wszSGGuid, out uint pcInfo, out IntPtr prgInfo);
}
