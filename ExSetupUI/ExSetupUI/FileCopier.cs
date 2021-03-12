using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Setup.Common;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200001B RID: 27
	internal class FileCopier
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000762C File Offset: 0x0000582C
		public FileCopier(string sourceDirectory, string destinationDirectory)
		{
			this.sourceDirectory = sourceDirectory;
			this.destinationDirectory = destinationDirectory;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600013E RID: 318 RVA: 0x00007668 File Offset: 0x00005868
		// (remove) Token: 0x0600013F RID: 319 RVA: 0x000076A0 File Offset: 0x000058A0
		public event FileCopierErrorHandler FileCopierErrorEvent;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000140 RID: 320 RVA: 0x000076D8 File Offset: 0x000058D8
		// (remove) Token: 0x06000141 RID: 321 RVA: 0x00007710 File Offset: 0x00005910
		public event FileCopierBeforeFileCopyHandler FileCopierBeforeFileCopyEvent;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000142 RID: 322 RVA: 0x00007748 File Offset: 0x00005948
		// (remove) Token: 0x06000143 RID: 323 RVA: 0x00007780 File Offset: 0x00005980
		public event FileCopierProgressChangeHandler FileCopierProgressEvent;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000144 RID: 324 RVA: 0x000077B8 File Offset: 0x000059B8
		// (remove) Token: 0x06000145 RID: 325 RVA: 0x000077F0 File Offset: 0x000059F0
		public event FileCopierCompletedHandler FileCopierCompletedEvent;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000146 RID: 326 RVA: 0x00007828 File Offset: 0x00005A28
		// (remove) Token: 0x06000147 RID: 327 RVA: 0x00007860 File Offset: 0x00005A60
		public event FileCopierCanceledHandler FileCopierCancelEvent;

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00007895 File Offset: 0x00005A95
		public int PercentageCopiedFiles
		{
			get
			{
				if (this.totalDiskSpaceNeeded > 0L)
				{
					return (int)(this.totalFileSpaceCopied * 100L / this.totalDiskSpaceNeeded);
				}
				return 0;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000078B5 File Offset: 0x00005AB5
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000078BD File Offset: 0x00005ABD
		public string CurrentFileName { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000078C6 File Offset: 0x00005AC6
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000078CE File Offset: 0x00005ACE
		public int TotalFileCopied { get; private set; }

		// Token: 0x0600014D RID: 333 RVA: 0x000078D8 File Offset: 0x00005AD8
		public void StartFileCopying()
		{
			SetupLogger.Log(Strings.StartFileCopying);
			this.cancelFileCopy = false;
			this.TotalFileCopied = 0;
			this.CopyFiles();
			if (this.cancelFileCopy)
			{
				try
				{
					this.DeleteTempFiles();
				}
				catch (Exception e)
				{
					SetupLogger.LogError(e);
				}
				this.FileCopierCancelEvent();
			}
			else
			{
				this.FileCopierCompletedEvent();
			}
			SetupLogger.Log(Strings.FileCopyingFinished);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007954 File Offset: 0x00005B54
		public void StopFileCopying()
		{
			SetupLogger.Log(Strings.CancelFileCopying);
			this.cancelFileCopy = true;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000796C File Offset: 0x00005B6C
		internal FileInfo[] GetFiles(string path)
		{
			List<FileInfo> list = new List<FileInfo>();
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			IEnumerable<DirectoryInfo> enumerable = directoryInfo.EnumerateDirectories("*", SearchOption.AllDirectories);
			foreach (DirectoryInfo directoryInfo2 in enumerable)
			{
				if (!this.excludePaths.Contains(directoryInfo2.Name.ToLower()))
				{
					List<FileInfo> collection = (List<FileInfo>)directoryInfo.EnumerateFileSystemInfos();
					list.AddRange(collection);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007A00 File Offset: 0x00005C00
		internal long GetFreeDiskSpace()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
			DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(folderPath));
			return driveInfo.AvailableFreeSpace;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007A28 File Offset: 0x00005C28
		internal long GetTotalFilesSize(IEnumerable<DirectoryInfo> directoryCollection)
		{
			long num = 0L;
			foreach (DirectoryInfo directoryInfo in directoryCollection)
			{
				if (!this.excludePaths.Contains(directoryInfo.Name.ToLower()))
				{
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
					{
						num += fileInfo.Length;
					}
				}
			}
			return num;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007AB0 File Offset: 0x00005CB0
		internal void CopyFiles()
		{
			try
			{
				this.totalFileSpaceCopied = 0L;
				SetupLogger.Log(Strings.CopyingDirectoryFromTo(this.sourceDirectory, this.destinationDirectory));
				DirectoryInfo directoryInfo = new DirectoryInfo(this.sourceDirectory);
				IEnumerable<DirectoryInfo> collection = directoryInfo.EnumerateDirectories("*", SearchOption.AllDirectories);
				List<DirectoryInfo> list = new List<DirectoryInfo>(collection);
				list.Add(directoryInfo);
				this.totalDiskSpaceNeeded = this.GetTotalFilesSize(list);
				SetupLogger.Log(Strings.DiskSpaceRequired(this.totalDiskSpaceNeeded.ToString()));
				if (this.totalDiskSpaceNeeded == 0L)
				{
					SetupLogger.Log(Strings.NoFilesToCopy(this.sourceDirectory));
					throw new FileCopierReadFileException(this.sourceDirectory);
				}
				long freeDiskSpace = this.GetFreeDiskSpace();
				SetupLogger.Log(Strings.DiskSpaceAvailable(freeDiskSpace.ToString()));
				if (freeDiskSpace < this.totalDiskSpaceNeeded)
				{
					SetupLogger.Log(Strings.InsufficientDiskSpace);
					throw new FileCopierInsufficientDiskSpaceException();
				}
				foreach (DirectoryInfo directoryInfo2 in list)
				{
					if (!this.excludePaths.Contains(directoryInfo2.Name.ToLower()))
					{
						string text = directoryInfo2.FullName.Replace(this.sourceDirectory, this.destinationDirectory);
						if (!Directory.Exists(text))
						{
							Directory.CreateDirectory(text);
						}
						foreach (FileInfo fileInfo in directoryInfo2.GetFiles())
						{
							string text2 = string.Format("{0}\\{1}", text, fileInfo.Name);
							this.CurrentFileName = fileInfo.Name;
							this.FileCopierBeforeFileCopyEvent();
							if (!File.Exists(text2))
							{
								fileInfo.CopyTo(text2);
							}
							this.TotalFileCopied++;
							this.totalFileSpaceCopied += fileInfo.Length;
							this.FileCopierProgressEvent();
							if (this.cancelFileCopy)
							{
								break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				SetupLogger.LogError(ex);
				this.FindErrorTypeAndNotify(ex);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007CE0 File Offset: 0x00005EE0
		private void DeleteTempFiles()
		{
			SetupLogger.Log(Strings.DeletingTempFiles);
			if (Directory.Exists(this.destinationDirectory))
			{
				Directory.Delete(this.destinationDirectory, true);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007D05 File Offset: 0x00005F05
		private void FindErrorTypeAndNotify(object error)
		{
			if (this.FileCopierErrorEvent != null)
			{
				SetupWizardEventArgs.ErrorException = (Exception)error;
				this.FileCopierErrorEvent(SetupWizardEventArgs.ErrorException);
				this.FileCopierCancelEvent();
			}
		}

		// Token: 0x040000A3 RID: 163
		private readonly string sourceDirectory;

		// Token: 0x040000A4 RID: 164
		private readonly string destinationDirectory;

		// Token: 0x040000A5 RID: 165
		private volatile bool cancelFileCopy;

		// Token: 0x040000A6 RID: 166
		private long totalDiskSpaceNeeded;

		// Token: 0x040000A7 RID: 167
		private long totalFileSpaceCopied;

		// Token: 0x040000A8 RID: 168
		private List<string> excludePaths = new List<string>
		{
			"search"
		};
	}
}
