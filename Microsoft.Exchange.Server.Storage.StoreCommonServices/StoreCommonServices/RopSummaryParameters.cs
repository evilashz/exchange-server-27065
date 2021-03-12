using System;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200011C RID: 284
	public struct RopSummaryParameters : ITraceParameters
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x000389C0 File Offset: 0x00036BC0
		public RopSummaryParameters(TimeSpan elapsedTime, uint errorCode, bool isNewActivity, JET_THREADSTATS threadStats, uint directoryOperations, uint offPageHits, TimeSpan cpuTimeKernel, TimeSpan cpuTimeUser, uint operationChunks, TimeSpan maximumChunkTime, TimeSpan lockWaitTime, TimeSpan directoryWaitTime, TimeSpan databaseTime, TimeSpan fastWaitTime)
		{
			this = new RopSummaryParameters(elapsedTime, errorCode, isNewActivity, threadStats, directoryOperations, offPageHits, cpuTimeKernel, cpuTimeUser, operationChunks, maximumChunkTime, lockWaitTime, directoryWaitTime, databaseTime, fastWaitTime, 0);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000389F0 File Offset: 0x00036BF0
		public RopSummaryParameters(TimeSpan elapsedTime, uint errorCode, bool isNewActivity, JET_THREADSTATS threadStats, uint directoryOperations, uint offPageHits, TimeSpan cpuTimeKernel, TimeSpan cpuTimeUser, uint operationChunks, TimeSpan maximumChunkTime, TimeSpan lockWaitTime, TimeSpan directoryWaitTime, TimeSpan databaseTime, TimeSpan fastWaitTime, int undefinedAlpha)
		{
			this = new RopSummaryParameters(elapsedTime, errorCode, isNewActivity, threadStats, directoryOperations, offPageHits, cpuTimeKernel, cpuTimeUser, operationChunks, maximumChunkTime, lockWaitTime, directoryWaitTime, databaseTime, fastWaitTime, undefinedAlpha, 0);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00038A20 File Offset: 0x00036C20
		public RopSummaryParameters(TimeSpan elapsedTime, uint errorCode, bool isNewActivity, JET_THREADSTATS threadStats, uint directoryOperations, uint offPageHits, TimeSpan cpuTimeKernel, TimeSpan cpuTimeUser, uint operationChunks, TimeSpan maximumChunkTime, TimeSpan lockWaitTime, TimeSpan directoryWaitTime, TimeSpan databaseTime, TimeSpan fastWaitTime, int undefinedAlpha, int undefinedBeta)
		{
			this = new RopSummaryParameters(elapsedTime, errorCode, isNewActivity, threadStats, directoryOperations, offPageHits, cpuTimeKernel, cpuTimeUser, operationChunks, maximumChunkTime, lockWaitTime, directoryWaitTime, databaseTime, fastWaitTime, undefinedAlpha, undefinedBeta, 0);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00038A54 File Offset: 0x00036C54
		public RopSummaryParameters(TimeSpan elapsedTime, uint errorCode, bool isNewActivity, JET_THREADSTATS threadStats, uint directoryOperations, uint offPageHits, TimeSpan cpuTimeKernel, TimeSpan cpuTimeUser, uint operationChunks, TimeSpan maximumChunkTime, TimeSpan lockWaitTime, TimeSpan directoryWaitTime, TimeSpan databaseTime, TimeSpan fastWaitTime, int undefinedAlpha, int undefinedBeta, int undefinedGamma)
		{
			this = new RopSummaryParameters(elapsedTime, errorCode, isNewActivity, threadStats, directoryOperations, offPageHits, cpuTimeKernel, cpuTimeUser, operationChunks, maximumChunkTime, lockWaitTime, directoryWaitTime, databaseTime, fastWaitTime, undefinedAlpha, undefinedBeta, undefinedGamma, 0);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00038A88 File Offset: 0x00036C88
		public RopSummaryParameters(TimeSpan elapsedTime, uint errorCode, bool isNewActivity, JET_THREADSTATS threadStats, uint directoryOperations, uint offPageHits, TimeSpan cpuTimeKernel, TimeSpan cpuTimeUser, uint operationChunks, TimeSpan maximumChunkTime, TimeSpan lockWaitTime, TimeSpan directoryWaitTime, TimeSpan databaseTime, TimeSpan fastWaitTime, int undefinedAlpha, int undefinedBeta, int undefinedGamma, int undefinedDelta)
		{
			this = new RopSummaryParameters(elapsedTime, errorCode, isNewActivity, threadStats, directoryOperations, offPageHits, cpuTimeKernel, cpuTimeUser, operationChunks, maximumChunkTime, lockWaitTime, directoryWaitTime, databaseTime, fastWaitTime, undefinedAlpha, undefinedBeta, undefinedGamma, undefinedDelta, 0);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00038AC0 File Offset: 0x00036CC0
		public RopSummaryParameters(TimeSpan elapsedTime, uint errorCode, bool isNewActivity, JET_THREADSTATS threadStats, uint directoryOperations, uint offPageHits, TimeSpan cpuTimeKernel, TimeSpan cpuTimeUser, uint operationChunks, TimeSpan maximumChunkTime, TimeSpan lockWaitTime, TimeSpan directoryWaitTime, TimeSpan databaseTime, TimeSpan fastWaitTime, int undefinedAlpha, int undefinedBeta, int undefinedGamma, int undefinedDelta, int undefinedOmega)
		{
			this.elapsedTime = elapsedTime;
			this.errorCode = errorCode;
			this.isNewActivity = isNewActivity;
			this.cbLogRecord = ((threadStats.cbLogRecord >= 0) ? threadStats.cbLogRecord : int.MaxValue);
			this.cPageDirtied = threadStats.cPageDirtied;
			this.cPagePreread = threadStats.cPagePreread;
			this.cPageRead = threadStats.cPageRead;
			this.cPageRedirtied = threadStats.cPageRedirtied;
			this.jetReservedAlpha = 0;
			this.jetReservedBeta = 0;
			this.directoryOperations = directoryOperations;
			this.offPageHits = offPageHits;
			this.cpuTimeKernel = cpuTimeKernel;
			this.cpuTimeUser = cpuTimeUser;
			this.operationChunks = operationChunks;
			this.maximumChunkTime = maximumChunkTime;
			this.lockWaitTime = lockWaitTime;
			this.directoryWaitTime = directoryWaitTime;
			this.databaseTime = databaseTime;
			this.fastWaitTime = fastWaitTime;
			this.undefinedAlpha = undefinedAlpha;
			this.undefinedBeta = undefinedBeta;
			this.undefinedGamma = undefinedGamma;
			this.undefinedDelta = undefinedDelta;
			this.undefinedOmega = undefinedOmega;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00038BBA File Offset: 0x00036DBA
		public bool HasDataToLog
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00038BBD File Offset: 0x00036DBD
		public TimeSpan ElapsedTime
		{
			get
			{
				return this.elapsedTime;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00038BC5 File Offset: 0x00036DC5
		public uint ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00038BCD File Offset: 0x00036DCD
		public bool IsNewActivity
		{
			get
			{
				return this.isNewActivity;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00038BD5 File Offset: 0x00036DD5
		public int LogBytes
		{
			get
			{
				return this.cbLogRecord;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00038BDD File Offset: 0x00036DDD
		public int PagesPreread
		{
			get
			{
				return this.cPagePreread;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00038BE5 File Offset: 0x00036DE5
		public int PagesRead
		{
			get
			{
				return this.cPageRead;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00038BED File Offset: 0x00036DED
		public int PagesDirtied
		{
			get
			{
				return this.cPageDirtied;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00038BF5 File Offset: 0x00036DF5
		public int JetReservedAlpha
		{
			get
			{
				return this.jetReservedAlpha;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00038BFD File Offset: 0x00036DFD
		public int JetReservedBeta
		{
			get
			{
				return this.jetReservedBeta;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00038C05 File Offset: 0x00036E05
		public int PagesRedirtied
		{
			get
			{
				return this.cPageRedirtied;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00038C0D File Offset: 0x00036E0D
		public uint DirectoryOperations
		{
			get
			{
				return this.directoryOperations;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00038C15 File Offset: 0x00036E15
		public uint OffPageHits
		{
			get
			{
				return this.offPageHits;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00038C1D File Offset: 0x00036E1D
		public TimeSpan CpuTimeKernel
		{
			get
			{
				return this.cpuTimeKernel;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00038C25 File Offset: 0x00036E25
		public TimeSpan CpuTimeUser
		{
			get
			{
				return this.cpuTimeUser;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00038C2D File Offset: 0x00036E2D
		public uint OperationChunks
		{
			get
			{
				return this.operationChunks;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00038C35 File Offset: 0x00036E35
		public TimeSpan MaximumChunkTime
		{
			get
			{
				return this.maximumChunkTime;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00038C3D File Offset: 0x00036E3D
		public TimeSpan LockWaitTime
		{
			get
			{
				return this.lockWaitTime;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00038C45 File Offset: 0x00036E45
		public TimeSpan DirectoryWaitTime
		{
			get
			{
				return this.directoryWaitTime;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00038C4D File Offset: 0x00036E4D
		public TimeSpan DatabaseTime
		{
			get
			{
				return this.databaseTime;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00038C55 File Offset: 0x00036E55
		public TimeSpan FastWaitTime
		{
			get
			{
				return this.fastWaitTime;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00038C5D File Offset: 0x00036E5D
		public int UndefinedAlpha
		{
			get
			{
				return this.undefinedAlpha;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00038C65 File Offset: 0x00036E65
		public int UndefinedBeta
		{
			get
			{
				return this.undefinedBeta;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00038C6D File Offset: 0x00036E6D
		public int UndefinedGamma
		{
			get
			{
				return this.undefinedGamma;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00038C75 File Offset: 0x00036E75
		public int UndefinedDelta
		{
			get
			{
				return this.undefinedDelta;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00038C7D File Offset: 0x00036E7D
		public int UndefinedOmega
		{
			get
			{
				return this.undefinedOmega;
			}
		}

		// Token: 0x04000609 RID: 1545
		public const int TraceSchemaVersion = 100;

		// Token: 0x0400060A RID: 1546
		private readonly TimeSpan elapsedTime;

		// Token: 0x0400060B RID: 1547
		private readonly uint errorCode;

		// Token: 0x0400060C RID: 1548
		private readonly bool isNewActivity;

		// Token: 0x0400060D RID: 1549
		private readonly int cbLogRecord;

		// Token: 0x0400060E RID: 1550
		private readonly int cPagePreread;

		// Token: 0x0400060F RID: 1551
		private readonly int cPageRead;

		// Token: 0x04000610 RID: 1552
		private readonly int cPageDirtied;

		// Token: 0x04000611 RID: 1553
		private readonly int cPageRedirtied;

		// Token: 0x04000612 RID: 1554
		private readonly int jetReservedAlpha;

		// Token: 0x04000613 RID: 1555
		private readonly int jetReservedBeta;

		// Token: 0x04000614 RID: 1556
		private readonly uint directoryOperations;

		// Token: 0x04000615 RID: 1557
		private readonly uint offPageHits;

		// Token: 0x04000616 RID: 1558
		private readonly TimeSpan cpuTimeKernel;

		// Token: 0x04000617 RID: 1559
		private readonly TimeSpan cpuTimeUser;

		// Token: 0x04000618 RID: 1560
		private readonly uint operationChunks;

		// Token: 0x04000619 RID: 1561
		private readonly TimeSpan maximumChunkTime;

		// Token: 0x0400061A RID: 1562
		private readonly TimeSpan lockWaitTime;

		// Token: 0x0400061B RID: 1563
		private readonly TimeSpan directoryWaitTime;

		// Token: 0x0400061C RID: 1564
		private readonly TimeSpan databaseTime;

		// Token: 0x0400061D RID: 1565
		private readonly TimeSpan fastWaitTime;

		// Token: 0x0400061E RID: 1566
		private readonly int undefinedAlpha;

		// Token: 0x0400061F RID: 1567
		private readonly int undefinedBeta;

		// Token: 0x04000620 RID: 1568
		private readonly int undefinedGamma;

		// Token: 0x04000621 RID: 1569
		private readonly int undefinedDelta;

		// Token: 0x04000622 RID: 1570
		private readonly int undefinedOmega;
	}
}
