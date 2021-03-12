using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006A0 RID: 1696
	public struct Delta<T>
	{
		// Token: 0x04003118 RID: 12568
		public List<T> AddedObjects;

		// Token: 0x04003119 RID: 12569
		public List<T> RemovedObjects;

		// Token: 0x0400311A RID: 12570
		public List<T> UnchangedObjects;
	}
}
