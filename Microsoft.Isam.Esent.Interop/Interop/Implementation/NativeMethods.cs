using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Isam.Esent.Interop.Vista;
using Microsoft.Isam.Esent.Interop.Windows8;

namespace Microsoft.Isam.Esent.Interop.Implementation
{
	// Token: 0x02000026 RID: 38
	[BestFitMapping(false, ThrowOnUnmappableChar = true)]
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x06000237 RID: 567
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetTracing(int traceop, int tracetag, IntPtr ul);

		// Token: 0x06000238 RID: 568
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetResourceParam(JET_INSTANCE instance, JET_resoper resoper, JET_resid resid, IntPtr ulParam);

		// Token: 0x06000239 RID: 569
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetResourceParam(JET_INSTANCE instance, JET_resoper resoper, JET_resid resid, out IntPtr ulParam);

		// Token: 0x0600023A RID: 570
		[DllImport("eseback2.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public unsafe static extern int HrESEBackupRestoreRegister3(string wszDisplayName, uint fFlags, string wszEndpointAnnotation, NATIVE_ESEBACK_CALLBACKS_IMPL* pCallbacks, Guid* pguidCrimsonPublisher);

		// Token: 0x0600023B RID: 571
		[DllImport("eseback2.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int HrESEBackupRestoreUnregister();

		// Token: 0x0600023C RID: 572
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseFileInfoW(string szFilename, out NATIVE_DBINFOMISC7 dbinfomisc, uint cbMax, uint InfoLevel);

		// Token: 0x0600023D RID: 573
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetLogFileInfoW(string szLog, out NATIVE_LOGINFOMISC2 pvResult, uint cbMax, uint InfoLevel);

		// Token: 0x0600023E RID: 574
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public unsafe static extern int JetGetPageInfo2(IntPtr pvPages, uint cbData, NATIVE_PAGEINFO* rgPageInfo, uint cbPageInfo, uint grbit, uint ulInfoLevel);

		// Token: 0x0600023F RID: 575
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetConsumeLogData(JET_INSTANCE instance, NATIVE_EMITDATACTX* pEmitLogDataCtrx, byte* pvLogData, uint cbLogData, uint grbit);

		// Token: 0x06000240 RID: 576
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetInstanceMiscInfo(IntPtr instance, ref NATIVE_CHECKPOINTINFO pvResult, uint cbMax, uint infoLevel);

		// Token: 0x06000241 RID: 577
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetBeginDatabaseIncrementalReseedW(JET_INSTANCE instance, string szDatabase, uint grbit);

		// Token: 0x06000242 RID: 578
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetEndDatabaseIncrementalReseedW(JET_INSTANCE instance, string wszDatabase, uint genMinRequired, uint genFirstDivergedLog, uint genMaxRequired, uint grbit);

		// Token: 0x06000243 RID: 579
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public unsafe static extern int JetPatchDatabasePagesW(JET_INSTANCE instance, string szDatabase, uint pgnoStart, uint cpg, byte* pv, uint cb, uint grbit);

		// Token: 0x06000244 RID: 580
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetRemoveLogfileW(string szDatabase, string szLogfile, uint grbit);

		// Token: 0x06000245 RID: 581
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetDatabasePages(IntPtr sesid, uint dbid, uint pgnoStart, uint cpg, IntPtr pv, uint cb, out uint cbActual, uint grbit);

		// Token: 0x06000246 RID: 582
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetDBUtilitiesW(ref NATIVE_DBUTIL_LEGACY pdbutil);

		// Token: 0x06000247 RID: 583
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetDBUtilitiesW(ref NATIVE_DBUTIL_CHECKSUMLOG pdbutil);

		// Token: 0x06000248 RID: 584
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTestHook(int opcode, ref uint pv);

		// Token: 0x06000249 RID: 585
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTestHook(int opcode, ref NATIVE_TESTHOOKTESTINJECTION pv);

		// Token: 0x0600024A RID: 586
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTestHook(int opcode, ref NATIVE_TESTHOOKEVICTCACHE pv);

		// Token: 0x0600024B RID: 587
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTestHook(int opcode, ref IntPtr pv);

		// Token: 0x0600024C RID: 588
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTestHook(int opcode, ref NATIVE_TESTHOOKTRACETESTMARKER pv);

		// Token: 0x0600024D RID: 589
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTestHook(int opcode, ref NATIVE_TESTHOOKCORRUPT_DATABASEFILE pv);

		// Token: 0x0600024E RID: 590
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetPrereadTablesW(IntPtr sesid, uint dbid, string[] rgsz, int csz, int grbit);

		// Token: 0x0600024F RID: 591
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetDatabaseScan(IntPtr sesid, uint dbid, ref uint pcSeconds, uint cmsecSleep, IntPtr callback, uint grbit);

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000088A4 File Offset: 0x00006AA4
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000088AB File Offset: 0x00006AAB
		public static Encoding Encoding { get; private set; } = LibraryHelpers.EncodingASCII;

		// Token: 0x06000253 RID: 595
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCreateInstance(out IntPtr instance, string szInstanceName);

		// Token: 0x06000254 RID: 596
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateInstanceW(out IntPtr instance, string szInstanceName);

		// Token: 0x06000255 RID: 597
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCreateInstance2(out IntPtr instance, string szInstanceName, string szDisplayName, uint grbit);

		// Token: 0x06000256 RID: 598
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateInstance2W(out IntPtr instance, string szInstanceName, string szDisplayName, uint grbit);

		// Token: 0x06000257 RID: 599
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetInit(ref IntPtr instance);

		// Token: 0x06000258 RID: 600
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetInit2(ref IntPtr instance, uint grbit);

		// Token: 0x06000259 RID: 601
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetInit3W(ref IntPtr instance, ref NATIVE_RSTINFO prstinfo, uint grbit);

		// Token: 0x0600025A RID: 602
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetInit3W(ref IntPtr instance, IntPtr prstinfo, uint grbit);

		// Token: 0x0600025B RID: 603
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetGetInstanceInfo(out uint pcInstanceInfo, out NATIVE_INSTANCE_INFO* prgInstanceInfo);

		// Token: 0x0600025C RID: 604
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetGetInstanceInfoW(out uint pcInstanceInfo, out NATIVE_INSTANCE_INFO* prgInstanceInfo);

		// Token: 0x0600025D RID: 605
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetInstanceMiscInfo(IntPtr instance, ref NATIVE_SIGNATURE pvResult, uint cbMax, uint infoLevel);

		// Token: 0x0600025E RID: 606
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetStopBackupInstance(IntPtr instance);

		// Token: 0x0600025F RID: 607
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetStopServiceInstance(IntPtr instance);

		// Token: 0x06000260 RID: 608
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetStopServiceInstance2(IntPtr instance, uint grbit);

		// Token: 0x06000261 RID: 609
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTerm(IntPtr instance);

		// Token: 0x06000262 RID: 610
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTerm2(IntPtr instance, uint grbit);

		// Token: 0x06000263 RID: 611
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public unsafe static extern int JetSetSystemParameter(IntPtr* pinstance, IntPtr sesid, uint paramid, IntPtr lParam, string szParam);

		// Token: 0x06000264 RID: 612
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public unsafe static extern int JetSetSystemParameterW(IntPtr* pinstance, IntPtr sesid, uint paramid, IntPtr lParam, string szParam);

		// Token: 0x06000265 RID: 613
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetSystemParameter(IntPtr instance, IntPtr sesid, uint paramid, ref IntPtr plParam, [Out] StringBuilder szParam, uint cbMax);

		// Token: 0x06000266 RID: 614
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetSystemParameterW(IntPtr instance, IntPtr sesid, uint paramid, ref IntPtr plParam, [Out] StringBuilder szParam, uint cbMax);

		// Token: 0x06000267 RID: 615
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetVersion(IntPtr sesid, out uint dwVersion);

		// Token: 0x06000268 RID: 616
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCreateDatabase(IntPtr sesid, string szFilename, string szConnect, out uint dbid, uint grbit);

		// Token: 0x06000269 RID: 617
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateDatabaseW(IntPtr sesid, string szFilename, string szConnect, out uint dbid, uint grbit);

		// Token: 0x0600026A RID: 618
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCreateDatabase2(IntPtr sesid, string szFilename, uint cpgDatabaseSizeMax, out uint dbid, uint grbit);

		// Token: 0x0600026B RID: 619
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateDatabase2W(IntPtr sesid, string szFilename, uint cpgDatabaseSizeMax, out uint dbid, uint grbit);

		// Token: 0x0600026C RID: 620
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetAttachDatabase(IntPtr sesid, string szFilename, uint grbit);

		// Token: 0x0600026D RID: 621
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetAttachDatabaseW(IntPtr sesid, string szFilename, uint grbit);

		// Token: 0x0600026E RID: 622
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetAttachDatabase2(IntPtr sesid, string szFilename, uint cpgDatabaseSizeMax, uint grbit);

		// Token: 0x0600026F RID: 623
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetAttachDatabase2W(IntPtr sesid, string szFilename, uint cpgDatabaseSizeMax, uint grbit);

		// Token: 0x06000270 RID: 624
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetDetachDatabase(IntPtr sesid, string szFilename);

		// Token: 0x06000271 RID: 625
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetDetachDatabase2(IntPtr sesid, string szFilename, uint grbit);

		// Token: 0x06000272 RID: 626
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetDetachDatabase2W(IntPtr sesid, string szFilename, uint grbit);

		// Token: 0x06000273 RID: 627
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetDetachDatabaseW(IntPtr sesid, string szFilename);

		// Token: 0x06000274 RID: 628
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetOpenDatabase(IntPtr sesid, string database, string szConnect, out uint dbid, uint grbit);

		// Token: 0x06000275 RID: 629
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetOpenDatabaseW(IntPtr sesid, string database, string szConnect, out uint dbid, uint grbit);

		// Token: 0x06000276 RID: 630
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetCloseDatabase(IntPtr sesid, uint dbid, uint grbit);

		// Token: 0x06000277 RID: 631
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCompact(IntPtr sesid, string szDatabaseSrc, string szDatabaseDest, IntPtr pfnStatus, IntPtr pconvert, uint grbit);

		// Token: 0x06000278 RID: 632
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCompactW(IntPtr sesid, string szDatabaseSrc, string szDatabaseDest, IntPtr pfnStatus, IntPtr pconvert, uint grbit);

		// Token: 0x06000279 RID: 633
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGrowDatabase(IntPtr sesid, uint dbid, uint cpg, out uint pcpgReal);

		// Token: 0x0600027A RID: 634
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetSetDatabaseSize(IntPtr sesid, string szDatabaseName, uint cpg, out uint pcpgReal);

		// Token: 0x0600027B RID: 635
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetSetDatabaseSizeW(IntPtr sesid, string szDatabaseName, uint cpg, out uint pcpgReal);

		// Token: 0x0600027C RID: 636
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetDatabaseInfo(IntPtr sesid, uint dbid, out int intValue, uint cbMax, uint InfoLevel);

		// Token: 0x0600027D RID: 637
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetDatabaseInfo(IntPtr sesid, uint dbid, out NATIVE_DBINFOMISC dbinfomisc, uint cbMax, uint InfoLevel);

		// Token: 0x0600027E RID: 638
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetDatabaseInfo(IntPtr sesid, uint dbid, out NATIVE_DBINFOMISC4 dbinfomisc, uint cbMax, uint InfoLevel);

		// Token: 0x0600027F RID: 639
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetDatabaseInfo(IntPtr sesid, uint dbid, [Out] StringBuilder stringValue, uint cbMax, uint InfoLevel);

		// Token: 0x06000280 RID: 640
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseInfoW(IntPtr sesid, uint dbid, out int intValue, uint cbMax, uint InfoLevel);

		// Token: 0x06000281 RID: 641
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseInfoW(IntPtr sesid, uint dbid, out NATIVE_DBINFOMISC dbinfomisc, uint cbMax, uint InfoLevel);

		// Token: 0x06000282 RID: 642
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseInfoW(IntPtr sesid, uint dbid, out NATIVE_DBINFOMISC4 dbinfomisc, uint cbMax, uint InfoLevel);

		// Token: 0x06000283 RID: 643
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseInfoW(IntPtr sesid, uint dbid, [Out] StringBuilder stringValue, uint cbMax, uint InfoLevel);

		// Token: 0x06000284 RID: 644
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseFileInfoW(string szFilename, out int intValue, uint cbMax, uint InfoLevel);

		// Token: 0x06000285 RID: 645
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetDatabaseFileInfo(string szFilename, out int intValue, uint cbMax, uint InfoLevel);

		// Token: 0x06000286 RID: 646
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseFileInfoW(string szFilename, out long intValue, uint cbMax, uint InfoLevel);

		// Token: 0x06000287 RID: 647
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetDatabaseFileInfo(string szFilename, out long intValue, uint cbMax, uint InfoLevel);

		// Token: 0x06000288 RID: 648
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseFileInfoW(string szFilename, out NATIVE_DBINFOMISC4 dbinfomisc, uint cbMax, uint InfoLevel);

		// Token: 0x06000289 RID: 649
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetDatabaseFileInfo(string szFilename, out NATIVE_DBINFOMISC dbinfomisc, uint cbMax, uint InfoLevel);

		// Token: 0x0600028A RID: 650
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetDatabaseFileInfoW(string szFilename, out NATIVE_DBINFOMISC dbinfomisc, uint cbMax, uint InfoLevel);

		// Token: 0x0600028B RID: 651
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetBackupInstance(IntPtr instance, string szBackupPath, uint grbit, IntPtr pfnStatus);

		// Token: 0x0600028C RID: 652
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetBackupInstanceW(IntPtr instance, string szBackupPath, uint grbit, IntPtr pfnStatus);

		// Token: 0x0600028D RID: 653
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetRestoreInstance(IntPtr instance, string sz, string szDest, IntPtr pfn);

		// Token: 0x0600028E RID: 654
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetRestoreInstanceW(IntPtr instance, string sz, string szDest, IntPtr pfn);

		// Token: 0x0600028F RID: 655
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOSSnapshotPrepare(out IntPtr snapId, uint grbit);

		// Token: 0x06000290 RID: 656
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOSSnapshotPrepareInstance(IntPtr snapId, IntPtr instance, uint grbit);

		// Token: 0x06000291 RID: 657
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetOSSnapshotFreeze(IntPtr snapId, out uint pcInstanceInfo, out NATIVE_INSTANCE_INFO* prgInstanceInfo, uint grbit);

		// Token: 0x06000292 RID: 658
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetOSSnapshotFreezeW(IntPtr snapId, out uint pcInstanceInfo, out NATIVE_INSTANCE_INFO* prgInstanceInfo, uint grbit);

		// Token: 0x06000293 RID: 659
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetOSSnapshotGetFreezeInfoW(IntPtr snapId, out uint pcInstanceInfo, out NATIVE_INSTANCE_INFO* prgInstanceInfo, uint grbit);

		// Token: 0x06000294 RID: 660
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOSSnapshotThaw(IntPtr snapId, uint grbit);

		// Token: 0x06000295 RID: 661
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOSSnapshotTruncateLog(IntPtr snapId, uint grbit);

		// Token: 0x06000296 RID: 662
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOSSnapshotTruncateLogInstance(IntPtr snapId, IntPtr instance, uint grbit);

		// Token: 0x06000297 RID: 663
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOSSnapshotEnd(IntPtr snapId, uint grbit);

		// Token: 0x06000298 RID: 664
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOSSnapshotAbort(IntPtr snapId, uint grbit);

		// Token: 0x06000299 RID: 665
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetBeginExternalBackupInstance(IntPtr instance, uint grbit);

		// Token: 0x0600029A RID: 666
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetCloseFileInstance(IntPtr instance, IntPtr handle);

		// Token: 0x0600029B RID: 667
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetEndExternalBackupInstance(IntPtr instance);

		// Token: 0x0600029C RID: 668
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetEndExternalBackupInstance2(IntPtr instance, uint grbit);

		// Token: 0x0600029D RID: 669
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetAttachInfoInstance(IntPtr instance, [Out] byte[] szz, uint cbMax, out uint pcbActual);

		// Token: 0x0600029E RID: 670
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetAttachInfoInstanceW(IntPtr instance, [Out] byte[] szz, uint cbMax, out uint pcbActual);

		// Token: 0x0600029F RID: 671
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetLogInfoInstance(IntPtr instance, [Out] byte[] szz, uint cbMax, out uint pcbActual);

		// Token: 0x060002A0 RID: 672
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetLogInfoInstanceW(IntPtr instance, [Out] byte[] szz, uint cbMax, out uint pcbActual);

		// Token: 0x060002A1 RID: 673
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTruncateLogInfoInstance(IntPtr instance, [Out] byte[] szz, uint cbMax, out uint pcbActual);

		// Token: 0x060002A2 RID: 674
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTruncateLogInfoInstanceW(IntPtr instance, [Out] byte[] szz, uint cbMax, out uint pcbActual);

		// Token: 0x060002A3 RID: 675
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetOpenFileInstance(IntPtr instance, string szFileName, out IntPtr phfFile, out uint pulFileSizeLow, out uint pulFileSizeHigh);

		// Token: 0x060002A4 RID: 676
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetOpenFileInstanceW(IntPtr instance, string szFileName, out IntPtr phfFile, out uint pulFileSizeLow, out uint pulFileSizeHigh);

		// Token: 0x060002A5 RID: 677
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetReadFileInstance(IntPtr instance, IntPtr handle, IntPtr pv, uint cb, out uint pcbActual);

		// Token: 0x060002A6 RID: 678
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetTruncateLogInstance(IntPtr instance);

		// Token: 0x060002A7 RID: 679
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetBeginSession(IntPtr instance, out IntPtr session, string username, string password);

		// Token: 0x060002A8 RID: 680
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetSessionContext(IntPtr session, IntPtr context);

		// Token: 0x060002A9 RID: 681
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetResetSessionContext(IntPtr session);

		// Token: 0x060002AA RID: 682
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetEndSession(IntPtr sesid, uint grbit);

		// Token: 0x060002AB RID: 683
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetDupSession(IntPtr sesid, out IntPtr newSesid);

		// Token: 0x060002AC RID: 684
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetGetThreadStats(JET_THREADSTATS* pvResult, uint cbMax);

		// Token: 0x060002AD RID: 685
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetOpenTable(IntPtr sesid, uint dbid, string tablename, byte[] pvParameters, uint cbParameters, uint grbit, out IntPtr tableid);

		// Token: 0x060002AE RID: 686
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetOpenTableW(IntPtr sesid, uint dbid, string tablename, byte[] pvParameters, uint cbParameters, uint grbit, out IntPtr tableid);

		// Token: 0x060002AF RID: 687
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetCloseTable(IntPtr sesid, IntPtr tableid);

		// Token: 0x060002B0 RID: 688
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetDupCursor(IntPtr sesid, IntPtr tableid, out IntPtr tableidNew, uint grbit);

		// Token: 0x060002B1 RID: 689
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetComputeStats(IntPtr sesid, IntPtr tableid);

		// Token: 0x060002B2 RID: 690
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetLS(IntPtr sesid, IntPtr tableid, IntPtr ls, uint grbit);

		// Token: 0x060002B3 RID: 691
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetLS(IntPtr sesid, IntPtr tableid, out IntPtr pls, uint grbit);

		// Token: 0x060002B4 RID: 692
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetCursorInfo(IntPtr sesid, IntPtr tableid, IntPtr pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002B5 RID: 693
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetBeginTransaction(IntPtr sesid);

		// Token: 0x060002B6 RID: 694
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetBeginTransaction2(IntPtr sesid, uint grbit);

		// Token: 0x060002B7 RID: 695
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetBeginTransaction3(IntPtr sesid, long trxid, uint grbit);

		// Token: 0x060002B8 RID: 696
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetCommitTransaction(IntPtr sesid, uint grbit);

		// Token: 0x060002B9 RID: 697
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetRollback(IntPtr sesid, uint grbit);

		// Token: 0x060002BA RID: 698
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCreateTable(IntPtr sesid, uint dbid, string szTableName, int pages, int density, out IntPtr tableid);

		// Token: 0x060002BB RID: 699
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetAddColumn(IntPtr sesid, IntPtr tableid, string szColumnName, [In] ref NATIVE_COLUMNDEF columndef, [In] byte[] pvDefault, uint cbDefault, out uint columnid);

		// Token: 0x060002BC RID: 700
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetDeleteColumn(IntPtr sesid, IntPtr tableid, string szColumnName);

		// Token: 0x060002BD RID: 701
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetDeleteColumn2(IntPtr sesid, IntPtr tableid, string szColumnName, uint grbit);

		// Token: 0x060002BE RID: 702
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetDeleteIndex(IntPtr sesid, IntPtr tableid, string szIndexName);

		// Token: 0x060002BF RID: 703
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetDeleteTable(IntPtr sesid, uint dbid, string szTableName);

		// Token: 0x060002C0 RID: 704
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCreateIndex(IntPtr sesid, IntPtr tableid, string szIndexName, uint grbit, string szKey, uint cbKey, uint lDensity);

		// Token: 0x060002C1 RID: 705
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCreateIndex2(IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEXCREATE[] pindexcreate, uint cIndexCreate);

		// Token: 0x060002C2 RID: 706
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateIndex2W(IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEXCREATE1[] pindexcreate, uint cIndexCreate);

		// Token: 0x060002C3 RID: 707
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateIndex3W(IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEXCREATE2[] pindexcreate, uint cIndexCreate);

		// Token: 0x060002C4 RID: 708
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOpenTempTable(IntPtr sesid, [In] NATIVE_COLUMNDEF[] rgcolumndef, uint ccolumn, uint grbit, out IntPtr ptableid, [Out] uint[] rgcolumnid);

		// Token: 0x060002C5 RID: 709
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOpenTempTable2(IntPtr sesid, [In] NATIVE_COLUMNDEF[] rgcolumndef, uint ccolumn, uint lcid, uint grbit, out IntPtr ptableid, [Out] uint[] rgcolumnid);

		// Token: 0x060002C6 RID: 710
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOpenTempTable3(IntPtr sesid, [In] NATIVE_COLUMNDEF[] rgcolumndef, uint ccolumn, [In] ref NATIVE_UNICODEINDEX pidxunicode, uint grbit, out IntPtr ptableid, [Out] uint[] rgcolumnid);

		// Token: 0x060002C7 RID: 711
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOpenTemporaryTable(IntPtr sesid, [In] [Out] ref NATIVE_OPENTEMPORARYTABLE popentemporarytable);

		// Token: 0x060002C8 RID: 712
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOpenTempTable3(IntPtr sesid, [In] NATIVE_COLUMNDEF[] rgcolumndef, uint ccolumn, IntPtr pidxunicode, uint grbit, out IntPtr ptableid, [Out] uint[] rgcolumnid);

		// Token: 0x060002C9 RID: 713
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetCreateTableColumnIndex2(IntPtr sesid, uint dbid, ref NATIVE_TABLECREATE2 tablecreate3);

		// Token: 0x060002CA RID: 714
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateTableColumnIndex2W(IntPtr sesid, uint dbid, ref NATIVE_TABLECREATE2 tablecreate3);

		// Token: 0x060002CB RID: 715
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateTableColumnIndex3W(IntPtr sesid, uint dbid, ref NATIVE_TABLECREATE3 tablecreate3);

		// Token: 0x060002CC RID: 716
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableColumnInfo(IntPtr sesid, IntPtr tableid, string szColumnName, ref NATIVE_COLUMNDEF columndef, uint cbMax, uint InfoLevel);

		// Token: 0x060002CD RID: 717
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetTableColumnInfo(IntPtr sesid, IntPtr tableid, ref uint pcolumnid, ref NATIVE_COLUMNDEF columndef, uint cbMax, uint InfoLevel);

		// Token: 0x060002CE RID: 718
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableColumnInfo(IntPtr sesid, IntPtr tableid, string szColumnName, ref NATIVE_COLUMNBASE columnbase, uint cbMax, uint InfoLevel);

		// Token: 0x060002CF RID: 719
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableColumnInfo(IntPtr sesid, IntPtr tableid, string szIgnored, ref NATIVE_COLUMNLIST columnlist, uint cbMax, uint InfoLevel);

		// Token: 0x060002D0 RID: 720
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableColumnInfoW(IntPtr sesid, IntPtr tableid, string szColumnName, ref NATIVE_COLUMNDEF columndef, uint cbMax, uint InfoLevel);

		// Token: 0x060002D1 RID: 721
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetTableColumnInfoW(IntPtr sesid, IntPtr tableid, ref uint pcolumnid, ref NATIVE_COLUMNDEF columndef, uint cbMax, uint InfoLevel);

		// Token: 0x060002D2 RID: 722
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableColumnInfoW(IntPtr sesid, IntPtr tableid, string szColumnName, ref NATIVE_COLUMNBASE_WIDE columnbase, uint cbMax, uint InfoLevel);

		// Token: 0x060002D3 RID: 723
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetTableColumnInfoW(IntPtr sesid, IntPtr tableid, ref uint pcolumnid, ref NATIVE_COLUMNBASE_WIDE columnbase, uint cbMax, uint InfoLevel);

		// Token: 0x060002D4 RID: 724
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableColumnInfoW(IntPtr sesid, IntPtr tableid, string szIgnored, ref NATIVE_COLUMNLIST columnlist, uint cbMax, uint InfoLevel);

		// Token: 0x060002D5 RID: 725
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetColumnInfo(IntPtr sesid, uint dbid, string szTableName, string szColumnName, ref NATIVE_COLUMNDEF columndef, uint cbMax, uint InfoLevel);

		// Token: 0x060002D6 RID: 726
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetColumnInfo(IntPtr sesid, uint dbid, string szTableName, string szColumnName, ref NATIVE_COLUMNLIST columnlist, uint cbMax, uint InfoLevel);

		// Token: 0x060002D7 RID: 727
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetColumnInfo(IntPtr sesid, uint dbid, string szTableName, string szColumnName, ref NATIVE_COLUMNBASE columnbase, uint cbMax, uint InfoLevel);

		// Token: 0x060002D8 RID: 728
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetColumnInfo(IntPtr sesid, uint dbid, string szTableName, ref uint pcolumnid, ref NATIVE_COLUMNBASE columnbase, uint cbMax, uint InfoLevel);

		// Token: 0x060002D9 RID: 729
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetColumnInfoW(IntPtr sesid, uint dbid, string szTableName, string szColumnName, ref NATIVE_COLUMNDEF columndef, uint cbMax, uint InfoLevel);

		// Token: 0x060002DA RID: 730
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetColumnInfoW(IntPtr sesid, uint dbid, string szTableName, string szColumnName, ref NATIVE_COLUMNLIST columnlist, uint cbMax, uint InfoLevel);

		// Token: 0x060002DB RID: 731
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetColumnInfoW(IntPtr sesid, uint dbid, string szTableName, string szColumnName, ref NATIVE_COLUMNBASE_WIDE columnbase, uint cbMax, uint InfoLevel);

		// Token: 0x060002DC RID: 732
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetColumnInfoW(IntPtr sesid, uint dbid, string szTableName, ref uint pcolumnid, ref NATIVE_COLUMNBASE_WIDE columnbase, uint cbMax, uint InfoLevel);

		// Token: 0x060002DD RID: 733
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetObjectInfo(IntPtr sesid, uint dbid, uint objtyp, string szContainerName, string szObjectName, [In] [Out] ref NATIVE_OBJECTLIST objectlist, uint cbMax, uint InfoLevel);

		// Token: 0x060002DE RID: 734
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetObjectInfoW(IntPtr sesid, uint dbid, uint objtyp, string szContainerName, string szObjectName, [In] [Out] ref NATIVE_OBJECTLIST objectlist, uint cbMax, uint InfoLevel);

		// Token: 0x060002DF RID: 735
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetObjectInfo(IntPtr sesid, uint dbid, uint objtyp, string szContainerName, string szObjectName, [In] [Out] ref NATIVE_OBJECTINFO objectinfo, uint cbMax, uint InfoLevel);

		// Token: 0x060002E0 RID: 736
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetObjectInfoW(IntPtr sesid, uint dbid, uint objtyp, string szContainerName, string szObjectName, [In] [Out] ref NATIVE_OBJECTINFO objectinfo, uint cbMax, uint InfoLevel);

		// Token: 0x060002E1 RID: 737
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetCurrentIndex(IntPtr sesid, IntPtr tableid, [Out] StringBuilder szIndexName, uint cchIndexName);

		// Token: 0x060002E2 RID: 738
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableInfo(IntPtr sesid, IntPtr tableid, out NATIVE_OBJECTINFO pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002E3 RID: 739
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableInfo(IntPtr sesid, IntPtr tableid, out uint pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002E4 RID: 740
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableInfo(IntPtr sesid, IntPtr tableid, [Out] int[] pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002E5 RID: 741
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableInfo(IntPtr sesid, IntPtr tableid, [Out] StringBuilder pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002E6 RID: 742
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableInfoW(IntPtr sesid, IntPtr tableid, out NATIVE_OBJECTINFO pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002E7 RID: 743
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableInfoW(IntPtr sesid, IntPtr tableid, out uint pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002E8 RID: 744
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableInfoW(IntPtr sesid, IntPtr tableid, [Out] int[] pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002E9 RID: 745
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableInfoW(IntPtr sesid, IntPtr tableid, [Out] StringBuilder pvResult, uint cbMax, uint infoLevel);

		// Token: 0x060002EA RID: 746
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetIndexInfo(IntPtr sesid, uint dbid, string szTableName, string szIndexName, out ushort result, uint cbResult, uint InfoLevel);

		// Token: 0x060002EB RID: 747
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetIndexInfo(IntPtr sesid, uint dbid, string szTableName, string szIndexName, out uint result, uint cbResult, uint InfoLevel);

		// Token: 0x060002EC RID: 748
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetIndexInfo(IntPtr sesid, uint dbid, string szTableName, string szIndexName, out JET_INDEXID result, uint cbResult, uint InfoLevel);

		// Token: 0x060002ED RID: 749
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetIndexInfo(IntPtr sesid, uint dbid, string szTableName, string szIndexName, [In] [Out] ref NATIVE_INDEXLIST result, uint cbResult, uint InfoLevel);

		// Token: 0x060002EE RID: 750
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetIndexInfoW(IntPtr sesid, uint dbid, string szTableName, string szIndexName, out ushort result, uint cbResult, uint InfoLevel);

		// Token: 0x060002EF RID: 751
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetIndexInfoW(IntPtr sesid, uint dbid, string szTableName, string szIndexName, out uint result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F0 RID: 752
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetIndexInfoW(IntPtr sesid, uint dbid, string szTableName, string szIndexName, out JET_INDEXID result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F1 RID: 753
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetIndexInfoW(IntPtr sesid, uint dbid, string szTableName, string szIndexName, [In] [Out] ref NATIVE_INDEXLIST result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F2 RID: 754
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetIndexInfoW(IntPtr sesid, uint dbid, string szTableName, string szIndexName, [Out] StringBuilder result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F3 RID: 755
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfo(IntPtr sesid, IntPtr tableid, string szIndexName, out ushort result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F4 RID: 756
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfo(IntPtr sesid, IntPtr tableid, string szIndexName, out uint result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F5 RID: 757
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfo(IntPtr sesid, IntPtr tableid, string szIndexName, out JET_INDEXID result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F6 RID: 758
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfo(IntPtr sesid, IntPtr tableid, string szIndexName, [In] [Out] ref NATIVE_INDEXLIST result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F7 RID: 759
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfoW(IntPtr sesid, IntPtr tableid, string szIndexName, out ushort result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F8 RID: 760
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfoW(IntPtr sesid, IntPtr tableid, string szIndexName, out uint result, uint cbResult, uint InfoLevel);

		// Token: 0x060002F9 RID: 761
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfoW(IntPtr sesid, IntPtr tableid, string szIndexName, out JET_INDEXID result, uint cbResult, uint InfoLevel);

		// Token: 0x060002FA RID: 762
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfoW(IntPtr sesid, IntPtr tableid, string szIndexName, [In] [Out] ref NATIVE_INDEXLIST result, uint cbResult, uint InfoLevel);

		// Token: 0x060002FB RID: 763
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetGetTableIndexInfoW(IntPtr sesid, IntPtr tableid, string szIndexName, [Out] StringBuilder result, uint cbResult, uint InfoLevel);

		// Token: 0x060002FC RID: 764
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetRenameTable(IntPtr sesid, uint dbid, string szName, string szNameNew);

		// Token: 0x060002FD RID: 765
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetRenameColumn(IntPtr sesid, IntPtr tableid, string szName, string szNameNew, uint grbit);

		// Token: 0x060002FE RID: 766
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetSetColumnDefaultValue(IntPtr sesid, uint tableid, [MarshalAs(UnmanagedType.LPStr)] string szTableName, [MarshalAs(UnmanagedType.LPStr)] string szColumnName, byte[] pvData, uint cbData, uint grbit);

		// Token: 0x060002FF RID: 767
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGotoBookmark(IntPtr sesid, IntPtr tableid, [In] byte[] pvBookmark, uint cbBookmark);

		// Token: 0x06000300 RID: 768
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGotoSecondaryIndexBookmark(IntPtr sesid, IntPtr tableid, [In] byte[] pvSecondaryKey, uint cbSecondaryKey, [In] byte[] pvPrimaryBookmark, uint cbPrimaryBookmark, uint grbit);

		// Token: 0x06000301 RID: 769
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetMove(IntPtr sesid, IntPtr tableid, int cRow, uint grbit);

		// Token: 0x06000302 RID: 770
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetMakeKey(IntPtr sesid, IntPtr tableid, IntPtr pvData, uint cbData, uint grbit);

		// Token: 0x06000303 RID: 771
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSeek(IntPtr sesid, IntPtr tableid, uint grbit);

		// Token: 0x06000304 RID: 772
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetIndexRange(IntPtr sesid, IntPtr tableid, uint grbit);

		// Token: 0x06000305 RID: 773
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetIntersectIndexes(IntPtr sesid, [In] NATIVE_INDEXRANGE[] rgindexrange, uint cindexrange, [In] [Out] ref NATIVE_RECORDLIST recordlist, uint grbit);

		// Token: 0x06000306 RID: 774
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetSetCurrentIndex(IntPtr sesid, IntPtr tableid, string szIndexName);

		// Token: 0x06000307 RID: 775
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetSetCurrentIndex2(IntPtr sesid, IntPtr tableid, string szIndexName, uint grbit);

		// Token: 0x06000308 RID: 776
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetSetCurrentIndex3(IntPtr sesid, IntPtr tableid, string szIndexName, uint grbit, uint itagSequence);

		// Token: 0x06000309 RID: 777
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetSetCurrentIndex4(IntPtr sesid, IntPtr tableid, string szIndexName, [In] ref JET_INDEXID indexid, uint grbit, uint itagSequence);

		// Token: 0x0600030A RID: 778
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetIndexRecordCount(IntPtr sesid, IntPtr tableid, out uint crec, uint crecMax);

		// Token: 0x0600030B RID: 779
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetTableSequential(IntPtr sesid, IntPtr tableid, uint grbit);

		// Token: 0x0600030C RID: 780
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetResetTableSequential(IntPtr sesid, IntPtr tableid, uint grbit);

		// Token: 0x0600030D RID: 781
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetRecordPosition(IntPtr sesid, IntPtr tableid, out NATIVE_RECPOS precpos, uint cbRecpos);

		// Token: 0x0600030E RID: 782
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGotoPosition(IntPtr sesid, IntPtr tableid, [In] ref NATIVE_RECPOS precpos);

		// Token: 0x0600030F RID: 783
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetPrereadKeys(IntPtr sesid, IntPtr tableid, void** rgpvKeys, uint* rgcbKeys, int ckeys, out int pckeysPreread, uint grbit);

		// Token: 0x06000310 RID: 784
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetBookmark(IntPtr sesid, IntPtr tableid, [Out] byte[] pvBookmark, uint cbMax, out uint cbActual);

		// Token: 0x06000311 RID: 785
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetSecondaryIndexBookmark(IntPtr sesid, IntPtr tableid, [Out] byte[] secondaryKey, uint secondaryKeySize, out uint actualSecondaryKeySize, [Out] byte[] primaryKey, uint primaryKeySize, out uint actualPrimaryKeySize, uint grbit);

		// Token: 0x06000312 RID: 786
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetRetrieveColumn(IntPtr sesid, IntPtr tableid, uint columnid, IntPtr pvData, uint cbData, out uint cbActual, uint grbit, IntPtr pretinfo);

		// Token: 0x06000313 RID: 787
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetRetrieveColumn(IntPtr sesid, IntPtr tableid, uint columnid, IntPtr pvData, uint cbData, out uint cbActual, uint grbit, [In] [Out] ref NATIVE_RETINFO pretinfo);

		// Token: 0x06000314 RID: 788
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetRetrieveColumns(IntPtr sesid, IntPtr tableid, [In] [Out] NATIVE_RETRIEVECOLUMN* psetcolumn, uint csetcolumn);

		// Token: 0x06000315 RID: 789
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetRetrieveKey(IntPtr sesid, IntPtr tableid, [Out] byte[] pvData, uint cbMax, out uint cbActual, uint grbit);

		// Token: 0x06000316 RID: 790
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetEnumerateColumns(IntPtr sesid, IntPtr tableid, uint cEnumColumnId, NATIVE_ENUMCOLUMNID* rgEnumColumnId, out uint pcEnumColumn, out NATIVE_ENUMCOLUMN* prgEnumColumn, JET_PFNREALLOC pfnRealloc, IntPtr pvReallocContext, uint cbDataMost, uint grbit);

		// Token: 0x06000317 RID: 791
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetRecordSize(IntPtr sesid, IntPtr tableid, ref NATIVE_RECSIZE precsize, uint grbit);

		// Token: 0x06000318 RID: 792
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetRecordSize2(IntPtr sesid, IntPtr tableid, ref NATIVE_RECSIZE2 precsize, uint grbit);

		// Token: 0x06000319 RID: 793
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetDelete(IntPtr sesid, IntPtr tableid);

		// Token: 0x0600031A RID: 794
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetPrepareUpdate(IntPtr sesid, IntPtr tableid, uint prep);

		// Token: 0x0600031B RID: 795
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetUpdate(IntPtr sesid, IntPtr tableid, [Out] byte[] pvBookmark, uint cbBookmark, out uint cbActual);

		// Token: 0x0600031C RID: 796
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetUpdate2(IntPtr sesid, IntPtr tableid, [Out] byte[] pvBookmark, uint cbBookmark, out uint cbActual, uint grbit);

		// Token: 0x0600031D RID: 797
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetColumn(IntPtr sesid, IntPtr tableid, uint columnid, IntPtr pvData, uint cbData, uint grbit, IntPtr psetinfo);

		// Token: 0x0600031E RID: 798
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetColumn(IntPtr sesid, IntPtr tableid, uint columnid, IntPtr pvData, uint cbData, uint grbit, [In] ref NATIVE_SETINFO psetinfo);

		// Token: 0x0600031F RID: 799
		[DllImport("ese.dll", ExactSpelling = true)]
		public unsafe static extern int JetSetColumns(IntPtr sesid, IntPtr tableid, [In] [Out] NATIVE_SETCOLUMN* psetcolumn, uint csetcolumn);

		// Token: 0x06000320 RID: 800
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetLock(IntPtr sesid, IntPtr tableid, uint grbit);

		// Token: 0x06000321 RID: 801
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetEscrowUpdate(IntPtr sesid, IntPtr tableid, uint columnid, [In] byte[] pv, uint cbMax, [Out] byte[] pvOld, uint cbOldMax, out uint cbOldActual, uint grbit);

		// Token: 0x06000322 RID: 802
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetRegisterCallback(IntPtr sesid, IntPtr tableid, uint cbtyp, NATIVE_CALLBACK callback, IntPtr pvContext, out IntPtr pCallbackId);

		// Token: 0x06000323 RID: 803
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetUnregisterCallback(IntPtr sesid, IntPtr tableid, uint cbtyp, IntPtr hCallbackId);

		// Token: 0x06000324 RID: 804
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetDefragment(IntPtr sesid, uint dbid, string szTableName, ref uint pcPasses, ref uint pcSeconds, uint grbit);

		// Token: 0x06000325 RID: 805
		[DllImport("ese.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern int JetDefragment2(IntPtr sesid, uint dbid, string szTableName, ref uint pcPasses, ref uint pcSeconds, IntPtr callback, uint grbit);

		// Token: 0x06000326 RID: 806
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetDefragment2W(IntPtr sesid, uint dbid, string szTableName, ref uint pcPasses, ref uint pcSeconds, IntPtr callback, uint grbit);

		// Token: 0x06000327 RID: 807
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetIdle(IntPtr sesid, uint grbit);

		// Token: 0x06000328 RID: 808
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetConfigureProcessForCrashDump(uint grbit);

		// Token: 0x06000329 RID: 809
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetFreeBuffer(IntPtr pbBuf);

		// Token: 0x0600032A RID: 810
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetGetErrorInfoW(ref int error, [In] [Out] ref NATIVE_ERRINFOBASIC pvResult, uint cbMax, uint InfoLevel, uint grbit);

		// Token: 0x0600032B RID: 811
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetResizeDatabase(IntPtr sesid, uint dbid, uint cpg, out uint pcpgActual, uint grbit);

		// Token: 0x0600032C RID: 812
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateIndex4W(IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEXCREATE3[] pindexcreate, uint cIndexCreate);

		// Token: 0x0600032D RID: 813
		[DllImport("ese.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int JetCreateTableColumnIndex4W(IntPtr sesid, uint dbid, ref NATIVE_TABLECREATE4 tablecreate3);

		// Token: 0x0600032E RID: 814
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetOpenTemporaryTable2(IntPtr sesid, [In] [Out] ref NATIVE_OPENTEMPORARYTABLE2 popentemporarytable);

		// Token: 0x0600032F RID: 815
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetSessionParameter(IntPtr sesid, uint sesparamid, IntPtr data, int dataSize);

		// Token: 0x06000330 RID: 816
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetCommitTransaction2(IntPtr sesid, uint grbit, uint cmsecDurableCommit, ref NATIVE_COMMIT_ID pCommitId);

		// Token: 0x06000331 RID: 817
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetPrereadIndexRanges(IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEX_RANGE[] pIndexRanges, uint cIndexRanges, out int pcRangesPreread, uint[] rgcolumnidPreread, uint ccolumnidPreread, uint grbit);

		// Token: 0x06000332 RID: 818
		[DllImport("ese.dll", ExactSpelling = true)]
		public static extern int JetSetCursorFilter(IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEX_COLUMN[] pFilters, uint cFilters, uint grbit);

		// Token: 0x0400006D RID: 109
		private const string EsentDll = "ese.dll";

		// Token: 0x0400006E RID: 110
		private const string Eseback2Dll = "eseback2.dll";

		// Token: 0x0400006F RID: 111
		private const CharSet EsentCharSet = CharSet.Ansi;
	}
}
