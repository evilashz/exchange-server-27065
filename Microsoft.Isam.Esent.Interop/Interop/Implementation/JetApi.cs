using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Isam.Esent.Interop.Server2003;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Isam.Esent.Interop.Vista;
using Microsoft.Isam.Esent.Interop.Win32;
using Microsoft.Isam.Esent.Interop.Windows7;
using Microsoft.Isam.Esent.Interop.Windows8;

namespace Microsoft.Isam.Esent.Interop.Implementation
{
	// Token: 0x02000025 RID: 37
	internal sealed class JetApi : IJetApi
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00003322 File Offset: 0x00001522
		internal static void ReportUnhandledException(Exception exception, string description)
		{
			ExWatson.SendReportAndCrashOnAnotherThread(exception, ReportOptions.None, null, description);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003330 File Offset: 0x00001530
		private void DetermineCapabilities()
		{
			this.Capabilities = new JetCapabilities();
			if (this.versionOverride == 84557192U)
			{
				this.Capabilities.ColumnsKeyMost = 12;
				return;
			}
			if (this.versionOverride == 1612148993U)
			{
				this.Capabilities.SupportsServer2003Features = true;
				this.Capabilities.SupportsVistaFeatures = true;
				this.Capabilities.SupportsUnicodePaths = true;
				this.Capabilities.SupportsLargeKeys = true;
				this.Capabilities.ColumnsKeyMost = 16;
				return;
			}
			if (this.versionOverride == 1612558593U)
			{
				this.Capabilities.SupportsServer2003Features = true;
				this.Capabilities.SupportsVistaFeatures = true;
				this.Capabilities.SupportsWindows7Features = true;
				this.Capabilities.SupportsUnicodePaths = true;
				this.Capabilities.SupportsLargeKeys = true;
				this.Capabilities.ColumnsKeyMost = 16;
				return;
			}
			this.Capabilities.SupportsServer2003Features = true;
			this.Capabilities.SupportsVistaFeatures = true;
			this.Capabilities.SupportsUnicodePaths = true;
			this.Capabilities.SupportsLargeKeys = true;
			this.Capabilities.ColumnsKeyMost = 16;
			this.Capabilities.SupportsWindows7Features = true;
			this.Capabilities.SupportsWindows8Features = true;
			this.Capabilities.SupportsWindows81Features = true;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003466 File Offset: 0x00001666
		public int JetTracing(JET_traceop operation, JET_tracetag tag, bool value)
		{
			return JetApi.Err(NativeMethods.JetTracing((int)operation, (int)tag, new IntPtr(value ? 1 : 0)));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00003480 File Offset: 0x00001680
		public int JetTracing(JET_traceop operation, JET_tracetag tag, JET_DBID value)
		{
			return JetApi.Err(NativeMethods.JetTracing((int)operation, (int)tag, new IntPtr((long)((ulong)value.Value))));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000349B File Offset: 0x0000169B
		public int JetTracing(JET_traceop operation, JET_tracetag tag, int value)
		{
			return JetApi.Err(NativeMethods.JetTracing((int)operation, (int)tag, new IntPtr(value)));
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000034B0 File Offset: 0x000016B0
		public int JetTracing(JET_traceop operation, JET_tracetag tag, JET_PFNTRACEREGISTER callback)
		{
			IntPtr ul = (callback == null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(callback);
			int result = JetApi.Err(NativeMethods.JetTracing((int)operation, (int)tag, ul));
			GC.KeepAlive(callback);
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000034E4 File Offset: 0x000016E4
		public int JetTracing(JET_traceop operation, JET_tracetag tag, JET_PFNTRACEEMIT callback)
		{
			JetApi.traceCallback = callback;
			IntPtr ul = (callback == null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(callback);
			return JetApi.Err(NativeMethods.JetTracing((int)operation, (int)tag, ul));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003518 File Offset: 0x00001718
		public int JetSetResourceParam(JET_INSTANCE instance, JET_resoper resoper, JET_resid resid, long longParameter)
		{
			IntPtr ulParam = new IntPtr(longParameter);
			return JetApi.Err(NativeMethods.JetSetResourceParam(instance, resoper, resid, ulParam));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003540 File Offset: 0x00001740
		public int JetGetResourceParam(JET_INSTANCE instance, JET_resoper resoper, JET_resid resid, out long paramValue)
		{
			IntPtr intPtr;
			int result = JetApi.Err(NativeMethods.JetGetResourceParam(instance, resoper, resid, out intPtr));
			paramValue = intPtr.ToInt64();
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003568 File Offset: 0x00001768
		public unsafe int JetConsumeLogData(JET_INSTANCE instance, JET_EMITDATACTX emitLogDataCtx, byte[] logDataBuf, int logDataStartOffset, int logDataLength, ShadowLogConsumeGrbit grbit)
		{
			JetApi.CheckDataSize<byte>(logDataBuf, logDataStartOffset, "logDataStartOffset", logDataLength, "logDataLength");
			NATIVE_EMITDATACTX nativeEmitdatactx = emitLogDataCtx.GetNativeEmitdatactx();
			int result;
			fixed (byte* ptr = logDataBuf)
			{
				byte* pvLogData = null;
				if (ptr != null)
				{
					pvLogData = ptr + logDataStartOffset;
				}
				result = JetApi.Err(NativeMethods.JetConsumeLogData(instance, &nativeEmitdatactx, pvLogData, (uint)logDataLength, (uint)grbit));
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000035D4 File Offset: 0x000017D4
		public int JetGetLogFileInfo(string logFileName, out JET_LOGINFOMISC info, JET_LogInfo infoLevel)
		{
			info = null;
			switch (infoLevel)
			{
			case JET_LogInfo.Misc:
				throw new NotImplementedException("Only JET_LogInfo.Misc2 is currently implemented.");
			case JET_LogInfo.Misc2:
			{
				NATIVE_LOGINFOMISC2 native_LOGINFOMISC = default(NATIVE_LOGINFOMISC2);
				int num = JetApi.Err(NativeMethods.JetGetLogFileInfoW(logFileName, out native_LOGINFOMISC, (uint)Marshal.SizeOf(typeof(NATIVE_LOGINFOMISC2)), (uint)infoLevel));
				if (num >= 0)
				{
					info = new JET_LOGINFOMISC();
					info.SetFromNativeLoginfoMisc(ref native_LOGINFOMISC);
				}
				return num;
			}
			default:
				throw new ArgumentException("Unsupported JET_LogInfo passed in.", "infoLevel");
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003650 File Offset: 0x00001850
		public unsafe int JetGetPageInfo2(byte[] bytesPages, int bytesPagesLength, JET_PAGEINFO[] pageInfos, PageInfoGrbit grbit, JET_PageInfo infoLevel)
		{
			JetApi.CheckNotNull(bytesPages, "bytesPages");
			JetApi.CheckDataSize<byte>(bytesPages, bytesPagesLength, "bytesPagesLength");
			JetApi.CheckNotNull(pageInfos, "pageInfos");
			if (infoLevel != JET_PageInfo.Level1)
			{
				throw new NotImplementedException("Only JET_PageInfo.Level1 is currently implemented.");
			}
			int num = pageInfos.Length * Marshal.SizeOf(typeof(NATIVE_PAGEINFO));
			NATIVE_PAGEINFO[] nativePageinfos = JetApi.GetNativePageinfos(pageInfos, out num);
			int num2 = nativePageinfos.Length;
			IntPtr intPtr = NativeMethods.VirtualAlloc(IntPtr.Zero, (UIntPtr)((ulong)((long)bytesPagesLength)), 12288U, 4U);
			NativeMethods.ThrowExceptionOnNull(intPtr, "VirtualAlloc");
			int num3;
			try
			{
				Marshal.Copy(bytesPages, 0, intPtr, bytesPagesLength);
				try
				{
					fixed (NATIVE_PAGEINFO* ptr = &nativePageinfos[0])
					{
						num3 = JetApi.Err(checked(NativeMethods.JetGetPageInfo2(intPtr, (uint)bytesPagesLength, ptr, (uint)num, (uint)grbit, (uint)infoLevel)));
					}
				}
				finally
				{
					NATIVE_PAGEINFO* ptr = null;
				}
			}
			finally
			{
				bool success = NativeMethods.VirtualFree(intPtr, UIntPtr.Zero, 32768U);
				NativeMethods.ThrowExceptionOnFailure(success, "VirtualFree");
			}
			if (num3 >= 0)
			{
				for (int i = 0; i < num2; i++)
				{
					pageInfos[i].SetFromNativePageInfo(ref nativePageinfos[i]);
				}
			}
			return num3;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000376C File Offset: 0x0000196C
		public int JetGetInstanceMiscInfo(JET_INSTANCE instance, out JET_CHECKPOINTINFO checkpointInfo, JET_InstanceMiscInfo infoLevel)
		{
			NATIVE_CHECKPOINTINFO native_CHECKPOINTINFO = default(NATIVE_CHECKPOINTINFO);
			int err = NativeMethods.JetGetInstanceMiscInfo(instance.Value, ref native_CHECKPOINTINFO, checked((uint)NATIVE_CHECKPOINTINFO.Size), (uint)infoLevel);
			checkpointInfo = new JET_CHECKPOINTINFO();
			checkpointInfo.SetFromNativeCheckpointInfo(ref native_CHECKPOINTINFO);
			return JetApi.Err(err);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000037AD File Offset: 0x000019AD
		public int JetBeginDatabaseIncrementalReseed(JET_INSTANCE instance, string wszDatabase, BeginDatabaseIncrementalReseedGrbit grbit)
		{
			JetApi.CheckNotNull(wszDatabase, "wszDatabase");
			return JetApi.Err(NativeMethods.JetBeginDatabaseIncrementalReseedW(instance, wszDatabase, (uint)grbit));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000037C7 File Offset: 0x000019C7
		public int JetEndDatabaseIncrementalReseed(JET_INSTANCE instance, string wszDatabase, int genMinRequired, int genFirstDivergedLog, int genMaxRequired, EndDatabaseIncrementalReseedGrbit grbit)
		{
			JetApi.CheckNotNull(wszDatabase, "wszDatabase");
			return JetApi.Err(checked(NativeMethods.JetEndDatabaseIncrementalReseedW(instance, wszDatabase, (uint)genMinRequired, (uint)genFirstDivergedLog, (uint)genMaxRequired, (uint)grbit)));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000037EC File Offset: 0x000019EC
		public unsafe int JetPatchDatabasePages(JET_INSTANCE instance, string databaseFileName, int pgnoStart, int pageCount, byte[] inputData, int dataLength, PatchDatabasePagesGrbit grbit)
		{
			JetApi.CheckNotNull(databaseFileName, "databaseFileName");
			JetApi.CheckDataSize<byte>(inputData, dataLength, "inputData");
			fixed (byte* ptr = inputData)
			{
				return JetApi.Err(checked(NativeMethods.JetPatchDatabasePagesW(instance, databaseFileName, (uint)pgnoStart, (uint)pageCount, ptr, (uint)dataLength, (uint)grbit)));
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003846 File Offset: 0x00001A46
		public int JetRemoveLogfile(string databaseFileName, string logFileName, RemoveLogfileGrbit grbit)
		{
			JetApi.CheckNotNull(databaseFileName, "databaseFileName");
			JetApi.CheckNotNull(logFileName, "logFileName");
			return JetApi.Err(NativeMethods.JetRemoveLogfileW(databaseFileName, logFileName, (uint)grbit));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000386C File Offset: 0x00001A6C
		public int JetGetDatabasePages(JET_SESID sesid, JET_DBID dbid, int pgnoStart, int countPages, byte[] pageBytes, int pageBytesLength, out int pageBytesRead, GetDatabasePagesGrbit grbit)
		{
			JetApi.CheckNotNull(pageBytes, "pageBytes");
			JetApi.CheckDataSize<byte>(pageBytes, pageBytesLength, "pageBytesLength");
			JetApi.CheckNotNegative(pgnoStart, "pgnoStart");
			JetApi.CheckNotNegative(countPages, "countPages");
			JetApi.CheckNotNegative(pageBytesLength, "pageBytesLength");
			pageBytesRead = 0;
			IntPtr intPtr = NativeMethods.VirtualAlloc(IntPtr.Zero, (UIntPtr)((ulong)((long)pageBytesLength)), 12288U, 4U);
			NativeMethods.ThrowExceptionOnNull(intPtr, "VirtualAlloc");
			int num;
			try
			{
				uint num2;
				num = JetApi.Err(checked(NativeMethods.JetGetDatabasePages(sesid.Value, dbid.Value, (uint)pgnoStart, (uint)countPages, intPtr, (uint)pageBytesLength, out num2, (uint)grbit)));
				if (num >= 0)
				{
					pageBytesRead = (int)num2;
					Marshal.Copy(intPtr, pageBytes, 0, pageBytesRead);
				}
			}
			finally
			{
				bool success = NativeMethods.VirtualFree(intPtr, UIntPtr.Zero, 32768U);
				NativeMethods.ThrowExceptionOnFailure(success, "VirtualFree");
			}
			return num;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003948 File Offset: 0x00001B48
		public unsafe int JetDBUtilities(JET_DBUTIL dbutil)
		{
			JetApi.CheckNotNull(dbutil, "dbutil");
			dbutil.CheckMembersAreValid();
			if (dbutil.op == DBUTIL_OP.ChecksumLogFromMemory)
			{
				NATIVE_DBUTIL_CHECKSUMLOG nativeDbutilChecksumLog = dbutil.GetNativeDbutilChecksumLog();
				fixed (byte* ptr = &dbutil.pvBuffer[0])
				{
					IntPtr pvBuffer = (IntPtr)((void*)ptr);
					nativeDbutilChecksumLog.pvBuffer = pvBuffer;
					nativeDbutilChecksumLog.cbBuffer = dbutil.pvBuffer.Length;
					return JetApi.Err(NativeMethods.JetDBUtilitiesW(ref nativeDbutilChecksumLog));
				}
			}
			NATIVE_DBUTIL_LEGACY nativeDbutilLegacy = dbutil.GetNativeDbutilLegacy();
			return JetApi.Err(NativeMethods.JetDBUtilitiesW(ref nativeDbutilLegacy));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000039C9 File Offset: 0x00001BC9
		public int JetTestHook(int opcode, ref uint pv)
		{
			return JetApi.Err(NativeMethods.JetTestHook(opcode, ref pv));
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000039D7 File Offset: 0x00001BD7
		public int JetTestHook(int opcode, ref NATIVE_TESTHOOKTESTINJECTION pv)
		{
			this.CheckSupportsWindows7Features("JetTestHook");
			return JetApi.Err(NativeMethods.JetTestHook(opcode, ref pv));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000039F0 File Offset: 0x00001BF0
		public int JetTestHook(int opcode, JET_TESTHOOKEVICTCACHE testEvictCachedPage)
		{
			this.CheckSupportsWindows7Features("JetTestHook");
			NATIVE_TESTHOOKEVICTCACHE nativeTestHookEvictCache = testEvictCachedPage.GetNativeTestHookEvictCache();
			return JetApi.Err(NativeMethods.JetTestHook(opcode, ref nativeTestHookEvictCache));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003A1C File Offset: 0x00001C1C
		public int JetTestHook(int opcode, ref IntPtr pv)
		{
			return JetApi.Err(NativeMethods.JetTestHook(opcode, ref pv));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003A2A File Offset: 0x00001C2A
		public int JetTestHook(int opcode, ref NATIVE_TESTHOOKTRACETESTMARKER pv)
		{
			return JetApi.Err(NativeMethods.JetTestHook(opcode, ref pv));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003A38 File Offset: 0x00001C38
		public int JetTestHook(int opcode, ref NATIVE_TESTHOOKCORRUPT_DATABASEFILE pv)
		{
			return JetApi.Err(NativeMethods.JetTestHook(opcode, ref pv));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003A48 File Offset: 0x00001C48
		public int JetPrereadTables(JET_SESID sesid, JET_DBID dbid, string[] rgsz, PrereadTablesGrbit grbit)
		{
			int err = NativeMethods.JetPrereadTablesW(sesid.Value, dbid.Value, rgsz, rgsz.Length, (int)grbit);
			return JetApi.Err(err);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00003A78 File Offset: 0x00001C78
		public int JetDatabaseScan(JET_SESID sesid, JET_DBID dbid, ref int seconds, int sleepInMsec, JET_CALLBACK callback, DatabaseScanGrbit grbit)
		{
			uint num = (uint)seconds;
			IntPtr callback2;
			if (callback == null)
			{
				callback2 = IntPtr.Zero;
			}
			else
			{
				JetCallbackWrapper jetCallbackWrapper = this.callbackWrappers.Add(callback);
				callback2 = Marshal.GetFunctionPointerForDelegate(jetCallbackWrapper.NativeCallback);
			}
			int result = JetApi.Err(NativeMethods.JetDatabaseScan(sesid.Value, dbid.Value, ref num, (uint)sleepInMsec, callback2, (uint)grbit));
			seconds = (int)num;
			this.callbackWrappers.Collect();
			return result;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public unsafe int JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, int valueToSet)
		{
			this.CheckSupportsWindows8Features("JetSetSessionParameter");
			void* value = (void*)(&valueToSet);
			IntPtr data = (IntPtr)value;
			int err = NativeMethods.JetSetSessionParameter(sesid.Value, (uint)sesparamid, data, 4);
			return JetApi.Err(err);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003B20 File Offset: 0x00001D20
		public unsafe int JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, ref NATIVE_OPERATIONCONTEXT operationContext)
		{
			this.CheckSupportsWindows8Features("JetSetSessionParameter");
			int dataSize = Marshal.SizeOf(operationContext);
			int err;
			fixed (IntPtr* ptr = (IntPtr*)(&operationContext))
			{
				IntPtr data = (IntPtr)((void*)ptr);
				err = NativeMethods.JetSetSessionParameter(sesid.Value, (uint)sesparamid, data, dataSize);
			}
			return JetApi.Err(err);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003B6C File Offset: 0x00001D6C
		private static NATIVE_PAGEINFO[] GetNativePageinfos(IList<JET_PAGEINFO> managedPageinfos, out int bytesNativePageinfos)
		{
			NATIVE_PAGEINFO[] array = new NATIVE_PAGEINFO[managedPageinfos.Count];
			bytesNativePageinfos = 0;
			if (managedPageinfos != null && managedPageinfos.Count > 0)
			{
				array = new NATIVE_PAGEINFO[managedPageinfos.Count];
				for (int i = 0; i < managedPageinfos.Count; i++)
				{
					array[i] = managedPageinfos[i].GetNativePageinfo();
					bytesNativePageinfos += Marshal.SizeOf(typeof(NATIVE_PAGEINFO));
				}
			}
			return array;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private void NotYetPublishedGetDbinfomisc(string databaseName, ref JET_DBINFOMISC dbinfomisc, JET_DbInfo infoLevel, ref bool notYetPublishedSupported, ref int err)
		{
			NATIVE_DBINFOMISC7 native_DBINFOMISC;
			err = JetApi.Err(NativeMethods.JetGetDatabaseFileInfoW(databaseName, out native_DBINFOMISC, (uint)Marshal.SizeOf(typeof(NATIVE_DBINFOMISC7)), (uint)infoLevel));
			dbinfomisc = new JET_DBINFOMISC();
			dbinfomisc.SetFromNativeDbinfoMisc(ref native_DBINFOMISC);
			notYetPublishedSupported = true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003C24 File Offset: 0x00001E24
		static JetApi()
		{
			RuntimeHelpers.PrepareMethod(typeof(JetApi).GetMethod("JetCreateInstance").MethodHandle);
			RuntimeHelpers.PrepareMethod(typeof(JetApi).GetMethod("JetCreateInstance2").MethodHandle);
			RuntimeHelpers.PrepareMethod(typeof(JetApi).GetMethod("JetInit").MethodHandle);
			RuntimeHelpers.PrepareMethod(typeof(JetApi).GetMethod("JetInit2").MethodHandle);
			RuntimeHelpers.PrepareMethod(typeof(JetApi).GetMethod("JetInit3").MethodHandle);
			RuntimeHelpers.PrepareMethod(typeof(JetApi).GetMethod("JetTerm").MethodHandle);
			RuntimeHelpers.PrepareMethod(typeof(JetApi).GetMethod("JetTerm2").MethodHandle);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003D17 File Offset: 0x00001F17
		public JetApi(uint version)
		{
			this.versionOverride = version;
			this.DetermineCapabilities();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003D37 File Offset: 0x00001F37
		public JetApi()
		{
			this.DetermineCapabilities();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00003D50 File Offset: 0x00001F50
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00003D58 File Offset: 0x00001F58
		public JetCapabilities Capabilities { get; private set; }

		// Token: 0x06000172 RID: 370 RVA: 0x00003D61 File Offset: 0x00001F61
		public int JetCreateInstance(out JET_INSTANCE instance, string name)
		{
			instance.Value = IntPtr.Zero;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetCreateInstanceW(out instance.Value, name));
			}
			return JetApi.Err(NativeMethods.JetCreateInstance(out instance.Value, name));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public int JetCreateInstance2(out JET_INSTANCE instance, string name, string displayName, CreateInstanceGrbit grbit)
		{
			instance.Value = IntPtr.Zero;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetCreateInstance2W(out instance.Value, name, displayName, (uint)grbit));
			}
			return JetApi.Err(NativeMethods.JetCreateInstance2(out instance.Value, name, displayName, (uint)grbit));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00003DEE File Offset: 0x00001FEE
		public int JetInit(ref JET_INSTANCE instance)
		{
			return JetApi.Err(NativeMethods.JetInit(ref instance.Value));
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00003E00 File Offset: 0x00002000
		public int JetInit2(ref JET_INSTANCE instance, InitGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetInit2(ref instance.Value, (uint)grbit));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00003E14 File Offset: 0x00002014
		public unsafe int JetInit3(ref JET_INSTANCE instance, JET_RSTINFO recoveryOptions, InitGrbit grbit)
		{
			this.CheckSupportsVistaFeatures("JetInit3");
			if (recoveryOptions != null)
			{
				StatusCallbackWrapper statusCallbackWrapper = new StatusCallbackWrapper(recoveryOptions.pfnStatus);
				NATIVE_RSTINFO nativeRstinfo = recoveryOptions.GetNativeRstinfo();
				int num = (recoveryOptions.rgrstmap == null) ? 0 : recoveryOptions.rgrstmap.Length;
				try
				{
					NATIVE_RSTMAP* rgrstmap = stackalloc NATIVE_RSTMAP[checked(unchecked((UIntPtr)num) * (UIntPtr)sizeof(NATIVE_RSTMAP))];
					if (num > 0)
					{
						nativeRstinfo.rgrstmap = rgrstmap;
						for (int i = 0; i < num; i++)
						{
							nativeRstinfo.rgrstmap[i] = recoveryOptions.rgrstmap[i].GetNativeRstmap();
						}
					}
					nativeRstinfo.pfnStatus = statusCallbackWrapper.NativeCallback;
					int result = JetApi.Err(NativeMethods.JetInit3W(ref instance.Value, ref nativeRstinfo, (uint)grbit));
					statusCallbackWrapper.ThrowSavedException();
					return result;
				}
				finally
				{
					if (null != nativeRstinfo.rgrstmap)
					{
						for (int j = 0; j < num; j++)
						{
							nativeRstinfo.rgrstmap[j].FreeHGlobal();
						}
					}
				}
			}
			return JetApi.Err(NativeMethods.JetInit3W(ref instance.Value, IntPtr.Zero, (uint)grbit));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003F30 File Offset: 0x00002130
		public unsafe int JetGetInstanceInfo(out int numInstances, out JET_INSTANCE_INFO[] instances)
		{
			uint nativeNumInstance = 0U;
			NATIVE_INSTANCE_INFO* nativeInstanceInfos = null;
			int err;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				err = NativeMethods.JetGetInstanceInfoW(out nativeNumInstance, out nativeInstanceInfos);
				instances = this.ConvertInstanceInfosUnicode(nativeNumInstance, nativeInstanceInfos);
			}
			else
			{
				err = NativeMethods.JetGetInstanceInfo(out nativeNumInstance, out nativeInstanceInfos);
				instances = this.ConvertInstanceInfosAscii(nativeNumInstance, nativeInstanceInfos);
			}
			numInstances = instances.Length;
			return JetApi.Err(err);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00003F88 File Offset: 0x00002188
		public int JetGetInstanceMiscInfo(JET_INSTANCE instance, out JET_SIGNATURE signature, JET_InstanceMiscInfo infoLevel)
		{
			this.CheckSupportsVistaFeatures("JetGetInstanceMiscInfo");
			NATIVE_SIGNATURE native = default(NATIVE_SIGNATURE);
			int err = NativeMethods.JetGetInstanceMiscInfo(instance.Value, ref native, checked((uint)NATIVE_SIGNATURE.Size), (uint)infoLevel);
			signature = new JET_SIGNATURE(native);
			return JetApi.Err(err);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003FD0 File Offset: 0x000021D0
		public int JetStopBackupInstance(JET_INSTANCE instance)
		{
			return JetApi.Err(NativeMethods.JetStopBackupInstance(instance.Value));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00003FE3 File Offset: 0x000021E3
		public int JetStopServiceInstance(JET_INSTANCE instance)
		{
			return JetApi.Err(NativeMethods.JetStopServiceInstance(instance.Value));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003FF6 File Offset: 0x000021F6
		public int JetStopServiceInstance2(JET_INSTANCE instance, StopServiceGrbit grbit)
		{
			this.CheckSupportsWindows8Features("JetStopServiceInstance2");
			return JetApi.Err(NativeMethods.JetStopServiceInstance2(instance.Value, (uint)grbit));
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004015 File Offset: 0x00002215
		public int JetTerm(JET_INSTANCE instance)
		{
			this.callbackWrappers.Collect();
			if (!instance.IsInvalid)
			{
				return JetApi.Err(NativeMethods.JetTerm(instance.Value));
			}
			return 0;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000403E File Offset: 0x0000223E
		public int JetTerm2(JET_INSTANCE instance, TermGrbit grbit)
		{
			this.callbackWrappers.Collect();
			if (!instance.IsInvalid)
			{
				return JetApi.Err(NativeMethods.JetTerm2(instance.Value, (uint)grbit));
			}
			return 0;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004068 File Offset: 0x00002268
		public unsafe int JetSetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, IntPtr paramValue, string paramString)
		{
			IntPtr* pinstance = (IntPtr.Zero == instance.Value) ? null : (&instance.Value);
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetSetSystemParameterW(pinstance, sesid.Value, (uint)paramid, paramValue, paramString));
			}
			return JetApi.Err(NativeMethods.JetSetSystemParameter(pinstance, sesid.Value, (uint)paramid, paramValue, paramString));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000040D4 File Offset: 0x000022D4
		public unsafe int JetSetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, JET_CALLBACK paramValue, string paramString)
		{
			IntPtr* pinstance = (IntPtr.Zero == instance.Value) ? null : (&instance.Value);
			if (paramValue == null)
			{
				return JetApi.Err(NativeMethods.JetSetSystemParameter(pinstance, sesid.Value, (uint)paramid, IntPtr.Zero, paramString));
			}
			JetCallbackWrapper jetCallbackWrapper = this.callbackWrappers.Add(paramValue);
			this.callbackWrappers.Collect();
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(jetCallbackWrapper.NativeCallback);
			return JetApi.Err(NativeMethods.JetSetSystemParameter(pinstance, sesid.Value, (uint)paramid, functionPointerForDelegate, paramString));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000415C File Offset: 0x0000235C
		public int JetGetSystemParameter(JET_INSTANCE instance, JET_SESID sesid, JET_param paramid, ref IntPtr paramValue, out string paramString, int maxParam)
		{
			JetApi.CheckNotNegative(maxParam, "maxParam");
			uint cbMax = checked((uint)(this.Capabilities.SupportsUnicodePaths ? (maxParam * 2) : maxParam));
			StringBuilder stringBuilder = new StringBuilder(maxParam);
			int result;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				result = JetApi.Err(NativeMethods.JetGetSystemParameterW(instance.Value, sesid.Value, (uint)paramid, ref paramValue, stringBuilder, cbMax));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetSystemParameter(instance.Value, sesid.Value, (uint)paramid, ref paramValue, stringBuilder, cbMax));
			}
			paramString = stringBuilder.ToString();
			paramString = StringCache.TryToIntern(paramString);
			return result;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000041F4 File Offset: 0x000023F4
		public int JetGetVersion(JET_SESID sesid, out uint version)
		{
			uint num;
			int result;
			if (this.versionOverride != 0U)
			{
				num = this.versionOverride;
				result = 0;
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetVersion(sesid.Value, out num));
			}
			version = num;
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000422C File Offset: 0x0000242C
		public int JetCreateDatabase(JET_SESID sesid, string database, string connect, out JET_DBID dbid, CreateDatabaseGrbit grbit)
		{
			JetApi.CheckNotNull(database, "database");
			dbid = JET_DBID.Nil;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetCreateDatabaseW(sesid.Value, database, connect, out dbid.Value, (uint)grbit));
			}
			return JetApi.Err(NativeMethods.JetCreateDatabase(sesid.Value, database, connect, out dbid.Value, (uint)grbit));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004298 File Offset: 0x00002498
		public int JetCreateDatabase2(JET_SESID sesid, string database, int maxPages, out JET_DBID dbid, CreateDatabaseGrbit grbit)
		{
			JetApi.CheckNotNull(database, "database");
			JetApi.CheckNotNegative(maxPages, "maxPages");
			dbid = JET_DBID.Nil;
			uint cpgDatabaseSizeMax = checked((uint)maxPages);
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetCreateDatabase2W(sesid.Value, database, cpgDatabaseSizeMax, out dbid.Value, (uint)grbit));
			}
			return JetApi.Err(NativeMethods.JetCreateDatabase2(sesid.Value, database, cpgDatabaseSizeMax, out dbid.Value, (uint)grbit));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004310 File Offset: 0x00002510
		public int JetAttachDatabase(JET_SESID sesid, string database, AttachDatabaseGrbit grbit)
		{
			JetApi.CheckNotNull(database, "database");
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetAttachDatabaseW(sesid.Value, database, (uint)grbit));
			}
			return JetApi.Err(NativeMethods.JetAttachDatabase(sesid.Value, database, (uint)grbit));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000435C File Offset: 0x0000255C
		public int JetAttachDatabase2(JET_SESID sesid, string database, int maxPages, AttachDatabaseGrbit grbit)
		{
			JetApi.CheckNotNull(database, "database");
			JetApi.CheckNotNegative(maxPages, "maxPages");
			checked
			{
				if (this.Capabilities.SupportsUnicodePaths)
				{
					return JetApi.Err(NativeMethods.JetAttachDatabase2W(sesid.Value, database, (uint)maxPages, (uint)grbit));
				}
				return JetApi.Err(NativeMethods.JetAttachDatabase2(sesid.Value, database, (uint)maxPages, (uint)grbit));
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000043BC File Offset: 0x000025BC
		public int JetOpenDatabase(JET_SESID sesid, string database, string connect, out JET_DBID dbid, OpenDatabaseGrbit grbit)
		{
			JetApi.CheckNotNull(database, "database");
			dbid = JET_DBID.Nil;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetOpenDatabaseW(sesid.Value, database, connect, out dbid.Value, (uint)grbit));
			}
			return JetApi.Err(NativeMethods.JetOpenDatabase(sesid.Value, database, connect, out dbid.Value, (uint)grbit));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004426 File Offset: 0x00002626
		public int JetCloseDatabase(JET_SESID sesid, JET_DBID dbid, CloseDatabaseGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetCloseDatabase(sesid.Value, dbid.Value, (uint)grbit));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004441 File Offset: 0x00002641
		public int JetDetachDatabase(JET_SESID sesid, string database)
		{
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetDetachDatabaseW(sesid.Value, database));
			}
			return JetApi.Err(NativeMethods.JetDetachDatabase(sesid.Value, database));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00004475 File Offset: 0x00002675
		public int JetDetachDatabase2(JET_SESID sesid, string database, DetachDatabaseGrbit grbit)
		{
			if (this.Capabilities.SupportsUnicodePaths)
			{
				return JetApi.Err(NativeMethods.JetDetachDatabase2W(sesid.Value, database, (uint)grbit));
			}
			return JetApi.Err(NativeMethods.JetDetachDatabase2(sesid.Value, database, (uint)grbit));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000044AC File Offset: 0x000026AC
		public int JetCompact(JET_SESID sesid, string sourceDatabase, string destinationDatabase, JET_PFNSTATUS statusCallback, object ignored, CompactGrbit grbit)
		{
			JetApi.CheckNotNull(sourceDatabase, "sourceDatabase");
			JetApi.CheckNotNull(destinationDatabase, "destinationDatabase");
			if (ignored != null)
			{
				throw new ArgumentException("must be null", "ignored");
			}
			StatusCallbackWrapper statusCallbackWrapper = new StatusCallbackWrapper(statusCallback);
			IntPtr pfnStatus = (statusCallback == null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(statusCallbackWrapper.NativeCallback);
			int result;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				result = JetApi.Err(NativeMethods.JetCompactW(sesid.Value, sourceDatabase, destinationDatabase, pfnStatus, IntPtr.Zero, (uint)grbit));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetCompact(sesid.Value, sourceDatabase, destinationDatabase, pfnStatus, IntPtr.Zero, (uint)grbit));
			}
			statusCallbackWrapper.ThrowSavedException();
			return result;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004550 File Offset: 0x00002750
		public int JetGrowDatabase(JET_SESID sesid, JET_DBID dbid, int desiredPages, out int actualPages)
		{
			JetApi.CheckNotNegative(desiredPages, "desiredPages");
			uint num = 0U;
			checked
			{
				int result = JetApi.Err(NativeMethods.JetGrowDatabase(sesid.Value, dbid.Value, (uint)desiredPages, out num));
				actualPages = (int)num;
				return result;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004590 File Offset: 0x00002790
		public int JetSetDatabaseSize(JET_SESID sesid, string database, int desiredPages, out int actualPages)
		{
			JetApi.CheckNotNegative(desiredPages, "desiredPages");
			JetApi.CheckNotNull(database, "database");
			uint num = 0U;
			checked
			{
				int result;
				if (this.Capabilities.SupportsUnicodePaths)
				{
					result = JetApi.Err(NativeMethods.JetSetDatabaseSizeW(sesid.Value, database, (uint)desiredPages, out num));
				}
				else
				{
					result = JetApi.Err(NativeMethods.JetSetDatabaseSize(sesid.Value, database, (uint)desiredPages, out num));
				}
				actualPages = (int)num;
				return result;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000045F8 File Offset: 0x000027F8
		public int JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out int value, JET_DbInfo infoLevel)
		{
			return JetApi.Err(NativeMethods.JetGetDatabaseInfo(sesid.Value, dbid.Value, out value, 4U, (uint)infoLevel));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00004618 File Offset: 0x00002818
		public int JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out JET_DBINFOMISC dbinfomisc, JET_DbInfo infoLevel)
		{
			int result;
			if (this.Capabilities.SupportsWindows7Features)
			{
				NATIVE_DBINFOMISC4 native_DBINFOMISC;
				result = JetApi.Err(NativeMethods.JetGetDatabaseInfoW(sesid.Value, dbid.Value, out native_DBINFOMISC, (uint)Marshal.SizeOf(typeof(NATIVE_DBINFOMISC4)), (uint)infoLevel));
				dbinfomisc = new JET_DBINFOMISC();
				dbinfomisc.SetFromNativeDbinfoMisc(ref native_DBINFOMISC);
			}
			else if (this.Capabilities.SupportsVistaFeatures)
			{
				NATIVE_DBINFOMISC native_DBINFOMISC2;
				result = JetApi.Err(NativeMethods.JetGetDatabaseInfoW(sesid.Value, dbid.Value, out native_DBINFOMISC2, (uint)Marshal.SizeOf(typeof(NATIVE_DBINFOMISC)), (uint)infoLevel));
				dbinfomisc = new JET_DBINFOMISC();
				dbinfomisc.SetFromNativeDbinfoMisc(ref native_DBINFOMISC2);
			}
			else
			{
				NATIVE_DBINFOMISC native_DBINFOMISC3;
				result = JetApi.Err(NativeMethods.JetGetDatabaseInfo(sesid.Value, dbid.Value, out native_DBINFOMISC3, (uint)Marshal.SizeOf(typeof(NATIVE_DBINFOMISC)), (uint)infoLevel));
				dbinfomisc = new JET_DBINFOMISC();
				dbinfomisc.SetFromNativeDbinfoMisc(ref native_DBINFOMISC3);
			}
			return result;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000046FC File Offset: 0x000028FC
		public int JetGetDatabaseInfo(JET_SESID sesid, JET_DBID dbid, out string value, JET_DbInfo infoLevel)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			int result;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				result = JetApi.Err(NativeMethods.JetGetDatabaseInfoW(sesid.Value, dbid.Value, stringBuilder, 1024U, (uint)infoLevel));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetDatabaseInfo(sesid.Value, dbid.Value, stringBuilder, 1024U, (uint)infoLevel));
			}
			value = stringBuilder.ToString();
			return result;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004770 File Offset: 0x00002970
		public int JetGetDatabaseFileInfo(string databaseName, out int value, JET_DbInfo infoLevel)
		{
			int result;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				result = JetApi.Err(NativeMethods.JetGetDatabaseFileInfoW(databaseName, out value, 4U, (uint)infoLevel));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetDatabaseFileInfo(databaseName, out value, 4U, (uint)infoLevel));
			}
			return result;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000047AC File Offset: 0x000029AC
		public int JetGetDatabaseFileInfo(string databaseName, out long value, JET_DbInfo infoLevel)
		{
			int result;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				result = JetApi.Err(NativeMethods.JetGetDatabaseFileInfoW(databaseName, out value, 8U, (uint)infoLevel));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetDatabaseFileInfo(databaseName, out value, 8U, (uint)infoLevel));
			}
			return result;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000047E8 File Offset: 0x000029E8
		public int JetGetDatabaseFileInfo(string databaseName, out JET_DBINFOMISC dbinfomisc, JET_DbInfo infoLevel)
		{
			int result = 0;
			dbinfomisc = null;
			bool flag = false;
			this.NotYetPublishedGetDbinfomisc(databaseName, ref dbinfomisc, infoLevel, ref flag, ref result);
			if (!flag)
			{
				if (this.Capabilities.SupportsWindows7Features)
				{
					NATIVE_DBINFOMISC4 native_DBINFOMISC;
					result = JetApi.Err(NativeMethods.JetGetDatabaseFileInfoW(databaseName, out native_DBINFOMISC, (uint)Marshal.SizeOf(typeof(NATIVE_DBINFOMISC4)), (uint)infoLevel));
					dbinfomisc = new JET_DBINFOMISC();
					dbinfomisc.SetFromNativeDbinfoMisc(ref native_DBINFOMISC);
				}
				else
				{
					NATIVE_DBINFOMISC native_DBINFOMISC2;
					if (this.Capabilities.SupportsUnicodePaths)
					{
						result = JetApi.Err(NativeMethods.JetGetDatabaseFileInfoW(databaseName, out native_DBINFOMISC2, (uint)Marshal.SizeOf(typeof(NATIVE_DBINFOMISC)), (uint)infoLevel));
					}
					else
					{
						result = JetApi.Err(NativeMethods.JetGetDatabaseFileInfo(databaseName, out native_DBINFOMISC2, (uint)Marshal.SizeOf(typeof(NATIVE_DBINFOMISC)), (uint)infoLevel));
					}
					dbinfomisc = new JET_DBINFOMISC();
					dbinfomisc.SetFromNativeDbinfoMisc(ref native_DBINFOMISC2);
				}
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000048A8 File Offset: 0x00002AA8
		public int JetBackupInstance(JET_INSTANCE instance, string destination, BackupGrbit grbit, JET_PFNSTATUS statusCallback)
		{
			StatusCallbackWrapper statusCallbackWrapper = new StatusCallbackWrapper(statusCallback);
			IntPtr pfnStatus = (statusCallback == null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(statusCallbackWrapper.NativeCallback);
			int result;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				result = JetApi.Err(NativeMethods.JetBackupInstanceW(instance.Value, destination, (uint)grbit, pfnStatus));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetBackupInstance(instance.Value, destination, (uint)grbit, pfnStatus));
			}
			statusCallbackWrapper.ThrowSavedException();
			return result;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004914 File Offset: 0x00002B14
		public int JetRestoreInstance(JET_INSTANCE instance, string source, string destination, JET_PFNSTATUS statusCallback)
		{
			JetApi.CheckNotNull(source, "source");
			StatusCallbackWrapper statusCallbackWrapper = new StatusCallbackWrapper(statusCallback);
			IntPtr pfn = (statusCallback == null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(statusCallbackWrapper.NativeCallback);
			int result;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				result = JetApi.Err(NativeMethods.JetRestoreInstanceW(instance.Value, source, destination, pfn));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetRestoreInstance(instance.Value, source, destination, pfn));
			}
			statusCallbackWrapper.ThrowSavedException();
			return result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000498B File Offset: 0x00002B8B
		public int JetOSSnapshotPrepare(out JET_OSSNAPID snapid, SnapshotPrepareGrbit grbit)
		{
			snapid = JET_OSSNAPID.Nil;
			return JetApi.Err(NativeMethods.JetOSSnapshotPrepare(out snapid.Value, (uint)grbit));
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000049A9 File Offset: 0x00002BA9
		public int JetOSSnapshotPrepareInstance(JET_OSSNAPID snapshot, JET_INSTANCE instance, SnapshotPrepareInstanceGrbit grbit)
		{
			this.CheckSupportsVistaFeatures("JetOSSnapshotPrepareInstance");
			return JetApi.Err(NativeMethods.JetOSSnapshotPrepareInstance(snapshot.Value, instance.Value, (uint)grbit));
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000049D0 File Offset: 0x00002BD0
		public unsafe int JetOSSnapshotFreeze(JET_OSSNAPID snapshot, out int numInstances, out JET_INSTANCE_INFO[] instances, SnapshotFreezeGrbit grbit)
		{
			uint nativeNumInstance = 0U;
			NATIVE_INSTANCE_INFO* nativeInstanceInfos = null;
			int err;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				err = NativeMethods.JetOSSnapshotFreezeW(snapshot.Value, out nativeNumInstance, out nativeInstanceInfos, (uint)grbit);
				instances = this.ConvertInstanceInfosUnicode(nativeNumInstance, nativeInstanceInfos);
			}
			else
			{
				err = NativeMethods.JetOSSnapshotFreeze(snapshot.Value, out nativeNumInstance, out nativeInstanceInfos, (uint)grbit);
				instances = this.ConvertInstanceInfosAscii(nativeNumInstance, nativeInstanceInfos);
			}
			numInstances = instances.Length;
			return JetApi.Err(err);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004A38 File Offset: 0x00002C38
		public unsafe int JetOSSnapshotGetFreezeInfo(JET_OSSNAPID snapshot, out int numInstances, out JET_INSTANCE_INFO[] instances, SnapshotGetFreezeInfoGrbit grbit)
		{
			this.CheckSupportsVistaFeatures("JetOSSnapshotGetFreezeInfo");
			uint nativeNumInstance = 0U;
			NATIVE_INSTANCE_INFO* nativeInstanceInfos = null;
			int err = NativeMethods.JetOSSnapshotGetFreezeInfoW(snapshot.Value, out nativeNumInstance, out nativeInstanceInfos, (uint)grbit);
			instances = this.ConvertInstanceInfosUnicode(nativeNumInstance, nativeInstanceInfos);
			numInstances = instances.Length;
			return JetApi.Err(err);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004A7E File Offset: 0x00002C7E
		public int JetOSSnapshotThaw(JET_OSSNAPID snapid, SnapshotThawGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetOSSnapshotThaw(snapid.Value, (uint)grbit));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004A92 File Offset: 0x00002C92
		public int JetOSSnapshotTruncateLog(JET_OSSNAPID snapshot, SnapshotTruncateLogGrbit grbit)
		{
			this.CheckSupportsVistaFeatures("JetOSSnapshotTruncateLog");
			return JetApi.Err(NativeMethods.JetOSSnapshotTruncateLog(snapshot.Value, (uint)grbit));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004AB1 File Offset: 0x00002CB1
		public int JetOSSnapshotTruncateLogInstance(JET_OSSNAPID snapshot, JET_INSTANCE instance, SnapshotTruncateLogGrbit grbit)
		{
			this.CheckSupportsVistaFeatures("JetOSSnapshotTruncateLogInstance");
			return JetApi.Err(NativeMethods.JetOSSnapshotTruncateLogInstance(snapshot.Value, instance.Value, (uint)grbit));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004AD7 File Offset: 0x00002CD7
		public int JetOSSnapshotEnd(JET_OSSNAPID snapid, SnapshotEndGrbit grbit)
		{
			this.CheckSupportsVistaFeatures("JetOSSnapshotEnd");
			return JetApi.Err(NativeMethods.JetOSSnapshotEnd(snapid.Value, (uint)grbit));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004AF6 File Offset: 0x00002CF6
		public int JetOSSnapshotAbort(JET_OSSNAPID snapid, SnapshotAbortGrbit grbit)
		{
			this.CheckSupportsServer2003Features("JetOSSnapshotAbort");
			return JetApi.Err(NativeMethods.JetOSSnapshotAbort(snapid.Value, (uint)grbit));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004B15 File Offset: 0x00002D15
		public int JetBeginExternalBackupInstance(JET_INSTANCE instance, BeginExternalBackupGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetBeginExternalBackupInstance(instance.Value, (uint)grbit));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004B29 File Offset: 0x00002D29
		public int JetCloseFileInstance(JET_INSTANCE instance, JET_HANDLE handle)
		{
			return JetApi.Err(NativeMethods.JetCloseFileInstance(instance.Value, handle.Value));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004B43 File Offset: 0x00002D43
		public int JetEndExternalBackupInstance(JET_INSTANCE instance)
		{
			return JetApi.Err(NativeMethods.JetEndExternalBackupInstance(instance.Value));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004B56 File Offset: 0x00002D56
		public int JetEndExternalBackupInstance2(JET_INSTANCE instance, EndExternalBackupGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetEndExternalBackupInstance2(instance.Value, (uint)grbit));
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004B6C File Offset: 0x00002D6C
		public int JetGetAttachInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars)
		{
			JetApi.CheckNotNegative(maxChars, "maxChars");
			checked
			{
				int result;
				if (this.Capabilities.SupportsUnicodePaths)
				{
					uint num = unchecked(checked((uint)maxChars) * 2U);
					byte[] array = new byte[num];
					uint num2 = 0U;
					result = JetApi.Err(NativeMethods.JetGetAttachInfoInstanceW(instance.Value, array, num, out num2));
					actualChars = (int)num2 / 2;
					files = Encoding.Unicode.GetString(array, 0, Math.Min(array.Length, (int)num2));
				}
				else
				{
					uint num3 = (uint)maxChars;
					byte[] array2 = new byte[num3];
					uint num4 = 0U;
					result = JetApi.Err(NativeMethods.JetGetAttachInfoInstance(instance.Value, array2, num3, out num4));
					actualChars = (int)num4;
					files = LibraryHelpers.EncodingASCII.GetString(array2, 0, Math.Min(array2.Length, (int)num4));
				}
				return result;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004C20 File Offset: 0x00002E20
		public int JetGetLogInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars)
		{
			JetApi.CheckNotNegative(maxChars, "maxChars");
			checked
			{
				int result;
				if (this.Capabilities.SupportsUnicodePaths)
				{
					uint num = unchecked(checked((uint)maxChars) * 2U);
					byte[] array = new byte[num];
					uint num2 = 0U;
					result = JetApi.Err(NativeMethods.JetGetLogInfoInstanceW(instance.Value, array, num, out num2));
					actualChars = (int)num2 / 2;
					files = Encoding.Unicode.GetString(array, 0, Math.Min(array.Length, (int)num2));
				}
				else
				{
					uint num3 = (uint)maxChars;
					byte[] array2 = new byte[num3];
					uint num4 = 0U;
					result = JetApi.Err(NativeMethods.JetGetLogInfoInstance(instance.Value, array2, num3, out num4));
					actualChars = (int)num4;
					files = LibraryHelpers.EncodingASCII.GetString(array2, 0, Math.Min(array2.Length, (int)num4));
				}
				return result;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public int JetGetTruncateLogInfoInstance(JET_INSTANCE instance, out string files, int maxChars, out int actualChars)
		{
			JetApi.CheckNotNegative(maxChars, "maxChars");
			checked
			{
				int result;
				if (this.Capabilities.SupportsUnicodePaths)
				{
					uint num = unchecked(checked((uint)maxChars) * 2U);
					byte[] array = new byte[num];
					uint num2 = 0U;
					result = JetApi.Err(NativeMethods.JetGetTruncateLogInfoInstanceW(instance.Value, array, num, out num2));
					actualChars = (int)num2 / 2;
					files = Encoding.Unicode.GetString(array, 0, Math.Min(array.Length, (int)num2));
				}
				else
				{
					uint num3 = (uint)maxChars;
					byte[] array2 = new byte[num3];
					uint num4 = 0U;
					result = JetApi.Err(NativeMethods.JetGetTruncateLogInfoInstance(instance.Value, array2, num3, out num4));
					actualChars = (int)num4;
					files = LibraryHelpers.EncodingASCII.GetString(array2, 0, Math.Min(array2.Length, (int)num4));
				}
				return result;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00004D88 File Offset: 0x00002F88
		public int JetOpenFileInstance(JET_INSTANCE instance, string file, out JET_HANDLE handle, out long fileSizeLow, out long fileSizeHigh)
		{
			JetApi.CheckNotNull(file, "file");
			handle = JET_HANDLE.Nil;
			uint num;
			uint num2;
			int result;
			if (this.Capabilities.SupportsUnicodePaths)
			{
				result = JetApi.Err(NativeMethods.JetOpenFileInstanceW(instance.Value, file, out handle.Value, out num, out num2));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetOpenFileInstance(instance.Value, file, out handle.Value, out num, out num2));
			}
			fileSizeLow = (long)((ulong)num);
			fileSizeHigh = (long)((ulong)num2);
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004E00 File Offset: 0x00003000
		public int JetReadFileInstance(JET_INSTANCE instance, JET_HANDLE file, byte[] buffer, int bufferSize, out int bytesRead)
		{
			JetApi.CheckNotNull(buffer, "buffer");
			JetApi.CheckDataSize<byte>(buffer, bufferSize, "bufferSize");
			IntPtr intPtr = NativeMethods.VirtualAlloc(IntPtr.Zero, (UIntPtr)((ulong)((long)bufferSize)), 12288U, 4U);
			NativeMethods.ThrowExceptionOnNull(intPtr, "VirtualAlloc");
			checked
			{
				int result;
				try
				{
					uint num = 0U;
					int num2 = JetApi.Err(NativeMethods.JetReadFileInstance(instance.Value, file.Value, intPtr, (uint)bufferSize, out num));
					bytesRead = (int)num;
					Marshal.Copy(intPtr, buffer, 0, bytesRead);
					result = num2;
				}
				finally
				{
					bool success = NativeMethods.VirtualFree(intPtr, UIntPtr.Zero, 32768U);
					NativeMethods.ThrowExceptionOnFailure(success, "VirtualFree");
				}
				return result;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00004EB0 File Offset: 0x000030B0
		public int JetTruncateLogInstance(JET_INSTANCE instance)
		{
			return JetApi.Err(NativeMethods.JetTruncateLogInstance(instance.Value));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00004EC3 File Offset: 0x000030C3
		public int JetBeginSession(JET_INSTANCE instance, out JET_SESID sesid, string username, string password)
		{
			sesid = JET_SESID.Nil;
			return JetApi.Err(NativeMethods.JetBeginSession(instance.Value, out sesid.Value, username, password));
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00004EEA File Offset: 0x000030EA
		public int JetSetSessionContext(JET_SESID sesid, IntPtr context)
		{
			return JetApi.Err(NativeMethods.JetSetSessionContext(sesid.Value, context));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00004EFE File Offset: 0x000030FE
		public int JetResetSessionContext(JET_SESID sesid)
		{
			return JetApi.Err(NativeMethods.JetResetSessionContext(sesid.Value));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00004F11 File Offset: 0x00003111
		public int JetEndSession(JET_SESID sesid, EndSessionGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetEndSession(sesid.Value, (uint)grbit));
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00004F25 File Offset: 0x00003125
		public int JetDupSession(JET_SESID sesid, out JET_SESID newSesid)
		{
			newSesid = JET_SESID.Nil;
			return JetApi.Err(NativeMethods.JetDupSession(sesid.Value, out newSesid.Value));
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00004F4C File Offset: 0x0000314C
		public unsafe int JetGetThreadStats(out JET_THREADSTATS threadstats)
		{
			this.CheckSupportsVistaFeatures("JetGetThreadStats");
			fixed (JET_THREADSTATS* ptr = &threadstats)
			{
				return JetApi.Err(NativeMethods.JetGetThreadStats(ptr, JET_THREADSTATS.Size));
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004F7C File Offset: 0x0000317C
		public int JetOpenTable(JET_SESID sesid, JET_DBID dbid, string tablename, byte[] parameters, int parametersLength, OpenTableGrbit grbit, out JET_TABLEID tableid)
		{
			tableid = JET_TABLEID.Nil;
			JetApi.CheckNotNull(tablename, "tablename");
			JetApi.CheckDataSize<byte>(parameters, parametersLength, "parametersLength");
			return JetApi.Err(NativeMethods.JetOpenTable(sesid.Value, dbid.Value, tablename, parameters, checked((uint)parametersLength), (uint)grbit, out tableid.Value));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004FD5 File Offset: 0x000031D5
		public int JetCloseTable(JET_SESID sesid, JET_TABLEID tableid)
		{
			return JetApi.Err(NativeMethods.JetCloseTable(sesid.Value, tableid.Value));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00004FEF File Offset: 0x000031EF
		public int JetDupCursor(JET_SESID sesid, JET_TABLEID tableid, out JET_TABLEID newTableid, DupCursorGrbit grbit)
		{
			newTableid = JET_TABLEID.Nil;
			return JetApi.Err(NativeMethods.JetDupCursor(sesid.Value, tableid.Value, out newTableid.Value, (uint)grbit));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000501C File Offset: 0x0000321C
		public int JetComputeStats(JET_SESID sesid, JET_TABLEID tableid)
		{
			return JetApi.Err(NativeMethods.JetComputeStats(sesid.Value, tableid.Value));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005036 File Offset: 0x00003236
		public int JetSetLS(JET_SESID sesid, JET_TABLEID tableid, JET_LS ls, LsGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetSetLS(sesid.Value, tableid.Value, ls.Value, (uint)grbit));
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000505C File Offset: 0x0000325C
		public int JetGetLS(JET_SESID sesid, JET_TABLEID tableid, out JET_LS ls, LsGrbit grbit)
		{
			IntPtr value;
			int err = NativeMethods.JetGetLS(sesid.Value, tableid.Value, out value, (uint)grbit);
			ls = new JET_LS
			{
				Value = value
			};
			return JetApi.Err(err);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000509E File Offset: 0x0000329E
		public int JetGetCursorInfo(JET_SESID sesid, JET_TABLEID tableid)
		{
			return JetApi.Err(NativeMethods.JetGetCursorInfo(sesid.Value, tableid.Value, IntPtr.Zero, 0U, 0U));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000050BF File Offset: 0x000032BF
		public int JetBeginTransaction(JET_SESID sesid)
		{
			return JetApi.Err(NativeMethods.JetBeginTransaction(sesid.Value));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000050D2 File Offset: 0x000032D2
		public int JetBeginTransaction2(JET_SESID sesid, BeginTransactionGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetBeginTransaction2(sesid.Value, (uint)grbit));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000050E6 File Offset: 0x000032E6
		public int JetCommitTransaction(JET_SESID sesid, CommitTransactionGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetCommitTransaction(sesid.Value, (uint)grbit));
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000050FA File Offset: 0x000032FA
		public int JetRollback(JET_SESID sesid, RollbackTransactionGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetRollback(sesid.Value, (uint)grbit));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000510E File Offset: 0x0000330E
		public int JetCreateTable(JET_SESID sesid, JET_DBID dbid, string table, int pages, int density, out JET_TABLEID tableid)
		{
			tableid = JET_TABLEID.Nil;
			JetApi.CheckNotNull(table, "table");
			return JetApi.Err(NativeMethods.JetCreateTable(sesid.Value, dbid.Value, table, pages, density, out tableid.Value));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000514B File Offset: 0x0000334B
		public int JetDeleteTable(JET_SESID sesid, JET_DBID dbid, string table)
		{
			JetApi.CheckNotNull(table, "table");
			return JetApi.Err(NativeMethods.JetDeleteTable(sesid.Value, dbid.Value, table));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005174 File Offset: 0x00003374
		public int JetAddColumn(JET_SESID sesid, JET_TABLEID tableid, string column, JET_COLUMNDEF columndef, byte[] defaultValue, int defaultValueSize, out JET_COLUMNID columnid)
		{
			columnid = JET_COLUMNID.Nil;
			JetApi.CheckNotNull(column, "column");
			JetApi.CheckNotNull(columndef, "columndef");
			JetApi.CheckDataSize<byte>(defaultValue, defaultValueSize, "defaultValueSize");
			NATIVE_COLUMNDEF nativeColumndef = columndef.GetNativeColumndef();
			int result = JetApi.Err(NativeMethods.JetAddColumn(sesid.Value, tableid.Value, column, ref nativeColumndef, defaultValue, checked((uint)defaultValueSize), out columnid.Value));
			columndef.columnid = new JET_COLUMNID
			{
				Value = columnid.Value
			};
			return result;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005201 File Offset: 0x00003401
		public int JetDeleteColumn(JET_SESID sesid, JET_TABLEID tableid, string column)
		{
			JetApi.CheckNotNull(column, "column");
			return JetApi.Err(NativeMethods.JetDeleteColumn(sesid.Value, tableid.Value, column));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005227 File Offset: 0x00003427
		public int JetDeleteColumn2(JET_SESID sesid, JET_TABLEID tableid, string column, DeleteColumnGrbit grbit)
		{
			JetApi.CheckNotNull(column, "column");
			return JetApi.Err(NativeMethods.JetDeleteColumn2(sesid.Value, tableid.Value, column, (uint)grbit));
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005250 File Offset: 0x00003450
		public int JetCreateIndex(JET_SESID sesid, JET_TABLEID tableid, string indexName, CreateIndexGrbit grbit, string keyDescription, int keyDescriptionLength, int density)
		{
			JetApi.CheckNotNull(indexName, "indexName");
			JetApi.CheckNotNegative(keyDescriptionLength, "keyDescriptionLength");
			JetApi.CheckNotNegative(density, "density");
			checked
			{
				if (keyDescriptionLength > keyDescription.Length + 1)
				{
					throw new ArgumentOutOfRangeException("keyDescriptionLength", keyDescriptionLength, "cannot be greater than keyDescription.Length");
				}
				return JetApi.Err(NativeMethods.JetCreateIndex(sesid.Value, tableid.Value, indexName, (uint)grbit, keyDescription, (uint)keyDescriptionLength, (uint)density));
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000052C8 File Offset: 0x000034C8
		public int JetCreateIndex2(JET_SESID sesid, JET_TABLEID tableid, JET_INDEXCREATE[] indexcreates, int numIndexCreates)
		{
			JetApi.CheckNotNull(indexcreates, "indexcreates");
			JetApi.CheckNotNegative(numIndexCreates, "numIndexCreates");
			if (numIndexCreates > indexcreates.Length)
			{
				throw new ArgumentOutOfRangeException("numIndexCreates", numIndexCreates, "numIndexCreates is larger than the number of indexes passed in");
			}
			if (this.Capabilities.SupportsWindows7Features)
			{
				return JetApi.CreateIndexes2(sesid, tableid, indexcreates, numIndexCreates);
			}
			if (this.Capabilities.SupportsVistaFeatures)
			{
				return JetApi.CreateIndexes1(sesid, tableid, indexcreates, numIndexCreates);
			}
			return JetApi.CreateIndexes(sesid, tableid, indexcreates, numIndexCreates);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005344 File Offset: 0x00003544
		public int JetDeleteIndex(JET_SESID sesid, JET_TABLEID tableid, string index)
		{
			JetApi.CheckNotNull(index, "index");
			return JetApi.Err(NativeMethods.JetDeleteIndex(sesid.Value, tableid.Value, index));
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000536C File Offset: 0x0000356C
		public int JetOpenTempTable(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids)
		{
			JetApi.CheckNotNull(columns, "columnns");
			JetApi.CheckDataSize<JET_COLUMNDEF>(columns, numColumns, "numColumns");
			JetApi.CheckNotNull(columnids, "columnids");
			JetApi.CheckDataSize<JET_COLUMNID>(columnids, numColumns, "numColumns");
			tableid = JET_TABLEID.Nil;
			NATIVE_COLUMNDEF[] nativecolumndefs = JetApi.GetNativecolumndefs(columns, numColumns);
			uint[] array = new uint[numColumns];
			int result = JetApi.Err(NativeMethods.JetOpenTempTable(sesid.Value, nativecolumndefs, checked((uint)numColumns), (uint)grbit, out tableid.Value, array));
			JetApi.SetColumnids(columns, columnids, array, numColumns);
			return result;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000053F0 File Offset: 0x000035F0
		public int JetOpenTempTable2(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, int lcid, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids)
		{
			JetApi.CheckNotNull(columns, "columnns");
			JetApi.CheckDataSize<JET_COLUMNDEF>(columns, numColumns, "numColumns");
			JetApi.CheckNotNull(columnids, "columnids");
			JetApi.CheckDataSize<JET_COLUMNID>(columnids, numColumns, "numColumns");
			tableid = JET_TABLEID.Nil;
			NATIVE_COLUMNDEF[] nativecolumndefs = JetApi.GetNativecolumndefs(columns, numColumns);
			uint[] array = new uint[numColumns];
			int result = JetApi.Err(NativeMethods.JetOpenTempTable2(sesid.Value, nativecolumndefs, checked((uint)numColumns), (uint)lcid, (uint)grbit, out tableid.Value, array));
			JetApi.SetColumnids(columns, columnids, array, numColumns);
			return result;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005474 File Offset: 0x00003674
		public int JetOpenTempTable3(JET_SESID sesid, JET_COLUMNDEF[] columns, int numColumns, JET_UNICODEINDEX unicodeindex, TempTableGrbit grbit, out JET_TABLEID tableid, JET_COLUMNID[] columnids)
		{
			JetApi.CheckNotNull(columns, "columnns");
			JetApi.CheckDataSize<JET_COLUMNDEF>(columns, numColumns, "numColumns");
			JetApi.CheckNotNull(columnids, "columnids");
			JetApi.CheckDataSize<JET_COLUMNID>(columnids, numColumns, "numColumns");
			tableid = JET_TABLEID.Nil;
			NATIVE_COLUMNDEF[] nativecolumndefs = JetApi.GetNativecolumndefs(columns, numColumns);
			uint[] array = new uint[numColumns];
			checked
			{
				int result;
				if (unicodeindex != null)
				{
					NATIVE_UNICODEINDEX nativeUnicodeIndex = unicodeindex.GetNativeUnicodeIndex();
					result = JetApi.Err(NativeMethods.JetOpenTempTable3(sesid.Value, nativecolumndefs, (uint)numColumns, ref nativeUnicodeIndex, (uint)grbit, out tableid.Value, array));
				}
				else
				{
					result = JetApi.Err(NativeMethods.JetOpenTempTable3(sesid.Value, nativecolumndefs, (uint)numColumns, IntPtr.Zero, (uint)grbit, out tableid.Value, array));
				}
				JetApi.SetColumnids(columns, columnids, array, numColumns);
				return result;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000552C File Offset: 0x0000372C
		public unsafe int JetOpenTemporaryTable(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable)
		{
			this.CheckSupportsVistaFeatures("JetOpenTemporaryTable");
			JetApi.CheckNotNull(temporarytable, "temporarytable");
			NATIVE_OPENTEMPORARYTABLE nativeOpenTemporaryTable = temporarytable.GetNativeOpenTemporaryTable();
			uint[] array = new uint[nativeOpenTemporaryTable.ccolumn];
			NATIVE_COLUMNDEF[] nativecolumndefs = JetApi.GetNativecolumndefs(temporarytable.prgcolumndef, temporarytable.ccolumn);
			int result;
			using (GCHandleCollection gchandleCollection = default(GCHandleCollection))
			{
				nativeOpenTemporaryTable.prgcolumndef = (NATIVE_COLUMNDEF*)((void*)gchandleCollection.Add(nativecolumndefs));
				nativeOpenTemporaryTable.rgcolumnid = (uint*)((void*)gchandleCollection.Add(array));
				if (temporarytable.pidxunicode != null)
				{
					nativeOpenTemporaryTable.pidxunicode = (NATIVE_UNICODEINDEX*)((void*)gchandleCollection.Add(temporarytable.pidxunicode.GetNativeUnicodeIndex()));
				}
				int num = JetApi.Err(NativeMethods.JetOpenTemporaryTable(sesid.Value, ref nativeOpenTemporaryTable));
				JetApi.SetColumnids(temporarytable.prgcolumndef, temporarytable.prgcolumnid, array, temporarytable.ccolumn);
				temporarytable.tableid = new JET_TABLEID
				{
					Value = nativeOpenTemporaryTable.tableid
				};
				result = num;
			}
			return result;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005644 File Offset: 0x00003844
		public int JetCreateTableColumnIndex3(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate)
		{
			JetApi.CheckNotNull(tablecreate, "tablecreate");
			if (this.Capabilities.SupportsWindows7Features)
			{
				return JetApi.CreateTableColumnIndex3(sesid, dbid, tablecreate);
			}
			return this.CreateTableColumnIndex2(sesid, dbid, tablecreate);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005670 File Offset: 0x00003870
		public int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string columnName, out JET_COLUMNDEF columndef)
		{
			columndef = new JET_COLUMNDEF();
			JetApi.CheckNotNull(columnName, "columnName");
			NATIVE_COLUMNDEF fromNativeColumndef = default(NATIVE_COLUMNDEF);
			fromNativeColumndef.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_COLUMNDEF)));
			int result;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result = JetApi.Err(NativeMethods.JetGetTableColumnInfoW(sesid.Value, tableid.Value, columnName, ref fromNativeColumndef, fromNativeColumndef.cbStruct, 0U));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetTableColumnInfo(sesid.Value, tableid.Value, columnName, ref fromNativeColumndef, fromNativeColumndef.cbStruct, 0U));
			}
			columndef.SetFromNativeColumndef(fromNativeColumndef);
			return result;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005710 File Offset: 0x00003910
		public int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, out JET_COLUMNDEF columndef)
		{
			columndef = new JET_COLUMNDEF();
			NATIVE_COLUMNDEF fromNativeColumndef = default(NATIVE_COLUMNDEF);
			fromNativeColumndef.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_COLUMNDEF)));
			int result;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result = JetApi.Err(NativeMethods.JetGetTableColumnInfoW(sesid.Value, tableid.Value, ref columnid.Value, ref fromNativeColumndef, fromNativeColumndef.cbStruct, 6U));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetTableColumnInfo(sesid.Value, tableid.Value, ref columnid.Value, ref fromNativeColumndef, fromNativeColumndef.cbStruct, 6U));
			}
			columndef.SetFromNativeColumndef(fromNativeColumndef);
			return result;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000057B4 File Offset: 0x000039B4
		public int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string columnName, out JET_COLUMNBASE columnbase)
		{
			JetApi.CheckNotNull(columnName, "columnName");
			checked
			{
				int result;
				if (this.Capabilities.SupportsVistaFeatures)
				{
					NATIVE_COLUMNBASE_WIDE value = default(NATIVE_COLUMNBASE_WIDE);
					value.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_COLUMNBASE_WIDE));
					result = JetApi.Err(NativeMethods.JetGetTableColumnInfoW(sesid.Value, tableid.Value, columnName, ref value, value.cbStruct, 4U));
					columnbase = new JET_COLUMNBASE(value);
				}
				else
				{
					NATIVE_COLUMNBASE value2 = default(NATIVE_COLUMNBASE);
					value2.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_COLUMNBASE));
					result = JetApi.Err(NativeMethods.JetGetTableColumnInfo(sesid.Value, tableid.Value, columnName, ref value2, value2.cbStruct, 4U));
					columnbase = new JET_COLUMNBASE(value2);
				}
				return result;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005874 File Offset: 0x00003A74
		public int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, out JET_COLUMNBASE columnbase)
		{
			this.CheckSupportsVistaFeatures("JetGetTableColumnInfo");
			NATIVE_COLUMNBASE_WIDE value = default(NATIVE_COLUMNBASE_WIDE);
			value.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_COLUMNBASE_WIDE)));
			int result = JetApi.Err(NativeMethods.JetGetTableColumnInfoW(sesid.Value, tableid.Value, ref columnid.Value, ref value, value.cbStruct, 8U));
			columnbase = new JET_COLUMNBASE(value);
			return result;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000058E0 File Offset: 0x00003AE0
		public int JetGetTableColumnInfo(JET_SESID sesid, JET_TABLEID tableid, string ignored, ColInfoGrbit grbit, out JET_COLUMNLIST columnlist)
		{
			columnlist = new JET_COLUMNLIST();
			NATIVE_COLUMNLIST fromNativeColumnlist = default(NATIVE_COLUMNLIST);
			fromNativeColumnlist.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_COLUMNLIST)));
			int result;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result = JetApi.Err(NativeMethods.JetGetTableColumnInfoW(sesid.Value, tableid.Value, ignored, ref fromNativeColumnlist, fromNativeColumnlist.cbStruct, (uint)(grbit | (ColInfoGrbit)1)));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetTableColumnInfo(sesid.Value, tableid.Value, ignored, ref fromNativeColumnlist, fromNativeColumnlist.cbStruct, (uint)(grbit | (ColInfoGrbit)1)));
			}
			columnlist.SetFromNativeColumnlist(fromNativeColumnlist);
			return result;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000597C File Offset: 0x00003B7C
		public int JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string columnName, out JET_COLUMNDEF columndef)
		{
			columndef = new JET_COLUMNDEF();
			JetApi.CheckNotNull(tablename, "tablename");
			JetApi.CheckNotNull(columnName, "columnName");
			NATIVE_COLUMNDEF fromNativeColumndef = default(NATIVE_COLUMNDEF);
			fromNativeColumndef.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_COLUMNDEF)));
			int result;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result = JetApi.Err(NativeMethods.JetGetColumnInfoW(sesid.Value, dbid.Value, tablename, columnName, ref fromNativeColumndef, fromNativeColumndef.cbStruct, 0U));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetColumnInfo(sesid.Value, dbid.Value, tablename, columnName, ref fromNativeColumndef, fromNativeColumndef.cbStruct, 0U));
			}
			columndef.SetFromNativeColumndef(fromNativeColumndef);
			return result;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005A2C File Offset: 0x00003C2C
		public int JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string ignored, out JET_COLUMNLIST columnlist)
		{
			columnlist = new JET_COLUMNLIST();
			JetApi.CheckNotNull(tablename, "tablename");
			NATIVE_COLUMNLIST fromNativeColumnlist = default(NATIVE_COLUMNLIST);
			fromNativeColumnlist.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_COLUMNLIST)));
			int result;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result = JetApi.Err(NativeMethods.JetGetColumnInfoW(sesid.Value, dbid.Value, tablename, ignored, ref fromNativeColumnlist, fromNativeColumnlist.cbStruct, 1U));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetColumnInfo(sesid.Value, dbid.Value, tablename, ignored, ref fromNativeColumnlist, fromNativeColumnlist.cbStruct, 1U));
			}
			columnlist.SetFromNativeColumnlist(fromNativeColumnlist);
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public int JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string columnName, out JET_COLUMNBASE columnbase)
		{
			JetApi.CheckNotNull(tablename, "tablename");
			JetApi.CheckNotNull(columnName, "columnName");
			checked
			{
				int result;
				if (this.Capabilities.SupportsVistaFeatures)
				{
					NATIVE_COLUMNBASE_WIDE value = default(NATIVE_COLUMNBASE_WIDE);
					value.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_COLUMNBASE_WIDE));
					result = JetApi.Err(NativeMethods.JetGetColumnInfoW(sesid.Value, dbid.Value, tablename, columnName, ref value, value.cbStruct, 4U));
					columnbase = new JET_COLUMNBASE(value);
				}
				else
				{
					NATIVE_COLUMNBASE value2 = default(NATIVE_COLUMNBASE);
					value2.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_COLUMNBASE));
					result = JetApi.Err(NativeMethods.JetGetColumnInfo(sesid.Value, dbid.Value, tablename, columnName, ref value2, value2.cbStruct, 4U));
					columnbase = new JET_COLUMNBASE(value2);
				}
				return result;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public int JetGetColumnInfo(JET_SESID sesid, JET_DBID dbid, string tablename, JET_COLUMNID columnid, out JET_COLUMNBASE columnbase)
		{
			this.CheckSupportsVistaFeatures("JetGetColumnInfo");
			JetApi.CheckNotNull(tablename, "tablename");
			checked
			{
				int result;
				if (this.Capabilities.SupportsVistaFeatures)
				{
					NATIVE_COLUMNBASE_WIDE value = default(NATIVE_COLUMNBASE_WIDE);
					value.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_COLUMNBASE_WIDE));
					result = JetApi.Err(NativeMethods.JetGetColumnInfoW(sesid.Value, dbid.Value, tablename, ref columnid.Value, ref value, value.cbStruct, 8U));
					columnbase = new JET_COLUMNBASE(value);
				}
				else
				{
					NATIVE_COLUMNBASE value2 = default(NATIVE_COLUMNBASE);
					value2.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_COLUMNBASE));
					result = JetApi.Err(NativeMethods.JetGetColumnInfo(sesid.Value, dbid.Value, tablename, ref columnid.Value, ref value2, value2.cbStruct, 8U));
					columnbase = new JET_COLUMNBASE(value2);
				}
				return result;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005C7C File Offset: 0x00003E7C
		public int JetGetObjectInfo(JET_SESID sesid, JET_DBID dbid, out JET_OBJECTLIST objectlist)
		{
			objectlist = new JET_OBJECTLIST();
			NATIVE_OBJECTLIST fromNativeObjectlist = default(NATIVE_OBJECTLIST);
			fromNativeObjectlist.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_OBJECTLIST)));
			int result;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result = JetApi.Err(NativeMethods.JetGetObjectInfoW(sesid.Value, dbid.Value, 1U, null, null, ref fromNativeObjectlist, fromNativeObjectlist.cbStruct, 1U));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetObjectInfo(sesid.Value, dbid.Value, 1U, null, null, ref fromNativeObjectlist, fromNativeObjectlist.cbStruct, 1U));
			}
			objectlist.SetFromNativeObjectlist(fromNativeObjectlist);
			return result;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005D14 File Offset: 0x00003F14
		public int JetGetObjectInfo(JET_SESID sesid, JET_DBID dbid, JET_objtyp objtyp, string objectName, out JET_OBJECTINFO objectinfo)
		{
			objectinfo = new JET_OBJECTINFO();
			NATIVE_OBJECTINFO native_OBJECTINFO = default(NATIVE_OBJECTINFO);
			native_OBJECTINFO.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_OBJECTINFO)));
			int result;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result = JetApi.Err(NativeMethods.JetGetObjectInfoW(sesid.Value, dbid.Value, (uint)objtyp, null, objectName, ref native_OBJECTINFO, native_OBJECTINFO.cbStruct, 5U));
			}
			else
			{
				result = JetApi.Err(NativeMethods.JetGetObjectInfo(sesid.Value, dbid.Value, (uint)objtyp, null, objectName, ref native_OBJECTINFO, native_OBJECTINFO.cbStruct, 5U));
			}
			objectinfo.SetFromNativeObjectinfo(ref native_OBJECTINFO);
			return result;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public int JetGetCurrentIndex(JET_SESID sesid, JET_TABLEID tableid, out string indexName, int maxNameLength)
		{
			JetApi.CheckNotNegative(maxNameLength, "maxNameLength");
			StringBuilder stringBuilder = new StringBuilder(maxNameLength);
			int result = JetApi.Err(NativeMethods.JetGetCurrentIndex(sesid.Value, tableid.Value, stringBuilder, checked((uint)maxNameLength)));
			indexName = stringBuilder.ToString();
			indexName = StringCache.TryToIntern(indexName);
			return result;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005E00 File Offset: 0x00004000
		public int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out JET_OBJECTINFO result, JET_TblInfo infoLevel)
		{
			NATIVE_OBJECTINFO native_OBJECTINFO = default(NATIVE_OBJECTINFO);
			checked
			{
				int result2;
				if (this.Capabilities.SupportsVistaFeatures)
				{
					result2 = JetApi.Err(NativeMethods.JetGetTableInfoW(sesid.Value, tableid.Value, out native_OBJECTINFO, (uint)Marshal.SizeOf(typeof(NATIVE_OBJECTINFO)), (uint)infoLevel));
				}
				else
				{
					result2 = JetApi.Err(NativeMethods.JetGetTableInfo(sesid.Value, tableid.Value, out native_OBJECTINFO, (uint)Marshal.SizeOf(typeof(NATIVE_OBJECTINFO)), (uint)infoLevel));
				}
				result = new JET_OBJECTINFO();
				result.SetFromNativeObjectinfo(ref native_OBJECTINFO);
				return result2;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005E90 File Offset: 0x00004090
		public int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out string result, JET_TblInfo infoLevel)
		{
			StringBuilder stringBuilder = new StringBuilder(65);
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableInfoW(sesid.Value, tableid.Value, stringBuilder, (uint)stringBuilder.Capacity, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableInfo(sesid.Value, tableid.Value, stringBuilder, (uint)stringBuilder.Capacity, (uint)infoLevel));
			}
			result = stringBuilder.ToString();
			result = StringCache.TryToIntern(result);
			return result2;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005F0C File Offset: 0x0000410C
		public int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out JET_DBID result, JET_TblInfo infoLevel)
		{
			result = JET_DBID.Nil;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				return JetApi.Err(NativeMethods.JetGetTableInfoW(sesid.Value, tableid.Value, out result.Value, 4U, (uint)infoLevel));
			}
			return JetApi.Err(NativeMethods.JetGetTableInfo(sesid.Value, tableid.Value, out result.Value, 4U, (uint)infoLevel));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005F74 File Offset: 0x00004174
		public int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, int[] result, JET_TblInfo infoLevel)
		{
			JetApi.CheckNotNull(result, "result");
			uint cbMax = checked((uint)(result.Length * 4));
			if (this.Capabilities.SupportsVistaFeatures)
			{
				return JetApi.Err(NativeMethods.JetGetTableInfoW(sesid.Value, tableid.Value, result, cbMax, (uint)infoLevel));
			}
			return JetApi.Err(NativeMethods.JetGetTableInfo(sesid.Value, tableid.Value, result, cbMax, (uint)infoLevel));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005FDC File Offset: 0x000041DC
		public int JetGetTableInfo(JET_SESID sesid, JET_TABLEID tableid, out int result, JET_TblInfo infoLevel)
		{
			uint num;
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableInfoW(sesid.Value, tableid.Value, out num, 4U, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableInfo(sesid.Value, tableid.Value, out num, 4U, (uint)infoLevel));
			}
			result = (int)num;
			return result2;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006038 File Offset: 0x00004238
		public int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out ushort result, JET_IdxInfo infoLevel)
		{
			JetApi.CheckNotNull(tablename, "tablename");
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetIndexInfoW(sesid.Value, dbid.Value, tablename, indexname, out result, 2U, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetIndexInfo(sesid.Value, dbid.Value, tablename, indexname, out result, 2U, (uint)infoLevel));
			}
			return result2;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000060A4 File Offset: 0x000042A4
		public int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out int result, JET_IdxInfo infoLevel)
		{
			JetApi.CheckNotNull(tablename, "tablename");
			uint num;
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetIndexInfoW(sesid.Value, dbid.Value, tablename, indexname, out num, 4U, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetIndexInfo(sesid.Value, dbid.Value, tablename, indexname, out num, 4U, (uint)infoLevel));
			}
			result = (int)num;
			return result2;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006114 File Offset: 0x00004314
		public int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out JET_INDEXID result, JET_IdxInfo infoLevel)
		{
			JetApi.CheckNotNull(tablename, "tablename");
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetIndexInfoW(sesid.Value, dbid.Value, tablename, indexname, out result, JET_INDEXID.SizeOfIndexId, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetIndexInfo(sesid.Value, dbid.Value, tablename, indexname, out result, JET_INDEXID.SizeOfIndexId, (uint)infoLevel));
			}
			return result2;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006188 File Offset: 0x00004388
		public int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out JET_INDEXLIST result, JET_IdxInfo infoLevel)
		{
			JetApi.CheckNotNull(tablename, "tablename");
			NATIVE_INDEXLIST fromNativeIndexlist = default(NATIVE_INDEXLIST);
			fromNativeIndexlist.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_INDEXLIST)));
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetIndexInfoW(sesid.Value, dbid.Value, tablename, indexname, ref fromNativeIndexlist, fromNativeIndexlist.cbStruct, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetIndexInfo(sesid.Value, dbid.Value, tablename, indexname, ref fromNativeIndexlist, fromNativeIndexlist.cbStruct, (uint)infoLevel));
			}
			result = new JET_INDEXLIST();
			result.SetFromNativeIndexlist(fromNativeIndexlist);
			return result2;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006230 File Offset: 0x00004430
		public int JetGetIndexInfo(JET_SESID sesid, JET_DBID dbid, string tablename, string indexname, out string result, JET_IdxInfo infoLevel)
		{
			JetApi.CheckNotNull(tablename, "tablename");
			uint cbResult = 170U;
			StringBuilder stringBuilder = new StringBuilder(85);
			int result2 = JetApi.Err(NativeMethods.JetGetIndexInfoW(sesid.Value, dbid.Value, tablename, indexname, stringBuilder, cbResult, (uint)infoLevel));
			result = stringBuilder.ToString();
			result = StringCache.TryToIntern(result);
			return result2;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000628C File Offset: 0x0000448C
		public int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out ushort result, JET_IdxInfo infoLevel)
		{
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfoW(sesid.Value, tableid.Value, indexname, out result, 2U, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfo(sesid.Value, tableid.Value, indexname, out result, 2U, (uint)infoLevel));
			}
			return result2;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000062E8 File Offset: 0x000044E8
		public int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out int result, JET_IdxInfo infoLevel)
		{
			uint num;
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfoW(sesid.Value, tableid.Value, indexname, out num, 4U, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfo(sesid.Value, tableid.Value, indexname, out num, 4U, (uint)infoLevel));
			}
			result = (int)num;
			return result2;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006348 File Offset: 0x00004548
		public int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out JET_INDEXID result, JET_IdxInfo infoLevel)
		{
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfoW(sesid.Value, tableid.Value, indexname, out result, JET_INDEXID.SizeOfIndexId, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfo(sesid.Value, tableid.Value, indexname, out result, JET_INDEXID.SizeOfIndexId, (uint)infoLevel));
			}
			return result2;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000063AC File Offset: 0x000045AC
		public int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out JET_INDEXLIST result, JET_IdxInfo infoLevel)
		{
			NATIVE_INDEXLIST fromNativeIndexlist = default(NATIVE_INDEXLIST);
			fromNativeIndexlist.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_INDEXLIST)));
			int result2;
			if (this.Capabilities.SupportsVistaFeatures)
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfoW(sesid.Value, tableid.Value, indexname, ref fromNativeIndexlist, fromNativeIndexlist.cbStruct, (uint)infoLevel));
			}
			else
			{
				result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfo(sesid.Value, tableid.Value, indexname, ref fromNativeIndexlist, fromNativeIndexlist.cbStruct, (uint)infoLevel));
			}
			result = new JET_INDEXLIST();
			result.SetFromNativeIndexlist(fromNativeIndexlist);
			return result2;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006444 File Offset: 0x00004644
		public int JetGetTableIndexInfo(JET_SESID sesid, JET_TABLEID tableid, string indexname, out string result, JET_IdxInfo infoLevel)
		{
			uint cbResult = 170U;
			StringBuilder stringBuilder = new StringBuilder(85);
			int result2 = JetApi.Err(NativeMethods.JetGetTableIndexInfoW(sesid.Value, tableid.Value, indexname, stringBuilder, cbResult, (uint)infoLevel));
			result = stringBuilder.ToString();
			result = StringCache.TryToIntern(result);
			return result2;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006492 File Offset: 0x00004692
		public int JetRenameTable(JET_SESID sesid, JET_DBID dbid, string tableName, string newTableName)
		{
			JetApi.CheckNotNull(tableName, "tableName");
			JetApi.CheckNotNull(newTableName, "newTableName");
			return JetApi.Err(NativeMethods.JetRenameTable(sesid.Value, dbid.Value, tableName, newTableName));
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000064C6 File Offset: 0x000046C6
		public int JetRenameColumn(JET_SESID sesid, JET_TABLEID tableid, string name, string newName, RenameColumnGrbit grbit)
		{
			JetApi.CheckNotNull(name, "name");
			JetApi.CheckNotNull(newName, "newName");
			return JetApi.Err(NativeMethods.JetRenameColumn(sesid.Value, tableid.Value, name, newName, (uint)grbit));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000064FC File Offset: 0x000046FC
		public int JetSetColumnDefaultValue(JET_SESID sesid, JET_DBID dbid, string tableName, string columnName, byte[] data, int dataSize, SetColumnDefaultValueGrbit grbit)
		{
			JetApi.CheckNotNull(tableName, "tableName");
			JetApi.CheckNotNull(columnName, "columnName");
			JetApi.CheckDataSize<byte>(data, dataSize, "dataSize");
			return JetApi.Err(NativeMethods.JetSetColumnDefaultValue(sesid.Value, dbid.Value, tableName, columnName, data, checked((uint)dataSize), (uint)grbit));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006550 File Offset: 0x00004750
		public int JetGotoBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize)
		{
			JetApi.CheckNotNull(bookmark, "bookmark");
			JetApi.CheckDataSize<byte>(bookmark, bookmarkSize, "bookmarkSize");
			return JetApi.Err(NativeMethods.JetGotoBookmark(sesid.Value, tableid.Value, bookmark, checked((uint)bookmarkSize)));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006588 File Offset: 0x00004788
		public int JetGotoSecondaryIndexBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] secondaryKey, int secondaryKeySize, byte[] primaryKey, int primaryKeySize, GotoSecondaryIndexBookmarkGrbit grbit)
		{
			JetApi.CheckNotNull(secondaryKey, "secondaryKey");
			JetApi.CheckDataSize<byte>(secondaryKey, secondaryKeySize, "secondaryKeySize");
			JetApi.CheckDataSize<byte>(primaryKey, primaryKeySize, "primaryKeySize");
			return JetApi.Err(checked(NativeMethods.JetGotoSecondaryIndexBookmark(sesid.Value, tableid.Value, secondaryKey, (uint)secondaryKeySize, primaryKey, (uint)primaryKeySize, (uint)grbit)));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000065DE File Offset: 0x000047DE
		public int JetMakeKey(JET_SESID sesid, JET_TABLEID tableid, IntPtr data, int dataSize, MakeKeyGrbit grbit)
		{
			JetApi.CheckNotNegative(dataSize, "dataSize");
			return JetApi.Err(NativeMethods.JetMakeKey(sesid.Value, tableid.Value, data, checked((uint)dataSize), (uint)grbit));
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000660A File Offset: 0x0000480A
		public int JetSeek(JET_SESID sesid, JET_TABLEID tableid, SeekGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetSeek(sesid.Value, tableid.Value, (uint)grbit));
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006625 File Offset: 0x00004825
		public int JetMove(JET_SESID sesid, JET_TABLEID tableid, int numRows, MoveGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetMove(sesid.Value, tableid.Value, numRows, (uint)grbit));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006642 File Offset: 0x00004842
		public int JetSetIndexRange(JET_SESID sesid, JET_TABLEID tableid, SetIndexRangeGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetSetIndexRange(sesid.Value, tableid.Value, (uint)grbit));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006660 File Offset: 0x00004860
		public int JetIntersectIndexes(JET_SESID sesid, JET_INDEXRANGE[] ranges, int numRanges, out JET_RECORDLIST recordlist, IntersectIndexesGrbit grbit)
		{
			JetApi.CheckNotNull(ranges, "ranges");
			JetApi.CheckDataSize<JET_INDEXRANGE>(ranges, numRanges, "numRanges");
			if (numRanges < 2)
			{
				throw new ArgumentOutOfRangeException("numRanges", numRanges, "JetIntersectIndexes requires at least two index ranges.");
			}
			NATIVE_INDEXRANGE[] array = new NATIVE_INDEXRANGE[numRanges];
			for (int i = 0; i < numRanges; i++)
			{
				array[i] = ranges[i].GetNativeIndexRange();
			}
			NATIVE_RECORDLIST fromNativeRecordlist = default(NATIVE_RECORDLIST);
			checked
			{
				fromNativeRecordlist.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_RECORDLIST));
				int result = JetApi.Err(NativeMethods.JetIntersectIndexes(sesid.Value, array, (uint)array.Length, ref fromNativeRecordlist, (uint)grbit));
				recordlist = new JET_RECORDLIST();
				recordlist.SetFromNativeRecordlist(fromNativeRecordlist);
				return result;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006711 File Offset: 0x00004911
		public int JetSetCurrentIndex(JET_SESID sesid, JET_TABLEID tableid, string index)
		{
			return JetApi.Err(NativeMethods.JetSetCurrentIndex(sesid.Value, tableid.Value, index));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000672C File Offset: 0x0000492C
		public int JetSetCurrentIndex2(JET_SESID sesid, JET_TABLEID tableid, string index, SetCurrentIndexGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetSetCurrentIndex2(sesid.Value, tableid.Value, index, (uint)grbit));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006749 File Offset: 0x00004949
		public int JetSetCurrentIndex3(JET_SESID sesid, JET_TABLEID tableid, string index, SetCurrentIndexGrbit grbit, int itagSequence)
		{
			return JetApi.Err(NativeMethods.JetSetCurrentIndex3(sesid.Value, tableid.Value, index, (uint)grbit, checked((uint)itagSequence)));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006769 File Offset: 0x00004969
		public int JetSetCurrentIndex4(JET_SESID sesid, JET_TABLEID tableid, string index, JET_INDEXID indexid, SetCurrentIndexGrbit grbit, int itagSequence)
		{
			return JetApi.Err(NativeMethods.JetSetCurrentIndex4(sesid.Value, tableid.Value, index, ref indexid, (uint)grbit, checked((uint)itagSequence)));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000678C File Offset: 0x0000498C
		public int JetIndexRecordCount(JET_SESID sesid, JET_TABLEID tableid, out int numRecords, int maxRecordsToCount)
		{
			JetApi.CheckNotNegative(maxRecordsToCount, "maxRecordsToCount");
			uint num = 0U;
			int result = JetApi.Err(NativeMethods.JetIndexRecordCount(sesid.Value, tableid.Value, out num, (uint)maxRecordsToCount));
			numRecords = checked((int)num);
			return result;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000067C9 File Offset: 0x000049C9
		public int JetSetTableSequential(JET_SESID sesid, JET_TABLEID tableid, SetTableSequentialGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetSetTableSequential(sesid.Value, tableid.Value, (uint)grbit));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000067E4 File Offset: 0x000049E4
		public int JetResetTableSequential(JET_SESID sesid, JET_TABLEID tableid, ResetTableSequentialGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetResetTableSequential(sesid.Value, tableid.Value, (uint)grbit));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006800 File Offset: 0x00004A00
		public int JetGetRecordPosition(JET_SESID sesid, JET_TABLEID tableid, out JET_RECPOS recpos)
		{
			recpos = new JET_RECPOS();
			NATIVE_RECPOS nativeRecpos = recpos.GetNativeRecpos();
			int result = JetApi.Err(NativeMethods.JetGetRecordPosition(sesid.Value, tableid.Value, out nativeRecpos, nativeRecpos.cbStruct));
			recpos.SetFromNativeRecpos(nativeRecpos);
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006848 File Offset: 0x00004A48
		public int JetGotoPosition(JET_SESID sesid, JET_TABLEID tableid, JET_RECPOS recpos)
		{
			NATIVE_RECPOS nativeRecpos = recpos.GetNativeRecpos();
			return JetApi.Err(NativeMethods.JetGotoPosition(sesid.Value, tableid.Value, ref nativeRecpos));
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006878 File Offset: 0x00004A78
		public unsafe int JetPrereadKeys(JET_SESID sesid, JET_TABLEID tableid, byte[][] keys, int[] keyLengths, int keyIndex, int keyCount, out int keysPreread, PrereadKeysGrbit grbit)
		{
			this.CheckSupportsWindows7Features("JetPrereadKeys");
			JetApi.CheckDataSize<byte[]>(keys, keyIndex, "keyIndex", keyCount, "keyCount");
			JetApi.CheckDataSize<int>(keyLengths, keyIndex, "keyIndex", keyCount, "keyCount");
			JetApi.CheckNotNull(keys, "keys");
			JetApi.CheckNotNull(keyLengths, "keyLengths");
			void** ptr;
			uint* ptr2;
			checked
			{
				ptr = stackalloc void*[unchecked((UIntPtr)keyCount) * (UIntPtr)sizeof(void*)];
				ptr2 = stackalloc uint[unchecked((UIntPtr)keyCount) * 4];
			}
			int result;
			using (GCHandleCollection gchandleCollection = default(GCHandleCollection))
			{
				for (int i = 0; i < keyCount; i++)
				{
					*(IntPtr*)(ptr + (IntPtr)i * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) = (void*)gchandleCollection.Add(keys[keyIndex + i]);
					ptr2[i] = checked((uint)keyLengths[keyIndex + i]);
				}
				result = JetApi.Err(NativeMethods.JetPrereadKeys(sesid.Value, tableid.Value, ptr, ptr2, keyCount, out keysPreread, (uint)grbit));
			}
			return result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006974 File Offset: 0x00004B74
		public int JetGetBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize)
		{
			JetApi.CheckDataSize<byte>(bookmark, bookmarkSize, "bookmarkSize");
			uint numBytesActual = 0U;
			int result = JetApi.Err(NativeMethods.JetGetBookmark(sesid.Value, tableid.Value, bookmark, checked((uint)bookmarkSize), out numBytesActual));
			actualBookmarkSize = JetApi.GetActualSize(numBytesActual);
			return result;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000069BC File Offset: 0x00004BBC
		public int JetGetSecondaryIndexBookmark(JET_SESID sesid, JET_TABLEID tableid, byte[] secondaryKey, int secondaryKeySize, out int actualSecondaryKeySize, byte[] primaryKey, int primaryKeySize, out int actualPrimaryKeySize, GetSecondaryIndexBookmarkGrbit grbit)
		{
			JetApi.CheckDataSize<byte>(secondaryKey, secondaryKeySize, "secondaryKeySize");
			JetApi.CheckDataSize<byte>(primaryKey, primaryKeySize, "primaryKeySize");
			uint numBytesActual = 0U;
			uint numBytesActual2 = 0U;
			int result = JetApi.Err(checked(NativeMethods.JetGetSecondaryIndexBookmark(sesid.Value, tableid.Value, secondaryKey, (uint)secondaryKeySize, out numBytesActual, primaryKey, (uint)primaryKeySize, out numBytesActual2, (uint)grbit)));
			actualSecondaryKeySize = JetApi.GetActualSize(numBytesActual);
			actualPrimaryKeySize = JetApi.GetActualSize(numBytesActual2);
			return result;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006A24 File Offset: 0x00004C24
		public int JetRetrieveKey(JET_SESID sesid, JET_TABLEID tableid, byte[] data, int dataSize, out int actualDataSize, RetrieveKeyGrbit grbit)
		{
			JetApi.CheckDataSize<byte>(data, dataSize, "dataSize");
			uint numBytesActual = 0U;
			int result = JetApi.Err(NativeMethods.JetRetrieveKey(sesid.Value, tableid.Value, data, checked((uint)dataSize), out numBytesActual, (uint)grbit));
			actualDataSize = JetApi.GetActualSize(numBytesActual);
			return result;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006A6C File Offset: 0x00004C6C
		public int JetRetrieveColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, IntPtr data, int dataSize, out int actualDataSize, RetrieveColumnGrbit grbit, JET_RETINFO retinfo)
		{
			JetApi.CheckNotNegative(dataSize, "dataSize");
			uint num = 0U;
			checked
			{
				int result;
				if (retinfo != null)
				{
					NATIVE_RETINFO nativeRetinfo = retinfo.GetNativeRetinfo();
					result = JetApi.Err(NativeMethods.JetRetrieveColumn(sesid.Value, tableid.Value, columnid.Value, data, (uint)dataSize, out num, (uint)grbit, ref nativeRetinfo));
					retinfo.SetFromNativeRetinfo(nativeRetinfo);
				}
				else
				{
					result = JetApi.Err(NativeMethods.JetRetrieveColumn(sesid.Value, tableid.Value, columnid.Value, data, (uint)dataSize, out num, (uint)grbit, IntPtr.Zero));
				}
				actualDataSize = (int)num;
				return result;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006AFC File Offset: 0x00004CFC
		public unsafe int JetRetrieveColumns(JET_SESID sesid, JET_TABLEID tableid, NATIVE_RETRIEVECOLUMN* retrievecolumns, int numColumns)
		{
			return JetApi.Err(NativeMethods.JetRetrieveColumns(sesid.Value, tableid.Value, retrievecolumns, checked((uint)numColumns)));
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006B1C File Offset: 0x00004D1C
		public unsafe int JetEnumerateColumns(JET_SESID sesid, JET_TABLEID tableid, int numColumnids, JET_ENUMCOLUMNID[] columnids, out int numColumnValues, out JET_ENUMCOLUMN[] columnValues, JET_PFNREALLOC allocator, IntPtr allocatorContext, int maxDataSize, EnumerateColumnsGrbit grbit)
		{
			JetApi.CheckNotNull(allocator, "allocator");
			JetApi.CheckNotNegative(maxDataSize, "maxDataSize");
			JetApi.CheckDataSize<JET_ENUMCOLUMNID>(columnids, numColumnids, "numColumnids");
			checked
			{
				NATIVE_ENUMCOLUMNID* ptr = stackalloc NATIVE_ENUMCOLUMNID[unchecked((UIntPtr)numColumnids) * (UIntPtr)sizeof(NATIVE_ENUMCOLUMNID)];
				int num = JetApi.ConvertEnumColumnids(columnids, numColumnids, ptr);
				uint* tags = stackalloc uint[unchecked((UIntPtr)num) * 4];
				JetApi.ConvertEnumColumnidTags(columnids, numColumnids, ptr, tags);
				uint numEnumColumn;
				NATIVE_ENUMCOLUMN* nativeenumcolumns;
				int err = NativeMethods.JetEnumerateColumns(sesid.Value, tableid.Value, (uint)numColumnids, (numColumnids > 0) ? ptr : null, out numEnumColumn, out nativeenumcolumns, allocator, allocatorContext, (uint)maxDataSize, (uint)grbit);
				JetApi.ConvertEnumerateColumnsResult(allocator, allocatorContext, numEnumColumn, nativeenumcolumns, out numColumnValues, out columnValues);
				return JetApi.Err(err);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006BBC File Offset: 0x00004DBC
		public int JetGetRecordSize(JET_SESID sesid, JET_TABLEID tableid, ref JET_RECSIZE recsize, GetRecordSizeGrbit grbit)
		{
			this.CheckSupportsVistaFeatures("JetGetRecordSize");
			int err;
			if (this.Capabilities.SupportsWindows7Features)
			{
				NATIVE_RECSIZE2 nativeRecsize = recsize.GetNativeRecsize2();
				err = NativeMethods.JetGetRecordSize2(sesid.Value, tableid.Value, ref nativeRecsize, (uint)grbit);
				recsize.SetFromNativeRecsize(nativeRecsize);
			}
			else
			{
				NATIVE_RECSIZE nativeRecsize2 = recsize.GetNativeRecsize();
				err = NativeMethods.JetGetRecordSize(sesid.Value, tableid.Value, ref nativeRecsize2, (uint)grbit);
				recsize.SetFromNativeRecsize(nativeRecsize2);
			}
			return JetApi.Err(err);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006C35 File Offset: 0x00004E35
		public int JetDelete(JET_SESID sesid, JET_TABLEID tableid)
		{
			return JetApi.Err(NativeMethods.JetDelete(sesid.Value, tableid.Value));
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006C4F File Offset: 0x00004E4F
		public int JetPrepareUpdate(JET_SESID sesid, JET_TABLEID tableid, JET_prep prep)
		{
			return JetApi.Err(NativeMethods.JetPrepareUpdate(sesid.Value, tableid.Value, (uint)prep));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006C6C File Offset: 0x00004E6C
		public int JetUpdate(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize)
		{
			JetApi.CheckDataSize<byte>(bookmark, bookmarkSize, "bookmarkSize");
			uint numBytesActual;
			int result = JetApi.Err(NativeMethods.JetUpdate(sesid.Value, tableid.Value, bookmark, checked((uint)bookmarkSize), out numBytesActual));
			actualBookmarkSize = JetApi.GetActualSize(numBytesActual);
			return result;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public int JetUpdate2(JET_SESID sesid, JET_TABLEID tableid, byte[] bookmark, int bookmarkSize, out int actualBookmarkSize, UpdateGrbit grbit)
		{
			JetApi.CheckDataSize<byte>(bookmark, bookmarkSize, "bookmarkSize");
			this.CheckSupportsServer2003Features("JetUpdate2");
			uint numBytesActual;
			int result = JetApi.Err(NativeMethods.JetUpdate2(sesid.Value, tableid.Value, bookmark, checked((uint)bookmarkSize), out numBytesActual, (uint)grbit));
			actualBookmarkSize = JetApi.GetActualSize(numBytesActual);
			return result;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006D00 File Offset: 0x00004F00
		public int JetSetColumn(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, IntPtr data, int dataSize, SetColumnGrbit grbit, JET_SETINFO setinfo)
		{
			JetApi.CheckNotNegative(dataSize, "dataSize");
			if (IntPtr.Zero == data && dataSize > 0 && SetColumnGrbit.SizeLV != (grbit & SetColumnGrbit.SizeLV))
			{
				throw new ArgumentOutOfRangeException("dataSize", dataSize, "cannot be greater than the length of the data (unless the SizeLV option is used)");
			}
			checked
			{
				if (setinfo != null)
				{
					NATIVE_SETINFO nativeSetinfo = setinfo.GetNativeSetinfo();
					return JetApi.Err(NativeMethods.JetSetColumn(sesid.Value, tableid.Value, columnid.Value, data, (uint)dataSize, (uint)grbit, ref nativeSetinfo));
				}
				return JetApi.Err(NativeMethods.JetSetColumn(sesid.Value, tableid.Value, columnid.Value, data, (uint)dataSize, (uint)grbit, IntPtr.Zero));
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006DAA File Offset: 0x00004FAA
		public unsafe int JetSetColumns(JET_SESID sesid, JET_TABLEID tableid, NATIVE_SETCOLUMN* setcolumns, int numColumns)
		{
			return JetApi.Err(NativeMethods.JetSetColumns(sesid.Value, tableid.Value, setcolumns, checked((uint)numColumns)));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public int JetGetLock(JET_SESID sesid, JET_TABLEID tableid, GetLockGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetGetLock(sesid.Value, tableid.Value, (uint)grbit));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public int JetEscrowUpdate(JET_SESID sesid, JET_TABLEID tableid, JET_COLUMNID columnid, byte[] delta, int deltaSize, byte[] previousValue, int previousValueLength, out int actualPreviousValueLength, EscrowUpdateGrbit grbit)
		{
			JetApi.CheckNotNull(delta, "delta");
			JetApi.CheckDataSize<byte>(delta, deltaSize, "deltaSize");
			JetApi.CheckDataSize<byte>(previousValue, previousValueLength, "previousValueLength");
			uint num = 0U;
			checked
			{
				int result = JetApi.Err(NativeMethods.JetEscrowUpdate(sesid.Value, tableid.Value, columnid.Value, delta, (uint)deltaSize, previousValue, (uint)previousValueLength, out num, (uint)grbit));
				actualPreviousValueLength = (int)num;
				return result;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006E50 File Offset: 0x00005050
		public int JetRegisterCallback(JET_SESID sesid, JET_TABLEID tableid, JET_cbtyp cbtyp, JET_CALLBACK callback, IntPtr context, out JET_HANDLE callbackId)
		{
			JetApi.CheckNotNull(callback, "callback");
			callbackId = JET_HANDLE.Nil;
			return JetApi.Err(NativeMethods.JetRegisterCallback(sesid.Value, tableid.Value, (uint)cbtyp, this.callbackWrappers.Add(callback).NativeCallback, context, out callbackId.Value));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006EA9 File Offset: 0x000050A9
		public int JetUnregisterCallback(JET_SESID sesid, JET_TABLEID tableid, JET_cbtyp cbtyp, JET_HANDLE callbackId)
		{
			this.callbackWrappers.Collect();
			return JetApi.Err(NativeMethods.JetUnregisterCallback(sesid.Value, tableid.Value, (uint)cbtyp, callbackId.Value));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006ED8 File Offset: 0x000050D8
		public int JetDefragment(JET_SESID sesid, JET_DBID dbid, string tableName, ref int passes, ref int seconds, DefragGrbit grbit)
		{
			uint num = (uint)passes;
			uint num2 = (uint)seconds;
			int result = JetApi.Err(NativeMethods.JetDefragment(sesid.Value, dbid.Value, tableName, ref num, ref num2, (uint)grbit));
			passes = (int)num;
			seconds = (int)num2;
			return result;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006F18 File Offset: 0x00005118
		public int JetDefragment2(JET_SESID sesid, JET_DBID dbid, string tableName, ref int passes, ref int seconds, JET_CALLBACK callback, DefragGrbit grbit)
		{
			uint num = (uint)passes;
			uint num2 = (uint)seconds;
			IntPtr callback2;
			if (callback == null)
			{
				callback2 = IntPtr.Zero;
			}
			else
			{
				JetCallbackWrapper jetCallbackWrapper = this.callbackWrappers.Add(callback);
				callback2 = Marshal.GetFunctionPointerForDelegate(jetCallbackWrapper.NativeCallback);
			}
			int result = JetApi.Err(NativeMethods.JetDefragment2(sesid.Value, dbid.Value, tableName, ref num, ref num2, callback2, (uint)grbit));
			passes = (int)num;
			seconds = (int)num2;
			this.callbackWrappers.Collect();
			return result;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006F8A File Offset: 0x0000518A
		public int JetIdle(JET_SESID sesid, IdleGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetIdle(sesid.Value, (uint)grbit));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006F9E File Offset: 0x0000519E
		public int JetConfigureProcessForCrashDump(CrashDumpGrbit grbit)
		{
			this.CheckSupportsWindows7Features("JetConfigureProcessForCrashDump");
			return JetApi.Err(NativeMethods.JetConfigureProcessForCrashDump((uint)grbit));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006FB6 File Offset: 0x000051B6
		public int JetFreeBuffer(IntPtr buffer)
		{
			return JetApi.Err(NativeMethods.JetFreeBuffer(buffer));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006FC4 File Offset: 0x000051C4
		internal static int GetActualSize(uint numBytesActual)
		{
			int result;
			if (3722304989U == numBytesActual)
			{
				result = 0;
			}
			else
			{
				result = checked((int)numBytesActual);
			}
			return result;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006FE4 File Offset: 0x000051E4
		private static void CheckDataSize<T>(ICollection<T> data, int dataOffset, string offsetArgumentName, int dataSize, string sizeArgumentName)
		{
			JetApi.CheckNotNegative(dataSize, sizeArgumentName);
			JetApi.CheckNotNegative(dataOffset, offsetArgumentName);
			if ((data == null && dataOffset != 0) || (data != null && dataOffset >= data.Count))
			{
				throw new ArgumentOutOfRangeException(offsetArgumentName, dataOffset, "cannot be greater than the length of the buffer");
			}
			if ((data == null && dataSize != 0) || (data != null && dataSize > data.Count - dataOffset))
			{
				throw new ArgumentOutOfRangeException(sizeArgumentName, dataSize, "cannot be greater than the length of the buffer");
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000704B File Offset: 0x0000524B
		private static void CheckDataSize<T>(ICollection<T> data, int dataSize, string argumentName)
		{
			JetApi.CheckDataSize<T>(data, 0, string.Empty, dataSize, argumentName);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000705B File Offset: 0x0000525B
		private static void CheckNotNull(object o, string paramName)
		{
			if (o == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007067 File Offset: 0x00005267
		private static void CheckNotNegative(int i, string paramName)
		{
			if (i < 0)
			{
				throw new ArgumentOutOfRangeException(paramName, i, "cannot be negative");
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007080 File Offset: 0x00005280
		private static Exception UnsupportedApiException(string method)
		{
			string message = string.Format(CultureInfo.InvariantCulture, "Method {0} is not supported by this version of ESENT", new object[]
			{
				method
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000070AF File Offset: 0x000052AF
		[Conditional("TRACE")]
		private static void TraceFunctionCall(string function)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000070B1 File Offset: 0x000052B1
		private static int Err(int err)
		{
			return err;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000070B4 File Offset: 0x000052B4
		[Conditional("TRACE")]
		private static void TraceErr(int err)
		{
			if (err == 0)
			{
				return;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000070C0 File Offset: 0x000052C0
		private unsafe static int ConvertEnumColumnids(IList<JET_ENUMCOLUMNID> columnids, int numColumnids, NATIVE_ENUMCOLUMNID* nativecolumnids)
		{
			int num = 0;
			for (int i = 0; i < numColumnids; i++)
			{
				nativecolumnids[i] = columnids[i].GetNativeEnumColumnid();
				checked
				{
					num += columnids[i].ctagSequence;
				}
			}
			return num;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007108 File Offset: 0x00005308
		private unsafe static void ConvertEnumColumnidTags(IList<JET_ENUMCOLUMNID> columnids, int numColumnids, NATIVE_ENUMCOLUMNID* nativecolumnids, uint* tags)
		{
			for (int i = 0; i < numColumnids; i++)
			{
				nativecolumnids[i].rgtagSequence = tags;
				for (int j = 0; j < columnids[i].ctagSequence; j++)
				{
					nativecolumnids[i].rgtagSequence[j] = checked((uint)columnids[i].rgtagSequence[j]);
				}
				tags += columnids[i].ctagSequence;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007184 File Offset: 0x00005384
		private unsafe static void ConvertEnumerateColumnsResult(JET_PFNREALLOC allocator, IntPtr allocatorContext, uint numEnumColumn, NATIVE_ENUMCOLUMN* nativeenumcolumns, out int numColumnValues, out JET_ENUMCOLUMN[] columnValues)
		{
			numColumnValues = checked((int)numEnumColumn);
			columnValues = new JET_ENUMCOLUMN[numColumnValues];
			for (int i = 0; i < numColumnValues; i++)
			{
				columnValues[i] = new JET_ENUMCOLUMN();
				columnValues[i].SetFromNativeEnumColumn(nativeenumcolumns[i]);
				if (JET_wrn.ColumnSingleValue != columnValues[i].err)
				{
					columnValues[i].rgEnumColumnValue = new JET_ENUMCOLUMNVALUE[columnValues[i].cEnumColumnValue];
					for (int j = 0; j < columnValues[i].cEnumColumnValue; j++)
					{
						columnValues[i].rgEnumColumnValue[j] = new JET_ENUMCOLUMNVALUE();
						columnValues[i].rgEnumColumnValue[j].SetFromNativeEnumColumnValue(nativeenumcolumns[i].rgEnumColumnValue[j]);
					}
					allocator(allocatorContext, new IntPtr((void*)nativeenumcolumns[i].rgEnumColumnValue), 0U);
					nativeenumcolumns[i].rgEnumColumnValue = null;
				}
			}
			allocator(allocatorContext, new IntPtr((void*)nativeenumcolumns), 0U);
			nativeenumcolumns = null;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000072A0 File Offset: 0x000054A0
		private static NATIVE_COLUMNDEF[] GetNativecolumndefs(IList<JET_COLUMNDEF> columns, int numColumns)
		{
			NATIVE_COLUMNDEF[] array = new NATIVE_COLUMNDEF[numColumns];
			for (int i = 0; i < numColumns; i++)
			{
				array[i] = columns[i].GetNativeColumndef();
			}
			return array;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000072DC File Offset: 0x000054DC
		private static IntPtr GetNativeConditionalColumns(IList<JET_CONDITIONALCOLUMN> conditionalColumns, bool useUnicodeData, ref GCHandleCollection handles)
		{
			if (conditionalColumns == null)
			{
				return IntPtr.Zero;
			}
			NATIVE_CONDITIONALCOLUMN[] array = new NATIVE_CONDITIONALCOLUMN[conditionalColumns.Count];
			for (int i = 0; i < conditionalColumns.Count; i++)
			{
				array[i] = conditionalColumns[i].GetNativeConditionalColumn();
				if (useUnicodeData)
				{
					array[i].szColumnName = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(conditionalColumns[i].szColumnName));
				}
				else
				{
					array[i].szColumnName = handles.Add(Util.ConvertToNullTerminatedAsciiByteArray(conditionalColumns[i].szColumnName));
				}
			}
			return handles.Add(array);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000737C File Offset: 0x0000557C
		private static IntPtr GetNativeColumnCreates(IList<JET_COLUMNCREATE> managedColumnCreates, bool useUnicodeData, ref GCHandleCollection handles)
		{
			IntPtr result = IntPtr.Zero;
			if (managedColumnCreates != null && managedColumnCreates.Count > 0)
			{
				NATIVE_COLUMNCREATE[] array = new NATIVE_COLUMNCREATE[managedColumnCreates.Count];
				for (int i = 0; i < managedColumnCreates.Count; i++)
				{
					if (managedColumnCreates[i] != null)
					{
						JET_COLUMNCREATE jet_COLUMNCREATE = managedColumnCreates[i];
						array[i] = jet_COLUMNCREATE.GetNativeColumnCreate();
						if (useUnicodeData)
						{
							array[i].szColumnName = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(jet_COLUMNCREATE.szColumnName));
						}
						else
						{
							array[i].szColumnName = handles.Add(Util.ConvertToNullTerminatedAsciiByteArray(jet_COLUMNCREATE.szColumnName));
						}
						if (jet_COLUMNCREATE.cbDefault > 0)
						{
							array[i].pvDefault = handles.Add(jet_COLUMNCREATE.pvDefault);
						}
					}
				}
				result = handles.Add(array);
			}
			return result;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007450 File Offset: 0x00005650
		private unsafe static NATIVE_INDEXCREATE[] GetNativeIndexCreates(IList<JET_INDEXCREATE> managedIndexCreates, ref GCHandleCollection handles)
		{
			NATIVE_INDEXCREATE[] array = null;
			if (managedIndexCreates != null && managedIndexCreates.Count > 0)
			{
				array = new NATIVE_INDEXCREATE[managedIndexCreates.Count];
				for (int i = 0; i < managedIndexCreates.Count; i++)
				{
					array[i] = managedIndexCreates[i].GetNativeIndexcreate();
					if (managedIndexCreates[i].pidxUnicode != null)
					{
						NATIVE_UNICODEINDEX nativeUnicodeIndex = managedIndexCreates[i].pidxUnicode.GetNativeUnicodeIndex();
						array[i].pidxUnicode = (NATIVE_UNICODEINDEX*)((void*)handles.Add(nativeUnicodeIndex));
						NATIVE_INDEXCREATE[] array2 = array;
						int num = i;
						array2[num].grbit = (array2[num].grbit | 2048U);
					}
					array[i].szKey = handles.Add(Util.ConvertToNullTerminatedAsciiByteArray(managedIndexCreates[i].szKey));
					array[i].szIndexName = handles.Add(Util.ConvertToNullTerminatedAsciiByteArray(managedIndexCreates[i].szIndexName));
					array[i].rgconditionalcolumn = JetApi.GetNativeConditionalColumns(managedIndexCreates[i].rgconditionalcolumn, false, ref handles);
				}
			}
			return array;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007568 File Offset: 0x00005768
		private unsafe static NATIVE_INDEXCREATE1[] GetNativeIndexCreate1s(IList<JET_INDEXCREATE> managedIndexCreates, ref GCHandleCollection handles)
		{
			NATIVE_INDEXCREATE1[] array = null;
			if (managedIndexCreates != null && managedIndexCreates.Count > 0)
			{
				array = new NATIVE_INDEXCREATE1[managedIndexCreates.Count];
				for (int i = 0; i < managedIndexCreates.Count; i++)
				{
					array[i] = managedIndexCreates[i].GetNativeIndexcreate1();
					if (managedIndexCreates[i].pidxUnicode != null)
					{
						NATIVE_UNICODEINDEX nativeUnicodeIndex = managedIndexCreates[i].pidxUnicode.GetNativeUnicodeIndex();
						array[i].indexcreate.pidxUnicode = (NATIVE_UNICODEINDEX*)((void*)handles.Add(nativeUnicodeIndex));
						NATIVE_INDEXCREATE1[] array2 = array;
						int num = i;
						array2[num].indexcreate.grbit = (array2[num].indexcreate.grbit | 2048U);
					}
					array[i].indexcreate.szKey = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].szKey));
					NATIVE_INDEXCREATE1[] array3 = array;
					int num2 = i;
					array3[num2].indexcreate.cbKey = array3[num2].indexcreate.cbKey * 2U;
					array[i].indexcreate.szIndexName = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].szIndexName));
					array[i].indexcreate.rgconditionalcolumn = JetApi.GetNativeConditionalColumns(managedIndexCreates[i].rgconditionalcolumn, false, ref handles);
				}
			}
			return array;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000076B4 File Offset: 0x000058B4
		private unsafe static NATIVE_INDEXCREATE2[] GetNativeIndexCreate2s(IList<JET_INDEXCREATE> managedIndexCreates, ref GCHandleCollection handles)
		{
			NATIVE_INDEXCREATE2[] array = null;
			if (managedIndexCreates != null && managedIndexCreates.Count > 0)
			{
				array = new NATIVE_INDEXCREATE2[managedIndexCreates.Count];
				for (int i = 0; i < managedIndexCreates.Count; i++)
				{
					array[i] = managedIndexCreates[i].GetNativeIndexcreate2();
					if (managedIndexCreates[i].pidxUnicode != null)
					{
						NATIVE_UNICODEINDEX nativeUnicodeIndex = managedIndexCreates[i].pidxUnicode.GetNativeUnicodeIndex();
						array[i].indexcreate1.indexcreate.pidxUnicode = (NATIVE_UNICODEINDEX*)((void*)handles.Add(nativeUnicodeIndex));
						NATIVE_INDEXCREATE2[] array2 = array;
						int num = i;
						array2[num].indexcreate1.indexcreate.grbit = (array2[num].indexcreate1.indexcreate.grbit | 2048U);
					}
					array[i].indexcreate1.indexcreate.szKey = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].szKey));
					NATIVE_INDEXCREATE2[] array3 = array;
					int num2 = i;
					array3[num2].indexcreate1.indexcreate.cbKey = array3[num2].indexcreate1.indexcreate.cbKey * 2U;
					array[i].indexcreate1.indexcreate.szIndexName = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].szIndexName));
					array[i].indexcreate1.indexcreate.rgconditionalcolumn = JetApi.GetNativeConditionalColumns(managedIndexCreates[i].rgconditionalcolumn, true, ref handles);
					if (managedIndexCreates[i].pSpaceHints != null)
					{
						NATIVE_SPACEHINTS nativeSpaceHints = managedIndexCreates[i].pSpaceHints.GetNativeSpaceHints();
						array[i].pSpaceHints = handles.Add(nativeSpaceHints);
					}
				}
			}
			return array;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007854 File Offset: 0x00005A54
		private static void SetColumnids(IList<JET_COLUMNDEF> columns, IList<JET_COLUMNID> columnids, IList<uint> nativecolumnids, int numColumns)
		{
			for (int i = 0; i < numColumns; i++)
			{
				columnids[i] = new JET_COLUMNID
				{
					Value = nativecolumnids[i]
				};
				columns[i].columnid = columnids[i];
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000078A0 File Offset: 0x00005AA0
		private static int CreateIndexes(JET_SESID sesid, JET_TABLEID tableid, IList<JET_INDEXCREATE> indexcreates, int numIndexCreates)
		{
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				NATIVE_INDEXCREATE[] nativeIndexCreates = JetApi.GetNativeIndexCreates(indexcreates, ref gchandleCollection);
				result = JetApi.Err(NativeMethods.JetCreateIndex2(sesid.Value, tableid.Value, nativeIndexCreates, checked((uint)numIndexCreates)));
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000078F8 File Offset: 0x00005AF8
		private static int CreateIndexes1(JET_SESID sesid, JET_TABLEID tableid, IList<JET_INDEXCREATE> indexcreates, int numIndexCreates)
		{
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				NATIVE_INDEXCREATE1[] nativeIndexCreate1s = JetApi.GetNativeIndexCreate1s(indexcreates, ref gchandleCollection);
				result = JetApi.Err(NativeMethods.JetCreateIndex2W(sesid.Value, tableid.Value, nativeIndexCreate1s, checked((uint)numIndexCreates)));
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007950 File Offset: 0x00005B50
		private static int CreateIndexes2(JET_SESID sesid, JET_TABLEID tableid, IList<JET_INDEXCREATE> indexcreates, int numIndexCreates)
		{
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				NATIVE_INDEXCREATE2[] nativeIndexCreate2s = JetApi.GetNativeIndexCreate2s(indexcreates, ref gchandleCollection);
				result = JetApi.Err(NativeMethods.JetCreateIndex3W(sesid.Value, tableid.Value, nativeIndexCreate2s, checked((uint)numIndexCreates)));
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000079A8 File Offset: 0x00005BA8
		private unsafe static int CreateTableColumnIndex3(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate)
		{
			NATIVE_TABLECREATE3 nativeTableCreate = tablecreate.GetNativeTableCreate3();
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				nativeTableCreate.rgcolumncreate = (NATIVE_COLUMNCREATE*)((void*)JetApi.GetNativeColumnCreates(tablecreate.rgcolumncreate, true, ref gchandleCollection));
				NATIVE_INDEXCREATE2[] nativeIndexCreate2s = JetApi.GetNativeIndexCreate2s(tablecreate.rgindexcreate, ref gchandleCollection);
				nativeTableCreate.rgindexcreate = gchandleCollection.Add(nativeIndexCreate2s);
				if (tablecreate.pSeqSpacehints != null)
				{
					NATIVE_SPACEHINTS nativeSpaceHints = tablecreate.pSeqSpacehints.GetNativeSpaceHints();
					nativeTableCreate.pSeqSpacehints = (NATIVE_SPACEHINTS*)((void*)gchandleCollection.Add(nativeSpaceHints));
				}
				if (tablecreate.pLVSpacehints != null)
				{
					NATIVE_SPACEHINTS nativeSpaceHints2 = tablecreate.pLVSpacehints.GetNativeSpaceHints();
					nativeTableCreate.pLVSpacehints = (NATIVE_SPACEHINTS*)((void*)gchandleCollection.Add(nativeSpaceHints2));
				}
				int err = NativeMethods.JetCreateTableColumnIndex3W(sesid.Value, dbid.Value, ref nativeTableCreate);
				tablecreate.tableid = new JET_TABLEID
				{
					Value = nativeTableCreate.tableid
				};
				tablecreate.cCreated = checked((int)nativeTableCreate.cCreated);
				if (tablecreate.rgcolumncreate != null)
				{
					for (int i = 0; i < tablecreate.rgcolumncreate.Length; i++)
					{
						tablecreate.rgcolumncreate[i].SetFromNativeColumnCreate(nativeTableCreate.rgcolumncreate[i]);
					}
				}
				if (tablecreate.rgindexcreate != null)
				{
					for (int j = 0; j < tablecreate.rgindexcreate.Length; j++)
					{
						tablecreate.rgindexcreate[j].SetFromNativeIndexCreate(nativeIndexCreate2s[j]);
					}
				}
				result = JetApi.Err(err);
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007B4C File Offset: 0x00005D4C
		private unsafe JET_INSTANCE_INFO[] ConvertInstanceInfosUnicode(uint nativeNumInstance, NATIVE_INSTANCE_INFO* nativeInstanceInfos)
		{
			int num = checked((int)nativeNumInstance);
			JET_INSTANCE_INFO[] array = new JET_INSTANCE_INFO[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new JET_INSTANCE_INFO();
				array[i].SetFromNativeUnicode(nativeInstanceInfos[i]);
			}
			this.JetFreeBuffer(new IntPtr((void*)nativeInstanceInfos));
			return array;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007BA0 File Offset: 0x00005DA0
		private unsafe JET_INSTANCE_INFO[] ConvertInstanceInfosAscii(uint nativeNumInstance, NATIVE_INSTANCE_INFO* nativeInstanceInfos)
		{
			int num = checked((int)nativeNumInstance);
			JET_INSTANCE_INFO[] array = new JET_INSTANCE_INFO[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new JET_INSTANCE_INFO();
				array[i].SetFromNativeAscii(nativeInstanceInfos[i]);
			}
			this.JetFreeBuffer(new IntPtr((void*)nativeInstanceInfos));
			return array;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007BF1 File Offset: 0x00005DF1
		private void CheckSupportsServer2003Features(string api)
		{
			if (!this.Capabilities.SupportsServer2003Features)
			{
				throw JetApi.UnsupportedApiException(api);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007C07 File Offset: 0x00005E07
		private void CheckSupportsVistaFeatures(string api)
		{
			if (!this.Capabilities.SupportsVistaFeatures)
			{
				throw JetApi.UnsupportedApiException(api);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007C1D File Offset: 0x00005E1D
		private void CheckSupportsWindows7Features(string api)
		{
			if (!this.Capabilities.SupportsWindows7Features)
			{
				throw JetApi.UnsupportedApiException(api);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007C33 File Offset: 0x00005E33
		private void CheckSupportsWindows8Features(string api)
		{
			if (!this.Capabilities.SupportsWindows8Features)
			{
				throw JetApi.UnsupportedApiException(api);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007C4C File Offset: 0x00005E4C
		private unsafe int CreateTableColumnIndex2(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate)
		{
			NATIVE_TABLECREATE2 nativeTableCreate = tablecreate.GetNativeTableCreate2();
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				NATIVE_INDEXCREATE1[] array = null;
				NATIVE_INDEXCREATE[] array2 = null;
				int err;
				if (this.Capabilities.SupportsVistaFeatures)
				{
					nativeTableCreate.rgcolumncreate = (NATIVE_COLUMNCREATE*)((void*)JetApi.GetNativeColumnCreates(tablecreate.rgcolumncreate, true, ref gchandleCollection));
					array = JetApi.GetNativeIndexCreate1s(tablecreate.rgindexcreate, ref gchandleCollection);
					nativeTableCreate.rgindexcreate = gchandleCollection.Add(array);
					err = NativeMethods.JetCreateTableColumnIndex2W(sesid.Value, dbid.Value, ref nativeTableCreate);
				}
				else
				{
					nativeTableCreate.rgcolumncreate = (NATIVE_COLUMNCREATE*)((void*)JetApi.GetNativeColumnCreates(tablecreate.rgcolumncreate, false, ref gchandleCollection));
					array2 = JetApi.GetNativeIndexCreates(tablecreate.rgindexcreate, ref gchandleCollection);
					nativeTableCreate.rgindexcreate = gchandleCollection.Add(array2);
					err = NativeMethods.JetCreateTableColumnIndex2(sesid.Value, dbid.Value, ref nativeTableCreate);
				}
				tablecreate.tableid = new JET_TABLEID
				{
					Value = nativeTableCreate.tableid
				};
				tablecreate.cCreated = checked((int)nativeTableCreate.cCreated);
				if (tablecreate.rgcolumncreate != null)
				{
					for (int i = 0; i < tablecreate.rgcolumncreate.Length; i++)
					{
						tablecreate.rgcolumncreate[i].SetFromNativeColumnCreate(nativeTableCreate.rgcolumncreate[i]);
					}
				}
				if (tablecreate.rgindexcreate != null)
				{
					for (int j = 0; j < tablecreate.rgindexcreate.Length; j++)
					{
						if (array != null)
						{
							tablecreate.rgindexcreate[j].SetFromNativeIndexCreate(array[j]);
						}
						else
						{
							tablecreate.rgindexcreate[j].SetFromNativeIndexCreate(array2[j]);
						}
					}
				}
				result = JetApi.Err(err);
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007E14 File Offset: 0x00006014
		public int JetBeginTransaction3(JET_SESID sesid, long userTransactionId, BeginTransactionGrbit grbit)
		{
			return JetApi.Err(NativeMethods.JetBeginTransaction3(sesid.Value, userTransactionId, (uint)grbit));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007E2C File Offset: 0x0000602C
		public int JetGetErrorInfo(JET_err error, out JET_ERRINFOBASIC errinfo)
		{
			this.CheckSupportsWindows8Features("JetGetErrorInfo");
			NATIVE_ERRINFOBASIC native_ERRINFOBASIC = default(NATIVE_ERRINFOBASIC);
			errinfo = new JET_ERRINFOBASIC();
			native_ERRINFOBASIC.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_ERRINFOBASIC)));
			int num = (int)error;
			int result = NativeMethods.JetGetErrorInfoW(ref num, ref native_ERRINFOBASIC, native_ERRINFOBASIC.cbStruct, 1U, 0U);
			errinfo.SetFromNative(ref native_ERRINFOBASIC);
			return result;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007E8C File Offset: 0x0000608C
		public int JetResizeDatabase(JET_SESID sesid, JET_DBID dbid, int desiredPages, out int actualPages, ResizeDatabaseGrbit grbit)
		{
			this.CheckSupportsWindows8Features("JetResizeDatabase");
			JetApi.CheckNotNegative(desiredPages, "desiredPages");
			uint num = 0U;
			checked
			{
				int result = JetApi.Err(NativeMethods.JetResizeDatabase(sesid.Value, dbid.Value, (uint)desiredPages, out num, (uint)grbit));
				actualPages = (int)num;
				return result;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007ED8 File Offset: 0x000060D8
		public int JetCreateIndex4(JET_SESID sesid, JET_TABLEID tableid, JET_INDEXCREATE[] indexcreates, int numIndexCreates)
		{
			this.CheckSupportsWindows8Features("JetCreateIndex4");
			JetApi.CheckNotNull(indexcreates, "indexcreates");
			JetApi.CheckNotNegative(numIndexCreates, "numIndexCreates");
			if (numIndexCreates > indexcreates.Length)
			{
				throw new ArgumentOutOfRangeException("numIndexCreates", numIndexCreates, "numIndexCreates is larger than the number of indexes passed in");
			}
			return JetApi.CreateIndexes3(sesid, tableid, indexcreates, numIndexCreates);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007F30 File Offset: 0x00006130
		public unsafe int JetOpenTemporaryTable2(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable)
		{
			this.CheckSupportsWindows8Features("JetOpenTemporaryTable2");
			JetApi.CheckNotNull(temporarytable, "temporarytable");
			NATIVE_OPENTEMPORARYTABLE2 nativeOpenTemporaryTable = temporarytable.GetNativeOpenTemporaryTable2();
			uint[] array = new uint[nativeOpenTemporaryTable.ccolumn];
			NATIVE_COLUMNDEF[] nativecolumndefs = JetApi.GetNativecolumndefs(temporarytable.prgcolumndef, temporarytable.ccolumn);
			int result;
			using (GCHandleCollection gchandleCollection = default(GCHandleCollection))
			{
				nativeOpenTemporaryTable.prgcolumndef = (NATIVE_COLUMNDEF*)((void*)gchandleCollection.Add(nativecolumndefs));
				nativeOpenTemporaryTable.rgcolumnid = (uint*)((void*)gchandleCollection.Add(array));
				if (temporarytable.pidxunicode != null)
				{
					NATIVE_UNICODEINDEX2 nativeUnicodeIndex = temporarytable.pidxunicode.GetNativeUnicodeIndex2();
					nativeUnicodeIndex.szLocaleName = gchandleCollection.Add(Util.ConvertToNullTerminatedUnicodeByteArray(temporarytable.pidxunicode.GetEffectiveLocaleName()));
					nativeOpenTemporaryTable.pidxunicode = (NATIVE_UNICODEINDEX2*)((void*)gchandleCollection.Add(nativeUnicodeIndex));
				}
				int num = JetApi.Err(NativeMethods.JetOpenTemporaryTable2(sesid.Value, ref nativeOpenTemporaryTable));
				JetApi.SetColumnids(temporarytable.prgcolumndef, temporarytable.prgcolumnid, array, temporarytable.ccolumn);
				temporarytable.tableid = new JET_TABLEID
				{
					Value = nativeOpenTemporaryTable.tableid
				};
				result = num;
			}
			return result;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008068 File Offset: 0x00006268
		public int JetCreateTableColumnIndex4(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate)
		{
			this.CheckSupportsWindows8Features("JetCreateTableColumnIndex4");
			JetApi.CheckNotNull(tablecreate, "tablecreate");
			return JetApi.CreateTableColumnIndex4(sesid, dbid, tablecreate);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008088 File Offset: 0x00006288
		public unsafe int JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, byte[] data, int dataSize)
		{
			this.CheckSupportsWindows8Features("JetSetSessionParameter");
			JetApi.CheckNotNegative(dataSize, "dataSize");
			JetApi.CheckDataSize<byte>(data, dataSize, "dataSize");
			int err;
			fixed (byte* ptr = data)
			{
				IntPtr data2 = (IntPtr)((void*)ptr);
				err = NativeMethods.JetSetSessionParameter(sesid.Value, (uint)sesparamid, data2, dataSize);
			}
			return JetApi.Err(err);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000080F4 File Offset: 0x000062F4
		public int JetCommitTransaction2(JET_SESID sesid, CommitTransactionGrbit grbit, TimeSpan durableCommit, out JET_COMMIT_ID commitId)
		{
			this.CheckSupportsWindows8Features("JetCommitTransaction2");
			uint cmsecDurableCommit = (uint)durableCommit.TotalMilliseconds;
			NATIVE_COMMIT_ID native = default(NATIVE_COMMIT_ID);
			int result = JetApi.Err(NativeMethods.JetCommitTransaction2(sesid.Value, (uint)grbit, cmsecDurableCommit, ref native));
			commitId = new JET_COMMIT_ID(native);
			return result;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008140 File Offset: 0x00006340
		public int JetPrereadIndexRanges(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_RANGE[] indexRanges, int rangeIndex, int rangeCount, out int rangesPreread, JET_COLUMNID[] columnsPreread, PrereadIndexRangesGrbit grbit)
		{
			this.CheckSupportsWindows8Features("JetPrereadIndexRanges");
			JetApi.CheckNotNull(indexRanges, "indexRanges");
			JetApi.CheckDataSize<JET_INDEX_RANGE>(indexRanges, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				NATIVE_INDEX_RANGE[] array = new NATIVE_INDEX_RANGE[rangeCount];
				for (int i = 0; i < rangeCount; i++)
				{
					array[i] = indexRanges[i + rangeIndex].GetNativeIndexRange(ref gchandleCollection);
				}
				if (columnsPreread != null)
				{
					uint[] array2 = new uint[columnsPreread.Length];
					for (int j = 0; j < columnsPreread.Length; j++)
					{
						array2[j] = columnsPreread[j].Value;
					}
					result = JetApi.Err(NativeMethods.JetPrereadIndexRanges(sesid.Value, tableid.Value, array, (uint)rangeCount, out rangesPreread, array2, (uint)columnsPreread.Length, checked((uint)grbit)));
				}
				else
				{
					result = JetApi.Err(NativeMethods.JetPrereadIndexRanges(sesid.Value, tableid.Value, array, (uint)rangeCount, out rangesPreread, null, 0U, checked((uint)grbit)));
				}
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000824C File Offset: 0x0000644C
		public int JetPrereadKeyRanges(JET_SESID sesid, JET_TABLEID tableid, byte[][] keysStart, int[] keyStartLengths, byte[][] keysEnd, int[] keyEndLengths, int rangeIndex, int rangeCount, out int rangesPreread, JET_COLUMNID[] columnsPreread, PrereadIndexRangesGrbit grbit)
		{
			this.CheckSupportsWindows8Features("JetPrereadKeyRanges");
			JetApi.CheckDataSize<byte[]>(keysStart, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
			JetApi.CheckDataSize<int>(keyStartLengths, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
			JetApi.CheckNotNull(keysStart, "keysStart");
			if (keysEnd != null)
			{
				JetApi.CheckNotNull(keyEndLengths, "keyEndLengths");
				JetApi.CheckDataSize<byte[]>(keysEnd, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
			}
			if (keyEndLengths != null)
			{
				JetApi.CheckNotNull(keysEnd, "keysEnd");
				JetApi.CheckDataSize<int>(keyEndLengths, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
			}
			grbit |= PrereadIndexRangesGrbit.NormalizedKey;
			int result;
			using (GCHandleCollection gchandleCollection = default(GCHandleCollection))
			{
				NATIVE_INDEX_RANGE[] array = new NATIVE_INDEX_RANGE[rangeCount];
				for (int i = 0; i < rangeCount; i++)
				{
					NATIVE_INDEX_COLUMN[] array2 = new NATIVE_INDEX_COLUMN[1];
					array2[0].pvData = gchandleCollection.Add(keysStart[i + rangeIndex]);
					array2[0].cbData = (uint)keyStartLengths[i + rangeIndex];
					array[i].rgStartColumns = gchandleCollection.Add(array2);
					array[i].cStartColumns = 1U;
					if (keysEnd != null)
					{
						NATIVE_INDEX_COLUMN[] array3 = new NATIVE_INDEX_COLUMN[1];
						array3[0].pvData = gchandleCollection.Add(keysEnd[i + rangeIndex]);
						array3[0].cbData = (uint)keyEndLengths[i + rangeIndex];
						array[i].rgEndColumns = gchandleCollection.Add(array3);
						array[i].cEndColumns = 1U;
					}
				}
				if (columnsPreread != null)
				{
					uint[] array4 = new uint[columnsPreread.Length];
					for (int j = 0; j < columnsPreread.Length; j++)
					{
						array4[j] = columnsPreread[j].Value;
					}
					result = JetApi.Err(NativeMethods.JetPrereadIndexRanges(sesid.Value, tableid.Value, array, (uint)rangeCount, out rangesPreread, array4, (uint)columnsPreread.Length, checked((uint)grbit)));
				}
				else
				{
					result = JetApi.Err(NativeMethods.JetPrereadIndexRanges(sesid.Value, tableid.Value, array, (uint)rangeCount, out rangesPreread, null, 0U, checked((uint)grbit)));
				}
			}
			return result;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008478 File Offset: 0x00006678
		public int JetSetCursorFilter(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_COLUMN[] filters, CursorFilterGrbit grbit)
		{
			this.CheckSupportsWindows8Features("JetSetCursorFilter");
			if (filters == null || filters.Length == 0)
			{
				return JetApi.Err(NativeMethods.JetSetCursorFilter(sesid.Value, tableid.Value, null, 0U, checked((uint)grbit)));
			}
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				NATIVE_INDEX_COLUMN[] array = new NATIVE_INDEX_COLUMN[filters.Length];
				for (int i = 0; i < filters.Length; i++)
				{
					array[i] = filters[i].GetNativeIndexColumn(ref gchandleCollection);
				}
				result = JetApi.Err(NativeMethods.JetSetCursorFilter(sesid.Value, tableid.Value, array, (uint)filters.Length, checked((uint)grbit)));
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008528 File Offset: 0x00006728
		private unsafe static NATIVE_INDEXCREATE3[] GetNativeIndexCreate3s(IList<JET_INDEXCREATE> managedIndexCreates, ref GCHandleCollection handles)
		{
			NATIVE_INDEXCREATE3[] array = null;
			if (managedIndexCreates != null && managedIndexCreates.Count > 0)
			{
				array = new NATIVE_INDEXCREATE3[managedIndexCreates.Count];
				for (int i = 0; i < managedIndexCreates.Count; i++)
				{
					array[i] = managedIndexCreates[i].GetNativeIndexcreate3();
					if (managedIndexCreates[i].pidxUnicode != null)
					{
						NATIVE_UNICODEINDEX2 nativeUnicodeIndex = managedIndexCreates[i].pidxUnicode.GetNativeUnicodeIndex2();
						nativeUnicodeIndex.szLocaleName = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].pidxUnicode.GetEffectiveLocaleName()));
						array[i].pidxUnicode = (NATIVE_UNICODEINDEX2*)((void*)handles.Add(nativeUnicodeIndex));
						NATIVE_INDEXCREATE3[] array2 = array;
						int num = i;
						array2[num].grbit = (array2[num].grbit | 2048U);
					}
					array[i].szKey = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].szKey));
					array[i].szIndexName = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].szIndexName));
					array[i].rgconditionalcolumn = JetApi.GetNativeConditionalColumns(managedIndexCreates[i].rgconditionalcolumn, true, ref handles);
					if (managedIndexCreates[i].pSpaceHints != null)
					{
						NATIVE_SPACEHINTS nativeSpaceHints = managedIndexCreates[i].pSpaceHints.GetNativeSpaceHints();
						array[i].pSpaceHints = handles.Add(nativeSpaceHints);
					}
				}
			}
			return array;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000869C File Offset: 0x0000689C
		private static int CreateIndexes3(JET_SESID sesid, JET_TABLEID tableid, IList<JET_INDEXCREATE> indexcreates, int numIndexCreates)
		{
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				NATIVE_INDEXCREATE3[] nativeIndexCreate3s = JetApi.GetNativeIndexCreate3s(indexcreates, ref gchandleCollection);
				result = JetApi.Err(NativeMethods.JetCreateIndex4W(sesid.Value, tableid.Value, nativeIndexCreate3s, checked((uint)numIndexCreates)));
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000086F4 File Offset: 0x000068F4
		private unsafe static int CreateTableColumnIndex4(JET_SESID sesid, JET_DBID dbid, JET_TABLECREATE tablecreate)
		{
			NATIVE_TABLECREATE4 nativeTableCreate = tablecreate.GetNativeTableCreate4();
			GCHandleCollection gchandleCollection = default(GCHandleCollection);
			int result;
			try
			{
				nativeTableCreate.rgcolumncreate = (NATIVE_COLUMNCREATE*)((void*)JetApi.GetNativeColumnCreates(tablecreate.rgcolumncreate, true, ref gchandleCollection));
				NATIVE_INDEXCREATE3[] nativeIndexCreate3s = JetApi.GetNativeIndexCreate3s(tablecreate.rgindexcreate, ref gchandleCollection);
				nativeTableCreate.rgindexcreate = gchandleCollection.Add(nativeIndexCreate3s);
				if (tablecreate.pSeqSpacehints != null)
				{
					NATIVE_SPACEHINTS nativeSpaceHints = tablecreate.pSeqSpacehints.GetNativeSpaceHints();
					nativeTableCreate.pSeqSpacehints = (NATIVE_SPACEHINTS*)((void*)gchandleCollection.Add(nativeSpaceHints));
				}
				if (tablecreate.pLVSpacehints != null)
				{
					NATIVE_SPACEHINTS nativeSpaceHints2 = tablecreate.pLVSpacehints.GetNativeSpaceHints();
					nativeTableCreate.pLVSpacehints = (NATIVE_SPACEHINTS*)((void*)gchandleCollection.Add(nativeSpaceHints2));
				}
				int err = NativeMethods.JetCreateTableColumnIndex4W(sesid.Value, dbid.Value, ref nativeTableCreate);
				tablecreate.tableid = new JET_TABLEID
				{
					Value = nativeTableCreate.tableid
				};
				tablecreate.cCreated = checked((int)nativeTableCreate.cCreated);
				if (tablecreate.rgcolumncreate != null)
				{
					for (int i = 0; i < tablecreate.rgcolumncreate.Length; i++)
					{
						tablecreate.rgcolumncreate[i].SetFromNativeColumnCreate(nativeTableCreate.rgcolumncreate[i]);
					}
				}
				if (tablecreate.rgindexcreate != null)
				{
					for (int j = 0; j < tablecreate.rgindexcreate.Length; j++)
					{
						tablecreate.rgindexcreate[j].SetFromNativeIndexCreate(nativeIndexCreate3s[j]);
					}
				}
				result = JetApi.Err(err);
			}
			finally
			{
				gchandleCollection.Dispose();
			}
			return result;
		}

		// Token: 0x04000068 RID: 104
		private static JET_PFNTRACEEMIT traceCallback;

		// Token: 0x04000069 RID: 105
		private static readonly TraceSwitch TraceSwitch = new TraceSwitch("ESENT P/Invoke", "P/Invoke calls to ESENT");

		// Token: 0x0400006A RID: 106
		private readonly uint versionOverride;

		// Token: 0x0400006B RID: 107
		private readonly CallbackWrappers callbackWrappers = new CallbackWrappers();
	}
}
