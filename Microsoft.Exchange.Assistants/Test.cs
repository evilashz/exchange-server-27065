using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200007F RID: 127
	internal static class Test
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00011D7B File Offset: 0x0000FF7B
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00011D82 File Offset: 0x0000FF82
		public static Test.NotifyAllWatermarksCommittedDelegate NotifyAllWatermarksCommitted { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00011D8A File Offset: 0x0000FF8A
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00011D91 File Offset: 0x0000FF91
		public static Test.NotifyOnPostWatermarkCommitDelegate NotifyOnPostWatermarkCommit { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00011D99 File Offset: 0x0000FF99
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		public static Test.NotifyPhase1ShutdownCompleteDelegate NotifyPhase1ShutdownComplete { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00011DA8 File Offset: 0x0000FFA8
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00011DAF File Offset: 0x0000FFAF
		public static Test.NotifyPoisonEventSkippedDelegate NotifyPoisonEventSkipped { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00011DB7 File Offset: 0x0000FFB7
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00011DBE File Offset: 0x0000FFBE
		public static Test.NotifyPoisonMailboxSkippedDelegate NotifyPoisonMailboxSkipped { get; set; }

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x060003B5 RID: 949
		public delegate void NotifyPhase1ShutdownCompleteDelegate();

		// Token: 0x02000081 RID: 129
		// (Invoke) Token: 0x060003B9 RID: 953
		internal delegate void NotifyAllWatermarksCommittedDelegate();

		// Token: 0x02000082 RID: 130
		// (Invoke) Token: 0x060003BD RID: 957
		internal delegate void NotifyOnPostWatermarkCommitDelegate(Watermark[] watermarksToSave, bool experiencedPartialCompletion);

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x060003C1 RID: 961
		internal delegate void NotifyPoisonEventSkippedDelegate(DatabaseInfo databaseInfo, MapiEvent mapiEvent);

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x060003C5 RID: 965
		internal delegate void NotifyPoisonMailboxSkippedDelegate(DatabaseInfo databaseInfo, Guid mailboxGuid);
	}
}
