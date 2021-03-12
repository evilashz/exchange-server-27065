﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x0200000E RID: 14
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	internal static class Win32Native
	{
		// Token: 0x0600001A RID: 26
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern void GetSystemInfo(ref Win32Native.SYSTEM_INFO lpSystemInfo);

		// Token: 0x0600001B RID: 27
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Auto)]
		internal static extern int FormatMessage(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, [Out] StringBuilder lpBuffer, int nSize, IntPtr va_list_arguments);

		// Token: 0x0600001C RID: 28 RVA: 0x00002214 File Offset: 0x00000414
		internal static string GetMessage(int errorCode)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(512);
			int num = Win32Native.FormatMessage(12800, IntPtr.Zero, errorCode, 0, stringBuilder, stringBuilder.Capacity, IntPtr.Zero);
			if (num != 0)
			{
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
			StringBuilderCache.Release(stringBuilder);
			return Environment.GetResourceString("UnknownError_Num", new object[]
			{
				errorCode
			});
		}

		// Token: 0x0600001D RID: 29
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", EntryPoint = "LocalAlloc")]
		internal static extern IntPtr LocalAlloc_NoSafeHandle(int uFlags, UIntPtr sizetdwBytes);

		// Token: 0x0600001E RID: 30
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeLocalAllocHandle LocalAlloc([In] int uFlags, [In] UIntPtr sizetdwBytes);

		// Token: 0x0600001F RID: 31
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06000020 RID: 32
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", EntryPoint = "RtlZeroMemory")]
		internal static extern void ZeroMemory(IntPtr address, UIntPtr length);

		// Token: 0x06000021 RID: 33 RVA: 0x00002273 File Offset: 0x00000473
		internal static bool GlobalMemoryStatusEx(ref Win32Native.MEMORYSTATUSEX buffer)
		{
			buffer.length = Marshal.SizeOf(typeof(Win32Native.MEMORYSTATUSEX));
			return Win32Native.GlobalMemoryStatusExNative(ref buffer);
		}

		// Token: 0x06000022 RID: 34
		[DllImport("kernel32.dll", EntryPoint = "GlobalMemoryStatusEx", SetLastError = true)]
		private static extern bool GlobalMemoryStatusExNative([In] [Out] ref Win32Native.MEMORYSTATUSEX buffer);

		// Token: 0x06000023 RID: 35
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern UIntPtr VirtualQuery(void* address, ref Win32Native.MEMORY_BASIC_INFORMATION buffer, UIntPtr sizeOfBuffer);

		// Token: 0x06000024 RID: 36
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern void* VirtualAlloc(void* address, UIntPtr numBytes, int commitOrReserve, int pageProtectionMode);

		// Token: 0x06000025 RID: 37
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool VirtualFree(void* address, UIntPtr numBytes, int pageFreeMode);

		// Token: 0x06000026 RID: 38
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);

		// Token: 0x06000027 RID: 39
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string moduleName);

		// Token: 0x06000028 RID: 40 RVA: 0x00002290 File Offset: 0x00000490
		[SecurityCritical]
		internal static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = Win32Native.GetModuleHandle(moduleName);
			if (moduleHandle == IntPtr.Zero)
			{
				return false;
			}
			IntPtr procAddress = Win32Native.GetProcAddress(moduleHandle, methodName);
			return procAddress != IntPtr.Zero;
		}

		// Token: 0x06000029 RID: 41
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWow64Process([In] IntPtr hSourceProcessHandle, [MarshalAs(UnmanagedType.Bool)] out bool isWow64);

		// Token: 0x0600002A RID: 42
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern uint GetTempPath(int bufferLen, [Out] StringBuilder buffer);

		// Token: 0x0600002B RID: 43
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		internal static extern int lstrlenA(IntPtr ptr);

		// Token: 0x0600002C RID: 44
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int lstrlenW(IntPtr ptr);

		// Token: 0x0600002D RID: 45
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr SysAllocStringLen(string src, int len);

		// Token: 0x0600002E RID: 46
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("oleaut32.dll")]
		internal static extern IntPtr SysAllocStringByteLen(byte[] str, uint len);

		// Token: 0x0600002F RID: 47
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll")]
		internal static extern uint SysStringByteLen(IntPtr bstr);

		// Token: 0x06000030 RID: 48
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll")]
		internal static extern uint SysStringLen(IntPtr bstr);

		// Token: 0x06000031 RID: 49
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll")]
		internal static extern uint SysStringLen(SafeBSTRHandle bstr);

		// Token: 0x06000032 RID: 50
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll")]
		internal static extern void SysFreeString(IntPtr bstr);

		// Token: 0x06000033 RID: 51
		[DllImport("kernel32.dll")]
		internal static extern int GetACP();

		// Token: 0x06000034 RID: 52
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetEvent(SafeWaitHandle handle);

		// Token: 0x06000035 RID: 53
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ResetEvent(SafeWaitHandle handle);

		// Token: 0x06000036 RID: 54
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeWaitHandle CreateEvent(Win32Native.SECURITY_ATTRIBUTES lpSecurityAttributes, bool isManualReset, bool initialState, string name);

		// Token: 0x06000037 RID: 55
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeWaitHandle OpenEvent(int desiredAccess, bool inheritHandle, string name);

		// Token: 0x06000038 RID: 56
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeWaitHandle CreateMutex(Win32Native.SECURITY_ATTRIBUTES lpSecurityAttributes, bool initialOwner, string name);

		// Token: 0x06000039 RID: 57
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeWaitHandle OpenMutex(int desiredAccess, bool inheritHandle, string name);

		// Token: 0x0600003A RID: 58
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReleaseMutex(SafeWaitHandle handle);

		// Token: 0x0600003B RID: 59
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal unsafe static extern int GetFullPathName(char* path, int numBufferChars, char* buffer, IntPtr mustBeZero);

		// Token: 0x0600003C RID: 60
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern uint GetFullPathNameW(char* path, uint numBufferChars, SafeHandle buffer, IntPtr mustBeZero);

		// Token: 0x0600003D RID: 61
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetFullPathName(string path, int numBufferChars, [Out] StringBuilder buffer, IntPtr mustBeZero);

		// Token: 0x0600003E RID: 62
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal unsafe static extern int GetLongPathName(char* path, char* longPathBuffer, int bufferLength);

		// Token: 0x0600003F RID: 63
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetLongPathName(string path, [Out] StringBuilder longPathBuffer, int bufferLength);

		// Token: 0x06000040 RID: 64
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetLongPathNameW(SafeHandle lpszShortPath, SafeHandle lpszLongPath, uint cchBuffer);

		// Token: 0x06000041 RID: 65
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetLongPathNameW(string lpszShortPath, SafeHandle lpszLongPath, uint cchBuffer);

		// Token: 0x06000042 RID: 66 RVA: 0x000022C8 File Offset: 0x000004C8
		[SecurityCritical]
		internal static SafeFileHandle SafeCreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, Win32Native.SECURITY_ATTRIBUTES securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile)
		{
			SafeFileHandle safeFileHandle = Win32Native.CreateFile(lpFileName, dwDesiredAccess, dwShareMode, securityAttrs, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
			if (!safeFileHandle.IsInvalid)
			{
				int fileType = Win32Native.GetFileType(safeFileHandle);
				if (fileType != 1)
				{
					safeFileHandle.Dispose();
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_FileStreamOnNonFiles"));
				}
			}
			return safeFileHandle;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002310 File Offset: 0x00000510
		[SecurityCritical]
		internal static SafeFileHandle UnsafeCreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, Win32Native.SECURITY_ATTRIBUTES securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile)
		{
			return Win32Native.CreateFile(lpFileName, dwDesiredAccess, dwShareMode, securityAttrs, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
		}

		// Token: 0x06000044 RID: 68
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, Win32Native.SECURITY_ATTRIBUTES securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000045 RID: 69
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeFileMappingHandle CreateFileMapping(SafeFileHandle hFile, IntPtr lpAttributes, uint fProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

		// Token: 0x06000046 RID: 70
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr MapViewOfFile(SafeFileMappingHandle handle, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, UIntPtr dwNumerOfBytesToMap);

		// Token: 0x06000047 RID: 71
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		// Token: 0x06000048 RID: 72
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000049 RID: 73
		[DllImport("kernel32.dll")]
		internal static extern int GetFileType(SafeFileHandle handle);

		// Token: 0x0600004A RID: 74
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetEndOfFile(SafeFileHandle hFile);

		// Token: 0x0600004B RID: 75
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FlushFileBuffers(SafeFileHandle hFile);

		// Token: 0x0600004C RID: 76
		[DllImport("kernel32.dll", EntryPoint = "SetFilePointer", SetLastError = true)]
		private unsafe static extern int SetFilePointerWin32(SafeFileHandle handle, int lo, int* hi, int origin);

		// Token: 0x0600004D RID: 77 RVA: 0x00002330 File Offset: 0x00000530
		[SecurityCritical]
		internal unsafe static long SetFilePointer(SafeFileHandle handle, long offset, SeekOrigin origin, out int hr)
		{
			hr = 0;
			int num = (int)offset;
			int num2 = (int)(offset >> 32);
			num = Win32Native.SetFilePointerWin32(handle, num, &num2, (int)origin);
			if (num == -1 && (hr = Marshal.GetLastWin32Error()) != 0)
			{
				return -1L;
			}
			return (long)((ulong)num2 << 32 | (ulong)num);
		}

		// Token: 0x0600004E RID: 78
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafeFileHandle handle, byte* bytes, int numBytesToRead, IntPtr numBytesRead_mustBeZero, NativeOverlapped* overlapped);

		// Token: 0x0600004F RID: 79
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafeFileHandle handle, byte* bytes, int numBytesToRead, out int numBytesRead, IntPtr mustBeZero);

		// Token: 0x06000050 RID: 80
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeFileHandle handle, byte* bytes, int numBytesToWrite, IntPtr numBytesWritten_mustBeZero, NativeOverlapped* lpOverlapped);

		// Token: 0x06000051 RID: 81
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeFileHandle handle, byte* bytes, int numBytesToWrite, out int numBytesWritten, IntPtr mustBeZero);

		// Token: 0x06000052 RID: 82
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool CancelIoEx(SafeFileHandle handle, NativeOverlapped* lpOverlapped);

		// Token: 0x06000053 RID: 83
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetDiskFreeSpaceEx(string drive, out long freeBytesForUser, out long totalBytes, out long freeBytes);

		// Token: 0x06000054 RID: 84
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetDriveType(string drive);

		// Token: 0x06000055 RID: 85
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetVolumeInformation(string drive, [Out] StringBuilder volumeName, int volumeNameBufLen, out int volSerialNumber, out int maxFileNameLen, out int fileSystemFlags, [Out] StringBuilder fileSystemName, int fileSystemNameBufLen);

		// Token: 0x06000056 RID: 86
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetVolumeLabel(string driveLetter, string volumeName);

		// Token: 0x06000057 RID: 87
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryPerformanceCounter(out long value);

		// Token: 0x06000058 RID: 88
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryPerformanceFrequency(out long value);

		// Token: 0x06000059 RID: 89
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeWaitHandle CreateSemaphore(Win32Native.SECURITY_ATTRIBUTES lpSecurityAttributes, int initialCount, int maximumCount, string name);

		// Token: 0x0600005A RID: 90
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ReleaseSemaphore(SafeWaitHandle handle, int releaseCount, out int previousCount);

		// Token: 0x0600005B RID: 91
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetWindowsDirectory([Out] StringBuilder sb, int length);

		// Token: 0x0600005C RID: 92
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetSystemDirectory([Out] StringBuilder sb, int length);

		// Token: 0x0600005D RID: 93
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool SetFileTime(SafeFileHandle hFile, Win32Native.FILE_TIME* creationTime, Win32Native.FILE_TIME* lastAccessTime, Win32Native.FILE_TIME* lastWriteTime);

		// Token: 0x0600005E RID: 94
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetFileSize(SafeFileHandle hFile, out int highSize);

		// Token: 0x0600005F RID: 95
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool LockFile(SafeFileHandle handle, int offsetLow, int offsetHigh, int countLow, int countHigh);

		// Token: 0x06000060 RID: 96
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool UnlockFile(SafeFileHandle handle, int offsetLow, int offsetHigh, int countLow, int countHigh);

		// Token: 0x06000061 RID: 97
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetStdHandle(int nStdHandle);

		// Token: 0x06000062 RID: 98 RVA: 0x00002370 File Offset: 0x00000570
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}

		// Token: 0x06000063 RID: 99
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CopyFile(string src, string dst, bool failIfExists);

		// Token: 0x06000064 RID: 100
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CreateDirectory(string path, Win32Native.SECURITY_ATTRIBUTES lpSecurityAttributes);

		// Token: 0x06000065 RID: 101
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DeleteFile(string path);

		// Token: 0x06000066 RID: 102
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ReplaceFile(string replacedFileName, string replacementFileName, string backupFileName, int dwReplaceFlags, IntPtr lpExclude, IntPtr lpReserved);

		// Token: 0x06000067 RID: 103
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DecryptFile(string path, int reservedMustBeZero);

		// Token: 0x06000068 RID: 104
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool EncryptFile(string path);

		// Token: 0x06000069 RID: 105
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeFindHandle FindFirstFile(string fileName, ref Win32Native.WIN32_FIND_DATA data);

		// Token: 0x0600006A RID: 106
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool FindNextFile(SafeFindHandle hndFindFile, ref Win32Native.WIN32_FIND_DATA lpFindFileData);

		// Token: 0x0600006B RID: 107
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern bool FindClose(IntPtr handle);

		// Token: 0x0600006C RID: 108
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetCurrentDirectory(int nBufferLength, [Out] StringBuilder lpBuffer);

		// Token: 0x0600006D RID: 109
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetCurrentDirectoryW(uint nBufferLength, SafeHandle lpBuffer);

		// Token: 0x0600006E RID: 110
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetFileAttributesEx(string name, int fileInfoLevel, ref Win32Native.WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

		// Token: 0x0600006F RID: 111
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetFileAttributes(string name, int attr);

		// Token: 0x06000070 RID: 112
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetLogicalDrives();

		// Token: 0x06000071 RID: 113
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern uint GetTempFileName(string tmpPath, string prefix, uint uniqueIdOrZero, [Out] StringBuilder tmpFileName);

		// Token: 0x06000072 RID: 114
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool MoveFile(string src, string dst);

		// Token: 0x06000073 RID: 115
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DeleteVolumeMountPoint(string mountPoint);

		// Token: 0x06000074 RID: 116
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool RemoveDirectory(string path);

		// Token: 0x06000075 RID: 117
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetCurrentDirectory(string path);

		// Token: 0x06000076 RID: 118
		[DllImport("kernel32.dll", EntryPoint = "SetErrorMode", ExactSpelling = true)]
		private static extern int SetErrorMode_VistaAndOlder(int newMode);

		// Token: 0x06000077 RID: 119
		[DllImport("kernel32.dll", EntryPoint = "SetThreadErrorMode", SetLastError = true)]
		private static extern bool SetErrorMode_Win7AndNewer(int newMode, out int oldMode);

		// Token: 0x06000078 RID: 120 RVA: 0x0000237C File Offset: 0x0000057C
		internal static int SetErrorMode(int newMode)
		{
			if (Environment.OSVersion.Version >= Win32Native.ThreadErrorModeMinOsVersion)
			{
				int result;
				Win32Native.SetErrorMode_Win7AndNewer(newMode, out result);
				return result;
			}
			return Win32Native.SetErrorMode_VistaAndOlder(newMode);
		}

		// Token: 0x06000079 RID: 121
		[DllImport("kernel32.dll")]
		internal unsafe static extern int WideCharToMultiByte(uint cp, uint flags, char* pwzSource, int cchSource, byte* pbDestBuffer, int cbDestBuffer, IntPtr null1, IntPtr null2);

		// Token: 0x0600007A RID: 122
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCtrlHandler(Win32Native.ConsoleCtrlHandlerRoutine handler, bool addOrRemove);

		// Token: 0x0600007B RID: 123
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetEnvironmentVariable(string lpName, string lpValue);

		// Token: 0x0600007C RID: 124
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int GetEnvironmentVariable(string lpName, [Out] StringBuilder lpValue, int size);

		// Token: 0x0600007D RID: 125
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern char* GetEnvironmentStrings();

		// Token: 0x0600007E RID: 126
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern bool FreeEnvironmentStrings(char* pStrings);

		// Token: 0x0600007F RID: 127
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern uint GetCurrentProcessId();

		// Token: 0x06000080 RID: 128
		[DllImport("advapi32.dll", CharSet = CharSet.Auto)]
		internal static extern bool GetUserName([Out] StringBuilder lpBuffer, ref int nSize);

		// Token: 0x06000081 RID: 129
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int GetComputerName([Out] StringBuilder nameBuffer, ref int bufferSize);

		// Token: 0x06000082 RID: 130
		[DllImport("ole32.dll")]
		internal static extern int CoCreateGuid(out Guid guid);

		// Token: 0x06000083 RID: 131
		[DllImport("ole32.dll")]
		internal static extern IntPtr CoTaskMemAlloc(UIntPtr cb);

		// Token: 0x06000084 RID: 132
		[DllImport("ole32.dll")]
		internal static extern IntPtr CoTaskMemRealloc(IntPtr pv, UIntPtr cb);

		// Token: 0x06000085 RID: 133
		[DllImport("ole32.dll")]
		internal static extern void CoTaskMemFree(IntPtr ptr);

		// Token: 0x06000086 RID: 134
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

		// Token: 0x06000087 RID: 135
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int mode);

		// Token: 0x06000088 RID: 136
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool Beep(int frequency, int duration);

		// Token: 0x06000089 RID: 137
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out Win32Native.CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

		// Token: 0x0600008A RID: 138
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, Win32Native.COORD size);

		// Token: 0x0600008B RID: 139
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern Win32Native.COORD GetLargestConsoleWindowSize(IntPtr hConsoleOutput);

		// Token: 0x0600008C RID: 140
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool FillConsoleOutputCharacter(IntPtr hConsoleOutput, char character, int nLength, Win32Native.COORD dwWriteCoord, out int pNumCharsWritten);

		// Token: 0x0600008D RID: 141
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FillConsoleOutputAttribute(IntPtr hConsoleOutput, short wColorAttribute, int numCells, Win32Native.COORD startCoord, out int pNumBytesWritten);

		// Token: 0x0600008E RID: 142
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool SetConsoleWindowInfo(IntPtr hConsoleOutput, bool absolute, Win32Native.SMALL_RECT* consoleWindow);

		// Token: 0x0600008F RID: 143
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, short attributes);

		// Token: 0x06000090 RID: 144
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput, Win32Native.COORD cursorPosition);

		// Token: 0x06000091 RID: 145
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetConsoleCursorInfo(IntPtr hConsoleOutput, out Win32Native.CONSOLE_CURSOR_INFO cci);

		// Token: 0x06000092 RID: 146
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, ref Win32Native.CONSOLE_CURSOR_INFO cci);

		// Token: 0x06000093 RID: 147
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetConsoleTitle(string title);

		// Token: 0x06000094 RID: 148
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ReadConsoleInput(IntPtr hConsoleInput, out Win32Native.InputRecord buffer, int numInputRecords_UseOne, out int numEventsRead);

		// Token: 0x06000095 RID: 149
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool PeekConsoleInput(IntPtr hConsoleInput, out Win32Native.InputRecord buffer, int numInputRecords_UseOne, out int numEventsRead);

		// Token: 0x06000096 RID: 150
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool ReadConsoleOutput(IntPtr hConsoleOutput, Win32Native.CHAR_INFO* pBuffer, Win32Native.COORD bufferSize, Win32Native.COORD bufferCoord, ref Win32Native.SMALL_RECT readRegion);

		// Token: 0x06000097 RID: 151
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool ReadConsoleW(SafeFileHandle hConsoleInput, byte* lpBuffer, int nNumberOfCharsToRead, out int lpNumberOfCharsRead, IntPtr pInputControl);

		// Token: 0x06000098 RID: 152
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, Win32Native.CHAR_INFO* buffer, Win32Native.COORD bufferSize, Win32Native.COORD bufferCoord, ref Win32Native.SMALL_RECT writeRegion);

		// Token: 0x06000099 RID: 153
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool WriteConsoleW(SafeFileHandle hConsoleOutput, byte* lpBuffer, int nNumberOfCharsToWrite, out int lpNumberOfCharsWritten, IntPtr lpReservedMustBeNull);

		// Token: 0x0600009A RID: 154
		[DllImport("user32.dll")]
		internal static extern short GetKeyState(int virtualKeyCode);

		// Token: 0x0600009B RID: 155
		[DllImport("kernel32.dll")]
		internal static extern uint GetConsoleCP();

		// Token: 0x0600009C RID: 156
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCP(uint codePage);

		// Token: 0x0600009D RID: 157
		[DllImport("kernel32.dll")]
		internal static extern uint GetConsoleOutputCP();

		// Token: 0x0600009E RID: 158
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleOutputCP(uint codePage);

		// Token: 0x0600009F RID: 159
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegConnectRegistry(string machineName, SafeRegistryHandle key, out SafeRegistryHandle result);

		// Token: 0x060000A0 RID: 160
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegCreateKeyEx(SafeRegistryHandle hKey, string lpSubKey, int Reserved, string lpClass, int dwOptions, int samDesired, Win32Native.SECURITY_ATTRIBUTES lpSecurityAttributes, out SafeRegistryHandle hkResult, out int lpdwDisposition);

		// Token: 0x060000A1 RID: 161
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegDeleteKey(SafeRegistryHandle hKey, string lpSubKey);

		// Token: 0x060000A2 RID: 162
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegDeleteKeyEx(SafeRegistryHandle hKey, string lpSubKey, int samDesired, int Reserved);

		// Token: 0x060000A3 RID: 163
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegDeleteValue(SafeRegistryHandle hKey, string lpValueName);

		// Token: 0x060000A4 RID: 164
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal unsafe static extern int RegEnumKeyEx(SafeRegistryHandle hKey, int dwIndex, char* lpName, ref int lpcbName, int[] lpReserved, [Out] StringBuilder lpClass, int[] lpcbClass, long[] lpftLastWriteTime);

		// Token: 0x060000A5 RID: 165
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal unsafe static extern int RegEnumValue(SafeRegistryHandle hKey, int dwIndex, char* lpValueName, ref int lpcbValueName, IntPtr lpReserved_MustBeZero, int[] lpType, byte[] lpData, int[] lpcbData);

		// Token: 0x060000A6 RID: 166
		[DllImport("advapi32.dll")]
		internal static extern int RegFlushKey(SafeRegistryHandle hKey);

		// Token: 0x060000A7 RID: 167
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegOpenKeyEx(SafeRegistryHandle hKey, string lpSubKey, int ulOptions, int samDesired, out SafeRegistryHandle hkResult);

		// Token: 0x060000A8 RID: 168
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegOpenKeyEx(IntPtr hKey, string lpSubKey, int ulOptions, int samDesired, out SafeRegistryHandle hkResult);

		// Token: 0x060000A9 RID: 169
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegQueryInfoKey(SafeRegistryHandle hKey, [Out] StringBuilder lpClass, int[] lpcbClass, IntPtr lpReserved_MustBeZero, ref int lpcSubKeys, int[] lpcbMaxSubKeyLen, int[] lpcbMaxClassLen, ref int lpcValues, int[] lpcbMaxValueNameLen, int[] lpcbMaxValueLen, int[] lpcbSecurityDescriptor, int[] lpftLastWriteTime);

		// Token: 0x060000AA RID: 170
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] byte[] lpData, ref int lpcbData);

		// Token: 0x060000AB RID: 171
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, ref int lpData, ref int lpcbData);

		// Token: 0x060000AC RID: 172
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, ref long lpData, ref int lpcbData);

		// Token: 0x060000AD RID: 173
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] char[] lpData, ref int lpcbData);

		// Token: 0x060000AE RID: 174
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] StringBuilder lpData, ref int lpcbData);

		// Token: 0x060000AF RID: 175
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, byte[] lpData, int cbData);

		// Token: 0x060000B0 RID: 176
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, ref int lpData, int cbData);

		// Token: 0x060000B1 RID: 177
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, ref long lpData, int cbData);

		// Token: 0x060000B2 RID: 178
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, string lpData, int cbData);

		// Token: 0x060000B3 RID: 179
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int ExpandEnvironmentStrings(string lpSrc, [Out] StringBuilder lpDst, int nSize);

		// Token: 0x060000B4 RID: 180
		[DllImport("kernel32.dll")]
		internal static extern IntPtr LocalReAlloc(IntPtr handle, IntPtr sizetcbBytes, int uFlags);

		// Token: 0x060000B5 RID: 181
		[DllImport("shell32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, [Out] StringBuilder lpszPath);

		// Token: 0x060000B6 RID: 182
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern byte GetUserNameEx(int format, [Out] StringBuilder domainName, ref uint domainNameLen);

		// Token: 0x060000B7 RID: 183
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool LookupAccountName(string machineName, string accountName, byte[] sid, ref int sidLen, [Out] StringBuilder domainName, ref uint domainNameLen, out int peUse);

		// Token: 0x060000B8 RID: 184
		[DllImport("user32.dll", ExactSpelling = true)]
		internal static extern IntPtr GetProcessWindowStation();

		// Token: 0x060000B9 RID: 185
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetUserObjectInformation(IntPtr hObj, int nIndex, [MarshalAs(UnmanagedType.LPStruct)] Win32Native.USEROBJECTFLAGS pvBuffer, int nLength, ref int lpnLengthNeeded);

		// Token: 0x060000BA RID: 186
		[DllImport("user32.dll", BestFitMapping = false, SetLastError = true)]
		internal static extern IntPtr SendMessageTimeout(IntPtr hWnd, int Msg, IntPtr wParam, string lParam, uint fuFlags, uint uTimeout, IntPtr lpdwResult);

		// Token: 0x060000BB RID: 187
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int SystemFunction040([In] [Out] SafeBSTRHandle pDataIn, [In] uint cbDataIn, [In] uint dwFlags);

		// Token: 0x060000BC RID: 188
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int SystemFunction041([In] [Out] SafeBSTRHandle pDataIn, [In] uint cbDataIn, [In] uint dwFlags);

		// Token: 0x060000BD RID: 189
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int LsaNtStatusToWinError([In] int status);

		// Token: 0x060000BE RID: 190
		[DllImport("bcrypt.dll")]
		internal static extern uint BCryptGetFipsAlgorithmMode([MarshalAs(UnmanagedType.U1)] out bool pfEnabled);

		// Token: 0x060000BF RID: 191
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool AdjustTokenPrivileges([In] SafeAccessTokenHandle TokenHandle, [In] bool DisableAllPrivileges, [In] ref Win32Native.TOKEN_PRIVILEGE NewState, [In] uint BufferLength, [In] [Out] ref Win32Native.TOKEN_PRIVILEGE PreviousState, [In] [Out] ref uint ReturnLength);

		// Token: 0x060000C0 RID: 192
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool AllocateLocallyUniqueId([In] [Out] ref Win32Native.LUID Luid);

		// Token: 0x060000C1 RID: 193
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CheckTokenMembership([In] SafeAccessTokenHandle TokenHandle, [In] byte[] SidToCheck, [In] [Out] ref bool IsMember);

		// Token: 0x060000C2 RID: 194
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "ConvertSecurityDescriptorToStringSecurityDescriptorW", ExactSpelling = true, SetLastError = true)]
		internal static extern int ConvertSdToStringSd(byte[] securityDescriptor, uint requestedRevision, uint securityInformation, out IntPtr resultString, ref uint resultStringLength);

		// Token: 0x060000C3 RID: 195
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "ConvertStringSecurityDescriptorToSecurityDescriptorW", ExactSpelling = true, SetLastError = true)]
		internal static extern int ConvertStringSdToSd(string stringSd, uint stringSdRevision, out IntPtr resultSd, ref uint resultSdLength);

		// Token: 0x060000C4 RID: 196
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "ConvertStringSidToSidW", ExactSpelling = true, SetLastError = true)]
		internal static extern int ConvertStringSidToSid(string stringSid, out IntPtr ByteArray);

		// Token: 0x060000C5 RID: 197
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "ConvertSidToStringSidW", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ConvertSidToStringSid(IntPtr Sid, ref IntPtr StringSid);

		// Token: 0x060000C6 RID: 198
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern int CreateWellKnownSid(int sidType, byte[] domainSid, [Out] byte[] resultSid, ref uint resultSidLength);

		// Token: 0x060000C7 RID: 199
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DuplicateHandle([In] IntPtr hSourceProcessHandle, [In] IntPtr hSourceHandle, [In] IntPtr hTargetProcessHandle, [In] [Out] ref SafeAccessTokenHandle lpTargetHandle, [In] uint dwDesiredAccess, [In] bool bInheritHandle, [In] uint dwOptions);

		// Token: 0x060000C8 RID: 200
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DuplicateHandle([In] IntPtr hSourceProcessHandle, [In] SafeAccessTokenHandle hSourceHandle, [In] IntPtr hTargetProcessHandle, [In] [Out] ref SafeAccessTokenHandle lpTargetHandle, [In] uint dwDesiredAccess, [In] bool bInheritHandle, [In] uint dwOptions);

		// Token: 0x060000C9 RID: 201
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DuplicateTokenEx([In] SafeAccessTokenHandle ExistingTokenHandle, [In] TokenAccessLevels DesiredAccess, [In] IntPtr TokenAttributes, [In] Win32Native.SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, [In] System.Security.Principal.TokenType TokenType, [In] [Out] ref SafeAccessTokenHandle DuplicateTokenHandle);

		// Token: 0x060000CA RID: 202
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DuplicateTokenEx([In] SafeAccessTokenHandle hExistingToken, [In] uint dwDesiredAccess, [In] IntPtr lpTokenAttributes, [In] uint ImpersonationLevel, [In] uint TokenType, [In] [Out] ref SafeAccessTokenHandle phNewToken);

		// Token: 0x060000CB RID: 203
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "EqualDomainSid", ExactSpelling = true, SetLastError = true)]
		internal static extern int IsEqualDomainSid(byte[] sid1, byte[] sid2, out bool result);

		// Token: 0x060000CC RID: 204
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x060000CD RID: 205
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetCurrentThread();

		// Token: 0x060000CE RID: 206
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetSecurityDescriptorLength(IntPtr byteArray);

		// Token: 0x060000CF RID: 207
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetSecurityInfo", ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetSecurityInfoByHandle(SafeHandle handle, uint objectType, uint securityInformation, out IntPtr sidOwner, out IntPtr sidGroup, out IntPtr dacl, out IntPtr sacl, out IntPtr securityDescriptor);

		// Token: 0x060000D0 RID: 208
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetNamedSecurityInfoW", ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetSecurityInfoByName(string name, uint objectType, uint securityInformation, out IntPtr sidOwner, out IntPtr sidGroup, out IntPtr dacl, out IntPtr sacl, out IntPtr securityDescriptor);

		// Token: 0x060000D1 RID: 209
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetTokenInformation([In] IntPtr TokenHandle, [In] uint TokenInformationClass, [In] SafeLocalAllocHandle TokenInformation, [In] uint TokenInformationLength, out uint ReturnLength);

		// Token: 0x060000D2 RID: 210
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetTokenInformation([In] SafeAccessTokenHandle TokenHandle, [In] uint TokenInformationClass, [In] SafeLocalAllocHandle TokenInformation, [In] uint TokenInformationLength, out uint ReturnLength);

		// Token: 0x060000D3 RID: 211
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern int GetWindowsAccountDomainSid(byte[] sid, [Out] byte[] resultSid, ref uint resultSidLength);

		// Token: 0x060000D4 RID: 212
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern int IsWellKnownSid(byte[] sid, int type);

		// Token: 0x060000D5 RID: 213
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint LsaOpenPolicy(string systemName, ref Win32Native.LSA_OBJECT_ATTRIBUTES attributes, int accessMask, out SafeLsaPolicyHandle handle);

		// Token: 0x060000D6 RID: 214
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "LookupPrivilegeValueW", ExactSpelling = true, SetLastError = true)]
		internal static extern bool LookupPrivilegeValue([In] string lpSystemName, [In] string lpName, [In] [Out] ref Win32Native.LUID Luid);

		// Token: 0x060000D7 RID: 215
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint LsaLookupSids(SafeLsaPolicyHandle handle, int count, IntPtr[] sids, ref SafeLsaMemoryHandle referencedDomains, ref SafeLsaMemoryHandle names);

		// Token: 0x060000D8 RID: 216
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern int LsaFreeMemory(IntPtr handle);

		// Token: 0x060000D9 RID: 217
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint LsaLookupNames(SafeLsaPolicyHandle handle, int count, Win32Native.UNICODE_STRING[] names, ref SafeLsaMemoryHandle referencedDomains, ref SafeLsaMemoryHandle sids);

		// Token: 0x060000DA RID: 218
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint LsaLookupNames2(SafeLsaPolicyHandle handle, int flags, int count, Win32Native.UNICODE_STRING[] names, ref SafeLsaMemoryHandle referencedDomains, ref SafeLsaMemoryHandle sids);

		// Token: 0x060000DB RID: 219
		[DllImport("secur32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int LsaConnectUntrusted([In] [Out] ref SafeLsaLogonProcessHandle LsaHandle);

		// Token: 0x060000DC RID: 220
		[DllImport("secur32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int LsaGetLogonSessionData([In] ref Win32Native.LUID LogonId, [In] [Out] ref SafeLsaReturnBufferHandle ppLogonSessionData);

		// Token: 0x060000DD RID: 221
		[DllImport("secur32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int LsaLogonUser([In] SafeLsaLogonProcessHandle LsaHandle, [In] ref Win32Native.UNICODE_INTPTR_STRING OriginName, [In] uint LogonType, [In] uint AuthenticationPackage, [In] IntPtr AuthenticationInformation, [In] uint AuthenticationInformationLength, [In] IntPtr LocalGroups, [In] ref Win32Native.TOKEN_SOURCE SourceContext, [In] [Out] ref SafeLsaReturnBufferHandle ProfileBuffer, [In] [Out] ref uint ProfileBufferLength, [In] [Out] ref Win32Native.LUID LogonId, [In] [Out] ref SafeAccessTokenHandle Token, [In] [Out] ref Win32Native.QUOTA_LIMITS Quotas, [In] [Out] ref int SubStatus);

		// Token: 0x060000DE RID: 222
		[DllImport("secur32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int LsaLookupAuthenticationPackage([In] SafeLsaLogonProcessHandle LsaHandle, [In] ref Win32Native.UNICODE_INTPTR_STRING PackageName, [In] [Out] ref uint AuthenticationPackage);

		// Token: 0x060000DF RID: 223
		[DllImport("secur32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int LsaRegisterLogonProcess([In] ref Win32Native.UNICODE_INTPTR_STRING LogonProcessName, [In] [Out] ref SafeLsaLogonProcessHandle LsaHandle, [In] [Out] ref IntPtr SecurityMode);

		// Token: 0x060000E0 RID: 224
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll", SetLastError = true)]
		internal static extern int LsaDeregisterLogonProcess(IntPtr handle);

		// Token: 0x060000E1 RID: 225
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern int LsaClose(IntPtr handle);

		// Token: 0x060000E2 RID: 226
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll", SetLastError = true)]
		internal static extern int LsaFreeReturnBuffer(IntPtr handle);

		// Token: 0x060000E3 RID: 227
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool OpenProcessToken([In] IntPtr ProcessToken, [In] TokenAccessLevels DesiredAccess, out SafeAccessTokenHandle TokenHandle);

		// Token: 0x060000E4 RID: 228
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetNamedSecurityInfoW", ExactSpelling = true, SetLastError = true)]
		internal static extern uint SetSecurityInfoByName(string name, uint objectType, uint securityInformation, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x060000E5 RID: 229
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetSecurityInfo", ExactSpelling = true, SetLastError = true)]
		internal static extern uint SetSecurityInfoByHandle(SafeHandle handle, uint objectType, uint securityInformation, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x060000E6 RID: 230
		[DllImport("clr.dll", CharSet = CharSet.Unicode)]
		internal static extern int CreateAssemblyNameObject(out IAssemblyName ppEnum, string szAssemblyName, uint dwFlags, IntPtr pvReserved);

		// Token: 0x060000E7 RID: 231
		[DllImport("clr.dll", CharSet = CharSet.Auto)]
		internal static extern int CreateAssemblyEnum(out IAssemblyEnum ppEnum, IApplicationContext pAppCtx, IAssemblyName pName, uint dwFlags, IntPtr pvReserved);

		// Token: 0x060000E8 RID: 232
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryUnbiasedInterruptTime(out ulong UnbiasedTime);

		// Token: 0x060000E9 RID: 233
		[DllImport("ntdll.dll")]
		internal static extern int RtlGetVersion(out Win32Native.RTL_OSVERSIONINFOEX lpVersionInformation);

		// Token: 0x060000EA RID: 234
		[DllImport("kernel32.dll")]
		internal static extern void GetNativeSystemInfo(out Win32Native.SYSTEM_INFO lpSystemInfo);

		// Token: 0x0400006D RID: 109
		internal const int KEY_QUERY_VALUE = 1;

		// Token: 0x0400006E RID: 110
		internal const int KEY_SET_VALUE = 2;

		// Token: 0x0400006F RID: 111
		internal const int KEY_CREATE_SUB_KEY = 4;

		// Token: 0x04000070 RID: 112
		internal const int KEY_ENUMERATE_SUB_KEYS = 8;

		// Token: 0x04000071 RID: 113
		internal const int KEY_NOTIFY = 16;

		// Token: 0x04000072 RID: 114
		internal const int KEY_CREATE_LINK = 32;

		// Token: 0x04000073 RID: 115
		internal const int KEY_READ = 131097;

		// Token: 0x04000074 RID: 116
		internal const int KEY_WRITE = 131078;

		// Token: 0x04000075 RID: 117
		internal const int KEY_WOW64_64KEY = 256;

		// Token: 0x04000076 RID: 118
		internal const int KEY_WOW64_32KEY = 512;

		// Token: 0x04000077 RID: 119
		internal const int REG_OPTION_NON_VOLATILE = 0;

		// Token: 0x04000078 RID: 120
		internal const int REG_OPTION_VOLATILE = 1;

		// Token: 0x04000079 RID: 121
		internal const int REG_OPTION_CREATE_LINK = 2;

		// Token: 0x0400007A RID: 122
		internal const int REG_OPTION_BACKUP_RESTORE = 4;

		// Token: 0x0400007B RID: 123
		internal const int REG_NONE = 0;

		// Token: 0x0400007C RID: 124
		internal const int REG_SZ = 1;

		// Token: 0x0400007D RID: 125
		internal const int REG_EXPAND_SZ = 2;

		// Token: 0x0400007E RID: 126
		internal const int REG_BINARY = 3;

		// Token: 0x0400007F RID: 127
		internal const int REG_DWORD = 4;

		// Token: 0x04000080 RID: 128
		internal const int REG_DWORD_LITTLE_ENDIAN = 4;

		// Token: 0x04000081 RID: 129
		internal const int REG_DWORD_BIG_ENDIAN = 5;

		// Token: 0x04000082 RID: 130
		internal const int REG_LINK = 6;

		// Token: 0x04000083 RID: 131
		internal const int REG_MULTI_SZ = 7;

		// Token: 0x04000084 RID: 132
		internal const int REG_RESOURCE_LIST = 8;

		// Token: 0x04000085 RID: 133
		internal const int REG_FULL_RESOURCE_DESCRIPTOR = 9;

		// Token: 0x04000086 RID: 134
		internal const int REG_RESOURCE_REQUIREMENTS_LIST = 10;

		// Token: 0x04000087 RID: 135
		internal const int REG_QWORD = 11;

		// Token: 0x04000088 RID: 136
		internal const int HWND_BROADCAST = 65535;

		// Token: 0x04000089 RID: 137
		internal const int WM_SETTINGCHANGE = 26;

		// Token: 0x0400008A RID: 138
		internal const uint CRYPTPROTECTMEMORY_BLOCK_SIZE = 16U;

		// Token: 0x0400008B RID: 139
		internal const uint CRYPTPROTECTMEMORY_SAME_PROCESS = 0U;

		// Token: 0x0400008C RID: 140
		internal const uint CRYPTPROTECTMEMORY_CROSS_PROCESS = 1U;

		// Token: 0x0400008D RID: 141
		internal const uint CRYPTPROTECTMEMORY_SAME_LOGON = 2U;

		// Token: 0x0400008E RID: 142
		internal const int SECURITY_ANONYMOUS = 0;

		// Token: 0x0400008F RID: 143
		internal const int SECURITY_SQOS_PRESENT = 1048576;

		// Token: 0x04000090 RID: 144
		internal const string MICROSOFT_KERBEROS_NAME = "Kerberos";

		// Token: 0x04000091 RID: 145
		internal const uint ANONYMOUS_LOGON_LUID = 998U;

		// Token: 0x04000092 RID: 146
		internal const int SECURITY_ANONYMOUS_LOGON_RID = 7;

		// Token: 0x04000093 RID: 147
		internal const int SECURITY_AUTHENTICATED_USER_RID = 11;

		// Token: 0x04000094 RID: 148
		internal const int SECURITY_LOCAL_SYSTEM_RID = 18;

		// Token: 0x04000095 RID: 149
		internal const int SECURITY_BUILTIN_DOMAIN_RID = 32;

		// Token: 0x04000096 RID: 150
		internal const uint SE_PRIVILEGE_DISABLED = 0U;

		// Token: 0x04000097 RID: 151
		internal const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 1U;

		// Token: 0x04000098 RID: 152
		internal const uint SE_PRIVILEGE_ENABLED = 2U;

		// Token: 0x04000099 RID: 153
		internal const uint SE_PRIVILEGE_USED_FOR_ACCESS = 2147483648U;

		// Token: 0x0400009A RID: 154
		internal const uint SE_GROUP_MANDATORY = 1U;

		// Token: 0x0400009B RID: 155
		internal const uint SE_GROUP_ENABLED_BY_DEFAULT = 2U;

		// Token: 0x0400009C RID: 156
		internal const uint SE_GROUP_ENABLED = 4U;

		// Token: 0x0400009D RID: 157
		internal const uint SE_GROUP_OWNER = 8U;

		// Token: 0x0400009E RID: 158
		internal const uint SE_GROUP_USE_FOR_DENY_ONLY = 16U;

		// Token: 0x0400009F RID: 159
		internal const uint SE_GROUP_LOGON_ID = 3221225472U;

		// Token: 0x040000A0 RID: 160
		internal const uint SE_GROUP_RESOURCE = 536870912U;

		// Token: 0x040000A1 RID: 161
		internal const uint DUPLICATE_CLOSE_SOURCE = 1U;

		// Token: 0x040000A2 RID: 162
		internal const uint DUPLICATE_SAME_ACCESS = 2U;

		// Token: 0x040000A3 RID: 163
		internal const uint DUPLICATE_SAME_ATTRIBUTES = 4U;

		// Token: 0x040000A4 RID: 164
		internal const int TIME_ZONE_ID_INVALID = -1;

		// Token: 0x040000A5 RID: 165
		internal const int TIME_ZONE_ID_UNKNOWN = 0;

		// Token: 0x040000A6 RID: 166
		internal const int TIME_ZONE_ID_STANDARD = 1;

		// Token: 0x040000A7 RID: 167
		internal const int TIME_ZONE_ID_DAYLIGHT = 2;

		// Token: 0x040000A8 RID: 168
		internal const int MAX_PATH = 260;

		// Token: 0x040000A9 RID: 169
		internal const int MUI_LANGUAGE_ID = 4;

		// Token: 0x040000AA RID: 170
		internal const int MUI_LANGUAGE_NAME = 8;

		// Token: 0x040000AB RID: 171
		internal const int MUI_PREFERRED_UI_LANGUAGES = 16;

		// Token: 0x040000AC RID: 172
		internal const int MUI_INSTALLED_LANGUAGES = 32;

		// Token: 0x040000AD RID: 173
		internal const int MUI_ALL_LANGUAGES = 64;

		// Token: 0x040000AE RID: 174
		internal const int MUI_LANG_NEUTRAL_PE_FILE = 256;

		// Token: 0x040000AF RID: 175
		internal const int MUI_NON_LANG_NEUTRAL_FILE = 512;

		// Token: 0x040000B0 RID: 176
		internal const int LOAD_LIBRARY_AS_DATAFILE = 2;

		// Token: 0x040000B1 RID: 177
		internal const int LOAD_STRING_MAX_LENGTH = 500;

		// Token: 0x040000B2 RID: 178
		internal const int READ_CONTROL = 131072;

		// Token: 0x040000B3 RID: 179
		internal const int SYNCHRONIZE = 1048576;

		// Token: 0x040000B4 RID: 180
		internal const int STANDARD_RIGHTS_READ = 131072;

		// Token: 0x040000B5 RID: 181
		internal const int STANDARD_RIGHTS_WRITE = 131072;

		// Token: 0x040000B6 RID: 182
		internal const int SEMAPHORE_MODIFY_STATE = 2;

		// Token: 0x040000B7 RID: 183
		internal const int EVENT_MODIFY_STATE = 2;

		// Token: 0x040000B8 RID: 184
		internal const int MUTEX_MODIFY_STATE = 1;

		// Token: 0x040000B9 RID: 185
		internal const int MUTEX_ALL_ACCESS = 2031617;

		// Token: 0x040000BA RID: 186
		internal const int LMEM_FIXED = 0;

		// Token: 0x040000BB RID: 187
		internal const int LMEM_ZEROINIT = 64;

		// Token: 0x040000BC RID: 188
		internal const int LPTR = 64;

		// Token: 0x040000BD RID: 189
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x040000BE RID: 190
		internal const string USER32 = "user32.dll";

		// Token: 0x040000BF RID: 191
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x040000C0 RID: 192
		internal const string OLE32 = "ole32.dll";

		// Token: 0x040000C1 RID: 193
		internal const string OLEAUT32 = "oleaut32.dll";

		// Token: 0x040000C2 RID: 194
		internal const string SHELL32 = "shell32.dll";

		// Token: 0x040000C3 RID: 195
		internal const string SHIM = "mscoree.dll";

		// Token: 0x040000C4 RID: 196
		internal const string CRYPT32 = "crypt32.dll";

		// Token: 0x040000C5 RID: 197
		internal const string SECUR32 = "secur32.dll";

		// Token: 0x040000C6 RID: 198
		internal const string NTDLL = "ntdll.dll";

		// Token: 0x040000C7 RID: 199
		internal const string MSCORWKS = "clr.dll";

		// Token: 0x040000C8 RID: 200
		internal const int SEM_FAILCRITICALERRORS = 1;

		// Token: 0x040000C9 RID: 201
		internal const int FIND_STARTSWITH = 1048576;

		// Token: 0x040000CA RID: 202
		internal const int FIND_ENDSWITH = 2097152;

		// Token: 0x040000CB RID: 203
		internal const int FIND_FROMSTART = 4194304;

		// Token: 0x040000CC RID: 204
		internal const int FIND_FROMEND = 8388608;

		// Token: 0x040000CD RID: 205
		internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		// Token: 0x040000CE RID: 206
		internal const int STD_INPUT_HANDLE = -10;

		// Token: 0x040000CF RID: 207
		internal const int STD_OUTPUT_HANDLE = -11;

		// Token: 0x040000D0 RID: 208
		internal const int STD_ERROR_HANDLE = -12;

		// Token: 0x040000D1 RID: 209
		internal const int CTRL_C_EVENT = 0;

		// Token: 0x040000D2 RID: 210
		internal const int CTRL_BREAK_EVENT = 1;

		// Token: 0x040000D3 RID: 211
		internal const int CTRL_CLOSE_EVENT = 2;

		// Token: 0x040000D4 RID: 212
		internal const int CTRL_LOGOFF_EVENT = 5;

		// Token: 0x040000D5 RID: 213
		internal const int CTRL_SHUTDOWN_EVENT = 6;

		// Token: 0x040000D6 RID: 214
		internal const short KEY_EVENT = 1;

		// Token: 0x040000D7 RID: 215
		internal const int FILE_TYPE_DISK = 1;

		// Token: 0x040000D8 RID: 216
		internal const int FILE_TYPE_CHAR = 2;

		// Token: 0x040000D9 RID: 217
		internal const int FILE_TYPE_PIPE = 3;

		// Token: 0x040000DA RID: 218
		internal const int REPLACEFILE_WRITE_THROUGH = 1;

		// Token: 0x040000DB RID: 219
		internal const int REPLACEFILE_IGNORE_MERGE_ERRORS = 2;

		// Token: 0x040000DC RID: 220
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x040000DD RID: 221
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x040000DE RID: 222
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		// Token: 0x040000DF RID: 223
		internal const uint FILE_MAP_WRITE = 2U;

		// Token: 0x040000E0 RID: 224
		internal const uint FILE_MAP_READ = 4U;

		// Token: 0x040000E1 RID: 225
		internal const int FILE_ATTRIBUTE_READONLY = 1;

		// Token: 0x040000E2 RID: 226
		internal const int FILE_ATTRIBUTE_DIRECTORY = 16;

		// Token: 0x040000E3 RID: 227
		internal const int FILE_ATTRIBUTE_REPARSE_POINT = 1024;

		// Token: 0x040000E4 RID: 228
		internal const int IO_REPARSE_TAG_MOUNT_POINT = -1610612733;

		// Token: 0x040000E5 RID: 229
		internal const int PAGE_READWRITE = 4;

		// Token: 0x040000E6 RID: 230
		internal const int MEM_COMMIT = 4096;

		// Token: 0x040000E7 RID: 231
		internal const int MEM_RESERVE = 8192;

		// Token: 0x040000E8 RID: 232
		internal const int MEM_RELEASE = 32768;

		// Token: 0x040000E9 RID: 233
		internal const int MEM_FREE = 65536;

		// Token: 0x040000EA RID: 234
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x040000EB RID: 235
		internal const int ERROR_INVALID_FUNCTION = 1;

		// Token: 0x040000EC RID: 236
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x040000ED RID: 237
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x040000EE RID: 238
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x040000EF RID: 239
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x040000F0 RID: 240
		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x040000F1 RID: 241
		internal const int ERROR_INVALID_DATA = 13;

		// Token: 0x040000F2 RID: 242
		internal const int ERROR_INVALID_DRIVE = 15;

		// Token: 0x040000F3 RID: 243
		internal const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x040000F4 RID: 244
		internal const int ERROR_NOT_READY = 21;

		// Token: 0x040000F5 RID: 245
		internal const int ERROR_BAD_LENGTH = 24;

		// Token: 0x040000F6 RID: 246
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x040000F7 RID: 247
		internal const int ERROR_NOT_SUPPORTED = 50;

		// Token: 0x040000F8 RID: 248
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x040000F9 RID: 249
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x040000FA RID: 250
		internal const int ERROR_BROKEN_PIPE = 109;

		// Token: 0x040000FB RID: 251
		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		// Token: 0x040000FC RID: 252
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x040000FD RID: 253
		internal const int ERROR_INVALID_NAME = 123;

		// Token: 0x040000FE RID: 254
		internal const int ERROR_BAD_PATHNAME = 161;

		// Token: 0x040000FF RID: 255
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000100 RID: 256
		internal const int ERROR_ENVVAR_NOT_FOUND = 203;

		// Token: 0x04000101 RID: 257
		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04000102 RID: 258
		internal const int ERROR_NO_DATA = 232;

		// Token: 0x04000103 RID: 259
		internal const int ERROR_PIPE_NOT_CONNECTED = 233;

		// Token: 0x04000104 RID: 260
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x04000105 RID: 261
		internal const int ERROR_DIRECTORY = 267;

		// Token: 0x04000106 RID: 262
		internal const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x04000107 RID: 263
		internal const int ERROR_NOT_FOUND = 1168;

		// Token: 0x04000108 RID: 264
		internal const int ERROR_NO_TOKEN = 1008;

		// Token: 0x04000109 RID: 265
		internal const int ERROR_DLL_INIT_FAILED = 1114;

		// Token: 0x0400010A RID: 266
		internal const int ERROR_NON_ACCOUNT_SID = 1257;

		// Token: 0x0400010B RID: 267
		internal const int ERROR_NOT_ALL_ASSIGNED = 1300;

		// Token: 0x0400010C RID: 268
		internal const int ERROR_UNKNOWN_REVISION = 1305;

		// Token: 0x0400010D RID: 269
		internal const int ERROR_INVALID_OWNER = 1307;

		// Token: 0x0400010E RID: 270
		internal const int ERROR_INVALID_PRIMARY_GROUP = 1308;

		// Token: 0x0400010F RID: 271
		internal const int ERROR_NO_SUCH_PRIVILEGE = 1313;

		// Token: 0x04000110 RID: 272
		internal const int ERROR_PRIVILEGE_NOT_HELD = 1314;

		// Token: 0x04000111 RID: 273
		internal const int ERROR_NONE_MAPPED = 1332;

		// Token: 0x04000112 RID: 274
		internal const int ERROR_INVALID_ACL = 1336;

		// Token: 0x04000113 RID: 275
		internal const int ERROR_INVALID_SID = 1337;

		// Token: 0x04000114 RID: 276
		internal const int ERROR_INVALID_SECURITY_DESCR = 1338;

		// Token: 0x04000115 RID: 277
		internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;

		// Token: 0x04000116 RID: 278
		internal const int ERROR_CANT_OPEN_ANONYMOUS = 1347;

		// Token: 0x04000117 RID: 279
		internal const int ERROR_NO_SECURITY_ON_OBJECT = 1350;

		// Token: 0x04000118 RID: 280
		internal const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		// Token: 0x04000119 RID: 281
		internal const uint STATUS_SUCCESS = 0U;

		// Token: 0x0400011A RID: 282
		internal const uint STATUS_SOME_NOT_MAPPED = 263U;

		// Token: 0x0400011B RID: 283
		internal const uint STATUS_NO_MEMORY = 3221225495U;

		// Token: 0x0400011C RID: 284
		internal const uint STATUS_OBJECT_NAME_NOT_FOUND = 3221225524U;

		// Token: 0x0400011D RID: 285
		internal const uint STATUS_NONE_MAPPED = 3221225587U;

		// Token: 0x0400011E RID: 286
		internal const uint STATUS_INSUFFICIENT_RESOURCES = 3221225626U;

		// Token: 0x0400011F RID: 287
		internal const uint STATUS_ACCESS_DENIED = 3221225506U;

		// Token: 0x04000120 RID: 288
		internal const int INVALID_FILE_SIZE = -1;

		// Token: 0x04000121 RID: 289
		internal const int STATUS_ACCOUNT_RESTRICTION = -1073741714;

		// Token: 0x04000122 RID: 290
		private static readonly Version ThreadErrorModeMinOsVersion = new Version(6, 1, 7600);

		// Token: 0x04000123 RID: 291
		internal const int LCID_SUPPORTED = 2;

		// Token: 0x04000124 RID: 292
		internal const int ENABLE_PROCESSED_INPUT = 1;

		// Token: 0x04000125 RID: 293
		internal const int ENABLE_LINE_INPUT = 2;

		// Token: 0x04000126 RID: 294
		internal const int ENABLE_ECHO_INPUT = 4;

		// Token: 0x04000127 RID: 295
		internal const int SHGFP_TYPE_CURRENT = 0;

		// Token: 0x04000128 RID: 296
		internal const int UOI_FLAGS = 1;

		// Token: 0x04000129 RID: 297
		internal const int WSF_VISIBLE = 1;

		// Token: 0x0400012A RID: 298
		internal const int CSIDL_FLAG_CREATE = 32768;

		// Token: 0x0400012B RID: 299
		internal const int CSIDL_FLAG_DONT_VERIFY = 16384;

		// Token: 0x0400012C RID: 300
		internal const int CSIDL_ADMINTOOLS = 48;

		// Token: 0x0400012D RID: 301
		internal const int CSIDL_CDBURN_AREA = 59;

		// Token: 0x0400012E RID: 302
		internal const int CSIDL_COMMON_ADMINTOOLS = 47;

		// Token: 0x0400012F RID: 303
		internal const int CSIDL_COMMON_DOCUMENTS = 46;

		// Token: 0x04000130 RID: 304
		internal const int CSIDL_COMMON_MUSIC = 53;

		// Token: 0x04000131 RID: 305
		internal const int CSIDL_COMMON_OEM_LINKS = 58;

		// Token: 0x04000132 RID: 306
		internal const int CSIDL_COMMON_PICTURES = 54;

		// Token: 0x04000133 RID: 307
		internal const int CSIDL_COMMON_STARTMENU = 22;

		// Token: 0x04000134 RID: 308
		internal const int CSIDL_COMMON_PROGRAMS = 23;

		// Token: 0x04000135 RID: 309
		internal const int CSIDL_COMMON_STARTUP = 24;

		// Token: 0x04000136 RID: 310
		internal const int CSIDL_COMMON_DESKTOPDIRECTORY = 25;

		// Token: 0x04000137 RID: 311
		internal const int CSIDL_COMMON_TEMPLATES = 45;

		// Token: 0x04000138 RID: 312
		internal const int CSIDL_COMMON_VIDEO = 55;

		// Token: 0x04000139 RID: 313
		internal const int CSIDL_FONTS = 20;

		// Token: 0x0400013A RID: 314
		internal const int CSIDL_MYVIDEO = 14;

		// Token: 0x0400013B RID: 315
		internal const int CSIDL_NETHOOD = 19;

		// Token: 0x0400013C RID: 316
		internal const int CSIDL_PRINTHOOD = 27;

		// Token: 0x0400013D RID: 317
		internal const int CSIDL_PROFILE = 40;

		// Token: 0x0400013E RID: 318
		internal const int CSIDL_PROGRAM_FILES_COMMONX86 = 44;

		// Token: 0x0400013F RID: 319
		internal const int CSIDL_PROGRAM_FILESX86 = 42;

		// Token: 0x04000140 RID: 320
		internal const int CSIDL_RESOURCES = 56;

		// Token: 0x04000141 RID: 321
		internal const int CSIDL_RESOURCES_LOCALIZED = 57;

		// Token: 0x04000142 RID: 322
		internal const int CSIDL_SYSTEMX86 = 41;

		// Token: 0x04000143 RID: 323
		internal const int CSIDL_WINDOWS = 36;

		// Token: 0x04000144 RID: 324
		internal const int CSIDL_APPDATA = 26;

		// Token: 0x04000145 RID: 325
		internal const int CSIDL_COMMON_APPDATA = 35;

		// Token: 0x04000146 RID: 326
		internal const int CSIDL_LOCAL_APPDATA = 28;

		// Token: 0x04000147 RID: 327
		internal const int CSIDL_COOKIES = 33;

		// Token: 0x04000148 RID: 328
		internal const int CSIDL_FAVORITES = 6;

		// Token: 0x04000149 RID: 329
		internal const int CSIDL_HISTORY = 34;

		// Token: 0x0400014A RID: 330
		internal const int CSIDL_INTERNET_CACHE = 32;

		// Token: 0x0400014B RID: 331
		internal const int CSIDL_PROGRAMS = 2;

		// Token: 0x0400014C RID: 332
		internal const int CSIDL_RECENT = 8;

		// Token: 0x0400014D RID: 333
		internal const int CSIDL_SENDTO = 9;

		// Token: 0x0400014E RID: 334
		internal const int CSIDL_STARTMENU = 11;

		// Token: 0x0400014F RID: 335
		internal const int CSIDL_STARTUP = 7;

		// Token: 0x04000150 RID: 336
		internal const int CSIDL_SYSTEM = 37;

		// Token: 0x04000151 RID: 337
		internal const int CSIDL_TEMPLATES = 21;

		// Token: 0x04000152 RID: 338
		internal const int CSIDL_DESKTOPDIRECTORY = 16;

		// Token: 0x04000153 RID: 339
		internal const int CSIDL_PERSONAL = 5;

		// Token: 0x04000154 RID: 340
		internal const int CSIDL_PROGRAM_FILES = 38;

		// Token: 0x04000155 RID: 341
		internal const int CSIDL_PROGRAM_FILES_COMMON = 43;

		// Token: 0x04000156 RID: 342
		internal const int CSIDL_DESKTOP = 0;

		// Token: 0x04000157 RID: 343
		internal const int CSIDL_DRIVES = 17;

		// Token: 0x04000158 RID: 344
		internal const int CSIDL_MYMUSIC = 13;

		// Token: 0x04000159 RID: 345
		internal const int CSIDL_MYPICTURES = 39;

		// Token: 0x0400015A RID: 346
		internal const int NameSamCompatible = 2;

		// Token: 0x0400015B RID: 347
		internal const int CLAIM_SECURITY_ATTRIBUTE_TYPE_INVALID = 0;

		// Token: 0x0400015C RID: 348
		internal const int CLAIM_SECURITY_ATTRIBUTE_TYPE_INT64 = 1;

		// Token: 0x0400015D RID: 349
		internal const int CLAIM_SECURITY_ATTRIBUTE_TYPE_UINT64 = 2;

		// Token: 0x0400015E RID: 350
		internal const int CLAIM_SECURITY_ATTRIBUTE_TYPE_STRING = 3;

		// Token: 0x0400015F RID: 351
		internal const int CLAIM_SECURITY_ATTRIBUTE_TYPE_FQBN = 4;

		// Token: 0x04000160 RID: 352
		internal const int CLAIM_SECURITY_ATTRIBUTE_TYPE_SID = 5;

		// Token: 0x04000161 RID: 353
		internal const int CLAIM_SECURITY_ATTRIBUTE_TYPE_BOOLEAN = 6;

		// Token: 0x04000162 RID: 354
		internal const int CLAIM_SECURITY_ATTRIBUTE_TYPE_OCTET_STRING = 16;

		// Token: 0x04000163 RID: 355
		internal const int CLAIM_SECURITY_ATTRIBUTE_NON_INHERITABLE = 1;

		// Token: 0x04000164 RID: 356
		internal const int CLAIM_SECURITY_ATTRIBUTE_VALUE_CASE_SENSITIVE = 2;

		// Token: 0x04000165 RID: 357
		internal const int CLAIM_SECURITY_ATTRIBUTE_USE_FOR_DENY_ONLY = 4;

		// Token: 0x04000166 RID: 358
		internal const int CLAIM_SECURITY_ATTRIBUTE_DISABLED_BY_DEFAULT = 8;

		// Token: 0x04000167 RID: 359
		internal const int CLAIM_SECURITY_ATTRIBUTE_DISABLED = 16;

		// Token: 0x04000168 RID: 360
		internal const int CLAIM_SECURITY_ATTRIBUTE_MANDATORY = 32;

		// Token: 0x04000169 RID: 361
		internal const int CLAIM_SECURITY_ATTRIBUTE_VALID_FLAGS = 63;

		// Token: 0x02000A5B RID: 2651
		internal struct SystemTime
		{
			// Token: 0x04002EDD RID: 11997
			[MarshalAs(UnmanagedType.U2)]
			public short Year;

			// Token: 0x04002EDE RID: 11998
			[MarshalAs(UnmanagedType.U2)]
			public short Month;

			// Token: 0x04002EDF RID: 11999
			[MarshalAs(UnmanagedType.U2)]
			public short DayOfWeek;

			// Token: 0x04002EE0 RID: 12000
			[MarshalAs(UnmanagedType.U2)]
			public short Day;

			// Token: 0x04002EE1 RID: 12001
			[MarshalAs(UnmanagedType.U2)]
			public short Hour;

			// Token: 0x04002EE2 RID: 12002
			[MarshalAs(UnmanagedType.U2)]
			public short Minute;

			// Token: 0x04002EE3 RID: 12003
			[MarshalAs(UnmanagedType.U2)]
			public short Second;

			// Token: 0x04002EE4 RID: 12004
			[MarshalAs(UnmanagedType.U2)]
			public short Milliseconds;
		}

		// Token: 0x02000A5C RID: 2652
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TimeZoneInformation
		{
			// Token: 0x06006861 RID: 26721 RVA: 0x00166A00 File Offset: 0x00164C00
			public TimeZoneInformation(Win32Native.DynamicTimeZoneInformation dtzi)
			{
				this.Bias = dtzi.Bias;
				this.StandardName = dtzi.StandardName;
				this.StandardDate = dtzi.StandardDate;
				this.StandardBias = dtzi.StandardBias;
				this.DaylightName = dtzi.DaylightName;
				this.DaylightDate = dtzi.DaylightDate;
				this.DaylightBias = dtzi.DaylightBias;
			}

			// Token: 0x04002EE5 RID: 12005
			[MarshalAs(UnmanagedType.I4)]
			public int Bias;

			// Token: 0x04002EE6 RID: 12006
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string StandardName;

			// Token: 0x04002EE7 RID: 12007
			public Win32Native.SystemTime StandardDate;

			// Token: 0x04002EE8 RID: 12008
			[MarshalAs(UnmanagedType.I4)]
			public int StandardBias;

			// Token: 0x04002EE9 RID: 12009
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DaylightName;

			// Token: 0x04002EEA RID: 12010
			public Win32Native.SystemTime DaylightDate;

			// Token: 0x04002EEB RID: 12011
			[MarshalAs(UnmanagedType.I4)]
			public int DaylightBias;
		}

		// Token: 0x02000A5D RID: 2653
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DynamicTimeZoneInformation
		{
			// Token: 0x04002EEC RID: 12012
			[MarshalAs(UnmanagedType.I4)]
			public int Bias;

			// Token: 0x04002EED RID: 12013
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string StandardName;

			// Token: 0x04002EEE RID: 12014
			public Win32Native.SystemTime StandardDate;

			// Token: 0x04002EEF RID: 12015
			[MarshalAs(UnmanagedType.I4)]
			public int StandardBias;

			// Token: 0x04002EF0 RID: 12016
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string DaylightName;

			// Token: 0x04002EF1 RID: 12017
			public Win32Native.SystemTime DaylightDate;

			// Token: 0x04002EF2 RID: 12018
			[MarshalAs(UnmanagedType.I4)]
			public int DaylightBias;

			// Token: 0x04002EF3 RID: 12019
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string TimeZoneKeyName;

			// Token: 0x04002EF4 RID: 12020
			[MarshalAs(UnmanagedType.Bool)]
			public bool DynamicDaylightTimeDisabled;
		}

		// Token: 0x02000A5E RID: 2654
		internal struct RegistryTimeZoneInformation
		{
			// Token: 0x06006862 RID: 26722 RVA: 0x00166A61 File Offset: 0x00164C61
			public RegistryTimeZoneInformation(Win32Native.TimeZoneInformation tzi)
			{
				this.Bias = tzi.Bias;
				this.StandardDate = tzi.StandardDate;
				this.StandardBias = tzi.StandardBias;
				this.DaylightDate = tzi.DaylightDate;
				this.DaylightBias = tzi.DaylightBias;
			}

			// Token: 0x06006863 RID: 26723 RVA: 0x00166AA0 File Offset: 0x00164CA0
			public RegistryTimeZoneInformation(byte[] bytes)
			{
				if (bytes == null || bytes.Length != 44)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidREG_TZI_FORMAT"), "bytes");
				}
				this.Bias = BitConverter.ToInt32(bytes, 0);
				this.StandardBias = BitConverter.ToInt32(bytes, 4);
				this.DaylightBias = BitConverter.ToInt32(bytes, 8);
				this.StandardDate.Year = BitConverter.ToInt16(bytes, 12);
				this.StandardDate.Month = BitConverter.ToInt16(bytes, 14);
				this.StandardDate.DayOfWeek = BitConverter.ToInt16(bytes, 16);
				this.StandardDate.Day = BitConverter.ToInt16(bytes, 18);
				this.StandardDate.Hour = BitConverter.ToInt16(bytes, 20);
				this.StandardDate.Minute = BitConverter.ToInt16(bytes, 22);
				this.StandardDate.Second = BitConverter.ToInt16(bytes, 24);
				this.StandardDate.Milliseconds = BitConverter.ToInt16(bytes, 26);
				this.DaylightDate.Year = BitConverter.ToInt16(bytes, 28);
				this.DaylightDate.Month = BitConverter.ToInt16(bytes, 30);
				this.DaylightDate.DayOfWeek = BitConverter.ToInt16(bytes, 32);
				this.DaylightDate.Day = BitConverter.ToInt16(bytes, 34);
				this.DaylightDate.Hour = BitConverter.ToInt16(bytes, 36);
				this.DaylightDate.Minute = BitConverter.ToInt16(bytes, 38);
				this.DaylightDate.Second = BitConverter.ToInt16(bytes, 40);
				this.DaylightDate.Milliseconds = BitConverter.ToInt16(bytes, 42);
			}

			// Token: 0x04002EF5 RID: 12021
			[MarshalAs(UnmanagedType.I4)]
			public int Bias;

			// Token: 0x04002EF6 RID: 12022
			[MarshalAs(UnmanagedType.I4)]
			public int StandardBias;

			// Token: 0x04002EF7 RID: 12023
			[MarshalAs(UnmanagedType.I4)]
			public int DaylightBias;

			// Token: 0x04002EF8 RID: 12024
			public Win32Native.SystemTime StandardDate;

			// Token: 0x04002EF9 RID: 12025
			public Win32Native.SystemTime DaylightDate;
		}

		// Token: 0x02000A5F RID: 2655
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal class OSVERSIONINFO
		{
			// Token: 0x06006864 RID: 26724 RVA: 0x00166C23 File Offset: 0x00164E23
			internal OSVERSIONINFO()
			{
				this.OSVersionInfoSize = Marshal.SizeOf<Win32Native.OSVERSIONINFO>(this);
			}

			// Token: 0x04002EFA RID: 12026
			internal int OSVersionInfoSize;

			// Token: 0x04002EFB RID: 12027
			internal int MajorVersion;

			// Token: 0x04002EFC RID: 12028
			internal int MinorVersion;

			// Token: 0x04002EFD RID: 12029
			internal int BuildNumber;

			// Token: 0x04002EFE RID: 12030
			internal int PlatformId;

			// Token: 0x04002EFF RID: 12031
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string CSDVersion;
		}

		// Token: 0x02000A60 RID: 2656
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal class OSVERSIONINFOEX
		{
			// Token: 0x06006865 RID: 26725 RVA: 0x00166C37 File Offset: 0x00164E37
			public OSVERSIONINFOEX()
			{
				this.OSVersionInfoSize = Marshal.SizeOf<Win32Native.OSVERSIONINFOEX>(this);
			}

			// Token: 0x04002F00 RID: 12032
			internal int OSVersionInfoSize;

			// Token: 0x04002F01 RID: 12033
			internal int MajorVersion;

			// Token: 0x04002F02 RID: 12034
			internal int MinorVersion;

			// Token: 0x04002F03 RID: 12035
			internal int BuildNumber;

			// Token: 0x04002F04 RID: 12036
			internal int PlatformId;

			// Token: 0x04002F05 RID: 12037
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string CSDVersion;

			// Token: 0x04002F06 RID: 12038
			internal ushort ServicePackMajor;

			// Token: 0x04002F07 RID: 12039
			internal ushort ServicePackMinor;

			// Token: 0x04002F08 RID: 12040
			internal short SuiteMask;

			// Token: 0x04002F09 RID: 12041
			internal byte ProductType;

			// Token: 0x04002F0A RID: 12042
			internal byte Reserved;
		}

		// Token: 0x02000A61 RID: 2657
		internal struct SYSTEM_INFO
		{
			// Token: 0x04002F0B RID: 12043
			internal ushort wProcessorArchitecture;

			// Token: 0x04002F0C RID: 12044
			internal ushort wReserved;

			// Token: 0x04002F0D RID: 12045
			internal int dwPageSize;

			// Token: 0x04002F0E RID: 12046
			internal IntPtr lpMinimumApplicationAddress;

			// Token: 0x04002F0F RID: 12047
			internal IntPtr lpMaximumApplicationAddress;

			// Token: 0x04002F10 RID: 12048
			internal IntPtr dwActiveProcessorMask;

			// Token: 0x04002F11 RID: 12049
			internal int dwNumberOfProcessors;

			// Token: 0x04002F12 RID: 12050
			internal int dwProcessorType;

			// Token: 0x04002F13 RID: 12051
			internal int dwAllocationGranularity;

			// Token: 0x04002F14 RID: 12052
			internal short wProcessorLevel;

			// Token: 0x04002F15 RID: 12053
			internal short wProcessorRevision;
		}

		// Token: 0x02000A62 RID: 2658
		[StructLayout(LayoutKind.Sequential)]
		internal class SECURITY_ATTRIBUTES
		{
			// Token: 0x04002F16 RID: 12054
			internal int nLength;

			// Token: 0x04002F17 RID: 12055
			internal unsafe byte* pSecurityDescriptor = null;

			// Token: 0x04002F18 RID: 12056
			internal int bInheritHandle;
		}

		// Token: 0x02000A63 RID: 2659
		[Serializable]
		internal struct WIN32_FILE_ATTRIBUTE_DATA
		{
			// Token: 0x06006867 RID: 26727 RVA: 0x00166C5C File Offset: 0x00164E5C
			[SecurityCritical]
			internal void PopulateFrom(ref Win32Native.WIN32_FIND_DATA findData)
			{
				this.fileAttributes = findData.dwFileAttributes;
				this.ftCreationTime = findData.ftCreationTime;
				this.ftLastAccessTime = findData.ftLastAccessTime;
				this.ftLastWriteTime = findData.ftLastWriteTime;
				this.fileSizeHigh = findData.nFileSizeHigh;
				this.fileSizeLow = findData.nFileSizeLow;
			}

			// Token: 0x04002F19 RID: 12057
			internal int fileAttributes;

			// Token: 0x04002F1A RID: 12058
			internal Win32Native.FILE_TIME ftCreationTime;

			// Token: 0x04002F1B RID: 12059
			internal Win32Native.FILE_TIME ftLastAccessTime;

			// Token: 0x04002F1C RID: 12060
			internal Win32Native.FILE_TIME ftLastWriteTime;

			// Token: 0x04002F1D RID: 12061
			internal int fileSizeHigh;

			// Token: 0x04002F1E RID: 12062
			internal int fileSizeLow;
		}

		// Token: 0x02000A64 RID: 2660
		internal struct FILE_TIME
		{
			// Token: 0x06006868 RID: 26728 RVA: 0x00166CB1 File Offset: 0x00164EB1
			public FILE_TIME(long fileTime)
			{
				this.ftTimeLow = (uint)fileTime;
				this.ftTimeHigh = (uint)(fileTime >> 32);
			}

			// Token: 0x06006869 RID: 26729 RVA: 0x00166CC6 File Offset: 0x00164EC6
			public long ToTicks()
			{
				return (long)(((ulong)this.ftTimeHigh << 32) + (ulong)this.ftTimeLow);
			}

			// Token: 0x04002F1F RID: 12063
			internal uint ftTimeLow;

			// Token: 0x04002F20 RID: 12064
			internal uint ftTimeHigh;
		}

		// Token: 0x02000A65 RID: 2661
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct KERB_S4U_LOGON
		{
			// Token: 0x04002F21 RID: 12065
			internal uint MessageType;

			// Token: 0x04002F22 RID: 12066
			internal uint Flags;

			// Token: 0x04002F23 RID: 12067
			internal Win32Native.UNICODE_INTPTR_STRING ClientUpn;

			// Token: 0x04002F24 RID: 12068
			internal Win32Native.UNICODE_INTPTR_STRING ClientRealm;
		}

		// Token: 0x02000A66 RID: 2662
		internal struct LSA_OBJECT_ATTRIBUTES
		{
			// Token: 0x04002F25 RID: 12069
			internal int Length;

			// Token: 0x04002F26 RID: 12070
			internal IntPtr RootDirectory;

			// Token: 0x04002F27 RID: 12071
			internal IntPtr ObjectName;

			// Token: 0x04002F28 RID: 12072
			internal int Attributes;

			// Token: 0x04002F29 RID: 12073
			internal IntPtr SecurityDescriptor;

			// Token: 0x04002F2A RID: 12074
			internal IntPtr SecurityQualityOfService;
		}

		// Token: 0x02000A67 RID: 2663
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct UNICODE_STRING
		{
			// Token: 0x04002F2B RID: 12075
			internal ushort Length;

			// Token: 0x04002F2C RID: 12076
			internal ushort MaximumLength;

			// Token: 0x04002F2D RID: 12077
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Buffer;
		}

		// Token: 0x02000A68 RID: 2664
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct UNICODE_INTPTR_STRING
		{
			// Token: 0x0600686A RID: 26730 RVA: 0x00166CDA File Offset: 0x00164EDA
			[SecurityCritical]
			internal UNICODE_INTPTR_STRING(int stringBytes, SafeLocalAllocHandle buffer)
			{
				this.Length = (ushort)stringBytes;
				this.MaxLength = (ushort)buffer.ByteLength;
				this.Buffer = buffer.DangerousGetHandle();
			}

			// Token: 0x0600686B RID: 26731 RVA: 0x00166CFD File Offset: 0x00164EFD
			internal UNICODE_INTPTR_STRING(int stringBytes, IntPtr buffer)
			{
				this.Length = (ushort)stringBytes;
				this.MaxLength = (ushort)stringBytes;
				this.Buffer = buffer;
			}

			// Token: 0x04002F2E RID: 12078
			internal ushort Length;

			// Token: 0x04002F2F RID: 12079
			internal ushort MaxLength;

			// Token: 0x04002F30 RID: 12080
			internal IntPtr Buffer;
		}

		// Token: 0x02000A69 RID: 2665
		internal struct LSA_TRANSLATED_NAME
		{
			// Token: 0x04002F31 RID: 12081
			internal int Use;

			// Token: 0x04002F32 RID: 12082
			internal Win32Native.UNICODE_INTPTR_STRING Name;

			// Token: 0x04002F33 RID: 12083
			internal int DomainIndex;
		}

		// Token: 0x02000A6A RID: 2666
		internal struct LSA_TRANSLATED_SID
		{
			// Token: 0x04002F34 RID: 12084
			internal int Use;

			// Token: 0x04002F35 RID: 12085
			internal uint Rid;

			// Token: 0x04002F36 RID: 12086
			internal int DomainIndex;
		}

		// Token: 0x02000A6B RID: 2667
		internal struct LSA_TRANSLATED_SID2
		{
			// Token: 0x04002F37 RID: 12087
			internal int Use;

			// Token: 0x04002F38 RID: 12088
			internal IntPtr Sid;

			// Token: 0x04002F39 RID: 12089
			internal int DomainIndex;

			// Token: 0x04002F3A RID: 12090
			private uint Flags;
		}

		// Token: 0x02000A6C RID: 2668
		internal struct LSA_TRUST_INFORMATION
		{
			// Token: 0x04002F3B RID: 12091
			internal Win32Native.UNICODE_INTPTR_STRING Name;

			// Token: 0x04002F3C RID: 12092
			internal IntPtr Sid;
		}

		// Token: 0x02000A6D RID: 2669
		internal struct LSA_REFERENCED_DOMAIN_LIST
		{
			// Token: 0x04002F3D RID: 12093
			internal int Entries;

			// Token: 0x04002F3E RID: 12094
			internal IntPtr Domains;
		}

		// Token: 0x02000A6E RID: 2670
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct LUID
		{
			// Token: 0x04002F3F RID: 12095
			internal uint LowPart;

			// Token: 0x04002F40 RID: 12096
			internal uint HighPart;
		}

		// Token: 0x02000A6F RID: 2671
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct LUID_AND_ATTRIBUTES
		{
			// Token: 0x04002F41 RID: 12097
			internal Win32Native.LUID Luid;

			// Token: 0x04002F42 RID: 12098
			internal uint Attributes;
		}

		// Token: 0x02000A70 RID: 2672
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct QUOTA_LIMITS
		{
			// Token: 0x04002F43 RID: 12099
			internal IntPtr PagedPoolLimit;

			// Token: 0x04002F44 RID: 12100
			internal IntPtr NonPagedPoolLimit;

			// Token: 0x04002F45 RID: 12101
			internal IntPtr MinimumWorkingSetSize;

			// Token: 0x04002F46 RID: 12102
			internal IntPtr MaximumWorkingSetSize;

			// Token: 0x04002F47 RID: 12103
			internal IntPtr PagefileLimit;

			// Token: 0x04002F48 RID: 12104
			internal IntPtr TimeLimit;
		}

		// Token: 0x02000A71 RID: 2673
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SECURITY_LOGON_SESSION_DATA
		{
			// Token: 0x04002F49 RID: 12105
			internal uint Size;

			// Token: 0x04002F4A RID: 12106
			internal Win32Native.LUID LogonId;

			// Token: 0x04002F4B RID: 12107
			internal Win32Native.UNICODE_INTPTR_STRING UserName;

			// Token: 0x04002F4C RID: 12108
			internal Win32Native.UNICODE_INTPTR_STRING LogonDomain;

			// Token: 0x04002F4D RID: 12109
			internal Win32Native.UNICODE_INTPTR_STRING AuthenticationPackage;

			// Token: 0x04002F4E RID: 12110
			internal uint LogonType;

			// Token: 0x04002F4F RID: 12111
			internal uint Session;

			// Token: 0x04002F50 RID: 12112
			internal IntPtr Sid;

			// Token: 0x04002F51 RID: 12113
			internal long LogonTime;
		}

		// Token: 0x02000A72 RID: 2674
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SID_AND_ATTRIBUTES
		{
			// Token: 0x04002F52 RID: 12114
			internal IntPtr Sid;

			// Token: 0x04002F53 RID: 12115
			internal uint Attributes;

			// Token: 0x04002F54 RID: 12116
			internal static readonly long SizeOf = (long)Marshal.SizeOf(typeof(Win32Native.SID_AND_ATTRIBUTES));
		}

		// Token: 0x02000A73 RID: 2675
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TOKEN_GROUPS
		{
			// Token: 0x04002F55 RID: 12117
			internal uint GroupCount;

			// Token: 0x04002F56 RID: 12118
			internal Win32Native.SID_AND_ATTRIBUTES Groups;
		}

		// Token: 0x02000A74 RID: 2676
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TOKEN_PRIMARY_GROUP
		{
			// Token: 0x04002F57 RID: 12119
			internal IntPtr PrimaryGroup;
		}

		// Token: 0x02000A75 RID: 2677
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TOKEN_PRIVILEGE
		{
			// Token: 0x04002F58 RID: 12120
			internal uint PrivilegeCount;

			// Token: 0x04002F59 RID: 12121
			internal Win32Native.LUID_AND_ATTRIBUTES Privilege;
		}

		// Token: 0x02000A76 RID: 2678
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TOKEN_SOURCE
		{
			// Token: 0x04002F5A RID: 12122
			private const int TOKEN_SOURCE_LENGTH = 8;

			// Token: 0x04002F5B RID: 12123
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			internal char[] Name;

			// Token: 0x04002F5C RID: 12124
			internal Win32Native.LUID SourceIdentifier;
		}

		// Token: 0x02000A77 RID: 2679
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TOKEN_STATISTICS
		{
			// Token: 0x04002F5D RID: 12125
			internal Win32Native.LUID TokenId;

			// Token: 0x04002F5E RID: 12126
			internal Win32Native.LUID AuthenticationId;

			// Token: 0x04002F5F RID: 12127
			internal long ExpirationTime;

			// Token: 0x04002F60 RID: 12128
			internal uint TokenType;

			// Token: 0x04002F61 RID: 12129
			internal uint ImpersonationLevel;

			// Token: 0x04002F62 RID: 12130
			internal uint DynamicCharged;

			// Token: 0x04002F63 RID: 12131
			internal uint DynamicAvailable;

			// Token: 0x04002F64 RID: 12132
			internal uint GroupCount;

			// Token: 0x04002F65 RID: 12133
			internal uint PrivilegeCount;

			// Token: 0x04002F66 RID: 12134
			internal Win32Native.LUID ModifiedId;
		}

		// Token: 0x02000A78 RID: 2680
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TOKEN_USER
		{
			// Token: 0x04002F67 RID: 12135
			internal Win32Native.SID_AND_ATTRIBUTES User;
		}

		// Token: 0x02000A79 RID: 2681
		internal struct MEMORYSTATUSEX
		{
			// Token: 0x04002F68 RID: 12136
			internal int length;

			// Token: 0x04002F69 RID: 12137
			internal int memoryLoad;

			// Token: 0x04002F6A RID: 12138
			internal ulong totalPhys;

			// Token: 0x04002F6B RID: 12139
			internal ulong availPhys;

			// Token: 0x04002F6C RID: 12140
			internal ulong totalPageFile;

			// Token: 0x04002F6D RID: 12141
			internal ulong availPageFile;

			// Token: 0x04002F6E RID: 12142
			internal ulong totalVirtual;

			// Token: 0x04002F6F RID: 12143
			internal ulong availVirtual;

			// Token: 0x04002F70 RID: 12144
			internal ulong availExtendedVirtual;
		}

		// Token: 0x02000A7A RID: 2682
		internal struct MEMORY_BASIC_INFORMATION
		{
			// Token: 0x04002F71 RID: 12145
			internal unsafe void* BaseAddress;

			// Token: 0x04002F72 RID: 12146
			internal unsafe void* AllocationBase;

			// Token: 0x04002F73 RID: 12147
			internal uint AllocationProtect;

			// Token: 0x04002F74 RID: 12148
			internal UIntPtr RegionSize;

			// Token: 0x04002F75 RID: 12149
			internal uint State;

			// Token: 0x04002F76 RID: 12150
			internal uint Protect;

			// Token: 0x04002F77 RID: 12151
			internal uint Type;
		}

		// Token: 0x02000A7B RID: 2683
		internal struct NlsVersionInfoEx
		{
			// Token: 0x04002F78 RID: 12152
			internal int dwNLSVersionInfoSize;

			// Token: 0x04002F79 RID: 12153
			internal int dwNLSVersion;

			// Token: 0x04002F7A RID: 12154
			internal int dwDefinedVersion;

			// Token: 0x04002F7B RID: 12155
			internal int dwEffectiveId;

			// Token: 0x04002F7C RID: 12156
			internal Guid guidCustomVersion;
		}

		// Token: 0x02000A7C RID: 2684
		[BestFitMapping(false)]
		[Serializable]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WIN32_FIND_DATA
		{
			// Token: 0x170011CC RID: 4556
			// (get) Token: 0x0600686D RID: 26733 RVA: 0x00166D30 File Offset: 0x00164F30
			internal unsafe string cFileName
			{
				get
				{
					fixed (char* ptr = &this._cFileName.FixedElementField)
					{
						return new string(ptr);
					}
				}
			}

			// Token: 0x170011CD RID: 4557
			// (get) Token: 0x0600686E RID: 26734 RVA: 0x00166D50 File Offset: 0x00164F50
			internal unsafe bool IsRelativeDirectory
			{
				get
				{
					fixed (char* ptr = &this._cFileName.FixedElementField)
					{
						char c = *ptr;
						if (c != '.')
						{
							return false;
						}
						char c2 = ptr[1];
						return c2 == '\0' || (c2 == '.' && ptr[2] == '\0');
					}
				}
			}

			// Token: 0x170011CE RID: 4558
			// (get) Token: 0x0600686F RID: 26735 RVA: 0x00166D91 File Offset: 0x00164F91
			internal bool IsFile
			{
				get
				{
					return (this.dwFileAttributes & 16) == 0;
				}
			}

			// Token: 0x170011CF RID: 4559
			// (get) Token: 0x06006870 RID: 26736 RVA: 0x00166D9F File Offset: 0x00164F9F
			internal bool IsNormalDirectory
			{
				get
				{
					return (this.dwFileAttributes & 16) != 0 && !this.IsRelativeDirectory;
				}
			}

			// Token: 0x04002F7D RID: 12157
			internal int dwFileAttributes;

			// Token: 0x04002F7E RID: 12158
			internal Win32Native.FILE_TIME ftCreationTime;

			// Token: 0x04002F7F RID: 12159
			internal Win32Native.FILE_TIME ftLastAccessTime;

			// Token: 0x04002F80 RID: 12160
			internal Win32Native.FILE_TIME ftLastWriteTime;

			// Token: 0x04002F81 RID: 12161
			internal int nFileSizeHigh;

			// Token: 0x04002F82 RID: 12162
			internal int nFileSizeLow;

			// Token: 0x04002F83 RID: 12163
			internal int dwReserved0;

			// Token: 0x04002F84 RID: 12164
			internal int dwReserved1;

			// Token: 0x04002F85 RID: 12165
			[FixedBuffer(typeof(char), 260)]
			private Win32Native.WIN32_FIND_DATA.<_cFileName>e__FixedBuffer _cFileName;

			// Token: 0x04002F86 RID: 12166
			[FixedBuffer(typeof(char), 14)]
			private Win32Native.WIN32_FIND_DATA.<_cAlternateFileName>e__FixedBuffer _cAlternateFileName;

			// Token: 0x02000CB9 RID: 3257
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 520)]
			public struct <_cFileName>e__FixedBuffer
			{
				// Token: 0x04003807 RID: 14343
				public char FixedElementField;
			}

			// Token: 0x02000CBA RID: 3258
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 28)]
			public struct <_cAlternateFileName>e__FixedBuffer
			{
				// Token: 0x04003808 RID: 14344
				public char FixedElementField;
			}
		}

		// Token: 0x02000A7D RID: 2685
		// (Invoke) Token: 0x06006872 RID: 26738
		internal delegate bool ConsoleCtrlHandlerRoutine(int controlType);

		// Token: 0x02000A7E RID: 2686
		internal struct COORD
		{
			// Token: 0x04002F87 RID: 12167
			internal short X;

			// Token: 0x04002F88 RID: 12168
			internal short Y;
		}

		// Token: 0x02000A7F RID: 2687
		internal struct SMALL_RECT
		{
			// Token: 0x04002F89 RID: 12169
			internal short Left;

			// Token: 0x04002F8A RID: 12170
			internal short Top;

			// Token: 0x04002F8B RID: 12171
			internal short Right;

			// Token: 0x04002F8C RID: 12172
			internal short Bottom;
		}

		// Token: 0x02000A80 RID: 2688
		internal struct CONSOLE_SCREEN_BUFFER_INFO
		{
			// Token: 0x04002F8D RID: 12173
			internal Win32Native.COORD dwSize;

			// Token: 0x04002F8E RID: 12174
			internal Win32Native.COORD dwCursorPosition;

			// Token: 0x04002F8F RID: 12175
			internal short wAttributes;

			// Token: 0x04002F90 RID: 12176
			internal Win32Native.SMALL_RECT srWindow;

			// Token: 0x04002F91 RID: 12177
			internal Win32Native.COORD dwMaximumWindowSize;
		}

		// Token: 0x02000A81 RID: 2689
		internal struct CONSOLE_CURSOR_INFO
		{
			// Token: 0x04002F92 RID: 12178
			internal int dwSize;

			// Token: 0x04002F93 RID: 12179
			internal bool bVisible;
		}

		// Token: 0x02000A82 RID: 2690
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct KeyEventRecord
		{
			// Token: 0x04002F94 RID: 12180
			internal bool keyDown;

			// Token: 0x04002F95 RID: 12181
			internal short repeatCount;

			// Token: 0x04002F96 RID: 12182
			internal short virtualKeyCode;

			// Token: 0x04002F97 RID: 12183
			internal short virtualScanCode;

			// Token: 0x04002F98 RID: 12184
			internal char uChar;

			// Token: 0x04002F99 RID: 12185
			internal int controlKeyState;
		}

		// Token: 0x02000A83 RID: 2691
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct InputRecord
		{
			// Token: 0x04002F9A RID: 12186
			internal short eventType;

			// Token: 0x04002F9B RID: 12187
			internal Win32Native.KeyEventRecord keyEvent;
		}

		// Token: 0x02000A84 RID: 2692
		[Flags]
		[Serializable]
		internal enum Color : short
		{
			// Token: 0x04002F9D RID: 12189
			Black = 0,
			// Token: 0x04002F9E RID: 12190
			ForegroundBlue = 1,
			// Token: 0x04002F9F RID: 12191
			ForegroundGreen = 2,
			// Token: 0x04002FA0 RID: 12192
			ForegroundRed = 4,
			// Token: 0x04002FA1 RID: 12193
			ForegroundYellow = 6,
			// Token: 0x04002FA2 RID: 12194
			ForegroundIntensity = 8,
			// Token: 0x04002FA3 RID: 12195
			BackgroundBlue = 16,
			// Token: 0x04002FA4 RID: 12196
			BackgroundGreen = 32,
			// Token: 0x04002FA5 RID: 12197
			BackgroundRed = 64,
			// Token: 0x04002FA6 RID: 12198
			BackgroundYellow = 96,
			// Token: 0x04002FA7 RID: 12199
			BackgroundIntensity = 128,
			// Token: 0x04002FA8 RID: 12200
			ForegroundMask = 15,
			// Token: 0x04002FA9 RID: 12201
			BackgroundMask = 240,
			// Token: 0x04002FAA RID: 12202
			ColorMask = 255
		}

		// Token: 0x02000A85 RID: 2693
		internal struct CHAR_INFO
		{
			// Token: 0x04002FAB RID: 12203
			private ushort charData;

			// Token: 0x04002FAC RID: 12204
			private short attributes;
		}

		// Token: 0x02000A86 RID: 2694
		[StructLayout(LayoutKind.Sequential)]
		internal class USEROBJECTFLAGS
		{
			// Token: 0x04002FAD RID: 12205
			internal int fInherit;

			// Token: 0x04002FAE RID: 12206
			internal int fReserved;

			// Token: 0x04002FAF RID: 12207
			internal int dwFlags;
		}

		// Token: 0x02000A87 RID: 2695
		internal enum SECURITY_IMPERSONATION_LEVEL
		{
			// Token: 0x04002FB1 RID: 12209
			Anonymous,
			// Token: 0x04002FB2 RID: 12210
			Identification,
			// Token: 0x04002FB3 RID: 12211
			Impersonation,
			// Token: 0x04002FB4 RID: 12212
			Delegation
		}

		// Token: 0x02000A88 RID: 2696
		[StructLayout(LayoutKind.Explicit)]
		internal struct CLAIM_SECURITY_ATTRIBUTE_INFORMATION_V1
		{
			// Token: 0x04002FB5 RID: 12213
			[FieldOffset(0)]
			public IntPtr pAttributeV1;
		}

		// Token: 0x02000A89 RID: 2697
		internal struct CLAIM_SECURITY_ATTRIBUTES_INFORMATION
		{
			// Token: 0x04002FB6 RID: 12214
			public ushort Version;

			// Token: 0x04002FB7 RID: 12215
			public ushort Reserved;

			// Token: 0x04002FB8 RID: 12216
			public uint AttributeCount;

			// Token: 0x04002FB9 RID: 12217
			public Win32Native.CLAIM_SECURITY_ATTRIBUTE_INFORMATION_V1 Attribute;
		}

		// Token: 0x02000A8A RID: 2698
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CLAIM_SECURITY_ATTRIBUTE_FQBN_VALUE
		{
			// Token: 0x04002FBA RID: 12218
			public ulong Version;

			// Token: 0x04002FBB RID: 12219
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Name;
		}

		// Token: 0x02000A8B RID: 2699
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CLAIM_SECURITY_ATTRIBUTE_OCTET_STRING_VALUE
		{
			// Token: 0x04002FBC RID: 12220
			public IntPtr pValue;

			// Token: 0x04002FBD RID: 12221
			public uint ValueLength;
		}

		// Token: 0x02000A8C RID: 2700
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		internal struct CLAIM_VALUES_ATTRIBUTE_V1
		{
			// Token: 0x04002FBE RID: 12222
			[FieldOffset(0)]
			public IntPtr pInt64;

			// Token: 0x04002FBF RID: 12223
			[FieldOffset(0)]
			public IntPtr pUint64;

			// Token: 0x04002FC0 RID: 12224
			[FieldOffset(0)]
			public IntPtr ppString;

			// Token: 0x04002FC1 RID: 12225
			[FieldOffset(0)]
			public IntPtr pFqbn;

			// Token: 0x04002FC2 RID: 12226
			[FieldOffset(0)]
			public IntPtr pOctetString;
		}

		// Token: 0x02000A8D RID: 2701
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CLAIM_SECURITY_ATTRIBUTE_V1
		{
			// Token: 0x04002FC3 RID: 12227
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Name;

			// Token: 0x04002FC4 RID: 12228
			public ushort ValueType;

			// Token: 0x04002FC5 RID: 12229
			public ushort Reserved;

			// Token: 0x04002FC6 RID: 12230
			public uint Flags;

			// Token: 0x04002FC7 RID: 12231
			public uint ValueCount;

			// Token: 0x04002FC8 RID: 12232
			public Win32Native.CLAIM_VALUES_ATTRIBUTE_V1 Values;
		}

		// Token: 0x02000A8E RID: 2702
		internal struct RTL_OSVERSIONINFOEX
		{
			// Token: 0x04002FC9 RID: 12233
			internal uint dwOSVersionInfoSize;

			// Token: 0x04002FCA RID: 12234
			internal uint dwMajorVersion;

			// Token: 0x04002FCB RID: 12235
			internal uint dwMinorVersion;

			// Token: 0x04002FCC RID: 12236
			internal uint dwBuildNumber;

			// Token: 0x04002FCD RID: 12237
			internal uint dwPlatformId;

			// Token: 0x04002FCE RID: 12238
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string szCSDVersion;
		}

		// Token: 0x02000A8F RID: 2703
		internal enum ProcessorArchitecture : ushort
		{
			// Token: 0x04002FD0 RID: 12240
			Processor_Architecture_INTEL,
			// Token: 0x04002FD1 RID: 12241
			Processor_Architecture_ARM = 5,
			// Token: 0x04002FD2 RID: 12242
			Processor_Architecture_IA64,
			// Token: 0x04002FD3 RID: 12243
			Processor_Architecture_AMD64 = 9,
			// Token: 0x04002FD4 RID: 12244
			Processor_Architecture_ARM64 = 12,
			// Token: 0x04002FD5 RID: 12245
			Processor_Architecture_UNKNOWN = 65535
		}
	}
}
