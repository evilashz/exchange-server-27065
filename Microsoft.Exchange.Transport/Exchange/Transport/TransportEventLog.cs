using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000074 RID: 116
	internal static class TransportEventLog
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000F758 File Offset: 0x0000D958
		internal static string GetEventSource()
		{
			string currentProcessName;
			switch (currentProcessName = TransportEventLog.CurrentProcessName)
			{
			case "edgetransport":
				return "MSExchangeTransport";
			case "msexchangefrontendtransport":
				return "MSExchangeFrontEndTransport";
			case "msexchangedelivery":
				return "MSExchangeTransportDelivery";
			case "msexchangesubmission":
				return "MSExchangeTransportSubmission";
			case "msexchangemailboxassistants":
				return "MSExchangeTransportMailboxAssistants";
			case "msexchangetransportlogsearch":
				return "MSExchangeTransportSearch";
			}
			return "MSExchangeTransport";
		}

		// Token: 0x040001E1 RID: 481
		internal const string HubEventSource = "MSExchangeTransport";

		// Token: 0x040001E2 RID: 482
		internal const string FrontEndEventSource = "MSExchangeFrontEndTransport";

		// Token: 0x040001E3 RID: 483
		internal const string MailboxDeliveryEventSource = "MSExchangeTransportDelivery";

		// Token: 0x040001E4 RID: 484
		internal const string MailboxSubmissionEventSource = "MSExchangeTransportSubmission";

		// Token: 0x040001E5 RID: 485
		internal const string MailSubmissionEventSource = "MSExchangeTransportMailSubmission";

		// Token: 0x040001E6 RID: 486
		internal const string MailboxAssistantsEventSource = "MSExchangeTransportMailboxAssistants";

		// Token: 0x040001E7 RID: 487
		internal const string TransportLogSearchEventSource = "MSExchangeTransportSearch";

		// Token: 0x040001E8 RID: 488
		private static readonly string CurrentProcessName = Process.GetCurrentProcess().ProcessName.ToLower();
	}
}
