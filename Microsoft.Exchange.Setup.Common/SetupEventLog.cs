using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000002 RID: 2
	internal static class SetupEventLog
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal static void LogEvent(long eventId, int categoryId, EventLogEntryType type, params object[] messageArgs)
		{
			EventInstance instance = new EventInstance(eventId, categoryId, type);
			try
			{
				SetupEventLog.setupEventLog.WriteEvent(instance, messageArgs);
			}
			catch (Win32Exception)
			{
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002108 File Offset: 0x00000308
		internal static void LogStartEvent(string installableUnit)
		{
			SetupEventLog.LogEvent(1000L, 1, EventLogEntryType.Information, new object[]
			{
				SetupEventLog.GetVersionAndLocalizedInstallableUnitName(installableUnit)
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002134 File Offset: 0x00000334
		internal static void LogSuccessEvent(string installableUnit)
		{
			SetupEventLog.LogEvent(1001L, 1, EventLogEntryType.Information, new object[]
			{
				SetupEventLog.GetVersionAndLocalizedInstallableUnitName(installableUnit)
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002160 File Offset: 0x00000360
		internal static void LogFailureEvent(string installableUnit, string error)
		{
			SetupEventLog.LogEvent(1002L, 1, EventLogEntryType.Error, new object[]
			{
				SetupEventLog.GetLocalizedInstallableUnitName(installableUnit),
				error
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000218F File Offset: 0x0000038F
		private static string GetVersionAndLocalizedInstallableUnitName(string installableUnit)
		{
			return ConfigurationContext.Setup.GetExecutingVersion().ToString() + ":" + SetupEventLog.GetLocalizedInstallableUnitName(installableUnit);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021AB File Offset: 0x000003AB
		private static string GetLocalizedInstallableUnitName(string installableUnit)
		{
			return InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnit).DisplayName;
		}

		// Token: 0x04000001 RID: 1
		private static string eventSourceName = "MSExchangeSetup";

		// Token: 0x04000002 RID: 2
		private static EventLog setupEventLog = new EventLog("Application", ".", SetupEventLog.eventSourceName);
	}
}
