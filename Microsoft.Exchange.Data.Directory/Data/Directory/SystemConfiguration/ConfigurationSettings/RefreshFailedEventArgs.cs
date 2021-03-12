using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000669 RID: 1641
	internal sealed class RefreshFailedEventArgs : EventArgs
	{
		// Token: 0x06004CB0 RID: 19632 RVA: 0x0011B269 File Offset: 0x00119469
		public RefreshFailedEventArgs(Exception e)
		{
			this.Exception = e;
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06004CB1 RID: 19633 RVA: 0x0011B278 File Offset: 0x00119478
		// (set) Token: 0x06004CB2 RID: 19634 RVA: 0x0011B280 File Offset: 0x00119480
		public Exception Exception { get; private set; }
	}
}
