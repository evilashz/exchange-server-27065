using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Replay.MountPoint;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Cluster.Replay.IO
{
	// Token: 0x020000E2 RID: 226
	public class DirectorySpaceEnumerator
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x0002B338 File Offset: 0x00029538
		public static IEnumerable<DirectorySpaceEnumerator.DirectorySize> GetTopNDirectories(string rootDirectory, int maxDirectories)
		{
			DirectorySpaceEnumerator directorySpaceEnumerator = new DirectorySpaceEnumerator(rootDirectory, maxDirectories);
			return directorySpaceEnumerator.GetTopNDirectories();
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002B355 File Offset: 0x00029555
		private DirectorySpaceEnumerator(string rootDirectory, int maxDirectories)
		{
			this.m_rootDirectory = rootDirectory;
			this.m_maxDirectories = maxDirectories;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002B458 File Offset: 0x00029658
		private IEnumerable<DirectorySpaceEnumerator.DirectorySize> GetTopNDirectories()
		{
			TopNList<DirectorySpaceEnumerator.DirectorySize> topDirsBySize = new TopNList<DirectorySpaceEnumerator.DirectorySize>(this.m_maxDirectories, Dependencies.Assert);
			MountPointUtil.HandleIOExceptions(delegate
			{
				DirectoryInfo path = new DirectoryInfo(this.m_rootDirectory);
				using (DirectoryEnumerator directoryEnumerator = new DirectoryEnumerator(path, true, true))
				{
					Predicate<NativeMethods.WIN32_FIND_DATA> extendedFilter = (NativeMethods.WIN32_FIND_DATA findData) => (findData.FileAttributes & NativeMethods.FileAttributes.ReparsePoint) == NativeMethods.FileAttributes.None;
					foreach (string dir in directoryEnumerator.EnumerateDirectories("*", extendedFilter))
					{
						this.EnumerateFilesForDirectory(topDirsBySize, dir);
					}
					this.EnumerateFilesForDirectory(topDirsBySize, this.m_rootDirectory);
				}
			});
			return topDirsBySize;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002B53C File Offset: 0x0002973C
		private void EnumerateFilesForDirectory(TopNList<DirectorySpaceEnumerator.DirectorySize> topDirsBySize, string dir)
		{
			MountPointUtil.HandleIOExceptions(delegate
			{
				DirectoryInfo path = new DirectoryInfo(dir);
				using (DirectoryEnumerator directoryEnumerator = new DirectoryEnumerator(path, false, false))
				{
					ulong num = 0UL;
					string text;
					NativeMethods.WIN32_FIND_DATA win32_FIND_DATA;
					while (directoryEnumerator.GetNextFileExtendedInfo("*", out text, out win32_FIND_DATA))
					{
						ulong longFileSize = this.GetLongFileSize(win32_FIND_DATA.FileSizeLow, win32_FIND_DATA.FileSizeHigh);
						num += longFileSize;
					}
					this.InsertIfNecessary(topDirsBySize, dir, num);
				}
			});
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002B578 File Offset: 0x00029778
		private ulong GetLongFileSize(uint low, uint high)
		{
			return (ulong)high << 32 | (ulong)low;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0002B590 File Offset: 0x00029790
		private bool InsertIfNecessary(TopNList<DirectorySpaceEnumerator.DirectorySize> topDirsBySize, string dir, ulong dirSize)
		{
			DirectorySpaceEnumerator.DirectorySize directorySize = new DirectorySpaceEnumerator.DirectorySize(dir, dirSize);
			return topDirsBySize.InsertIfNecessary(directorySize);
		}

		// Token: 0x040003D3 RID: 979
		private readonly int m_maxDirectories;

		// Token: 0x040003D4 RID: 980
		private readonly string m_rootDirectory;

		// Token: 0x020000E3 RID: 227
		public struct DirectorySize : IComparable<DirectorySpaceEnumerator.DirectorySize>
		{
			// Token: 0x0600093D RID: 2365 RVA: 0x0002B5AD File Offset: 0x000297AD
			public DirectorySize(string dirName, ulong size)
			{
				this.Name = dirName;
				this.Size = ByteQuantifiedSize.FromBytes(size);
			}

			// Token: 0x0600093E RID: 2366 RVA: 0x0002B5C2 File Offset: 0x000297C2
			public int CompareTo(DirectorySpaceEnumerator.DirectorySize other)
			{
				return this.Size.CompareTo(other.Size);
			}

			// Token: 0x040003D5 RID: 981
			public string Name;

			// Token: 0x040003D6 RID: 982
			public ByteQuantifiedSize Size;
		}
	}
}
