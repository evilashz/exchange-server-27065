using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation
{
	// Token: 0x02000202 RID: 514
	internal static class TimeInResourcePerfCounter
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x0003D638 File Offset: 0x0003B838
		static TimeInResourcePerfCounter()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				string processName = currentProcess.ProcessName;
				string cachedAppName = ActivityCoverageReport.CachedAppName;
				string instanceName = string.Format("{0}-{1}-{2}", "AD", processName, cachedAppName);
				TimeInResourcePerfCounter.adInstance = MSExchangeActivityContext.GetInstance(instanceName);
				string instanceName2 = string.Format("{0}-{1}-{2}", DisplayNameAttribute.GetEnumName(ActivityOperationType.MailboxCall), processName, cachedAppName);
				TimeInResourcePerfCounter.mailboxInstance = MSExchangeActivityContext.GetInstance(instanceName2);
				string instanceName3 = string.Format("{0}-{1}-{2}", DisplayNameAttribute.GetEnumName(ActivityOperationType.ExRpcAdmin), processName, cachedAppName);
				TimeInResourcePerfCounter.exRpcAdminInstance = MSExchangeActivityContext.GetInstance(instanceName3);
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003D6DC File Offset: 0x0003B8DC
		internal static void AddOperation(ActivityOperationType activityOperationType, float value)
		{
			MSExchangeActivityContextInstance instance = TimeInResourcePerfCounter.GetInstance(activityOperationType);
			if (instance != null)
			{
				instance.TimeInResourcePerSecond.IncrementBy((long)value);
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003D704 File Offset: 0x0003B904
		private static MSExchangeActivityContextInstance GetInstance(ActivityOperationType operation)
		{
			MSExchangeActivityContextInstance result = null;
			switch (operation)
			{
			case ActivityOperationType.ADRead:
			case ActivityOperationType.ADSearch:
			case ActivityOperationType.ADWrite:
				result = TimeInResourcePerfCounter.adInstance;
				break;
			case ActivityOperationType.MailboxCall:
				result = TimeInResourcePerfCounter.mailboxInstance;
				break;
			case ActivityOperationType.ExRpcAdmin:
				result = TimeInResourcePerfCounter.exRpcAdminInstance;
				break;
			}
			return result;
		}

		// Token: 0x04000AB3 RID: 2739
		private const string InstanceFormat = "{0}-{1}-{2}";

		// Token: 0x04000AB4 RID: 2740
		private const string AdOpName = "AD";

		// Token: 0x04000AB5 RID: 2741
		private static readonly MSExchangeActivityContextInstance adInstance;

		// Token: 0x04000AB6 RID: 2742
		private static readonly MSExchangeActivityContextInstance mailboxInstance;

		// Token: 0x04000AB7 RID: 2743
		private static readonly MSExchangeActivityContextInstance exRpcAdminInstance;
	}
}
