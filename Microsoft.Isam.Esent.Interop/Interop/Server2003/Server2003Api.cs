using System;

namespace Microsoft.Isam.Esent.Interop.Server2003
{
	// Token: 0x020002E0 RID: 736
	public static class Server2003Api
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x0001B0A2 File Offset: 0x000192A2
		public static void JetOSSnapshotAbort(JET_OSSNAPID snapid, SnapshotAbortGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotAbort(snapid, grbit));
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0001B0B6 File Offset: 0x000192B6
		public static void JetUpdate2(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize, UpdateGrbit grbit)
		{
			Api.Check(Api.Impl.JetUpdate2(sesid, tableid, bookmark, bookmarkSize, out actualBookmarkSize, grbit));
		}
	}
}
