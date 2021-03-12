using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.EventLogs;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x0200000B RID: 11
	internal static class ConcurrencyGuards
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002D2D File Offset: 0x00000F2D
		public static ConcurrencyGuard TargetBackend
		{
			get
			{
				return ConcurrencyGuards.targetBackend;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002D34 File Offset: 0x00000F34
		public static ConcurrencyGuard TargetDag
		{
			get
			{
				return ConcurrencyGuards.targetDag;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002D3B File Offset: 0x00000F3B
		public static ConcurrencyGuard TargetForest
		{
			get
			{
				return ConcurrencyGuards.targetForest;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002D42 File Offset: 0x00000F42
		public static ConcurrencyGuard SharedCache
		{
			get
			{
				return ConcurrencyGuards.sharedCache;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D4C File Offset: 0x00000F4C
		private static void LogTargetOustandingRequests(ConcurrencyGuard guard, string bucketName, object stateObject)
		{
			RequestDetailsLogger requestDetailsLogger = stateObject as RequestDetailsLogger;
			if (requestDetailsLogger == null)
			{
				return;
			}
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, HttpProxyMetadata.TargetOutstandingRequests, guard.GetCurrentValue(bucketName));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D80 File Offset: 0x00000F80
		private static void LogEventOnRejectDelegate(ConcurrencyGuard guard, string bucketName, object stateObject, Exception ex)
		{
			string text = ConcurrencyGuard.FormatGuardBucketName(guard, bucketName);
			Diagnostics.Logger.LogEvent(FrontEndHttpProxyEventLogConstants.Tuple_TooManyOutstandingRequests, text, new object[]
			{
				HttpProxyGlobals.ProtocolType,
				text,
				guard.MaxConcurrency
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002DF4 File Offset: 0x00000FF4
		// Note: this type is marked as 'beforefieldinit'.
		static ConcurrencyGuards()
		{
			string guardName = "TargetBackend";
			int value = ConcurrencyGuards.TargetBackendLimit.Value;
			Action<ConcurrencyGuard, string, object> onIncrementDelegate = new Action<ConcurrencyGuard, string, object>(ConcurrencyGuards.LogTargetOustandingRequests);
			Action<ConcurrencyGuard, string, object, MaxConcurrencyReachedException> onRejectDelegate = new Action<ConcurrencyGuard, string, object, MaxConcurrencyReachedException>(ConcurrencyGuards.LogEventOnRejectDelegate);
			ConcurrencyGuards.targetBackend = new ConcurrencyGuard(guardName, value, ConcurrencyGuards.UseTrainingMode, onIncrementDelegate, null, null, onRejectDelegate);
			string guardName2 = "TargetDag";
			int value2 = ConcurrencyGuards.TargetDagLimit.Value;
			Action<ConcurrencyGuard, string, object, MaxConcurrencyReachedException> onRejectDelegate2 = new Action<ConcurrencyGuard, string, object, MaxConcurrencyReachedException>(ConcurrencyGuards.LogEventOnRejectDelegate);
			ConcurrencyGuards.targetDag = new ConcurrencyGuard(guardName2, value2, ConcurrencyGuards.UseTrainingMode, null, null, null, onRejectDelegate2);
			string guardName3 = "TargetForest";
			int value3 = ConcurrencyGuards.TargetForestLimit.Value;
			Action<ConcurrencyGuard, string, object, MaxConcurrencyReachedException> onRejectDelegate3 = new Action<ConcurrencyGuard, string, object, MaxConcurrencyReachedException>(ConcurrencyGuards.LogEventOnRejectDelegate);
			ConcurrencyGuards.targetForest = new ConcurrencyGuard(guardName3, value3, ConcurrencyGuards.UseTrainingMode, null, null, null, onRejectDelegate3);
			string guardName4 = "SharedCache";
			int value4 = ConcurrencyGuards.SharedCacheLimit.Value;
			Action<ConcurrencyGuard, string, object> onIncrementDelegate2 = delegate(ConcurrencyGuard a, string b, object c)
			{
				PerfCounters.HttpProxyCountersInstance.OutstandingSharedCacheRequests.Increment();
			};
			Action<ConcurrencyGuard, string, object> onDecrementDelegate = delegate(ConcurrencyGuard a, string b, object c)
			{
				PerfCounters.HttpProxyCountersInstance.OutstandingSharedCacheRequests.Decrement();
			};
			ConcurrencyGuards.sharedCache = new ConcurrencyGuard(guardName4, value4, ConcurrencyGuards.UseTrainingMode, onIncrementDelegate2, onDecrementDelegate, null, null);
		}

		// Token: 0x0400002F RID: 47
		private static readonly IntAppSettingsEntry TargetBackendLimit = new IntAppSettingsEntry(HttpProxySettings.Prefix("ConcurrencyGuards.TargetBackendLimit"), 150, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000030 RID: 48
		private static readonly IntAppSettingsEntry TargetDagLimit = new IntAppSettingsEntry(HttpProxySettings.Prefix("ConcurrencyGuards.TargetDagLimit"), 5000, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000031 RID: 49
		private static readonly IntAppSettingsEntry TargetForestLimit = new IntAppSettingsEntry(HttpProxySettings.Prefix("ConcurrencyGuards.TargetForestLimit"), 15000, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000032 RID: 50
		private static readonly IntAppSettingsEntry SharedCacheLimit = new IntAppSettingsEntry(HttpProxySettings.Prefix("ConcurrencyGuards.SharedCacheLimit"), 100, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000033 RID: 51
		private static readonly bool UseTrainingMode = !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.EnforceConcurrencyGuards.Enabled;

		// Token: 0x04000034 RID: 52
		private static ConcurrencyGuard targetBackend;

		// Token: 0x04000035 RID: 53
		private static ConcurrencyGuard targetDag;

		// Token: 0x04000036 RID: 54
		private static ConcurrencyGuard targetForest;

		// Token: 0x04000037 RID: 55
		private static ConcurrencyGuard sharedCache;
	}
}
