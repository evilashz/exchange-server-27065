using System;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x02000312 RID: 786
	public static class Windows8Api
	{
		// Token: 0x06000E38 RID: 3640 RVA: 0x0001CA5B File Offset: 0x0001AC5B
		public static void JetStopServiceInstance2(JET_INSTANCE instance, StopServiceGrbit grbit)
		{
			Api.Check(Api.Impl.JetStopServiceInstance2(instance, grbit));
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0001CA6F File Offset: 0x0001AC6F
		public static void JetBeginTransaction3(JET_SESID sesid, long userTransactionId, BeginTransactionGrbit grbit)
		{
			Api.Check(Api.Impl.JetBeginTransaction3(sesid, userTransactionId, grbit));
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0001CA84 File Offset: 0x0001AC84
		public static void JetGetErrorInfo(JET_err error, out JET_ERRINFOBASIC errinfo)
		{
			Api.Check(Api.Impl.JetGetErrorInfo(error, out errinfo));
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0001CA98 File Offset: 0x0001AC98
		public static void JetResizeDatabase(JET_SESID sesid, JET_DBID dbid, int desiredPages, out int actualPages, ResizeDatabaseGrbit grbit)
		{
			Api.Check(Api.Impl.JetResizeDatabase(sesid, dbid, desiredPages, out actualPages, grbit));
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
		public static void JetCreateIndex4(JET_SESID sesid, JET_TABLEID tableid, JET_INDEXCREATE[] indexcreates, int numIndexCreates)
		{
			Api.Check(Api.Impl.JetCreateIndex4(sesid, tableid, indexcreates, numIndexCreates));
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0001CAC6 File Offset: 0x0001ACC6
		public static void JetOpenTemporaryTable2(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable)
		{
			Api.Check(Api.Impl.JetOpenTemporaryTable2(sesid, temporarytable));
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0001CADA File Offset: 0x0001ACDA
		public static void JetCreateTableColumnIndex4(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate)
		{
			Api.Check(Api.Impl.JetCreateTableColumnIndex4(sesid, dbid, tablecreate));
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0001CAEF File Offset: 0x0001ACEF
		public static void JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, byte[] data, int dataSize)
		{
			Api.Check(Api.Impl.JetSetSessionParameter(sesid, sesparamid, data, dataSize));
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0001CB05 File Offset: 0x0001AD05
		public static void JetCommitTransaction2(JET_SESID sesid, CommitTransactionGrbit grbit, TimeSpan durableCommit, out JET_COMMIT_ID commitId)
		{
			Api.Check(Api.Impl.JetCommitTransaction2(sesid, grbit, durableCommit, out commitId));
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		public static bool JetTryPrereadIndexRanges(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_RANGE[] indexRanges, int rangeIndex, int rangeCount, out int rangesPreread, JET_COLUMNID[] columnsPreread, PrereadIndexRangesGrbit grbit)
		{
			JET_err jet_err = (JET_err)Api.Impl.JetPrereadIndexRanges(sesid, tableid, indexRanges, rangeIndex, rangeCount, out rangesPreread, columnsPreread, grbit);
			return jet_err >= JET_err.Success;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0001CB48 File Offset: 0x0001AD48
		public static void JetPrereadIndexRanges(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_RANGE[] indexRanges, int rangeIndex, int rangeCount, out int rangesPreread, JET_COLUMNID[] columnsPreread, PrereadIndexRangesGrbit grbit)
		{
			Api.Check(Api.Impl.JetPrereadIndexRanges(sesid, tableid, indexRanges, rangeIndex, rangeCount, out rangesPreread, columnsPreread, grbit));
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0001CB74 File Offset: 0x0001AD74
		public static void PrereadKeyRanges(JET_SESID sesid, JET_TABLEID tableid, byte[][] keysStart, int[] keyStartLengths, byte[][] keysEnd, int[] keyEndLengths, int rangeIndex, int rangeCount, out int rangesPreread, JET_COLUMNID[] columnsPreread, PrereadIndexRangesGrbit grbit)
		{
			Api.Check(Api.Impl.JetPrereadKeyRanges(sesid, tableid, keysStart, keyStartLengths, keysEnd, keyEndLengths, rangeIndex, rangeCount, out rangesPreread, columnsPreread, grbit));
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0001CBA3 File Offset: 0x0001ADA3
		public static void JetSetCursorFilter(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_COLUMN[] filters, CursorFilterGrbit grbit)
		{
			Api.Check(Api.Impl.JetSetCursorFilter(sesid, tableid, filters, grbit));
		}
	}
}
