using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x0200017B RID: 379
	[ComVisible(true)]
	[Serializable]
	public sealed class DirectoryInfo : FileSystemInfo
	{
		// Token: 0x06001751 RID: 5969 RVA: 0x0004AD5C File Offset: 0x00048F5C
		[SecuritySafeCritical]
		public DirectoryInfo(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.Init(path, true);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0004AD7C File Offset: 0x00048F7C
		[SecurityCritical]
		private void Init(string path, bool checkHost)
		{
			if (path.Length == 2 && path[1] == ':')
			{
				this.OriginalPath = ".";
			}
			else
			{
				this.OriginalPath = path;
			}
			string fullPathAndCheckPermissions = Directory.GetFullPathAndCheckPermissions(path, checkHost, FileSecurityStateAccess.Read);
			this.FullPath = fullPathAndCheckPermissions;
			base.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0004ADD8 File Offset: 0x00048FD8
		internal DirectoryInfo(string fullPath, bool junk)
		{
			this.OriginalPath = Path.GetFileName(fullPath);
			this.FullPath = fullPath;
			base.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0004AE0A File Offset: 0x0004900A
		internal DirectoryInfo(string fullPath, string fileName)
		{
			this.OriginalPath = fileName;
			this.FullPath = fullPath;
			base.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0004AE37 File Offset: 0x00049037
		[SecurityCritical]
		private DirectoryInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			Directory.CheckPermissions(string.Empty, this.FullPath, false, FileSecurityStateAccess.Read);
			base.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0004AE6A File Offset: 0x0004906A
		public override string Name
		{
			get
			{
				return DirectoryInfo.GetDirName(this.FullPath);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0004AE77 File Offset: 0x00049077
		public override string FullName
		{
			[SecuritySafeCritical]
			get
			{
				Directory.CheckPermissions(string.Empty, this.FullPath, true, FileSecurityStateAccess.PathDiscovery);
				return this.FullPath;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0004AE91 File Offset: 0x00049091
		internal override string UnsafeGetFullName
		{
			[SecurityCritical]
			get
			{
				Directory.CheckPermissions(string.Empty, this.FullPath, false, FileSecurityStateAccess.PathDiscovery);
				return this.FullPath;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0004AEAC File Offset: 0x000490AC
		public DirectoryInfo Parent
		{
			[SecuritySafeCritical]
			get
			{
				string text = this.FullPath;
				if (text.Length > 3 && text.EndsWith(Path.DirectorySeparatorChar))
				{
					text = this.FullPath.Substring(0, this.FullPath.Length - 1);
				}
				string directoryName = Path.GetDirectoryName(text);
				if (directoryName == null)
				{
					return null;
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(directoryName, false);
				Directory.CheckPermissions(string.Empty, directoryInfo.FullPath, true, FileSecurityStateAccess.Read | FileSecurityStateAccess.PathDiscovery);
				return directoryInfo;
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0004AF18 File Offset: 0x00049118
		public DirectoryInfo CreateSubdirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return this.CreateSubdirectory(path, null);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0004AF30 File Offset: 0x00049130
		[SecuritySafeCritical]
		public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
		{
			return this.CreateSubdirectoryHelper(path, directorySecurity);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0004AF3C File Offset: 0x0004913C
		[SecurityCritical]
		private DirectoryInfo CreateSubdirectoryHelper(string path, object directorySecurity)
		{
			string path2 = Path.InternalCombine(this.FullPath, path);
			string fullPathInternal = Path.GetFullPathInternal(path2);
			if (string.Compare(this.FullPath, 0, fullPathInternal, 0, this.FullPath.Length, StringComparison.OrdinalIgnoreCase) != 0)
			{
				string displayablePath = __Error.GetDisplayablePath(base.DisplayPath, false);
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSubPath", new object[]
				{
					path,
					displayablePath
				}));
			}
			string fullPath = Directory.GetDemandDir(fullPathInternal, true);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPath, false, false);
			Directory.InternalCreateDirectory(fullPathInternal, path, directorySecurity);
			return new DirectoryInfo(fullPathInternal);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0004AFC3 File Offset: 0x000491C3
		public void Create()
		{
			Directory.InternalCreateDirectory(this.FullPath, this.OriginalPath, null, true);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0004AFD8 File Offset: 0x000491D8
		public void Create(DirectorySecurity directorySecurity)
		{
			Directory.InternalCreateDirectory(this.FullPath, this.OriginalPath, directorySecurity, true);
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0004AFF0 File Offset: 0x000491F0
		public override bool Exists
		{
			[SecuritySafeCritical]
			get
			{
				bool result;
				try
				{
					if (this._dataInitialised == -1)
					{
						base.Refresh();
					}
					if (this._dataInitialised != 0)
					{
						result = false;
					}
					else
					{
						result = (this._data.fileAttributes != -1 && (this._data.fileAttributes & 16) != 0);
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0004B054 File Offset: 0x00049254
		public DirectorySecurity GetAccessControl()
		{
			return Directory.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0004B063 File Offset: 0x00049263
		public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
		{
			return Directory.GetAccessControl(this.FullPath, includeSections);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0004B071 File Offset: 0x00049271
		public void SetAccessControl(DirectorySecurity directorySecurity)
		{
			Directory.SetAccessControl(this.FullPath, directorySecurity);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0004B07F File Offset: 0x0004927F
		public FileInfo[] GetFiles(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return this.InternalGetFiles(searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0004B097 File Offset: 0x00049297
		public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return this.InternalGetFiles(searchPattern, searchOption);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0004B0CC File Offset: 0x000492CC
		private FileInfo[] InternalGetFiles(string searchPattern, SearchOption searchOption)
		{
			IEnumerable<FileInfo> collection = FileSystemEnumerableFactory.CreateFileInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
			List<FileInfo> list = new List<FileInfo>(collection);
			return list.ToArray();
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0004B0FA File Offset: 0x000492FA
		public FileInfo[] GetFiles()
		{
			return this.InternalGetFiles("*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0004B108 File Offset: 0x00049308
		public DirectoryInfo[] GetDirectories()
		{
			return this.InternalGetDirectories("*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0004B116 File Offset: 0x00049316
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return this.InternalGetFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0004B12E File Offset: 0x0004932E
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return this.InternalGetFileSystemInfos(searchPattern, searchOption);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0004B164 File Offset: 0x00049364
		private FileSystemInfo[] InternalGetFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			IEnumerable<FileSystemInfo> collection = FileSystemEnumerableFactory.CreateFileSystemInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
			List<FileSystemInfo> list = new List<FileSystemInfo>(collection);
			return list.ToArray();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0004B192 File Offset: 0x00049392
		public FileSystemInfo[] GetFileSystemInfos()
		{
			return this.InternalGetFileSystemInfos("*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0004B1A0 File Offset: 0x000493A0
		public DirectoryInfo[] GetDirectories(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return this.InternalGetDirectories(searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0004B1B8 File Offset: 0x000493B8
		public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return this.InternalGetDirectories(searchPattern, searchOption);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0004B1EC File Offset: 0x000493EC
		private DirectoryInfo[] InternalGetDirectories(string searchPattern, SearchOption searchOption)
		{
			IEnumerable<DirectoryInfo> collection = FileSystemEnumerableFactory.CreateDirectoryInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
			List<DirectoryInfo> list = new List<DirectoryInfo>(collection);
			return list.ToArray();
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0004B21A File Offset: 0x0004941A
		public IEnumerable<DirectoryInfo> EnumerateDirectories()
		{
			return this.InternalEnumerateDirectories("*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0004B228 File Offset: 0x00049428
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return this.InternalEnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0004B240 File Offset: 0x00049440
		public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return this.InternalEnumerateDirectories(searchPattern, searchOption);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0004B274 File Offset: 0x00049474
		private IEnumerable<DirectoryInfo> InternalEnumerateDirectories(string searchPattern, SearchOption searchOption)
		{
			return FileSystemEnumerableFactory.CreateDirectoryInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0004B289 File Offset: 0x00049489
		public IEnumerable<FileInfo> EnumerateFiles()
		{
			return this.InternalEnumerateFiles("*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0004B297 File Offset: 0x00049497
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return this.InternalEnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0004B2AF File Offset: 0x000494AF
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return this.InternalEnumerateFiles(searchPattern, searchOption);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0004B2E3 File Offset: 0x000494E3
		private IEnumerable<FileInfo> InternalEnumerateFiles(string searchPattern, SearchOption searchOption)
		{
			return FileSystemEnumerableFactory.CreateFileInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0004B2F8 File Offset: 0x000494F8
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
		{
			return this.InternalEnumerateFileSystemInfos("*", SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0004B306 File Offset: 0x00049506
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return this.InternalEnumerateFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0004B31E File Offset: 0x0004951E
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			return this.InternalEnumerateFileSystemInfos(searchPattern, searchOption);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0004B352 File Offset: 0x00049552
		private IEnumerable<FileSystemInfo> InternalEnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
		{
			return FileSystemEnumerableFactory.CreateFileSystemInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0004B368 File Offset: 0x00049568
		public DirectoryInfo Root
		{
			[SecuritySafeCritical]
			get
			{
				int rootLength = Path.GetRootLength(this.FullPath);
				string text = this.FullPath.Substring(0, rootLength);
				string fullPath = Directory.GetDemandDir(text, true);
				FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, fullPath, false, false);
				return new DirectoryInfo(text);
			}
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0004B3A8 File Offset: 0x000495A8
		[SecuritySafeCritical]
		public void MoveTo(string destDirName)
		{
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destDirName");
			}
			Directory.CheckPermissions(base.DisplayPath, this.FullPath, true, FileSecurityStateAccess.Read | FileSecurityStateAccess.Write);
			string text = Path.GetFullPathInternal(destDirName);
			if (!text.EndsWith(Path.DirectorySeparatorChar))
			{
				text += Path.DirectorySeparatorChar.ToString();
			}
			Directory.CheckPermissions(destDirName, text, true, FileSecurityStateAccess.Read | FileSecurityStateAccess.Write);
			string text2;
			if (this.FullPath.EndsWith(Path.DirectorySeparatorChar))
			{
				text2 = this.FullPath;
			}
			else
			{
				text2 = this.FullPath + Path.DirectorySeparatorChar.ToString();
			}
			if (string.Compare(text2, text, StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
			}
			string pathRoot = Path.GetPathRoot(text2);
			string pathRoot2 = Path.GetPathRoot(text);
			if (string.Compare(pathRoot, pathRoot2, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
			}
			if (!Win32Native.MoveFile(this.FullPath, destDirName))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
					__Error.WinIOError(num, base.DisplayPath);
				}
				if (num == 5)
				{
					throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", new object[]
					{
						base.DisplayPath
					}));
				}
				__Error.WinIOError(num, string.Empty);
			}
			this.FullPath = text;
			this.OriginalPath = destDirName;
			base.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
			this._dataInitialised = -1;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0004B522 File Offset: 0x00049722
		[SecuritySafeCritical]
		public override void Delete()
		{
			Directory.Delete(this.FullPath, this.OriginalPath, false, true);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0004B537 File Offset: 0x00049737
		[SecuritySafeCritical]
		public void Delete(bool recursive)
		{
			Directory.Delete(this.FullPath, this.OriginalPath, recursive, true);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0004B54C File Offset: 0x0004974C
		public override string ToString()
		{
			return base.DisplayPath;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0004B554 File Offset: 0x00049754
		private static string GetDisplayName(string originalPath, string fullPath)
		{
			string result;
			if (originalPath.Length == 2 && originalPath[1] == ':')
			{
				result = ".";
			}
			else
			{
				result = originalPath;
			}
			return result;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0004B588 File Offset: 0x00049788
		private static string GetDirName(string fullPath)
		{
			string result;
			if (fullPath.Length > 3)
			{
				string path = fullPath;
				if (fullPath.EndsWith(Path.DirectorySeparatorChar))
				{
					path = fullPath.Substring(0, fullPath.Length - 1);
				}
				result = Path.GetFileName(path);
			}
			else
			{
				result = fullPath;
			}
			return result;
		}

		// Token: 0x0400081F RID: 2079
		private string[] demandDir;
	}
}
