using System;

namespace Microsoft.Filtering
{
	// Token: 0x02000009 RID: 9
	internal interface IFipsDataStreamFilteringService : IDisposable
	{
		// Token: 0x0600000F RID: 15
		IAsyncResult BeginScan(FipsDataStreamFilteringRequest fipsDataStreamFilteringRequest, FilteringRequest filteringRequest, AsyncCallback callback, object state);

		// Token: 0x06000010 RID: 16
		FilteringResponse EndScan(IAsyncResult ar);
	}
}
