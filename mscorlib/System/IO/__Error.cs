using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000175 RID: 373
	internal static class __Error
	{
		// Token: 0x06001688 RID: 5768 RVA: 0x000472E3 File Offset: 0x000454E3
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(Environment.GetResourceString("IO.EOF_ReadBeyondEOF"));
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000472F4 File Offset: 0x000454F4
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_FileClosed"));
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00047306 File Offset: 0x00045506
		internal static void StreamIsClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00047318 File Offset: 0x00045518
		internal static void MemoryStreamNotExpandable()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_MemStreamNotExpandable"));
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00047329 File Offset: 0x00045529
		internal static void ReaderClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ReaderClosed"));
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0004733B File Offset: 0x0004553B
		internal static void ReadNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0004734C File Offset: 0x0004554C
		internal static void SeekNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0004735D File Offset: 0x0004555D
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_WrongAsyncResult"));
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0004736E File Offset: 0x0004556E
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(Environment.GetResourceString("InvalidOperation_EndReadCalledMultiple"));
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0004737F File Offset: 0x0004557F
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(Environment.GetResourceString("InvalidOperation_EndWriteCalledMultiple"));
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00047390 File Offset: 0x00045590
		[SecurityCritical]
		internal static string GetDisplayablePath(string path, bool isInvalidPath)
		{
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			if (path.Length < 2)
			{
				return path;
			}
			if (PathInternal.IsPartiallyQualified(path) && !isInvalidPath)
			{
				return path;
			}
			bool flag = false;
			try
			{
				if (!isInvalidPath)
				{
					FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, path, false, false);
					flag = true;
				}
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			if (!flag)
			{
				if (Path.IsDirectorySeparator(path[path.Length - 1]))
				{
					path = Environment.GetResourceString("IO.IO_NoPermissionToDirectoryName");
				}
				else
				{
					path = Path.GetFileName(path);
				}
			}
			return path;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00047438 File Offset: 0x00045638
		[SecuritySafeCritical]
		internal static void WinIOError()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			__Error.WinIOError(lastWin32Error, string.Empty);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00047458 File Offset: 0x00045658
		[SecurityCritical]
		internal static void WinIOError(int errorCode, string maybeFullPath)
		{
			bool isInvalidPath = errorCode == 123 || errorCode == 161;
			string displayablePath = __Error.GetDisplayablePath(maybeFullPath, isInvalidPath);
			if (errorCode <= 80)
			{
				if (errorCode <= 15)
				{
					switch (errorCode)
					{
					case 2:
						if (displayablePath.Length == 0)
						{
							throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound"));
						}
						throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", new object[]
						{
							displayablePath
						}), displayablePath);
					case 3:
						if (displayablePath.Length == 0)
						{
							throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_NoPathName"));
						}
						throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", new object[]
						{
							displayablePath
						}));
					case 4:
						break;
					case 5:
						if (displayablePath.Length == 0)
						{
							throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_IODenied_NoPathName"));
						}
						throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", new object[]
						{
							displayablePath
						}));
					default:
						if (errorCode == 15)
						{
							throw new DriveNotFoundException(Environment.GetResourceString("IO.DriveNotFound_Drive", new object[]
							{
								displayablePath
							}));
						}
						break;
					}
				}
				else if (errorCode != 32)
				{
					if (errorCode == 80)
					{
						if (displayablePath.Length != 0)
						{
							throw new IOException(Environment.GetResourceString("IO.IO_FileExists_Name", new object[]
							{
								displayablePath
							}), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
						}
					}
				}
				else
				{
					if (displayablePath.Length == 0)
					{
						throw new IOException(Environment.GetResourceString("IO.IO_SharingViolation_NoFileName"), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
					}
					throw new IOException(Environment.GetResourceString("IO.IO_SharingViolation_File", new object[]
					{
						displayablePath
					}), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
				}
			}
			else if (errorCode <= 183)
			{
				if (errorCode == 87)
				{
					throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
				}
				if (errorCode == 183)
				{
					if (displayablePath.Length != 0)
					{
						throw new IOException(Environment.GetResourceString("IO.IO_AlreadyExists_Name", new object[]
						{
							displayablePath
						}), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
					}
				}
			}
			else
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				if (errorCode == 995)
				{
					throw new OperationCanceledException();
				}
			}
			throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode), maybeFullPath);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00047680 File Offset: 0x00045880
		[SecuritySafeCritical]
		internal static void WinIODriveError(string driveName)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			__Error.WinIODriveError(driveName, lastWin32Error);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0004769A File Offset: 0x0004589A
		[SecurityCritical]
		internal static void WinIODriveError(string driveName, int errorCode)
		{
			if (errorCode == 3 || errorCode == 15)
			{
				throw new DriveNotFoundException(Environment.GetResourceString("IO.DriveNotFound_Drive", new object[]
				{
					driveName
				}));
			}
			__Error.WinIOError(errorCode, driveName);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000476C6 File Offset: 0x000458C6
		internal static void WriteNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000476D7 File Offset: 0x000458D7
		internal static void WriterClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_WriterClosed"));
		}

		// Token: 0x040007F0 RID: 2032
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x040007F1 RID: 2033
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x040007F2 RID: 2034
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x040007F3 RID: 2035
		internal const int ERROR_INVALID_PARAMETER = 87;
	}
}
