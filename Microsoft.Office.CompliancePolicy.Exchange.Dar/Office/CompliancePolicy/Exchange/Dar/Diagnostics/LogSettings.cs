using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics
{
	// Token: 0x02000013 RID: 19
	internal static class LogSettings
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000473F File Offset: 0x0000293F
		public static bool IsMonitored(string name)
		{
			return LogSettings.monitoredEvents.Contains(name);
		}

		// Token: 0x04000036 RID: 54
		public const string DefaultServiceName = "DarRuntime";

		// Token: 0x04000037 RID: 55
		public const string DefaultComponentName = "Unknown";

		// Token: 0x04000038 RID: 56
		private static HashSet<string> monitoredEvents = new HashSet<string>
		{
			NotificationItem.GenerateResultName("DarRuntime", "ServiceLet", "RuntimeStartupFailed"),
			NotificationItem.GenerateResultName("DarRuntime", "TaskLifeCycle", "TaskFailedWorkloadSubmission"),
			NotificationItem.GenerateResultName("DarRuntime", "BindingTask", "BindingTask3"),
			NotificationItem.GenerateResultName("DarRuntime", "BindingTask", "BindingTask13"),
			NotificationItem.GenerateResultName("DarRuntime", "BindingTask", "BindingTask14"),
			NotificationItem.GenerateResultName("DarRuntime", "ComplianceService", "ComplianceService5"),
			NotificationItem.GenerateResultName("DarRuntime", "ComplianceService", "ComplianceService6"),
			NotificationItem.GenerateResultName("DarRuntime", "ComplianceService", "ComplianceService8"),
			NotificationItem.GenerateResultName("DarRuntime", "PolicyConfigChangeEventHandler", "PolicyConfigChangeEventHandler0"),
			NotificationItem.GenerateResultName("DarRuntime", "PolicyConfigChangeEventHandler", "PolicyConfigChangeEventHandler14"),
			NotificationItem.GenerateResultName("DarRuntime", "DarTask", "DarTask2"),
			NotificationItem.GenerateResultName("DarRuntime", "DarTask", "DarTask4"),
			NotificationItem.GenerateResultName("DarRuntime", "DarTask", "DarTask5"),
			NotificationItem.GenerateResultName("DarRuntime", "TaskGenerator", "TaskGenerator2"),
			NotificationItem.GenerateResultName("DarRuntime", "TaskGenerator", "TaskGenerator7"),
			NotificationItem.GenerateResultName("DarRuntime", "DarTaskManager", null)
		};
	}
}
