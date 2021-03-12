using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002D8 RID: 728
	// (Invoke) Token: 0x06000D4F RID: 3407
	internal delegate JET_err NATIVE_CALLBACK(IntPtr sesid, uint dbid, IntPtr tableid, uint cbtyp, IntPtr arg1, IntPtr arg2, IntPtr context, IntPtr unused);
}
