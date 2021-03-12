using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F4 RID: 244
	internal static class CmdletLatencyTracker
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x0001BF7C File Offset: 0x0001A17C
		internal static LatencyTracker GetLatencyTracker(Guid cmdletUniqueId)
		{
			if (cmdletUniqueId == Guid.Empty && !CmdletThreadStaticData.TryGetCurrentCmdletUniqueId(out cmdletUniqueId))
			{
				return null;
			}
			LatencyTracker result;
			CmdletStaticDataWithUniqueId<LatencyTracker>.TryGet(cmdletUniqueId, out result);
			return result;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001BFAC File Offset: 0x0001A1AC
		internal static void StartLatencyTracker(Guid cmdletUniqueId)
		{
			LatencyTracker latencyTracker = CmdletStaticDataWithUniqueId<LatencyTracker>.Get(cmdletUniqueId);
			if (latencyTracker != null)
			{
				CmdletLogger.SafeAppendGenericError(cmdletUniqueId, "StartLatencyTracker", string.Format("Latency tracker with this cmdlet {0} already exists. Override it anyway.", cmdletUniqueId), false);
			}
			latencyTracker = new LatencyTracker(cmdletUniqueId.ToString(), new Func<IActivityScope>(ActivityContext.GetCurrentActivityScope));
			latencyTracker.Start();
			CmdletStaticDataWithUniqueId<LatencyTracker>.Set(cmdletUniqueId, latencyTracker);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001C00C File Offset: 0x0001A20C
		internal static long StopLatencyTracker(Guid cmdletUniqueId)
		{
			LatencyTracker latencyTracker = CmdletStaticDataWithUniqueId<LatencyTracker>.Get(cmdletUniqueId);
			if (latencyTracker == null)
			{
				return -1L;
			}
			return latencyTracker.Stop();
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001C02C File Offset: 0x0001A22C
		internal static void DisposeLatencyTracker(Guid cmdletUniqueId)
		{
			CmdletStaticDataWithUniqueId<LatencyTracker>.Remove(cmdletUniqueId);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001C035 File Offset: 0x0001A235
		internal static bool StartInternalTracking(Guid cmdletUniqueId, string funcName)
		{
			return CmdletLatencyTracker.StartInternalTracking(cmdletUniqueId, funcName, false);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001C03F File Offset: 0x0001A23F
		internal static bool StartInternalTracking(Guid cmdletUniqueId, string funcName, bool logDetailsAlways)
		{
			return CmdletLatencyTracker.StartInternalTracking(cmdletUniqueId, funcName, funcName, logDetailsAlways);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001C04C File Offset: 0x0001A24C
		internal static bool StartInternalTracking(Guid cmdletUniqueId, string groupName, string funcName, bool logDetailsAlways)
		{
			LatencyTracker latencyTracker = CmdletLatencyTracker.GetLatencyTracker(cmdletUniqueId);
			return latencyTracker != null && latencyTracker.StartInternalTracking(groupName, funcName, logDetailsAlways);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001C06E File Offset: 0x0001A26E
		internal static void EndInternalTracking(Guid cmdletUniqueId, string funcName)
		{
			CmdletLatencyTracker.EndInternalTracking(cmdletUniqueId, funcName, funcName);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001C078 File Offset: 0x0001A278
		internal static void EndInternalTracking(Guid cmdletUniqueId, string groupName, string funcName)
		{
			LatencyTracker latencyTracker = CmdletLatencyTracker.GetLatencyTracker(cmdletUniqueId);
			if (latencyTracker == null)
			{
				return;
			}
			latencyTracker.EndInternalTracking(groupName, funcName);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001C098 File Offset: 0x0001A298
		internal static void PushLatencyDetailsToLog(Guid cmdletUniqueId, Dictionary<string, Enum> knownFuncNameToLogMetadataDic, Action<Enum, double> updateLatencyToLogger, Action<string, string> defaultLatencyLogger)
		{
			LatencyTracker latencyTracker = CmdletLatencyTracker.GetLatencyTracker(cmdletUniqueId);
			if (latencyTracker != null)
			{
				latencyTracker.PushLatencyDetailsToLog(knownFuncNameToLogMetadataDic, updateLatencyToLogger, defaultLatencyLogger);
				return;
			}
			if (defaultLatencyLogger != null)
			{
				defaultLatencyLogger("LatencyMissed", "latencyTracker is null");
				return;
			}
			if (updateLatencyToLogger != null)
			{
				updateLatencyToLogger(RpsCommonMetadata.GenericLatency, 0.0);
			}
		}
	}
}
