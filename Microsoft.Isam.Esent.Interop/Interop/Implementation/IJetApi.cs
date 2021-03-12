using System;
using Microsoft.Isam.Esent.Interop.Server2003;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Isam.Esent.Interop.Vista;
using Microsoft.Isam.Esent.Interop.Windows7;
using Microsoft.Isam.Esent.Interop.Windows8;

namespace Microsoft.Isam.Esent.Interop.Implementation
{
	// Token: 0x02000024 RID: 36
	internal interface IJetApi
	{
		// Token: 0x0600008E RID: 142
		int JetTracing(JET_traceop operation, JET_tracetag tag, bool value);

		// Token: 0x0600008F RID: 143
		int JetTracing(JET_traceop operation, JET_tracetag tag, JET_DBID value);

		// Token: 0x06000090 RID: 144
		int JetTracing(JET_traceop operation, JET_tracetag tag, int value);

		// Token: 0x06000091 RID: 145
		int JetTracing(JET_traceop operation, JET_tracetag tag, JET_PFNTRACEREGISTER callback);

		// Token: 0x06000092 RID: 146
		int JetTracing(JET_traceop operation, JET_tracetag tag, JET_PFNTRACEEMIT callback);

		// Token: 0x06000093 RID: 147
		int JetSetResourceParam(JET_INSTANCE instance, JET_resoper resoper, JET_resid resid, long longParameter);

		// Token: 0x06000094 RID: 148
		int JetGetResourceParam(JET_INSTANCE instance, JET_resoper resoper, JET_resid resid, out long paramValue);

		// Token: 0x06000095 RID: 149
		int JetConsumeLogData(JET_INSTANCE instance, JET_EMITDATACTX emitLogDataCtx, byte[] logDataBuf, int logDataStartOffset, int logDataLength, ShadowLogConsumeGrbit grbit);

		// Token: 0x06000096 RID: 150
		int JetGetLogFileInfo(string logFileName, out JET_LOGINFOMISC info, JET_LogInfo infoLevel);

		// Token: 0x06000097 RID: 151
		int JetGetPageInfo2(byte[] bytesPages, int bytesPagesLength, JET_PAGEINFO[] pageInfos, PageInfoGrbit grbit, JET_PageInfo infoLevel);

		// Token: 0x06000098 RID: 152
		int JetGetInstanceMiscInfo(JET_INSTANCE instance, out JET_CHECKPOINTINFO checkpointInfo, JET_InstanceMiscInfo infoLevel);

		// Token: 0x06000099 RID: 153
		int JetBeginDatabaseIncrementalReseed(JET_INSTANCE instance, string wszDatabase, BeginDatabaseIncrementalReseedGrbit grbit);

		// Token: 0x0600009A RID: 154
		int JetEndDatabaseIncrementalReseed(JET_INSTANCE instance, string wszDatabase, int genMinRequired, int genFirstDivergedLog, int genMaxRequired, EndDatabaseIncrementalReseedGrbit grbit);

		// Token: 0x0600009B RID: 155
		int JetPatchDatabasePages(JET_INSTANCE instance, string databaseFileName, int pgnoStart, int pageCount, byte[] inputData, int dataLength, PatchDatabasePagesGrbit grbit);

		// Token: 0x0600009C RID: 156
		int JetRemoveLogfile(string databaseFileName, string logFileName, RemoveLogfileGrbit grbit);

		// Token: 0x0600009D RID: 157
		int JetGetDatabasePages(JET_SESID sesid, JET_DBID dbid, int pgnoStart, int countPages, byte[] pageBytes, int pageBytesLength, out int pageBytesRead, GetDatabasePagesGrbit grbit);

		// Token: 0x0600009E RID: 158
		int JetDBUtilities(JET_DBUTIL dbutil);

		// Token: 0x0600009F RID: 159
		int JetTestHook(int opcode, ref uint pv);

		// Token: 0x060000A0 RID: 160
		int JetTestHook(int opcode, ref NATIVE_TESTHOOKTESTINJECTION pv);

		// Token: 0x060000A1 RID: 161
		int JetTestHook(int opcode, JET_TESTHOOKEVICTCACHE nativeTestHookEvictCachedPage);

		// Token: 0x060000A2 RID: 162
		int JetTestHook(int opcode, ref IntPtr pv);

		// Token: 0x060000A3 RID: 163
		int JetTestHook(int opcode, ref NATIVE_TESTHOOKTRACETESTMARKER pv);

		// Token: 0x060000A4 RID: 164
		int JetTestHook(int opcode, ref NATIVE_TESTHOOKCORRUPT_DATABASEFILE pv);

		// Token: 0x060000A5 RID: 165
		int JetPrereadTables(JET_SESID sesid, JET_DBID dbid, string[] rgsz, PrereadTablesGrbit grbit);

		// Token: 0x060000A6 RID: 166
		int JetDatabaseScan(JET_SESID sesid, JET_DBID dbid, ref int seconds, int sleepInMsec, JET_CALLBACK callback, DatabaseScanGrbit grbit);

		// Token: 0x060000A7 RID: 167
		int JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, int value);

		// Token: 0x060000A8 RID: 168
		int JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, ref NATIVE_OPERATIONCONTEXT operationContext);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A9 RID: 169
		JetCapabilities Capabilities { get; }

		// Token: 0x060000AA RID: 170
		int JetCreateInstance(out JET_INSTANCE instance, string name);

		// Token: 0x060000AB RID: 171
		int JetCreateInstance2(out JET_INSTANCE instance, string name, string displayName, CreateInstanceGrbit grbit);

		// Token: 0x060000AC RID: 172
		int JetInit(ref JET_INSTANCE instance);

		// Token: 0x060000AD RID: 173
		int JetInit2(ref JET_INSTANCE instance, InitGrbit grbit);

		// Token: 0x060000AE RID: 174
		int JetInit3(ref JET_INSTANCE instance, JET_RSTINFO recoveryOptions, InitGrbit grbit);

		// Token: 0x060000AF RID: 175
		int JetGetInstanceInfo(out int numInstances, out JET_INSTANCE_INFO[] instances);

		// Token: 0x060000B0 RID: 176
		int JetGetInstanceMiscInfo(JET_INSTANCE instance, out JET_SIGNATURE signature, JET_InstanceMiscInfo infoLevel);

		// Token: 0x060000B1 RID: 177
		int JetStopBackupInstance(JET_INSTANCE instance);

		// Token: 0x060000B2 RID: 178
		int JetStopServiceInstance(JET_INSTANCE instance);

		// Token: 0x060000B3 RID: 179
		int JetStopServiceInstance2(JET_INSTANCE instance, StopServiceGrbit grbit);

		// Token: 0x060000B4 RID: 180
		int JetTerm(JET_INSTANCE instance);

		// Token: 0x060000B5 RID: 181
		int JetTerm2(JET_INSTANCE instance, TermGrbit grbit);

		// Token: 0x060000B6 RID: 182
		int JetSetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, IntPtr paramValue, string paramString);

		// Token: 0x060000B7 RID: 183
		int JetSetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, JET_CALLBACK paramValue, string paramString);

		// Token: 0x060000B8 RID: 184
		int JetGetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, ref IntPtr paramValue, out string paramString, int maxParam);

		// Token: 0x060000B9 RID: 185
		int JetGetVersion(JET_SESID sesid, out uint version);

		// Token: 0x060000BA RID: 186
		int JetCreateDatabase(JET_SESID sesid, string database, string connect, out JET_DBID dbid, CreateDatabaseGrbit grbit);

		// Token: 0x060000BB RID: 187
		int JetCreateDatabase2(JET_SESID sesid, string database, int maxPages, out JET_DBID dbid, CreateDatabaseGrbit grbit);

		// Token: 0x060000BC RID: 188
		int JetAttachDatabase(JET_SESID sesid, string database, AttachDatabaseGrbit grbit);

		// Token: 0x060000BD RID: 189
		int JetAttachDatabase2(JET_SESID sesid, string database, int maxPages, AttachDatabaseGrbit grbit);

		// Token: 0x060000BE RID: 190
		int JetOpenDatabase(JET_SESID sesid, string database, string connect, out JET_DBID dbid, OpenDatabaseGrbit grbit);

		// Token: 0x060000BF RID: 191
		int JetCloseDatabase(JET_SESID sesid, JET_DBID dbid, CloseDatabaseGrbit grbit);

		// Token: 0x060000C0 RID: 192
		int JetDetachDatabase(JET_SESID sesid, string database);

		// Token: 0x060000C1 RID: 193
		int JetDetachDatabase2(JET_SESID sesid, string database, DetachDatabaseGrbit grbit);

		// Token: 0x060000C2 RID: 194
		int JetCompact(JET_SESID sesid, string sourceDatabase, string destinationDatabase, JET_PFNSTATUS statusCallback, object ignored, CompactGrbit grbit);

		// Token: 0x060000C3 RID: 195
		int JetGrowDatabase(JET_SESID sesid, JET_DBID dbid, int desiredPages, out int actualPages);

		// Token: 0x060000C4 RID: 196
		int JetSetDatabaseSize(JET_SESID sesid, string database, int desiredPages, out int actualPages);

		// Token: 0x060000C5 RID: 197
		int JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out int value, JET_DbInfo infoLevel);

		// Token: 0x060000C6 RID: 198
		int JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out string value, JET_DbInfo infoLevel);

		// Token: 0x060000C7 RID: 199
		int JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out JET_DBINFOMISC dbinfomisc, JET_DbInfo infoLevel);

		// Token: 0x060000C8 RID: 200
		int JetGetDatabaseFileInfo(string databaseName, out int value, JET_DbInfo infoLevel);

		// Token: 0x060000C9 RID: 201
		int JetGetDatabaseFileInfo(string databaseName, out long value, JET_DbInfo infoLevel);

		// Token: 0x060000CA RID: 202
		int JetGetDatabaseFileInfo(string databaseName, out JET_DBINFOMISC dbinfomisc, JET_DbInfo infoLevel);

		// Token: 0x060000CB RID: 203
		int JetBackupInstance(JET_INSTANCE instance, string destination, BackupGrbit grbit, JET_PFNSTATUS statusCallback);

		// Token: 0x060000CC RID: 204
		int JetRestoreInstance(JET_INSTANCE instance, string source, string destination, JET_PFNSTATUS statusCallback);

		// Token: 0x060000CD RID: 205
		int JetOSSnapshotPrepare(out JET_OSSNAPID snapid, SnapshotPrepareGrbit grbit);

		// Token: 0x060000CE RID: 206
		int JetOSSnapshotPrepareInstance(JET_OSSNAPID snapshot, JET_INSTANCE instance, SnapshotPrepareInstanceGrbit grbit);

		// Token: 0x060000CF RID: 207
		int JetOSSnapshotFreeze(JET_OSSNAPID snapshot, out int numInstances, out JET_INSTANCE_INFO[] instances, SnapshotFreezeGrbit grbit);

		// Token: 0x060000D0 RID: 208
		int JetOSSnapshotGetFreezeInfo(JET_OSSNAPID snapshot, out int numInstances, out JET_INSTANCE_INFO[] instances, SnapshotGetFreezeInfoGrbit grbit);

		// Token: 0x060000D1 RID: 209
		int JetOSSnapshotThaw(JET_OSSNAPID snapid, SnapshotThawGrbit grbit);

		// Token: 0x060000D2 RID: 210
		int JetOSSnapshotTruncateLog(JET_OSSNAPID snapshot, SnapshotTruncateLogGrbit grbit);

		// Token: 0x060000D3 RID: 211
		int JetOSSnapshotTruncateLogInstance(JET_OSSNAPID snapshot, JET_INSTANCE instance, SnapshotTruncateLogGrbit grbit);

		// Token: 0x060000D4 RID: 212
		int JetOSSnapshotEnd(JET_OSSNAPID snapid, SnapshotEndGrbit grbit);

		// Token: 0x060000D5 RID: 213
		int JetOSSnapshotAbort(JET_OSSNAPID snapid, SnapshotAbortGrbit grbit);

		// Token: 0x060000D6 RID: 214
		int JetBeginExternalBackupInstance(JET_INSTANCE instance, BeginExternalBackupGrbit grbit);

		// Token: 0x060000D7 RID: 215
		int JetCloseFileInstance(JET_INSTANCE instance, JET_HANDLE handle);

		// Token: 0x060000D8 RID: 216
		int JetEndExternalBackupInstance(JET_INSTANCE instance);

		// Token: 0x060000D9 RID: 217
		int JetEndExternalBackupInstance2(JET_INSTANCE instance, EndExternalBackupGrbit grbit);

		// Token: 0x060000DA RID: 218
		int JetGetAttachInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars);

		// Token: 0x060000DB RID: 219
		int JetGetLogInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars);

		// Token: 0x060000DC RID: 220
		int JetGetTruncateLogInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars);

		// Token: 0x060000DD RID: 221
		int JetOpenFileInstance(JET_INSTANCE instance, string file, out JET_HANDLE handle, out long fileSizeLow, out long fileSizeHigh);

		// Token: 0x060000DE RID: 222
		int JetReadFileInstance(JET_INSTANCE instance, JET_HANDLE file, byte[] buffer, int bufferSize, out int bytesRead);

		// Token: 0x060000DF RID: 223
		int JetTruncateLogInstance(JET_INSTANCE instance);

		// Token: 0x060000E0 RID: 224
		int JetBeginSession(JET_INSTANCE instance, out JET_SESID sesid, string username, string password);

		// Token: 0x060000E1 RID: 225
		int JetSetSessionContext(JET_SESID sesid, IntPtr context);

		// Token: 0x060000E2 RID: 226
		int JetResetSessionContext(JET_SESID sesid);

		// Token: 0x060000E3 RID: 227
		int JetEndSession(JET_SESID sesid, EndSessionGrbit grbit);

		// Token: 0x060000E4 RID: 228
		int JetDupSession(JET_SESID sesid, out JET_SESID newSesid);

		// Token: 0x060000E5 RID: 229
		int JetGetThreadStats(out JET_THREADSTATS threadstats);

		// Token: 0x060000E6 RID: 230
		int JetOpenTable(JET_SESID sesid, JET_DBID dbid, string tablename, byte[] parameters, int parametersLength, OpenTableGrbit grbit, out JET_TABLEID tableid);

		// Token: 0x060000E7 RID: 231
		int JetCloseTable(JET_SESID sesid, JET_TABLEID tableid);

		// Token: 0x060000E8 RID: 232
		int JetDupCursor(JET_SESID sesid, JET_TABLEID tableid, out JET_TABLEID newTableid, DupCursorGrbit grbit);

		// Token: 0x060000E9 RID: 233
		int JetComputeStats(JET_SESID sesid, JET_TABLEID tableid);

		// Token: 0x060000EA RID: 234
		int JetSetLS(JET_SESID sesid, JET_TABLEID tableid, JET_LS ls, LsGrbit grbit);

		// Token: 0x060000EB RID: 235
		int JetGetLS(JET_SESID sesid, JET_TABLEID tableid, out JET_LS ls, LsGrbit grbit);

		// Token: 0x060000EC RID: 236
		int JetGetCursorInfo(JET_SESID sesid, JET_TABLEID tableid);

		// Token: 0x060000ED RID: 237
		int JetBeginTransaction(JET_SESID sesid);

		// Token: 0x060000EE RID: 238
		int JetBeginTransaction2(JET_SESID sesid, BeginTransactionGrbit grbit);

		// Token: 0x060000EF RID: 239
		int JetBeginTransaction3(JET_SESID sesid, long userTransactionId, BeginTransactionGrbit grbit);

		// Token: 0x060000F0 RID: 240
		int JetCommitTransaction(JET_SESID sesid, CommitTransactionGrbit grbit);

		// Token: 0x060000F1 RID: 241
		int JetRollback(JET_SESID sesid, RollbackTransactionGrbit grbit);

		// Token: 0x060000F2 RID: 242
		int JetCreateTable(JET_SESID sesid, JET_DBID dbid, string table, int pages, int density, out JET_TABLEID tableid);

		// Token: 0x060000F3 RID: 243
		int JetAddColumn(JET_SESID sesid, JET_TABLEID tableid, string column, JET_COLUMNDEF columndef, byte[] defaultValue, int defaultValueSize, out JET_COLUMNID columnid);

		// Token: 0x060000F4 RID: 244
		int JetDeleteColumn(JET_SESID sesid, JET_TABLEID tableid, string column);

		// Token: 0x060000F5 RID: 245
		int JetDeleteColumn2(JET_SESID sesid, JET_TABLEID tableid, string column, DeleteColumnGrbit grbit);

		// Token: 0x060000F6 RID: 246
		int JetDeleteIndex(JET_SESID sesid, JET_TABLEID tableid, string index);

		// Token: 0x060000F7 RID: 247
		int JetDeleteTable(JET_SESID sesid, JET_DBID dbid, string table);

		// Token: 0x060000F8 RID: 248
		int JetCreateIndex(JET_SESID sesid, JET_TABLEID tableid, string indexName, CreateIndexGrbit grbit, string keyDescription, int keyDescriptionLength, int density);

		// Token: 0x060000F9 RID: 249
		int JetCreateIndex2(JET_SESID sesid, JET_TABLEID tableid, JET_INDEXCREATE[] indexcreates, int numIndexCreates);

		// Token: 0x060000FA RID: 250
		int JetOpenTempTable(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids);

		// Token: 0x060000FB RID: 251
		int JetOpenTempTable2(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, int lcid, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids);

		// Token: 0x060000FC RID: 252
		int JetOpenTempTable3(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, JET_UNICODEINDEX unicodeindex, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids);

		// Token: 0x060000FD RID: 253
		int JetOpenTemporaryTable(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable);

		// Token: 0x060000FE RID: 254
		int JetCreateTableColumnIndex3(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate);

		// Token: 0x060000FF RID: 255
		int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string columnName, out JET_COLUMNDEF columndef);

		// Token: 0x06000100 RID: 256
		int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, out JET_COLUMNDEF columndef);

		// Token: 0x06000101 RID: 257
		int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string columnName, out JET_COLUMNBASE columnbase);

		// Token: 0x06000102 RID: 258
		int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, out JET_COLUMNBASE columnbase);

		// Token: 0x06000103 RID: 259
		int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string ignored, ColInfoGrbit grbit, out JET_COLUMNLIST columnlist);

		// Token: 0x06000104 RID: 260
		int JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string columnName, out JET_COLUMNDEF columndef);

		// Token: 0x06000105 RID: 261
		int JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string ignored, out JET_COLUMNLIST columnlist);

		// Token: 0x06000106 RID: 262
		int JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string columnName, out JET_COLUMNBASE columnbase);

		// Token: 0x06000107 RID: 263
		int JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string columnName, JET_COLUMNID columnid, out JET_COLUMNBASE columnbase);

		// Token: 0x06000108 RID: 264
		int JetGetObjectInfo(JET_SESID sesid, JET_DBID dbid, out JET_OBJECTLIST objectlist);

		// Token: 0x06000109 RID: 265
		int JetGetObjectInfo(JET_SESID sesid, JET_DBID dbid, JET_objtyp objtyp, string objectName, out JET_OBJECTINFO objectinfo);

		// Token: 0x0600010A RID: 266
		int JetGetCurrentIndex(JET_SESID sesid, JET_TABLEID tableid, out string indexName, int maxNameLength);

		// Token: 0x0600010B RID: 267
		int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out JET_OBJECTINFO result, JET_TblInfo infoLevel);

		// Token: 0x0600010C RID: 268
		int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out string result, JET_TblInfo infoLevel);

		// Token: 0x0600010D RID: 269
		int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out JET_DBID result, JET_TblInfo infoLevel);

		// Token: 0x0600010E RID: 270
		int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, int[] result, JET_TblInfo infoLevel);

		// Token: 0x0600010F RID: 271
		int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out int result, JET_TblInfo infoLevel);

		// Token: 0x06000110 RID: 272
		int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out ushort result, JET_IdxInfo infoLevel);

		// Token: 0x06000111 RID: 273
		int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out int result, JET_IdxInfo infoLevel);

		// Token: 0x06000112 RID: 274
		int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out JET_INDEXID result, JET_IdxInfo infoLevel);

		// Token: 0x06000113 RID: 275
		int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out JET_INDEXLIST result, JET_IdxInfo infoLevel);

		// Token: 0x06000114 RID: 276
		int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out string result, JET_IdxInfo infoLevel);

		// Token: 0x06000115 RID: 277
		int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out ushort result, JET_IdxInfo infoLevel);

		// Token: 0x06000116 RID: 278
		int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out int result, JET_IdxInfo infoLevel);

		// Token: 0x06000117 RID: 279
		int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out JET_INDEXID result, JET_IdxInfo infoLevel);

		// Token: 0x06000118 RID: 280
		int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out JET_INDEXLIST result, JET_IdxInfo infoLevel);

		// Token: 0x06000119 RID: 281
		int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out string result, JET_IdxInfo infoLevel);

		// Token: 0x0600011A RID: 282
		int JetRenameTable(JET_SESID sesid, JET_DBID dbid, string tableName, string newTableName);

		// Token: 0x0600011B RID: 283
		int JetRenameColumn(JET_SESID sesid, JET_TABLEID tableid, string name, string newName, RenameColumnGrbit grbit);

		// Token: 0x0600011C RID: 284
		int JetSetColumnDefaultValue(JET_SESID sesid, JET_DBID dbid, string tableName, string columnName, byte[] data, int dataSize, SetColumnDefaultValueGrbit grbit);

		// Token: 0x0600011D RID: 285
		int JetGotoBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize);

		// Token: 0x0600011E RID: 286
		int JetGotoSecondaryIndexBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] secondaryKey, int secondaryKeySize, byte[] primaryKey, int primaryKeySize, GotoSecondaryIndexBookmarkGrbit grbit);

		// Token: 0x0600011F RID: 287
		int JetMove(JET_SESID sesid, JET_TABLEID tableid, int numRows, MoveGrbit grbit);

		// Token: 0x06000120 RID: 288
		int JetMakeKey(JET_SESID sesid, JET_TABLEID tableid, IntPtr data, int dataSize, MakeKeyGrbit grbit);

		// Token: 0x06000121 RID: 289
		int JetSeek(JET_SESID sesid, JET_TABLEID tableid, SeekGrbit grbit);

		// Token: 0x06000122 RID: 290
		int JetSetIndexRange(JET_SESID sesid, JET_TABLEID tableid, SetIndexRangeGrbit grbit);

		// Token: 0x06000123 RID: 291
		int JetIntersectIndexes(JET_SESID sesid, JET_INDEXRANGE[] ranges, int numRanges, out JET_RECORDLIST recordlist, IntersectIndexesGrbit grbit);

		// Token: 0x06000124 RID: 292
		int JetSetCurrentIndex(JET_SESID sesid, JET_TABLEID tableid, string index);

		// Token: 0x06000125 RID: 293
		int JetSetCurrentIndex2(JET_SESID sesid, JET_TABLEID tableid, string index, SetCurrentIndexGrbit grbit);

		// Token: 0x06000126 RID: 294
		int JetSetCurrentIndex3(JET_SESID sesid, JET_TABLEID tableid, string index, SetCurrentIndexGrbit grbit, int itagSequence);

		// Token: 0x06000127 RID: 295
		int JetSetCurrentIndex4(JET_SESID sesid, JET_TABLEID tableid, string index, JET_INDEXID indexid, SetCurrentIndexGrbit grbit, int itagSequence);

		// Token: 0x06000128 RID: 296
		int JetIndexRecordCount(JET_SESID sesid, JET_TABLEID tableid, out int numRecords, int maxRecordsToCount);

		// Token: 0x06000129 RID: 297
		int JetSetTableSequential(JET_SESID sesid, JET_TABLEID tableid, SetTableSequentialGrbit grbit);

		// Token: 0x0600012A RID: 298
		int JetResetTableSequential(JET_SESID sesid, JET_TABLEID tableid, ResetTableSequentialGrbit grbit);

		// Token: 0x0600012B RID: 299
		int JetGetRecordPosition(JET_SESID sesid, JET_TABLEID tableid, out JET_RECPOS recpos);

		// Token: 0x0600012C RID: 300
		int JetGotoPosition(JET_SESID sesid, JET_TABLEID tableid, JET_RECPOS recpos);

		// Token: 0x0600012D RID: 301
		int JetPrereadKeys(JET_SESID sesid, JET_TABLEID tableid, byte[][] keys, int[] keyLengths, int keyIndex, int keyCount, out int keysPreread, PrereadKeysGrbit grbit);

		// Token: 0x0600012E RID: 302
		int JetGetBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize);

		// Token: 0x0600012F RID: 303
		int JetGetSecondaryIndexBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] secondaryKey, int secondaryKeySize, out int actualSecondaryKeySize, byte[] primaryKey, int primaryKeySize, out int actualPrimaryKeySize, GetSecondaryIndexBookmarkGrbit grbit);

		// Token: 0x06000130 RID: 304
		int JetRetrieveKey(JET_SESID sesid, JET_TABLEID tableid, byte[] data, int dataSize, out int actualDataSize, RetrieveKeyGrbit grbit);

		// Token: 0x06000131 RID: 305
		int JetRetrieveColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, IntPtr data, int dataSize, out int actualDataSize, RetrieveColumnGrbit grbit, JET_RETINFO retinfo);

		// Token: 0x06000132 RID: 306
		unsafe int JetRetrieveColumns(JET_SESID sesid, JET_TABLEID tableid, NATIVE_RETRIEVECOLUMN* retrievecolumns, int numColumns);

		// Token: 0x06000133 RID: 307
		int JetEnumerateColumns(JET_SESID sesid, JET_TABLEID tableid, int numColumnids, JET_ENUMCOLUMNID[] columnids, out int numColumnValues, out JET_ENUMCOLUMN[] columnValues, JET_PFNREALLOC allocator, IntPtr allocatorContext, int maxDataSize, EnumerateColumnsGrbit grbit);

		// Token: 0x06000134 RID: 308
		int JetGetRecordSize(JET_SESID sesid, JET_TABLEID tableid, ref JET_RECSIZE recsize, GetRecordSizeGrbit grbit);

		// Token: 0x06000135 RID: 309
		int JetDelete(JET_SESID sesid, JET_TABLEID tableid);

		// Token: 0x06000136 RID: 310
		int JetPrepareUpdate(JET_SESID sesid, JET_TABLEID tableid, JET_prep prep);

		// Token: 0x06000137 RID: 311
		int JetUpdate(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize);

		// Token: 0x06000138 RID: 312
		int JetUpdate2(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize, UpdateGrbit grbit);

		// Token: 0x06000139 RID: 313
		int JetSetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, IntPtr data, int dataSize, SetColumnGrbit grbit, JET_SETINFO setinfo);

		// Token: 0x0600013A RID: 314
		unsafe int JetSetColumns(JET_SESID sesid, JET_TABLEID tableid, NATIVE_SETCOLUMN* setcolumns, int numColumns);

		// Token: 0x0600013B RID: 315
		int JetGetLock(JET_SESID sesid, JET_TABLEID tableid, GetLockGrbit grbit);

		// Token: 0x0600013C RID: 316
		int JetEscrowUpdate(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] delta, int deltaSize, byte[] previousValue, int previousValueLength, out int actualPreviousValueLength, EscrowUpdateGrbit grbit);

		// Token: 0x0600013D RID: 317
		int JetRegisterCallback(JET_SESID sesid, JET_TABLEID tableid, JET_cbtyp cbtyp, JET_CALLBACK callback, IntPtr context, out JET_HANDLE callbackId);

		// Token: 0x0600013E RID: 318
		int JetUnregisterCallback(JET_SESID sesid, JET_TABLEID tableid, JET_cbtyp cbtyp, JET_HANDLE callbackId);

		// Token: 0x0600013F RID: 319
		int JetDefragment(JET_SESID sesid, JET_DBID dbid, string tableName, ref int passes, ref int seconds, DefragGrbit grbit);

		// Token: 0x06000140 RID: 320
		int JetDefragment2(JET_SESID sesid, JET_DBID dbid, string tableName, ref int passes, ref int seconds, JET_CALLBACK callback, DefragGrbit grbit);

		// Token: 0x06000141 RID: 321
		int JetIdle(JET_SESID sesid, IdleGrbit grbit);

		// Token: 0x06000142 RID: 322
		int JetConfigureProcessForCrashDump(CrashDumpGrbit grbit);

		// Token: 0x06000143 RID: 323
		int JetFreeBuffer(IntPtr buffer);

		// Token: 0x06000144 RID: 324
		int JetGetErrorInfo(JET_err error, out JET_ERRINFOBASIC errinfo);

		// Token: 0x06000145 RID: 325
		int JetResizeDatabase(JET_SESID sesid, JET_DBID dbid, int desiredPages, out int actualPages, ResizeDatabaseGrbit grbit);

		// Token: 0x06000146 RID: 326
		int JetCreateIndex4(JET_SESID sesid, JET_TABLEID tableid, JET_INDEXCREATE[] indexcreates, int numIndexCreates);

		// Token: 0x06000147 RID: 327
		int JetOpenTemporaryTable2(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable);

		// Token: 0x06000148 RID: 328
		int JetCreateTableColumnIndex4(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate);

		// Token: 0x06000149 RID: 329
		int JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, byte[] data, int dataSize);

		// Token: 0x0600014A RID: 330
		int JetCommitTransaction2(JET_SESID sesid, CommitTransactionGrbit grbit, TimeSpan durableCommit, out JET_COMMIT_ID commitId);

		// Token: 0x0600014B RID: 331
		int JetPrereadIndexRanges(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_RANGE[] indexRanges, int rangeIndex, int rangeCount, out int rangesPreread, JET_COLUMNID[] columnsPreread, PrereadIndexRangesGrbit grbit);

		// Token: 0x0600014C RID: 332
		int JetPrereadKeyRanges(JET_SESID sesid, JET_TABLEID tableid, byte[][] keysStart, int[] keyStartLengths, byte[][] keysEnd, int[] keyEndLengths, int rangeIndex, int rangeCount, out int rangesPreread, JET_COLUMNID[] columnsPreread, PrereadIndexRangesGrbit grbit);

		// Token: 0x0600014D RID: 333
		int JetSetCursorFilter(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_COLUMN[] filters, CursorFilterGrbit grbit);
	}
}
