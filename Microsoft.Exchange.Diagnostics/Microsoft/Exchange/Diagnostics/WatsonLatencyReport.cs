using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000D7 RID: 215
	internal class WatsonLatencyReport : WatsonGenericReport
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x00019768 File Offset: 0x00017968
		public WatsonLatencyReport(string eventType, string triggerVersion, string locationIdentity, string exceptionName, string callstack, string methodName, string detailedExceptionInformation) : base(eventType, WatsonReport.GetValidString(triggerVersion), ExWatson.AppName, WatsonReport.ExchangeFormattedVersion(ExWatson.ApplicationVersion), WatsonReport.GetValidString(locationIdentity), WatsonReport.GetValidString(exceptionName), WatsonReport.GetValidString(callstack), WatsonGenericReport.StringHashFromStackTrace(WatsonReport.GetValidString(callstack)), WatsonReport.GetValidString(methodName), detailedExceptionInformation)
		{
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000197BA File Offset: 0x000179BA
		protected override WatsonIssueType GetIssueTypeCode()
		{
			return WatsonIssueType.ManagedCodeLatencyIssue;
		}
	}
}
