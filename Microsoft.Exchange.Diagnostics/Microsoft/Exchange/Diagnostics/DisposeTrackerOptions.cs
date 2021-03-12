using System;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DisposeTrackerOptions
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003E79 File Offset: 0x00002079
		static DisposeTrackerOptions()
		{
			DisposeTrackerOptions.RefreshNow();
			DisposeTrackerOptions.lastConfigRefreshTicks = (uint)Environment.TickCount;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003E90 File Offset: 0x00002090
		public static bool DontStopCollecting
		{
			get
			{
				return DisposeTrackerOptions.dontStopCollecting;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003E97 File Offset: 0x00002097
		public static int NumberOfStackTracesToCollect
		{
			get
			{
				return DisposeTrackerOptions.numberOfStackTracesToCollect;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003E9E File Offset: 0x0000209E
		public static int PercentageOfStackTracesToCollect
		{
			get
			{
				return DisposeTrackerOptions.percentageOfStackTracesToCollect;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003EA5 File Offset: 0x000020A5
		public static bool UseFullSymbols
		{
			get
			{
				return DisposeTrackerOptions.useFullSymbols;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003EAC File Offset: 0x000020AC
		public static int ThrottleMilliseconds
		{
			get
			{
				return DisposeTrackerOptions.throttleMilliseconds;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003EB3 File Offset: 0x000020B3
		public static int MaximumWatsonsPerSecond
		{
			get
			{
				return DisposeTrackerOptions.maximumWatsonsPerSecond;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003EBA File Offset: 0x000020BA
		public static bool TerminateOnReport
		{
			get
			{
				return DisposeTrackerOptions.terminateOnReport;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003EC1 File Offset: 0x000020C1
		public static bool Enabled
		{
			get
			{
				return DisposeTrackerOptions.enabled;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003EC8 File Offset: 0x000020C8
		public static bool DebugBreakOnLeak
		{
			get
			{
				return DisposeTrackerOptions.debugBreakOnLeak;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003ECF File Offset: 0x000020CF
		public static bool CollectStackTracesForLeakDetection
		{
			get
			{
				return DisposeTrackerOptions.collectStackTracesForLeakDetection;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003ED6 File Offset: 0x000020D6
		internal static int NumberOfStackTracesToSkip
		{
			get
			{
				return DisposeTrackerOptions.numberOfStackTracesToSkip;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003EDD File Offset: 0x000020DD
		public static void ScheduleRefreshIfNecessary()
		{
			if ((ulong)(Environment.TickCount - (int)DisposeTrackerOptions.lastConfigRefreshTicks) >= (ulong)((long)DisposeTrackerOptions.minConfigChangeMilliseconds))
			{
				Task.Factory.StartNew(new Action(DisposeTrackerOptions.OptionsThreadProc));
				DisposeTrackerOptions.lastConfigRefreshTicks = (uint)Environment.TickCount;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F14 File Offset: 0x00002114
		public static void RefreshNowIfNecessary()
		{
			if ((ulong)(Environment.TickCount - (int)DisposeTrackerOptions.lastConfigRefreshTicks) >= (ulong)((long)DisposeTrackerOptions.minConfigChangeMilliseconds))
			{
				DisposeTrackerOptions.RefreshNow();
				DisposeTrackerOptions.lastConfigRefreshTicks = (uint)Environment.TickCount;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003F39 File Offset: 0x00002139
		public static void RefreshNow()
		{
			DisposeTrackerOptions.ReloadRegistryData();
			DisposeTrackerOptions.RefreshCalculatedProperties();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F45 File Offset: 0x00002145
		private static void OptionsThreadProc()
		{
			DisposeTrackerOptions.RefreshNow();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F4C File Offset: 0x0000214C
		private static void ReloadRegistryData()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\DisposeTrackerOptions", false))
			{
				DisposeTrackerOptions.dontStopCollecting = DisposeTrackerOptions.GetRegistryBoolean(registryKey, "DontStopCollecting", false);
				DisposeTrackerOptions.numberOfStackTracesToCollect = Math.Min(DisposeTrackerOptions.GetRegistryInt(registryKey, "NumberOfStackTracesToCollect", 0), 65535);
				DisposeTrackerOptions.percentageOfStackTracesToCollect = Math.Min(DisposeTrackerOptions.GetRegistryInt(registryKey, "PercentageOfStackTracesToCollect", 25), 100);
				DisposeTrackerOptions.useFullSymbols = DisposeTrackerOptions.GetRegistryBoolean(registryKey, "UseFullSymbols", true);
				DisposeTrackerOptions.throttleMilliseconds = Math.Min(DisposeTrackerOptions.GetRegistryInt(registryKey, "ThrottleMilliseconds", 33), 300000);
				DisposeTrackerOptions.maximumWatsonsPerSecond = Math.Min(DisposeTrackerOptions.GetRegistryInt(registryKey, "MaximumWatsonsPerSecond", 25), 1000);
				DisposeTrackerOptions.minConfigChangeMilliseconds = Math.Min(DisposeTrackerOptions.GetRegistryInt(registryKey, "MinConfigChangeMilliseconds", 30000), 3600000);
				DisposeTrackerOptions.terminateOnReport = DisposeTrackerOptions.GetRegistryBoolean(registryKey, "TerminateOnReport", false);
				DisposeTrackerOptions.debugBreakOnLeak = DisposeTrackerOptions.GetRegistryBoolean(registryKey, "DebugBreakOnLeak", false);
				DisposeTrackerOptions.collectStackTracesForLeakDetection = DisposeTrackerOptions.GetRegistryBoolean(registryKey, "CollectStackTracesForLeakDetection", false);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004068 File Offset: 0x00002268
		private static bool GetRegistryBoolean(RegistryKey key, string name, bool defaultValue)
		{
			if (key == null)
			{
				return defaultValue;
			}
			object value = key.GetValue(name, defaultValue);
			if (!(value is int))
			{
				return defaultValue;
			}
			return (int)value != 0;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000040A0 File Offset: 0x000022A0
		private static int GetRegistryInt(RegistryKey key, string name, int defaultValue)
		{
			if (key == null)
			{
				return defaultValue;
			}
			object value = key.GetValue(name, defaultValue);
			if (!(value is int))
			{
				return defaultValue;
			}
			if ((int)value >= 0)
			{
				return (int)value;
			}
			return defaultValue;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000040DC File Offset: 0x000022DC
		private static void RefreshCalculatedProperties()
		{
			if (DisposeTrackerOptions.percentageOfStackTracesToCollect > 0)
			{
				DisposeTrackerOptions.numberOfStackTracesToSkip = (100 - DisposeTrackerOptions.percentageOfStackTracesToCollect) / DisposeTrackerOptions.percentageOfStackTracesToCollect;
				DisposeTrackerOptions.enabled = true;
			}
			else
			{
				DisposeTrackerOptions.numberOfStackTracesToSkip = 0;
				DisposeTrackerOptions.enabled = false;
			}
			if (DisposeTrackerOptions.numberOfStackTracesToCollect == 0)
			{
				DisposeTrackerOptions.enabled = false;
			}
			if (DisposeTrackerOptions.maximumWatsonsPerSecond == 0)
			{
				DisposeTrackerOptions.enabled = false;
			}
		}

		// Token: 0x04000097 RID: 151
		internal const string ConfigRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\DisposeTrackerOptions";

		// Token: 0x04000098 RID: 152
		private const bool DefaultDontStopCollecting = false;

		// Token: 0x04000099 RID: 153
		private const int DefaultPercentageOfStackTracesToCollect = 25;

		// Token: 0x0400009A RID: 154
		private const int DefaultNumberOfStackTracesToCollect = 0;

		// Token: 0x0400009B RID: 155
		private const int MaxNumberOfStackTracesToCollect = 65535;

		// Token: 0x0400009C RID: 156
		private const bool DefaultUseFullSymbols = true;

		// Token: 0x0400009D RID: 157
		private const int DefaultThrottleMilliseconds = 33;

		// Token: 0x0400009E RID: 158
		private const int MaxThrottleMilliseconds = 300000;

		// Token: 0x0400009F RID: 159
		private const int DefaultMaximumWatsonsPerSecond = 25;

		// Token: 0x040000A0 RID: 160
		private const int MaxMaximumWatsonsPerSecond = 1000;

		// Token: 0x040000A1 RID: 161
		private const int DefaultMinConfigChangeMilliseconds = 30000;

		// Token: 0x040000A2 RID: 162
		private const int MaxMinConfigChangeMilliseconds = 3600000;

		// Token: 0x040000A3 RID: 163
		private const bool DefaultTerminateOnReport = false;

		// Token: 0x040000A4 RID: 164
		private const bool DefaultDebugBreakOnLeak = false;

		// Token: 0x040000A5 RID: 165
		private const bool DefaultCollectStackTracesForLeakDetection = false;

		// Token: 0x040000A6 RID: 166
		private static bool dontStopCollecting;

		// Token: 0x040000A7 RID: 167
		private static int numberOfStackTracesToCollect;

		// Token: 0x040000A8 RID: 168
		private static int percentageOfStackTracesToCollect;

		// Token: 0x040000A9 RID: 169
		private static int numberOfStackTracesToSkip;

		// Token: 0x040000AA RID: 170
		private static bool useFullSymbols;

		// Token: 0x040000AB RID: 171
		private static int throttleMilliseconds;

		// Token: 0x040000AC RID: 172
		private static int maximumWatsonsPerSecond;

		// Token: 0x040000AD RID: 173
		private static int minConfigChangeMilliseconds;

		// Token: 0x040000AE RID: 174
		private static bool enabled = true;

		// Token: 0x040000AF RID: 175
		private static bool terminateOnReport;

		// Token: 0x040000B0 RID: 176
		private static uint lastConfigRefreshTicks;

		// Token: 0x040000B1 RID: 177
		private static bool debugBreakOnLeak;

		// Token: 0x040000B2 RID: 178
		private static bool collectStackTracesForLeakDetection;
	}
}
