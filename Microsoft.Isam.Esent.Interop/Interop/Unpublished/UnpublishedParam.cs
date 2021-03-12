using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200008C RID: 140
	public static class UnpublishedParam
	{
		// Token: 0x040002D8 RID: 728
		public const JET_param AssertAction = (JET_param)36;

		// Token: 0x040002D9 RID: 729
		public const JET_param AccessDeniedRetryPeriod = (JET_param)53;

		// Token: 0x040002DA RID: 730
		public const JET_param ExceptionAction = JET_param.ExceptionAction;

		// Token: 0x040002DB RID: 731
		public const JET_param MaxCoalesceReadSize = (JET_param)164;

		// Token: 0x040002DC RID: 732
		public const JET_param MaxCoalesceWriteSize = (JET_param)165;

		// Token: 0x040002DD RID: 733
		public const JET_param MaxCoalesceReadGapSize = (JET_param)166;

		// Token: 0x040002DE RID: 734
		public const JET_param MaxCoalesceWriteGapSize = (JET_param)167;

		// Token: 0x040002DF RID: 735
		public const JET_param WaypointLatency = (JET_param)153;

		// Token: 0x040002E0 RID: 736
		public const JET_param CheckpointTooDeep = (JET_param)154;

		// Token: 0x040002E1 RID: 737
		public const JET_param AggressiveLogRollover = (JET_param)157;

		// Token: 0x040002E2 RID: 738
		public const JET_param EnableHaPublish = (JET_param)168;

		// Token: 0x040002E3 RID: 739
		public const JET_param EmitLogDataCallback = (JET_param)173;

		// Token: 0x040002E4 RID: 740
		public const JET_param EmitLogDataCallbackCtx = (JET_param)174;

		// Token: 0x040002E5 RID: 741
		public const JET_param EnableExternalAutoHealing = (JET_param)175;

		// Token: 0x040002E6 RID: 742
		public const JET_param PatchRequestTimeout = (JET_param)176;

		// Token: 0x040002E7 RID: 743
		public const JET_param AutomaticShrinkDatabaseFreeSpaceThreshold = (JET_param)185;

		// Token: 0x040002E8 RID: 744
		public const JET_param ConfigStoreSpec = (JET_param)189;

		// Token: 0x040002E9 RID: 745
		public const JET_param StageFlighting = (JET_param)190;

		// Token: 0x040002EA RID: 746
		public const JET_param ZeroDatabaseUnusedSpace = (JET_param)191;
	}
}
