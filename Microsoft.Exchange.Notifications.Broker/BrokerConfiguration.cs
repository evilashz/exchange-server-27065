using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class BrokerConfiguration
	{
		// Token: 0x040000BA RID: 186
		public static readonly IntAppSettingsEntry MaxDatabaseManagerThreads = new IntAppSettingsEntry("MaxDatabaseManagerThreads", 2, ExTraceGlobals.ServiceTracer);

		// Token: 0x040000BB RID: 187
		public static readonly IntAppSettingsEntry MaxConcurrentPushes = new IntAppSettingsEntry("MaxConcurrentPushes", 1, ExTraceGlobals.ServiceTracer);

		// Token: 0x040000BC RID: 188
		public static readonly IntAppSettingsEntry MaxConcurrentAccepts = new IntAppSettingsEntry("MaxConcurrentAccepts", 1, ExTraceGlobals.ServiceTracer);

		// Token: 0x040000BD RID: 189
		public static readonly Version MaximumProtocolVersion = new Version(new StringAppSettingsEntry("MaximumProtocolVersion", "1.0", ExTraceGlobals.ServiceTracer).Value);

		// Token: 0x040000BE RID: 190
		public static readonly Version MinimumProtocolVersion = new Version(new StringAppSettingsEntry("MinimumProtocolVersion", "1.0", ExTraceGlobals.ServiceTracer).Value);

		// Token: 0x040000BF RID: 191
		public static readonly IntAppSettingsEntry PreferredBatchSize = new IntAppSettingsEntry("PreferredBatchSize", 100, ExTraceGlobals.ServiceTracer);

		// Token: 0x040000C0 RID: 192
		public static readonly IntAppSettingsEntry ConnectionLimit = new IntAppSettingsEntry("ConnectionLimit", 128, ExTraceGlobals.ServiceTracer);

		// Token: 0x040000C1 RID: 193
		public static readonly StringAppSettingsEntry VdirName = new StringAppSettingsEntry("VdirName", "NotificationBroker", ExTraceGlobals.ServiceTracer);
	}
}
