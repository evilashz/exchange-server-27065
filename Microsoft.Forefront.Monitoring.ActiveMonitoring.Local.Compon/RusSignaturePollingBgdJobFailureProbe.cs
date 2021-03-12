using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Threading;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200001A RID: 26
	public class RusSignaturePollingBgdJobFailureProbe : RusPublishingPipelineBase
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00007217 File Offset: 0x00005417
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000721F File Offset: 0x0000541F
		private TimeSpan LookbackIntervalTimeSpan { get; set; }

		// Token: 0x060000DD RID: 221 RVA: 0x00007228 File Offset: 0x00005428
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.TraceDebug("RusSignaturePollingBgdJobFailureProbe started.");
			base.RusEngineName = base.GetExtensionAttributeStringFromXml(base.Definition.ExtensionAttributes, "//RusSignaturePollingBgdJobFailureProbeParam", "EngineName", true);
			base.TraceDebug(string.Format("[EngineName: {0}]", base.RusEngineName));
			int integerExtensionAttributeFromXml = base.GetIntegerExtensionAttributeFromXml(base.Definition.ExtensionAttributes, "//RusSignaturePollingBgdJobFailureProbeParam", "MaxConsecutiveFailureLimit", 3, 1, 10);
			base.TraceDebug(string.Format("[MaxConsecutiveFailureLimit from probe xml: {0}]", integerExtensionAttributeFromXml));
			this.LookbackIntervalTimeSpan = base.GetTimeSpanExtensionAttributeFromXml(base.Definition.ExtensionAttributes, "//RusSignaturePollingBgdJobFailureProbeParam", "LookbackIntervalTimeSpan", RusSignaturePollingBgdJobFailureProbe.defaultLookBackInterval, RusSignaturePollingBgdJobFailureProbe.minimumLookBackInterval, RusSignaturePollingBgdJobFailureProbe.maximumLookBackInterval);
			Collection<PSObject> backgroundJobTaskResults = this.GetBackgroundJobTaskResults(base.RusEngineName, (int)this.LookbackIntervalTimeSpan.TotalMinutes);
			int num = 0;
			if (backgroundJobTaskResults != null && backgroundJobTaskResults.Count > 0)
			{
				int num2 = backgroundJobTaskResults.Count - 1;
				int count = backgroundJobTaskResults.Count;
				base.TraceDebug(string.Format("[Number of BGDTasks found in last {0} minutes: {1}]", this.LookbackIntervalTimeSpan.TotalMinutes, count));
				int num3 = count;
				for (int i = num2; i >= 0; i--)
				{
					string a = Convert.ToString(backgroundJobTaskResults[i].Properties["TaskCompletionStatus"].Value);
					if (string.Equals(a, "None", StringComparison.OrdinalIgnoreCase) && i == num2)
					{
						num3 = count - 1;
						if (num3 == 0)
						{
							string errorMsg = string.Format("'{0}' cmdlet for BgdJob with fileName: {1} returned only one task in last {2} minutes with TaskCompletionStatus 'None'", "Get-BackgroundJobTask", "Microsoft.Forefront.Hygiene.Rus.SignaturePollingJob.exe", this.LookbackIntervalTimeSpan.TotalMinutes);
							base.LogTraceErrorAndThrowApplicationException(errorMsg);
						}
					}
					else
					{
						if (string.Equals(a, "Success", StringComparison.OrdinalIgnoreCase))
						{
							break;
						}
						num++;
						int num4 = (num3 < integerExtensionAttributeFromXml) ? num3 : integerExtensionAttributeFromXml;
						if (num >= num4)
						{
							string text = base.FormatBackgroundJobTaskResultsToString(backgroundJobTaskResults);
							string errorMsg2 = string.Format("More than or equal to [{0}] consecutive failures found with [{1}] engine signature polling bgd job in last {2} minutes. \n {3}", new object[]
							{
								num,
								base.RusEngineName,
								this.LookbackIntervalTimeSpan.TotalMinutes,
								text
							});
							base.LogTraceErrorAndThrowApplicationException(errorMsg2);
						}
					}
				}
			}
			else
			{
				string errorMsg3 = string.Format("'{0}' cmdlet for BgdJob with fileName: {1} returned null or empty results", "Get-BackgroundJobTask", "Microsoft.Forefront.Hygiene.Rus.SignaturePollingJob.exe");
				base.LogTraceErrorAndThrowApplicationException(errorMsg3);
			}
			base.TraceDebug("RusSignaturePollingBgdJobFailureProbe finished with success.");
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000748C File Offset: 0x0000568C
		private Collection<PSObject> GetBackgroundJobTaskResults(string engineName, int startTimeLookbackMinutesFromCurrentUtc)
		{
			string psScript = string.Format("{0} -FileName {1} | where {2}$_.CommandLine -eq '{3}'{4} | {5} | {6} | where {7}$_.StartTime -gt [DateTime]::UtcNow.AddMinutes(-{8}){9}", new object[]
			{
				"Get-BackgroundJobDefinition",
				"Microsoft.Forefront.Hygiene.Rus.SignaturePollingJob.exe",
				"{",
				engineName,
				"}",
				"Get-BackgroundJobSchedule",
				"Get-BackgroundJobTask",
				"{",
				startTimeLookbackMinutesFromCurrentUtc,
				"}"
			});
			return base.ExecuteForeFrontManagementShellScript(psScript, false);
		}

		// Token: 0x040000A1 RID: 161
		private const string ProbeParamXmlNode = "//RusSignaturePollingBgdJobFailureProbeParam";

		// Token: 0x040000A2 RID: 162
		private const string MaxConsecutiveFailuresXmlAttribute = "MaxConsecutiveFailureLimit";

		// Token: 0x040000A3 RID: 163
		private const string LookbackIntervalTimeSpanXmlAttribute = "LookbackIntervalTimeSpan";

		// Token: 0x040000A4 RID: 164
		private const int DefaultMaxConsecutiveFailures = 3;

		// Token: 0x040000A5 RID: 165
		private const int MinimumConsecutiveFailures = 1;

		// Token: 0x040000A6 RID: 166
		private const int MaximumConsecutiveFailures = 10;

		// Token: 0x040000A7 RID: 167
		private static TimeSpan defaultLookBackInterval = TimeSpan.FromMinutes(60.0);

		// Token: 0x040000A8 RID: 168
		private static TimeSpan minimumLookBackInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x040000A9 RID: 169
		private static TimeSpan maximumLookBackInterval = TimeSpan.FromHours(24.0);
	}
}
