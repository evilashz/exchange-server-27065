using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002C1 RID: 705
	internal class MountDirectPerformanceTracker : FailoverPerformanceTrackerBase<MountDatabaseDirectOperation>
	{
		// Token: 0x06001B45 RID: 6981 RVA: 0x0007587E File Offset: 0x00073A7E
		public MountDirectPerformanceTracker(Guid dbGuid) : base("MountDirectPerf")
		{
			this.m_dbGuid = dbGuid;
			this.m_dbName = this.LookupDatabaseName();
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0007589E File Offset: 0x00073A9E
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x000758A6 File Offset: 0x00073AA6
		public long LastAcllLossAmount { private get; set; }

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x000758AF File Offset: 0x00073AAF
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x000758B7 File Offset: 0x00073AB7
		public bool LastAcllRunWithSkipHealthChecks { private get; set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x000758C0 File Offset: 0x00073AC0
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x000758C8 File Offset: 0x00073AC8
		public long HighestLogGenBefore { private get; set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000758D1 File Offset: 0x00073AD1
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x000758D9 File Offset: 0x00073AD9
		public long HighestLogGenAfter { private get; set; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000758E2 File Offset: 0x00073AE2
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x000758EA File Offset: 0x00073AEA
		public bool IsLossyMountEnabled { private get; set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000758F3 File Offset: 0x00073AF3
		// (set) Token: 0x06001B51 RID: 6993 RVA: 0x000758FB File Offset: 0x00073AFB
		public bool IsDismountInProgress { private get; set; }

		// Token: 0x06001B52 RID: 6994 RVA: 0x00075904 File Offset: 0x00073B04
		public override void LogEvent()
		{
			ReplayCrimsonEvents.MountDirectPerformance.Log<string, Guid, long, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, TimeSpan, bool, long, long, bool, bool>(this.m_dbName, this.m_dbGuid, this.LastAcllLossAmount, base.GetDuration(MountDatabaseDirectOperation.AmPreMountCallback), base.GetDuration(MountDatabaseDirectOperation.RegistryReplicatorCopy), base.GetDuration(MountDatabaseDirectOperation.StoreMount), base.GetDuration(MountDatabaseDirectOperation.PreMountQueuedOpStart), base.GetDuration(MountDatabaseDirectOperation.PreMountQueuedOpExecution), base.GetDuration(MountDatabaseDirectOperation.PreventMountIfNecessary), base.GetDuration(MountDatabaseDirectOperation.ResumeActiveCopy), base.GetDuration(MountDatabaseDirectOperation.UpdateLastLogGenOnMount), base.GetDuration(MountDatabaseDirectOperation.GetRunningReplicaInstance), base.GetDuration(MountDatabaseDirectOperation.ConfirmLogReset), base.GetDuration(MountDatabaseDirectOperation.LowestGenerationInDirectory), base.GetDuration(MountDatabaseDirectOperation.HighestGenerationInDirectory), base.GetDuration(MountDatabaseDirectOperation.GenerationAvailableInDirectory), base.GetDuration(MountDatabaseDirectOperation.UpdateLastLogGeneratedInClusDB), this.IsLossyMountEnabled, this.HighestLogGenBefore, this.HighestLogGenAfter, this.LastAcllRunWithSkipHealthChecks, this.IsDismountInProgress);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000759B4 File Offset: 0x00073BB4
		private string LookupDatabaseName()
		{
			string text = null;
			IADDatabase iaddatabase = Dependencies.ReplayAdObjectLookup.DatabaseLookup.FindAdObjectByGuid(this.m_dbGuid);
			if (iaddatabase != null)
			{
				text = iaddatabase.Name;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = this.m_dbGuid.ToString();
			}
			return text;
		}

		// Token: 0x04000B34 RID: 2868
		private Guid m_dbGuid;

		// Token: 0x04000B35 RID: 2869
		private string m_dbName;
	}
}
