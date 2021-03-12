using System;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A2 RID: 2210
	[Serializable]
	public sealed class DatabaseRedundancy : IConfigurable
	{
		// Token: 0x06004DAD RID: 19885 RVA: 0x00143BD0 File Offset: 0x00141DD0
		internal DatabaseRedundancy(HealthInfoPersisted healthInfo, DbHealthInfoPersisted dbHealth, string serverContactedFqdn)
		{
			this.Identity = new ConfigObjectId(dbHealth.DbName);
			this.DbGuid = dbHealth.DbGuid;
			this.ServerContactedFqdn = serverContactedFqdn.ToUpperInvariant();
			this.HealthInfoCreateTime = DateTimeHelper.ParseIntoNullableLocalDateTimeIfPossible(healthInfo.CreateTimeUtcStr);
			this.HealthInfoLastUpdateTime = DateTimeHelper.ParseIntoNullableLocalDateTimeIfPossible(healthInfo.LastUpdateTimeUtcStr);
			this.DatabaseFoundInAD = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.DbFoundInAD);
			this.IsDatabaseFoundInAD = this.DatabaseFoundInAD.IsSuccess;
			this.SkippedFromMonitoring = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.SkippedFromMonitoring);
			this.AtLeast1RedundantCopy = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.IsAtLeast1RedundantCopy);
			this.AtLeast2RedundantCopy = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.IsAtLeast2RedundantCopy);
			this.AtLeast3RedundantCopy = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.IsAtLeast3RedundantCopy);
			this.AtLeast4RedundantCopy = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.IsAtLeast4RedundantCopy);
			this.AtLeast1AvailableCopy = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.IsAtLeast1AvailableCopy);
			this.AtLeast2AvailableCopy = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.IsAtLeast2AvailableCopy);
			this.AtLeast3AvailableCopy = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.IsAtLeast3AvailableCopy);
			this.AtLeast4AvailableCopy = TransitionInfo.ConstructFromRemoteSerializable(dbHealth.IsAtLeast4AvailableCopy);
			this.DbCopies = new DatabaseCopyRedundancy[dbHealth.DbCopies.Count];
			for (int i = 0; i < dbHealth.DbCopies.Count; i++)
			{
				this.DbCopies[i] = new DatabaseCopyRedundancy(dbHealth, dbHealth.DbCopies[i]);
			}
			this.IsAtLeast2RedundantCopy = this.AtLeast2RedundantCopy.IsSuccess;
			this.IsAtLeast2AvailableCopy = this.AtLeast2AvailableCopy.IsSuccess;
			this.AtLeast2RedundantCopyTransitionTime = this.AtLeast2RedundantCopy.LastTransitionTime;
			this.AtLeast2AvailableCopyTransitionTime = this.AtLeast2AvailableCopy.LastTransitionTime;
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06004DAE RID: 19886 RVA: 0x00143D73 File Offset: 0x00141F73
		// (set) Token: 0x06004DAF RID: 19887 RVA: 0x00143D7B File Offset: 0x00141F7B
		public Guid DbGuid { get; private set; }

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06004DB0 RID: 19888 RVA: 0x00143D84 File Offset: 0x00141F84
		// (set) Token: 0x06004DB1 RID: 19889 RVA: 0x00143D8C File Offset: 0x00141F8C
		public ObjectId Identity { get; private set; }

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x00143D95 File Offset: 0x00141F95
		// (set) Token: 0x06004DB3 RID: 19891 RVA: 0x00143D9D File Offset: 0x00141F9D
		public bool IsDatabaseFoundInAD { get; private set; }

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x00143DA6 File Offset: 0x00141FA6
		// (set) Token: 0x06004DB5 RID: 19893 RVA: 0x00143DAE File Offset: 0x00141FAE
		public bool IsAtLeast2RedundantCopy { get; private set; }

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x00143DB7 File Offset: 0x00141FB7
		// (set) Token: 0x06004DB7 RID: 19895 RVA: 0x00143DBF File Offset: 0x00141FBF
		public bool IsAtLeast2AvailableCopy { get; private set; }

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x00143DC8 File Offset: 0x00141FC8
		// (set) Token: 0x06004DB9 RID: 19897 RVA: 0x00143DD0 File Offset: 0x00141FD0
		public DateTime? AtLeast2RedundantCopyTransitionTime { get; private set; }

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06004DBA RID: 19898 RVA: 0x00143DD9 File Offset: 0x00141FD9
		// (set) Token: 0x06004DBB RID: 19899 RVA: 0x00143DE1 File Offset: 0x00141FE1
		public DateTime? AtLeast2AvailableCopyTransitionTime { get; private set; }

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x00143DEA File Offset: 0x00141FEA
		// (set) Token: 0x06004DBD RID: 19901 RVA: 0x00143DF2 File Offset: 0x00141FF2
		public string ServerContactedFqdn { get; private set; }

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x00143DFB File Offset: 0x00141FFB
		// (set) Token: 0x06004DBF RID: 19903 RVA: 0x00143E03 File Offset: 0x00142003
		public DateTime? HealthInfoCreateTime { get; private set; }

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x00143E0C File Offset: 0x0014200C
		// (set) Token: 0x06004DC1 RID: 19905 RVA: 0x00143E14 File Offset: 0x00142014
		public DateTime? HealthInfoLastUpdateTime { get; private set; }

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x00143E1D File Offset: 0x0014201D
		// (set) Token: 0x06004DC3 RID: 19907 RVA: 0x00143E25 File Offset: 0x00142025
		public TransitionInfo DatabaseFoundInAD { get; private set; }

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x00143E2E File Offset: 0x0014202E
		// (set) Token: 0x06004DC5 RID: 19909 RVA: 0x00143E36 File Offset: 0x00142036
		public TransitionInfo SkippedFromMonitoring { get; private set; }

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x00143E3F File Offset: 0x0014203F
		// (set) Token: 0x06004DC7 RID: 19911 RVA: 0x00143E47 File Offset: 0x00142047
		public TransitionInfo AtLeast1RedundantCopy { get; private set; }

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06004DC8 RID: 19912 RVA: 0x00143E50 File Offset: 0x00142050
		// (set) Token: 0x06004DC9 RID: 19913 RVA: 0x00143E58 File Offset: 0x00142058
		public TransitionInfo AtLeast2RedundantCopy { get; private set; }

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x00143E61 File Offset: 0x00142061
		// (set) Token: 0x06004DCB RID: 19915 RVA: 0x00143E69 File Offset: 0x00142069
		public TransitionInfo AtLeast3RedundantCopy { get; private set; }

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x00143E72 File Offset: 0x00142072
		// (set) Token: 0x06004DCD RID: 19917 RVA: 0x00143E7A File Offset: 0x0014207A
		public TransitionInfo AtLeast4RedundantCopy { get; private set; }

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x00143E83 File Offset: 0x00142083
		// (set) Token: 0x06004DCF RID: 19919 RVA: 0x00143E8B File Offset: 0x0014208B
		public TransitionInfo AtLeast1AvailableCopy { get; private set; }

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06004DD0 RID: 19920 RVA: 0x00143E94 File Offset: 0x00142094
		// (set) Token: 0x06004DD1 RID: 19921 RVA: 0x00143E9C File Offset: 0x0014209C
		public TransitionInfo AtLeast2AvailableCopy { get; private set; }

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x00143EA5 File Offset: 0x001420A5
		// (set) Token: 0x06004DD3 RID: 19923 RVA: 0x00143EAD File Offset: 0x001420AD
		public TransitionInfo AtLeast3AvailableCopy { get; private set; }

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x00143EB6 File Offset: 0x001420B6
		// (set) Token: 0x06004DD5 RID: 19925 RVA: 0x00143EBE File Offset: 0x001420BE
		public TransitionInfo AtLeast4AvailableCopy { get; private set; }

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x00143EC7 File Offset: 0x001420C7
		// (set) Token: 0x06004DD7 RID: 19927 RVA: 0x00143ECF File Offset: 0x001420CF
		public DatabaseCopyRedundancy[] DbCopies { get; private set; }

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x06004DD8 RID: 19928 RVA: 0x00143ED8 File Offset: 0x001420D8
		internal bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06004DD9 RID: 19929 RVA: 0x00143EDB File Offset: 0x001420DB
		bool IConfigurable.IsValid
		{
			get
			{
				return this.IsValid;
			}
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06004DDA RID: 19930 RVA: 0x00143EE3 File Offset: 0x001420E3
		internal ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06004DDB RID: 19931 RVA: 0x00143EE6 File Offset: 0x001420E6
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return this.ObjectState;
			}
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x00143EEE File Offset: 0x001420EE
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x00143EF6 File Offset: 0x001420F6
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x00143EFD File Offset: 0x001420FD
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}
	}
}
