using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000D9 RID: 217
	internal class WatsonTroubleshootingReport : WatsonGenericReport
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00019900 File Offset: 0x00017B00
		internal WatsonTroubleshootingReport(string eventType, string triggerVersion, string locationIdentity, string exceptionName, string callstack, string methodName, string detailedExceptionInformation, string traceFileName) : base(eventType, WatsonReport.GetValidString(triggerVersion), ExWatson.AppName, WatsonReport.ExchangeFormattedVersion(ExWatson.ApplicationVersion), WatsonReport.GetValidString(locationIdentity), WatsonReport.GetValidString(exceptionName), WatsonReport.GetValidString(callstack), WatsonGenericReport.StringHashFromStackTrace(WatsonReport.GetValidString(callstack)), WatsonReport.GetValidString(methodName), detailedExceptionInformation)
		{
			this.traceFileName = traceFileName;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001995A File Offset: 0x00017B5A
		protected override WatsonIssueType GetIssueTypeCode()
		{
			return WatsonIssueType.ManagedCodeTroubleshootingIssue;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001995D File Offset: 0x00017B5D
		protected override void AddExtraFiles(WerSafeHandle watsonReportHandle)
		{
			DiagnosticsNativeMethods.WerReportAddFile(watsonReportHandle, this.traceFileName, DiagnosticsNativeMethods.WER_FILE_TYPE.WerFileTypeOther, (DiagnosticsNativeMethods.WER_FILE_FLAGS)0U);
		}

		// Token: 0x0400044F RID: 1103
		private string traceFileName;
	}
}
