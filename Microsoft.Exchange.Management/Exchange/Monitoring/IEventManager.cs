using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000542 RID: 1346
	internal interface IEventManager
	{
		// Token: 0x0600302D RID: 12333
		bool HasEvents();

		// Token: 0x0600302E RID: 12334
		void LogEvent(int eventId, string eventMessage);

		// Token: 0x0600302F RID: 12335
		void LogEvents(CheckId checkId, ReplicationCheckResultEnum result, List<MessageInfo> messages);
	}
}
