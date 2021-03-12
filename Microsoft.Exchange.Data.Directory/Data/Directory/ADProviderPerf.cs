using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory.Cache;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000065 RID: 101
	internal static class ADProviderPerf
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0001A8E7 File Offset: 0x00018AE7
		public static void PrepareDCCountersForRefresh()
		{
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				Globals.InitializeUnknownPerfCounterInstance();
			}
			NativeMethods.DsaccessPerfDCPrepareForRefresh();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001A8FA File Offset: 0x00018AFA
		public static void FinalizeDCCountersRefresh()
		{
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				Globals.InitializeUnknownPerfCounterInstance();
			}
			NativeMethods.DsaccessPerfDCFinalizeRefresh();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001A90D File Offset: 0x00018B0D
		public static void AddDCInstance(string serverName)
		{
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				Globals.InitializeUnknownPerfCounterInstance();
			}
			NativeMethods.DsaccessPerfDCAddToList(serverName);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001A921 File Offset: 0x00018B21
		public static void UpdateProcessCounter(Counter counter, UpdateType updateType, uint value)
		{
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				Globals.InitializeUnknownPerfCounterInstance();
			}
			NativeMethods.DsaccessPerfCounterUpdate(76U, (uint)counter, (uint)updateType, value, null);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001A93C File Offset: 0x00018B3C
		public static void UpdateProcessTimeSearchPercentileCounter(uint value)
		{
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				Globals.InitializeUnknownPerfCounterInstance();
			}
			ADProviderPerf.perProcessPercentileADLatency.AddValue((long)((ulong)value));
			uint value2 = (uint)ADProviderPerf.perProcessPercentileADLatency.PercentileQuery(90.0);
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchNinetiethPercentile, UpdateType.Add, value2);
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchNinetiethPercentileBase, UpdateType.Add, 1U);
			value2 = (uint)ADProviderPerf.perProcessPercentileADLatency.PercentileQuery(95.0);
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchNinetyFifthPercentile, UpdateType.Add, value2);
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchNinetyFifthPercentileBase, UpdateType.Add, 1U);
			value2 = (uint)ADProviderPerf.perProcessPercentileADLatency.PercentileQuery(99.0);
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchNinetyNinethPercentile, UpdateType.Add, value2);
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessTimeSearchNinetyNinethPercentileBase, UpdateType.Add, 1U);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001A9DF File Offset: 0x00018BDF
		public static void UpdateDCCounter(string dcName, Counter counter, UpdateType updateType, uint value)
		{
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				Globals.InitializeUnknownPerfCounterInstance();
			}
			NativeMethods.DsaccessPerfCounterUpdate(146U, (uint)counter, (uint)updateType, value, dcName);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001A9FB File Offset: 0x00018BFB
		public static void UpdateGlobalCounter(Counter counter, UpdateType updateType, uint value)
		{
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				Globals.InitializeUnknownPerfCounterInstance();
			}
			NativeMethods.DsaccessPerfCounterUpdate(268U, (uint)counter, (uint)updateType, value, null);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001AA18 File Offset: 0x00018C18
		public static void UpdateGlsCallLatency(string apiName, bool isRead, int latencyMsec, bool success)
		{
			GlsProcessPerformanceCountersInstance value = ADProviderPerf.processGlsCounters.Value;
			GlsPerformanceCounters.AverageOverallLatency.IncrementBy((long)latencyMsec);
			GlsPerformanceCounters.AverageOverallLatencyBase.Increment();
			if (isRead)
			{
				GlsPerformanceCounters.AverageReadLatency.IncrementBy((long)latencyMsec);
				GlsPerformanceCounters.AverageReadLatencyBase.Increment();
				if (value != null)
				{
					value.AverageReadLatency.IncrementBy((long)latencyMsec);
					value.AverageReadLatencyBase.Increment();
				}
			}
			else
			{
				GlsPerformanceCounters.AverageWriteLatency.IncrementBy((long)latencyMsec);
				GlsPerformanceCounters.AverageWriteLatencyBase.Increment();
				if (value != null)
				{
					value.AverageWriteLatency.IncrementBy((long)latencyMsec);
					value.AverageWriteLatencyBase.Increment();
				}
			}
			if (value != null)
			{
				value.AverageOverallLatency.IncrementBy((long)latencyMsec);
				value.AverageOverallLatencyBase.Increment();
				ADProviderPerf.perProcessPercentileGlsLatency.AddValue((long)latencyMsec);
				uint num = (uint)ADProviderPerf.perProcessPercentileGlsLatency.PercentileQuery(95.0);
				value.NinetyFifthPercentileLatency.IncrementBy((long)((ulong)num));
				value.NinetyFifthPercentileLatencyBase.Increment();
				num = (uint)ADProviderPerf.perProcessPercentileGlsLatency.PercentileQuery(99.0);
				value.NinetyNinthPercentileLatency.IncrementBy((long)((ulong)num));
				value.NinetyNinthPercentileLatencyBase.Increment();
				lock (ADProviderPerf.slidingTotalLockRoot)
				{
					if (success)
					{
						ADProviderPerf.successesPerMinute.AddValue(1L);
					}
					else
					{
						ADProviderPerf.failuresPerMinute.AddValue(1L);
					}
				}
				ADProviderPerf.InitializeTimerIfRequired();
			}
			switch (apiName)
			{
			case "FindTenant":
				GlsApiPerformanceCounters.FindTenantAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.FindTenantAverageOverallLatencyBase.Increment();
				return;
			case "FindDomain":
			case "FindDomains":
				GlsApiPerformanceCounters.FindDomainAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.FindDomainAverageOverallLatencyBase.Increment();
				return;
			case "FindUser":
				GlsApiPerformanceCounters.FindUserAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.FindUserAverageOverallLatencyBase.Increment();
				return;
			case "SaveTenant":
				GlsApiPerformanceCounters.SaveTenantAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.SaveTenantAverageOverallLatencyBase.Increment();
				return;
			case "SaveDomain":
				GlsApiPerformanceCounters.SaveDomainAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.SaveDomainAverageOverallLatencyBase.Increment();
				return;
			case "SaveUser":
				GlsApiPerformanceCounters.SaveUserAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.SaveUserAverageOverallLatencyBase.Increment();
				return;
			case "DeleteTenant":
				GlsApiPerformanceCounters.DeleteTenantAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.DeleteTenantAverageOverallLatencyBase.Increment();
				return;
			case "DeleteDomain":
				GlsApiPerformanceCounters.DeleteDomainAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.DeleteDomainAverageOverallLatencyBase.Increment();
				return;
			case "DeleteUser":
				GlsApiPerformanceCounters.DeleteUserAverageOverallLatency.IncrementBy((long)latencyMsec);
				GlsApiPerformanceCounters.DeleteUserAverageOverallLatencyBase.Increment();
				return;
			}
			throw new ArgumentException("Unknown API " + apiName);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001AD68 File Offset: 0x00018F68
		private static void UpdatePerMinuteCounters(object state)
		{
			GlsProcessPerformanceCountersInstance value = ADProviderPerf.processGlsCounters.Value;
			if (value != null)
			{
				lock (ADProviderPerf.slidingTotalLockRoot)
				{
					value.SuccessfulCallsPerMinute.RawValue = ADProviderPerf.successesPerMinute.Sum;
					value.FailedCallsPerMinute.RawValue = ADProviderPerf.failuresPerMinute.Sum;
					value.NotFoundCallsPerMinute.RawValue = ADProviderPerf.notFoundsPerMinute.Sum;
					value.CacheHitsRatioPerMinute.RawValue = (long)ADProviderPerf.cacheHitsPercentageForLastMinute.GetSlidingPercentage();
					value.AcceptedDomainLookupCacheHitsRatioPerMinute.RawValue = (long)ADProviderPerf.acceptedDomainLookupCacheHitsPercentageForLastMinute.GetSlidingPercentage();
					value.ExternalDirectoryOrganizationIdCacheHitsRatioPerMinute.RawValue = (long)ADProviderPerf.externalDirOrgIdCacheHitsPercentageForLastMinute.GetSlidingPercentage();
					value.MSAUserNetIdCacheHitsRatioPerMinute.RawValue = (long)ADProviderPerf.msaUserNetIdLookupCacheHitsPercentageForLastMinute.GetSlidingPercentage();
				}
			}
			if (ADProviderPerf.directoryCacheHitCounter.IsInitialized && ADProviderPerf.directoryCacheHitCounter.Value != null)
			{
				MSExchangeDirectoryCacheServiceCounters.CacheHit.RawValue = (long)ADProviderPerf.directoryCacheHitCounter.Value.GetSlidingPercentage();
			}
			if (ADProviderPerf.directoryAcceptedDomainCacheHitCounter.IsInitialized && ADProviderPerf.directoryAcceptedDomainCacheHitCounter.Value != null)
			{
				MSExchangeDirectoryCacheServiceCounters.AcceptedDomainHit.RawValue = (long)ADProviderPerf.directoryAcceptedDomainCacheHitCounter.Value.GetSlidingPercentage();
			}
			if (ADProviderPerf.directoryADRawEntryCacheHitCounter.IsInitialized && ADProviderPerf.directoryADRawEntryCacheHitCounter.Value != null)
			{
				MSExchangeDirectoryCacheServiceCounters.ADRawEntryCacheHit.RawValue = (long)ADProviderPerf.directoryADRawEntryCacheHitCounter.Value.GetSlidingPercentage();
			}
			if (ADProviderPerf.directoryADRawEntryPropertiesMisMatchCounter.IsInitialized && ADProviderPerf.directoryADRawEntryPropertiesMisMatchCounter.Value != null)
			{
				MSExchangeDirectoryCacheServiceCounters.ADRawEntryPropertiesMismatchLastMinute.RawValue = (long)ADProviderPerf.directoryADRawEntryPropertiesMisMatchCounter.Value.GetSlidingPercentage();
			}
			if (ADProviderPerf.directoryConfigUnitCacheHitCounter.IsInitialized && ADProviderPerf.directoryConfigUnitCacheHitCounter.Value != null)
			{
				MSExchangeDirectoryCacheServiceCounters.ConfigurationUnitHit.RawValue = (long)ADProviderPerf.directoryConfigUnitCacheHitCounter.Value.GetSlidingPercentage();
			}
			if (ADProviderPerf.directoryRecipientCacheHitCounter.IsInitialized && ADProviderPerf.directoryRecipientCacheHitCounter.Value != null)
			{
				MSExchangeDirectoryCacheServiceCounters.RecipientHit.RawValue = (long)ADProviderPerf.directoryRecipientCacheHitCounter.Value.GetSlidingPercentage();
			}
			if (ADProviderPerf.adDriverCacheHitCounter.IsInitialized && ADProviderPerf.adDriverCacheHitCounter.Value != null)
			{
				MSExchangeADAccessCacheCountersInstance value2 = ADProviderPerf.processADDriverCacheCounters.Value;
				if (value2 != null)
				{
					value2.CacheHit.RawValue = (long)ADProviderPerf.adDriverCacheHitCounter.Value.GetSlidingPercentage();
				}
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001AFB8 File Offset: 0x000191B8
		public static void IncrementNotFoundCounter()
		{
			lock (ADProviderPerf.slidingTotalLockRoot)
			{
				ADProviderPerf.notFoundsPerMinute.AddValue(1L);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001B000 File Offset: 0x00019200
		public static void UpdateGlsCacheHitRatio(GlsLookupKey glsLookupKey, bool cacheHit)
		{
			long numerator = cacheHit ? 1L : 0L;
			ADProviderPerf.cacheHitsPercentageForLastMinute.Add(numerator, 1L);
			switch (glsLookupKey)
			{
			case GlsLookupKey.ExternalDirectoryObjectId:
				ADProviderPerf.externalDirOrgIdCacheHitsPercentageForLastMinute.Add(numerator, 1L);
				return;
			case GlsLookupKey.AcceptedDomain:
				ADProviderPerf.acceptedDomainLookupCacheHitsPercentageForLastMinute.Add(numerator, 1L);
				return;
			case GlsLookupKey.MSAUserNetID:
				ADProviderPerf.msaUserNetIdLookupCacheHitsPercentageForLastMinute.Add(numerator, 1L);
				return;
			default:
				return;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001B068 File Offset: 0x00019268
		public static void UpdateDirectoryCacheHitRatio(bool cacheHit, ObjectType objectType)
		{
			ADProviderPerf.directoryCacheHitCounter.Value.AddDenominator(1L);
			if (cacheHit)
			{
				ADProviderPerf.directoryCacheHitCounter.Value.AddNumerator(1L);
			}
			if (objectType <= ObjectType.ActiveSyncMiniRecipient)
			{
				if (objectType <= ObjectType.MiniRecipient)
				{
					switch (objectType)
					{
					case ObjectType.ExchangeConfigurationUnit:
						ADProviderPerf.directoryConfigUnitCacheHitCounter.Value.AddDenominator(1L);
						if (cacheHit)
						{
							ADProviderPerf.directoryConfigUnitCacheHitCounter.Value.AddNumerator(1L);
							goto IL_15D;
						}
						goto IL_15D;
					case ObjectType.Recipient:
						break;
					case ObjectType.ExchangeConfigurationUnit | ObjectType.Recipient:
						goto IL_15D;
					case ObjectType.AcceptedDomain:
						ADProviderPerf.directoryAcceptedDomainCacheHitCounter.Value.AddDenominator(1L);
						if (cacheHit)
						{
							ADProviderPerf.directoryAcceptedDomainCacheHitCounter.Value.AddNumerator(1L);
							goto IL_15D;
						}
						goto IL_15D;
					default:
						if (objectType != ObjectType.MiniRecipient)
						{
							goto IL_15D;
						}
						break;
					}
				}
				else if (objectType != ObjectType.TransportMiniRecipient && objectType != ObjectType.OWAMiniRecipient && objectType != ObjectType.ActiveSyncMiniRecipient)
				{
					goto IL_15D;
				}
			}
			else if (objectType <= ObjectType.StorageMiniRecipient)
			{
				if (objectType != ObjectType.ADRawEntry)
				{
					if (objectType != ObjectType.StorageMiniRecipient)
					{
						goto IL_15D;
					}
				}
				else
				{
					ADProviderPerf.directoryADRawEntryCacheHitCounter.Value.AddDenominator(1L);
					if (cacheHit)
					{
						ADProviderPerf.directoryADRawEntryCacheHitCounter.Value.AddNumerator(1L);
						goto IL_15D;
					}
					goto IL_15D;
				}
			}
			else if (objectType != ObjectType.LoadBalancingMiniRecipient && objectType != ObjectType.MiniRecipientWithTokenGroups && objectType != ObjectType.FrontEndMiniRecipient)
			{
				goto IL_15D;
			}
			ADProviderPerf.directoryRecipientCacheHitCounter.Value.AddDenominator(1L);
			if (cacheHit)
			{
				ADProviderPerf.directoryRecipientCacheHitCounter.Value.AddNumerator(1L);
			}
			IL_15D:
			ADProviderPerf.InitializeTimerIfRequired();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001B1D7 File Offset: 0x000193D7
		public static void UpdateDirectoryADRawCachePropertiesMismatchRate(bool mismatch)
		{
			ADProviderPerf.directoryADRawEntryPropertiesMisMatchCounter.Value.AddDenominator(1L);
			if (mismatch)
			{
				ADProviderPerf.directoryADRawEntryPropertiesMisMatchCounter.Value.AddNumerator(1L);
			}
			ADProviderPerf.InitializeTimerIfRequired();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001B208 File Offset: 0x00019408
		public static void UpdateADDriverCacheHitRate(bool cacheHit)
		{
			ADProviderPerf.adDriverCacheHitCounter.Value.AddDenominator(1L);
			if (cacheHit)
			{
				ADProviderPerf.adDriverCacheHitCounter.Value.AddNumerator(1L);
			}
			MSExchangeADAccessCacheCountersInstance value = ADProviderPerf.processADDriverCacheCounters.Value;
			if (value != null)
			{
				value.NumberOfCacheRequests.Increment();
			}
			ADProviderPerf.InitializeTimerIfRequired();
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001B25C File Offset: 0x0001945C
		private static void InitializeTimerIfRequired()
		{
			if (ADProviderPerf.updateTimer == null)
			{
				lock (ADProviderPerf.guardedTimerLockRoot)
				{
					if (ADProviderPerf.updateTimer == null)
					{
						ADProviderPerf.updateTimer = new GuardedTimer(new TimerCallback(ADProviderPerf.UpdatePerMinuteCounters), null, ADProviderPerf.TenSeconds, ADProviderPerf.TenSeconds);
					}
				}
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001B2C4 File Offset: 0x000194C4
		private static GlsProcessPerformanceCountersInstance CreateGlsProcessPerfCountersInstance()
		{
			GlsProcessPerformanceCountersInstance result = null;
			string text = null;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				text = string.Format("{0} {1}{2}{3}", new object[]
				{
					currentProcess.ProcessName,
					Globals.ProcessAppName ?? string.Empty,
					string.IsNullOrEmpty(Globals.ProcessAppName) ? string.Empty : " ",
					Globals.ProcessId
				});
			}
			try
			{
				result = GlsProcessPerformanceCounters.GetInstance(text);
			}
			catch (InvalidOperationException arg)
			{
				ExTraceGlobals.PerfCountersTracer.TraceError<string, InvalidOperationException>(0L, "Get GlsProcessPerformanceCountersInstance {0} failed due to: {1}", text, arg);
			}
			return result;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001B380 File Offset: 0x00019580
		private static MSExchangeADAccessCacheCountersInstance CreateADDriverCacheProcessPerfCountersInstance()
		{
			MSExchangeADAccessCacheCountersInstance result = null;
			try
			{
				result = MSExchangeADAccessCacheCounters.GetInstance(Globals.ProcessNameAppName);
			}
			catch (InvalidOperationException arg)
			{
				ExTraceGlobals.PerfCountersTracer.TraceError<string, InvalidOperationException>(0L, "Get MSExchangeADAccessCacheCountersInstance {0} failed due to: {1}", Globals.ProcessNameAppName, arg);
			}
			return result;
		}

		// Token: 0x040001EF RID: 495
		private const double LatencyPercentile90 = 90.0;

		// Token: 0x040001F0 RID: 496
		private const double LatencyPercentile95 = 95.0;

		// Token: 0x040001F1 RID: 497
		private const double LatencyPercentile99 = 99.0;

		// Token: 0x040001F2 RID: 498
		private static readonly PercentileCounter perProcessPercentileADLatency = new PercentileCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0), 10L, 10000L);

		// Token: 0x040001F3 RID: 499
		private static readonly PercentileCounter perProcessPercentileGlsLatency = new PercentileCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0), 10L, 10000L);

		// Token: 0x040001F4 RID: 500
		private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1.0);

		// Token: 0x040001F5 RID: 501
		private static readonly TimeSpan TenSeconds = TimeSpan.FromSeconds(10.0);

		// Token: 0x040001F6 RID: 502
		private static readonly object slidingTotalLockRoot = new object();

		// Token: 0x040001F7 RID: 503
		private static readonly SlidingTotalCounter successesPerMinute = new SlidingTotalCounter(ADProviderPerf.OneMinute, ADProviderPerf.TenSeconds);

		// Token: 0x040001F8 RID: 504
		private static readonly SlidingTotalCounter failuresPerMinute = new SlidingTotalCounter(ADProviderPerf.OneMinute, ADProviderPerf.TenSeconds);

		// Token: 0x040001F9 RID: 505
		private static readonly SlidingTotalCounter notFoundsPerMinute = new SlidingTotalCounter(ADProviderPerf.OneMinute, ADProviderPerf.TenSeconds);

		// Token: 0x040001FA RID: 506
		private static readonly SlidingPercentageCounter cacheHitsPercentageForLastMinute = new SlidingPercentageCounter(ADProviderPerf.OneMinute, ADProviderPerf.TenSeconds);

		// Token: 0x040001FB RID: 507
		private static readonly SlidingPercentageCounter acceptedDomainLookupCacheHitsPercentageForLastMinute = new SlidingPercentageCounter(ADProviderPerf.OneMinute, ADProviderPerf.TenSeconds);

		// Token: 0x040001FC RID: 508
		private static readonly SlidingPercentageCounter externalDirOrgIdCacheHitsPercentageForLastMinute = new SlidingPercentageCounter(ADProviderPerf.OneMinute, ADProviderPerf.TenSeconds);

		// Token: 0x040001FD RID: 509
		private static readonly SlidingPercentageCounter msaUserNetIdLookupCacheHitsPercentageForLastMinute = new SlidingPercentageCounter(ADProviderPerf.OneMinute, ADProviderPerf.TenSeconds);

		// Token: 0x040001FE RID: 510
		private static readonly LazilyInitialized<GlsProcessPerformanceCountersInstance> processGlsCounters = new LazilyInitialized<GlsProcessPerformanceCountersInstance>(() => ADProviderPerf.CreateGlsProcessPerfCountersInstance());

		// Token: 0x040001FF RID: 511
		private static readonly LazilyInitialized<MSExchangeADAccessCacheCountersInstance> processADDriverCacheCounters = new LazilyInitialized<MSExchangeADAccessCacheCountersInstance>(() => ADProviderPerf.CreateADDriverCacheProcessPerfCountersInstance());

		// Token: 0x04000200 RID: 512
		private static readonly LazilyInitialized<SlidingPercentageCounter> directoryCacheHitCounter = new LazilyInitialized<SlidingPercentageCounter>(() => new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(15.0)));

		// Token: 0x04000201 RID: 513
		private static readonly LazilyInitialized<SlidingPercentageCounter> directoryAcceptedDomainCacheHitCounter = new LazilyInitialized<SlidingPercentageCounter>(() => new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(15.0)));

		// Token: 0x04000202 RID: 514
		private static readonly LazilyInitialized<SlidingPercentageCounter> directoryConfigUnitCacheHitCounter = new LazilyInitialized<SlidingPercentageCounter>(() => new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(15.0)));

		// Token: 0x04000203 RID: 515
		private static readonly LazilyInitialized<SlidingPercentageCounter> directoryRecipientCacheHitCounter = new LazilyInitialized<SlidingPercentageCounter>(() => new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(15.0)));

		// Token: 0x04000204 RID: 516
		private static readonly LazilyInitialized<SlidingPercentageCounter> directoryADRawEntryCacheHitCounter = new LazilyInitialized<SlidingPercentageCounter>(() => new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(15.0)));

		// Token: 0x04000205 RID: 517
		private static readonly LazilyInitialized<SlidingPercentageCounter> directoryADRawEntryPropertiesMisMatchCounter = new LazilyInitialized<SlidingPercentageCounter>(() => new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(15.0)));

		// Token: 0x04000206 RID: 518
		private static readonly LazilyInitialized<SlidingPercentageCounter> adDriverCacheHitCounter = new LazilyInitialized<SlidingPercentageCounter>(() => new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(15.0)));

		// Token: 0x04000207 RID: 519
		private static readonly object guardedTimerLockRoot = new object();

		// Token: 0x04000208 RID: 520
		private static GuardedTimer updateTimer;
	}
}
