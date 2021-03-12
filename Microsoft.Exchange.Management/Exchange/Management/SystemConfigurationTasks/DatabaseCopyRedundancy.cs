using System;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A3 RID: 2211
	[Serializable]
	public sealed class DatabaseCopyRedundancy
	{
		// Token: 0x06004DDF RID: 19935 RVA: 0x00143F04 File Offset: 0x00142104
		internal DatabaseCopyRedundancy(DbHealthInfoPersisted dbHealth, DbCopyHealthInfoPersisted dbchip)
		{
			this.DatabaseGuid = dbHealth.DbGuid;
			this.DatabaseName = dbHealth.DbName;
			this.ServerName = MachineName.GetNodeNameFromFqdn(dbchip.ServerFqdn).ToUpperInvariant();
			this.m_copyName = string.Format("{0}\\{1}", this.DatabaseName, this.ServerName);
			this.Identity = this.m_copyName;
			this.LastCopyStatusTransitionTime = DateTimeHelper.ToNullableLocalDateTime(dbchip.LastCopyStatusTransitionTime);
			this.CopyFoundInAD = TransitionInfo.ConstructFromRemoteSerializable(dbchip.CopyFoundInAD);
			this.CopyStatusRetrieved = TransitionInfo.ConstructFromRemoteSerializable(dbchip.CopyStatusRetrieved);
			this.CopyIsAvailable = TransitionInfo.ConstructFromRemoteSerializable(dbchip.CopyIsAvailable);
			this.CopyIsRedundant = TransitionInfo.ConstructFromRemoteSerializable(dbchip.CopyIsRedundant);
			this.CopyStatusHealthy = TransitionInfo.ConstructFromRemoteSerializable(dbchip.CopyStatusHealthy);
			this.CopyStatusActive = TransitionInfo.ConstructFromRemoteSerializable(dbchip.CopyStatusActive);
			this.CopyStatusMounted = TransitionInfo.ConstructFromRemoteSerializable(dbchip.CopyStatusMounted);
			this.IsCopyFoundInAD = this.CopyFoundInAD.IsSuccess;
			this.IsCopyAvailable = this.CopyIsAvailable.IsSuccess;
			this.IsCopyRedundant = this.CopyIsRedundant.IsSuccess;
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06004DE0 RID: 19936 RVA: 0x00144028 File Offset: 0x00142228
		// (set) Token: 0x06004DE1 RID: 19937 RVA: 0x00144030 File Offset: 0x00142230
		public string Identity { get; private set; }

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06004DE2 RID: 19938 RVA: 0x00144039 File Offset: 0x00142239
		// (set) Token: 0x06004DE3 RID: 19939 RVA: 0x00144041 File Offset: 0x00142241
		public string ServerName { get; private set; }

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x0014404A File Offset: 0x0014224A
		// (set) Token: 0x06004DE5 RID: 19941 RVA: 0x00144052 File Offset: 0x00142252
		public string DatabaseName { get; private set; }

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06004DE6 RID: 19942 RVA: 0x0014405B File Offset: 0x0014225B
		// (set) Token: 0x06004DE7 RID: 19943 RVA: 0x00144063 File Offset: 0x00142263
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x0014406C File Offset: 0x0014226C
		// (set) Token: 0x06004DE9 RID: 19945 RVA: 0x00144074 File Offset: 0x00142274
		public bool IsCopyFoundInAD { get; private set; }

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06004DEA RID: 19946 RVA: 0x0014407D File Offset: 0x0014227D
		// (set) Token: 0x06004DEB RID: 19947 RVA: 0x00144085 File Offset: 0x00142285
		public bool IsCopyRedundant { get; private set; }

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06004DEC RID: 19948 RVA: 0x0014408E File Offset: 0x0014228E
		// (set) Token: 0x06004DED RID: 19949 RVA: 0x00144096 File Offset: 0x00142296
		public bool IsCopyAvailable { get; private set; }

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06004DEE RID: 19950 RVA: 0x0014409F File Offset: 0x0014229F
		// (set) Token: 0x06004DEF RID: 19951 RVA: 0x001440A7 File Offset: 0x001422A7
		public DateTime? LastCopyStatusTransitionTime { get; set; }

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x06004DF0 RID: 19952 RVA: 0x001440B0 File Offset: 0x001422B0
		// (set) Token: 0x06004DF1 RID: 19953 RVA: 0x001440B8 File Offset: 0x001422B8
		public TransitionInfo CopyFoundInAD { get; set; }

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x06004DF2 RID: 19954 RVA: 0x001440C1 File Offset: 0x001422C1
		// (set) Token: 0x06004DF3 RID: 19955 RVA: 0x001440C9 File Offset: 0x001422C9
		public TransitionInfo CopyStatusRetrieved { get; set; }

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x001440D2 File Offset: 0x001422D2
		// (set) Token: 0x06004DF5 RID: 19957 RVA: 0x001440DA File Offset: 0x001422DA
		public TransitionInfo CopyIsAvailable { get; set; }

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x001440E3 File Offset: 0x001422E3
		// (set) Token: 0x06004DF7 RID: 19959 RVA: 0x001440EB File Offset: 0x001422EB
		public TransitionInfo CopyIsRedundant { get; set; }

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06004DF8 RID: 19960 RVA: 0x001440F4 File Offset: 0x001422F4
		// (set) Token: 0x06004DF9 RID: 19961 RVA: 0x001440FC File Offset: 0x001422FC
		public TransitionInfo CopyStatusHealthy { get; set; }

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x00144105 File Offset: 0x00142305
		// (set) Token: 0x06004DFB RID: 19963 RVA: 0x0014410D File Offset: 0x0014230D
		public TransitionInfo CopyStatusActive { get; set; }

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x00144116 File Offset: 0x00142316
		// (set) Token: 0x06004DFD RID: 19965 RVA: 0x0014411E File Offset: 0x0014231E
		public TransitionInfo CopyStatusMounted { get; set; }

		// Token: 0x06004DFE RID: 19966 RVA: 0x00144127 File Offset: 0x00142327
		public override string ToString()
		{
			return this.m_copyName;
		}

		// Token: 0x04002E66 RID: 11878
		private readonly string m_copyName;
	}
}
