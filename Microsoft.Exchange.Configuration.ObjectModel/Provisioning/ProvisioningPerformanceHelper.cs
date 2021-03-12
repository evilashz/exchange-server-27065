using System;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Mapi.Unmanaged;
using Microsoft.Win32;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020001FD RID: 509
	public static class ProvisioningPerformanceHelper
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x000375DC File Offset: 0x000357DC
		internal static LatencyDetectionContext StartLatencyDetection(Task task)
		{
			if (ProvisioningPerformanceHelper.enabled == 0 || task.CurrentTaskContext.InvocationInfo == null)
			{
				return null;
			}
			if (string.Compare(task.CurrentTaskContext.InvocationInfo.CommandName, "New-Mailbox", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(task.CurrentTaskContext.InvocationInfo.CommandName, "New-SyncMailbox", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(task.CurrentTaskContext.InvocationInfo.CommandName, "New-GroupMailbox", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return null;
			}
			IPerformanceDataProvider[] providers = new IPerformanceDataProvider[]
			{
				PerformanceContext.Current,
				RpcDataProvider.Instance,
				TaskPerformanceData.InternalValidate,
				TaskPerformanceData.InternalStateReset,
				TaskPerformanceData.WindowsLiveIdProvisioningHandlerForNew,
				TaskPerformanceData.MailboxProvisioningHandler,
				TaskPerformanceData.AdminLogProvisioningHandler,
				TaskPerformanceData.OtherProvisioningHandlers,
				TaskPerformanceData.InternalProcessRecord,
				TaskPerformanceData.SaveInitial,
				TaskPerformanceData.ReadUpdated,
				TaskPerformanceData.SaveResult,
				TaskPerformanceData.ReadResult,
				TaskPerformanceData.WriteResult
			};
			return ProvisioningPerformanceHelper.latencyDetectionContextFactory.CreateContext(ProvisioningPerformanceHelper.applicationVersion, task.CurrentTaskContext.InvocationInfo.CommandName, providers);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000376F8 File Offset: 0x000358F8
		internal static TaskPerformanceData[] StopLatencyDetection(LatencyDetectionContext latencyDetectionContext)
		{
			if (latencyDetectionContext == null)
			{
				return null;
			}
			TaskPerformanceData[] result = latencyDetectionContext.StopAndFinalizeCollection();
			if (latencyDetectionContext.Elapsed.TotalSeconds > (double)ProvisioningPerformanceHelper.threshold)
			{
				object[] array = new object[3];
				string text = latencyDetectionContext.ToString("s");
				if (text.Length > 32766)
				{
					text = text.Substring(0, 32766);
				}
				array[0] = ProvisioningPerformanceHelper.threshold;
				array[1] = text;
				array[2] = Environment.CurrentManagedThreadId;
				TaskLogger.LogEvent("All", ProvisioningPerformanceHelper.endTuple, array);
			}
			return result;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00037784 File Offset: 0x00035984
		private static int GetConfigurationValue(string registryValueName, int defaultValue)
		{
			int result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ProvisioningCmdletOptics"))
			{
				if (registryKey != null)
				{
					result = (int)registryKey.GetValue(registryValueName, defaultValue);
				}
				else
				{
					result = defaultValue;
				}
			}
			return result;
		}

		// Token: 0x04000431 RID: 1073
		public const string ThresholdValueName = "Threshold";

		// Token: 0x04000432 RID: 1074
		public const string EnabledValueName = "Enabled";

		// Token: 0x04000433 RID: 1075
		public const int DefaultThreshold = 20;

		// Token: 0x04000434 RID: 1076
		public const string SettingsOverrideRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ProvisioningCmdletOptics";

		// Token: 0x04000435 RID: 1077
		private static ExEventLog.EventTuple endTuple = TaskEventLogConstants.Tuple_ExecuteTaskScriptLatency;

		// Token: 0x04000436 RID: 1078
		private static readonly string applicationVersion = typeof(ProvisioningPerformanceHelper).GetApplicationVersion();

		// Token: 0x04000437 RID: 1079
		private static int threshold = ProvisioningPerformanceHelper.GetConfigurationValue("Threshold", 20);

		// Token: 0x04000438 RID: 1080
		private static int enabled = ProvisioningPerformanceHelper.GetConfigurationValue("Enabled", 1);

		// Token: 0x04000439 RID: 1081
		private static LatencyDetectionContextFactory latencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory("Provisioning Cmdlet Latency");
	}
}
