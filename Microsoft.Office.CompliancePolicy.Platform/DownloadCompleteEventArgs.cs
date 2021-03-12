using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000039 RID: 57
	internal sealed class DownloadCompleteEventArgs : EventArgs
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000393F File Offset: 0x00001B3F
		public DownloadCompleteEventArgs(long bytesDownloaded) : this(bytesDownloaded, 0L)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000394A File Offset: 0x00001B4A
		public DownloadCompleteEventArgs(long bytesDownloaded, long bytesUploaded)
		{
			this.BytesDownloaded = bytesDownloaded;
			this.BytesUploaded = bytesUploaded;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003960 File Offset: 0x00001B60
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003968 File Offset: 0x00001B68
		public long BytesDownloaded { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003971 File Offset: 0x00001B71
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003979 File Offset: 0x00001B79
		public long BytesUploaded { get; private set; }
	}
}
