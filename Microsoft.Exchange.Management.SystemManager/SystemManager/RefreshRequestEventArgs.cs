using System;
using System.ComponentModel;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200000C RID: 12
	public class RefreshRequestEventArgs : DoWorkEventArgs
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004FD6 File Offset: 0x000031D6
		public RefreshRequestEventArgs(bool isFullRefresh, IProgress progress, object argument, RefreshRequestPriority priority) : base(argument)
		{
			if (progress == null)
			{
				throw new ArgumentNullException("progress");
			}
			this.isFullRefresh = isFullRefresh;
			this.priority = priority;
			this.progress = progress;
			this.worker = new BackgroundWorker();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000500E File Offset: 0x0000320E
		public RefreshRequestEventArgs(bool isFullRefresh, IProgress progress, object argument) : this(isFullRefresh, progress, argument, 0)
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000501A File Offset: 0x0000321A
		internal IProgress ShellProgress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005022 File Offset: 0x00003222
		public BackgroundWorker BackgroundWorker
		{
			get
			{
				return this.worker;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000502A File Offset: 0x0000322A
		public bool ReportedProgress
		{
			get
			{
				return this.reportedProgress;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005032 File Offset: 0x00003232
		public bool IsFullRefresh
		{
			get
			{
				return this.isFullRefresh;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000503A File Offset: 0x0000323A
		public RefreshRequestPriority Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005042 File Offset: 0x00003242
		public bool CancellationPending
		{
			get
			{
				return this.worker.CancellationPending || this.ShellProgress.Canceled;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005060 File Offset: 0x00003260
		public void ReportProgress(int workProcessed, int totalWork, string statusText, object progressState)
		{
			RefreshProgressChangedEventArgs refreshProgressChangedEventArgs = new RefreshProgressChangedEventArgs(this, workProcessed, totalWork, statusText, progressState);
			this.reportedProgress = true;
			this.ShellProgress.ReportProgress(workProcessed, totalWork, statusText);
			this.worker.ReportProgress(refreshProgressChangedEventArgs.ProgressPercentage, refreshProgressChangedEventArgs);
		}

		// Token: 0x0400002F RID: 47
		private bool reportedProgress;

		// Token: 0x04000030 RID: 48
		private IProgress progress;

		// Token: 0x04000031 RID: 49
		private BackgroundWorker worker;

		// Token: 0x04000032 RID: 50
		private bool isFullRefresh;

		// Token: 0x04000033 RID: 51
		private RefreshRequestPriority priority;
	}
}
