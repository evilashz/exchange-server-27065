using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x0200052B RID: 1323
	public static class TimeBasedAssistantsDiscoveryHelpers
	{
		// Token: 0x06002099 RID: 8345 RVA: 0x000C6C20 File Offset: 0x000C4E20
		public static ProbeDefinition CreateProbe(Component component, string targetResource, string targetExtension, Type probeType, string probeName, int recurrenceInterval, int maxRetryAttempts, TracingContext tracingContext)
		{
			ArgumentValidator.ThrowIfNull("component", component);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("targetResource", targetResource);
			ArgumentValidator.ThrowIfNull("probeType", probeType);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("probeName", probeName);
			ArgumentValidator.ThrowIfZeroOrNegative("recurrenceInterval", recurrenceInterval);
			ArgumentValidator.ThrowIfOutOfRange<int>("maxRetryAttempts", maxRetryAttempts, 0, int.MaxValue);
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.TimeBasedAssistantsTracer, tracingContext, "{0}: Creating probe {1} for the resource {2}.", "TimeBasedAssistants", probeName, targetResource, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TimeBasedAssistants\\TimeBasedAssistantsDiscoveryHelpers.cs", 87);
			ProbeDefinition result = new ProbeDefinition
			{
				AssemblyPath = probeType.Assembly.Location,
				TypeName = probeType.FullName,
				Name = probeName,
				ServiceName = component.Name,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = recurrenceInterval / 2,
				MaxRetryAttempts = maxRetryAttempts,
				TargetResource = targetResource,
				TargetExtension = targetExtension
			};
			WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.TimeBasedAssistantsTracer, tracingContext, "{0}: Created probe {1} for the resource {2}.", "TimeBasedAssistants", probeName, targetResource, null, "CreateProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TimeBasedAssistants\\TimeBasedAssistantsDiscoveryHelpers.cs", 108);
			return result;
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000C6D32 File Offset: 0x000C4F32
		public static string GenerateProbeName(string baseName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("baseName", baseName);
			return string.Format("{0}{1}Probe", "TimeBasedAssistants", baseName);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000C6D4F File Offset: 0x000C4F4F
		public static string GenerateMonitorName(string baseName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("baseName", baseName);
			return string.Format("{0}{1}Monitor", "TimeBasedAssistants", baseName);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000C6D6C File Offset: 0x000C4F6C
		public static string GenerateResponderName(string baseName, string action)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("baseName", baseName);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("action", action);
			return string.Format("{0}{1}{2}", "TimeBasedAssistants", baseName, action);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000C6D98 File Offset: 0x000C4F98
		internal static Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> ReadTimeBasedAssistantsDiagnostics(string arguments)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("arguments", arguments);
			string tbaDiagnostics = ProcessAccessManager.ClientRunProcessCommand(null, "MSExchangeMailboxAssistants", "MailboxAssistants", arguments, false, true, null);
			return TimeBasedAssistantsDiagnosticsParser.ParseDiagnosticsString(tbaDiagnostics);
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000C6DCC File Offset: 0x000C4FCC
		internal static StringBuilder GenerateMessageFromDiagnostics(Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> failedCriteriaDiagnostics)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> keyValuePair in failedCriteriaDiagnostics)
			{
				string assistantName = keyValuePair.Key.AssistantName;
				string arg = keyValuePair.Key.WorkcycleLength.ToString();
				string arg2 = keyValuePair.Key.WorkcycleCheckpointLength.ToString();
				stringBuilder.AppendLine(string.Format("{0} :: {1} :: {2}", assistantName, arg, arg2));
				foreach (KeyValuePair<MailboxDatabase, WindowJob[]> keyValuePair2 in keyValuePair.Value)
				{
					string arg3 = keyValuePair2.Key.Guid.ToString();
					string arg4 = keyValuePair2.Key.StartTime.ToString(CultureInfo.InvariantCulture);
					stringBuilder.AppendLine(string.Format("Database GUID: {0}", arg3));
					stringBuilder.AppendLine(string.Format("Database driver start time: {0}", arg4));
					foreach (WindowJob windowJob in keyValuePair2.Value)
					{
						int interestingMailboxCount = windowJob.InterestingMailboxCount;
						int failedMailboxCount = windowJob.FailedMailboxCount;
						int failedToOpenStoreSessionCount = windowJob.FailedToOpenStoreSessionCount;
						int movedToOnDemandMailboxCount = windowJob.MovedToOnDemandMailboxCount;
						int completedMailboxCount = windowJob.CompletedMailboxCount;
						int retriedMailboxCount = windowJob.RetriedMailboxCount;
						int num = completedMailboxCount + failedMailboxCount + movedToOnDemandMailboxCount - retriedMailboxCount;
						int num2 = interestingMailboxCount - num;
						double num3 = (num == 0 && num2 == 0) ? 1.0 : ((double)num / (double)interestingMailboxCount);
						int totalOnDatabaseMailboxCount = windowJob.TotalOnDatabaseMailboxCount;
						int notInterestingMailboxCount = windowJob.NotInterestingMailboxCount;
						int filteredMailboxCount = windowJob.FilteredMailboxCount;
						bool flag = totalOnDatabaseMailboxCount == interestingMailboxCount + notInterestingMailboxCount + filteredMailboxCount;
						stringBuilder.AppendLine(string.Format("WindowJob: {0}-{1}, Queued: {2}, Succeeded: {3}, OnDemand: {4}, Failed: {5}, Failed Store Session: {6}, Retried: {7}, NotProcessed: {8}, Total on Database: {9}, Not Interesting to Assistant: {10}, Filtered by Infrastructure: {11}", new object[]
						{
							windowJob.StartTime,
							windowJob.EndTime,
							interestingMailboxCount,
							completedMailboxCount,
							movedToOnDemandMailboxCount,
							failedMailboxCount,
							failedToOpenStoreSessionCount,
							retriedMailboxCount,
							num2,
							totalOnDatabaseMailboxCount,
							notInterestingMailboxCount,
							filteredMailboxCount
						}));
						stringBuilder.AppendLine(string.Format("SLA: {0}, Completed infra filtering/IsInteresting checks: {1}", num3.ToString("F"), flag));
					}
					stringBuilder.AppendLine();
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000C70D0 File Offset: 0x000C52D0
		internal static string GenerateMessageForLastNFailures(Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> failures, TimeBasedAssistantsLastNCriteria criteriaInstance)
		{
			ArgumentValidator.ThrowIfNull("failures", failures);
			ArgumentValidator.ThrowIfNull("criteriaInstance", criteriaInstance);
			StringBuilder stringBuilder = TimeBasedAssistantsDiscoveryHelpers.GenerateMessageFromDiagnostics(failures);
			foreach (TimeBasedAssistantsLastNCriteria.FailedCriteriaRecord failedCriteriaRecord in criteriaInstance.FailedCriteriaRecordList)
			{
				stringBuilder.AppendLine(string.Format("Assistant: {0}, Database Guid {1}.", failedCriteriaRecord.AssistantName, failedCriteriaRecord.DatabaseGuid));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040017EC RID: 6124
		public const string AssistantsServiceName = "MSExchangeMailboxAssistants";

		// Token: 0x040017ED RID: 6125
		public const string AssistantsComponentName = "MailboxAssistants";

		// Token: 0x040017EE RID: 6126
		private const string TimeBasedAssistantsPrefix = "TimeBasedAssistants";
	}
}
