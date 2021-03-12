using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.Shared.MountPoint;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200006C RID: 108
	internal class NativeMethods
	{
		// Token: 0x0600032F RID: 815
		[DllImport("shlwapi.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "PathMatchSpecW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool PathMatchSpec([In] string fileName, [In] string pattern);

		// Token: 0x06000330 RID: 816
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern uint FormatMessage(uint dwFlags, ModuleHandle lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr bufferPointer, uint bufferSize, IntPtr arguments);

		// Token: 0x06000331 RID: 817
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern ModuleHandle LoadLibraryEx(string libFileName, IntPtr hFile, uint dwFlags);

		// Token: 0x06000332 RID: 818
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FreeLibrary(IntPtr hmodule);

		// Token: 0x06000333 RID: 819
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeFileHandle CreateFile(string fileName, FileAccess fileAccess, FileShare fileShare, IntPtr securityAttributes, FileMode creationDisposition, FileFlags flags, IntPtr template);

		// Token: 0x06000334 RID: 820
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetFilePointerEx(SafeFileHandle handle, long distanceToMove, out long newFilePointer, uint moveMethod);

		// Token: 0x06000335 RID: 821
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetEndOfFile(SafeFileHandle handle);

		// Token: 0x06000336 RID: 822
		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeviceIoControl(SafeFileHandle hDevice, uint dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);

		// Token: 0x06000337 RID: 823
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

		// Token: 0x06000338 RID: 824
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetFileAttributesExW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetFileAttributesEx([In] string fileName, [In] NativeMethods.GET_FILEEX_INFO_LEVELS fInfoLevelId, out NativeMethods.WIN32_FILE_ATTRIBUTE_DATA fileInformation);

		// Token: 0x06000339 RID: 825
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeviceIoControl([In] SafeFileHandle hDevice, [In] int dwIoControlCode, [In] IntPtr lpInBuffer, [In] int nInBufferSize, [Out] IntPtr lpOutBuffer, [In] int nOutBufferSize, out int lpBytesReturned, [In] IntPtr lpOverlapped);

		// Token: 0x0600033A RID: 826
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern void SetLastError([In] int errorCode);

		// Token: 0x0600033B RID: 827
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle([In] IntPtr handle);

		// Token: 0x0600033C RID: 828
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FindClose([In] SafeFileHandle handle);

		// Token: 0x0600033D RID: 829
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryPerformanceFrequency(out long freq);

		// Token: 0x0600033E RID: 830
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryPerformanceCounter(out long count);

		// Token: 0x0600033F RID: 831
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern long GetTickCount64();

		// Token: 0x06000340 RID: 832
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetVolumePathNameW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetVolumePathName([In] string fileName, [Out] StringBuilder volumePathName, [In] uint bufferLength);

		// Token: 0x06000341 RID: 833
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetVolumeNameForVolumeMountPointW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetVolumeNameForVolumeMountPoint([In] string volumeMountPoint, [Out] StringBuilder volumeName, [In] uint bufferLength);

		// Token: 0x06000342 RID: 834
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetVolumePathNamesForVolumeNameW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetVolumePathNamesForVolumeName([MarshalAs(UnmanagedType.LPWStr)] [In] string volumeName, [MarshalAs(UnmanagedType.LPWStr)] [Out] string volumePathNames, [In] uint bufferLen, [In] [Out] ref uint requiredBufferLen);

		// Token: 0x06000343 RID: 835
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindFirstVolumeW", SetLastError = true)]
		internal static extern SafeVolumeFindHandle FindFirstVolume([Out] StringBuilder volumeName, [In] uint bufferLength);

		// Token: 0x06000344 RID: 836
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindNextVolumeW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FindNextVolume([In] SafeVolumeFindHandle hFindVolume, [Out] StringBuilder volumeName, [In] uint bufferLength);

		// Token: 0x06000345 RID: 837
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindFirstVolumeMountPointW", SetLastError = true)]
		internal static extern SafeVolumeMountPointFindHandle FindFirstVolumeMountPoint([In] string volumeName, [Out] StringBuilder volumeMountPoint, [In] uint bufferLength);

		// Token: 0x06000346 RID: 838
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindNextVolumeMountPointW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FindNextVolumeMountPoint([In] SafeVolumeMountPointFindHandle hFindVolumeMountPoint, [Out] StringBuilder volumeMountPoint, [In] uint bufferLength);

		// Token: 0x06000347 RID: 839
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "DeleteVolumeMountPointW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteVolumeMountPoint([In] string volumeMountPoint);

		// Token: 0x06000348 RID: 840
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "SetVolumeMountPointW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetVolumeMountPoint([In] string volumeMountPoint, [In] string volumeName);

		// Token: 0x06000349 RID: 841
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "SetVolumeLabelW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetVolumeLabel([In] string volumeMountPoint, [In] string volumeLabel);

		// Token: 0x0600034A RID: 842
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetDriveTypeW", SetLastError = true)]
		internal static extern DriveType GetDriveType([In] string rootPathName);

		// Token: 0x0600034B RID: 843
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool ControlService(SafeHandle serviceHandle, int controlCode, ref NativeMethods.SERVICE_STATUS status);

		// Token: 0x0600034C RID: 844
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr OpenService(IntPtr databaseHandle, string serviceName, int access);

		// Token: 0x0600034D RID: 845
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr RegisterServiceCtrlHandler(string serviceName, Delegate callback);

		// Token: 0x0600034E RID: 846
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr RegisterServiceCtrlHandlerEx(string serviceName, Delegate callback, IntPtr userData);

		// Token: 0x0600034F RID: 847
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public unsafe static extern bool SetServiceStatus(IntPtr serviceStatusHandle, NativeMethods.SERVICE_STATUS* status);

		// Token: 0x06000350 RID: 848
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool StartServiceCtrlDispatcher(IntPtr entry);

		// Token: 0x06000351 RID: 849
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr CreateService(IntPtr databaseHandle, string serviceName, string displayName, int access, int serviceType, int startType, int errorControl, string binaryPath, string loadOrderGroup, IntPtr pTagId, string dependencies, string servicesStartName, string password);

		// Token: 0x06000352 RID: 850
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool DeleteService(IntPtr serviceHandle);

		// Token: 0x06000353 RID: 851
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool QueryServiceStatusEx(SafeHandle serviceHandle, int infoLevel, IntPtr buffer, int bufferSize, out int bytesNeeded);

		// Token: 0x040001E6 RID: 486
		internal const uint FILE_BEGIN = 0U;

		// Token: 0x040001E7 RID: 487
		internal const uint FILE_CURRENT = 1U;

		// Token: 0x040001E8 RID: 488
		internal const uint FILE_END = 2U;

		// Token: 0x040001E9 RID: 489
		internal const uint LOAD_LIBRARY_AS_DATAFILE = 2U;

		// Token: 0x040001EA RID: 490
		internal const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 256U;

		// Token: 0x040001EB RID: 491
		internal const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512U;

		// Token: 0x040001EC RID: 492
		internal const uint FORMAT_MESSAGE_FROM_HMODULE = 2048U;

		// Token: 0x040001ED RID: 493
		internal const uint FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192U;

		// Token: 0x040001EE RID: 494
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x040001EF RID: 495
		internal const string SHLWAPI = "shlwapi.dll";

		// Token: 0x040001F0 RID: 496
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x040001F1 RID: 497
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x040001F2 RID: 498
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x040001F3 RID: 499
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x040001F4 RID: 500
		internal const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x040001F5 RID: 501
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x040001F6 RID: 502
		internal const int ERROR_HANDLE_EOF = 38;

		// Token: 0x040001F7 RID: 503
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x040001F8 RID: 504
		internal const int ERROR_SHUTDOWN_IN_PROGRESS = 1115;

		// Token: 0x040001F9 RID: 505
		internal const int ERROR_RECOVERY_NOT_NEEDED = 6821;

		// Token: 0x040001FA RID: 506
		internal const int ERROR_SERVICE_DOES_NOT_EXIST = 1060;

		// Token: 0x040001FB RID: 507
		internal const int ERROR_NOT_A_REPARSE_POINT = 4390;

		// Token: 0x040001FC RID: 508
		internal const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

		// Token: 0x040001FD RID: 509
		private const int METHOD_BUFFERED = 0;

		// Token: 0x040001FE RID: 510
		private const int FILE_WRITE_DATA = 2;

		// Token: 0x040001FF RID: 511
		private const int FILE_DEVICE_FILE_SYSTEM = 9;

		// Token: 0x04000200 RID: 512
		private const int ROLLFORWARD_REDO_FUNCTION = 84;

		// Token: 0x04000201 RID: 513
		private const int ROLLFORWARD_UNDO_FUNCTION = 85;

		// Token: 0x04000202 RID: 514
		private const int START_RM_FUNCTION = 86;

		// Token: 0x04000203 RID: 515
		private const int SHUTDOWN_RM_FUNCTION = 87;

		// Token: 0x04000204 RID: 516
		private const int CREATE_SECONDARY_RM_FUNCTION = 90;

		// Token: 0x04000205 RID: 517
		internal const int MAX_VOLUME_GUID_LENGTH = 50;

		// Token: 0x04000206 RID: 518
		internal const int MAX_PATH = 260;

		// Token: 0x04000207 RID: 519
		public const int START_TYPE_AUTO = 2;

		// Token: 0x04000208 RID: 520
		public const int START_TYPE_BOOT = 0;

		// Token: 0x04000209 RID: 521
		public const int START_TYPE_DEMAND = 3;

		// Token: 0x0400020A RID: 522
		public const int START_TYPE_DISABLED = 4;

		// Token: 0x0400020B RID: 523
		public const int START_TYPE_SYSTEM = 1;

		// Token: 0x0400020C RID: 524
		public const int SERVICE_ACTIVE = 1;

		// Token: 0x0400020D RID: 525
		public const int SERVICE_INACTIVE = 2;

		// Token: 0x0400020E RID: 526
		public const int SERVICE_STATE_ALL = 3;

		// Token: 0x0400020F RID: 527
		public const int STATE_CONTINUE_PENDING = 5;

		// Token: 0x04000210 RID: 528
		public const int STATE_PAUSED = 7;

		// Token: 0x04000211 RID: 529
		public const int STATE_PAUSE_PENDING = 6;

		// Token: 0x04000212 RID: 530
		public const int STATE_RUNNING = 4;

		// Token: 0x04000213 RID: 531
		public const int STATE_START_PENDING = 2;

		// Token: 0x04000214 RID: 532
		public const int STATE_STOPPED = 1;

		// Token: 0x04000215 RID: 533
		public const int STATE_STOP_PENDING = 3;

		// Token: 0x04000216 RID: 534
		public const int STATUS_ACTIVE = 1;

		// Token: 0x04000217 RID: 535
		public const int STATUS_INACTIVE = 2;

		// Token: 0x04000218 RID: 536
		public const int STATUS_ALL = 3;

		// Token: 0x04000219 RID: 537
		public const int STATUS_OBJECT_NAME_NOT_FOUND = -1073741772;

		// Token: 0x0400021A RID: 538
		public const int NO_ERROR = 0;

		// Token: 0x0400021B RID: 539
		public const int ACCEPT_NETBINDCHANGE = 16;

		// Token: 0x0400021C RID: 540
		public const int ACCEPT_PAUSE_CONTINUE = 2;

		// Token: 0x0400021D RID: 541
		public const int ACCEPT_PARAMCHANGE = 8;

		// Token: 0x0400021E RID: 542
		public const int ACCEPT_POWEREVENT = 64;

		// Token: 0x0400021F RID: 543
		public const int ACCEPT_SHUTDOWN = 4;

		// Token: 0x04000220 RID: 544
		public const int ACCEPT_STOP = 1;

		// Token: 0x04000221 RID: 545
		public const int ACCEPT_SESSIONCHANGE = 128;

		// Token: 0x04000222 RID: 546
		public const int ACCEPT_PRESHUTDOWN = 256;

		// Token: 0x04000223 RID: 547
		public const int CONTROL_CONTINUE = 3;

		// Token: 0x04000224 RID: 548
		public const int CONTROL_INTERROGATE = 4;

		// Token: 0x04000225 RID: 549
		public const int CONTROL_NETBINDADD = 7;

		// Token: 0x04000226 RID: 550
		public const int CONTROL_NETBINDDISABLE = 10;

		// Token: 0x04000227 RID: 551
		public const int CONTROL_NETBINDENABLE = 9;

		// Token: 0x04000228 RID: 552
		public const int CONTROL_NETBINDREMOVE = 8;

		// Token: 0x04000229 RID: 553
		public const int CONTROL_PARAMCHANGE = 6;

		// Token: 0x0400022A RID: 554
		public const int CONTROL_PAUSE = 2;

		// Token: 0x0400022B RID: 555
		public const int CONTROL_POWEREVENT = 13;

		// Token: 0x0400022C RID: 556
		public const int CONTROL_SHUTDOWN = 5;

		// Token: 0x0400022D RID: 557
		public const int CONTROL_STOP = 1;

		// Token: 0x0400022E RID: 558
		public const int CONTROL_DEVICEEVENT = 11;

		// Token: 0x0400022F RID: 559
		public const int CONTROL_SESSIONCHANGE = 14;

		// Token: 0x04000230 RID: 560
		public const int CONTROL_PRESHUTDOWN = 15;

		// Token: 0x04000231 RID: 561
		public const int SERVICE_CONFIG_DESCRIPTION = 1;

		// Token: 0x04000232 RID: 562
		public const int SERVICE_CONFIG_FAILURE_ACTIONS = 2;

		// Token: 0x04000233 RID: 563
		public const int SERVICE_TYPE_WIN32_OWN_PROCESS = 16;

		// Token: 0x04000234 RID: 564
		public const int SERVICE_TYPE_WIN32_SHARE_PROCESS = 32;

		// Token: 0x04000235 RID: 565
		public const int SC_STATUS_PROCESS_INFO = 0;

		// Token: 0x0200006D RID: 109
		[Flags]
		internal enum CopyFileFlags : uint
		{
			// Token: 0x04000237 RID: 567
			COPY_FILE_FAIL_IF_EXISTS = 1U,
			// Token: 0x04000238 RID: 568
			COPY_FILE_RESTARTABLE = 2U,
			// Token: 0x04000239 RID: 569
			COPY_FILE_OPEN_SOURCE_FOR_WRITE = 4U,
			// Token: 0x0400023A RID: 570
			COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 8U,
			// Token: 0x0400023B RID: 571
			COPY_FILE_COPY_SYMLINK = 2048U
		}

		// Token: 0x0200006E RID: 110
		[Flags]
		internal enum MoveFileFlags : uint
		{
			// Token: 0x0400023D RID: 573
			MOVEFILE_REPLACE_EXISTING = 1U,
			// Token: 0x0400023E RID: 574
			MOVEFILE_COPY_ALLOWED = 2U,
			// Token: 0x0400023F RID: 575
			MOVEFILE_DELAY_UNTIL_REBOOT = 4U,
			// Token: 0x04000240 RID: 576
			MOVEFILE_WRITE_THROUGH = 8U,
			// Token: 0x04000241 RID: 577
			MOVEFILE_CREATE_HARDLINK = 16U,
			// Token: 0x04000242 RID: 578
			MOVEFILE_FAIL_IF_NOT_TRACKABLE = 32U
		}

		// Token: 0x0200006F RID: 111
		internal enum FINDEX_INFO_LEVELS
		{
			// Token: 0x04000244 RID: 580
			FindExInfoStandard,
			// Token: 0x04000245 RID: 581
			FindExInfoMaxInfoLevel
		}

		// Token: 0x02000070 RID: 112
		internal enum FINDEX_SEARCH_OPS
		{
			// Token: 0x04000247 RID: 583
			FindExSearchNameMatch,
			// Token: 0x04000248 RID: 584
			FindExSearchLimitToDirectories,
			// Token: 0x04000249 RID: 585
			FindExSearchLimitToDevices,
			// Token: 0x0400024A RID: 586
			FindExSearchMaxSearchOp
		}

		// Token: 0x02000071 RID: 113
		internal struct FILETIME
		{
			// Token: 0x0400024B RID: 587
			public uint DateTimeLow;

			// Token: 0x0400024C RID: 588
			public uint DateTimeHigh;
		}

		// Token: 0x02000072 RID: 114
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct WIN32_FIND_DATA
		{
			// Token: 0x0400024D RID: 589
			public uint dwFileAttributes;

			// Token: 0x0400024E RID: 590
			public NativeMethods.FILETIME ftCreationTime;

			// Token: 0x0400024F RID: 591
			public NativeMethods.FILETIME ftLastAccessTime;

			// Token: 0x04000250 RID: 592
			public NativeMethods.FILETIME ftLastWriteTime;

			// Token: 0x04000251 RID: 593
			public uint nFileSizeHigh;

			// Token: 0x04000252 RID: 594
			public uint nFileSizeLow;

			// Token: 0x04000253 RID: 595
			public uint dwReserved0;

			// Token: 0x04000254 RID: 596
			public uint dwReserved1;

			// Token: 0x04000255 RID: 597
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string cFileName;

			// Token: 0x04000256 RID: 598
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;
		}

		// Token: 0x02000073 RID: 115
		[BestFitMapping(false)]
		[Serializable]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WIN32_FILE_ATTRIBUTE_DATA
		{
			// Token: 0x04000257 RID: 599
			internal FileAttributes FileAttributes;

			// Token: 0x04000258 RID: 600
			internal NativeMethods.FILETIME CreationTime;

			// Token: 0x04000259 RID: 601
			internal NativeMethods.FILETIME LastAccessTime;

			// Token: 0x0400025A RID: 602
			internal NativeMethods.FILETIME LastWriteTime;

			// Token: 0x0400025B RID: 603
			public uint FileSizeHigh;

			// Token: 0x0400025C RID: 604
			public uint FileSizeLow;
		}

		// Token: 0x02000074 RID: 116
		public enum GET_FILEEX_INFO_LEVELS
		{
			// Token: 0x0400025E RID: 606
			GetFileExInfoStandard,
			// Token: 0x0400025F RID: 607
			GetFileExMaxInfoLevel
		}

		// Token: 0x02000075 RID: 117
		internal enum ReparseTags : uint
		{
			// Token: 0x04000261 RID: 609
			MountPoint = 2684354563U,
			// Token: 0x04000262 RID: 610
			SymbolicLink = 2684354572U
		}

		// Token: 0x02000076 RID: 118
		public struct SERVICE_STATUS
		{
			// Token: 0x04000263 RID: 611
			public int serviceType;

			// Token: 0x04000264 RID: 612
			public int currentState;

			// Token: 0x04000265 RID: 613
			public int controlsAccepted;

			// Token: 0x04000266 RID: 614
			public int win32ExitCode;

			// Token: 0x04000267 RID: 615
			public int serviceSpecificExitCode;

			// Token: 0x04000268 RID: 616
			public int checkPoint;

			// Token: 0x04000269 RID: 617
			public int waitHint;
		}

		// Token: 0x02000077 RID: 119
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class SERVICE_STATUS_PROCESS
		{
			// Token: 0x0400026A RID: 618
			public int serviceType;

			// Token: 0x0400026B RID: 619
			public int currentState;

			// Token: 0x0400026C RID: 620
			public int controlsAccepted;

			// Token: 0x0400026D RID: 621
			public int win32ExitCode;

			// Token: 0x0400026E RID: 622
			public int serviceSpecificExitCode;

			// Token: 0x0400026F RID: 623
			public int checkPoint;

			// Token: 0x04000270 RID: 624
			public int waitHint;

			// Token: 0x04000271 RID: 625
			public int processID;

			// Token: 0x04000272 RID: 626
			public int serviceFlags;
		}

		// Token: 0x02000078 RID: 120
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class ENUM_SERVICE_STATUS
		{
			// Token: 0x04000273 RID: 627
			public string serviceName;

			// Token: 0x04000274 RID: 628
			public string displayName;

			// Token: 0x04000275 RID: 629
			public int serviceType;

			// Token: 0x04000276 RID: 630
			public int currentState;

			// Token: 0x04000277 RID: 631
			public int controlsAccepted;

			// Token: 0x04000278 RID: 632
			public int win32ExitCode;

			// Token: 0x04000279 RID: 633
			public int serviceSpecificExitCode;

			// Token: 0x0400027A RID: 634
			public int checkPoint;

			// Token: 0x0400027B RID: 635
			public int waitHint;
		}

		// Token: 0x02000079 RID: 121
		[StructLayout(LayoutKind.Sequential)]
		public class SERVICE_TABLE_ENTRY
		{
			// Token: 0x0400027C RID: 636
			public IntPtr name;

			// Token: 0x0400027D RID: 637
			public Delegate callback;
		}

		// Token: 0x0200007A RID: 122
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class WTSSESSION_NOTIFICATION
		{
			// Token: 0x0400027E RID: 638
			public int size;

			// Token: 0x0400027F RID: 639
			public int sessionId;
		}

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x0600035A RID: 858
		public delegate void ServiceMainCallback(int argCount, IntPtr argPointer);

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x0600035E RID: 862
		public delegate void ServiceControlCallback(int control);

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x06000362 RID: 866
		public delegate int ServiceControlCallbackEx(int control, int eventType, IntPtr eventData, IntPtr eventContext);
	}
}
