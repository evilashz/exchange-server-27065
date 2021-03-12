﻿using System;
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
	// Token: 0x0200017A RID: 378
	[ComVisible(true)]
	public static class Directory
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x00049A94 File Offset: 0x00047C94
		public static DirectoryInfo GetParent(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"), "path");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			string directoryName = Path.GetDirectoryName(fullPathInternal);
			if (directoryName == null)
			{
				return null;
			}
			return new DirectoryInfo(directoryName);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00049AE5 File Offset: 0x00047CE5
		[SecuritySafeCritical]
		public static DirectoryInfo CreateDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
			}
			return Directory.InternalCreateDirectoryHelper(path, true);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00049B14 File Offset: 0x00047D14
		[SecurityCritical]
		internal static DirectoryInfo UnsafeCreateDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
			}
			return Directory.InternalCreateDirectoryHelper(path, false);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00049B44 File Offset: 0x00047D44
		[SecurityCritical]
		internal static DirectoryInfo InternalCreateDirectoryHelper(string path, bool checkHost)
		{
			string fullPathAndCheckPermissions = Directory.GetFullPathAndCheckPermissions(path, checkHost, FileSecurityStateAccess.Read);
			Directory.InternalCreateDirectory(fullPathAndCheckPermissions, path, null, checkHost);
			return new DirectoryInfo(fullPathAndCheckPermissions, false);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00049B6C File Offset: 0x00047D6C
		internal static string GetFullPathAndCheckPermissions(string path, bool checkHost, FileSecurityStateAccess access = FileSecurityStateAccess.Read)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			Directory.CheckPermissions(path, fullPathInternal, checkHost, access);
			return fullPathInternal;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00049B8A File Offset: 0x00047D8A
		[SecuritySafeCritical]
		internal static void CheckPermissions(string displayPath, string fullPath, bool checkHost, FileSecurityStateAccess access = FileSecurityStateAccess.Read)
		{
			if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
				return;
			}
			FileIOPermission.QuickDemand((FileIOPermissionAccess)access, Directory.GetDemandDir(fullPath, true), false, false);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00049BAC File Offset: 0x00047DAC
		[SecuritySafeCritical]
		public static DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
			}
			string fullPathAndCheckPermissions = Directory.GetFullPathAndCheckPermissions(path, true, FileSecurityStateAccess.Read);
			Directory.InternalCreateDirectory(fullPathAndCheckPermissions, path, directorySecurity);
			return new DirectoryInfo(fullPathAndCheckPermissions, false);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00049BF8 File Offset: 0x00047DF8
		internal static string GetDemandDir(string fullPath, bool thisDirOnly)
		{
			string result;
			if (thisDirOnly)
			{
				if (fullPath.EndsWith(Path.DirectorySeparatorChar) || fullPath.EndsWith(Path.AltDirectorySeparatorChar))
				{
					result = fullPath + ".";
				}
				else
				{
					result = fullPath + "\\.";
				}
			}
			else if (!fullPath.EndsWith(Path.DirectorySeparatorChar) && !fullPath.EndsWith(Path.AltDirectorySeparatorChar))
			{
				result = fullPath + "\\";
			}
			else
			{
				result = fullPath;
			}
			return result;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00049C69 File Offset: 0x00047E69
		internal static void InternalCreateDirectory(string fullPath, string path, object dirSecurityObj)
		{
			Directory.InternalCreateDirectory(fullPath, path, dirSecurityObj, false);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00049C74 File Offset: 0x00047E74
		[SecuritySafeCritical]
		internal unsafe static void InternalCreateDirectory(string fullPath, string path, object dirSecurityObj, bool checkHost)
		{
			DirectorySecurity directorySecurity = (DirectorySecurity)dirSecurityObj;
			int num = fullPath.Length;
			if (num >= 2 && Path.IsDirectorySeparator(fullPath[num - 1]))
			{
				num--;
			}
			int rootLength = Path.GetRootLength(fullPath);
			if (num == 2 && Path.IsDirectorySeparator(fullPath[1]))
			{
				throw new IOException(Environment.GetResourceString("IO.IO_CannotCreateDirectory", new object[]
				{
					path
				}));
			}
			if (Directory.InternalExists(fullPath))
			{
				return;
			}
			List<string> list = new List<string>();
			bool flag = false;
			if (num > rootLength)
			{
				int num2 = num - 1;
				while (num2 >= rootLength && !flag)
				{
					string text = fullPath.Substring(0, num2 + 1);
					if (!Directory.InternalExists(text))
					{
						list.Add(text);
					}
					else
					{
						flag = true;
					}
					while (num2 > rootLength && fullPath[num2] != Path.DirectorySeparatorChar && fullPath[num2] != Path.AltDirectorySeparatorChar)
					{
						num2--;
					}
					num2--;
				}
			}
			int count = list.Count;
			if (list.Count != 0 && !CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				string[] array = new string[list.Count];
				list.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array;
					int num3 = i;
					array2[num3] += "\\.";
				}
				AccessControlActions control = (directorySecurity == null) ? AccessControlActions.None : AccessControlActions.Change;
				FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, control, array, false, false);
			}
			Win32Native.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if (directorySecurity != null)
			{
				security_ATTRIBUTES = new Win32Native.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES);
				byte[] securityDescriptorBinaryForm = directorySecurity.GetSecurityDescriptorBinaryForm();
				byte* ptr = stackalloc byte[checked(unchecked((UIntPtr)securityDescriptorBinaryForm.Length) * 1)];
				Buffer.Memcpy(ptr, 0, securityDescriptorBinaryForm, 0, securityDescriptorBinaryForm.Length);
				security_ATTRIBUTES.pSecurityDescriptor = ptr;
			}
			bool flag2 = true;
			int num4 = 0;
			string maybeFullPath = path;
			while (list.Count > 0)
			{
				string text2 = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
				if (PathInternal.IsDirectoryTooLong(text2))
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				flag2 = Win32Native.CreateDirectory(text2, security_ATTRIBUTES);
				if (!flag2 && num4 == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 183)
					{
						num4 = lastWin32Error;
					}
					else if (File.InternalExists(text2) || (!Directory.InternalExists(text2, out lastWin32Error) && lastWin32Error == 5))
					{
						num4 = lastWin32Error;
						try
						{
							Directory.CheckPermissions(string.Empty, text2, checkHost, FileSecurityStateAccess.PathDiscovery);
							maybeFullPath = text2;
						}
						catch (SecurityException)
						{
						}
					}
				}
			}
			if (count == 0 && !flag)
			{
				string path2 = Directory.InternalGetDirectoryRoot(fullPath);
				if (!Directory.InternalExists(path2))
				{
					__Error.WinIOError(3, Directory.InternalGetDirectoryRoot(path));
				}
				return;
			}
			if (!flag2 && num4 != 0)
			{
				__Error.WinIOError(num4, maybeFullPath);
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00049EFC File Offset: 0x000480FC
		[SecuritySafeCritical]
		public static bool Exists(string path)
		{
			return Directory.InternalExistsHelper(path, true);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00049F05 File Offset: 0x00048105
		[SecurityCritical]
		internal static bool UnsafeExists(string path)
		{
			return Directory.InternalExistsHelper(path, false);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00049F10 File Offset: 0x00048110
		[SecurityCritical]
		internal static bool InternalExistsHelper(string path, bool checkHost)
		{
			if (path == null || path.Length == 0)
			{
				return false;
			}
			try
			{
				string fullPathAndCheckPermissions = Directory.GetFullPathAndCheckPermissions(path, checkHost, FileSecurityStateAccess.Read);
				return Directory.InternalExists(fullPathAndCheckPermissions);
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

		// Token: 0x06001712 RID: 5906 RVA: 0x00049F90 File Offset: 0x00048190
		[SecurityCritical]
		internal static bool InternalExists(string path)
		{
			int num = 0;
			return Directory.InternalExists(path, out num);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00049FA8 File Offset: 0x000481A8
		[SecurityCritical]
		internal static bool InternalExists(string path, out int lastError)
		{
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			lastError = File.FillAttributeInfo(path, ref win32_FILE_ATTRIBUTE_DATA, false, true);
			return lastError == 0 && win32_FILE_ATTRIBUTE_DATA.fileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) != 0;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00049FE4 File Offset: 0x000481E4
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			Directory.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00049FF4 File Offset: 0x000481F4
		[SecuritySafeCritical]
		public unsafe static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			using (SafeFileHandle safeFileHandle = Directory.OpenHandle(path))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(safeFileHandle, &file_TIME, null, null))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0004A050 File Offset: 0x00048250
		public static DateTime GetCreationTime(string path)
		{
			return File.GetCreationTime(path);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0004A058 File Offset: 0x00048258
		public static DateTime GetCreationTimeUtc(string path)
		{
			return File.GetCreationTimeUtc(path);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0004A060 File Offset: 0x00048260
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			Directory.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0004A070 File Offset: 0x00048270
		[SecuritySafeCritical]
		public unsafe static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			using (SafeFileHandle safeFileHandle = Directory.OpenHandle(path))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(safeFileHandle, null, null, &file_TIME))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0004A0CC File Offset: 0x000482CC
		public static DateTime GetLastWriteTime(string path)
		{
			return File.GetLastWriteTime(path);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0004A0D4 File Offset: 0x000482D4
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return File.GetLastWriteTimeUtc(path);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0004A0DC File Offset: 0x000482DC
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			Directory.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0004A0EC File Offset: 0x000482EC
		[SecuritySafeCritical]
		public unsafe static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			using (SafeFileHandle safeFileHandle = Directory.OpenHandle(path))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(safeFileHandle, null, &file_TIME, null))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0004A148 File Offset: 0x00048348
		public static DateTime GetLastAccessTime(string path)
		{
			return File.GetLastAccessTime(path);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0004A150 File Offset: 0x00048350
		public static DateTime GetLastAccessTimeUtc(string path)
		{
			return File.GetLastAccessTimeUtc(path);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0004A158 File Offset: 0x00048358
		public static DirectorySecurity GetAccessControl(string path)
		{
			return new DirectorySecurity(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0004A162 File Offset: 0x00048362
		public static DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return new DirectorySecurity(path, includeSections);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0004A16C File Offset: 0x0004836C
		[SecuritySafeCritical]
		public static void SetAccessControl(string path, DirectorySecurity directorySecurity)
		{
			if (directorySecurity == null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			directorySecurity.Persist(fullPathInternal);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0004A195 File Offset: 0x00048395
		public static string[] GetFiles(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalGetFiles(path, "*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0004A1B1 File Offset: 0x000483B1
		public static string[] GetFiles(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0004A1D8 File Offset: 0x000483D8
		public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return Directory.InternalGetFiles(path, searchPattern, searchOption);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0004A225 File Offset: 0x00048425
		private static string[] InternalGetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, false, searchOption, true);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0004A233 File Offset: 0x00048433
		[SecurityCritical]
		internal static string[] UnsafeGetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, false, searchOption, false);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0004A241 File Offset: 0x00048441
		public static string[] GetDirectories(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalGetDirectories(path, "*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0004A25D File Offset: 0x0004845D
		public static string[] GetDirectories(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0004A284 File Offset: 0x00048484
		public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return Directory.InternalGetDirectories(path, searchPattern, searchOption);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0004A2D1 File Offset: 0x000484D1
		private static string[] InternalGetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, false, true, searchOption, true);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0004A2DF File Offset: 0x000484DF
		[SecurityCritical]
		internal static string[] UnsafeGetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, false, true, searchOption, false);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0004A2ED File Offset: 0x000484ED
		public static string[] GetFileSystemEntries(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalGetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0004A309 File Offset: 0x00048509
		public static string[] GetFileSystemEntries(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0004A330 File Offset: 0x00048530
		public static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return Directory.InternalGetFileSystemEntries(path, searchPattern, searchOption);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0004A37D File Offset: 0x0004857D
		private static string[] InternalGetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, true, searchOption, true);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0004A38C File Offset: 0x0004858C
		internal static string[] InternalGetFileDirectoryNames(string path, string userPathOriginal, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
		{
			IEnumerable<string> collection = FileSystemEnumerableFactory.CreateFileNameIterator(path, userPathOriginal, searchPattern, includeFiles, includeDirs, searchOption, checkHost);
			List<string> list = new List<string>(collection);
			return list.ToArray();
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0004A3B6 File Offset: 0x000485B6
		public static IEnumerable<string> EnumerateDirectories(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalEnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0004A3D2 File Offset: 0x000485D2
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0004A3F8 File Offset: 0x000485F8
		public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return Directory.InternalEnumerateDirectories(path, searchPattern, searchOption);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0004A445 File Offset: 0x00048645
		private static IEnumerable<string> InternalEnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, false, true);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0004A451 File Offset: 0x00048651
		public static IEnumerable<string> EnumerateFiles(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalEnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0004A46D File Offset: 0x0004866D
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalEnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0004A494 File Offset: 0x00048694
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return Directory.InternalEnumerateFiles(path, searchPattern, searchOption);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0004A4E1 File Offset: 0x000486E1
		private static IEnumerable<string> InternalEnumerateFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, true, false);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0004A4ED File Offset: 0x000486ED
		public static IEnumerable<string> EnumerateFileSystemEntries(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Directory.InternalEnumerateFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0004A509 File Offset: 0x00048709
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalEnumerateFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0004A530 File Offset: 0x00048730
		public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return Directory.InternalEnumerateFileSystemEntries(path, searchPattern, searchOption);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0004A57D File Offset: 0x0004877D
		private static IEnumerable<string> InternalEnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, true, true);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0004A589 File Offset: 0x00048789
		private static IEnumerable<string> EnumerateFileSystemNames(string path, string searchPattern, SearchOption searchOption, bool includeFiles, bool includeDirs)
		{
			return FileSystemEnumerableFactory.CreateFileNameIterator(path, path, searchPattern, includeFiles, includeDirs, searchOption, true);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0004A598 File Offset: 0x00048798
		[SecuritySafeCritical]
		public static string[] GetLogicalDrives()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			int logicalDrives = Win32Native.GetLogicalDrives();
			if (logicalDrives == 0)
			{
				__Error.WinIOError();
			}
			uint num = (uint)logicalDrives;
			int num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					num2++;
				}
				num >>= 1;
			}
			string[] array = new string[num2];
			char[] array2 = new char[]
			{
				'A',
				':',
				'\\'
			};
			num = (uint)logicalDrives;
			num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					array[num2++] = new string(array2);
				}
				num >>= 1;
				char[] array3 = array2;
				int num3 = 0;
				array3[num3] += '\u0001';
			}
			return array;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0004A620 File Offset: 0x00048820
		[SecuritySafeCritical]
		public static string GetDirectoryRoot(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			string text = fullPathInternal.Substring(0, Path.GetRootLength(fullPathInternal));
			Directory.CheckPermissions(path, text, true, FileSecurityStateAccess.PathDiscovery);
			return text;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0004A65A File Offset: 0x0004885A
		internal static string InternalGetDirectoryRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			return path.Substring(0, Path.GetRootLength(path));
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0004A66E File Offset: 0x0004886E
		[SecuritySafeCritical]
		public static string GetCurrentDirectory()
		{
			return Directory.InternalGetCurrentDirectory(true);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0004A676 File Offset: 0x00048876
		[SecurityCritical]
		internal static string UnsafeGetCurrentDirectory()
		{
			return Directory.InternalGetCurrentDirectory(false);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0004A680 File Offset: 0x00048880
		[SecuritySafeCritical]
		private static string InternalGetCurrentDirectory(bool checkHost)
		{
			string text = AppContextSwitches.UseLegacyPathHandling ? Directory.LegacyGetCurrentDirectory() : Directory.NewGetCurrentDirectory();
			Directory.CheckPermissions(string.Empty, text, true, FileSecurityStateAccess.PathDiscovery);
			return text;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0004A6B0 File Offset: 0x000488B0
		[SecurityCritical]
		private static string LegacyGetCurrentDirectory()
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(261);
			if (Win32Native.GetCurrentDirectory(stringBuilder.Capacity, stringBuilder) == 0)
			{
				__Error.WinIOError();
			}
			string text = stringBuilder.ToString();
			if (text.IndexOf('~') >= 0)
			{
				int longPathName = Win32Native.GetLongPathName(text, stringBuilder, stringBuilder.Capacity);
				if (longPathName == 0 || longPathName >= 260)
				{
					int num = Marshal.GetLastWin32Error();
					if (longPathName >= 260)
					{
						num = 206;
					}
					if (num != 2 && num != 3 && num != 1 && num != 5)
					{
						__Error.WinIOError(num, string.Empty);
					}
				}
				text = stringBuilder.ToString();
			}
			StringBuilderCache.Release(stringBuilder);
			return text;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0004A744 File Offset: 0x00048944
		[SecurityCritical]
		private static string NewGetCurrentDirectory()
		{
			string result;
			using (StringBuffer stringBuffer = new StringBuffer(260U))
			{
				uint currentDirectoryW;
				while ((currentDirectoryW = Win32Native.GetCurrentDirectoryW(stringBuffer.CharCapacity, stringBuffer.GetHandle())) > stringBuffer.CharCapacity)
				{
					stringBuffer.EnsureCharCapacity(currentDirectoryW);
				}
				if (currentDirectoryW == 0U)
				{
					__Error.WinIOError();
				}
				stringBuffer.Length = currentDirectoryW;
				if (stringBuffer.Contains('~'))
				{
					result = LongPathHelper.GetLongPathName(stringBuffer);
				}
				else
				{
					result = stringBuffer.ToString();
				}
			}
			return result;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0004A7C8 File Offset: 0x000489C8
		[SecuritySafeCritical]
		public static void SetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("value");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
			}
			if (PathInternal.IsPathTooLong(path))
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			string fullPathInternal = Path.GetFullPathInternal(path);
			if (!Win32Native.SetCurrentDirectory(fullPathInternal))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
				}
				__Error.WinIOError(num, fullPathInternal);
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0004A840 File Offset: 0x00048A40
		[SecuritySafeCritical]
		public static void Move(string sourceDirName, string destDirName)
		{
			Directory.InternalMove(sourceDirName, destDirName, true);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0004A84A File Offset: 0x00048A4A
		[SecurityCritical]
		internal static void UnsafeMove(string sourceDirName, string destDirName)
		{
			Directory.InternalMove(sourceDirName, destDirName, false);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0004A854 File Offset: 0x00048A54
		[SecurityCritical]
		private static void InternalMove(string sourceDirName, string destDirName, bool checkHost)
		{
			if (sourceDirName == null)
			{
				throw new ArgumentNullException("sourceDirName");
			}
			if (sourceDirName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceDirName");
			}
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destDirName");
			}
			string fullPathInternal = Path.GetFullPathInternal(sourceDirName);
			string demandDir = Directory.GetDemandDir(fullPathInternal, false);
			if (PathInternal.IsDirectoryTooLong(demandDir))
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			string fullPathInternal2 = Path.GetFullPathInternal(destDirName);
			string demandDir2 = Directory.GetDemandDir(fullPathInternal2, false);
			if (PathInternal.IsDirectoryTooLong(demandDir))
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, demandDir, false, false);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, demandDir2, false, false);
			if (string.Compare(demandDir, demandDir2, StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
			}
			string pathRoot = Path.GetPathRoot(demandDir);
			string pathRoot2 = Path.GetPathRoot(demandDir2);
			if (string.Compare(pathRoot, pathRoot2, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
			}
			if (!Win32Native.MoveFile(sourceDirName, destDirName))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
					__Error.WinIOError(num, fullPathInternal);
				}
				if (num == 5)
				{
					throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", new object[]
					{
						sourceDirName
					}), Win32Native.MakeHRFromErrorCode(num));
				}
				__Error.WinIOError(num, string.Empty);
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0004A9B0 File Offset: 0x00048BB0
		[SecuritySafeCritical]
		public static void Delete(string path)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			Directory.Delete(fullPathInternal, path, false, true);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0004A9D0 File Offset: 0x00048BD0
		[SecuritySafeCritical]
		public static void Delete(string path, bool recursive)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			Directory.Delete(fullPathInternal, path, recursive, true);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0004A9F0 File Offset: 0x00048BF0
		[SecurityCritical]
		internal static void UnsafeDelete(string path, bool recursive)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			Directory.Delete(fullPathInternal, path, recursive, false);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0004AA10 File Offset: 0x00048C10
		[SecurityCritical]
		internal static void Delete(string fullPath, string userPath, bool recursive, bool checkHost)
		{
			string demandDir = Directory.GetDemandDir(fullPath, !recursive);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, demandDir, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, false, true);
			if (num != 0)
			{
				if (num == 2)
				{
					num = 3;
				}
				__Error.WinIOError(num, fullPath);
			}
			if ((win32_FILE_ATTRIBUTE_DATA.fileAttributes & 1024) != 0)
			{
				recursive = false;
			}
			Win32Native.WIN32_FIND_DATA win32_FIND_DATA = default(Win32Native.WIN32_FIND_DATA);
			Directory.DeleteHelper(fullPath, userPath, recursive, true, ref win32_FIND_DATA);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0004AA78 File Offset: 0x00048C78
		[SecurityCritical]
		private static void DeleteHelper(string fullPath, string userPath, bool recursive, bool throwOnTopLevelDirectoryNotFound, ref Win32Native.WIN32_FIND_DATA data)
		{
			Exception ex = null;
			if (recursive)
			{
				int num;
				using (SafeFindHandle safeFindHandle = Win32Native.FindFirstFile(fullPath + "\\*", ref data))
				{
					if (safeFindHandle.IsInvalid)
					{
						num = Marshal.GetLastWin32Error();
						__Error.WinIOError(num, fullPath);
					}
					for (;;)
					{
						bool flag = (data.dwFileAttributes & 16) != 0;
						if (!flag)
						{
							goto IL_12B;
						}
						if (!data.IsRelativeDirectory)
						{
							string cFileName = data.cFileName;
							bool flag2 = (data.dwFileAttributes & 1024) == 0;
							if (flag2)
							{
								string fullPath2 = Path.CombineNoChecks(fullPath, cFileName);
								string userPath2 = Path.CombineNoChecks(userPath, cFileName);
								try
								{
									Directory.DeleteHelper(fullPath2, userPath2, recursive, false, ref data);
									goto IL_163;
								}
								catch (Exception ex2)
								{
									if (ex == null)
									{
										ex = ex2;
									}
									goto IL_163;
								}
							}
							if (data.dwReserved0 == -1610612733)
							{
								string mountPoint = Path.CombineNoChecks(fullPath, cFileName + Path.DirectorySeparatorChar.ToString());
								if (!Win32Native.DeleteVolumeMountPoint(mountPoint))
								{
									num = Marshal.GetLastWin32Error();
									if (num != 3)
									{
										try
										{
											__Error.WinIOError(num, cFileName);
										}
										catch (Exception ex3)
										{
											if (ex == null)
											{
												ex = ex3;
											}
										}
									}
								}
							}
							string path = Path.CombineNoChecks(fullPath, cFileName);
							if (!Win32Native.RemoveDirectory(path))
							{
								num = Marshal.GetLastWin32Error();
								if (num != 3)
								{
									try
									{
										__Error.WinIOError(num, cFileName);
										goto IL_163;
									}
									catch (Exception ex4)
									{
										if (ex == null)
										{
											ex = ex4;
										}
										goto IL_163;
									}
									goto IL_12B;
								}
							}
						}
						IL_163:
						if (!Win32Native.FindNextFile(safeFindHandle, ref data))
						{
							break;
						}
						continue;
						IL_12B:
						string cFileName2 = data.cFileName;
						if (Win32Native.DeleteFile(Path.CombineNoChecks(fullPath, cFileName2)))
						{
							goto IL_163;
						}
						num = Marshal.GetLastWin32Error();
						if (num != 2)
						{
							try
							{
								__Error.WinIOError(num, cFileName2);
							}
							catch (Exception ex5)
							{
								if (ex == null)
								{
									ex = ex5;
								}
							}
							goto IL_163;
						}
						goto IL_163;
					}
					num = Marshal.GetLastWin32Error();
				}
				if (ex != null)
				{
					throw ex;
				}
				if (num != 0 && num != 18)
				{
					__Error.WinIOError(num, userPath);
				}
			}
			if (!Win32Native.RemoveDirectory(fullPath))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
				}
				if (num == 5)
				{
					throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", new object[]
					{
						userPath
					}));
				}
				if (num == 3 && !throwOnTopLevelDirectoryNotFound)
				{
					return;
				}
				__Error.WinIOError(num, fullPath);
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0004ACDC File Offset: 0x00048EDC
		[SecurityCritical]
		private static SafeFileHandle OpenHandle(string path)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			string pathRoot = Path.GetPathRoot(fullPathInternal);
			if (pathRoot == fullPathInternal && pathRoot[1] == Path.VolumeSeparatorChar)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PathIsVolume"));
			}
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, Directory.GetDemandDir(fullPathInternal, true), false, false);
			SafeFileHandle safeFileHandle = Win32Native.SafeCreateFile(fullPathInternal, 1073741824, FileShare.Write | FileShare.Delete, null, FileMode.Open, 33554432, IntPtr.Zero);
			if (safeFileHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
			return safeFileHandle;
		}

		// Token: 0x04000819 RID: 2073
		private const int FILE_ATTRIBUTE_DIRECTORY = 16;

		// Token: 0x0400081A RID: 2074
		private const int GENERIC_WRITE = 1073741824;

		// Token: 0x0400081B RID: 2075
		private const int FILE_SHARE_WRITE = 2;

		// Token: 0x0400081C RID: 2076
		private const int FILE_SHARE_DELETE = 4;

		// Token: 0x0400081D RID: 2077
		private const int OPEN_EXISTING = 3;

		// Token: 0x0400081E RID: 2078
		private const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

		// Token: 0x02000AE0 RID: 2784
		internal sealed class SearchData
		{
			// Token: 0x06006976 RID: 26998 RVA: 0x0016BDEA File Offset: 0x00169FEA
			public SearchData(string fullPath, string userPath, SearchOption searchOption)
			{
				this.fullPath = fullPath;
				this.userPath = userPath;
				this.searchOption = searchOption;
			}

			// Token: 0x040031D0 RID: 12752
			public readonly string fullPath;

			// Token: 0x040031D1 RID: 12753
			public readonly string userPath;

			// Token: 0x040031D2 RID: 12754
			public readonly SearchOption searchOption;
		}
	}
}
