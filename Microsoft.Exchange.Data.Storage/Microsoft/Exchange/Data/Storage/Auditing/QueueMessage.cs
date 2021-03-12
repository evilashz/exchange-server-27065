using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F55 RID: 3925
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class QueueMessage<T> : IQueueMessage<T>
	{
		// Token: 0x170023A8 RID: 9128
		// (get) Token: 0x06008698 RID: 34456 RVA: 0x0024EC00 File Offset: 0x0024CE00
		// (set) Token: 0x06008699 RID: 34457 RVA: 0x0024EC08 File Offset: 0x0024CE08
		public T Data { get; set; }
	}
}
