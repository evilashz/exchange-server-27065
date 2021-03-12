using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x02000008 RID: 8
	internal static class MailboxTransportSubmissionEventLogger
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000041CC File Offset: 0x000023CC
		internal static ExEventLog EventLogger
		{
			get
			{
				return MailboxTransportSubmissionEventLogger.eventLogger;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000041D3 File Offset: 0x000023D3
		internal static bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			return MailboxTransportSubmissionEventLogger.eventLogger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x04000056 RID: 86
		public static readonly string MailboxTransportSubmissionServiceEventSource = "MSExchangeSubmission";

		// Token: 0x04000057 RID: 87
		private static readonly ExEventLog eventLogger = new ExEventLog(MailboxTransportSubmissionService.MailboxTransportSubmissionServiceComponentGuid, MailboxTransportSubmissionEventLogger.MailboxTransportSubmissionServiceEventSource);
	}
}
