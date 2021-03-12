using System;

namespace Microsoft.Isam.Esent.Interop.Vista
{
	// Token: 0x020002FD RID: 765
	public static class VistaApi
	{
		// Token: 0x06000E23 RID: 3619 RVA: 0x0001C8E5 File Offset: 0x0001AAE5
		public static void JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, JET_COLUMNID columnid, out JET_COLUMNBASE columnbase)
		{
			Api.Check(Api.Impl.JetGetColumnInfo(sesid, dbid, tablename, columnid, out columnbase));
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0001C8FD File Offset: 0x0001AAFD
		public static void JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, out JET_COLUMNBASE columnbase)
		{
			Api.Check(Api.Impl.JetGetTableColumnInfo(sesid, tableid, columnid, out columnbase));
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0001C913 File Offset: 0x0001AB13
		public static void JetOpenTemporaryTable(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable)
		{
			Api.Check(Api.Impl.JetOpenTemporaryTable(sesid, temporarytable));
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0001C927 File Offset: 0x0001AB27
		public static void JetGetThreadStats(out JET_THREADSTATS threadstats)
		{
			Api.Check(Api.Impl.JetGetThreadStats(out threadstats));
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0001C93A File Offset: 0x0001AB3A
		public static void JetOSSnapshotPrepareInstance(JET_OSSNAPID snapshot, JET_INSTANCE instance, SnapshotPrepareInstanceGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotPrepareInstance(snapshot, instance, grbit));
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0001C94F File Offset: 0x0001AB4F
		public static void JetOSSnapshotTruncateLog(JET_OSSNAPID snapshot, SnapshotTruncateLogGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotTruncateLog(snapshot, grbit));
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0001C963 File Offset: 0x0001AB63
		public static void JetOSSnapshotTruncateLogInstance(JET_OSSNAPID snapshot, JET_INSTANCE instance, SnapshotTruncateLogGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotTruncateLogInstance(snapshot, instance, grbit));
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0001C978 File Offset: 0x0001AB78
		public static void JetOSSnapshotGetFreezeInfo(JET_OSSNAPID snapshot, out int numInstances, out JET_INSTANCE_INFO[] instances, SnapshotGetFreezeInfoGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotGetFreezeInfo(snapshot, out numInstances, out instances, grbit));
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0001C98E File Offset: 0x0001AB8E
		public static void JetOSSnapshotEnd(JET_OSSNAPID snapshot, SnapshotEndGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotEnd(snapshot, grbit));
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0001C9A2 File Offset: 0x0001ABA2
		public static void JetGetInstanceMiscInfo(JET_INSTANCE instance, out JET_SIGNATURE signature, JET_InstanceMiscInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetInstanceMiscInfo(instance, out signature, infoLevel));
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0001C9B7 File Offset: 0x0001ABB7
		public static JET_wrn JetInit3(ref JET_INSTANCE instance, JET_RSTINFO recoveryOptions, InitGrbit grbit)
		{
			return Api.Check(Api.Impl.JetInit3(ref instance, recoveryOptions, grbit));
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0001C9CB File Offset: 0x0001ABCB
		public static void JetGetRecordSize(JET_SESID sesid, JET_TABLEID tableid, ref JET_RECSIZE recsize, GetRecordSizeGrbit grbit)
		{
			Api.Check(Api.Impl.JetGetRecordSize(sesid, tableid, ref recsize, grbit));
		}
	}
}
