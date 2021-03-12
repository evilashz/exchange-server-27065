using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface INotificationSource
	{
		// Token: 0x0600002C RID: 44
		void Unadvise(object notificationHandle);

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002D RID: 45
		bool IsDisposedOrDead { get; }
	}
}
