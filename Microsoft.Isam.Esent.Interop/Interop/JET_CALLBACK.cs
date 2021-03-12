using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200026A RID: 618
	// (Invoke) Token: 0x06000AD4 RID: 2772
	public delegate JET_err JET_CALLBACK(JET_SESID sesid, JET_DBID dbid, JET_TABLEID tableid, JET_cbtyp cbtyp, object arg1, object arg2, IntPtr context, IntPtr unused);
}
