using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using Microsoft.Isam.Esent.Interop.Implementation;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000091 RID: 145
	public static class Api
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060005D8 RID: 1496 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		// (remove) Token: 0x060005D9 RID: 1497 RVA: 0x0000D61C File Offset: 0x0000B81C
		internal static event Api.ErrorHandler HandleError;

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000D64F File Offset: 0x0000B84F
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x0000D656 File Offset: 0x0000B856
		internal static IJetApi Impl { get; set; } = new JetApi();

		// Token: 0x060005DC RID: 1500 RVA: 0x0000D65E File Offset: 0x0000B85E
		public static void JetCreateInstance(out JET_INSTANCE instance, string name)
		{
			Api.Check(Api.Impl.JetCreateInstance(out instance, name));
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000D672 File Offset: 0x0000B872
		public static void JetCreateInstance2(out JET_INSTANCE instance, string name, string displayName, CreateInstanceGrbit grbit)
		{
			Api.Check(Api.Impl.JetCreateInstance2(out instance, name, displayName, grbit));
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000D688 File Offset: 0x0000B888
		public static void JetInit(ref JET_INSTANCE instance)
		{
			Api.Check(Api.Impl.JetInit(ref instance));
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000D69B File Offset: 0x0000B89B
		public static JET_wrn JetInit2(ref JET_INSTANCE instance, InitGrbit grbit)
		{
			return Api.Check(Api.Impl.JetInit2(ref instance, grbit));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000D6AE File Offset: 0x0000B8AE
		public static void JetGetInstanceInfo(out int numInstances, out JET_INSTANCE_INFO[] instances)
		{
			Api.Check(Api.Impl.JetGetInstanceInfo(out numInstances, out instances));
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000D6C2 File Offset: 0x0000B8C2
		public static void JetStopBackupInstance(JET_INSTANCE instance)
		{
			Api.Check(Api.Impl.JetStopBackupInstance(instance));
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000D6D5 File Offset: 0x0000B8D5
		public static void JetStopServiceInstance(JET_INSTANCE instance)
		{
			Api.Check(Api.Impl.JetStopServiceInstance(instance));
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
		public static void JetTerm(JET_INSTANCE instance)
		{
			Api.Check(Api.Impl.JetTerm(instance));
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000D6FB File Offset: 0x0000B8FB
		public static void JetTerm2(JET_INSTANCE instance, TermGrbit grbit)
		{
			Api.Check(Api.Impl.JetTerm2(instance, grbit));
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000D70F File Offset: 0x0000B90F
		public static JET_wrn JetSetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, int paramValue, string paramString)
		{
			return Api.Check(Api.Impl.JetSetSystemParameter(instance, sesid, paramid, new IntPtr(paramValue), paramString));
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000D72B File Offset: 0x0000B92B
		public static JET_wrn JetSetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, JET_CALLBACK paramValue, string paramString)
		{
			return Api.Check(Api.Impl.JetSetSystemParameter(instance, sesid, paramid, paramValue, paramString));
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000D742 File Offset: 0x0000B942
		public static JET_wrn JetSetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, IntPtr paramValue, string paramString)
		{
			return Api.Check(Api.Impl.JetSetSystemParameter(instance, sesid, paramid, paramValue, paramString));
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000D759 File Offset: 0x0000B959
		public static JET_wrn JetGetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, ref IntPtr paramValue, out string paramString, int maxParam)
		{
			return Api.Check(Api.Impl.JetGetSystemParameter(instance, sesid, paramid, ref paramValue, out paramString, maxParam));
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000D774 File Offset: 0x0000B974
		public static JET_wrn JetGetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, ref int paramValue, out string paramString, int maxParam)
		{
			IntPtr intPtr = new IntPtr(paramValue);
			JET_wrn result = Api.Check(Api.Impl.JetGetSystemParameter(instance, sesid, paramid, ref intPtr, out paramString, maxParam));
			paramValue = intPtr.ToInt32();
			return result;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000D7B2 File Offset: 0x0000B9B2
		[CLSCompliant(false)]
		public static void JetGetVersion(JET_SESID sesid, out uint version)
		{
			Api.Check(Api.Impl.JetGetVersion(sesid, out version));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000D7C6 File Offset: 0x0000B9C6
		public static void JetCreateDatabase(JET_SESID sesid, string database, string connect, out JET_DBID dbid, CreateDatabaseGrbit grbit)
		{
			Api.Check(Api.Impl.JetCreateDatabase(sesid, database, connect, out dbid, grbit));
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000D7DE File Offset: 0x0000B9DE
		public static void JetCreateDatabase2(JET_SESID sesid, string database, int maxPages, out JET_DBID dbid, CreateDatabaseGrbit grbit)
		{
			Api.Check(Api.Impl.JetCreateDatabase2(sesid, database, maxPages, out dbid, grbit));
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000D7F6 File Offset: 0x0000B9F6
		public static JET_wrn JetAttachDatabase(JET_SESID sesid, string database, AttachDatabaseGrbit grbit)
		{
			return Api.Check(Api.Impl.JetAttachDatabase(sesid, database, grbit));
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000D80A File Offset: 0x0000BA0A
		public static JET_wrn JetAttachDatabase2(JET_SESID sesid, string database, int maxPages, AttachDatabaseGrbit grbit)
		{
			return Api.Check(Api.Impl.JetAttachDatabase2(sesid, database, maxPages, grbit));
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000D81F File Offset: 0x0000BA1F
		public static JET_wrn JetOpenDatabase(JET_SESID sesid, string database, string connect, out JET_DBID dbid, OpenDatabaseGrbit grbit)
		{
			return Api.Check(Api.Impl.JetOpenDatabase(sesid, database, connect, out dbid, grbit));
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000D836 File Offset: 0x0000BA36
		public static void JetCloseDatabase(JET_SESID sesid, JET_DBID dbid, CloseDatabaseGrbit grbit)
		{
			Api.Check(Api.Impl.JetCloseDatabase(sesid, dbid, grbit));
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000D84B File Offset: 0x0000BA4B
		public static void JetDetachDatabase(JET_SESID sesid, string database)
		{
			Api.Check(Api.Impl.JetDetachDatabase(sesid, database));
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000D85F File Offset: 0x0000BA5F
		public static void JetDetachDatabase2(JET_SESID sesid, string database, DetachDatabaseGrbit grbit)
		{
			Api.Check(Api.Impl.JetDetachDatabase2(sesid, database, grbit));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000D874 File Offset: 0x0000BA74
		public static void JetCompact(JET_SESID sesid, string sourceDatabase, string destinationDatabase, JET_PFNSTATUS statusCallback, JET_CONVERT ignored, CompactGrbit grbit)
		{
			Api.Check(Api.Impl.JetCompact(sesid, sourceDatabase, destinationDatabase, statusCallback, ignored, grbit));
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000D88E File Offset: 0x0000BA8E
		public static void JetGrowDatabase(JET_SESID sesid, JET_DBID dbid, int desiredPages, out int actualPages)
		{
			Api.Check(Api.Impl.JetGrowDatabase(sesid, dbid, desiredPages, out actualPages));
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		public static void JetSetDatabaseSize(JET_SESID sesid, string database, int desiredPages, out int actualPages)
		{
			Api.Check(Api.Impl.JetSetDatabaseSize(sesid, database, desiredPages, out actualPages));
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000D8BA File Offset: 0x0000BABA
		public static void JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out int value, JET_DbInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetDatabaseInfo(sesid, dbid, out value, infoLevel));
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		public static void JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out JET_DBINFOMISC dbinfomisc, JET_DbInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetDatabaseInfo(sesid, dbid, out dbinfomisc, infoLevel));
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000D8E6 File Offset: 0x0000BAE6
		public static void JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out string value, JET_DbInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetDatabaseInfo(sesid, dbid, out value, infoLevel));
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000D8FC File Offset: 0x0000BAFC
		public static void JetGetDatabaseFileInfo(string databaseName, out int value, JET_DbInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetDatabaseFileInfo(databaseName, out value, infoLevel));
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000D911 File Offset: 0x0000BB11
		public static void JetGetDatabaseFileInfo(string databaseName, out long value, JET_DbInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetDatabaseFileInfo(databaseName, out value, infoLevel));
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000D926 File Offset: 0x0000BB26
		public static void JetGetDatabaseFileInfo(string databaseName, out JET_DBINFOMISC dbinfomisc, JET_DbInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetDatabaseFileInfo(databaseName, out dbinfomisc, infoLevel));
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000D93B File Offset: 0x0000BB3B
		public static void JetBackupInstance(JET_INSTANCE instance, string destination, BackupGrbit grbit, JET_PFNSTATUS statusCallback)
		{
			Api.Check(Api.Impl.JetBackupInstance(instance, destination, grbit, statusCallback));
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000D951 File Offset: 0x0000BB51
		public static void JetRestoreInstance(JET_INSTANCE instance, string source, string destination, JET_PFNSTATUS statusCallback)
		{
			Api.Check(Api.Impl.JetRestoreInstance(instance, source, destination, statusCallback));
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000D967 File Offset: 0x0000BB67
		public static void JetOSSnapshotFreeze(JET_OSSNAPID snapshot, out int numInstances, out JET_INSTANCE_INFO[] instances, SnapshotFreezeGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotFreeze(snapshot, out numInstances, out instances, grbit));
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000D97D File Offset: 0x0000BB7D
		public static void JetOSSnapshotPrepare(out JET_OSSNAPID snapshot, SnapshotPrepareGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotPrepare(out snapshot, grbit));
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000D991 File Offset: 0x0000BB91
		public static void JetOSSnapshotThaw(JET_OSSNAPID snapshot, SnapshotThawGrbit grbit)
		{
			Api.Check(Api.Impl.JetOSSnapshotThaw(snapshot, grbit));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000D9A5 File Offset: 0x0000BBA5
		public static void JetBeginExternalBackupInstance(JET_INSTANCE instance, BeginExternalBackupGrbit grbit)
		{
			Api.Check(Api.Impl.JetBeginExternalBackupInstance(instance, grbit));
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000D9B9 File Offset: 0x0000BBB9
		public static void JetCloseFileInstance(JET_INSTANCE instance, JET_HANDLE handle)
		{
			Api.Check(Api.Impl.JetCloseFileInstance(instance, handle));
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000D9CD File Offset: 0x0000BBCD
		public static void JetEndExternalBackupInstance(JET_INSTANCE instance)
		{
			Api.Check(Api.Impl.JetEndExternalBackupInstance(instance));
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		public static void JetEndExternalBackupInstance2(JET_INSTANCE instance, EndExternalBackupGrbit grbit)
		{
			Api.Check(Api.Impl.JetEndExternalBackupInstance2(instance, grbit));
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		public static void JetGetAttachInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars)
		{
			Api.Check(Api.Impl.JetGetAttachInfoInstance(instance, out files, maxChars, out actualChars));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000DA0A File Offset: 0x0000BC0A
		public static void JetGetLogInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars)
		{
			Api.Check(Api.Impl.JetGetLogInfoInstance(instance, out files, maxChars, out actualChars));
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000DA20 File Offset: 0x0000BC20
		public static void JetGetTruncateLogInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars)
		{
			Api.Check(Api.Impl.JetGetTruncateLogInfoInstance(instance, out files, maxChars, out actualChars));
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000DA36 File Offset: 0x0000BC36
		public static void JetOpenFileInstance(JET_INSTANCE instance, string file, out JET_HANDLE handle, out long fileSizeLow, out long fileSizeHigh)
		{
			Api.Check(Api.Impl.JetOpenFileInstance(instance, file, out handle, out fileSizeLow, out fileSizeHigh));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000DA4E File Offset: 0x0000BC4E
		public static void JetReadFileInstance(JET_INSTANCE instance, JET_HANDLE file, byte[] buffer, int bufferSize, out int bytesRead)
		{
			Api.Check(Api.Impl.JetReadFileInstance(instance, file, buffer, bufferSize, out bytesRead));
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000DA66 File Offset: 0x0000BC66
		public static void JetTruncateLogInstance(JET_INSTANCE instance)
		{
			Api.Check(Api.Impl.JetTruncateLogInstance(instance));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000DA79 File Offset: 0x0000BC79
		public static void JetBeginSession(JET_INSTANCE instance, out JET_SESID sesid, string username, string password)
		{
			Api.Check(Api.Impl.JetBeginSession(instance, out sesid, username, password));
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000DA8F File Offset: 0x0000BC8F
		public static void JetSetSessionContext(JET_SESID sesid, IntPtr context)
		{
			Api.Check(Api.Impl.JetSetSessionContext(sesid, context));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000DAA3 File Offset: 0x0000BCA3
		public static void JetResetSessionContext(JET_SESID sesid)
		{
			Api.Check(Api.Impl.JetResetSessionContext(sesid));
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000DAB6 File Offset: 0x0000BCB6
		public static void JetEndSession(JET_SESID sesid, EndSessionGrbit grbit)
		{
			Api.Check(Api.Impl.JetEndSession(sesid, grbit));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000DACA File Offset: 0x0000BCCA
		public static void JetDupSession(JET_SESID sesid, out JET_SESID newSesid)
		{
			Api.Check(Api.Impl.JetDupSession(sesid, out newSesid));
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0000DADE File Offset: 0x0000BCDE
		public static JET_wrn JetOpenTable(JET_SESID sesid, JET_DBID dbid, string tablename, byte[] parameters, int parametersSize, OpenTableGrbit grbit, out JET_TABLEID tableid)
		{
			return Api.Check(Api.Impl.JetOpenTable(sesid, dbid, tablename, parameters, parametersSize, grbit, out tableid));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0000DAF9 File Offset: 0x0000BCF9
		public static void JetCloseTable(JET_SESID sesid, JET_TABLEID tableid)
		{
			Api.Check(Api.Impl.JetCloseTable(sesid, tableid));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0000DB0D File Offset: 0x0000BD0D
		public static void JetDupCursor(JET_SESID sesid, JET_TABLEID tableid, out JET_TABLEID newTableid, DupCursorGrbit grbit)
		{
			Api.Check(Api.Impl.JetDupCursor(sesid, tableid, out newTableid, grbit));
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0000DB23 File Offset: 0x0000BD23
		public static void JetComputeStats(JET_SESID sesid, JET_TABLEID tableid)
		{
			Api.Check(Api.Impl.JetComputeStats(sesid, tableid));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0000DB37 File Offset: 0x0000BD37
		public static void JetSetLS(JET_SESID sesid, JET_TABLEID tableid, JET_LS ls, LsGrbit grbit)
		{
			Api.Check(Api.Impl.JetSetLS(sesid, tableid, ls, grbit));
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0000DB4D File Offset: 0x0000BD4D
		public static void JetGetLS(JET_SESID sesid, JET_TABLEID tableid, out JET_LS ls, LsGrbit grbit)
		{
			Api.Check(Api.Impl.JetGetLS(sesid, tableid, out ls, grbit));
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000DB63 File Offset: 0x0000BD63
		public static void JetGetCursorInfo(JET_SESID sesid, JET_TABLEID tableid)
		{
			Api.Check(Api.Impl.JetGetCursorInfo(sesid, tableid));
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000DB77 File Offset: 0x0000BD77
		public static void JetBeginTransaction(JET_SESID sesid)
		{
			Api.Check(Api.Impl.JetBeginTransaction(sesid));
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0000DB8A File Offset: 0x0000BD8A
		public static void JetBeginTransaction2(JET_SESID sesid, BeginTransactionGrbit grbit)
		{
			Api.Check(Api.Impl.JetBeginTransaction2(sesid, grbit));
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000DB9E File Offset: 0x0000BD9E
		public static void JetCommitTransaction(JET_SESID sesid, CommitTransactionGrbit grbit)
		{
			Api.Check(Api.Impl.JetCommitTransaction(sesid, grbit));
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000DBB2 File Offset: 0x0000BDB2
		public static void JetRollback(JET_SESID sesid, RollbackTransactionGrbit grbit)
		{
			Api.Check(Api.Impl.JetRollback(sesid, grbit));
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000DBC6 File Offset: 0x0000BDC6
		public static void JetCreateTable(JET_SESID sesid, JET_DBID dbid, string table, int pages, int density, out JET_TABLEID tableid)
		{
			Api.Check(Api.Impl.JetCreateTable(sesid, dbid, table, pages, density, out tableid));
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public static void JetAddColumn(JET_SESID sesid, JET_TABLEID tableid, string column, JET_COLUMNDEF columndef, byte[] defaultValue, int defaultValueSize, out JET_COLUMNID columnid)
		{
			Api.Check(Api.Impl.JetAddColumn(sesid, tableid, column, columndef, defaultValue, defaultValueSize, out columnid));
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public static void JetDeleteColumn(JET_SESID sesid, JET_TABLEID tableid, string column)
		{
			Api.Check(Api.Impl.JetDeleteColumn(sesid, tableid, column));
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000DC11 File Offset: 0x0000BE11
		public static void JetDeleteColumn2(JET_SESID sesid, JET_TABLEID tableid, string column, DeleteColumnGrbit grbit)
		{
			Api.Check(Api.Impl.JetDeleteColumn2(sesid, tableid, column, grbit));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000DC27 File Offset: 0x0000BE27
		public static void JetDeleteIndex(JET_SESID sesid, JET_TABLEID tableid, string index)
		{
			Api.Check(Api.Impl.JetDeleteIndex(sesid, tableid, index));
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		public static void JetDeleteTable(JET_SESID sesid, JET_DBID dbid, string table)
		{
			Api.Check(Api.Impl.JetDeleteTable(sesid, dbid, table));
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000DC51 File Offset: 0x0000BE51
		public static void JetCreateIndex(JET_SESID sesid, JET_TABLEID tableid, string indexName, CreateIndexGrbit grbit, string keyDescription, int keyDescriptionLength, int density)
		{
			Api.Check(Api.Impl.JetCreateIndex(sesid, tableid, indexName, grbit, keyDescription, keyDescriptionLength, density));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000DC6D File Offset: 0x0000BE6D
		public static void JetCreateIndex2(JET_SESID sesid, JET_TABLEID tableid, JET_INDEXCREATE[] indexcreates, int numIndexCreates)
		{
			Api.Check(Api.Impl.JetCreateIndex2(sesid, tableid, indexcreates, numIndexCreates));
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0000DC83 File Offset: 0x0000BE83
		public static void JetOpenTempTable(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids)
		{
			Api.Check(Api.Impl.JetOpenTempTable(sesid, columns, numColumns, grbit, out tableid, columnids));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000DC9D File Offset: 0x0000BE9D
		public static void JetOpenTempTable2(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, int lcid, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids)
		{
			Api.Check(Api.Impl.JetOpenTempTable2(sesid, columns, numColumns, lcid, grbit, out tableid, columnids));
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000DCB9 File Offset: 0x0000BEB9
		public static void JetOpenTempTable3(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, JET_UNICODEINDEX unicodeindex, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids)
		{
			Api.Check(Api.Impl.JetOpenTempTable3(sesid, columns, numColumns, unicodeindex, grbit, out tableid, columnids));
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000DCD5 File Offset: 0x0000BED5
		public static void JetCreateTableColumnIndex3(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate)
		{
			Api.Check(Api.Impl.JetCreateTableColumnIndex3(sesid, dbid, tablecreate));
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000DCEA File Offset: 0x0000BEEA
		public static void JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string columnName, out JET_COLUMNDEF columndef)
		{
			Api.Check(Api.Impl.JetGetTableColumnInfo(sesid, tableid, columnName, out columndef));
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000DD00 File Offset: 0x0000BF00
		public static void JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, out JET_COLUMNDEF columndef)
		{
			Api.Check(Api.Impl.JetGetTableColumnInfo(sesid, tableid, columnid, out columndef));
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000DD16 File Offset: 0x0000BF16
		public static void JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string columnName, out JET_COLUMNBASE columnbase)
		{
			Api.Check(Api.Impl.JetGetTableColumnInfo(sesid, tableid, columnName, out columnbase));
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public static void JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string columnName, out JET_COLUMNLIST columnlist)
		{
			Api.Check(Api.Impl.JetGetTableColumnInfo(sesid, tableid, columnName, ColInfoGrbit.None, out columnlist));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000DD43 File Offset: 0x0000BF43
		public static void JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string columnName, ColInfoGrbit grbit, out JET_COLUMNLIST columnlist)
		{
			Api.Check(Api.Impl.JetGetTableColumnInfo(sesid, tableid, columnName, grbit, out columnlist));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000DD5B File Offset: 0x0000BF5B
		public static void JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string columnName, out JET_COLUMNDEF columndef)
		{
			Api.Check(Api.Impl.JetGetColumnInfo(sesid, dbid, tablename, columnName, out columndef));
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000DD73 File Offset: 0x0000BF73
		public static void JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string columnName, out JET_COLUMNLIST columnlist)
		{
			Api.Check(Api.Impl.JetGetColumnInfo(sesid, dbid, tablename, columnName, out columnlist));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000DD8B File Offset: 0x0000BF8B
		public static void JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string columnName, out JET_COLUMNBASE columnbase)
		{
			Api.Check(Api.Impl.JetGetColumnInfo(sesid, dbid, tablename, columnName, out columnbase));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000DDA3 File Offset: 0x0000BFA3
		public static void JetGetObjectInfo(JET_SESID sesid, JET_DBID dbid, out JET_OBJECTLIST objectlist)
		{
			Api.Check(Api.Impl.JetGetObjectInfo(sesid, dbid, out objectlist));
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		public static void JetGetObjectInfo(JET_SESID sesid, JET_DBID dbid, JET_objtyp objtyp, string objectName, out JET_OBJECTINFO objectinfo)
		{
			Api.Check(Api.Impl.JetGetObjectInfo(sesid, dbid, objtyp, objectName, out objectinfo));
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		public static void JetGetCurrentIndex(JET_SESID sesid, JET_TABLEID tableid, out string indexName, int maxNameLength)
		{
			Api.Check(Api.Impl.JetGetCurrentIndex(sesid, tableid, out indexName, maxNameLength));
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000DDE6 File Offset: 0x0000BFE6
		public static void JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out JET_OBJECTINFO result, JET_TblInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableInfo(sesid, tableid, out result, infoLevel));
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		public static void JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out string result, JET_TblInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableInfo(sesid, tableid, out result, infoLevel));
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000DE12 File Offset: 0x0000C012
		public static void JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out JET_DBID result, JET_TblInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableInfo(sesid, tableid, out result, infoLevel));
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000DE28 File Offset: 0x0000C028
		public static void JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, int[] result, JET_TblInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableInfo(sesid, tableid, result, infoLevel));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000DE3E File Offset: 0x0000C03E
		public static void JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out int result, JET_TblInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableInfo(sesid, tableid, out result, infoLevel));
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000DE54 File Offset: 0x0000C054
		[CLSCompliant(false)]
		public static void JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out ushort result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetIndexInfo(sesid, dbid, tablename, indexname, out result, infoLevel));
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000DE6E File Offset: 0x0000C06E
		public static void JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out int result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetIndexInfo(sesid, dbid, tablename, indexname, out result, infoLevel));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000DE88 File Offset: 0x0000C088
		public static void JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out JET_INDEXID result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetIndexInfo(sesid, dbid, tablename, indexname, out result, infoLevel));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000DEA2 File Offset: 0x0000C0A2
		public static void JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out JET_INDEXLIST result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetIndexInfo(sesid, dbid, tablename, indexname, out result, infoLevel));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		public static void JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out string result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetIndexInfo(sesid, dbid, tablename, indexname, out result, infoLevel));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000DED6 File Offset: 0x0000C0D6
		[CLSCompliant(false)]
		public static void JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out ushort result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableIndexInfo(sesid, tableid, indexname, out result, infoLevel));
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000DEEE File Offset: 0x0000C0EE
		public static void JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out int result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableIndexInfo(sesid, tableid, indexname, out result, infoLevel));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000DF06 File Offset: 0x0000C106
		public static void JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out JET_INDEXID result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableIndexInfo(sesid, tableid, indexname, out result, infoLevel));
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000DF1E File Offset: 0x0000C11E
		public static void JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out JET_INDEXLIST result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableIndexInfo(sesid, tableid, indexname, out result, infoLevel));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000DF36 File Offset: 0x0000C136
		public static void JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out string result, JET_IdxInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetTableIndexInfo(sesid, tableid, indexname, out result, infoLevel));
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000DF4E File Offset: 0x0000C14E
		public static void JetRenameTable(JET_SESID sesid, JET_DBID dbid, string tableName, string newTableName)
		{
			Api.Check(Api.Impl.JetRenameTable(sesid, dbid, tableName, newTableName));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000DF64 File Offset: 0x0000C164
		public static void JetRenameColumn(JET_SESID sesid, JET_TABLEID tableid, string name, string newName, RenameColumnGrbit grbit)
		{
			Api.Check(Api.Impl.JetRenameColumn(sesid, tableid, name, newName, grbit));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public static void JetSetColumnDefaultValue(JET_SESID sesid, JET_DBID dbid, string tableName, string columnName, byte[] data, int dataSize, SetColumnDefaultValueGrbit grbit)
		{
			Api.Check(Api.Impl.JetSetColumnDefaultValue(sesid, dbid, tableName, columnName, data, dataSize, grbit));
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000DF98 File Offset: 0x0000C198
		public static void JetGotoBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize)
		{
			Api.Check(Api.Impl.JetGotoBookmark(sesid, tableid, bookmark, bookmarkSize));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000DFAE File Offset: 0x0000C1AE
		public static void JetGotoSecondaryIndexBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] secondaryKey, int secondaryKeySize, byte[] primaryKey, int primaryKeySize, GotoSecondaryIndexBookmarkGrbit grbit)
		{
			Api.Check(Api.Impl.JetGotoSecondaryIndexBookmark(sesid, tableid, secondaryKey, secondaryKeySize, primaryKey, primaryKeySize, grbit));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000DFCA File Offset: 0x0000C1CA
		public static void JetMove(JET_SESID sesid, JET_TABLEID tableid, int numRows, MoveGrbit grbit)
		{
			Api.Check(Api.Impl.JetMove(sesid, tableid, numRows, grbit));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		public static void JetMove(JET_SESID sesid, JET_TABLEID tableid, JET_Move numRows, MoveGrbit grbit)
		{
			Api.Check(Api.Impl.JetMove(sesid, tableid, (int)numRows, grbit));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		public unsafe static void JetMakeKey(JET_SESID sesid, JET_TABLEID tableid, byte[] data, int dataSize, MakeKeyGrbit grbit)
		{
			if ((data == null && dataSize != 0) || (data != null && dataSize > data.Length))
			{
				throw new ArgumentOutOfRangeException("dataSize", dataSize, "cannot be greater than the length of the data");
			}
			fixed (byte* ptr = data)
			{
				Api.JetMakeKey(sesid, tableid, new IntPtr((void*)ptr), dataSize, grbit);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0000E055 File Offset: 0x0000C255
		public static JET_wrn JetSeek(JET_SESID sesid, JET_TABLEID tableid, SeekGrbit grbit)
		{
			return Api.Check(Api.Impl.JetSeek(sesid, tableid, grbit));
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000E069 File Offset: 0x0000C269
		public static void JetSetIndexRange(JET_SESID sesid, JET_TABLEID tableid, SetIndexRangeGrbit grbit)
		{
			Api.Check(Api.Impl.JetSetIndexRange(sesid, tableid, grbit));
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000E07E File Offset: 0x0000C27E
		public static void JetIntersectIndexes(JET_SESID sesid, JET_INDEXRANGE[] ranges, int numRanges, out JET_RECORDLIST recordlist, IntersectIndexesGrbit grbit)
		{
			Api.Check(Api.Impl.JetIntersectIndexes(sesid, ranges, numRanges, out recordlist, grbit));
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000E096 File Offset: 0x0000C296
		public static void JetSetCurrentIndex(JET_SESID sesid, JET_TABLEID tableid, string index)
		{
			Api.Check(Api.Impl.JetSetCurrentIndex(sesid, tableid, index));
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000E0AB File Offset: 0x0000C2AB
		public static void JetSetCurrentIndex2(JET_SESID sesid, JET_TABLEID tableid, string index, SetCurrentIndexGrbit grbit)
		{
			Api.Check(Api.Impl.JetSetCurrentIndex2(sesid, tableid, index, grbit));
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
		public static void JetSetCurrentIndex3(JET_SESID sesid, JET_TABLEID tableid, string index, SetCurrentIndexGrbit grbit, int itagSequence)
		{
			Api.Check(Api.Impl.JetSetCurrentIndex3(sesid, tableid, index, grbit, itagSequence));
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0000E0D9 File Offset: 0x0000C2D9
		public static void JetSetCurrentIndex4(JET_SESID sesid, JET_TABLEID tableid, string index, JET_INDEXID indexid, SetCurrentIndexGrbit grbit, int itagSequence)
		{
			Api.Check(Api.Impl.JetSetCurrentIndex4(sesid, tableid, index, indexid, grbit, itagSequence));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000E0F3 File Offset: 0x0000C2F3
		public static void JetIndexRecordCount(JET_SESID sesid, JET_TABLEID tableid, out int numRecords, int maxRecordsToCount)
		{
			if (maxRecordsToCount == 0)
			{
				maxRecordsToCount = int.MaxValue;
			}
			Api.Check(Api.Impl.JetIndexRecordCount(sesid, tableid, out numRecords, maxRecordsToCount));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0000E113 File Offset: 0x0000C313
		public static void JetSetTableSequential(JET_SESID sesid, JET_TABLEID tableid, SetTableSequentialGrbit grbit)
		{
			Api.Check(Api.Impl.JetSetTableSequential(sesid, tableid, grbit));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0000E128 File Offset: 0x0000C328
		public static void JetResetTableSequential(JET_SESID sesid, JET_TABLEID tableid, ResetTableSequentialGrbit grbit)
		{
			Api.Check(Api.Impl.JetResetTableSequential(sesid, tableid, grbit));
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000E13D File Offset: 0x0000C33D
		public static void JetGetRecordPosition(JET_SESID sesid, JET_TABLEID tableid, out JET_RECPOS recpos)
		{
			Api.Check(Api.Impl.JetGetRecordPosition(sesid, tableid, out recpos));
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000E152 File Offset: 0x0000C352
		public static void JetGotoPosition(JET_SESID sesid, JET_TABLEID tableid, JET_RECPOS recpos)
		{
			Api.Check(Api.Impl.JetGotoPosition(sesid, tableid, recpos));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000E167 File Offset: 0x0000C367
		public static void JetGetBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize)
		{
			Api.Check(Api.Impl.JetGetBookmark(sesid, tableid, bookmark, bookmarkSize, out actualBookmarkSize));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000E180 File Offset: 0x0000C380
		public static void JetGetSecondaryIndexBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] secondaryKey, int secondaryKeySize, out int actualSecondaryKeySize, byte[] primaryKey, int primaryKeySize, out int actualPrimaryKeySize, GetSecondaryIndexBookmarkGrbit grbit)
		{
			Api.Check(Api.Impl.JetGetSecondaryIndexBookmark(sesid, tableid, secondaryKey, secondaryKeySize, out actualSecondaryKeySize, primaryKey, primaryKeySize, out actualPrimaryKeySize, grbit));
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000E1AB File Offset: 0x0000C3AB
		public static void JetRetrieveKey(JET_SESID sesid, JET_TABLEID tableid, byte[] data, int dataSize, out int actualDataSize, RetrieveKeyGrbit grbit)
		{
			Api.Check(Api.Impl.JetRetrieveKey(sesid, tableid, data, dataSize, out actualDataSize, grbit));
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		public static JET_wrn JetRetrieveColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] data, int dataSize, out int actualDataSize, RetrieveColumnGrbit grbit, JET_RETINFO retinfo)
		{
			return Api.JetRetrieveColumn(sesid, tableid, columnid, data, dataSize, 0, out actualDataSize, grbit, retinfo);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public unsafe static JET_wrn JetRetrieveColumns(JET_SESID sesid, JET_TABLEID tableid, JET_RETRIEVECOLUMN[] retrievecolumns, int numColumns)
		{
			if (retrievecolumns == null)
			{
				throw new ArgumentNullException("retrievecolumns");
			}
			if (numColumns < 0 || numColumns > retrievecolumns.Length)
			{
				throw new ArgumentOutOfRangeException("numColumns", numColumns, "cannot be negative or greater than retrievecolumns.Length");
			}
			NATIVE_RETRIEVECOLUMN* ptr = stackalloc NATIVE_RETRIEVECOLUMN[checked(unchecked((UIntPtr)numColumns) * (UIntPtr)sizeof(NATIVE_RETRIEVECOLUMN))];
			int err = Api.PinColumnsAndRetrieve(sesid, tableid, ptr, retrievecolumns, numColumns, 0);
			for (int i = 0; i < numColumns; i++)
			{
				retrievecolumns[i].UpdateFromNativeRetrievecolumn(ref ptr[i]);
			}
			return Api.Check(err);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000E260 File Offset: 0x0000C460
		[CLSCompliant(false)]
		public static JET_wrn JetEnumerateColumns(JET_SESID sesid, JET_TABLEID tableid, int numColumnids, JET_ENUMCOLUMNID[] columnids, out int numColumnValues, out JET_ENUMCOLUMN[] columnValues, JET_PFNREALLOC allocator, IntPtr allocatorContext, int maxDataSize, EnumerateColumnsGrbit grbit)
		{
			return Api.Check(Api.Impl.JetEnumerateColumns(sesid, tableid, numColumnids, columnids, out numColumnValues, out columnValues, allocator, allocatorContext, maxDataSize, grbit));
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000E28C File Offset: 0x0000C48C
		public static void JetDelete(JET_SESID sesid, JET_TABLEID tableid)
		{
			Api.Check(Api.Impl.JetDelete(sesid, tableid));
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		public static void JetPrepareUpdate(JET_SESID sesid, JET_TABLEID tableid, JET_prep prep)
		{
			Api.Check(Api.Impl.JetPrepareUpdate(sesid, tableid, prep));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000E2B5 File Offset: 0x0000C4B5
		public static void JetUpdate(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize)
		{
			Api.Check(Api.Impl.JetUpdate(sesid, tableid, bookmark, bookmarkSize, out actualBookmarkSize));
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		public static void JetUpdate(JET_SESID sesid, JET_TABLEID tableid)
		{
			int num;
			Api.Check(Api.Impl.JetUpdate(sesid, tableid, null, 0, out num));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0000E2F3 File Offset: 0x0000C4F3
		public static JET_wrn JetSetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] data, int dataSize, SetColumnGrbit grbit, JET_SETINFO setinfo)
		{
			return Api.JetSetColumn(sesid, tableid, columnid, data, dataSize, 0, grbit, setinfo);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000E308 File Offset: 0x0000C508
		[SecurityPermission(SecurityAction.LinkDemand)]
		public unsafe static JET_wrn JetSetColumns(JET_SESID sesid, JET_TABLEID tableid, JET_SETCOLUMN[] setcolumns, int numColumns)
		{
			if (setcolumns == null)
			{
				throw new ArgumentNullException("setcolumns");
			}
			if (numColumns < 0 || numColumns > setcolumns.Length)
			{
				throw new ArgumentOutOfRangeException("numColumns", numColumns, "cannot be negative or greater than setcolumns.Length");
			}
			JET_wrn result;
			using (GCHandleCollection gchandleCollection = default(GCHandleCollection))
			{
				NATIVE_SETCOLUMN* ptr = stackalloc NATIVE_SETCOLUMN[checked(unchecked((UIntPtr)numColumns) * (UIntPtr)sizeof(NATIVE_SETCOLUMN))];
				byte* ptr2 = stackalloc byte[(UIntPtr)128];
				int num = 128;
				for (int i = 0; i < numColumns; i++)
				{
					setcolumns[i].CheckDataSize();
					ptr[i] = setcolumns[i].GetNativeSetcolumn();
					if (setcolumns[i].pvData == null)
					{
						ptr[i].pvData = IntPtr.Zero;
					}
					else if (num >= setcolumns[i].cbData)
					{
						ptr[i].pvData = new IntPtr((void*)ptr2);
						Marshal.Copy(setcolumns[i].pvData, setcolumns[i].ibData, ptr[i].pvData, setcolumns[i].cbData);
						ptr2 += setcolumns[i].cbData;
						num -= setcolumns[i].cbData;
					}
					else
					{
						byte* ptr3 = (byte*)gchandleCollection.Add(setcolumns[i].pvData).ToPointer();
						ptr[i].pvData = new IntPtr((void*)(ptr3 + setcolumns[i].ibData));
					}
				}
				int err = Api.Impl.JetSetColumns(sesid, tableid, ptr, numColumns);
				for (int j = 0; j < numColumns; j++)
				{
					setcolumns[j].err = (JET_wrn)ptr[j].err;
				}
				result = Api.Check(err);
			}
			return result;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		public static void JetGetLock(JET_SESID sesid, JET_TABLEID tableid, GetLockGrbit grbit)
		{
			Api.Check(Api.Impl.JetGetLock(sesid, tableid, grbit));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		public static void JetEscrowUpdate(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] delta, int deltaSize, byte[] previousValue, int previousValueLength, out int actualPreviousValueLength, EscrowUpdateGrbit grbit)
		{
			Api.Check(Api.Impl.JetEscrowUpdate(sesid, tableid, columnid, delta, deltaSize, previousValue, previousValueLength, out actualPreviousValueLength, grbit));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000E527 File Offset: 0x0000C727
		public static void JetRegisterCallback(JET_SESID sesid, JET_TABLEID tableid, JET_cbtyp cbtyp, JET_CALLBACK callback, IntPtr context, out JET_HANDLE callbackId)
		{
			Api.Check(Api.Impl.JetRegisterCallback(sesid, tableid, cbtyp, callback, context, out callbackId));
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000E541 File Offset: 0x0000C741
		public static void JetUnregisterCallback(JET_SESID sesid, JET_TABLEID tableid, JET_cbtyp cbtyp, JET_HANDLE callbackId)
		{
			Api.Check(Api.Impl.JetUnregisterCallback(sesid, tableid, cbtyp, callbackId));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000E557 File Offset: 0x0000C757
		public static JET_wrn JetDefragment(JET_SESID sesid, JET_DBID dbid, string tableName, ref int passes, ref int seconds, DefragGrbit grbit)
		{
			return Api.Check(Api.Impl.JetDefragment(sesid, dbid, tableName, ref passes, ref seconds, grbit));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000E570 File Offset: 0x0000C770
		public static JET_wrn JetDefragment2(JET_SESID sesid, JET_DBID dbid, string tableName, ref int passes, ref int seconds, JET_CALLBACK callback, DefragGrbit grbit)
		{
			return Api.Check(Api.Impl.JetDefragment2(sesid, dbid, tableName, ref passes, ref seconds, callback, grbit));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000E58B File Offset: 0x0000C78B
		public static JET_wrn JetIdle(JET_SESID sesid, IdleGrbit grbit)
		{
			return Api.Check(Api.Impl.JetIdle(sesid, grbit));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000E59E File Offset: 0x0000C79E
		public static void JetFreeBuffer(IntPtr buffer)
		{
			Api.Check(Api.Impl.JetFreeBuffer(buffer));
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000E5B1 File Offset: 0x0000C7B1
		internal static JET_wrn Check(int err)
		{
			if (err < 0)
			{
				throw Api.CreateErrorException(err);
			}
			return (JET_wrn)err;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
		private static Exception CreateErrorException(int err)
		{
			Api.ErrorHandler handleError = Api.HandleError;
			if (handleError != null)
			{
				handleError((JET_err)err);
			}
			return EsentExceptionHelper.JetErrToException((JET_err)err);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		public static bool TryGetLock(JET_SESID sesid, JET_TABLEID tableid, GetLockGrbit grbit)
		{
			JET_err jet_err = (JET_err)Api.Impl.JetGetLock(sesid, tableid, grbit);
			if (JET_err.WriteConflict == jet_err)
			{
				return false;
			}
			Api.Check((int)jet_err);
			return true;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000E618 File Offset: 0x0000C818
		public unsafe static JET_wrn JetSetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] data, int dataSize, int dataOffset, SetColumnGrbit grbit, JET_SETINFO setinfo)
		{
			if (dataOffset < 0 || (data != null && dataSize != 0 && dataOffset >= data.Length) || (data == null && dataOffset != 0))
			{
				throw new ArgumentOutOfRangeException("dataOffset", dataOffset, "must be inside the data buffer");
			}
			if (data != null && dataSize > checked(data.Length - dataOffset) && SetColumnGrbit.SizeLV != (grbit & SetColumnGrbit.SizeLV))
			{
				throw new ArgumentOutOfRangeException("dataSize", dataSize, "cannot be greater than the length of the data (unless the SizeLV option is used)");
			}
			fixed (byte* ptr = data)
			{
				return Api.JetSetColumn(sesid, tableid, columnid, new IntPtr((void*)((byte*)ptr + dataOffset)), dataSize, grbit, setinfo);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		public unsafe static JET_wrn JetRetrieveColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] data, int dataSize, int dataOffset, out int actualDataSize, RetrieveColumnGrbit grbit, JET_RETINFO retinfo)
		{
			if (dataOffset < 0 || (data != null && dataSize != 0 && dataOffset >= data.Length) || (data == null && dataOffset != 0))
			{
				throw new ArgumentOutOfRangeException("dataOffset", dataOffset, "must be inside the data buffer");
			}
			if ((data == null && dataSize > 0) || (data != null && dataSize > data.Length))
			{
				throw new ArgumentOutOfRangeException("dataSize", dataSize, "cannot be greater than the length of the data");
			}
			fixed (byte* ptr = data)
			{
				return Api.JetRetrieveColumn(sesid, tableid, columnid, new IntPtr((void*)((byte*)ptr + dataOffset)), dataSize, out actualDataSize, grbit, retinfo);
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000E750 File Offset: 0x0000C950
		internal static JET_wrn JetRetrieveColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, IntPtr data, int dataSize, out int actualDataSize, RetrieveColumnGrbit grbit, JET_RETINFO retinfo)
		{
			return Api.Check(Api.Impl.JetRetrieveColumn(sesid, tableid, columnid, data, dataSize, out actualDataSize, grbit, retinfo));
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000E778 File Offset: 0x0000C978
		internal static void JetMakeKey(JET_SESID sesid, JET_TABLEID tableid, IntPtr data, int dataSize, MakeKeyGrbit grbit)
		{
			Api.Check(Api.Impl.JetMakeKey(sesid, tableid, data, dataSize, grbit));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000E790 File Offset: 0x0000C990
		internal static JET_wrn JetSetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, IntPtr data, int dataSize, SetColumnGrbit grbit, JET_SETINFO setinfo)
		{
			return Api.Check(Api.Impl.JetSetColumn(sesid, tableid, columnid, data, dataSize, grbit, setinfo));
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000E7AB File Offset: 0x0000C9AB
		public static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, byte[] data, MakeKeyGrbit grbit)
		{
			if (data == null)
			{
				Api.JetMakeKey(sesid, tableid, null, 0, grbit);
				return;
			}
			if (data.Length == 0)
			{
				Api.JetMakeKey(sesid, tableid, data, data.Length, grbit | MakeKeyGrbit.KeyDataZeroLength);
				return;
			}
			Api.JetMakeKey(sesid, tableid, data, data.Length, grbit);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, string data, Encoding encoding, MakeKeyGrbit grbit)
		{
			Api.CheckEncodingIsValid(encoding);
			if (data == null)
			{
				Api.JetMakeKey(sesid, tableid, null, 0, grbit);
				return;
			}
			if (data.Length == 0)
			{
				Api.JetMakeKey(sesid, tableid, null, 0, grbit | MakeKeyGrbit.KeyDataZeroLength);
				return;
			}
			if (Encoding.Unicode == encoding)
			{
				fixed (char* value = data)
				{
					Api.JetMakeKey(sesid, tableid, new IntPtr((void*)value), checked(data.Length * 2), grbit);
				}
				return;
			}
			byte[] array = null;
			try
			{
				array = Caches.ColumnCache.Allocate();
				int bytes;
				try
				{
					fixed (char* chars = data)
					{
						try
						{
							fixed (byte* ptr = array)
							{
								bytes = encoding.GetBytes(chars, data.Length, ptr, array.Length);
							}
						}
						finally
						{
							byte* ptr = null;
						}
					}
				}
				finally
				{
					string text = null;
				}
				Api.JetMakeKey(sesid, tableid, array, bytes, grbit);
			}
			finally
			{
				if (array != null)
				{
					Caches.ColumnCache.Free(ref array);
				}
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		public static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, bool data, MakeKeyGrbit grbit)
		{
			byte data2 = data ? byte.MaxValue : 0;
			Api.MakeKey(sesid, tableid, data2, grbit);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000E90C File Offset: 0x0000CB0C
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, byte data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 1, grbit);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000E930 File Offset: 0x0000CB30
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, short data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 2, grbit);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000E954 File Offset: 0x0000CB54
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, int data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 4, grbit);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000E978 File Offset: 0x0000CB78
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, long data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 8, grbit);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000E99C File Offset: 0x0000CB9C
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, Guid data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 16, grbit);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000E9BE File Offset: 0x0000CBBE
		public static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, DateTime data, MakeKeyGrbit grbit)
		{
			Api.MakeKey(sesid, tableid, data.ToOADate(), grbit);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, float data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 4, grbit);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, double data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 8, grbit);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000EA18 File Offset: 0x0000CC18
		[CLSCompliant(false)]
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, ushort data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 2, grbit);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		[CLSCompliant(false)]
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, uint data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 4, grbit);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000EA60 File Offset: 0x0000CC60
		[CLSCompliant(false)]
		public unsafe static void MakeKey(JET_SESID sesid, JET_TABLEID tableid, ulong data, MakeKeyGrbit grbit)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetMakeKey(sesid, tableid, data2, 8, grbit);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public static bool TryOpenTable(JET_SESID sesid, JET_DBID dbid, string tablename, OpenTableGrbit grbit, out JET_TABLEID tableid)
		{
			JET_err jet_err = (JET_err)Api.Impl.JetOpenTable(sesid, dbid, tablename, null, 0, grbit, out tableid);
			if (JET_err.ObjectNotFound == jet_err)
			{
				return false;
			}
			Api.Check((int)jet_err);
			return true;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		public static IDictionary<string, JET_COLUMNID> GetColumnDictionary(JET_SESID sesid, JET_TABLEID tableid)
		{
			JET_COLUMNLIST jet_COLUMNLIST;
			Api.JetGetTableColumnInfo(sesid, tableid, string.Empty, out jet_COLUMNLIST);
			Encoding encoding = EsentVersion.SupportsVistaFeatures ? Encoding.Unicode : LibraryHelpers.EncodingASCII;
			IDictionary<string, JET_COLUMNID> result;
			try
			{
				Dictionary<string, JET_COLUMNID> dictionary = new Dictionary<string, JET_COLUMNID>(jet_COLUMNLIST.cRecord, StringComparer.OrdinalIgnoreCase);
				if (jet_COLUMNLIST.cRecord > 0 && Api.TryMoveFirst(sesid, jet_COLUMNLIST.tableid))
				{
					do
					{
						string text = Api.RetrieveColumnAsString(sesid, jet_COLUMNLIST.tableid, jet_COLUMNLIST.columnidcolumnname, encoding, RetrieveColumnGrbit.None);
						text = StringCache.TryToIntern(text);
						uint value = Api.RetrieveColumnAsUInt32(sesid, jet_COLUMNLIST.tableid, jet_COLUMNLIST.columnidcolumnid).Value;
						JET_COLUMNID value2 = new JET_COLUMNID
						{
							Value = value
						};
						dictionary.Add(text, value2);
					}
					while (Api.TryMoveNext(sesid, jet_COLUMNLIST.tableid));
				}
				result = dictionary;
			}
			finally
			{
				Api.JetCloseTable(sesid, jet_COLUMNLIST.tableid);
			}
			return result;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000EB98 File Offset: 0x0000CD98
		public static JET_COLUMNID GetTableColumnid(JET_SESID sesid, JET_TABLEID tableid, string columnName)
		{
			JET_COLUMNDEF jet_COLUMNDEF;
			Api.JetGetTableColumnInfo(sesid, tableid, columnName, out jet_COLUMNDEF);
			return jet_COLUMNDEF.columnid;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public static IEnumerable<ColumnInfo> GetTableColumns(JET_SESID sesid, JET_TABLEID tableid)
		{
			return new GenericEnumerable<ColumnInfo>(() => new TableidColumnInfoEnumerator(sesid, tableid));
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000EC24 File Offset: 0x0000CE24
		public static IEnumerable<ColumnInfo> GetTableColumns(JET_SESID sesid, JET_DBID dbid, string tablename)
		{
			if (tablename == null)
			{
				throw new ArgumentNullException("tablename");
			}
			return new GenericEnumerable<ColumnInfo>(() => new TableColumnInfoEnumerator(sesid, dbid, tablename));
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public static IEnumerable<IndexInfo> GetTableIndexes(JET_SESID sesid, JET_TABLEID tableid)
		{
			return new GenericEnumerable<IndexInfo>(() => new TableidIndexInfoEnumerator(sesid, tableid));
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		public static IEnumerable<IndexInfo> GetTableIndexes(JET_SESID sesid, JET_DBID dbid, string tablename)
		{
			if (tablename == null)
			{
				throw new ArgumentNullException("tablename");
			}
			return new GenericEnumerable<IndexInfo>(() => new TableIndexInfoEnumerator(sesid, dbid, tablename));
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000ED48 File Offset: 0x0000CF48
		public static IEnumerable<string> GetTableNames(JET_SESID sesid, JET_DBID dbid)
		{
			return new GenericEnumerable<string>(() => new TableNameEnumerator(sesid, dbid));
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public static bool TryJetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out JET_INDEXID result, JET_IdxInfo infoLevel)
		{
			int num = Api.Impl.JetGetTableIndexInfo(sesid, tableid, indexname, out result, infoLevel);
			if (num == -1404)
			{
				return false;
			}
			Api.Check(num);
			return true;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		public static void MoveBeforeFirst(JET_SESID sesid, JET_TABLEID tableid)
		{
			Api.TryMoveFirst(sesid, tableid);
			Api.TryMovePrevious(sesid, tableid);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000EDBE File Offset: 0x0000CFBE
		public static void MoveAfterLast(JET_SESID sesid, JET_TABLEID tableid)
		{
			Api.TryMoveLast(sesid, tableid);
			Api.TryMoveNext(sesid, tableid);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public static bool TryMove(JET_SESID sesid, JET_TABLEID tableid, JET_Move move, MoveGrbit grbit)
		{
			JET_err jet_err = (JET_err)Api.Impl.JetMove(sesid, tableid, (int)move, grbit);
			if (JET_err.NoCurrentRecord == jet_err)
			{
				return false;
			}
			Api.Check((int)jet_err);
			return true;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000EDFE File Offset: 0x0000CFFE
		public static bool TryMoveFirst(JET_SESID sesid, JET_TABLEID tableid)
		{
			return Api.TryMove(sesid, tableid, JET_Move.First, MoveGrbit.None);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000EE0D File Offset: 0x0000D00D
		public static bool TryMoveLast(JET_SESID sesid, JET_TABLEID tableid)
		{
			return Api.TryMove(sesid, tableid, JET_Move.Last, MoveGrbit.None);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public static bool TryMoveNext(JET_SESID sesid, JET_TABLEID tableid)
		{
			return Api.TryMove(sesid, tableid, JET_Move.Next, MoveGrbit.None);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000EE27 File Offset: 0x0000D027
		public static bool TryMovePrevious(JET_SESID sesid, JET_TABLEID tableid)
		{
			return Api.TryMove(sesid, tableid, JET_Move.Previous, MoveGrbit.None);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000EE34 File Offset: 0x0000D034
		public static bool TrySeek(JET_SESID sesid, JET_TABLEID tableid, SeekGrbit grbit)
		{
			JET_err jet_err = (JET_err)Api.Impl.JetSeek(sesid, tableid, grbit);
			if (JET_err.RecordNotFound == jet_err)
			{
				return false;
			}
			Api.Check((int)jet_err);
			return true;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000EE64 File Offset: 0x0000D064
		public static bool TrySetIndexRange(JET_SESID sesid, JET_TABLEID tableid, SetIndexRangeGrbit grbit)
		{
			JET_err jet_err = (JET_err)Api.Impl.JetSetIndexRange(sesid, tableid, grbit);
			if (JET_err.NoCurrentRecord == jet_err)
			{
				return false;
			}
			Api.Check((int)jet_err);
			return true;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000EE94 File Offset: 0x0000D094
		public static void ResetIndexRange(JET_SESID sesid, JET_TABLEID tableid)
		{
			JET_err jet_err = (JET_err)Api.Impl.JetSetIndexRange(sesid, tableid, SetIndexRangeGrbit.RangeRemove);
			if (JET_err.InvalidOperation == jet_err)
			{
				return;
			}
			Api.Check((int)jet_err);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		public static IEnumerable<byte[]> IntersectIndexes(JET_SESID sesid, params JET_TABLEID[] tableids)
		{
			if (tableids == null)
			{
				throw new ArgumentNullException("tableids");
			}
			JET_INDEXRANGE[] ranges = new JET_INDEXRANGE[tableids.Length];
			for (int i = 0; i < tableids.Length; i++)
			{
				ranges[i] = new JET_INDEXRANGE
				{
					tableid = tableids[i]
				};
			}
			return new GenericEnumerable<byte[]>(() => new IntersectIndexesEnumerator(sesid, ranges));
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000EF52 File Offset: 0x0000D152
		[Obsolete("Use the overload that takes a JET_IdxInfo parameter, passing in JET_IdxInfo.List")]
		public static void JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string ignored, out JET_INDEXLIST indexlist)
		{
			Api.JetGetIndexInfo(sesid, dbid, tablename, ignored, out indexlist, JET_IdxInfo.List);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000EF60 File Offset: 0x0000D160
		[Obsolete("Use the overload that takes a JET_IdxInfo parameter, passing in JET_IdxInfo.List")]
		public static void JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out JET_INDEXLIST indexlist)
		{
			Api.JetGetTableIndexInfo(sesid, tableid, indexname, out indexlist, JET_IdxInfo.List);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000EF6C File Offset: 0x0000D16C
		public static byte[] GetBookmark(JET_SESID sesid, JET_TABLEID tableid)
		{
			byte[] array = null;
			byte[] result;
			try
			{
				array = Caches.BookmarkCache.Allocate();
				int length;
				Api.JetGetBookmark(sesid, tableid, array, array.Length, out length);
				result = MemoryCache.Duplicate(array, length);
			}
			finally
			{
				if (array != null)
				{
					Caches.BookmarkCache.Free(ref array);
				}
			}
			return result;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public static byte[] GetSecondaryBookmark(JET_SESID sesid, JET_TABLEID tableid, out byte[] primaryBookmark)
		{
			byte[] array = null;
			byte[] array2 = null;
			primaryBookmark = null;
			byte[] result;
			try
			{
				array = Caches.BookmarkCache.Allocate();
				array2 = Caches.SecondaryBookmarkCache.Allocate();
				int length;
				int length2;
				Api.JetGetSecondaryIndexBookmark(sesid, tableid, array2, array2.Length, out length, array, array.Length, out length2, GetSecondaryIndexBookmarkGrbit.None);
				primaryBookmark = MemoryCache.Duplicate(array, length2);
				result = MemoryCache.Duplicate(array2, length);
			}
			finally
			{
				if (array != null)
				{
					Caches.BookmarkCache.Free(ref array);
				}
				if (array2 != null)
				{
					Caches.BookmarkCache.Free(ref array2);
				}
			}
			return result;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000F044 File Offset: 0x0000D244
		public static byte[] RetrieveKey(JET_SESID sesid, JET_TABLEID tableid, RetrieveKeyGrbit grbit)
		{
			byte[] array = null;
			byte[] result;
			try
			{
				array = Caches.BookmarkCache.Allocate();
				int length;
				Api.JetRetrieveKey(sesid, tableid, array, array.Length, out length, grbit);
				result = MemoryCache.Duplicate(array, length);
			}
			finally
			{
				if (array != null)
				{
					Caches.BookmarkCache.Free(ref array);
				}
			}
			return result;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000F098 File Offset: 0x0000D298
		public static int? RetrieveColumnSize(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnSize(sesid, tableid, columnid, 1, RetrieveColumnGrbit.None);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		public static int? RetrieveColumnSize(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, int itagSequence, RetrieveColumnGrbit grbit)
		{
			JET_RETINFO retinfo = new JET_RETINFO
			{
				itagSequence = itagSequence
			};
			int value;
			JET_wrn jet_wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, null, 0, out value, grbit, retinfo);
			if (JET_wrn.ColumnNull == jet_wrn)
			{
				return null;
			}
			return new int?(value);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		public static byte[] RetrieveColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit, JET_RETINFO retinfo)
		{
			byte[] array = null;
			byte[] array2;
			try
			{
				array = Caches.ColumnCache.Allocate();
				array2 = array;
				int num;
				JET_wrn jet_wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, array2, array2.Length, out num, grbit, retinfo);
				if (JET_wrn.ColumnNull == jet_wrn)
				{
					array2 = null;
				}
				else if (jet_wrn == JET_wrn.Success)
				{
					array2 = MemoryCache.Duplicate(array2, num);
				}
				else
				{
					array2 = new byte[num];
					jet_wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, array2, array2.Length, out num, grbit, retinfo);
					if (jet_wrn != JET_wrn.Success)
					{
						string message = string.Format(CultureInfo.CurrentCulture, "Column size changed from {0} to {1}. The record was probably updated by another thread.", new object[]
						{
							array2.Length,
							num
						});
						throw new InvalidOperationException(message);
					}
				}
			}
			finally
			{
				if (array != null)
				{
					Caches.ColumnCache.Free(ref array);
				}
			}
			return array2;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		public static byte[] RetrieveColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumn(sesid, tableid, columnid, RetrieveColumnGrbit.None, null);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		public static string RetrieveColumnAsString(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsString(sesid, tableid, columnid, Encoding.Unicode, RetrieveColumnGrbit.None);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		public static string RetrieveColumnAsString(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, Encoding encoding)
		{
			return Api.RetrieveColumnAsString(sesid, tableid, columnid, encoding, RetrieveColumnGrbit.None);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public static string RetrieveColumnAsString(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, Encoding encoding, RetrieveColumnGrbit grbit)
		{
			if (Encoding.Unicode == encoding)
			{
				return Api.RetrieveUnicodeString(sesid, tableid, columnid, grbit);
			}
			byte[] array = null;
			string @string;
			try
			{
				array = Caches.ColumnCache.Allocate();
				byte[] array2 = array;
				int num;
				JET_wrn jet_wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, array2, array2.Length, out num, grbit, null);
				if (JET_wrn.ColumnNull == jet_wrn)
				{
					return null;
				}
				if (JET_wrn.BufferTruncated == jet_wrn)
				{
					array2 = new byte[num];
					jet_wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, array2, array2.Length, out num, grbit, null);
					if (JET_wrn.BufferTruncated == jet_wrn)
					{
						string message = string.Format(CultureInfo.CurrentCulture, "Column size changed from {0} to {1}. The record was probably updated by another thread.", new object[]
						{
							array2.Length,
							num
						});
						throw new InvalidOperationException(message);
					}
				}
				Encoding encoding2 = (encoding is ASCIIEncoding) ? Api.AsciiDecoder : encoding;
				@string = encoding2.GetString(array2, 0, num);
			}
			finally
			{
				if (array != null)
				{
					Caches.ColumnCache.Free(ref array);
				}
			}
			return @string;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public static short? RetrieveColumnAsInt16(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsInt16(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public unsafe static short? RetrieveColumnAsInt16(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			short data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 2, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<short>(data2, 2, wrn, actualDataSize);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000F30B File Offset: 0x0000D50B
		public static int? RetrieveColumnAsInt32(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsInt32(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000F318 File Offset: 0x0000D518
		public unsafe static int? RetrieveColumnAsInt32(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			int data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 4, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<int>(data2, 4, wrn, actualDataSize);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000F347 File Offset: 0x0000D547
		public static long? RetrieveColumnAsInt64(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsInt64(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000F354 File Offset: 0x0000D554
		public unsafe static long? RetrieveColumnAsInt64(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			long data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 8, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<long>(data2, 8, wrn, actualDataSize);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000F383 File Offset: 0x0000D583
		public static float? RetrieveColumnAsFloat(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsFloat(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000F390 File Offset: 0x0000D590
		public unsafe static float? RetrieveColumnAsFloat(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			float data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 4, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<float>(data2, 4, wrn, actualDataSize);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000F3BF File Offset: 0x0000D5BF
		public static double? RetrieveColumnAsDouble(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsDouble(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		public unsafe static double? RetrieveColumnAsDouble(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			double data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 8, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<double>(data2, 8, wrn, actualDataSize);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000F3FB File Offset: 0x0000D5FB
		public static bool? RetrieveColumnAsBoolean(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsBoolean(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000F408 File Offset: 0x0000D608
		public static bool? RetrieveColumnAsBoolean(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			byte? b = Api.RetrieveColumnAsByte(sesid, tableid, columnid, grbit);
			if (b != null)
			{
				return new bool?(0 != b.Value);
			}
			return null;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000F444 File Offset: 0x0000D644
		public static byte? RetrieveColumnAsByte(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsByte(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000F450 File Offset: 0x0000D650
		public unsafe static byte? RetrieveColumnAsByte(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			byte data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 1, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<byte>(data2, 1, wrn, actualDataSize);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000F47F File Offset: 0x0000D67F
		public static Guid? RetrieveColumnAsGuid(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsGuid(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000F48C File Offset: 0x0000D68C
		public unsafe static Guid? RetrieveColumnAsGuid(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			Guid data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 16, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<Guid>(data2, 16, wrn, actualDataSize);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000F4BD File Offset: 0x0000D6BD
		public static DateTime? RetrieveColumnAsDateTime(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsDateTime(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		public static DateTime? RetrieveColumnAsDateTime(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			double? num = Api.RetrieveColumnAsDouble(sesid, tableid, columnid, grbit);
			if (num != null)
			{
				return new DateTime?(Conversions.ConvertDoubleToDateTime(num.Value));
			}
			return null;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000F503 File Offset: 0x0000D703
		[CLSCompliant(false)]
		public static ushort? RetrieveColumnAsUInt16(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsUInt16(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000F510 File Offset: 0x0000D710
		[CLSCompliant(false)]
		public unsafe static ushort? RetrieveColumnAsUInt16(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			ushort data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 2, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<ushort>(data2, 2, wrn, actualDataSize);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000F53F File Offset: 0x0000D73F
		[CLSCompliant(false)]
		public static uint? RetrieveColumnAsUInt32(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsUInt32(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000F54C File Offset: 0x0000D74C
		[CLSCompliant(false)]
		public unsafe static uint? RetrieveColumnAsUInt32(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			uint data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 4, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<uint>(data2, 4, wrn, actualDataSize);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000F57B File Offset: 0x0000D77B
		[CLSCompliant(false)]
		public static ulong? RetrieveColumnAsUInt64(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.RetrieveColumnAsUInt64(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000F588 File Offset: 0x0000D788
		[CLSCompliant(false)]
		public unsafe static ulong? RetrieveColumnAsUInt64(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			ulong data2;
			IntPtr data = new IntPtr((void*)(&data2));
			int actualDataSize;
			JET_wrn wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, data, 8, out actualDataSize, grbit, null);
			return Api.CreateReturnValue<ulong>(data2, 8, wrn, actualDataSize);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000F5B7 File Offset: 0x0000D7B7
		public static object DeserializeObjectFromColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid)
		{
			return Api.DeserializeObjectFromColumn(sesid, tableid, columnid, RetrieveColumnGrbit.None);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		public static object DeserializeObjectFromColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			int num;
			if (JET_wrn.ColumnNull == Api.JetRetrieveColumn(sesid, tableid, columnid, null, 0, out num, grbit, null))
			{
				return null;
			}
			object result;
			using (ColumnStream columnStream = new ColumnStream(sesid, tableid, columnid))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				result = binaryFormatter.Deserialize(columnStream);
			}
			return result;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000F61C File Offset: 0x0000D81C
		public static void RetrieveColumns(JET_SESID sesid, JET_TABLEID tableid, params ColumnValue[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length == 0)
			{
				throw new ArgumentOutOfRangeException("values", values.Length, "must have at least one value");
			}
			ColumnValue.RetrieveColumns(sesid, tableid, values);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000F654 File Offset: 0x0000D854
		private static T? CreateReturnValue<T>(T data, int dataSize, JET_wrn wrn, int actualDataSize) where T : struct
		{
			if (JET_wrn.ColumnNull == wrn)
			{
				return null;
			}
			Api.CheckDataSize(dataSize, actualDataSize);
			return new T?(data);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000F680 File Offset: 0x0000D880
		private static void CheckDataSize(int expectedDataSize, int actualDataSize)
		{
			if (actualDataSize < expectedDataSize)
			{
				throw new EsentInvalidColumnException();
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000F68C File Offset: 0x0000D88C
		private unsafe static int PinColumnsAndRetrieve(JET_SESID sesid, JET_TABLEID tableid, NATIVE_RETRIEVECOLUMN* nativeretrievecolumns, IList<JET_RETRIEVECOLUMN> retrievecolumns, int numColumns, int i)
		{
			fixed (byte* pvData = retrievecolumns[i].pvData)
			{
				do
				{
					retrievecolumns[i].CheckDataSize();
					retrievecolumns[i].GetNativeRetrievecolumn(ref nativeretrievecolumns[i]);
					nativeretrievecolumns[i].pvData = new IntPtr((void*)((byte*)pvData + retrievecolumns[i].ibData));
					i++;
				}
				while (i < numColumns && retrievecolumns[i].pvData == retrievecolumns[i - 1].pvData);
				return (i == numColumns) ? Api.Impl.JetRetrieveColumns(sesid, tableid, nativeretrievecolumns, numColumns) : Api.PinColumnsAndRetrieve(sesid, tableid, nativeretrievecolumns, retrievecolumns, numColumns, i);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000F75C File Offset: 0x0000D95C
		private unsafe static string RetrieveUnicodeString(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, RetrieveColumnGrbit grbit)
		{
			char* value = stackalloc char[checked(unchecked((UIntPtr)512) * 2)];
			int num;
			JET_wrn jet_wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, new IntPtr((void*)value), 1024, out num, grbit, null);
			if (JET_wrn.ColumnNull == jet_wrn)
			{
				return null;
			}
			if (jet_wrn == JET_wrn.Success)
			{
				return new string(value, 0, num / 2);
			}
			string text = new string('\0', num / 2);
			fixed (char* value2 = text)
			{
				int num2;
				jet_wrn = Api.JetRetrieveColumn(sesid, tableid, columnid, new IntPtr((void*)value2), num, out num2, grbit, null);
				if (JET_wrn.BufferTruncated == jet_wrn)
				{
					string message = string.Format(CultureInfo.CurrentCulture, "Column size changed from {0} to {1}. The record was probably updated by another thread.", new object[]
					{
						num,
						num2
					});
					throw new InvalidOperationException(message);
				}
			}
			return text;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000F818 File Offset: 0x0000DA18
		public static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, string data, Encoding encoding)
		{
			Api.SetColumn(sesid, tableid, columnid, data, encoding, SetColumnGrbit.None);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000F828 File Offset: 0x0000DA28
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, string data, Encoding encoding, SetColumnGrbit grbit)
		{
			Api.CheckEncodingIsValid(encoding);
			if (data == null)
			{
				Api.JetSetColumn(sesid, tableid, columnid, null, 0, grbit, null);
				return;
			}
			if (data.Length == 0)
			{
				Api.JetSetColumn(sesid, tableid, columnid, null, 0, grbit | SetColumnGrbit.ZeroLength, null);
				return;
			}
			if (Encoding.Unicode == encoding)
			{
				fixed (char* value = data)
				{
					Api.JetSetColumn(sesid, tableid, columnid, new IntPtr((void*)value), checked(data.Length * 2), grbit, null);
				}
				return;
			}
			if (encoding.GetMaxByteCount(data.Length) <= Caches.ColumnCache.BufferSize)
			{
				byte[] array = null;
				try
				{
					array = Caches.ColumnCache.Allocate();
					try
					{
						fixed (char* chars = data)
						{
							try
							{
								fixed (byte* ptr = array)
								{
									int bytes = encoding.GetBytes(chars, data.Length, ptr, array.Length);
									Api.JetSetColumn(sesid, tableid, columnid, new IntPtr((void*)ptr), bytes, grbit, null);
								}
							}
							finally
							{
								byte* ptr = null;
							}
						}
					}
					finally
					{
						string text = null;
					}
					return;
				}
				finally
				{
					if (array != null)
					{
						Caches.ColumnCache.Free(ref array);
					}
				}
			}
			byte[] bytes2 = encoding.GetBytes(data);
			Api.JetSetColumn(sesid, tableid, columnid, bytes2, bytes2.Length, grbit, null);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000F97C File Offset: 0x0000DB7C
		public static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] data)
		{
			Api.SetColumn(sesid, tableid, columnid, data, SetColumnGrbit.None);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000F988 File Offset: 0x0000DB88
		public static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] data, SetColumnGrbit grbit)
		{
			if (data != null && data.Length == 0)
			{
				grbit |= SetColumnGrbit.ZeroLength;
			}
			int dataSize = (data == null) ? 0 : data.Length;
			Api.JetSetColumn(sesid, tableid, columnid, data, dataSize, grbit, null);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		public static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, bool data)
		{
			byte data2 = data ? byte.MaxValue : 0;
			Api.SetColumn(sesid, tableid, columnid, data2);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 1, SetColumnGrbit.None, null);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000FA04 File Offset: 0x0000DC04
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, short data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 2, SetColumnGrbit.None, null);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000FA28 File Offset: 0x0000DC28
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, int data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 4, SetColumnGrbit.None, null);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, long data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 8, SetColumnGrbit.None, null);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000FA70 File Offset: 0x0000DC70
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, Guid data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 16, SetColumnGrbit.None, null);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000FA95 File Offset: 0x0000DC95
		public static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, DateTime data)
		{
			Api.SetColumn(sesid, tableid, columnid, data.ToOADate());
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000FAA8 File Offset: 0x0000DCA8
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, float data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 4, SetColumnGrbit.None, null);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000FACC File Offset: 0x0000DCCC
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, double data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 8, SetColumnGrbit.None, null);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		public static int EscrowUpdate(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, int delta)
		{
			byte[] array = new byte[4];
			int num;
			Api.JetEscrowUpdate(sesid, tableid, columnid, BitConverter.GetBytes(delta), 4, array, array.Length, out num, EscrowUpdateGrbit.None);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000FB24 File Offset: 0x0000DD24
		[CLSCompliant(false)]
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, ushort data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 2, SetColumnGrbit.None, null);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000FB48 File Offset: 0x0000DD48
		[CLSCompliant(false)]
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, uint data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 4, SetColumnGrbit.None, null);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		[CLSCompliant(false)]
		public unsafe static void SetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, ulong data)
		{
			IntPtr data2 = new IntPtr((void*)(&data));
			Api.JetSetColumn(sesid, tableid, columnid, data2, 8, SetColumnGrbit.None, null);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000FB90 File Offset: 0x0000DD90
		public static void SerializeObjectToColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, object value)
		{
			if (value == null)
			{
				Api.SetColumn(sesid, tableid, columnid, null);
				return;
			}
			using (ColumnStream columnStream = new ColumnStream(sesid, tableid, columnid))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter
				{
					Context = new StreamingContext(StreamingContextStates.Persistence)
				};
				binaryFormatter.Serialize(columnStream, value);
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		public unsafe static void SetColumns(JET_SESID sesid, JET_TABLEID tableid, params ColumnValue[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length == 0)
			{
				throw new ArgumentOutOfRangeException("values", values.Length, "must have at least one value");
			}
			NATIVE_SETCOLUMN* nativeColumns = stackalloc NATIVE_SETCOLUMN[checked(unchecked((UIntPtr)values.Length) * (UIntPtr)sizeof(NATIVE_SETCOLUMN))];
			Api.Check(values[0].SetColumns(sesid, tableid, values, nativeColumns, 0));
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000FC48 File Offset: 0x0000DE48
		private static void CheckEncodingIsValid(Encoding encoding)
		{
			int codePage = encoding.CodePage;
			if (20127 != codePage && 1200 != codePage)
			{
				throw new ArgumentOutOfRangeException("encoding", codePage, "Invalid Encoding type. Only ASCII and Unicode encodings are allowed");
			}
		}

		// Token: 0x04000304 RID: 772
		private static readonly Encoding AsciiDecoder = new UTF8Encoding(false, true);

		// Token: 0x02000092 RID: 146
		// (Invoke) Token: 0x060006D3 RID: 1747
		internal delegate void ErrorHandler(JET_err error);
	}
}
