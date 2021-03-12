using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002C4 RID: 708
	internal class IncReseedPerformanceTracker : FailoverPerformanceTrackerBase<IncReseedOperation>
	{
		// Token: 0x06001B76 RID: 7030 RVA: 0x00075CEF File Offset: 0x00073EEF
		public IncReseedPerformanceTracker(IReplayConfiguration config) : base("IncReseedPerf")
		{
			this.m_config = config;
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00075D03 File Offset: 0x00073F03
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x00075D0B File Offset: 0x00073F0B
		public bool IsRunningACLL { private get; set; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00075D14 File Offset: 0x00073F14
		// (set) Token: 0x06001B7A RID: 7034 RVA: 0x00075D1C File Offset: 0x00073F1C
		public bool IsRestartedIncReseed { private get; set; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x00075D25 File Offset: 0x00073F25
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x00075D2D File Offset: 0x00073F2D
		public bool IsFailedPassivePagePatch { private get; set; }

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x00075D36 File Offset: 0x00073F36
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x00075D3E File Offset: 0x00073F3E
		public bool IsE00LogExists { private get; set; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00075D47 File Offset: 0x00073F47
		// (set) Token: 0x06001B80 RID: 7040 RVA: 0x00075D4F File Offset: 0x00073F4F
		public bool IsDivergentAfterSeed { private get; set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x00075D58 File Offset: 0x00073F58
		// (set) Token: 0x06001B82 RID: 7042 RVA: 0x00075D60 File Offset: 0x00073F60
		public bool IsPreviousLogNotBinaryEqual { private get; set; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00075D69 File Offset: 0x00073F69
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x00075D71 File Offset: 0x00073F71
		public bool IsIncReseedNeeded { private get; set; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x00075D7A File Offset: 0x00073F7A
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x00075D82 File Offset: 0x00073F82
		public bool IsIncReseedV1Performed { get; set; }

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00075D8B File Offset: 0x00073F8B
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x00075D93 File Offset: 0x00073F93
		public bool IsDatabaseConsistent { private get; set; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x00075D9C File Offset: 0x00073F9C
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x00075DA4 File Offset: 0x00073FA4
		public bool IsPagesReferencedInDivergentLogs { private get; set; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00075DAD File Offset: 0x00073FAD
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x00075DB5 File Offset: 0x00073FB5
		public long PassiveEOLGen { private get; set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00075DBE File Offset: 0x00073FBE
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00075DC6 File Offset: 0x00073FC6
		public long NumPagesToBePatched { private get; set; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x00075DCF File Offset: 0x00073FCF
		// (set) Token: 0x06001B90 RID: 7056 RVA: 0x00075DD7 File Offset: 0x00073FD7
		public long ActiveHighestLogGen { private get; set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x00075DE0 File Offset: 0x00073FE0
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x00075DE8 File Offset: 0x00073FE8
		public long HighestLogGenRequired { private get; set; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x00075DF1 File Offset: 0x00073FF1
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x00075DF9 File Offset: 0x00073FF9
		public long LowestLogGenRequired { private get; set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00075E02 File Offset: 0x00074002
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x00075E0A File Offset: 0x0007400A
		public long FirstDivergedLogGen { private get; set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x00075E13 File Offset: 0x00074013
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x00075E1B File Offset: 0x0007401B
		public string SourceServer { private get; set; }

		// Token: 0x06001B99 RID: 7065 RVA: 0x00075E24 File Offset: 0x00074024
		public override void LogEvent()
		{
			ReplayCrimsonEvents.IncrementalReseedPerformance.Log<string, Guid, bool, bool, bool, bool, bool, bool, bool, bool, bool, long, long, long, long, long, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, string, long, TimeSpan, TimeSpan, TimeSpan, TimeSpan, bool>(this.m_config.DatabaseName, this.m_config.IdentityGuid, this.IsRunningACLL, this.IsRestartedIncReseed, this.IsFailedPassivePagePatch, this.IsE00LogExists, this.IsDivergentAfterSeed, this.IsIncReseedNeeded, this.IsIncReseedV1Performed, this.IsDatabaseConsistent, this.IsPagesReferencedInDivergentLogs, this.PassiveEOLGen, this.NumPagesToBePatched, this.ActiveHighestLogGen, this.HighestLogGenRequired, this.LowestLogGenRequired, base.GetDuration(IncReseedOperation.IsIncrementalReseedRequiredOverall), base.GetDuration(IncReseedOperation.CheckForDivergenceAfterSeeding), base.GetDuration(IncReseedOperation.CheckSourceDatabaseMountedFirst), base.GetDuration(IncReseedOperation.QueryLogRangeFirst), base.GetDuration(IncReseedOperation.PerformIncrementalReseedOverall), base.GetDuration(IncReseedOperation.FindDivergencePoint), base.GetDuration(IncReseedOperation.PrepareIncReseedV2Overall), base.GetDuration(IncReseedOperation.RedirtyDatabase), base.GetDuration(IncReseedOperation.PauseTruncation), base.GetDuration(IncReseedOperation.GeneratePageListSinceDivergence), base.GetDuration(IncReseedOperation.ReadDatabasePagesFromActive), base.GetDuration(IncReseedOperation.CopyAndInspectRequiredLogFiles), base.GetDuration(IncReseedOperation.PatchDatabaseOverall), base.GetDuration(IncReseedOperation.ReplaceLogFiles), this.SourceServer, this.FirstDivergedLogGen, base.GetDuration(IncReseedOperation.ReplaceE00LogTransacted), base.GetDuration(IncReseedOperation.EnsureTargetDismounted), base.GetDuration(IncReseedOperation.IsLogfileEqual), base.GetDuration(IncReseedOperation.IsLogFileSubset), this.IsPreviousLogNotBinaryEqual);
		}

		// Token: 0x04000B4F RID: 2895
		private IReplayConfiguration m_config;
	}
}
