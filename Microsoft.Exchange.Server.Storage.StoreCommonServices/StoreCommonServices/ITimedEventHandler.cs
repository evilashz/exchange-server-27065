using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200014B RID: 331
	internal interface ITimedEventHandler
	{
		// Token: 0x06000CDA RID: 3290
		void Invoke(Context context, TimedEventEntry timedEvent);
	}
}
