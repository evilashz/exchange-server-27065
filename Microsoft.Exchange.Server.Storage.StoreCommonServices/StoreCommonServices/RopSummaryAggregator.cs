using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200011F RID: 287
	public sealed class RopSummaryAggregator : TraceDataAggregator<RopSummaryParameters>
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x00038CD1 File Offset: 0x00036ED1
		public RopSummaryAggregator()
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00038CDC File Offset: 0x00036EDC
		private RopSummaryAggregator(uint totalCalls, uint numberOfCallsSlow, TimeSpan maximumElapsedTime, uint numberOfCallsInError, uint lastKnownError, TimeSpan totalTime, uint numberOfActivities, int totalLogBytes, int totalPagesPreread, int totalPagesRead, int totalPagesDirtied, int totalPagesRedirtied, int totalJetReservedAlpha, int totalJetReservedBeta, uint totalDirectoryOperations, uint totalOffPageHits, TimeSpan totalCpuTimeKernel, TimeSpan totalCpuTimeUser, uint totalChunks, TimeSpan maximumChunkTime, TimeSpan totalLockWaitTime, TimeSpan totalDirectoryWaitTime, TimeSpan totalDatabaseTime, TimeSpan totalFastWaitTime, int totalUndefinedAlpha, int totalUndefinedBeta, int totalUndefinedGamma, int totalUndefinedDelta, int totalUndefinedOmega)
		{
			this.totalCalls = totalCalls;
			this.numberOfCallsSlow = numberOfCallsSlow;
			this.maximumElapsedTime = maximumElapsedTime;
			this.numberOfCallsInError = numberOfCallsInError;
			this.lastKnownError = lastKnownError;
			this.totalTime = totalTime;
			this.numberOfActivities = numberOfActivities;
			this.totalLogBytes = (uint)totalLogBytes;
			this.totalPagesPreread = (uint)totalPagesPreread;
			this.totalPagesRead = (uint)totalPagesRead;
			this.totalPagesDirtied = (uint)totalPagesDirtied;
			this.totalPagesRedirtied = (uint)totalPagesRedirtied;
			this.totalJetReservedAlpha = (uint)totalJetReservedAlpha;
			this.totalJetReservedBeta = (uint)totalJetReservedBeta;
			this.totalDirectoryOperations = totalDirectoryOperations;
			this.totalOffPageHits = totalOffPageHits;
			this.totalCpuTimeKernel = totalCpuTimeKernel;
			this.totalCpuTimeUser = totalCpuTimeUser;
			this.totalChunks = totalChunks;
			this.maximumChunkTime = maximumChunkTime;
			this.totalLockWaitTime = totalLockWaitTime;
			this.totalDirectoryWaitTime = totalDirectoryWaitTime;
			this.totalDatabaseTime = totalDatabaseTime;
			this.totalFastWaitTime = totalFastWaitTime;
			this.totalUndefinedAlpha = totalUndefinedAlpha;
			this.totalUndefinedBeta = totalUndefinedBeta;
			this.totalUndefinedGamma = totalUndefinedGamma;
			this.totalUndefinedDelta = totalUndefinedDelta;
			this.totalUndefinedOmega = totalUndefinedOmega;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00038DD4 File Offset: 0x00036FD4
		public RopSummaryAggregator(RopSummaryParameters parameters) : this(1U, (parameters.ElapsedTime > DefaultSettings.Get.RopSummarySlowThreshold) ? 1U : 0U, parameters.ElapsedTime, (parameters.ErrorCode == 0U) ? 0U : 1U, parameters.ErrorCode, parameters.ElapsedTime, 1U, parameters.LogBytes, parameters.PagesPreread, parameters.PagesRead, parameters.PagesDirtied, parameters.PagesRedirtied, parameters.JetReservedAlpha, parameters.JetReservedBeta, parameters.DirectoryOperations, parameters.OffPageHits, parameters.CpuTimeKernel, parameters.CpuTimeUser, parameters.OperationChunks, parameters.MaximumChunkTime, parameters.LockWaitTime, parameters.DirectoryWaitTime, parameters.DatabaseTime, parameters.FastWaitTime, parameters.UndefinedAlpha, parameters.UndefinedBeta, parameters.UndefinedGamma, parameters.UndefinedDelta, parameters.UndefinedOmega)
		{
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00038EC1 File Offset: 0x000370C1
		internal uint TotalCalls
		{
			get
			{
				return this.totalCalls;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00038EC9 File Offset: 0x000370C9
		internal uint NumberOfCallsSlow
		{
			get
			{
				return this.numberOfCallsSlow;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00038ED1 File Offset: 0x000370D1
		internal TimeSpan MaximumElapsedTime
		{
			get
			{
				return this.maximumElapsedTime;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00038ED9 File Offset: 0x000370D9
		internal uint NumberOfCallsInError
		{
			get
			{
				return this.numberOfCallsInError;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00038EE1 File Offset: 0x000370E1
		internal uint LastKnownError
		{
			get
			{
				return this.lastKnownError;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00038EE9 File Offset: 0x000370E9
		internal TimeSpan TotalTime
		{
			get
			{
				return this.totalTime;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00038EF1 File Offset: 0x000370F1
		internal uint NumberOfActivities
		{
			get
			{
				return this.numberOfActivities;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00038EF9 File Offset: 0x000370F9
		internal uint TotalLogBytes
		{
			get
			{
				return this.totalLogBytes;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00038F01 File Offset: 0x00037101
		internal uint TotalPagesPreread
		{
			get
			{
				return this.totalPagesPreread;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00038F09 File Offset: 0x00037109
		internal uint TotalPagesRead
		{
			get
			{
				return this.totalPagesRead;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00038F11 File Offset: 0x00037111
		internal uint TotalPagesDirtied
		{
			get
			{
				return this.totalPagesDirtied;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00038F19 File Offset: 0x00037119
		internal uint TotalPagesRedirtied
		{
			get
			{
				return this.totalPagesRedirtied;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00038F21 File Offset: 0x00037121
		internal uint TotalJetReservedAlpha
		{
			get
			{
				return this.totalJetReservedAlpha;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00038F29 File Offset: 0x00037129
		internal uint TotalJetReservedBeta
		{
			get
			{
				return this.totalJetReservedBeta;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00038F31 File Offset: 0x00037131
		internal uint TotalDirectoryOperations
		{
			get
			{
				return this.totalDirectoryOperations;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00038F39 File Offset: 0x00037139
		internal uint TotalOffPageHits
		{
			get
			{
				return this.totalOffPageHits;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00038F41 File Offset: 0x00037141
		internal TimeSpan TotalCpuTimeKernel
		{
			get
			{
				return this.totalCpuTimeKernel;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00038F49 File Offset: 0x00037149
		internal TimeSpan TotalCpuTimeUser
		{
			get
			{
				return this.totalCpuTimeUser;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00038F51 File Offset: 0x00037151
		internal uint TotalChunks
		{
			get
			{
				return this.totalChunks;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00038F59 File Offset: 0x00037159
		internal TimeSpan MaximumChunkTime
		{
			get
			{
				return this.maximumChunkTime;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00038F61 File Offset: 0x00037161
		internal TimeSpan TotalLockWaitTime
		{
			get
			{
				return this.totalLockWaitTime;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00038F69 File Offset: 0x00037169
		internal TimeSpan TotalDirectoryWaitTime
		{
			get
			{
				return this.totalDirectoryWaitTime;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00038F71 File Offset: 0x00037171
		internal TimeSpan TotalDatabaseTime
		{
			get
			{
				return this.totalDatabaseTime;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00038F79 File Offset: 0x00037179
		internal TimeSpan TotalFastWaitTime
		{
			get
			{
				return this.totalFastWaitTime;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00038F81 File Offset: 0x00037181
		internal int TotalUndefinedAlpha
		{
			get
			{
				return this.totalUndefinedAlpha;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00038F89 File Offset: 0x00037189
		internal int TotalUndefinedBeta
		{
			get
			{
				return this.totalUndefinedBeta;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00038F91 File Offset: 0x00037191
		internal int TotalUndefinedGamma
		{
			get
			{
				return this.totalUndefinedGamma;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00038F99 File Offset: 0x00037199
		internal int TotalUndefinedDelta
		{
			get
			{
				return this.totalUndefinedDelta;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00038FA1 File Offset: 0x000371A1
		internal int TotalUndefinedOmega
		{
			get
			{
				return this.totalUndefinedOmega;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00038FAC File Offset: 0x000371AC
		internal override void Add(RopSummaryParameters parameters)
		{
			this.totalCalls += 1U;
			this.numberOfCallsSlow += ((parameters.ElapsedTime > DefaultSettings.Get.RopSummarySlowThreshold) ? 1U : 0U);
			this.maximumElapsedTime = TimeSpan.FromTicks(Math.Max(this.maximumElapsedTime.Ticks, parameters.ElapsedTime.Ticks));
			this.numberOfCallsInError += ((parameters.ErrorCode != 0U) ? 1U : 0U);
			this.lastKnownError = ((parameters.ErrorCode != 0U) ? parameters.ErrorCode : this.lastKnownError);
			this.totalTime += parameters.ElapsedTime;
			this.numberOfActivities += (parameters.IsNewActivity ? 1U : 0U);
			this.totalLogBytes += (uint)parameters.LogBytes;
			this.totalPagesPreread += (uint)parameters.PagesPreread;
			this.totalPagesRead += (uint)parameters.PagesRead;
			this.totalPagesDirtied += (uint)parameters.PagesDirtied;
			this.totalPagesRedirtied += (uint)parameters.PagesRedirtied;
			this.totalJetReservedAlpha += (uint)parameters.JetReservedAlpha;
			this.totalJetReservedBeta += (uint)parameters.JetReservedBeta;
			this.totalDirectoryOperations += parameters.DirectoryOperations;
			this.totalOffPageHits += parameters.OffPageHits;
			this.totalCpuTimeKernel += parameters.CpuTimeKernel;
			this.totalCpuTimeUser += parameters.CpuTimeUser;
			this.totalChunks += parameters.OperationChunks;
			this.maximumChunkTime = TimeSpan.FromTicks(Math.Max(this.maximumChunkTime.Ticks, parameters.MaximumChunkTime.Ticks));
			this.totalLockWaitTime += parameters.LockWaitTime;
			this.totalDirectoryWaitTime += parameters.DirectoryWaitTime;
			this.totalDatabaseTime += parameters.DatabaseTime;
			this.totalFastWaitTime += parameters.FastWaitTime;
			this.totalUndefinedAlpha += parameters.UndefinedAlpha;
			this.totalUndefinedBeta += parameters.UndefinedBeta;
			this.totalUndefinedGamma += parameters.UndefinedGamma;
			this.totalUndefinedDelta += parameters.UndefinedDelta;
			this.totalUndefinedOmega += parameters.UndefinedOmega;
		}

		// Token: 0x04000624 RID: 1572
		private uint totalCalls;

		// Token: 0x04000625 RID: 1573
		private uint numberOfCallsSlow;

		// Token: 0x04000626 RID: 1574
		private TimeSpan maximumElapsedTime;

		// Token: 0x04000627 RID: 1575
		private uint numberOfCallsInError;

		// Token: 0x04000628 RID: 1576
		private uint lastKnownError;

		// Token: 0x04000629 RID: 1577
		private TimeSpan totalTime;

		// Token: 0x0400062A RID: 1578
		private uint numberOfActivities;

		// Token: 0x0400062B RID: 1579
		private uint totalLogBytes;

		// Token: 0x0400062C RID: 1580
		private uint totalPagesPreread;

		// Token: 0x0400062D RID: 1581
		private uint totalPagesRead;

		// Token: 0x0400062E RID: 1582
		private uint totalPagesDirtied;

		// Token: 0x0400062F RID: 1583
		private uint totalPagesRedirtied;

		// Token: 0x04000630 RID: 1584
		private uint totalJetReservedAlpha;

		// Token: 0x04000631 RID: 1585
		private uint totalJetReservedBeta;

		// Token: 0x04000632 RID: 1586
		private uint totalDirectoryOperations;

		// Token: 0x04000633 RID: 1587
		private uint totalOffPageHits;

		// Token: 0x04000634 RID: 1588
		private TimeSpan totalCpuTimeKernel;

		// Token: 0x04000635 RID: 1589
		private TimeSpan totalCpuTimeUser;

		// Token: 0x04000636 RID: 1590
		private uint totalChunks;

		// Token: 0x04000637 RID: 1591
		private TimeSpan maximumChunkTime;

		// Token: 0x04000638 RID: 1592
		private TimeSpan totalLockWaitTime;

		// Token: 0x04000639 RID: 1593
		private TimeSpan totalDirectoryWaitTime;

		// Token: 0x0400063A RID: 1594
		private TimeSpan totalDatabaseTime;

		// Token: 0x0400063B RID: 1595
		private TimeSpan totalFastWaitTime;

		// Token: 0x0400063C RID: 1596
		private int totalUndefinedAlpha;

		// Token: 0x0400063D RID: 1597
		private int totalUndefinedBeta;

		// Token: 0x0400063E RID: 1598
		private int totalUndefinedGamma;

		// Token: 0x0400063F RID: 1599
		private int totalUndefinedDelta;

		// Token: 0x04000640 RID: 1600
		private int totalUndefinedOmega;
	}
}
