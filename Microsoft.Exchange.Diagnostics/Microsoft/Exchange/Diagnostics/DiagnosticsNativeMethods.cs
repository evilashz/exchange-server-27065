using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000043 RID: 67
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal static class DiagnosticsNativeMethods
	{
		// Token: 0x06000181 RID: 385
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern int GetCurrentThreadId();

		// Token: 0x06000182 RID: 386
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern int GetCurrentProcessId();

		// Token: 0x06000183 RID: 387
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "StartTraceW")]
		internal static extern uint StartTrace(out long sessionHandle, [In] string sessionName, [In] [Out] ref DiagnosticsNativeMethods.EventTraceProperties properties);

		// Token: 0x06000184 RID: 388
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "FlushTraceW")]
		internal static extern uint FlushTrace([In] long sessionHandle, [In] string sessionName, [In] [Out] ref DiagnosticsNativeMethods.EventTraceProperties properties);

		// Token: 0x06000185 RID: 389
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "ControlTraceW")]
		internal static extern uint ControlTrace([In] long sessionHandle, [In] string sessionName, [In] [Out] ref DiagnosticsNativeMethods.EventTraceProperties properties, [In] uint controlCode);

		// Token: 0x06000186 RID: 390
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal unsafe static extern uint TraceMessage([In] long sessionHandle, [In] uint messageFlags, [In] ref Guid messageGuid, [In] ushort messageNumber, [In] byte* buffer, [In] int bufferLength, [In] IntPtr terminationPtr, [In] int terminationSize);

		// Token: 0x06000187 RID: 391
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern uint TraceMessage([In] long sessionHandle, [In] uint messageFlags, [In] ref Guid messageGuid, [In] ushort messageNumber, [In] ref int traceTag, [In] int sizeOfTraceTag, [In] string message, [In] int messageLengthInBytes, [In] ref long userId, [In] int sizeOfuserId, [In] ref int locationId, [In] int sizeOflocationId, [In] IntPtr terminationPtr, [In] int terminationSize);

		// Token: 0x06000188 RID: 392
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern uint TraceMessage([In] long sessionHandle, [In] uint messageFlags, [In] ref Guid messageGuid, [In] ushort messageNumber, [In] ref int startStop, [In] int sizeOfStartStop, [In] byte[] clientRequestID, [In] int sizeOfClientRequestID, [In] byte[] serviceProviderRequestID, [In] int sizeOfServiceProviderRequestID, [In] ref int bytesIn, [In] int sizeOfBytesIn, [In] ref int bytesOut, [In] int sizeOfBytesOut, [In] string serverAddress, [In] int sizeOfServerAddress, [In] string userContext, [In] int sizeOfUserContext, [In] string SpOp, [In] int sizeOfSpOp, [In] string SpOpData, [In] int sizeOfSpOpData, [In] string ClientOp, [In] int sizeOfClientOp, [In] IntPtr terminationPtr, [In] int terminationSize);

		// Token: 0x06000189 RID: 393
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint EnableTrace([In] uint enable, [In] uint enableFlag, [In] uint enableLevel, [In] ref Guid controlGuid, [In] long sessionHandle);

		// Token: 0x0600018A RID: 394
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x0600018B RID: 395
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		public static extern DiagnosticsNativeMethods.ErrorCode RegOpenKeyEx(SafeRegistryHandle parent, string subKey, int options, int samDesired, out SafeRegistryHandle key);

		// Token: 0x0600018C RID: 396
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		public static extern DiagnosticsNativeMethods.ErrorCode RegNotifyChangeKeyValue([In] SafeRegistryHandle key, [MarshalAs(UnmanagedType.Bool)] [In] bool watchSubtree, [In] DiagnosticsNativeMethods.RegistryNotifications notifyFilter, [In] SafeWaitHandle notifyEvent, [MarshalAs(UnmanagedType.Bool)] [In] bool asynchronous);

		// Token: 0x0600018D RID: 397
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll")]
		public static extern DiagnosticsNativeMethods.ErrorCode RegCloseKey([In] IntPtr key);

		// Token: 0x0600018E RID: 398
		[DllImport("advapi32.dll")]
		public static extern uint CreateTraceInstanceId([In] IntPtr registrationHandle, [In] [Out] ref DiagnosticsNativeMethods.EventInstanceInfo eventInstanceInfo);

		// Token: 0x0600018F RID: 399
		[DllImport("advapi32.dll")]
		public static extern uint TraceEventInstance([In] long sessionHandle, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] eventTrace, [In] ref DiagnosticsNativeMethods.EventInstanceInfo eventInstanceInfo, [In] ref DiagnosticsNativeMethods.EventInstanceInfo parentEventInstanceInfo);

		// Token: 0x06000190 RID: 400
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("Kernel32.dll", SetLastError = true)]
		internal static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFileName, int nSize);

		// Token: 0x06000191 RID: 401
		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FlushFileBuffers(SafeFileHandle handle);

		// Token: 0x06000192 RID: 402
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetShortPathNameW", SetLastError = true)]
		internal static extern int GetShortPathName([In] string longPath, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] char[] shortPath, [In] int bufferSize);

		// Token: 0x06000193 RID: 403
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern ProcSafeHandle OpenProcess(DiagnosticsNativeMethods.ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

		// Token: 0x06000194 RID: 404
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern ThreadSafeHandle OpenThread(DiagnosticsNativeMethods.ThreadAccess desiredAccess, [MarshalAs(UnmanagedType.Bool)] bool inheritHandle, int threadId);

		// Token: 0x06000195 RID: 405
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int SuspendThread(ThreadSafeHandle thread);

		// Token: 0x06000196 RID: 406
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int ResumeThread(ThreadSafeHandle thread);

		// Token: 0x06000197 RID: 407
		[DllImport("wer.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		internal static extern WerSafeHandle WerReportCreate([In] string eventType, [In] DiagnosticsNativeMethods.WER_REPORT_TYPE repType, [In] [Optional] DiagnosticsNativeMethods.WER_REPORT_INFORMATION reportInformation);

		// Token: 0x06000198 RID: 408
		[DllImport("wer.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		internal static extern void WerReportSetParameter([In] WerSafeHandle reportHandle, [In] uint paramID, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string value);

		// Token: 0x06000199 RID: 409
		[DllImport("wer.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		internal static extern void WerReportAddFile([In] WerSafeHandle reportHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string path, [In] DiagnosticsNativeMethods.WER_FILE_TYPE fileType, [In] DiagnosticsNativeMethods.WER_FILE_FLAGS fileFlags);

		// Token: 0x0600019A RID: 410
		[DllImport("wer.dll", PreserveSig = false)]
		internal static extern void WerReportAddDump([In] WerSafeHandle reportHandle, [In] ProcSafeHandle processHandle, [In] [Optional] IntPtr threadHandle, [In] DiagnosticsNativeMethods.WER_DUMP_TYPE dumpType, [In] [Optional] DiagnosticsNativeMethods.WER_EXCEPTION_INFORMATION exceptionParam, [In] [Optional] DiagnosticsNativeMethods.WER_DUMP_CUSTOM_OPTIONS dumpCustomOptions, [In] uint flags);

		// Token: 0x0600019B RID: 411
		[DllImport("kernel32.dll", PreserveSig = false)]
		internal static extern void WerSetFlags(DiagnosticsNativeMethods.WER_FLAGS flags);

		// Token: 0x0600019C RID: 412
		[DllImport("wer.dll", PreserveSig = false)]
		internal static extern int WerReportSubmit([In] WerSafeHandle reportHandle, [In] DiagnosticsNativeMethods.WER_CONSENT consent, [In] DiagnosticsNativeMethods.WER_SUBMIT_FLAGS flags, IntPtr submitResult);

		// Token: 0x0600019D RID: 413
		[DllImport("ExWatson.dll", PreserveSig = false)]
		internal static extern int SubmitExWatsonReport([In] WerSafeHandle reportHandle, [In] DiagnosticsNativeMethods.WER_CONSENT consent, [In] DiagnosticsNativeMethods.WER_SUBMIT_FLAGS flags, IntPtr submitResult, [In] bool fDumpRequested, [In] bool fProcessTerminating);

		// Token: 0x0600019E RID: 414
		[DllImport("wer.dll", PreserveSig = false)]
		internal static extern void WerReportCloseHandle([In] IntPtr reportHandle);

		// Token: 0x0600019F RID: 415
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern int TerminateProcess(IntPtr hProcess, int exitCode);

		// Token: 0x060001A0 RID: 416
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern int ExitProcess(int exitCode);

		// Token: 0x060001A1 RID: 417
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint TraceEvent([In] long sessionHandle, [In] ref DiagnosticsNativeMethods.BinaryEventTrace binaryEventTrace);

		// Token: 0x040000FD RID: 253
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x040000FE RID: 254
		private const int KEY_QUERY_VALUE = 1;

		// Token: 0x040000FF RID: 255
		private const int KEY_ENUMERATE_SUB_KEYS = 8;

		// Token: 0x04000100 RID: 256
		private const int KEY_NOTIFY = 16;

		// Token: 0x04000101 RID: 257
		private const int STANDARD_RIGHTS_READ = 131072;

		// Token: 0x04000102 RID: 258
		private const int SYNCHRONIZE = 1048576;

		// Token: 0x04000103 RID: 259
		public const int KEY_READ = 131097;

		// Token: 0x04000104 RID: 260
		private const int MAX_PATH = 260;

		// Token: 0x04000105 RID: 261
		internal const uint WnodeFlagTracedGuid = 131072U;

		// Token: 0x04000106 RID: 262
		internal const uint WnodeFlagUseMofPtr = 1048576U;

		// Token: 0x04000107 RID: 263
		internal const uint EVENT_TRACE_CONTROL_QUERY = 0U;

		// Token: 0x04000108 RID: 264
		internal const uint EVENT_TRACE_CONTROL_STOP = 1U;

		// Token: 0x04000109 RID: 265
		internal const uint EVENT_TRACE_CONTROL_UPDATE = 2U;

		// Token: 0x0400010A RID: 266
		internal const uint EVENT_TRACE_CONTROL_FLUSH = 3U;

		// Token: 0x0400010B RID: 267
		internal const int WMI_ENABLE_EVENTS = 4;

		// Token: 0x0400010C RID: 268
		internal const int WMI_DISABLE_EVENTS = 5;

		// Token: 0x0400010D RID: 269
		internal const uint TRACE_MESSAGE_SEQUENCE = 1U;

		// Token: 0x0400010E RID: 270
		internal const uint TRACE_MESSAGE_GUID = 2U;

		// Token: 0x0400010F RID: 271
		internal const uint TRACE_MESSAGE_TIMESTAMP = 8U;

		// Token: 0x04000110 RID: 272
		internal const uint TRACE_MESSAGE_SYSTEMINFO = 32U;

		// Token: 0x04000111 RID: 273
		internal const int TRACE_MESSAGE_MAXIMUM_SIZE = 8192;

		// Token: 0x04000112 RID: 274
		internal const uint TRACE_DISABLE = 0U;

		// Token: 0x04000113 RID: 275
		internal const uint TRACE_ENABLE = 1U;

		// Token: 0x04000114 RID: 276
		internal const uint TRACE_LEVEL_CRITICAL = 1U;

		// Token: 0x04000115 RID: 277
		internal const uint TRACE_LEVEL_ERROR = 2U;

		// Token: 0x04000116 RID: 278
		internal const uint TRACE_LEVEL_WARNING = 3U;

		// Token: 0x04000117 RID: 279
		internal const uint TRACE_LEVEL_INFORMATION = 4U;

		// Token: 0x04000118 RID: 280
		internal const uint TRACE_LEVEL_VERBOSE = 5U;

		// Token: 0x04000119 RID: 281
		internal const int MAXIMUM_SUSPEND_COUNT = 127;

		// Token: 0x0400011A RID: 282
		private static readonly ushort BinaryEventTraceSize = (ushort)(Marshal.SizeOf(typeof(DiagnosticsNativeMethods.EventTraceHeader)) + Marshal.SizeOf(typeof(DiagnosticsNativeMethods.MofField)));

		// Token: 0x02000044 RID: 68
		public enum ErrorCode
		{
			// Token: 0x0400011C RID: 284
			Success,
			// Token: 0x0400011D RID: 285
			FileNotFound = 2,
			// Token: 0x0400011E RID: 286
			InvalidHandle = 6,
			// Token: 0x0400011F RID: 287
			NotEnoughMemory = 8,
			// Token: 0x04000120 RID: 288
			OutOfMemory = 14,
			// Token: 0x04000121 RID: 289
			DiskFull = 112,
			// Token: 0x04000122 RID: 290
			KeyDeleted = 1018,
			// Token: 0x04000123 RID: 291
			NoMoreItems = 259,
			// Token: 0x04000124 RID: 292
			MoreData = 234
		}

		// Token: 0x02000045 RID: 69
		internal enum ExceptionFlag
		{
			// Token: 0x04000126 RID: 294
			Continuable,
			// Token: 0x04000127 RID: 295
			NonContinuable = -1073741787
		}

		// Token: 0x02000046 RID: 70
		[Flags]
		public enum LogFileMode : uint
		{
			// Token: 0x04000129 RID: 297
			EVENT_TRACE_FILE_MODE_NONE = 0U,
			// Token: 0x0400012A RID: 298
			EVENT_TRACE_FILE_MODE_SEQUENTIAL = 1U,
			// Token: 0x0400012B RID: 299
			EVENT_TRACE_FILE_MODE_CIRCULAR = 2U,
			// Token: 0x0400012C RID: 300
			EVENT_TRACE_FILE_MODE_APPEND = 4U,
			// Token: 0x0400012D RID: 301
			EVENT_TRACE_FILE_MODE_NEWFILE = 8U,
			// Token: 0x0400012E RID: 302
			EVENT_TRACE_FILE_MODE_PREALLOCATE = 32U,
			// Token: 0x0400012F RID: 303
			EVENT_TRACE_SECURE_MODE = 128U,
			// Token: 0x04000130 RID: 304
			EVENT_TRACE_REAL_TIME_MODE = 256U,
			// Token: 0x04000131 RID: 305
			EVENT_TRACE_BUFFERING_MODE = 1024U,
			// Token: 0x04000132 RID: 306
			EVENT_TRACE_PRIVATE_LOGGER_MODE = 2048U,
			// Token: 0x04000133 RID: 307
			EVENT_TRACE_USE_KBYTES_FOR_SIZE = 8192U,
			// Token: 0x04000134 RID: 308
			EVENT_TRACE_USE_GLOBAL_SEQUENCE = 16384U,
			// Token: 0x04000135 RID: 309
			EVENT_TRACE_USE_LOCAL_SEQUENCE = 32768U,
			// Token: 0x04000136 RID: 310
			EVENT_TRACE_RELOG_MODE = 65536U,
			// Token: 0x04000137 RID: 311
			EVENT_TRACE_PRIVATE_IN_PROC = 131072U,
			// Token: 0x04000138 RID: 312
			EVENT_TRACE_USE_PAGED_MEMORY = 16777216U
		}

		// Token: 0x02000047 RID: 71
		[Flags]
		internal enum ProcessAccess
		{
			// Token: 0x0400013A RID: 314
			Terminate = 1,
			// Token: 0x0400013B RID: 315
			CreateThread = 2,
			// Token: 0x0400013C RID: 316
			SetSessionId = 4,
			// Token: 0x0400013D RID: 317
			VmOperation = 8,
			// Token: 0x0400013E RID: 318
			VmRead = 16,
			// Token: 0x0400013F RID: 319
			VmWrite = 32,
			// Token: 0x04000140 RID: 320
			DupHandle = 64,
			// Token: 0x04000141 RID: 321
			CreateProcess = 128,
			// Token: 0x04000142 RID: 322
			SetQuota = 256,
			// Token: 0x04000143 RID: 323
			SetInformation = 512,
			// Token: 0x04000144 RID: 324
			QueryInformation = 1024,
			// Token: 0x04000145 RID: 325
			SuspendResume = 2048,
			// Token: 0x04000146 RID: 326
			StandardRightsRead = 131072,
			// Token: 0x04000147 RID: 327
			StandardRightsRequired = 983040,
			// Token: 0x04000148 RID: 328
			Synchronize = 1048576,
			// Token: 0x04000149 RID: 329
			AllAccess = 2035711
		}

		// Token: 0x02000048 RID: 72
		[Flags]
		internal enum ThreadAccess
		{
			// Token: 0x0400014B RID: 331
			Synchronize = 1048576,
			// Token: 0x0400014C RID: 332
			Terminate = 1,
			// Token: 0x0400014D RID: 333
			SuspendResume = 2,
			// Token: 0x0400014E RID: 334
			GetContext = 8,
			// Token: 0x0400014F RID: 335
			SetContext = 16,
			// Token: 0x04000150 RID: 336
			SetInformation = 32,
			// Token: 0x04000151 RID: 337
			QueryInformation = 64,
			// Token: 0x04000152 RID: 338
			SetThreadToken = 128,
			// Token: 0x04000153 RID: 339
			Impersonate = 256,
			// Token: 0x04000154 RID: 340
			DirectImpersonation = 512,
			// Token: 0x04000155 RID: 341
			SetLimitedInformation = 1024,
			// Token: 0x04000156 RID: 342
			QueryLimitedInformation = 2048,
			// Token: 0x04000157 RID: 343
			StandardRightsRequired = 983040,
			// Token: 0x04000158 RID: 344
			AllAccess = 2097151
		}

		// Token: 0x02000049 RID: 73
		internal enum WER_FLAGS
		{
			// Token: 0x0400015A RID: 346
			WER_FAULT_REPORTING_FLAG_NOHEAP = 1,
			// Token: 0x0400015B RID: 347
			WER_FAULT_REPORTING_FLAG_QUEUE,
			// Token: 0x0400015C RID: 348
			WER_FAULT_REPORTING_FLAG_DISABLE_THREAD_SUSPENSION = 4,
			// Token: 0x0400015D RID: 349
			WER_FAULT_REPORTING_FLAG_QUEUE_UPLOAD = 8
		}

		// Token: 0x0200004A RID: 74
		internal enum WER_REPORT_TYPE
		{
			// Token: 0x0400015F RID: 351
			WerReportNonCritical,
			// Token: 0x04000160 RID: 352
			WerReportCritical,
			// Token: 0x04000161 RID: 353
			WerReportApplicationCrash,
			// Token: 0x04000162 RID: 354
			WerReportApplicationHang,
			// Token: 0x04000163 RID: 355
			WerReportKernel,
			// Token: 0x04000164 RID: 356
			WerReportInvalid
		}

		// Token: 0x0200004B RID: 75
		[Flags]
		internal enum WER_FILE_FLAGS : uint
		{
			// Token: 0x04000166 RID: 358
			WER_FILE_DELETE_WHEN_DONE = 1U,
			// Token: 0x04000167 RID: 359
			WER_FILE_ANONYMOUS_DATA = 2U
		}

		// Token: 0x0200004C RID: 76
		[Flags]
		internal enum WER_DUMP_FLAGS
		{
			// Token: 0x04000169 RID: 361
			WER_DUMP_MASK_DUMPTYPE = 1,
			// Token: 0x0400016A RID: 362
			WER_DUMP_MASK_ONLY_THISTHREAD = 2,
			// Token: 0x0400016B RID: 363
			WER_DUMP_MASK_THREADFLAGS = 4,
			// Token: 0x0400016C RID: 364
			WER_DUMP_MASK_THREADFLAGS_EX = 8,
			// Token: 0x0400016D RID: 365
			WER_DUMP_MASK_OTHERTHREADFLAGS = 16,
			// Token: 0x0400016E RID: 366
			WER_DUMP_MASK_OTHERTHREADFLAGS_EX = 32,
			// Token: 0x0400016F RID: 367
			WER_DUMP_MASK_PREFERRED_MODULESFLAGS = 64,
			// Token: 0x04000170 RID: 368
			WER_DUMP_MASK_OTHER_MODULESFLAGS = 128,
			// Token: 0x04000171 RID: 369
			WER_DUMP_MASK_PREFERRED_MODULE_LIST = 256
		}

		// Token: 0x0200004D RID: 77
		[Flags]
		internal enum MINIDUMP_TYPE
		{
			// Token: 0x04000173 RID: 371
			MiniDumpNormal = 0,
			// Token: 0x04000174 RID: 372
			MiniDumpWithDataSegs = 1,
			// Token: 0x04000175 RID: 373
			MiniDumpWithFullMemory = 2,
			// Token: 0x04000176 RID: 374
			MiniDumpWithHandleData = 4,
			// Token: 0x04000177 RID: 375
			MiniDumpFilterMemory = 8,
			// Token: 0x04000178 RID: 376
			MiniDumpScanMemory = 16,
			// Token: 0x04000179 RID: 377
			MiniDumpWithUnloadedModules = 32,
			// Token: 0x0400017A RID: 378
			MiniDumpWithIndirectlyReferencedMemory = 64,
			// Token: 0x0400017B RID: 379
			MiniDumpFilterModulePaths = 128,
			// Token: 0x0400017C RID: 380
			MiniDumpWithProcessThreadData = 256,
			// Token: 0x0400017D RID: 381
			MiniDumpWithPrivateReadWriteMemory = 512,
			// Token: 0x0400017E RID: 382
			MiniDumpWithoutOptionalData = 1024,
			// Token: 0x0400017F RID: 383
			MiniDumpWithFullMemoryInfo = 2048,
			// Token: 0x04000180 RID: 384
			MiniDumpWithThreadInfo = 4096
		}

		// Token: 0x0200004E RID: 78
		internal enum WER_CONSENT
		{
			// Token: 0x04000182 RID: 386
			WerConsentNotAsked = 1,
			// Token: 0x04000183 RID: 387
			WerConsentApproved,
			// Token: 0x04000184 RID: 388
			WerConsentDenied
		}

		// Token: 0x0200004F RID: 79
		[Flags]
		internal enum WER_SUBMIT_FLAGS
		{
			// Token: 0x04000186 RID: 390
			WER_SUBMIT_HONOR_RECOVERY = 1,
			// Token: 0x04000187 RID: 391
			WER_SUBMIT_HONOR_RESTART = 2,
			// Token: 0x04000188 RID: 392
			WER_SUBMIT_QUEUE = 4,
			// Token: 0x04000189 RID: 393
			WER_SUBMIT_SHOW_DEBUG = 8,
			// Token: 0x0400018A RID: 394
			WER_SUBMIT_ADD_REGISTERED_DATA = 16,
			// Token: 0x0400018B RID: 395
			WER_SUBMIT_OUTOFPROCESS = 32,
			// Token: 0x0400018C RID: 396
			WER_SUBMIT_NO_CLOSE_UI = 64,
			// Token: 0x0400018D RID: 397
			WER_SUBMIT_NO_QUEUE = 128,
			// Token: 0x0400018E RID: 398
			WER_SUBMIT_NO_ARCHIVE = 256,
			// Token: 0x0400018F RID: 399
			WER_SUBMIT_START_MINIMIZED = 512,
			// Token: 0x04000190 RID: 400
			WER_SUBMIT_OUTOFPROCESS_ASYNC = 1024,
			// Token: 0x04000191 RID: 401
			WER_SUBMIT_BYPASS_DATA_THROTTLING = 2048
		}

		// Token: 0x02000050 RID: 80
		internal enum WER_FILE_TYPE
		{
			// Token: 0x04000193 RID: 403
			WerFileTypeMicrodump = 1,
			// Token: 0x04000194 RID: 404
			WerFileTypeMinidump,
			// Token: 0x04000195 RID: 405
			WerFileTypeHeapdump,
			// Token: 0x04000196 RID: 406
			WerFileTypeUserDocument,
			// Token: 0x04000197 RID: 407
			WerFileTypeOther
		}

		// Token: 0x02000051 RID: 81
		public enum WER_SUBMIT_RESULT
		{
			// Token: 0x04000199 RID: 409
			WerReportQueued = 1,
			// Token: 0x0400019A RID: 410
			WerReportUploaded,
			// Token: 0x0400019B RID: 411
			WerReportDebug,
			// Token: 0x0400019C RID: 412
			WerReportFailed,
			// Token: 0x0400019D RID: 413
			WerDisabled,
			// Token: 0x0400019E RID: 414
			WerReportCancelled,
			// Token: 0x0400019F RID: 415
			WerDisabledQueue,
			// Token: 0x040001A0 RID: 416
			WerReportAsync
		}

		// Token: 0x02000052 RID: 82
		public enum WER_DUMP_TYPE
		{
			// Token: 0x040001A2 RID: 418
			WerDumpTypeMicroDump = 1,
			// Token: 0x040001A3 RID: 419
			WerDumpTypeMiniDump,
			// Token: 0x040001A4 RID: 420
			WerDumpTypeHeapDump
		}

		// Token: 0x02000053 RID: 83
		[Flags]
		public enum RegistryNotifications : uint
		{
			// Token: 0x040001A6 RID: 422
			None = 0U,
			// Token: 0x040001A7 RID: 423
			ChangeName = 1U,
			// Token: 0x040001A8 RID: 424
			ChangeAttributes = 2U,
			// Token: 0x040001A9 RID: 425
			LastSet = 4U,
			// Token: 0x040001AA RID: 426
			ChangeSecurity = 8U
		}

		// Token: 0x02000054 RID: 84
		[StructLayout(LayoutKind.Explicit)]
		internal struct WNodeHeader
		{
			// Token: 0x040001AB RID: 427
			[FieldOffset(0)]
			public uint bufferSize;

			// Token: 0x040001AC RID: 428
			[FieldOffset(4)]
			public uint providerId;

			// Token: 0x040001AD RID: 429
			[FieldOffset(8)]
			public uint version;

			// Token: 0x040001AE RID: 430
			[FieldOffset(12)]
			public uint linkage;

			// Token: 0x040001AF RID: 431
			[FieldOffset(16)]
			public IntPtr kernelHandle;

			// Token: 0x040001B0 RID: 432
			[FieldOffset(24)]
			public Guid guid;

			// Token: 0x040001B1 RID: 433
			[FieldOffset(40)]
			public uint clientContext;

			// Token: 0x040001B2 RID: 434
			[FieldOffset(44)]
			public uint flags;
		}

		// Token: 0x02000055 RID: 85
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct EventTraceProperties_Inner
		{
			// Token: 0x040001B3 RID: 435
			public DiagnosticsNativeMethods.WNodeHeader wnode;

			// Token: 0x040001B4 RID: 436
			public uint bufferSize;

			// Token: 0x040001B5 RID: 437
			public uint minimumBuffers;

			// Token: 0x040001B6 RID: 438
			public uint maximumBuffers;

			// Token: 0x040001B7 RID: 439
			public uint maximumFileSize;

			// Token: 0x040001B8 RID: 440
			public uint logFileMode;

			// Token: 0x040001B9 RID: 441
			public uint flushTimer;

			// Token: 0x040001BA RID: 442
			public uint enableFlags;

			// Token: 0x040001BB RID: 443
			public int ageLimit;

			// Token: 0x040001BC RID: 444
			public uint numberOfBuffers;

			// Token: 0x040001BD RID: 445
			public uint freeBuffers;

			// Token: 0x040001BE RID: 446
			public uint eventsLost;

			// Token: 0x040001BF RID: 447
			public uint buffersWritten;

			// Token: 0x040001C0 RID: 448
			public uint logBuffersLost;

			// Token: 0x040001C1 RID: 449
			public uint realTimeBuffersLost;

			// Token: 0x040001C2 RID: 450
			public IntPtr loggerThreadId;

			// Token: 0x040001C3 RID: 451
			public uint logFileNameOffset;

			// Token: 0x040001C4 RID: 452
			public uint loggerNameOffset;
		}

		// Token: 0x02000056 RID: 86
		[StructLayout(LayoutKind.Sequential)]
		internal struct EventTraceHeader
		{
			// Token: 0x060001A3 RID: 419 RVA: 0x00006128 File Offset: 0x00004328
			public EventTraceHeader(Guid eventClassGuid, byte eventTypeId)
			{
				this.eventClassGuid = eventClassGuid;
				this.eventTypeId = eventTypeId;
				this.flags = 1048576U;
				this.size = DiagnosticsNativeMethods.BinaryEventTraceSize;
				this.fieldTypeFlags = 0;
				this.level = 0;
				this.version = 0;
				this.threadId = 0U;
				this.processId = 0U;
				this.timeStamp = 0UL;
				this.clientContext = 0U;
			}

			// Token: 0x040001C5 RID: 453
			private ushort size;

			// Token: 0x040001C6 RID: 454
			private ushort fieldTypeFlags;

			// Token: 0x040001C7 RID: 455
			private byte eventTypeId;

			// Token: 0x040001C8 RID: 456
			private byte level;

			// Token: 0x040001C9 RID: 457
			private ushort version;

			// Token: 0x040001CA RID: 458
			private uint threadId;

			// Token: 0x040001CB RID: 459
			private uint processId;

			// Token: 0x040001CC RID: 460
			private ulong timeStamp;

			// Token: 0x040001CD RID: 461
			private Guid eventClassGuid;

			// Token: 0x040001CE RID: 462
			private uint clientContext;

			// Token: 0x040001CF RID: 463
			private uint flags;
		}

		// Token: 0x02000057 RID: 87
		[StructLayout(LayoutKind.Sequential)]
		internal struct MofField
		{
			// Token: 0x060001A4 RID: 420 RVA: 0x0000618C File Offset: 0x0000438C
			public GCHandle SetData(byte[] dataBuffer)
			{
				GCHandle result = GCHandle.Alloc(dataBuffer, GCHandleType.Pinned);
				this.dataPtr = (ulong)result.AddrOfPinnedObject().ToInt64();
				this.length = (uint)dataBuffer.Length;
				return result;
			}

			// Token: 0x040001D0 RID: 464
			[MarshalAs(UnmanagedType.U8)]
			private ulong dataPtr;

			// Token: 0x040001D1 RID: 465
			[MarshalAs(UnmanagedType.U4)]
			private uint length;

			// Token: 0x040001D2 RID: 466
			[MarshalAs(UnmanagedType.U4)]
			private uint dataType;
		}

		// Token: 0x02000058 RID: 88
		[StructLayout(LayoutKind.Sequential)]
		internal struct BinaryEventTrace
		{
			// Token: 0x060001A5 RID: 421 RVA: 0x000061C0 File Offset: 0x000043C0
			public BinaryEventTrace(Guid componentGuid, byte traceTag, byte[] traceData, out GCHandle? pinnedMemory)
			{
				this.header = new DiagnosticsNativeMethods.EventTraceHeader(componentGuid, traceTag);
				this.mofField = default(DiagnosticsNativeMethods.MofField);
				pinnedMemory = new GCHandle?(this.mofField.SetData(traceData));
			}

			// Token: 0x040001D3 RID: 467
			[MarshalAs(UnmanagedType.Struct)]
			private DiagnosticsNativeMethods.EventTraceHeader header;

			// Token: 0x040001D4 RID: 468
			[MarshalAs(UnmanagedType.Struct)]
			private DiagnosticsNativeMethods.MofField mofField;
		}

		// Token: 0x02000059 RID: 89
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct EventTraceProperties
		{
			// Token: 0x040001D5 RID: 469
			[FieldOffset(0)]
			public DiagnosticsNativeMethods.EventTraceProperties_Inner etp;

			// Token: 0x040001D6 RID: 470
			[FieldOffset(120)]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
			public string loggerName;

			// Token: 0x040001D7 RID: 471
			[FieldOffset(2168)]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
			public string logFileName;
		}

		// Token: 0x0200005A RID: 90
		internal struct TraceGuidRegistration
		{
			// Token: 0x040001D8 RID: 472
			public unsafe Guid* guid;

			// Token: 0x040001D9 RID: 473
			public IntPtr handle;
		}

		// Token: 0x0200005B RID: 91
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class WER_REPORT_INFORMATION
		{
			// Token: 0x040001DA RID: 474
			public uint size;

			// Token: 0x040001DB RID: 475
			public IntPtr process;

			// Token: 0x040001DC RID: 476
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string consentKey;

			// Token: 0x040001DD RID: 477
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string friendlyEventName;

			// Token: 0x040001DE RID: 478
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string applicationName;

			// Token: 0x040001DF RID: 479
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string applicationPath;

			// Token: 0x040001E0 RID: 480
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			public string description;

			// Token: 0x040001E1 RID: 481
			public IntPtr parentWindowHandle;
		}

		// Token: 0x0200005C RID: 92
		[StructLayout(LayoutKind.Sequential)]
		internal class WER_EXCEPTION_INFORMATION
		{
			// Token: 0x040001E2 RID: 482
			public IntPtr exceptionPointers;

			// Token: 0x040001E3 RID: 483
			public bool clientPointers;
		}

		// Token: 0x0200005D RID: 93
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class WER_DUMP_CUSTOM_OPTIONS
		{
			// Token: 0x040001E4 RID: 484
			public int size;

			// Token: 0x040001E5 RID: 485
			public DiagnosticsNativeMethods.WER_DUMP_FLAGS mask;

			// Token: 0x040001E6 RID: 486
			public DiagnosticsNativeMethods.MINIDUMP_TYPE dumpFlags;

			// Token: 0x040001E7 RID: 487
			[MarshalAs(UnmanagedType.Bool)]
			public bool onlyThisThread;

			// Token: 0x040001E8 RID: 488
			public uint exceptionThreadFlags;

			// Token: 0x040001E9 RID: 489
			public uint otherThreadFlags;

			// Token: 0x040001EA RID: 490
			public uint exceptionThreadExFlags;

			// Token: 0x040001EB RID: 491
			public uint otherThreadExFlags;

			// Token: 0x040001EC RID: 492
			public uint preferredModuleFlags;

			// Token: 0x040001ED RID: 493
			public uint otherModuleFlags;

			// Token: 0x040001EE RID: 494
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string preferredModuleList;
		}

		// Token: 0x0200005E RID: 94
		internal struct ExceptionPointers
		{
			// Token: 0x040001EF RID: 495
			public IntPtr ExceptionRecord;

			// Token: 0x040001F0 RID: 496
			public IntPtr ContextRecord;

			// Token: 0x040001F1 RID: 497
			public static DiagnosticsNativeMethods.ExceptionPointers Empty = default(DiagnosticsNativeMethods.ExceptionPointers);
		}

		// Token: 0x0200005F RID: 95
		internal struct ExceptionRecord
		{
			// Token: 0x040001F2 RID: 498
			public const int AccessViolation = -1073741819;

			// Token: 0x040001F3 RID: 499
			public const int ArrayBoundsExceeded = -1073741684;

			// Token: 0x040001F4 RID: 500
			public const int Breakpoint = -2147483645;

			// Token: 0x040001F5 RID: 501
			public const int DatatypeMisalignment = -2147483646;

			// Token: 0x040001F6 RID: 502
			public const int FloatDenormalOperand = -1073741683;

			// Token: 0x040001F7 RID: 503
			public const int FloatDivideByZero = -1073741682;

			// Token: 0x040001F8 RID: 504
			public const int FloatInexactResult = -1073741681;

			// Token: 0x040001F9 RID: 505
			public const int FloatInvalidOperation = -1073741680;

			// Token: 0x040001FA RID: 506
			public const int FloatOverflow = -1073741679;

			// Token: 0x040001FB RID: 507
			public const int FloatStackCheck = -1073741678;

			// Token: 0x040001FC RID: 508
			public const int FloatUnderflow = -1073741677;

			// Token: 0x040001FD RID: 509
			public const int IllegalInstruction = -1073741795;

			// Token: 0x040001FE RID: 510
			public const int PageError = -1073741818;

			// Token: 0x040001FF RID: 511
			public const int IntegerDivideByZero = -1073741676;

			// Token: 0x04000200 RID: 512
			public const int IntegerOverflow = -1073741675;

			// Token: 0x04000201 RID: 513
			public const int InvalidDisposition = -1073741786;

			// Token: 0x04000202 RID: 514
			public const int NonContinuableException = -1073741787;

			// Token: 0x04000203 RID: 515
			public const int PrivilegedInstruction = -1073741674;

			// Token: 0x04000204 RID: 516
			public const int SingleStep = -2147483644;

			// Token: 0x04000205 RID: 517
			public const int StackOverflow = -1073741571;

			// Token: 0x04000206 RID: 518
			public int ExceptionCode;

			// Token: 0x04000207 RID: 519
			public DiagnosticsNativeMethods.ExceptionFlag ExceptionFlags;

			// Token: 0x04000208 RID: 520
			public IntPtr InnerExceptionRecord;

			// Token: 0x04000209 RID: 521
			public IntPtr ExceptionAddress;

			// Token: 0x0400020A RID: 522
			public int NumberParameters;

			// Token: 0x0400020B RID: 523
			public IntPtr ExceptionInformation;

			// Token: 0x0400020C RID: 524
			public static DiagnosticsNativeMethods.ExceptionRecord Empty = default(DiagnosticsNativeMethods.ExceptionRecord);
		}

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x060001AC RID: 428
		internal delegate uint ControlCallback(int requestCode, IntPtr context, IntPtr reserved, IntPtr buffer);

		// Token: 0x02000061 RID: 97
		internal sealed class CriticalTraceRegistrationHandle : CriticalHandle
		{
			// Token: 0x060001AF RID: 431 RVA: 0x00006225 File Offset: 0x00004425
			private CriticalTraceRegistrationHandle() : base(IntPtr.Zero)
			{
				this.traceHandle = -1L;
			}

			// Token: 0x060001B0 RID: 432 RVA: 0x0000623C File Offset: 0x0000443C
			private void Initialize(DiagnosticsNativeMethods.ControlCallback callback, long handle)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.callback = callback;
					this.traceHandle = handle;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006270 File Offset: 0x00004470
			public override bool IsInvalid
			{
				get
				{
					return this.traceHandle == -1L;
				}
			}

			// Token: 0x060001B2 RID: 434
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegisterTraceGuidsW")]
			private static extern int RegisterTraceGuids([In] DiagnosticsNativeMethods.ControlCallback callback, [In] IntPtr context, [In] ref Guid controlGuid, [In] int guidCount, [In] ref DiagnosticsNativeMethods.TraceGuidRegistration guidRegistration, [In] string mofImagePath, [In] string mofResourceName, out long registrationHandle);

			// Token: 0x060001B3 RID: 435
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegisterTraceGuidsW")]
			private static extern int RegisterTraceGuids([In] DiagnosticsNativeMethods.ControlCallback callback, [In] IntPtr context, [In] ref Guid controlGuid, [In] int guidCount, IntPtr guidRegistrations, [In] string mofImagePath, [In] string mofResourceName, out long registrationHandle);

			// Token: 0x060001B4 RID: 436 RVA: 0x0000627C File Offset: 0x0000447C
			public static DiagnosticsNativeMethods.CriticalTraceRegistrationHandle RegisterTrace(Guid provider, ref DiagnosticsNativeMethods.TraceGuidRegistration guidRegistration, DiagnosticsNativeMethods.ControlCallback callback)
			{
				DiagnosticsNativeMethods.CriticalTraceRegistrationHandle criticalTraceRegistrationHandle = new DiagnosticsNativeMethods.CriticalTraceRegistrationHandle();
				long handle;
				int num = DiagnosticsNativeMethods.CriticalTraceRegistrationHandle.RegisterTraceGuids(callback, IntPtr.Zero, ref provider, 1, ref guidRegistration, null, null, out handle);
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
				criticalTraceRegistrationHandle.Initialize(callback, handle);
				return criticalTraceRegistrationHandle;
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x000062B8 File Offset: 0x000044B8
			public static DiagnosticsNativeMethods.CriticalTraceRegistrationHandle RegisterTrace(Guid provider, DiagnosticsNativeMethods.ControlCallback callback)
			{
				DiagnosticsNativeMethods.CriticalTraceRegistrationHandle criticalTraceRegistrationHandle = new DiagnosticsNativeMethods.CriticalTraceRegistrationHandle();
				long handle;
				int num = DiagnosticsNativeMethods.CriticalTraceRegistrationHandle.RegisterTraceGuids(callback, IntPtr.Zero, ref provider, 0, IntPtr.Zero, null, null, out handle);
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
				criticalTraceRegistrationHandle.Initialize(callback, handle);
				return criticalTraceRegistrationHandle;
			}

			// Token: 0x060001B6 RID: 438
			[DllImport("advapi32.dll")]
			private static extern uint UnregisterTraceGuids([In] long registrationHandle);

			// Token: 0x060001B7 RID: 439 RVA: 0x000062F6 File Offset: 0x000044F6
			protected override bool ReleaseHandle()
			{
				if (!this.IsInvalid)
				{
					DiagnosticsNativeMethods.CriticalTraceRegistrationHandle.UnregisterTraceGuids(this.traceHandle);
					this.traceHandle = -1L;
				}
				return true;
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x00006315 File Offset: 0x00004515
			protected override void Dispose(bool disposing)
			{
				if (!base.IsClosed && disposing)
				{
					base.Dispose(disposing);
					this.ReleaseHandle();
				}
			}

			// Token: 0x0400020D RID: 525
			private long traceHandle;

			// Token: 0x0400020E RID: 526
			private DiagnosticsNativeMethods.ControlCallback callback;
		}

		// Token: 0x02000062 RID: 98
		internal sealed class CriticalTraceHandle : CriticalHandle
		{
			// Token: 0x060001B9 RID: 441 RVA: 0x00006330 File Offset: 0x00004530
			private CriticalTraceHandle() : base(IntPtr.Zero)
			{
				this.traceHandle = -1L;
			}

			// Token: 0x060001BA RID: 442 RVA: 0x00006345 File Offset: 0x00004545
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			public long DangerousGetHandle()
			{
				return this.traceHandle;
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00006350 File Offset: 0x00004550
			private void Initialize(long handle)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.traceHandle = handle;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060001BC RID: 444 RVA: 0x0000637C File Offset: 0x0000457C
			public override bool IsInvalid
			{
				get
				{
					return this.traceHandle == -1L;
				}
			}

			// Token: 0x060001BD RID: 445
			[DllImport("advapi32.dll", SetLastError = true)]
			private static extern long GetTraceLoggerHandle([In] IntPtr buffer);

			// Token: 0x060001BE RID: 446 RVA: 0x00006388 File Offset: 0x00004588
			public static DiagnosticsNativeMethods.CriticalTraceHandle Attach(IntPtr buffer)
			{
				DiagnosticsNativeMethods.CriticalTraceHandle criticalTraceHandle = new DiagnosticsNativeMethods.CriticalTraceHandle();
				criticalTraceHandle.Initialize(DiagnosticsNativeMethods.CriticalTraceHandle.GetTraceLoggerHandle(buffer));
				if (criticalTraceHandle.IsInvalid)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				return criticalTraceHandle;
			}

			// Token: 0x060001BF RID: 447 RVA: 0x000063BB File Offset: 0x000045BB
			protected override bool ReleaseHandle()
			{
				return true;
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x000063BE File Offset: 0x000045BE
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
			}

			// Token: 0x0400020F RID: 527
			private long traceHandle;
		}

		// Token: 0x02000063 RID: 99
		public struct EventInstanceInfo
		{
			// Token: 0x04000210 RID: 528
			public IntPtr RegistrationHandle;

			// Token: 0x04000211 RID: 529
			public uint InstanceId;
		}
	}
}
