using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x02000182 RID: 386
	[ComVisible(true)]
	public static class File
	{
		// Token: 0x0600179D RID: 6045 RVA: 0x0004BB21 File Offset: 0x00049D21
		public static StreamReader OpenText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamReader(path);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0004BB37 File Offset: 0x00049D37
		public static StreamWriter CreateText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamWriter(path, false);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0004BB4E File Offset: 0x00049D4E
		public static StreamWriter AppendText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamWriter(path, true);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0004BB68 File Offset: 0x00049D68
		public static void Copy(string sourceFileName, string destFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			File.InternalCopy(sourceFileName, destFileName, false, true);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0004BBEC File Offset: 0x00049DEC
		public static void Copy(string sourceFileName, string destFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			File.InternalCopy(sourceFileName, destFileName, overwrite, true);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0004BC70 File Offset: 0x00049E70
		[SecurityCritical]
		internal static void UnsafeCopy(string sourceFileName, string destFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			File.InternalCopy(sourceFileName, destFileName, overwrite, false);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0004BCF4 File Offset: 0x00049EF4
		[SecuritySafeCritical]
		internal static string InternalCopy(string sourceFileName, string destFileName, bool overwrite, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(sourceFileName);
			string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal2, false, false);
			if (!Win32Native.CopyFile(fullPathInternal, fullPathInternal2, !overwrite))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				string maybeFullPath = destFileName;
				if (lastWin32Error != 80)
				{
					using (SafeFileHandle safeFileHandle = Win32Native.UnsafeCreateFile(fullPathInternal, int.MinValue, FileShare.Read, null, FileMode.Open, 0, IntPtr.Zero))
					{
						if (safeFileHandle.IsInvalid)
						{
							maybeFullPath = sourceFileName;
						}
					}
					if (lastWin32Error == 5 && Directory.InternalExists(fullPathInternal2))
					{
						throw new IOException(Environment.GetResourceString("Arg_FileIsDirectory_Name", new object[]
						{
							destFileName
						}), 5, fullPathInternal2);
					}
				}
				__Error.WinIOError(lastWin32Error, maybeFullPath);
			}
			return fullPathInternal2;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0004BDB0 File Offset: 0x00049FB0
		public static FileStream Create(string path)
		{
			return File.Create(path, 4096);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0004BDBD File Offset: 0x00049FBD
		public static FileStream Create(string path, int bufferSize)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0004BDC9 File Offset: 0x00049FC9
		public static FileStream Create(string path, int bufferSize, FileOptions options)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize, options);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0004BDD6 File Offset: 0x00049FD6
		public static FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
		{
			return new FileStream(path, FileMode.Create, FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.ReadPermissions, FileShare.None, bufferSize, options, fileSecurity);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0004BDE8 File Offset: 0x00049FE8
		[SecuritySafeCritical]
		public static void Delete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			File.InternalDelete(path, true);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0004BDFF File Offset: 0x00049FFF
		[SecurityCritical]
		internal static void UnsafeDelete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			File.InternalDelete(path, false);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0004BE18 File Offset: 0x0004A018
		[SecurityCritical]
		internal static void InternalDelete(string path, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.DeleteFile(fullPathInternal))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 2)
				{
					return;
				}
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0004BE54 File Offset: 0x0004A054
		[SecuritySafeCritical]
		public static void Decrypt(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.DecryptFile(fullPathInternal, 0))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 5)
				{
					DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(fullPathInternal));
					if (!string.Equals("NTFS", driveInfo.DriveFormat))
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
					}
				}
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0004BECC File Offset: 0x0004A0CC
		[SecuritySafeCritical]
		public static void Encrypt(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.EncryptFile(fullPathInternal))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 5)
				{
					DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(fullPathInternal));
					if (!string.Equals("NTFS", driveInfo.DriveFormat))
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
					}
				}
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0004BF40 File Offset: 0x0004A140
		[SecuritySafeCritical]
		public static bool Exists(string path)
		{
			return File.InternalExistsHelper(path, true);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0004BF49 File Offset: 0x0004A149
		[SecurityCritical]
		internal static bool UnsafeExists(string path)
		{
			return File.InternalExistsHelper(path, false);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0004BF54 File Offset: 0x0004A154
		[SecurityCritical]
		private static bool InternalExistsHelper(string path, bool checkHost)
		{
			try
			{
				if (path == null)
				{
					return false;
				}
				if (path.Length == 0)
				{
					return false;
				}
				path = Path.GetFullPathInternal(path);
				if (path.Length > 0 && Path.IsDirectorySeparator(path[path.Length - 1]))
				{
					return false;
				}
				FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, path, false, false);
				return File.InternalExists(path);
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0004C004 File Offset: 0x0004A204
		[SecurityCritical]
		internal static bool InternalExists(string path)
		{
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			return File.FillAttributeInfo(path, ref win32_FILE_ATTRIBUTE_DATA, false, true) == 0 && win32_FILE_ATTRIBUTE_DATA.fileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) == 0;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0004C03E File Offset: 0x0004A23E
		public static FileStream Open(string path, FileMode mode)
		{
			return File.Open(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0004C050 File Offset: 0x0004A250
		public static FileStream Open(string path, FileMode mode, FileAccess access)
		{
			return File.Open(path, mode, access, FileShare.None);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0004C05B File Offset: 0x0004A25B
		public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(path, mode, access, share);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0004C066 File Offset: 0x0004A266
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			File.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0004C078 File Offset: 0x0004A278
		[SecuritySafeCritical]
		public unsafe static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			SafeFileHandle hFile;
			using (File.OpenFile(path, FileAccess.Write, out hFile))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(hFile, &file_TIME, null, null))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0004C0DC File Offset: 0x0004A2DC
		[SecuritySafeCritical]
		public static DateTime GetCreationTime(string path)
		{
			return File.InternalGetCreationTimeUtc(path, true).ToLocalTime();
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0004C0F8 File Offset: 0x0004A2F8
		[SecuritySafeCritical]
		public static DateTime GetCreationTimeUtc(string path)
		{
			return File.InternalGetCreationTimeUtc(path, false);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0004C104 File Offset: 0x0004A304
		[SecurityCritical]
		private static DateTime InternalGetCreationTimeUtc(string path, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPathInternal, ref win32_FILE_ATTRIBUTE_DATA, false, false);
			if (num != 0)
			{
				__Error.WinIOError(num, fullPathInternal);
			}
			return DateTime.FromFileTimeUtc(win32_FILE_ATTRIBUTE_DATA.ftCreationTime.ToTicks());
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0004C14F File Offset: 0x0004A34F
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			File.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0004C160 File Offset: 0x0004A360
		[SecuritySafeCritical]
		public unsafe static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			SafeFileHandle hFile;
			using (File.OpenFile(path, FileAccess.Write, out hFile))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(hFile, null, &file_TIME, null))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0004C1C4 File Offset: 0x0004A3C4
		[SecuritySafeCritical]
		public static DateTime GetLastAccessTime(string path)
		{
			return File.InternalGetLastAccessTimeUtc(path, true).ToLocalTime();
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0004C1E0 File Offset: 0x0004A3E0
		[SecuritySafeCritical]
		public static DateTime GetLastAccessTimeUtc(string path)
		{
			return File.InternalGetLastAccessTimeUtc(path, false);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0004C1EC File Offset: 0x0004A3EC
		[SecurityCritical]
		private static DateTime InternalGetLastAccessTimeUtc(string path, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPathInternal, ref win32_FILE_ATTRIBUTE_DATA, false, false);
			if (num != 0)
			{
				__Error.WinIOError(num, fullPathInternal);
			}
			return DateTime.FromFileTimeUtc(win32_FILE_ATTRIBUTE_DATA.ftLastAccessTime.ToTicks());
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0004C237 File Offset: 0x0004A437
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			File.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0004C248 File Offset: 0x0004A448
		[SecuritySafeCritical]
		public unsafe static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			SafeFileHandle hFile;
			using (File.OpenFile(path, FileAccess.Write, out hFile))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(hFile, null, null, &file_TIME))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0004C2AC File Offset: 0x0004A4AC
		[SecuritySafeCritical]
		public static DateTime GetLastWriteTime(string path)
		{
			return File.InternalGetLastWriteTimeUtc(path, true).ToLocalTime();
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0004C2C8 File Offset: 0x0004A4C8
		[SecuritySafeCritical]
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return File.InternalGetLastWriteTimeUtc(path, false);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0004C2D4 File Offset: 0x0004A4D4
		[SecurityCritical]
		private static DateTime InternalGetLastWriteTimeUtc(string path, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPathInternal, ref win32_FILE_ATTRIBUTE_DATA, false, false);
			if (num != 0)
			{
				__Error.WinIOError(num, fullPathInternal);
			}
			return DateTime.FromFileTimeUtc(win32_FILE_ATTRIBUTE_DATA.ftLastWriteTime.ToTicks());
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0004C320 File Offset: 0x0004A520
		[SecuritySafeCritical]
		public static FileAttributes GetAttributes(string path)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPathInternal, ref win32_FILE_ATTRIBUTE_DATA, false, true);
			if (num != 0)
			{
				__Error.WinIOError(num, fullPathInternal);
			}
			return (FileAttributes)win32_FILE_ATTRIBUTE_DATA.fileAttributes;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0004C360 File Offset: 0x0004A560
		[SecuritySafeCritical]
		public static void SetAttributes(string path, FileAttributes fileAttributes)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.SetFileAttributes(fullPathInternal, (int)fileAttributes))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 87)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileAttrs"));
				}
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0004C3AA File Offset: 0x0004A5AA
		public static FileSecurity GetAccessControl(string path)
		{
			return File.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0004C3B4 File Offset: 0x0004A5B4
		public static FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return new FileSecurity(path, includeSections);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
		[SecuritySafeCritical]
		public static void SetAccessControl(string path, FileSecurity fileSecurity)
		{
			if (fileSecurity == null)
			{
				throw new ArgumentNullException("fileSecurity");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			fileSecurity.Persist(fullPathInternal);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0004C3E9 File Offset: 0x0004A5E9
		public static FileStream OpenRead(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0004C3F4 File Offset: 0x0004A5F4
		public static FileStream OpenWrite(string path)
		{
			return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0004C3FF File Offset: 0x0004A5FF
		[SecuritySafeCritical]
		public static string ReadAllText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllText(path, Encoding.UTF8, true);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0004C433 File Offset: 0x0004A633
		[SecuritySafeCritical]
		public static string ReadAllText(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllText(path, encoding, true);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0004C471 File Offset: 0x0004A671
		[SecurityCritical]
		internal static string UnsafeReadAllText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllText(path, Encoding.UTF8, false);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0004C4A8 File Offset: 0x0004A6A8
		[SecurityCritical]
		private static string InternalReadAllText(string path, Encoding encoding, bool checkHost)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(path, encoding, true, StreamReader.DefaultBufferSize, checkHost))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0004C4E8 File Offset: 0x0004A6E8
		[SecuritySafeCritical]
		public static void WriteAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, true);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0004C51D File Offset: 0x0004A71D
		[SecuritySafeCritical]
		public static void WriteAllText(string path, string contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllText(path, contents, encoding, true);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0004C55C File Offset: 0x0004A75C
		[SecurityCritical]
		internal static void UnsafeWriteAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, false);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0004C594 File Offset: 0x0004A794
		[SecurityCritical]
		private static void InternalWriteAllText(string path, string contents, Encoding encoding, bool checkHost)
		{
			using (StreamWriter streamWriter = new StreamWriter(path, false, encoding, 1024, checkHost))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0004C5D4 File Offset: 0x0004A7D4
		[SecuritySafeCritical]
		public static byte[] ReadAllBytes(string path)
		{
			return File.InternalReadAllBytes(path, true);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0004C5DD File Offset: 0x0004A7DD
		[SecurityCritical]
		internal static byte[] UnsafeReadAllBytes(string path)
		{
			return File.InternalReadAllBytes(path, false);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0004C5E8 File Offset: 0x0004A7E8
		[SecurityCritical]
		private static byte[] InternalReadAllBytes(string path, bool checkHost)
		{
			byte[] array;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
			{
				int num = 0;
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_FileTooLong2GB"));
				}
				int i = (int)length;
				array = new byte[i];
				while (i > 0)
				{
					int num2 = fileStream.Read(array, num, i);
					if (num2 == 0)
					{
						__Error.EndOfFile();
					}
					num += num2;
					i -= num2;
				}
			}
			return array;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0004C684 File Offset: 0x0004A884
		[SecuritySafeCritical]
		public static void WriteAllBytes(string path, byte[] bytes)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			File.InternalWriteAllBytes(path, bytes, true);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0004C6D8 File Offset: 0x0004A8D8
		[SecurityCritical]
		internal static void UnsafeWriteAllBytes(string path, byte[] bytes)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			File.InternalWriteAllBytes(path, bytes, false);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0004C72C File Offset: 0x0004A92C
		[SecurityCritical]
		private static void InternalWriteAllBytes(string path, byte[] bytes, bool checkHost)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
			{
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0004C77C File Offset: 0x0004A97C
		public static string[] ReadAllLines(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllLines(path, Encoding.UTF8);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0004C7AF File Offset: 0x0004A9AF
		public static string[] ReadAllLines(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllLines(path, encoding);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0004C7EC File Offset: 0x0004A9EC
		private static string[] InternalReadAllLines(string path, Encoding encoding)
		{
			List<string> list = new List<string>();
			using (StreamReader streamReader = new StreamReader(path, encoding))
			{
				string item;
				while ((item = streamReader.ReadLine()) != null)
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0004C83C File Offset: 0x0004AA3C
		public static IEnumerable<string> ReadLines(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
			}
			return ReadLinesIterator.CreateIterator(path, Encoding.UTF8);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0004C874 File Offset: 0x0004AA74
		public static IEnumerable<string> ReadLines(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
			}
			return ReadLinesIterator.CreateIterator(path, encoding);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0004C8C4 File Offset: 0x0004AAC4
		public static void WriteAllLines(string path, string[] contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, StreamWriter.UTF8NoBOM), contents);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0004C918 File Offset: 0x0004AB18
		public static void WriteAllLines(string path, string[] contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, encoding), contents);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0004C978 File Offset: 0x0004AB78
		public static void WriteAllLines(string path, IEnumerable<string> contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, StreamWriter.UTF8NoBOM), contents);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0004C9CC File Offset: 0x0004ABCC
		public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, encoding), contents);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0004CA2C File Offset: 0x0004AC2C
		private static void InternalWriteAllLines(TextWriter writer, IEnumerable<string> contents)
		{
			try
			{
				foreach (string value in contents)
				{
					writer.WriteLine(value);
				}
			}
			finally
			{
				if (writer != null)
				{
					((IDisposable)writer).Dispose();
				}
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0004CA8C File Offset: 0x0004AC8C
		public static void AppendAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalAppendAllText(path, contents, StreamWriter.UTF8NoBOM);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0004CAC0 File Offset: 0x0004ACC0
		public static void AppendAllText(string path, string contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalAppendAllText(path, contents, encoding);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0004CB00 File Offset: 0x0004AD00
		private static void InternalAppendAllText(string path, string contents, Encoding encoding)
		{
			using (StreamWriter streamWriter = new StreamWriter(path, true, encoding))
			{
				streamWriter.Write(contents);
			}
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0004CB3C File Offset: 0x0004AD3C
		public static void AppendAllLines(string path, IEnumerable<string> contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, true, StreamWriter.UTF8NoBOM), contents);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0004CB90 File Offset: 0x0004AD90
		public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, true, encoding), contents);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0004CBED File Offset: 0x0004ADED
		[SecuritySafeCritical]
		public static void Move(string sourceFileName, string destFileName)
		{
			File.InternalMove(sourceFileName, destFileName, true);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0004CBF7 File Offset: 0x0004ADF7
		[SecurityCritical]
		internal static void UnsafeMove(string sourceFileName, string destFileName)
		{
			File.InternalMove(sourceFileName, destFileName, false);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0004CC04 File Offset: 0x0004AE04
		[SecurityCritical]
		private static void InternalMove(string sourceFileName, string destFileName, bool checkHost)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			string fullPathInternal = Path.GetFullPathInternal(sourceFileName);
			string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal2, false, false);
			if (!File.InternalExists(fullPathInternal))
			{
				__Error.WinIOError(2, fullPathInternal);
			}
			if (!Win32Native.MoveFile(fullPathInternal, fullPathInternal2))
			{
				__Error.WinIOError();
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0004CCB8 File Offset: 0x0004AEB8
		public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, false);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0004CCDF File Offset: 0x0004AEDF
		public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0004CD08 File Offset: 0x0004AF08
		[SecuritySafeCritical]
		private static void InternalReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			string fullPathInternal = Path.GetFullPathInternal(sourceFileName);
			string fullPathInternal2 = Path.GetFullPathInternal(destinationFileName);
			string text = null;
			if (destinationBackupFileName != null)
			{
				text = Path.GetFullPathInternal(destinationBackupFileName);
			}
			if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				FileIOPermission.EmulateFileIOPermissionChecks(fullPathInternal);
				FileIOPermission.EmulateFileIOPermissionChecks(fullPathInternal2);
				if (text != null)
				{
					FileIOPermission.EmulateFileIOPermissionChecks(text);
				}
			}
			else
			{
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[]
				{
					fullPathInternal,
					fullPathInternal2
				});
				if (text != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Write, text);
				}
				fileIOPermission.Demand();
			}
			int num = 1;
			if (ignoreMetadataErrors)
			{
				num |= 2;
			}
			if (!Win32Native.ReplaceFile(fullPathInternal2, fullPathInternal, text, num, IntPtr.Zero, IntPtr.Zero))
			{
				__Error.WinIOError();
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0004CDA0 File Offset: 0x0004AFA0
		[SecurityCritical]
		internal static int FillAttributeInfo(string path, ref Win32Native.WIN32_FILE_ATTRIBUTE_DATA data, bool tryagain, bool returnErrorOnNotFound)
		{
			int num = 0;
			if (tryagain)
			{
				Win32Native.WIN32_FIND_DATA win32_FIND_DATA = default(Win32Native.WIN32_FIND_DATA);
				string fileName = path.TrimEnd(new char[]
				{
					Path.DirectorySeparatorChar,
					Path.AltDirectorySeparatorChar
				});
				int errorMode = Win32Native.SetErrorMode(1);
				try
				{
					bool flag = false;
					SafeFindHandle safeFindHandle = Win32Native.FindFirstFile(fileName, ref win32_FIND_DATA);
					try
					{
						if (safeFindHandle.IsInvalid)
						{
							flag = true;
							num = Marshal.GetLastWin32Error();
							if ((num == 2 || num == 3 || num == 21) && !returnErrorOnNotFound)
							{
								num = 0;
								data.fileAttributes = -1;
							}
							return num;
						}
					}
					finally
					{
						try
						{
							safeFindHandle.Close();
						}
						catch
						{
							if (!flag)
							{
								__Error.WinIOError();
							}
						}
					}
				}
				finally
				{
					Win32Native.SetErrorMode(errorMode);
				}
				data.PopulateFrom(ref win32_FIND_DATA);
				return num;
			}
			bool flag2 = false;
			int errorMode2 = Win32Native.SetErrorMode(1);
			try
			{
				flag2 = Win32Native.GetFileAttributesEx(path, 0, ref data);
			}
			finally
			{
				Win32Native.SetErrorMode(errorMode2);
			}
			if (!flag2)
			{
				num = Marshal.GetLastWin32Error();
				if (num != 2 && num != 3 && num != 21)
				{
					return File.FillAttributeInfo(path, ref data, true, returnErrorOnNotFound);
				}
				if (!returnErrorOnNotFound)
				{
					num = 0;
					data.fileAttributes = -1;
				}
			}
			return num;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0004CED0 File Offset: 0x0004B0D0
		[SecurityCritical]
		private static FileStream OpenFile(string path, FileAccess access, out SafeFileHandle handle)
		{
			FileStream fileStream = new FileStream(path, FileMode.Open, access, FileShare.ReadWrite, 1);
			handle = fileStream.SafeFileHandle;
			if (handle.IsInvalid)
			{
				int num = Marshal.GetLastWin32Error();
				string fullPathInternal = Path.GetFullPathInternal(path);
				if (num == 3 && fullPathInternal.Equals(Directory.GetDirectoryRoot(fullPathInternal)))
				{
					num = 5;
				}
				__Error.WinIOError(num, path);
			}
			return fileStream;
		}

		// Token: 0x0400082D RID: 2093
		private const int GetFileExInfoStandard = 0;

		// Token: 0x0400082E RID: 2094
		private const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x0400082F RID: 2095
		private const int ERROR_ACCESS_DENIED = 5;
	}
}
