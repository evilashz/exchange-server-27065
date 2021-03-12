using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Isam.Esent.Interop.Implementation;
using Microsoft.Isam.Esent.Interop.Vista;
using Microsoft.Isam.Esent.Interop.Windows8;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200006F RID: 111
	public static class UnpublishedApi
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
		public static JET_DBID AllDatabases
		{
			get
			{
				return new JET_DBID
				{
					Value = 2147483647U
				};
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000C016 File Offset: 0x0000A216
		public static void JetTracing(JET_traceop operation, JET_tracetag tag, bool value)
		{
			Api.Check(Api.Impl.JetTracing(operation, tag, value));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000C02B File Offset: 0x0000A22B
		public static void JetTracing(JET_traceop operation, JET_tracetag tag, JET_DBID value)
		{
			Api.Check(Api.Impl.JetTracing(operation, tag, value));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000C040 File Offset: 0x0000A240
		public static void JetTracing(JET_traceop operation, JET_tracetag tag, int value)
		{
			Api.Check(Api.Impl.JetTracing(operation, tag, value));
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000C055 File Offset: 0x0000A255
		public static void JetTracing(JET_traceop operation, JET_tracetag tag, JET_PFNTRACEREGISTER callback)
		{
			Api.Check(Api.Impl.JetTracing(operation, tag, callback));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000C06A File Offset: 0x0000A26A
		public static void JetTracing(JET_traceop operation, JET_tracetag tag, JET_PFNTRACEEMIT callback)
		{
			Api.Check(Api.Impl.JetTracing(operation, tag, callback));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000C07F File Offset: 0x0000A27F
		public static void JetBeginDatabaseIncrementalReseed(JET_INSTANCE instance, string wszDatabase, BeginDatabaseIncrementalReseedGrbit grbit)
		{
			Api.Check(Api.Impl.JetBeginDatabaseIncrementalReseed(instance, wszDatabase, grbit));
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000C094 File Offset: 0x0000A294
		public static void JetEndDatabaseIncrementalReseed(JET_INSTANCE instance, string wszDatabase, int genMinRequired, int genFirstDivergedLog, int genMaxRequired, EndDatabaseIncrementalReseedGrbit grbit)
		{
			Api.Check(Api.Impl.JetEndDatabaseIncrementalReseed(instance, wszDatabase, genMinRequired, genFirstDivergedLog, genMaxRequired, grbit));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000C0AE File Offset: 0x0000A2AE
		public static void JetPatchDatabasePages(JET_INSTANCE instance, string databaseFileName, int pgnoStart, int pageCount, byte[] inputData, int dataLength, PatchDatabasePagesGrbit grbit)
		{
			Api.Check(Api.Impl.JetPatchDatabasePages(instance, databaseFileName, pgnoStart, pageCount, inputData, dataLength, grbit));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		public static void JetRemoveLogfile(string databaseFileName, string logFileName, RemoveLogfileGrbit grbit)
		{
			Api.Check(Api.Impl.JetRemoveLogfile(databaseFileName, logFileName, grbit));
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public static void JetGetDatabasePages(JET_SESID sesid, JET_DBID dbid, int pgnoStart, int countPages, byte[] pageBytes, int pageBytesLength, out int pageBytesRead, GetDatabasePagesGrbit grbit)
		{
			Api.Check(Api.Impl.JetGetDatabasePages(sesid, dbid, pgnoStart, countPages, pageBytes, pageBytesLength, out pageBytesRead, grbit));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000C109 File Offset: 0x0000A309
		public static void JetSetResourceParam(JET_INSTANCE instance, JET_resoper resoper, JET_resid resid, long longParameter)
		{
			Api.Check(Api.Impl.JetSetResourceParam(instance, resoper, resid, longParameter));
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000C11F File Offset: 0x0000A31F
		public static void JetGetResourceParam(JET_INSTANCE instance, JET_resoper resoper, JET_resid resid, out long longParameter)
		{
			Api.Check(Api.Impl.JetGetResourceParam(instance, resoper, resid, out longParameter));
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000C135 File Offset: 0x0000A335
		public static void JetConsumeLogData(JET_INSTANCE instance, JET_EMITDATACTX emitLogDataCtx, byte[] logDataBuf, int logDataStartOffset, int logDataLength, ShadowLogConsumeGrbit grbit)
		{
			Api.Check(Api.Impl.JetConsumeLogData(instance, emitLogDataCtx, logDataBuf, logDataStartOffset, logDataLength, grbit));
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000C14F File Offset: 0x0000A34F
		public static void JetGetLogFileInfo(string logFileName, out JET_LOGINFOMISC info, JET_LogInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetLogFileInfo(logFileName, out info, infoLevel));
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000C164 File Offset: 0x0000A364
		public static void JetGetPageInfo2(byte[] rawPageData, int pageDataLength, JET_PAGEINFO[] pageInfos, PageInfoGrbit grbit, JET_PageInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetPageInfo2(rawPageData, pageDataLength, pageInfos, grbit, infoLevel));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000C17C File Offset: 0x0000A37C
		public static void JetGetInstanceMiscInfo(JET_INSTANCE instance, out JET_CHECKPOINTINFO checkpointInfo, JET_InstanceMiscInfo infoLevel)
		{
			Api.Check(Api.Impl.JetGetInstanceMiscInfo(instance, out checkpointInfo, infoLevel));
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000C191 File Offset: 0x0000A391
		public static void JetDBUtilities(JET_DBUTIL dbutil)
		{
			Api.Check(Api.Impl.JetDBUtilities(dbutil));
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		public static void JetTestHook(TestHookOp opcode, TestHookNegativeTestingFlags flags)
		{
			uint num = (uint)flags;
			Api.Check(Api.Impl.JetTestHook((int)opcode, ref num));
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public static void JetTestHook(TestHookOp opcode, JET_TESTHOOKTESTINJECTION testInjection)
		{
			NATIVE_TESTHOOKTESTINJECTION nativeTestHookInjection = testInjection.GetNativeTestHookInjection();
			Api.Check(Api.Impl.JetTestHook((int)opcode, ref nativeTestHookInjection));
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000C1EF File Offset: 0x0000A3EF
		public static void JetTestHook(TestHookOp opcode, JET_TESTHOOKEVICTCACHE testEvictCachedPage)
		{
			Api.Check(Api.Impl.JetTestHook((int)opcode, testEvictCachedPage));
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000C203 File Offset: 0x0000A403
		public static void JetTestHook(TestHookOp opcode, ref IntPtr pv)
		{
			Api.Check(Api.Impl.JetTestHook((int)opcode, ref pv));
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000C217 File Offset: 0x0000A417
		public static void JetTestHook(TestHookOp opcode, ref uint pv)
		{
			Api.Check(Api.Impl.JetTestHook((int)opcode, ref pv));
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000C22C File Offset: 0x0000A42C
		public static void JetTestHook(TestHookOp opcode, JET_TESTHOOKTRACETESTMARKER traceTestMarker)
		{
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			try
			{
				NATIVE_TESTHOOKTRACETESTMARKER nativeTraceTestMarker = traceTestMarker.GetNativeTraceTestMarker(ref gchandleCollection);
				Api.Check(Api.Impl.JetTestHook((int)opcode, ref nativeTraceTestMarker));
			}
			finally
			{
				gchandleCollection.Dispose();
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000C278 File Offset: 0x0000A478
		public static void JetTestHook(TestHookOp opcode, JET_TESTHOOKCORRUPT corruptData)
		{
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			try
			{
				NATIVE_TESTHOOKCORRUPT_DATABASEFILE nativeCorruptDatabaseFile = corruptData.GetNativeCorruptDatabaseFile(ref gchandleCollection);
				Api.Check(Api.Impl.JetTestHook((int)opcode, ref nativeCorruptDatabaseFile));
			}
			finally
			{
				gchandleCollection.Dispose();
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
		public static void JetPrereadTables(JET_SESID sesid, JET_DBID dbid, string[] rgsz, PrereadTablesGrbit grbit)
		{
			Api.Check(Api.Impl.JetPrereadTables(sesid, dbid, rgsz, grbit));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000C2DA File Offset: 0x0000A4DA
		public static JET_wrn JetDatabaseScan(JET_SESID sesid, JET_DBID dbid, ref int seconds, int sleepInMsec, JET_CALLBACK callback, DatabaseScanGrbit grbit)
		{
			return Api.Check(Api.Impl.JetDatabaseScan(sesid, dbid, ref seconds, sleepInMsec, callback, grbit));
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000C2F3 File Offset: 0x0000A4F3
		public static void JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, int value)
		{
			Api.Check(Api.Impl.JetSetSessionParameter(sesid, sesparamid, value));
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000C308 File Offset: 0x0000A508
		public static void JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, JET_OPERATIONCONTEXT operationContext)
		{
			NATIVE_OPERATIONCONTEXT nativeOperationContext = operationContext.GetNativeOperationContext();
			Api.Check(Api.Impl.JetSetSessionParameter(sesid, sesparamid, ref nativeOperationContext));
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000C334 File Offset: 0x0000A534
		public static void TestHookEnableConfigOverrideInjection(int testHookIdentifier, IntPtr testHookValue, int probability, TestInjectionGrbit grbit)
		{
			UnpublishedApi.JetTestHook(TestHookOp.TestInjection, new JET_TESTHOOKTESTINJECTION
			{
				type = TestHookInjectionType.ConfigOverride,
				pv = testHookValue,
				ulID = (uint)testHookIdentifier,
				ulProbability = (uint)probability,
				grbit = grbit
			});
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000C374 File Offset: 0x0000A574
		public static void TestHookEnableFaultInjection(int testHookIdentifier, JET_err testHookValue, int probability, TestInjectionGrbit grbit)
		{
			UnpublishedApi.JetTestHook(TestHookOp.TestInjection, new JET_TESTHOOKTESTINJECTION
			{
				type = TestHookInjectionType.Fault,
				pv = new IntPtr((int)testHookValue),
				ulID = (uint)testHookIdentifier,
				ulProbability = (uint)probability,
				grbit = grbit
			});
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public static void TestHookEvictCachedPage(JET_DBID dbid, int pgnoToEvict)
		{
			UnpublishedApi.JetTestHook(TestHookOp.EvictCache, new JET_TESTHOOKEVICTCACHE
			{
				ulTargetContext = dbid,
				ulTargetData = pgnoToEvict,
				grbit = EvictCacheGrbit.EvictDataByPgno
			});
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		public static int GetLogGenFromCheckpointFileInfo(string checkpointFile)
		{
			return UnpublishedApi.GetCheckpointGeneration(checkpointFile);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public static void ChecksumLogFromMemory(JET_SESID sesid, string logFile, string basename, byte[] logBytes)
		{
			UnpublishedApi.CheckNotNull(logFile, "logFile");
			UnpublishedApi.CheckNotNull(basename, "basename");
			UnpublishedApi.CheckNotNull(logBytes, "logBytes");
			JET_DBUTIL dbutil = new JET_DBUTIL
			{
				op = DBUTIL_OP.ChecksumLogFromMemory,
				sesid = sesid,
				szLog = logFile,
				szBase = basename,
				pvBuffer = logBytes,
				cbBuffer = logBytes.Length
			};
			UnpublishedApi.JetDBUtilities(dbutil);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000C45C File Offset: 0x0000A65C
		private static int GetCheckpointGeneration(string checkpointFile)
		{
			UnpublishedApi.CheckNotNull(checkpointFile, "checkpointFile");
			byte[] array = new byte[4096];
			using (FileStream fileStream = new FileStream(checkpointFile, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				fileStream.Read(array, 0, 4096);
			}
			UnpublishedApi.VerifyCheckpoint(array);
			return BitConverter.ToInt32(array, 16);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		private static void VerifyCheckpoint(byte[] checkpointBytes)
		{
			if (4096 != checkpointBytes.Length)
			{
				throw new ArgumentException("VerifyCheckpoint() needs a 4k input buffer.");
			}
			uint num = BitConverter.ToUInt32(checkpointBytes, 0);
			uint num2 = UnpublishedApi.CalculateHeaderChecksum(checkpointBytes);
			if (num != num2)
			{
				throw new EsentCheckpointCorruptException();
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000C500 File Offset: 0x0000A700
		private static uint CalculateHeaderChecksum(byte[] headerBytes)
		{
			if (headerBytes.Length % 4096 != 0)
			{
				throw new ArgumentException("CalculateHeaderChecksum() needs an input buffer of a 4k-multiple.");
			}
			int num = headerBytes.Length;
			int num2 = 0;
			uint num3 = BitConverter.ToUInt32(headerBytes, num2);
			uint num4 = 2309737967U ^ num3;
			do
			{
				num4 ^= (BitConverter.ToUInt32(headerBytes, num2) ^ BitConverter.ToUInt32(headerBytes, num2 + 4) ^ BitConverter.ToUInt32(headerBytes, num2 + 8) ^ BitConverter.ToUInt32(headerBytes, num2 + 12) ^ BitConverter.ToUInt32(headerBytes, num2 + 16) ^ BitConverter.ToUInt32(headerBytes, num2 + 20) ^ BitConverter.ToUInt32(headerBytes, num2 + 24) ^ BitConverter.ToUInt32(headerBytes, num2 + 28));
				num -= 32;
				num2 += 32;
			}
			while (num > 0);
			return num4;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000C59D File Offset: 0x0000A79D
		private static void CheckNotNull(object o, string paramName)
		{
			if (o == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x0400025D RID: 605
		private static readonly TraceSwitch TraceSwitch = new TraceSwitch("ESENT P/Invoke", "P/Invoke calls to ESENT");
	}
}
