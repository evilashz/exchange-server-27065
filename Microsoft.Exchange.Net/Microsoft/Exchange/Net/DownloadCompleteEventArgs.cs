using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000731 RID: 1841
	internal sealed class DownloadCompleteEventArgs : EventArgs
	{
		// Token: 0x0600232F RID: 9007 RVA: 0x00047D7B File Offset: 0x00045F7B
		public DownloadCompleteEventArgs(long bytesDownloaded) : this(bytesDownloaded, 0L)
		{
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00047D86 File Offset: 0x00045F86
		public DownloadCompleteEventArgs(long bytesDownloaded, long bytesUploaded)
		{
			this.bytesDownloaded = bytesDownloaded;
			this.bytesUploaded = bytesUploaded;
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00047D9C File Offset: 0x00045F9C
		public long BytesDownloaded
		{
			get
			{
				return this.bytesDownloaded;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x00047DA4 File Offset: 0x00045FA4
		public long BytesUploaded
		{
			get
			{
				return this.bytesUploaded;
			}
		}

		// Token: 0x04002143 RID: 8515
		private long bytesDownloaded;

		// Token: 0x04002144 RID: 8516
		private long bytesUploaded;
	}
}
