using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000098 RID: 152
	internal sealed class FileHandler : IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600038D RID: 909 RVA: 0x0000DB44 File Offset: 0x0000BD44
		// (remove) Token: 0x0600038E RID: 910 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		private event Action ChangedEvent;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600038F RID: 911 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		// (remove) Token: 0x06000390 RID: 912 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		public event Action Changed
		{
			add
			{
				lock (this.locker)
				{
					this.ChangedEvent += value;
				}
			}
			remove
			{
				lock (this.locker)
				{
					this.ChangedEvent -= value;
				}
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		public void Dispose()
		{
			this.fileWatcher.Dispose();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000DC49 File Offset: 0x0000BE49
		internal FileHandler(string filePath)
		{
			this.Initialize(filePath);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000DC63 File Offset: 0x0000BE63
		internal void ChangeFile(string filePath)
		{
			this.fileWatcher.Dispose();
			this.Initialize(filePath);
			this.FileChangeHandler();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000DC7D File Offset: 0x0000BE7D
		private void Initialize(string filePath)
		{
			this.fileWatcher = new FileSystemWatcherTimer(filePath, new Action(this.FileChangeHandler));
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000DC98 File Offset: 0x0000BE98
		private void FileChangeHandler()
		{
			Action action = null;
			lock (this.locker)
			{
				action = this.ChangedEvent;
			}
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x04000318 RID: 792
		private FileSystemWatcherTimer fileWatcher;

		// Token: 0x0400031A RID: 794
		private object locker = new object();
	}
}
