using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000304 RID: 772
	// (Invoke) Token: 0x060016EC RID: 5868
	internal delegate T QueryMethod<K, T>(K key, T currentCache, out KeyValuePair<K, T>[] additionalRecords);
}
