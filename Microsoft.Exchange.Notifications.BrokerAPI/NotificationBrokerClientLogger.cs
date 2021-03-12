using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NotificationBrokerClientLogger : ExtensibleLogger
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002EE2 File Offset: 0x000010E2
		public NotificationBrokerClientLogger() : base(new NotificationBrokerClientLogConfiguration())
		{
		}
	}
}
