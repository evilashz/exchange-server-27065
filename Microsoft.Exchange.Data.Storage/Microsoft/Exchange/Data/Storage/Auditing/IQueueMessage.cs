using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F52 RID: 3922
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IQueueMessage<T>
	{
		// Token: 0x170023A7 RID: 9127
		// (get) Token: 0x0600868C RID: 34444
		// (set) Token: 0x0600868D RID: 34445
		T Data { get; set; }
	}
}
