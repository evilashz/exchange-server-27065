using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Inference
{
	// Token: 0x0200049A RID: 1178
	internal static class InferenceMonitoringHelper
	{
		// Token: 0x06001DB3 RID: 7603 RVA: 0x000B3728 File Offset: 0x000B1928
		static InferenceMonitoringHelper()
		{
			MonitoringLogConfiguration configuration = new MonitoringLogConfiguration(ExchangeComponent.Inference.Name);
			InferenceMonitoringHelper.monitoringLogger = new MonitoringLogger(configuration);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000B3750 File Offset: 0x000B1950
		internal static void LogInfo(string message, params object[] messageArgs)
		{
			InferenceMonitoringHelper.monitoringLogger.LogEvent(DateTime.UtcNow, message, messageArgs);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000B3763 File Offset: 0x000B1963
		internal static void LogInfo(WorkItem workItem, string message, params object[] messageArgs)
		{
			InferenceMonitoringHelper.monitoringLogger.LogEvent(DateTime.UtcNow, string.Format("{0}/{1}: ", workItem.Definition.Name, workItem.Definition.TargetResource) + message, messageArgs);
		}

		// Token: 0x040014A4 RID: 5284
		internal const string MailboxAssistantsServiceName = "msexchangemailboxassistants";

		// Token: 0x040014A5 RID: 5285
		internal const string DeliveryServiceName = "msexchangedelivery";

		// Token: 0x040014A6 RID: 5286
		internal const string MailboxAssistantsRegKeyName = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters";

		// Token: 0x040014A7 RID: 5287
		internal const string DeliveryAgentRegKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference";

		// Token: 0x040014A8 RID: 5288
		internal const string TrainingRegKeyValueName = "InferenceTrainingAssistantEnabledOverride";

		// Token: 0x040014A9 RID: 5289
		internal const string DataCollectionRegKeyValueName = "InferenceDataCollectionAssistantEnabledOverride";

		// Token: 0x040014AA RID: 5290
		internal const string ClassificationRegKeyValueName = "ClassificationPipelineEnabled";

		// Token: 0x040014AB RID: 5291
		internal const string DeliveryAppConfigFileName = "MSExchangeDelivery.exe.config";

		// Token: 0x040014AC RID: 5292
		internal const string ClassificationAppConfigOverrideName = "InferenceClassificationAgentEnabledOverride";

		// Token: 0x040014AD RID: 5293
		private static MonitoringLogger monitoringLogger;
	}
}
