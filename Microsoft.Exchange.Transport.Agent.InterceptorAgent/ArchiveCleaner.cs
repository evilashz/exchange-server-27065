using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000003 RID: 3
	internal class ArchiveCleaner : IDisposable
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000251A File Offset: 0x0000071A
		public ArchiveCleaner(string archivePath, TimeSpan maximumArchivedItemAge, int maximumArchivedItemCount, long maximumArchiveSize)
		{
			this.archivePath = archivePath;
			this.maximumArchivedItemAge = maximumArchivedItemAge;
			this.maximumArchivedItemCount = maximumArchivedItemCount;
			this.maximumArchiveSize = maximumArchiveSize;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000253F File Offset: 0x0000073F
		public void StartMonitoring(TimeSpan cleanupInterval)
		{
			if (this.timer != null)
			{
				this.timer.Change(cleanupInterval, cleanupInterval);
				return;
			}
			this.timer = new Timer(new TimerCallback(this.CleanupProcedure), null, cleanupInterval, cleanupInterval);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002572 File Offset: 0x00000772
		public void StopMonitoring()
		{
			if (this.timer != null)
			{
				this.timer.Change(-1, -1);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000258A File Offset: 0x0000078A
		public void Dispose()
		{
			if (this.timer != null)
			{
				this.StopMonitoring();
				this.timer.Dispose();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025A8 File Offset: 0x000007A8
		private static void ReduceTotalArchiveSize(List<ArchiveCleaner.ArchivedItem> items, long targetSize)
		{
			long num = 0L;
			int num2 = 0;
			while (num2 < items.Count && (num += items[num2].Size) <= targetSize)
			{
				num2++;
			}
			for (int i = num2; i < items.Count; i++)
			{
				items[i].Delete();
			}
			items.RemoveRange(num2, items.Count - num2);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002608 File Offset: 0x00000808
		private static void ReduceTotalArchivedItemsCount(List<ArchiveCleaner.ArchivedItem> items, int targetCount)
		{
			if (items.Count <= targetCount)
			{
				return;
			}
			for (int i = targetCount; i < items.Count; i++)
			{
				items[i].Delete();
			}
			items.RemoveRange(targetCount, items.Count - targetCount);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000264C File Offset: 0x0000084C
		private static void DeleteOldArchivedItems(IEnumerable<ArchiveCleaner.ArchivedItem> items, TimeSpan maximumAge)
		{
			foreach (ArchiveCleaner.ArchivedItem archivedItem in items)
			{
				archivedItem.DeleteIfOlderThan(maximumAge);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026B8 File Offset: 0x000008B8
		private static long ScanDirectory(string path, out List<ArchiveCleaner.ArchivedItem> items)
		{
			ConcurrentBag<ArchiveCleaner.ArchivedItem> concurrentBag = new ConcurrentBag<ArchiveCleaner.ArchivedItem>();
			long result = ArchiveCleaner.ScanDirectory(path, concurrentBag);
			List<ArchiveCleaner.ArchivedItem> list;
			items = (list = concurrentBag.ToList<ArchiveCleaner.ArchivedItem>());
			list.Sort((ArchiveCleaner.ArchivedItem item1, ArchiveCleaner.ArchivedItem item2) => item1.CreationTime.CompareTo(item2.CreationTime));
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002754 File Offset: 0x00000954
		private static long ScanDirectory(string path, ConcurrentBag<ArchiveCleaner.ArchivedItem> concurrentBag)
		{
			long totalSize = 0L;
			foreach (string text in Directory.GetFiles(path, "*.eml"))
			{
				long length = new FileInfo(text).Length;
				concurrentBag.Add(new ArchiveCleaner.ArchivedItem(text, length));
				Interlocked.Add(ref totalSize, length);
			}
			string[] directories = Directory.GetDirectories(path);
			Parallel.For<long>(0, directories.Length, () => 0L, delegate(int i, ParallelLoopState loop, long subtotal)
			{
				if ((File.GetAttributes(directories[i]) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
				{
					subtotal += ArchiveCleaner.ScanDirectory(directories[i], concurrentBag);
					return subtotal;
				}
				return 0L;
			}, delegate(long x)
			{
				Interlocked.Add(ref totalSize, x);
			});
			return totalSize;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000281C File Offset: 0x00000A1C
		private static void DeleteEmptySubDirectories(string path)
		{
			foreach (string path2 in Directory.GetDirectories(path))
			{
				ArchiveCleaner.DeleteDirectoryIfEmpty(path2);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002848 File Offset: 0x00000A48
		private static void DeleteDirectoryIfEmpty(string path)
		{
			foreach (string path2 in Directory.GetDirectories(path))
			{
				ArchiveCleaner.DeleteDirectoryIfEmpty(path2);
			}
			if (Directory.GetDirectories(path).Length == 0 && Directory.GetFiles(path).Length == 0)
			{
				try
				{
					Directory.Delete(path, false);
				}
				catch (Exception arg)
				{
					ExTraceGlobals.InterceptorAgentTracer.TraceError<string, Exception>((long)typeof(ArchiveCleaner.ArchivedItem).GetHashCode(), "Error deleting directory '{0}': {1}", path, arg);
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028C8 File Offset: 0x00000AC8
		private void CleanupProcedure(object state)
		{
			using (Mutex mutex = new Mutex(false, "ArchiveCleanerMutex_FBA1B140EAEA42C286369027B6169594"))
			{
				if (mutex.WaitOne(0))
				{
					ExTraceGlobals.InterceptorAgentTracer.TraceInformation(0, (long)this.GetHashCode(), "Starting interceptor archive cleaunup. Path: {0}; Max item age: {1}; Max item count: {2}; Max archive size: {3}", new object[]
					{
						this.archivePath,
						this.maximumArchivedItemAge,
						this.maximumArchivedItemCount,
						this.maximumArchiveSize
					});
					try
					{
						List<ArchiveCleaner.ArchivedItem> list;
						long num = ArchiveCleaner.ScanDirectory(this.archivePath, out list);
						if (this.maximumArchiveSize > 0L && num > this.maximumArchiveSize)
						{
							ArchiveCleaner.ReduceTotalArchiveSize(list, this.maximumArchiveSize);
						}
						if (this.maximumArchivedItemCount > 0 && list.Count > this.maximumArchivedItemCount)
						{
							ArchiveCleaner.ReduceTotalArchivedItemsCount(list, this.maximumArchivedItemCount);
						}
						ArchiveCleaner.DeleteOldArchivedItems(list, this.maximumArchivedItemAge);
						ArchiveCleaner.DeleteEmptySubDirectories(this.archivePath);
					}
					catch (Exception arg)
					{
						ExTraceGlobals.InterceptorAgentTracer.TraceError<Exception>((long)this.GetHashCode(), "Error while cleaning interceptor archive: {0}", arg);
					}
					finally
					{
						mutex.ReleaseMutex();
					}
				}
			}
		}

		// Token: 0x0400000A RID: 10
		private const string CleanupProcMutexName = "ArchiveCleanerMutex_FBA1B140EAEA42C286369027B6169594";

		// Token: 0x0400000B RID: 11
		private readonly string archivePath;

		// Token: 0x0400000C RID: 12
		private readonly TimeSpan maximumArchivedItemAge;

		// Token: 0x0400000D RID: 13
		private readonly int maximumArchivedItemCount;

		// Token: 0x0400000E RID: 14
		private readonly long maximumArchiveSize;

		// Token: 0x0400000F RID: 15
		private Timer timer;

		// Token: 0x02000004 RID: 4
		private class ArchivedItem
		{
			// Token: 0x0600001D RID: 29 RVA: 0x00002A08 File Offset: 0x00000C08
			public ArchivedItem(string path, long size)
			{
				this.Path = path;
				this.Size = size;
				this.CreationTime = File.GetCreationTimeUtc(path);
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001E RID: 30 RVA: 0x00002A2A File Offset: 0x00000C2A
			// (set) Token: 0x0600001F RID: 31 RVA: 0x00002A32 File Offset: 0x00000C32
			public string Path { get; private set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002A3B File Offset: 0x00000C3B
			// (set) Token: 0x06000021 RID: 33 RVA: 0x00002A43 File Offset: 0x00000C43
			public long Size { get; private set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000022 RID: 34 RVA: 0x00002A4C File Offset: 0x00000C4C
			// (set) Token: 0x06000023 RID: 35 RVA: 0x00002A54 File Offset: 0x00000C54
			public DateTime CreationTime { get; private set; }

			// Token: 0x06000024 RID: 36 RVA: 0x00002A5D File Offset: 0x00000C5D
			public void DeleteIfOlderThan(TimeSpan age)
			{
				if (DateTime.UtcNow - this.CreationTime >= age)
				{
					this.Delete();
				}
			}

			// Token: 0x06000025 RID: 37 RVA: 0x00002A80 File Offset: 0x00000C80
			public void Delete()
			{
				try
				{
					FileAttributes fileAttributes = File.GetAttributes(this.Path);
					if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						fileAttributes &= ~FileAttributes.ReadOnly;
						File.SetAttributes(this.Path, fileAttributes);
					}
					File.Delete(this.Path);
				}
				catch (Exception arg)
				{
					ExTraceGlobals.InterceptorAgentTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Error deleting file '{0}': {1}", this.Path, arg);
				}
			}
		}
	}
}
