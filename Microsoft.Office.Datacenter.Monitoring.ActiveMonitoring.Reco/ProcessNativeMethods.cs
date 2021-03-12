using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000008 RID: 8
	public class ProcessNativeMethods
	{
		// Token: 0x0600002A RID: 42
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern long GetTickCount64();

		// Token: 0x0600002B RID: 43
		[DllImport("dbghelp.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool MiniDumpWriteDump([In] IntPtr processHandle, [In] int processId, [In] IntPtr fileHandle, [In] uint dumpType, [In] IntPtr exceptionParam, [In] IntPtr userStreamParam, [In] IntPtr callbackParam);

		// Token: 0x0600002C RID: 44
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetDiskFreeSpaceEx(string directoryName, out ulong freeBytesAvailable, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes);

		// Token: 0x0600002D RID: 45
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern ToolhelpSnapshotSafeHandle CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

		// Token: 0x0600002E RID: 46
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool Process32FirstW(ToolhelpSnapshotSafeHandle handle, ref ProcessNativeMethods.PROCESSENTRY32W lppe);

		// Token: 0x0600002F RID: 47
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool Process32NextW(ToolhelpSnapshotSafeHandle handle, ref ProcessNativeMethods.PROCESSENTRY32W lppe);

		// Token: 0x06000030 RID: 48
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool QueryServiceStatusEx(SafeHandle serviceHandle, int infoLevel, IntPtr buffer, int bufferSize, out int bytesNeeded);

		// Token: 0x06000031 RID: 49
		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool ControlService(SafeHandle serviceHandle, int controlCode, ref ProcessNativeMethods.SERVICE_STATUS status);

		// Token: 0x04000016 RID: 22
		internal const int MaxPath = 260;

		// Token: 0x04000017 RID: 23
		internal const uint Th32CsSnapProcess = 2U;

		// Token: 0x04000018 RID: 24
		internal const int ErrorNoMoreFiles = 18;

		// Token: 0x02000009 RID: 9
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct PROCESSENTRY32W
		{
			// Token: 0x17000007 RID: 7
			// (set) Token: 0x06000033 RID: 51 RVA: 0x00002CB0 File Offset: 0x00000EB0
			internal uint Size
			{
				set
				{
					this.dwSize = value;
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000034 RID: 52 RVA: 0x00002CB9 File Offset: 0x00000EB9
			internal uint ProcessId
			{
				get
				{
					return this.th32ProcessID;
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000035 RID: 53 RVA: 0x00002CC1 File Offset: 0x00000EC1
			internal uint ParentProcessId
			{
				get
				{
					return this.th32ParentProcessID;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000036 RID: 54 RVA: 0x00002CC9 File Offset: 0x00000EC9
			internal string ProcessName
			{
				get
				{
					return this.szExeFile;
				}
			}

			// Token: 0x04000019 RID: 25
			private uint dwSize;

			// Token: 0x0400001A RID: 26
			private uint cntUsage;

			// Token: 0x0400001B RID: 27
			private uint th32ProcessID;

			// Token: 0x0400001C RID: 28
			private IntPtr th32DefaultHeapID;

			// Token: 0x0400001D RID: 29
			private uint th32ModuleID;

			// Token: 0x0400001E RID: 30
			private uint cntThreads;

			// Token: 0x0400001F RID: 31
			private uint th32ParentProcessID;

			// Token: 0x04000020 RID: 32
			private uint pcPriClassBase;

			// Token: 0x04000021 RID: 33
			private uint dwFlags;

			// Token: 0x04000022 RID: 34
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			private string szExeFile;
		}

		// Token: 0x0200000A RID: 10
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SERVICE_STATUS
		{
			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000037 RID: 55 RVA: 0x00002CD1 File Offset: 0x00000ED1
			public int ServiceType
			{
				get
				{
					return this.serviceType;
				}
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000038 RID: 56 RVA: 0x00002CD9 File Offset: 0x00000ED9
			public int CurrentState
			{
				get
				{
					return this.currentState;
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000039 RID: 57 RVA: 0x00002CE1 File Offset: 0x00000EE1
			public int ControlsAccepted
			{
				get
				{
					return this.controlsAccepted;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600003A RID: 58 RVA: 0x00002CE9 File Offset: 0x00000EE9
			public int Win32ExitCode
			{
				get
				{
					return this.win32ExitCode;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600003B RID: 59 RVA: 0x00002CF1 File Offset: 0x00000EF1
			public int ServiceSpecificExitCode
			{
				get
				{
					return this.serviceSpecificExitCode;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600003C RID: 60 RVA: 0x00002CF9 File Offset: 0x00000EF9
			public int CheckPoint
			{
				get
				{
					return this.checkPoint;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600003D RID: 61 RVA: 0x00002D01 File Offset: 0x00000F01
			public int WaitHint
			{
				get
				{
					return this.waitHint;
				}
			}

			// Token: 0x04000023 RID: 35
			private int serviceType;

			// Token: 0x04000024 RID: 36
			private int currentState;

			// Token: 0x04000025 RID: 37
			private int controlsAccepted;

			// Token: 0x04000026 RID: 38
			private int win32ExitCode;

			// Token: 0x04000027 RID: 39
			private int serviceSpecificExitCode;

			// Token: 0x04000028 RID: 40
			private int checkPoint;

			// Token: 0x04000029 RID: 41
			private int waitHint;
		}

		// Token: 0x0200000B RID: 11
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class SERVICE_STATUS_PROCESS
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x0600003E RID: 62 RVA: 0x00002D09 File Offset: 0x00000F09
			// (set) Token: 0x0600003F RID: 63 RVA: 0x00002D11 File Offset: 0x00000F11
			internal int ServiceType
			{
				get
				{
					return this.serviceType;
				}
				set
				{
					this.serviceType = value;
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000040 RID: 64 RVA: 0x00002D1A File Offset: 0x00000F1A
			// (set) Token: 0x06000041 RID: 65 RVA: 0x00002D22 File Offset: 0x00000F22
			internal int CurrentState
			{
				get
				{
					return this.currentState;
				}
				set
				{
					this.currentState = value;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D2B File Offset: 0x00000F2B
			// (set) Token: 0x06000043 RID: 67 RVA: 0x00002D33 File Offset: 0x00000F33
			internal int ControlsAccepted
			{
				get
				{
					return this.controlsAccepted;
				}
				set
				{
					this.controlsAccepted = value;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D3C File Offset: 0x00000F3C
			// (set) Token: 0x06000045 RID: 69 RVA: 0x00002D44 File Offset: 0x00000F44
			internal int Win32ExitCode
			{
				get
				{
					return this.win32ExitCode;
				}
				set
				{
					this.win32ExitCode = value;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D4D File Offset: 0x00000F4D
			// (set) Token: 0x06000047 RID: 71 RVA: 0x00002D55 File Offset: 0x00000F55
			internal int ServiceSpecificExitCode
			{
				get
				{
					return this.serviceSpecificExitCode;
				}
				set
				{
					this.serviceSpecificExitCode = value;
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000048 RID: 72 RVA: 0x00002D5E File Offset: 0x00000F5E
			// (set) Token: 0x06000049 RID: 73 RVA: 0x00002D66 File Offset: 0x00000F66
			internal int CheckPoint
			{
				get
				{
					return this.checkPoint;
				}
				set
				{
					this.checkPoint = value;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D6F File Offset: 0x00000F6F
			// (set) Token: 0x0600004B RID: 75 RVA: 0x00002D77 File Offset: 0x00000F77
			internal int WaitHint
			{
				get
				{
					return this.waitHint;
				}
				set
				{
					this.waitHint = value;
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D80 File Offset: 0x00000F80
			// (set) Token: 0x0600004D RID: 77 RVA: 0x00002D88 File Offset: 0x00000F88
			internal int ProcessID
			{
				get
				{
					return this.processID;
				}
				set
				{
					this.processID = value;
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D91 File Offset: 0x00000F91
			// (set) Token: 0x0600004F RID: 79 RVA: 0x00002D99 File Offset: 0x00000F99
			internal int ServiceFlags
			{
				get
				{
					return this.serviceFlags;
				}
				set
				{
					this.serviceFlags = value;
				}
			}

			// Token: 0x0400002A RID: 42
			private int serviceType;

			// Token: 0x0400002B RID: 43
			private int currentState;

			// Token: 0x0400002C RID: 44
			private int controlsAccepted;

			// Token: 0x0400002D RID: 45
			private int win32ExitCode;

			// Token: 0x0400002E RID: 46
			private int serviceSpecificExitCode;

			// Token: 0x0400002F RID: 47
			private int checkPoint;

			// Token: 0x04000030 RID: 48
			private int waitHint;

			// Token: 0x04000031 RID: 49
			private int processID;

			// Token: 0x04000032 RID: 50
			private int serviceFlags;
		}
	}
}
