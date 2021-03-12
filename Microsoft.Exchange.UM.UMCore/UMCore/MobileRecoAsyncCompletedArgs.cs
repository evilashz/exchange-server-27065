using System;
using System.ComponentModel;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000284 RID: 644
	internal class MobileRecoAsyncCompletedArgs : AsyncCompletedEventArgs
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0005562E File Offset: 0x0005382E
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x00055636 File Offset: 0x00053836
		public string Result { get; private set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0005563F File Offset: 0x0005383F
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x00055647 File Offset: 0x00053847
		public int ResultCount { get; private set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00055650 File Offset: 0x00053850
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x00055658 File Offset: 0x00053858
		public TimeSpan RequestElapsedTime { get; set; }

		// Token: 0x06001328 RID: 4904 RVA: 0x00055661 File Offset: 0x00053861
		public MobileRecoAsyncCompletedArgs(string result, int resultCount, Exception error) : base(error, false, null)
		{
			this.Result = result;
			this.ResultCount = resultCount;
			this.RequestElapsedTime = TimeSpan.Zero;
		}
	}
}
