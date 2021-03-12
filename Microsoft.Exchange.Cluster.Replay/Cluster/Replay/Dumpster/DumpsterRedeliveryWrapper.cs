using System;

namespace Microsoft.Exchange.Cluster.Replay.Dumpster
{
	// Token: 0x02000175 RID: 373
	internal static class DumpsterRedeliveryWrapper
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x00040038 File Offset: 0x0003E238
		public static void MarkRedeliveryRequired(ReplayConfiguration configuration, DateTime inspectorTime, long lastLogGenBeforeActivation, long numLogsLost)
		{
			SafetyNetRedelivery.MarkRedeliveryRequired(configuration, inspectorTime, lastLogGenBeforeActivation, numLogsLost);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00040043 File Offset: 0x0003E243
		public static void MarkRedeliveryRequired(ReplayConfiguration configuration, DateTime failoverTimeUtc, DateTime startTimeUtc, DateTime endTimeUtc, long lastLogGenBeforeActivation, long numLogsLost)
		{
			SafetyNetRedelivery.MarkRedeliveryRequired(configuration, failoverTimeUtc, startTimeUtc, endTimeUtc, lastLogGenBeforeActivation, numLogsLost);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00040052 File Offset: 0x0003E252
		public static void DoRedeliveryIfRequired(object replayConfig)
		{
			SafetyNetRedelivery.DoRedeliveryIfRequired(replayConfig);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0004005C File Offset: 0x0003E25C
		public static bool IsRedeliveryRequired(ReplayConfiguration replayConfig)
		{
			SafetyNetInfoCache safetyNetTable = replayConfig.ReplayState.GetSafetyNetTable();
			return safetyNetTable.IsRedeliveryRequired(true, true);
		}
	}
}
