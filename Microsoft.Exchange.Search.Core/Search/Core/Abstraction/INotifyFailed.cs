using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000031 RID: 49
	internal interface INotifyFailed
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000114 RID: 276
		// (remove) Token: 0x06000115 RID: 277
		event EventHandler<FailedEventArgs> Failed;
	}
}
