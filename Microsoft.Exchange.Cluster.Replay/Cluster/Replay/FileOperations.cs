using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000FA RID: 250
	internal static class FileOperations
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x0002EC84 File Offset: 0x0002CE84
		public static string GetCurrentDateString()
		{
			return DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002ECA3 File Offset: 0x0002CEA3
		public static void RemoveDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				Directory.Delete(path, true);
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
		public static void CreateDirectoryIfNeeded(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002ECC8 File Offset: 0x0002CEC8
		internal static void TruncateFile(string fileName, long fileSize)
		{
			int num;
			using (SafeFileHandle safeFileHandle = LogCopy.LogCopyCreateFile(fileName, FileAccess.Write, FileShare.None, FileMode.Open, FileFlags.FILE_ATTRIBUTE_NORMAL, out num))
			{
				long num2;
				NativeMethods.SetFilePointerEx(safeFileHandle, fileSize, out num2, 0U);
				NativeMethods.SetEndOfFile(safeFileHandle);
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002ED14 File Offset: 0x0002CF14
		internal static NativeMethods.WIN32_FILE_ATTRIBUTE_DATA GetFileAttributesEx(string path, out Exception ex)
		{
			NativeMethods.WIN32_FILE_ATTRIBUTE_DATA result = default(NativeMethods.WIN32_FILE_ATTRIBUTE_DATA);
			ex = null;
			if (!NativeMethods.GetFileAttributesEx(path, NativeMethods.GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out result))
			{
				ex = new Win32Exception();
				return result;
			}
			return result;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0002ED43 File Offset: 0x0002CF43
		internal static uint GetSectorSize(string fileName)
		{
			return 512U;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002ED4C File Offset: 0x0002CF4C
		internal static bool IsDiskFullException(IOException exception)
		{
			uint hrforException = (uint)Marshal.GetHRForException(exception);
			return hrforException == 2147942512U;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002ED6B File Offset: 0x0002CF6B
		internal static void ThrowDiskFullException(string msg)
		{
			throw new IOException(msg, -2147024784);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002ED78 File Offset: 0x0002CF78
		internal static bool ConvertHResultToWin32(int hresult, out int winErrorCode)
		{
			if (((long)hresult & (long)((ulong)-65536)) == (long)((ulong)-2147024896))
			{
				winErrorCode = (hresult & 65535);
				return true;
			}
			winErrorCode = 0;
			return false;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002ED9A File Offset: 0x0002CF9A
		internal static int ConvertWin32ToHResult(int winErrorCode)
		{
			return -2147024896 | (winErrorCode & 65535);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002EDAC File Offset: 0x0002CFAC
		internal static bool IsFatalIOException(IOException exception, out int hresult)
		{
			bool result = false;
			int win32Code = 0;
			hresult = Marshal.GetHRForException(exception);
			if (FileOperations.ConvertHResultToWin32(hresult, out win32Code))
			{
				result = FileOperations.IsFatalIOErrorCode(win32Code);
			}
			return result;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002EDD8 File Offset: 0x0002CFD8
		internal static bool IsFatalIOErrorCode(int win32Code)
		{
			bool result = false;
			if (win32Code <= 23)
			{
				if (win32Code != 15)
				{
					switch (win32Code)
					{
					case 20:
					case 21:
					case 23:
						break;
					case 22:
						return result;
					default:
						return result;
					}
				}
			}
			else if (win32Code != 1117 && win32Code != 1392)
			{
				return result;
			}
			result = true;
			return result;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002EE24 File Offset: 0x0002D024
		internal static bool IsCorruptedIOException(IOException exception, out int hresult)
		{
			bool result = false;
			int win32Code = 0;
			hresult = Marshal.GetHRForException(exception);
			if (FileOperations.ConvertHResultToWin32(hresult, out win32Code))
			{
				result = FileOperations.IsCorruptedIOErrorCode(win32Code);
			}
			return result;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002EE50 File Offset: 0x0002D050
		internal static bool IsCorruptedIOErrorCode(int win32Code)
		{
			bool result = false;
			if (win32Code != 23 && win32Code != 1005)
			{
				switch (win32Code)
				{
				case 1392:
				case 1393:
					break;
				default:
					return result;
				}
			}
			result = true;
			return result;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002EE88 File Offset: 0x0002D088
		internal static bool IsRetryableIOException(IOException exception)
		{
			bool result = false;
			int win32Code = 0;
			int hrforException = Marshal.GetHRForException(exception);
			if (FileOperations.IsVolumeLockedHResult(hrforException))
			{
				return true;
			}
			if (FileOperations.ConvertHResultToWin32(hrforException, out win32Code))
			{
				result = FileOperations.IsRetryableIOErrorCode(win32Code);
			}
			return result;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002EEBC File Offset: 0x0002D0BC
		internal static bool IsVolumeLockedException(IOException exception)
		{
			int hrforException = Marshal.GetHRForException(exception);
			return FileOperations.IsVolumeLockedHResult(hrforException);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002EED6 File Offset: 0x0002D0D6
		internal static bool IsVolumeLockedHResult(int hResult)
		{
			return hResult == -2143879145 || hResult == -2144272361;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		internal static bool IsRetryableIOErrorCode(int win32Code)
		{
			bool result = false;
			if (win32Code == 32 || win32Code == 995 || win32Code == 1450)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002EF18 File Offset: 0x0002D118
		internal static bool Win32ErrorCodeToIOFailureTag(int win32code, FailureTag defaultFailureTag, out FailureTag failureTag)
		{
			failureTag = FailureTag.NoOp;
			if (win32code <= 80)
			{
				switch (win32code)
				{
				case 0:
				case 2:
					break;
				case 1:
				case 4:
					goto IL_77;
				case 3:
				case 5:
					failureTag = FailureTag.Configuration;
					return true;
				default:
					switch (win32code)
					{
					case 38:
						break;
					case 39:
						goto IL_72;
					default:
						if (win32code != 80)
						{
							goto IL_77;
						}
						break;
					}
					break;
				}
			}
			else if (win32code <= 112)
			{
				if (win32code != 87)
				{
					if (win32code != 112)
					{
						goto IL_77;
					}
					goto IL_72;
				}
			}
			else if (win32code != 183)
			{
				if (win32code != 206)
				{
					goto IL_77;
				}
				failureTag = FailureTag.Unrecoverable;
				return true;
			}
			failureTag = FailureTag.NoOp;
			return true;
			IL_72:
			failureTag = FailureTag.Space;
			return true;
			IL_77:
			if (FileOperations.IsRetryableIOErrorCode(win32code))
			{
				failureTag = FailureTag.NoOp;
				return true;
			}
			if (FileOperations.IsFatalIOErrorCode(win32code))
			{
				failureTag = FailureTag.IoHard;
				return true;
			}
			failureTag = defaultFailureTag;
			return false;
		}

		// Token: 0x0400044D RID: 1101
		private const uint Win32ErrorDiskFull = 2147942512U;
	}
}
