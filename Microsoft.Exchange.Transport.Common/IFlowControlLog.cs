using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000007 RID: 7
	internal interface IFlowControlLog
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000026 RID: 38
		// (remove) Token: 0x06000027 RID: 39
		event Action<string> TrackSummary;

		// Token: 0x06000028 RID: 40
		void LogThrottle(ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData);

		// Token: 0x06000029 RID: 41
		void LogUnthrottle(ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, int impact, int observedValue, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData);

		// Token: 0x0600002A RID: 42
		void LogSummary(string sequenceNumber, ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, int observedValue, int impact, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData);

		// Token: 0x0600002B RID: 43
		void LogSummaryWarning(ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData);

		// Token: 0x0600002C RID: 44
		void LogWarning(ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData);

		// Token: 0x0600002D RID: 45
		void LogMaxLinesExceeded(string sequenceNumber, ThrottlingSource source, int threshold, int observedValue, IEnumerable<KeyValuePair<string, object>> extraData);
	}
}
