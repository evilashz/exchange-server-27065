using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200008F RID: 143
	public static class UnpublishedSnt
	{
		// Token: 0x040002F0 RID: 752
		public const JET_SNT OpenLog = (JET_SNT)1001;

		// Token: 0x040002F1 RID: 753
		public const JET_SNT OpenCheckpoint = (JET_SNT)1002;

		// Token: 0x040002F2 RID: 754
		public const JET_SNT MissingLog = (JET_SNT)1004;

		// Token: 0x040002F3 RID: 755
		public const JET_SNT BeginUndo = (JET_SNT)1005;

		// Token: 0x040002F4 RID: 756
		public const JET_SNT NotificationEvent = (JET_SNT)1006;

		// Token: 0x040002F5 RID: 757
		public const JET_SNT SignalErrorCondition = (JET_SNT)1007;

		// Token: 0x040002F6 RID: 758
		public const JET_SNT DbAttached = (JET_SNT)1008;

		// Token: 0x040002F7 RID: 759
		public const JET_SNT DbDetached = (JET_SNT)1009;

		// Token: 0x040002F8 RID: 760
		public const JET_SNT PagePatchRequest = (JET_SNT)1101;

		// Token: 0x040002F9 RID: 761
		public const JET_SNT CorruptedPage = (JET_SNT)1102;
	}
}
