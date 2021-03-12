using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;

namespace Microsoft.Search.Platform.Parallax.DataLoad
{
	// Token: 0x0200000C RID: 12
	internal sealed class FileChangesAccumulator : IDisposable
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002DBC File Offset: 0x00000FBC
		internal FileChangesAccumulator(string directoryPath, string filter, int accumulationTimeoutInMs, bool processSubdirectories = false)
		{
			if (directoryPath == null)
			{
				throw new ArgumentNullException("directoryPath");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			this.changesListAccessGate = new object();
			this.filter = filter;
			this.directoryPath = directoryPath;
			this.processSubdirectories = processSubdirectories;
			this.accumulatedFileChanges = new List<FileChangesAccumulator.Change>();
			this.fileChangeAccumulationTimer = new Timer((double)accumulationTimeoutInMs)
			{
				AutoReset = false
			};
			this.fileChangeAccumulationTimer.Elapsed += this.ApplyFileChanges;
			this.fileWatcher = new FileSystemWatcher(this.directoryPath, this.filter)
			{
				IncludeSubdirectories = this.processSubdirectories
			};
			this.fileWatcher.Changed += this.LoadNewFile;
			this.fileWatcher.Created += this.LoadNewFile;
			this.fileWatcher.Deleted += this.FileDeleted;
			this.fileWatcher.Renamed += this.FileRenamed;
			this.fileWatcher.Error += this.FileWatcherError;
			this.fileWatcher.EnableRaisingEvents = true;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000037 RID: 55 RVA: 0x00002EE8 File Offset: 0x000010E8
		// (remove) Token: 0x06000038 RID: 56 RVA: 0x00002F20 File Offset: 0x00001120
		internal event EventHandler<IEnumerable<string>> ChangesAccumulated;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000039 RID: 57 RVA: 0x00002F58 File Offset: 0x00001158
		// (remove) Token: 0x0600003A RID: 58 RVA: 0x00002F90 File Offset: 0x00001190
		internal event EventHandler<Exception> ErrorDetected;

		// Token: 0x0600003B RID: 59 RVA: 0x00002FC5 File Offset: 0x000011C5
		public void Dispose()
		{
			this.fileWatcher.Dispose();
			this.fileChangeAccumulationTimer.Dispose();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002FE0 File Offset: 0x000011E0
		private void OnChangesAccumulated(IEnumerable<string> changedFiles)
		{
			EventHandler<IEnumerable<string>> changesAccumulated = this.ChangesAccumulated;
			if (changesAccumulated != null)
			{
				changesAccumulated(this, changedFiles);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003014 File Offset: 0x00001214
		private void ApplyFileChanges(object sender, ElapsedEventArgs e)
		{
			List<FileChangesAccumulator.Change> list;
			lock (this.changesListAccessGate)
			{
				list = this.accumulatedFileChanges;
				this.accumulatedFileChanges = new List<FileChangesAccumulator.Change>();
			}
			this.ProcessOverlapping(list);
			IEnumerable<string> changedFiles = (from _ in list
			where !_.IsOverlapped
			select _.FilePath).ToArray<string>();
			this.OnChangesAccumulated(changedFiles);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000030BC File Offset: 0x000012BC
		private void ProcessOverlapping(List<FileChangesAccumulator.Change> currentlyAccumulatedChanges)
		{
			for (int i = currentlyAccumulatedChanges.Count; i > 1; i--)
			{
				FileChangesAccumulator.Change change = currentlyAccumulatedChanges[i - 1];
				if (!change.IsOverlapped)
				{
					for (int j = i - 1; j > 0; j--)
					{
						FileChangesAccumulator.Change change2 = currentlyAccumulatedChanges[j - 1];
						if (StringComparer.InvariantCultureIgnoreCase.Compare(change2.FilePath, change.FilePath) == 0)
						{
							change2.IsOverlapped = true;
						}
					}
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003123 File Offset: 0x00001323
		private void RestartTimer()
		{
			this.fileChangeAccumulationTimer.Stop();
			this.fileChangeAccumulationTimer.Start();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000313B File Offset: 0x0000133B
		private void StopTimer()
		{
			this.fileChangeAccumulationTimer.Stop();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003148 File Offset: 0x00001348
		private void FileDeleted(object sender, FileSystemEventArgs e)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000314C File Offset: 0x0000134C
		private void LoadNewFile(object sender, FileSystemEventArgs e)
		{
			this.RestartTimer();
			FileChangesAccumulator.Change item = new FileChangesAccumulator.Change(e.FullPath);
			lock (this.changesListAccessGate)
			{
				this.accumulatedFileChanges.Add(item);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000031A4 File Offset: 0x000013A4
		private void FileRenamed(object sender, RenamedEventArgs e)
		{
			this.RestartTimer();
			string directoryName = Path.GetDirectoryName(e.FullPath);
			string fileName = Path.GetFileName(e.FullPath);
			FileChangesAccumulator.Change change = null;
			if (this.IsDirectoryMonitored(directoryName) && this.FitsMask(fileName, this.filter))
			{
				change = new FileChangesAccumulator.Change(e.FullPath);
			}
			lock (this.changesListAccessGate)
			{
				if (change != null)
				{
					this.accumulatedFileChanges.Add(change);
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003234 File Offset: 0x00001434
		private void FileWatcherError(object sender, ErrorEventArgs e)
		{
			this.StopTimer();
			EventHandler<Exception> errorDetected = this.ErrorDetected;
			if (errorDetected != null)
			{
				errorDetected(this, e.GetException());
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003260 File Offset: 0x00001460
		private bool IsDirectoryMonitored(string directoryName)
		{
			bool flag = this.directoryPath == directoryName;
			if (flag)
			{
				return true;
			}
			bool flag2 = directoryName.IndexOf(this.directoryPath + "\\", StringComparison.InvariantCulture) != -1;
			return this.processSubdirectories && flag2;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000032A8 File Offset: 0x000014A8
		private bool FitsMask(string fileName, string fileMask)
		{
			if (this.fileNameFilter == null)
			{
				string pattern = "^" + fileMask.Replace(".", "\\.").Replace("*", ".*").Replace("?", ".") + "$";
				this.fileNameFilter = new Regex(pattern, RegexOptions.IgnoreCase);
			}
			return this.fileNameFilter.IsMatch(fileName);
		}

		// Token: 0x04000019 RID: 25
		private readonly string directoryPath;

		// Token: 0x0400001A RID: 26
		private readonly string filter;

		// Token: 0x0400001B RID: 27
		private readonly bool processSubdirectories;

		// Token: 0x0400001C RID: 28
		private readonly Timer fileChangeAccumulationTimer;

		// Token: 0x0400001D RID: 29
		private readonly FileSystemWatcher fileWatcher;

		// Token: 0x0400001E RID: 30
		private readonly object changesListAccessGate;

		// Token: 0x0400001F RID: 31
		private List<FileChangesAccumulator.Change> accumulatedFileChanges;

		// Token: 0x04000020 RID: 32
		private Regex fileNameFilter;

		// Token: 0x0200000D RID: 13
		[DebuggerDisplay("File: {FilePath}; IsOverlapped: {IsOverlapped}")]
		private class Change
		{
			// Token: 0x06000049 RID: 73 RVA: 0x00003314 File Offset: 0x00001514
			public Change(string fileFullPath)
			{
				this.FilePath = fileFullPath;
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600004A RID: 74 RVA: 0x00003323 File Offset: 0x00001523
			// (set) Token: 0x0600004B RID: 75 RVA: 0x0000332B File Offset: 0x0000152B
			public string FilePath { get; private set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600004C RID: 76 RVA: 0x00003334 File Offset: 0x00001534
			// (set) Token: 0x0600004D RID: 77 RVA: 0x0000333C File Offset: 0x0000153C
			public bool IsOverlapped { get; set; }
		}
	}
}
