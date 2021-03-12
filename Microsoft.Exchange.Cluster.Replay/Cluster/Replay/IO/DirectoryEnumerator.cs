using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Cluster.Replay.IO
{
	// Token: 0x020000E0 RID: 224
	internal class DirectoryEnumerator : DisposeTrackableBase
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x0002A804 File Offset: 0x00028A04
		public DirectoryEnumerator(DirectoryInfo path, bool recurse = false, bool includeRecycleBin = false)
		{
			this.m_path = path;
			this.m_recurse = recurse;
			this.m_includeRecycleBin = includeRecycleBin;
			this.m_realDirectories = new List<string>();
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0002A82C File Offset: 0x00028A2C
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0002A834 File Offset: 0x00028A34
		public bool ReturnBaseNames { get; set; }

		// Token: 0x06000924 RID: 2340 RVA: 0x0002AC28 File Offset: 0x00028E28
		public IEnumerable<string> EnumerateFiles(string filter, Predicate<NativeMethods.WIN32_FIND_DATA> extendedFilter = null)
		{
			if (!string.Equals(filter, "*"))
			{
				if (this.m_recurse)
				{
					bool returnBaseNames = this.ReturnBaseNames;
					try
					{
						this.ReturnBaseNames = false;
						IEnumerable<string> directories = this.EnumerateDirectories("*", extendedFilter);
						foreach (string dir in directories)
						{
							using (DirectoryEnumerator dirEnum = new DirectoryEnumerator(new DirectoryInfo(dir), false, this.m_includeRecycleBin))
							{
								dirEnum.ReturnBaseNames = returnBaseNames;
								foreach (string item in dirEnum.GetItemsInternal(filter, DirectoryEnumerator.EnumerationType.Files, extendedFilter))
								{
									yield return item;
								}
							}
						}
					}
					finally
					{
						this.ReturnBaseNames = returnBaseNames;
					}
					goto IL_1CD;
				}
			}
			string itemName;
			NativeMethods.WIN32_FIND_DATA findData;
			while (this.GetNextItem(filter, DirectoryEnumerator.EnumerationType.Files, out itemName, out findData, extendedFilter))
			{
				yield return itemName;
			}
			IL_1CD:
			yield break;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002AC53 File Offset: 0x00028E53
		public IEnumerable<string> EnumerateDirectories(string filter, Predicate<NativeMethods.WIN32_FIND_DATA> extendedFilter = null)
		{
			return this.GetItemsInternal(filter, DirectoryEnumerator.EnumerationType.Directories, extendedFilter);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002AC5E File Offset: 0x00028E5E
		public IEnumerable<string> EnumerateFilesAndDirectories(string filter, Predicate<NativeMethods.WIN32_FIND_DATA> extendedFilter = null)
		{
			return this.GetItemsInternal(filter, DirectoryEnumerator.EnumerationType.FilesOrDirectories, extendedFilter);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002AC69 File Offset: 0x00028E69
		public IEnumerable<string> EnumerateFilesAndDirectoriesExcludingHiddenAndSystem(string filter)
		{
			return this.GetItemsInternal(filter, DirectoryEnumerator.EnumerationType.FilesOrDirectories, DirectoryEnumerator.ExcludeHiddenAndSystemFilter);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0002AC78 File Offset: 0x00028E78
		public bool GetNextFile(string filter, out string fileName)
		{
			return this.GetNextItem(filter, DirectoryEnumerator.EnumerationType.Files, out fileName);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0002AC83 File Offset: 0x00028E83
		public bool GetNextDirectory(string filter, out string directoryName)
		{
			return this.GetNextItem(filter, DirectoryEnumerator.EnumerationType.Directories, out directoryName);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0002AC8E File Offset: 0x00028E8E
		public bool GetNextFileExtendedInfo(string filter, out string fileName, out NativeMethods.WIN32_FIND_DATA findData)
		{
			return this.GetNextItem(filter, DirectoryEnumerator.EnumerationType.Files, out fileName, out findData, null);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0002AC9B File Offset: 0x00028E9B
		public bool GetNextDirectoryExtendedInfo(string filter, out string directoryName, out NativeMethods.WIN32_FIND_DATA findData)
		{
			return this.GetNextItem(filter, DirectoryEnumerator.EnumerationType.Directories, out directoryName, out findData, null);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002ACA8 File Offset: 0x00028EA8
		protected virtual LocalizedString GetIOExceptionMessage(string directoryName, string apiName, string ioErrorMessage, int win32ErrorCode)
		{
			return ReplayStrings.DirectoryEnumeratorIOError(apiName, ioErrorMessage, win32ErrorCode, directoryName);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002ACB4 File Offset: 0x00028EB4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.ResetFindHandle();
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002ACBF File Offset: 0x00028EBF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DirectoryEnumerator>(this);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002B038 File Offset: 0x00029238
		private IEnumerable<string> GetItemsInternal(string filter, DirectoryEnumerator.EnumerationType enumType, Predicate<NativeMethods.WIN32_FIND_DATA> extendedFilter = null)
		{
			string itemName;
			NativeMethods.WIN32_FIND_DATA findData;
			while (this.GetNextItem(filter, enumType, out itemName, out findData, extendedFilter))
			{
				yield return itemName;
			}
			if (this.m_recurse)
			{
				foreach (string dir in this.m_realDirectories)
				{
					string fullPath = Path.Combine(this.m_path.FullName, dir);
					using (DirectoryEnumerator dirEnum = new DirectoryEnumerator(new DirectoryInfo(fullPath), this.m_recurse, this.m_includeRecycleBin))
					{
						foreach (string item in dirEnum.GetItemsInternal(filter, enumType, extendedFilter))
						{
							yield return item;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002B06C File Offset: 0x0002926C
		private bool GetNextItem(string filter, DirectoryEnumerator.EnumerationType enumType, out string itemName)
		{
			NativeMethods.WIN32_FIND_DATA win32_FIND_DATA;
			return this.GetNextItem(filter, enumType, out itemName, out win32_FIND_DATA, null);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002B088 File Offset: 0x00029288
		private bool GetNextItem(string filter, DirectoryEnumerator.EnumerationType enumType, out string itemName, out NativeMethods.WIN32_FIND_DATA findData, Predicate<NativeMethods.WIN32_FIND_DATA> extendedFilter = null)
		{
			bool flag = false;
			bool flag2 = false;
			itemName = null;
			findData = default(NativeMethods.WIN32_FIND_DATA);
			findData.FileName = string.Empty;
			findData.FileSizeHigh = 0U;
			findData.FileSizeLow = 0U;
			bool flag3;
			do
			{
				if (this.m_safeFindHandle == null)
				{
					string fileName = Path.Combine(this.m_path.FullName, filter);
					this.m_safeFindHandle = NativeMethods.FindFirstFile(fileName, out findData);
					if (this.m_safeFindHandle == null || this.m_safeFindHandle.IsInvalid)
					{
						flag3 = false;
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error != 2)
						{
							this.ThrowIOException(lastWin32Error, "FindFirstFile");
						}
					}
					else
					{
						flag3 = true;
					}
				}
				else
				{
					flag3 = NativeMethods.FindNextFile(this.m_safeFindHandle, out findData);
					if (!flag3)
					{
						int lastWin32Error2 = Marshal.GetLastWin32Error();
						if (lastWin32Error2 != 18)
						{
							this.ThrowIOException(lastWin32Error2, "FindNextFile");
						}
					}
				}
				if (flag3)
				{
					if ((findData.FileAttributes & NativeMethods.FileAttributes.Directory) != NativeMethods.FileAttributes.None && !this.IsSpecialDirectoryName(findData.FileName) && (extendedFilter == null || extendedFilter(findData)))
					{
						flag2 = true;
					}
					switch (enumType)
					{
					case DirectoryEnumerator.EnumerationType.Files:
						if ((findData.FileAttributes & NativeMethods.FileAttributes.Directory) == NativeMethods.FileAttributes.None && NativeMethods.PathMatchSpec(findData.FileName, filter))
						{
							flag = true;
						}
						break;
					case DirectoryEnumerator.EnumerationType.Directories:
						if (flag2)
						{
							flag = true;
						}
						break;
					case DirectoryEnumerator.EnumerationType.FilesOrDirectories:
						if (flag2)
						{
							flag = true;
						}
						else if ((findData.FileAttributes & NativeMethods.FileAttributes.Directory) == NativeMethods.FileAttributes.None && NativeMethods.PathMatchSpec(findData.FileName, filter))
						{
							flag = true;
						}
						break;
					}
					if (this.m_recurse && flag2)
					{
						this.m_realDirectories.Add(findData.FileName);
					}
					if (flag && extendedFilter != null && !extendedFilter(findData))
					{
						flag = false;
					}
				}
			}
			while (flag3 && !flag);
			if (flag)
			{
				if (this.ReturnBaseNames)
				{
					itemName = findData.FileName;
				}
				else
				{
					itemName = Path.Combine(this.m_path.FullName, findData.FileName);
				}
			}
			else
			{
				this.ResetFindHandle();
			}
			return flag;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0002B25B File Offset: 0x0002945B
		private bool IsSpecialDirectoryName(string name)
		{
			return StringUtil.IsEqualIgnoreCase(name, ".") || StringUtil.IsEqualIgnoreCase(name, "..") || (!this.m_includeRecycleBin && StringUtil.IsEqualIgnoreCase(name, "$RECYCLE.BIN"));
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002B28F File Offset: 0x0002948F
		private void ResetFindHandle()
		{
			if (this.m_safeFindHandle != null)
			{
				this.m_safeFindHandle.Close();
				this.m_safeFindHandle = null;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002B2AC File Offset: 0x000294AC
		private void ThrowIOException(int win32ErrorCode, string apiName)
		{
			Exception ex = new Win32Exception(win32ErrorCode);
			throw new IOException(this.GetIOExceptionMessage(this.m_path.FullName, apiName, ex.Message, win32ErrorCode), NativeMethods.HRESULT_FROM_WIN32((uint)win32ErrorCode));
		}

		// Token: 0x040003C3 RID: 963
		public const string FilterAll = "*";

		// Token: 0x040003C4 RID: 964
		private const string CurrentDirectory = ".";

		// Token: 0x040003C5 RID: 965
		private const string ParentDirectory = "..";

		// Token: 0x040003C6 RID: 966
		private const string RecycleBin = "$RECYCLE.BIN";

		// Token: 0x040003C7 RID: 967
		public static readonly Predicate<NativeMethods.WIN32_FIND_DATA> ExcludeHiddenAndSystemFilter = (NativeMethods.WIN32_FIND_DATA findData) => (findData.FileAttributes & NativeMethods.FileAttributes.Hidden) == NativeMethods.FileAttributes.None && (findData.FileAttributes & NativeMethods.FileAttributes.System) == NativeMethods.FileAttributes.None && (findData.FileAttributes & NativeMethods.FileAttributes.ReparsePoint) == NativeMethods.FileAttributes.None;

		// Token: 0x040003C8 RID: 968
		private readonly DirectoryInfo m_path;

		// Token: 0x040003C9 RID: 969
		private readonly bool m_recurse;

		// Token: 0x040003CA RID: 970
		private readonly bool m_includeRecycleBin;

		// Token: 0x040003CB RID: 971
		private SafeFindHandle m_safeFindHandle;

		// Token: 0x040003CC RID: 972
		private List<string> m_realDirectories;

		// Token: 0x020000E1 RID: 225
		private enum EnumerationType
		{
			// Token: 0x040003D0 RID: 976
			Files,
			// Token: 0x040003D1 RID: 977
			Directories,
			// Token: 0x040003D2 RID: 978
			FilesOrDirectories
		}
	}
}
