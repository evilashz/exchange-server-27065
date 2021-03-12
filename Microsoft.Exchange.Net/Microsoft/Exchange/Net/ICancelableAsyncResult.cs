using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200072F RID: 1839
	public interface ICancelableAsyncResult : IAsyncResult
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600231F RID: 8991
		bool IsCanceled { get; }

		// Token: 0x06002320 RID: 8992
		void Cancel();
	}
}
