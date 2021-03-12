using System;
using System.Text;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000179 RID: 377
	internal sealed class PublicFolderSplitConfig : Config
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x000599AA File Offset: 0x00057BAA
		private PublicFolderSplitConfig()
		{
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x000599B2 File Offset: 0x00057BB2
		internal static PublicFolderSplitConfig Instance
		{
			get
			{
				return PublicFolderSplitConfig.splitConfigInstance;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000599B9 File Offset: 0x00057BB9
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x000599C1 File Offset: 0x00057BC1
		internal ulong SplitThreshold { get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000599CA File Offset: 0x00057BCA
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000599D2 File Offset: 0x00057BD2
		internal TimeSpan SleepTimeBeforeSplitProcessingStarts { get; private set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000599DB File Offset: 0x00057BDB
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x000599E3 File Offset: 0x00057BE3
		internal TimeSpan TimeoutForSynchronousOperation { get; private set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000599EC File Offset: 0x00057BEC
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x000599F4 File Offset: 0x00057BF4
		internal bool SplitProcessingEnabled { get; private set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000599FD File Offset: 0x00057BFD
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00059A05 File Offset: 0x00057C05
		internal TimeSpan MaxJobAgeForDiagnostics { get; private set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00059A0E File Offset: 0x00057C0E
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00059A16 File Offset: 0x00057C16
		internal ulong MaxTargetOccupiedThreshold { get; private set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00059A1F File Offset: 0x00057C1F
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00059A27 File Offset: 0x00057C27
		internal TimeSpan QuerySyncStatusInterval { get; private set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00059A30 File Offset: 0x00057C30
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00059A38 File Offset: 0x00057C38
		internal int LongRunningSyncChangeCount { get; private set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00059A41 File Offset: 0x00057C41
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00059A49 File Offset: 0x00057C49
		internal int CompletedPublicFolderMoveRequestAgeLimit { get; private set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00059A52 File Offset: 0x00057C52
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00059A5A File Offset: 0x00057C5A
		internal int MoveInProgressRetryCount { get; private set; }

		// Token: 0x06000F12 RID: 3858 RVA: 0x00059A64 File Offset: 0x00057C64
		public override void Load()
		{
			this.SplitThreshold = (ulong)((long)base.ReadInt("SplitThreshold", 80));
			this.MaxTargetOccupiedThreshold = (ulong)((long)base.ReadInt("MaxTargetOccupiedThreshold", PublicFolderSplitConfig.DefaultMaxTargetOccupiedThreshold));
			this.SleepTimeBeforeSplitProcessingStarts = base.ReadTimeSpan("SleepTimeBeforeSplitProcessingStarts", PublicFolderSplitConfig.DefaultSleepTimeBeforeSplitProcessingStarts);
			this.TimeoutForSynchronousOperation = base.ReadTimeSpan("TimeoutValueForSynchronousOperation", PublicFolderSplitConfig.DefaultTimeoutForSynchronousOperation);
			this.SplitProcessingEnabled = base.ReadBool("SplitProcessingEnabled", true);
			this.MaxJobAgeForDiagnostics = base.ReadTimeSpan("MaxJobAgeForDiagnostics", PublicFolderSplitConfig.DefaultMaxJobAgeForDiagnostics);
			this.QuerySyncStatusInterval = base.ReadTimeSpan("QuerySyncStatusInterval", PublicFolderSplitConfig.DefaultQuerySyncStatusInterval);
			this.LongRunningSyncChangeCount = base.ReadInt("LongRunningSyncChangeCount", PublicFolderSplitConfig.DefaultLongRunningSyncChangeCount);
			this.CompletedPublicFolderMoveRequestAgeLimit = base.ReadInt("CompletedPublicFolderMoveRequestAgeLimit", PublicFolderSplitConfig.DefaultCompletedPublicFolderMoveRequestAgeLimit);
			this.MoveInProgressRetryCount = base.ReadInt("MoveInProgressRetryCount", PublicFolderSplitConfig.DefaultMoveInProgressRetryCount);
			this.Validate();
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00059B50 File Offset: 0x00057D50
		private void Validate()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder("Invalid PublicFolderSplitConfig parameters. ");
			if (this.SplitThreshold < 0UL || this.SplitThreshold > 100UL)
			{
				flag = false;
				stringBuilder.AppendFormat("SplitThreshold {0}. ", this.SplitThreshold.ToString());
			}
			if (this.MaxTargetOccupiedThreshold < 0UL || this.MaxTargetOccupiedThreshold > 100UL)
			{
				flag = false;
				stringBuilder.AppendFormat("MaxTargetOccupiedThreshold {0}. ", this.MaxTargetOccupiedThreshold.ToString());
			}
			if (this.SleepTimeBeforeSplitProcessingStarts < TimeSpan.Zero)
			{
				flag = false;
				stringBuilder.AppendFormat("SleepTimeBeforeSplitProcessingStarts {0}. ", this.SleepTimeBeforeSplitProcessingStarts.ToString());
			}
			if (this.TimeoutForSynchronousOperation < TimeSpan.Zero)
			{
				flag = false;
				stringBuilder.AppendFormat("TimeoutForSynchronousOperation {0}. ", this.TimeoutForSynchronousOperation.ToString());
			}
			if (this.MaxJobAgeForDiagnostics < TimeSpan.Zero)
			{
				flag = false;
				stringBuilder.AppendFormat("MaxJobAgeForDiagnostics {0}. ", this.MaxJobAgeForDiagnostics.ToString());
			}
			if (this.QuerySyncStatusInterval < TimeSpan.Zero)
			{
				flag = false;
				stringBuilder.AppendFormat("QuerySyncStatusInterval {0}. ", this.QuerySyncStatusInterval.ToString());
			}
			if (this.LongRunningSyncChangeCount < 0)
			{
				flag = false;
				stringBuilder.AppendFormat("LongRunningSyncChangeCount {0}. ", this.LongRunningSyncChangeCount.ToString());
			}
			if (this.CompletedPublicFolderMoveRequestAgeLimit < 0)
			{
				flag = false;
				stringBuilder.AppendFormat("CompletedPublicFolderMoveRequestAgeLimit {0}. ", this.CompletedPublicFolderMoveRequestAgeLimit.ToString());
			}
			if (this.MoveInProgressRetryCount < 0)
			{
				flag = false;
				stringBuilder.AppendFormat("MoveInProgressRetryCount {0}. ", this.MoveInProgressRetryCount.ToString());
			}
			if (!flag)
			{
				throw new InvalidOperationException(stringBuilder.ToString());
			}
		}

		// Token: 0x04000987 RID: 2439
		internal const int DefaultSplitThreshhold = 80;

		// Token: 0x04000988 RID: 2440
		internal const bool DefaultSplitProcessingEnabled = true;

		// Token: 0x04000989 RID: 2441
		private static readonly TimeSpan DefaultSleepTimeBeforeSplitProcessingStarts = TimeSpan.Zero;

		// Token: 0x0400098A RID: 2442
		internal static readonly TimeSpan DefaultTimeoutForSynchronousOperation = TimeSpan.FromMinutes(2.0);

		// Token: 0x0400098B RID: 2443
		internal static readonly TimeSpan DefaultMaxJobAgeForDiagnostics = TimeSpan.FromDays(90.0);

		// Token: 0x0400098C RID: 2444
		internal static readonly int DefaultMaxTargetOccupiedThreshold = 10;

		// Token: 0x0400098D RID: 2445
		internal static readonly TimeSpan DefaultQuerySyncStatusInterval = TimeSpan.FromSeconds(15.0);

		// Token: 0x0400098E RID: 2446
		internal static readonly int DefaultLongRunningSyncChangeCount = 500;

		// Token: 0x0400098F RID: 2447
		internal static readonly int DefaultCompletedPublicFolderMoveRequestAgeLimit = 3;

		// Token: 0x04000990 RID: 2448
		internal static readonly int DefaultMoveInProgressRetryCount = 2;

		// Token: 0x04000991 RID: 2449
		private static PublicFolderSplitConfig splitConfigInstance = new PublicFolderSplitConfig();
	}
}
