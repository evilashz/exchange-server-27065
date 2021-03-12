using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport.Pickup
{
	// Token: 0x0200051F RID: 1311
	internal sealed class DirectoryScanner
	{
		// Token: 0x06003D41 RID: 15681 RVA: 0x000FFC20 File Offset: 0x000FDE20
		public DirectoryScanner(string directory, int maxFilesPerMinute, string filter, DirectoryScanner.FileFoundCallBack fileFoundCallBack, DirectoryScanner.CheckDirectoryCallBack checkDirectorCallBack)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directory);
			this.fileFoundCallBack = fileFoundCallBack;
			this.numberOfFilesPerPoll = maxFilesPerMinute / 12;
			this.numberOfFilesOnLastPoll = this.numberOfFilesPerPoll + maxFilesPerMinute % 12;
			this.checkDirectoryCallBack = checkDirectorCallBack;
			this.fullDirectoryPath = directoryInfo.FullName;
			this.fileList = new FileList(this.fullDirectoryPath, filter);
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000FFC83 File Offset: 0x000FDE83
		public void Start()
		{
			this.stopping = false;
			this.filePollTimer = new GuardedTimer(new TimerCallback(this.FilePoll), null, TimeSpan.Zero, TimeSpan.FromSeconds(5.0));
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000FFCB7 File Offset: 0x000FDEB7
		public void Stop()
		{
			this.stopping = true;
			if (this.filePollTimer != null)
			{
				this.filePollTimer.Dispose(true);
				this.filePollTimer = null;
			}
			this.fileList.Dispose();
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000FFCE6 File Offset: 0x000FDEE6
		private bool CheckDirectory()
		{
			return this.checkDirectoryCallBack(this.fullDirectoryPath);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x000FFCFC File Offset: 0x000FDEFC
		private void FilePoll(object empty)
		{
			int num = this.numberOfFilesPerPoll;
			if (++this.currentPollCount == 12)
			{
				num = this.numberOfFilesOnLastPoll;
				this.currentPollCount = 0;
			}
			if (!this.CheckDirectory())
			{
				return;
			}
			int num2 = 0;
			string fullFilePath;
			ulong num3;
			while (this.fileList.GetNextFile(out fullFilePath, out num3))
			{
				if (this.stopping || ++num2 > num || !this.fileFoundCallBack(fullFilePath))
				{
					this.fileList.StopSearch();
					return;
				}
			}
		}

		// Token: 0x04001F2B RID: 7979
		private const int PollingIntervalSeconds = 5;

		// Token: 0x04001F2C RID: 7980
		private const int NumberOfPollsPerMinute = 12;

		// Token: 0x04001F2D RID: 7981
		private readonly int numberOfFilesOnLastPoll;

		// Token: 0x04001F2E RID: 7982
		private readonly int numberOfFilesPerPoll;

		// Token: 0x04001F2F RID: 7983
		private bool stopping;

		// Token: 0x04001F30 RID: 7984
		private GuardedTimer filePollTimer;

		// Token: 0x04001F31 RID: 7985
		private int currentPollCount;

		// Token: 0x04001F32 RID: 7986
		private FileList fileList;

		// Token: 0x04001F33 RID: 7987
		private string fullDirectoryPath;

		// Token: 0x04001F34 RID: 7988
		private DirectoryScanner.CheckDirectoryCallBack checkDirectoryCallBack;

		// Token: 0x04001F35 RID: 7989
		private DirectoryScanner.FileFoundCallBack fileFoundCallBack;

		// Token: 0x02000520 RID: 1312
		// (Invoke) Token: 0x06003D47 RID: 15687
		public delegate bool CheckDirectoryCallBack(string fullDirectoryPath);

		// Token: 0x02000521 RID: 1313
		// (Invoke) Token: 0x06003D4B RID: 15691
		public delegate bool FileFoundCallBack(string fullFilePath);
	}
}
