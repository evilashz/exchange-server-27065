using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Delivery
{
	// Token: 0x02000004 RID: 4
	internal static class MailboxTransportEventLogger
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002871 File Offset: 0x00000A71
		internal static ExEventLog EventLogger
		{
			get
			{
				return MailboxTransportEventLogger.eventLogger;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002878 File Offset: 0x00000A78
		internal static bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params string[] messageArgs)
		{
			return MailboxTransportEventLogger.eventLogger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x04000028 RID: 40
		public static readonly string MailboxTransportServiceEventSource = "MSExchangeDelivery";

		// Token: 0x04000029 RID: 41
		private static readonly ExEventLog eventLogger = new ExEventLog(MailboxTransportDeliveryService.MailboxTransportServiceComponentGuid, MailboxTransportEventLogger.MailboxTransportServiceEventSource);
	}
}
