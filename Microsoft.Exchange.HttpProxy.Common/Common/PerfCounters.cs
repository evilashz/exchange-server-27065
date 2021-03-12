using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x0200001B RID: 27
	internal class PerfCounters
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004F7E File Offset: 0x0000317E
		public static HttpProxyCountersInstance HttpProxyCountersInstance
		{
			get
			{
				return PerfCounters.counters.Member;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004F8A File Offset: 0x0000318A
		public static HttpProxyCacheCountersInstance HttpProxyCacheCountersInstance
		{
			get
			{
				return PerfCounters.cacheCounters.Member;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004F96 File Offset: 0x00003196
		public static bool RoutingLatenciesEnabled
		{
			get
			{
				return PerfCounters.RoutingLatencyPerfCountersEnabledAppSettingEntry.Value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004FA2 File Offset: 0x000031A2
		public static bool RoutingErrorsEnabled
		{
			get
			{
				return PerfCounters.RoutingErrorPerfCountersEnabledAppSettingEntry.Value;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004FB0 File Offset: 0x000031B0
		public static HttpProxyPerSiteCountersInstance GetHttpProxyPerSiteCountersInstance(string siteName)
		{
			if (string.IsNullOrEmpty(siteName))
			{
				siteName = "Unknown";
			}
			string text = HttpProxyGlobals.ProtocolType.ToString() + ";" + siteName;
			ExTraceGlobals.VerboseTracer.TraceDebug<string, bool>(0L, "[PerfCounters::GetHttpProxyPerSiteCountersInstance]: InstanceName = {0}, NeedsInit = {1}", text, !PerfCounters.httpProxyPerSiteInitializedInstances.Contains(text));
			HttpProxyPerSiteCountersInstance instance = HttpProxyPerSiteCounters.GetInstance(text);
			if (!PerfCounters.httpProxyPerSiteInitializedInstances.Contains(text))
			{
				lock (PerfCounters.httpProxyPerSiteInitializedInstances)
				{
					if (!PerfCounters.httpProxyPerSiteInitializedInstances.Contains(text))
					{
						PerfCounters.InitMaps<HttpProxyPerSiteCountersInstance>(instance);
						PerfCounters.httpProxyPerSiteInitializedInstances.Add(text);
					}
				}
			}
			return instance;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005068 File Offset: 0x00003268
		public static void UpdateMovingAveragePerformanceCounter(ExPerformanceCounter performanceCounter, long newValue)
		{
			if (PerfCounters.latencycounterToRunningAverageFloatMap.ContainsKey(performanceCounter))
			{
				RunningAverageFloat runningAverageFloat = PerfCounters.latencycounterToRunningAverageFloatMap[performanceCounter];
				long rawValue = (long)runningAverageFloat.Update((float)newValue);
				performanceCounter.RawValue = rawValue;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000050A0 File Offset: 0x000032A0
		public static void UpdateMovingPercentagePerformanceCounter(ExPerformanceCounter performanceCounter)
		{
			if (PerfCounters.latencycounterToRunningPercentageMap.ContainsKey(performanceCounter))
			{
				RunningPercentage runningPercentage = PerfCounters.latencycounterToRunningPercentageMap[performanceCounter];
				long rawValue = runningPercentage.Update();
				performanceCounter.RawValue = rawValue;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000050D4 File Offset: 0x000032D4
		public static void IncrementMovingPercentagePerformanceCounterBase(ExPerformanceCounter performanceCounter)
		{
			if (PerfCounters.latencycounterToRunningPercentageMap.ContainsKey(performanceCounter))
			{
				RunningPercentage runningPercentage = PerfCounters.latencycounterToRunningPercentageMap[performanceCounter];
				long rawValue = runningPercentage.IncrementBase();
				performanceCounter.RawValue = rawValue;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005108 File Offset: 0x00003308
		internal static void UpdateHttpProxyPerArrayCounters()
		{
			ClientAccessArray localServerClientAccessArray = Server.GetLocalServerClientAccessArray();
			if (localServerClientAccessArray == null)
			{
				PerfCounters.ResetHttpProxyPerArrayCounters(null);
				return;
			}
			HttpProxyPerArrayCountersInstance instance = HttpProxyPerArrayCounters.GetInstance(localServerClientAccessArray.Name);
			if (instance != null)
			{
				PerfCounters.ResetHttpProxyPerArrayCounters(localServerClientAccessArray.Name);
				instance.TotalServersInArray.RawValue = (long)Math.Max(1, localServerClientAccessArray.ServerCount);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005158 File Offset: 0x00003358
		private static void ResetHttpProxyPerArrayCounters(string excludeInstanceName)
		{
			foreach (string text in HttpProxyPerArrayCounters.GetInstanceNames())
			{
				if (!string.Equals(text, excludeInstanceName, StringComparison.OrdinalIgnoreCase))
				{
					HttpProxyPerArrayCounters.ResetInstance(text);
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005190 File Offset: 0x00003390
		private static void InitMaps<T>(T instance)
		{
			foreach (FieldInfo fieldInfo in typeof(T).GetFields())
			{
				ExPerformanceCounter exPerformanceCounter = (ExPerformanceCounter)fieldInfo.GetValue(instance);
				exPerformanceCounter.RawValue = 0L;
				if (fieldInfo.Name.StartsWith("MovingAverage") && !PerfCounters.latencycounterToRunningAverageFloatMap.ContainsKey(exPerformanceCounter))
				{
					PerfCounters.latencycounterToRunningAverageFloatMap.Add(exPerformanceCounter, new RunningAverageFloat(200));
				}
				if (fieldInfo.Name.StartsWith("MovingPercentage") && !PerfCounters.latencycounterToRunningPercentageMap.ContainsKey(exPerformanceCounter))
				{
					PerfCounters.latencycounterToRunningPercentageMap.Add(exPerformanceCounter, new RunningPercentage(200));
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005247 File Offset: 0x00003447
		[Conditional("DEBUG")]
		private static void AssertOnMissingCounterMap(ExPerformanceCounter performanceCounter, string type)
		{
			throw new ArgumentException(string.Format(type + " counter mapped instance not found : {0}({1})", performanceCounter.CounterName, performanceCounter.InstanceName));
		}

		// Token: 0x040000C6 RID: 198
		private const int MovingWindowNumberOfSamples = 200;

		// Token: 0x040000C7 RID: 199
		private const string UnknownInstance = "Unknown";

		// Token: 0x040000C8 RID: 200
		private static readonly BoolAppSettingsEntry RoutingLatencyPerfCountersEnabledAppSettingEntry = new BoolAppSettingsEntry(HttpProxySettings.Prefix("RoutingLatencyPerfCountersEnabled"), HttpProxyGlobals.IsPartnerHostedOnly || VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.ConfigurePerformanceCounters.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C9 RID: 201
		private static readonly BoolAppSettingsEntry RoutingErrorPerfCountersEnabledAppSettingEntry = new BoolAppSettingsEntry(HttpProxySettings.Prefix("RoutingErrorPerfCountersEnabled"), HttpProxyGlobals.IsPartnerHostedOnly || VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.ConfigurePerformanceCounters.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000CA RID: 202
		private static Dictionary<ExPerformanceCounter, RunningAverageFloat> latencycounterToRunningAverageFloatMap = new Dictionary<ExPerformanceCounter, RunningAverageFloat>();

		// Token: 0x040000CB RID: 203
		private static Dictionary<ExPerformanceCounter, RunningPercentage> latencycounterToRunningPercentageMap = new Dictionary<ExPerformanceCounter, RunningPercentage>();

		// Token: 0x040000CC RID: 204
		private static HashSet<string> httpProxyPerSiteInitializedInstances = new HashSet<string>();

		// Token: 0x040000CD RID: 205
		private static LazyMember<HttpProxyCountersInstance> counters = new LazyMember<HttpProxyCountersInstance>(delegate()
		{
			HttpProxyCountersInstance instance = HttpProxyCounters.GetInstance(HttpProxyGlobals.ProtocolType.ToString());
			PerfCounters.InitMaps<HttpProxyCountersInstance>(instance);
			return instance;
		});

		// Token: 0x040000CE RID: 206
		private static LazyMember<HttpProxyCacheCountersInstance> cacheCounters = new LazyMember<HttpProxyCacheCountersInstance>(delegate()
		{
			HttpProxyCacheCountersInstance instance = HttpProxyCacheCounters.GetInstance(HttpProxyGlobals.ProtocolType.ToString());
			PerfCounters.InitMaps<HttpProxyCacheCountersInstance>(instance);
			return instance;
		});
	}
}
