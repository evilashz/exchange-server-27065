using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200012A RID: 298
	internal interface ILogEvent
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000895 RID: 2197
		string EventId { get; }

		// Token: 0x06000896 RID: 2198
		ICollection<KeyValuePair<string, object>> GetEventData();
	}
}
