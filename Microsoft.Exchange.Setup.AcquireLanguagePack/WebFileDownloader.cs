using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000024 RID: 36
	internal class WebFileDownloader
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003F2F File Offset: 0x0000212F
		public WebFileDownloader()
		{
			this.numOfThreads = 0;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003F3E File Offset: 0x0000213E
		public WebFileDownloader(int numOfThreadsToDownload)
		{
			this.userSpecifiedNoThreads = true;
			this.numOfThreads = numOfThreadsToDownload;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000096 RID: 150 RVA: 0x00003F54 File Offset: 0x00002154
		// (remove) Token: 0x06000097 RID: 151 RVA: 0x00003F8C File Offset: 0x0000218C
		public event DownloaderErrorHandler DownloaderErrorEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000098 RID: 152 RVA: 0x00003FC4 File Offset: 0x000021C4
		// (remove) Token: 0x06000099 RID: 153 RVA: 0x00003FFC File Offset: 0x000021FC
		public event DownloadProgressChangeHandler DownloadProgressEvent;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600009A RID: 154 RVA: 0x00004034 File Offset: 0x00002234
		// (remove) Token: 0x0600009B RID: 155 RVA: 0x0000406C File Offset: 0x0000226C
		public event DownloadCompletedHandler DownloadCompletedEvent;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600009C RID: 156 RVA: 0x000040A4 File Offset: 0x000022A4
		// (remove) Token: 0x0600009D RID: 157 RVA: 0x000040DC File Offset: 0x000022DC
		public event DownloadCanceledHandler DownloadCancelEvent;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004111 File Offset: 0x00002311
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00004119 File Offset: 0x00002319
		private long TotalFileSize { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004122 File Offset: 0x00002322
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000412A File Offset: 0x0000232A
		private long TotalDownloaded { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004133 File Offset: 0x00002333
		public int PercentageDownloaded
		{
			get
			{
				if (this.TotalFileSize > 0L)
				{
					return (int)(this.TotalDownloaded * 100L / this.TotalFileSize);
				}
				return 0;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004154 File Offset: 0x00002354
		public void StartDownloading(List<DownloadFileInfo> downloads, string saveTo)
		{
			Logger.LoggerMessage("Getting ready to download...");
			try
			{
				ValidationHelper.ThrowIfNullOrEmpty<DownloadFileInfo>(downloads, "downloads");
				ValidationHelper.ThrowIfDirectoryNotExist(saveTo, "saveTo");
				this.errorFound = false;
				this.cancelDownload = false;
				this.TotalDownloaded = 0L;
				this.QueryDownloadFileInfo(downloads, saveTo);
				List<DownloadFileInfo> list = this.CheckForPreReqsBeforeDownload(downloads, saveTo);
				foreach (DownloadFileInfo downloadFileInfo in list)
				{
					this.currentDownloadFileInfo = downloadFileInfo;
					if (!this.userSpecifiedNoThreads)
					{
						this.numOfThreads = Math.Min(5, Environment.ProcessorCount + 1);
					}
					this.numOfThreads = Math.Min(this.numOfThreads, 5);
					if (this.currentDownloadFileInfo.FileSize < 40960L)
					{
						this.numOfThreads = 1;
					}
					this.allThreads = new List<Thread>();
					this.seg = new Segmentator(this.numOfThreads);
					DownloadParameter[] array = this.seg.SegmentTheFile(this.currentDownloadFileInfo.FileSize);
					Logger.LoggerMessage("Number of Threads: " + this.numOfThreads);
					this.numOfSegments = array.Length;
					for (int i = 0; i < this.numOfSegments; i++)
					{
						Logger.LoggerMessage(string.Concat(new object[]
						{
							"Thread ",
							i,
							" Start Position: ",
							array[i].StartPosition,
							" End Position: ",
							array[i].EndPosition
						}));
						Thread thread = new Thread(new ParameterizedThreadStart(this.DownloadFile));
						thread.Name = i.ToString();
						this.allThreads.Add(thread);
						thread.Start(array[i]);
					}
					this.cleanupThread = new Thread(new ParameterizedThreadStart(this.CleanUp));
					this.cleanupThread.Start(array);
					this.cleanupThread.Join();
				}
				this.DownloadCompletedEvent(list.Count);
			}
			catch (Exception error)
			{
				this.FindErrorTypeAndNotify(error);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000043C8 File Offset: 0x000025C8
		public void StopDownloading()
		{
			Logger.LoggerMessage("User cancelling the download...");
			this.cancelDownload = true;
			Thread.MemoryBarrier();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000043E4 File Offset: 0x000025E4
		private void QueryDownloadFileInfo(List<DownloadFileInfo> downloads, string saveTo)
		{
			for (int i = 0; i < downloads.Count; i++)
			{
				if (downloads[i].UriLink.ToString().IndexOf("http://") != 0)
				{
					throw new WebException(Strings.URLCantBeReached(downloads[i].UriLink.ToString()));
				}
				DownloadFileInfo value = downloads[i];
				HttpProtocol.QueryFileNameSize(ref value);
				downloads[i] = value;
				downloads[i].FilePath = Path.Combine(saveTo, downloads[i].FilePath);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004478 File Offset: 0x00002678
		private List<DownloadFileInfo> CheckForPreReqsBeforeDownload(List<DownloadFileInfo> downloads, string saveTo)
		{
			Logger.LoggerMessage("Checking the prechecks...");
			List<DownloadFileInfo> list = new List<DownloadFileInfo>();
			long num = 0L;
			foreach (DownloadFileInfo downloadFileInfo in downloads)
			{
				if (downloadFileInfo.FileSize <= 1024L)
				{
					throw new WebException(Strings.URLCantBeReached(downloadFileInfo.UriLink.ToString()));
				}
				if (!downloadFileInfo.IsFileNameValid())
				{
					if (!downloadFileInfo.IgnoreInvalidFileName)
					{
						throw new WebException(Strings.URLCantBeReached(downloadFileInfo.UriLink.ToString()));
					}
				}
				else
				{
					num += downloadFileInfo.FileSize;
					list.Add(downloadFileInfo);
				}
			}
			using (DiskSpaceValidator diskSpaceValidator = new DiskSpaceValidator((long)((double)num * 1.2), saveTo))
			{
				if (!diskSpaceValidator.Validate())
				{
					throw new IOException(Strings.InsufficientDiskSpace((double)num * 1.2));
				}
			}
			this.TotalFileSize = num;
			return list;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004590 File Offset: 0x00002790
		private void DownloadFile(object downloadParameters)
		{
			int num = -1;
			DownloadParameter downloadParameter = (DownloadParameter)downloadParameters;
			int num2 = 0;
			try
			{
				using (HttpProtocol httpProtocol = new HttpProtocol())
				{
					using (Stream stream = httpProtocol.GetStream(downloadParameter.StartPosition, downloadParameter.EndPosition, this.currentDownloadFileInfo.UriLink, this.numOfThreads))
					{
						if (stream == null)
						{
							throw new WebException(Strings.UnableToDownload);
						}
						using (FileStream fileStream = new FileStream(downloadParameter.PathToFile, FileMode.Create, FileAccess.Write, FileShare.None))
						{
							byte[] array = new byte[8096];
							while (num != 0)
							{
								num = stream.Read(array, 0, array.Length);
								num2 += num;
								fileStream.Write(array, 0, num);
								fileStream.Flush();
								lock (this)
								{
									this.TotalDownloaded += (long)num;
								}
								this.DownloadProgressEvent();
								if (this.cancelDownload)
								{
									httpProtocol.CancelDownload();
									break;
								}
							}
						}
					}
				}
			}
			catch (Exception error)
			{
				if (!this.errorFound)
				{
					Logger.LoggerMessage(string.Concat(new object[]
					{
						"This threaded has downloaded: ",
						num2,
						" -- Total Downloaded: ",
						this.TotalDownloaded
					}));
					this.errorFound = true;
					this.FindErrorTypeAndNotify(error);
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004740 File Offset: 0x00002940
		private void DeleteTempFiles(DownloadParameter[] downloadParams)
		{
			try
			{
				Logger.LoggerMessage("Deleting the temp files...");
				for (int i = 0; i < this.numOfSegments; i++)
				{
					if (File.Exists(downloadParams[i].PathToFile))
					{
						File.Delete(downloadParams[i].PathToFile);
					}
				}
			}
			catch (IOException ex)
			{
				string message = ex.Message;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000047A8 File Offset: 0x000029A8
		private void CleanUp(object downloadParameters)
		{
			DownloadParameter[] array = (DownloadParameter[])downloadParameters;
			for (int i = 0; i < this.numOfSegments; i++)
			{
				this.allThreads[i].Join();
				Logger.LoggerMessage("Thread: " + i + " is done...");
			}
			Logger.LoggerMessage("All Threads are done...");
			try
			{
				if (!this.cancelDownload && !this.errorFound)
				{
					Logger.LoggerMessage("Attemptiing to append the temp files... ");
					this.AppendFile(array);
				}
				else if (this.cancelDownload)
				{
					this.DownloadCancelEvent();
				}
			}
			catch (Exception error)
			{
				this.FindErrorTypeAndNotify(error);
			}
			finally
			{
				this.DeleteTempFiles(array);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004870 File Offset: 0x00002A70
		private void AppendFile(DownloadParameter[] downloadParameters)
		{
			Logger.LoggerMessage("Appending the files...");
			if (File.Exists(this.currentDownloadFileInfo.FilePath))
			{
				File.Delete(this.currentDownloadFileInfo.FilePath);
			}
			using (FileStream fileStream = new FileStream(this.currentDownloadFileInfo.FilePath, FileMode.Append, FileAccess.Write, FileShare.None))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					for (int i = 0; i < this.numOfSegments; i++)
					{
						using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(downloadParameters[i].PathToFile)))
						{
							byte[] array = new byte[32768];
							int count;
							while ((count = binaryReader.Read(array, 0, array.Length)) > 0)
							{
								binaryWriter.Write(array, 0, count);
							}
						}
					}
				}
			}
			FileInfo fileInfo = new FileInfo(this.currentDownloadFileInfo.FilePath);
			if (fileInfo.Length != this.currentDownloadFileInfo.FileSize)
			{
				throw new ApplicationException(Strings.UnableToDownload);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000499C File Offset: 0x00002B9C
		private void FindErrorTypeAndNotify(object error)
		{
			if (this.DownloaderErrorEvent != null)
			{
				if (error is AsyncCompletedEventArgs)
				{
					AsyncCompletedEventArgs asyncCompletedEventArgs = (AsyncCompletedEventArgs)error;
					WebDownloaderEventArgs.ErrorException = asyncCompletedEventArgs.Error;
				}
				else
				{
					WebDownloaderEventArgs.ErrorException = (Exception)error;
				}
				this.DownloaderErrorEvent(WebDownloaderEventArgs.ErrorException);
			}
		}

		// Token: 0x04000052 RID: 82
		public const int MaxNumOfThreadsAndSegments = 5;

		// Token: 0x04000053 RID: 83
		public const int MinFileSize = 40960;

		// Token: 0x04000054 RID: 84
		private const double InflateDiskSpace = 1.2;

		// Token: 0x04000055 RID: 85
		private const int MinValidFileSize = 1024;

		// Token: 0x04000056 RID: 86
		private List<Thread> allThreads;

		// Token: 0x04000057 RID: 87
		private Thread cleanupThread;

		// Token: 0x04000058 RID: 88
		private int numOfSegments;

		// Token: 0x04000059 RID: 89
		private int numOfThreads;

		// Token: 0x0400005A RID: 90
		private Segmentator seg;

		// Token: 0x0400005B RID: 91
		private DownloadFileInfo currentDownloadFileInfo;

		// Token: 0x0400005C RID: 92
		private volatile bool errorFound;

		// Token: 0x0400005D RID: 93
		private volatile bool cancelDownload;

		// Token: 0x0400005E RID: 94
		private bool userSpecifiedNoThreads;
	}
}
