using System;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200001E RID: 30
	internal class WebDownloaderEventArgs : EventArgs
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003F18 File Offset: 0x00002118
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003F1F File Offset: 0x0000211F
		public static Exception ErrorException
		{
			get
			{
				return WebDownloaderEventArgs.errorException;
			}
			set
			{
				WebDownloaderEventArgs.errorException = value;
			}
		}

		// Token: 0x0400004E RID: 78
		private static Exception errorException;
	}
}
