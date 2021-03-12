using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Mapi
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CrossServerConnectionRegistryParameters
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002C74 File Offset: 0x00000E74
		public static bool IsCrossServerLoggingEnabled()
		{
			return CrossServerConnectionRegistryParameters.CheckBooleanValue("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CrossServerConnectionPolicy", "EnableCrossServerConnectionLog", true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C86 File Offset: 0x00000E86
		public static bool IsCrossServerBlockEnabled()
		{
			return CrossServerConnectionRegistryParameters.CheckBooleanValue("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CrossServerConnectionPolicy", "EnableCrossServerConnectionBlock", true);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C98 File Offset: 0x00000E98
		public static bool IsCrossServerMonitoringBlockEnabled()
		{
			return CrossServerConnectionRegistryParameters.CheckBooleanValue("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CrossServerConnectionPolicy", "EnableCrossServerMonitoringBlock", true);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CAA File Offset: 0x00000EAA
		public static TimeSpan GetInfoWatsonThrottlingInterval()
		{
			return CrossServerConnectionRegistryParameters.ReadTimeSpanFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CrossServerConnectionPolicy", "CrossServerInfoWatsonThrottlingInterval", CrossServerConnectionRegistryParameters.defaultInfoWatsonThrottlingInterval);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public static bool TryGetClientSpecificOverrides(string clientId, CrossServerBehavior crossServerBehaviorDefaults, out CrossServerBehavior crossServerBehavior)
		{
			crossServerBehavior = null;
			string text = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CrossServerConnectionPolicy\\ClientIdOverrides\\" + clientId;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(text))
			{
				if (registryKey != null)
				{
					bool shouldTrace = CrossServerConnectionRegistryParameters.CheckBooleanValue(text, "ShouldTrace", crossServerBehaviorDefaults.ShouldTrace);
					bool shouldLogInfoWatson = CrossServerConnectionRegistryParameters.CheckBooleanValue(text, "ShouldLogInfoWatson", crossServerBehaviorDefaults.ShouldLogInfoWatson);
					bool shouldBlock = CrossServerConnectionRegistryParameters.CheckBooleanValue(text, "ShouldBlock", crossServerBehaviorDefaults.ShouldBlock);
					crossServerBehavior = new CrossServerBehavior(clientId, crossServerBehaviorDefaults.PreExchange15, shouldTrace, shouldLogInfoWatson, shouldBlock);
				}
			}
			return crossServerBehavior != null;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D58 File Offset: 0x00000F58
		public static bool ConvertBooleanRegistryValue(object registryValue, bool defaultValue)
		{
			bool result = defaultValue;
			if (registryValue is int)
			{
				switch ((int)registryValue)
				{
				case 0:
					result = false;
					break;
				case 1:
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D90 File Offset: 0x00000F90
		public static TimeSpan ConvertTimeSpanRegistryValue(object registryValue, TimeSpan defaultValue)
		{
			TimeSpan result = defaultValue;
			TimeSpan timeSpan;
			if (registryValue != null && registryValue is string && TimeSpan.TryParse(registryValue as string, out timeSpan))
			{
				result = timeSpan;
			}
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DBC File Offset: 0x00000FBC
		private static bool CheckBooleanValue(string keyPath, string valueName, bool defaultValue)
		{
			return CrossServerConnectionRegistryParameters.ConvertBooleanRegistryValue(CrossServerConnectionRegistryParameters.ReadRegistryKey(keyPath, valueName), defaultValue);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DCB File Offset: 0x00000FCB
		private static TimeSpan ReadTimeSpanFromRegistry(string keyPath, string valueName, TimeSpan defaultValue)
		{
			return CrossServerConnectionRegistryParameters.ConvertTimeSpanRegistryValue(CrossServerConnectionRegistryParameters.ReadRegistryKey(keyPath, valueName), defaultValue);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DDC File Offset: 0x00000FDC
		private static object ReadRegistryKey(string keyPath, string valueName)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyPath))
			{
				if (registryKey != null)
				{
					return registryKey.GetValue(valueName, null);
				}
			}
			return null;
		}

		// Token: 0x0400006F RID: 111
		public const string ExchangeServerCrossServerConnectionPolicyRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CrossServerConnectionPolicy";

		// Token: 0x04000070 RID: 112
		public const string ExchangeServerClientIdOverridesRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CrossServerConnectionPolicy\\ClientIdOverrides";

		// Token: 0x04000071 RID: 113
		public const string EnableCrossServerLogRegistryValue = "EnableCrossServerConnectionLog";

		// Token: 0x04000072 RID: 114
		public const string EnableCrossServerBlockRegistryValue = "EnableCrossServerConnectionBlock";

		// Token: 0x04000073 RID: 115
		public const string EnableCrossServerMonitoringBlockRegistryValue = "EnableCrossServerMonitoringBlock";

		// Token: 0x04000074 RID: 116
		public const string CrossServerInfoWatsonThrottlingIntervalRegistryValue = "CrossServerInfoWatsonThrottlingInterval";

		// Token: 0x04000075 RID: 117
		public const string ShouldTraceRegistryValue = "ShouldTrace";

		// Token: 0x04000076 RID: 118
		public const string ShouldLogInfoWatsonRegistryValue = "ShouldLogInfoWatson";

		// Token: 0x04000077 RID: 119
		public const string ShouldBlockRegistryValue = "ShouldBlock";

		// Token: 0x04000078 RID: 120
		private static TimeSpan defaultInfoWatsonThrottlingInterval = TimeSpan.FromDays(7.0);
	}
}
