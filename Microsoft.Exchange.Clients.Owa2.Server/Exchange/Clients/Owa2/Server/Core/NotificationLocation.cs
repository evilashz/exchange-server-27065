using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000163 RID: 355
	internal abstract class NotificationLocation
	{
		// Token: 0x06000D34 RID: 3380
		public abstract KeyValuePair<string, object> GetEventData();
	}
}
